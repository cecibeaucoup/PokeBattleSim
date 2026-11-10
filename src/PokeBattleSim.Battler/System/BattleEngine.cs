using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.Battler.System;

public sealed class BattleEngine(
    BattleState _state,
    IBattleRoller _roller,
    SystemContext? _configuration = null,
    TagRuleRegistry? _rules = null)
{
    private readonly SystemContext _configuration = _configuration ?? new();
    private readonly TagRuleRegistry _rules = _rules ?? new TagRuleRegistry([new CombatTagRule()]);

    public BattleState State => _state;

    public void ChangeWeather(Weather weather)
    {
        _state.Environment.SetWeather(weather);
        Publish(new EnvironmentChangedEvent(_state.Round, _state.Environment.Weather, _state.Environment.Terrain), null);
    }

    public void ChangeTerrain(Terrains terrain)
    {
        _state.Environment.SetTerrain(terrain);
        Publish(new EnvironmentChangedEvent(_state.Round, _state.Environment.Weather, _state.Environment.Terrain), null);
    }

    private void PublishCheckpoint(EnvironmentCheckpoint checkpoint, BattleAction? action) =>
        Publish(new EnvironmentCheckpointEvent(_state.Round, checkpoint, action?.ActorId, action?.TargetId), action);

    public void ExecuteRound(IEnumerable<BattleAction> actions)
    {
        TransitionTo(BattlePhase.RoundStart);
        _state.Round++;
        foreach (var combatant in _state.Combatants.Values)
            combatant.ResetRoundModifiers();
        PublishCheckpoint(EnvironmentCheckpoint.RoundStart, null);

        TransitionTo(BattlePhase.ActionCollection);
        var legalActions = actions.Where(action => IsActionLegal(action)).ToList();
        foreach (var action in legalActions)
            PublishCheckpoint(EnvironmentCheckpoint.SpeedPriority, action);

        var orderedActions = legalActions
            .OrderByDescending(action => _state.GetCombatant(action.ActorId).Speed +
                                         _state.GetCombatant(action.ActorId).RoundPriorityModifier)
            .ToList();

        TransitionTo(BattlePhase.ActionResolution);
        foreach (var action in orderedActions)
        {
            if (!IsActionLegal(action))
                continue;

            Publish(new ActionDeclaredEvent(_state.Round, action), action);
            if (action.TargetDefense is DefensePatterns defense)
                Publish(new DefenseSelectedEvent(_state.Round, action.TargetId, action.ActorId, defense), action);

            ResolveAction(action);
        }

        TransitionTo(BattlePhase.KnockoutResolution);
        foreach (var combatant in _state.Combatants.Values.Where(c => c.IsFainted))
        {
            if (_state.Events.OfType<FaintedEvent>().All(e => e.ActorId != combatant.Id))
                Publish(new FaintedEvent(_state.Round, combatant.Id), null);
        }

        TransitionTo(BattlePhase.EndOfRound);
        PublishCheckpoint(EnvironmentCheckpoint.RoundEnd, null);
        Publish(new RoundEndedEvent(_state.Round), null);
        if (IsBattleComplete())
        {
            TransitionTo(BattlePhase.Completed);
            _state.IsComplete = true;
        }
    }

    private void ResolveAction(BattleAction action)
    {
        var actor = _state.GetCombatant(action.ActorId);
        var target = _state.GetCombatant(action.TargetId);
        var move = action.Move.MoveStats;

        ApplyEffects([new ChangeEnergyEffect(actor.Id, actor.Id, -1)]);
        if (actor.IsFainted || target.IsFainted)
            return;

        for (var hitIndex = 0; hitIndex < Math.Max(1, move.NumberOfHits); hitIndex++)
        {
            if (target.IsFainted)
                break;

            TransitionTo(BattlePhase.HitResolution);
            PublishCheckpoint(EnvironmentCheckpoint.HitRoll, action);
            var hitRoll = _roller.Roll(move.HitDice, _configuration.RollsConfiguration.DieSize,
                _configuration.RollsConfiguration.Threshold);
            Publish(new RollResolvedEvent(_state.Round, actor.Id, target.Id, "Hit", move.HitDice,
                move.HitAutos, hitRoll.Successes + move.HitAutos), action);

            var dodge = target.Dodge + GetDefenseBonus(action.TargetDefense);
            var hits = hitRoll.Successes + move.HitAutos;
            var hit = hits >= dodge;
            Publish(new HitResolvedEvent(_state.Round, actor.Id, target.Id, hit, hits, dodge), action);
            if (!hit)
                continue;

            TransitionTo(BattlePhase.DamageResolution);
            PublishCheckpoint(EnvironmentCheckpoint.DamageRoll, action);
            var damageRoll = _roller.Roll(move.DamageDice + actor.RoundDamageModifier,
                _configuration.RollsConfiguration.DieSize, _configuration.RollsConfiguration.Threshold);
            var damage = damageRoll.Successes + move.DamageAutos;
            var defense = target.Defense + GetDefenseBonus(action.TargetDefense);
            var wounds = Math.Max(0, damage - defense);
            Publish(new RollResolvedEvent(_state.Round, actor.Id, target.Id, "Damage", move.DamageDice,
                move.DamageAutos, damage), action);
            var damageEvent = new DamageResolvedEvent(_state.Round, actor.Id, target.Id, wounds, defense);
            Publish(damageEvent, action);
            ApplyEffects(_rules.Evaluate(damageEvent, new BattleRuleContext(_state, action)));
            ApplyEffects([new ChangeWoundsEffect(actor.Id, target.Id, -wounds)]);
        }
    }

    private bool IsActionLegal(BattleAction action) =>
        _state.Combatants.ContainsKey(action.ActorId) &&
        _state.Combatants.ContainsKey(action.TargetId) &&
        !_state.GetCombatant(action.ActorId).IsFainted &&
        !_state.GetCombatant(action.TargetId).IsFainted &&
        _state.GetCombatant(action.ActorId).Energy > 0;

    private static int GetDefenseBonus(DefensePatterns? defense) =>
        defense is DefensePatterns.Defend ? 2 : 0;

    private void Publish(BattleEvent battleEvent, BattleAction? action)
    {
        _state.Events.Add(battleEvent);
        ApplyEffects(_rules.Evaluate(battleEvent, new BattleRuleContext(_state, action)));
    }

    private void ApplyEffects(IEnumerable<BattleEffect> effects)
    {
        foreach (var effect in effects)
        {
            var target = effect.TargetId is null ? null : _state.GetCombatant(effect.TargetId);
            switch (effect)
            {
                case ChangeWoundsEffect wounds when target is not null:
                    target.ChangeWounds(wounds.Amount);
                    break;
                case ChangeEnergyEffect energy when target is not null:
                    target.ChangeEnergy(energy.Amount);
                    break;
                case ChangePriorityEffect priority when target is not null:
                    target.ChangeRoundPriority(priority.Amount);
                    break;
                case ChangeDamageEffect damage when target is not null:
                    target.ChangeRoundDamage(damage.Amount);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported effect type {effect.GetType().Name}.");
            }

            _state.Events.Add(new EffectAppliedEvent(_state.Round, effect.SourceId, effect.TargetId,
                effect.GetType().Name));
        }
    }

    private void TransitionTo(BattlePhase next)
    {
        if (!IsValidTransition(_state.Phase, next))
            throw new InvalidOperationException($"Cannot transition from {_state.Phase} to {next}.");

        var previous = _state.Phase;
        _state.Phase = next;
        _state.Events.Add(new PhaseChangedEvent(_state.Round, previous, next));
    }

    private static bool IsValidTransition(BattlePhase current, BattlePhase next) =>
        (current, next) switch
        {
            (BattlePhase.Setup, BattlePhase.RoundStart) => true,
            (BattlePhase.EndOfRound, BattlePhase.RoundStart) => true,
            (BattlePhase.RoundStart, BattlePhase.ActionCollection) => true,
            (BattlePhase.ActionCollection, BattlePhase.ActionResolution) => true,
            (BattlePhase.ActionResolution, BattlePhase.HitResolution) => true,
            (BattlePhase.HitResolution, BattlePhase.DamageResolution) => true,
            (BattlePhase.HitResolution, BattlePhase.KnockoutResolution) => true,
            (BattlePhase.DamageResolution, BattlePhase.HitResolution) => true,
            (BattlePhase.DamageResolution, BattlePhase.KnockoutResolution) => true,
            (BattlePhase.KnockoutResolution, BattlePhase.EndOfRound) => true,
            (BattlePhase.EndOfRound, BattlePhase.Completed) => true,
            _ => false
        };

    private bool IsBattleComplete() =>
        _state.Combatants.Values.Count(combatant => !combatant.IsFainted) <= 1;
}
