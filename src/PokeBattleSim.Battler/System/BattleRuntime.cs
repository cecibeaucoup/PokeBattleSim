using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Data.Entities;

namespace PokeBattleSim.Battler.System;

public enum EnvironmentCheckpoint
{
    RoundStart,
    SpeedPriority,
    HitRoll,
    DamageRoll,
    RoundEnd
}

public enum BattlePhase
{
    Setup,
    RoundStart,
    ActionCollection,
    ActionResolution,
    HitResolution,
    DamageResolution,
    KnockoutResolution,
    EndOfRound,
    Completed
}

public sealed class CombatantState(
    string _id,
    string _name,
    int _maxWounds,
    int _maxEnergy,
    int _speed,
    int _dodge,
    int _defense,
    IEnumerable<TagBinding>? _tags = null)
{
    public string Id { get; } = _id;
    public string Name { get; } = _name;
    public int MaxWounds { get; } = _maxWounds;
    public int Wounds { get; private set; } = _maxWounds;
    public int MaxEnergy { get; } = _maxEnergy;
    public int Energy { get; private set; } = _maxEnergy;
    public int Speed { get; } = _speed;
    public int Dodge { get; } = _dodge;
    public int Defense { get; } = _defense;
    public bool IsFainted => Wounds <= 0;
    public ISet<TagBinding> Tags { get; } = new HashSet<TagBinding>(_tags ?? []);
    public int RoundPriorityModifier { get; private set; }
    public int RoundDamageModifier { get; private set; }

    public void ChangeWounds(int amount) => Wounds = Math.Clamp(Wounds + amount, 0, MaxWounds);
    public void ChangeEnergy(int amount) => Energy = Math.Clamp(Energy + amount, 0, MaxEnergy);
    public void ChangeRoundPriority(int amount) => RoundPriorityModifier += amount;
    public void ChangeRoundDamage(int amount) => RoundDamageModifier += amount;
    public void ResetRoundModifiers()
    {
        RoundPriorityModifier = 0;
        RoundDamageModifier = 0;
    }
}

public sealed record BattleAction(
    string ActorId,
    string TargetId,
    Move Move,
    AttackPatterns AttackPattern = AttackPatterns.Aggresive,
    DefensePatterns? TargetDefense = null);

public sealed class BattleState(IEnumerable<CombatantState> _combatants, BattleEnvironment? _environment = null)
{
    private readonly Dictionary<string, CombatantState> _combatants =
        _combatants.ToDictionary(c => c.Id, StringComparer.Ordinal);

    public BattleEnvironment Environment { get; } = _environment ?? new BattleEnvironment();

    public int Round { get; internal set; }
    public BattlePhase Phase { get; internal set; } = BattlePhase.Setup;
    public bool IsComplete { get; internal set; }
    public IReadOnlyDictionary<string, CombatantState> Combatants => _combatants;
    public List<BattleEvent> Events { get; } = [];
    public List<string> Diagnostics { get; } = [];

    public CombatantState GetCombatant(string id) =>
        _combatants.TryGetValue(id, out var combatant)
            ? combatant
            : throw new KeyNotFoundException($"Unknown combatant '{id}'.");

    public void AddDiagnostic(string diagnostic) => Diagnostics.Add(diagnostic);
}

public abstract record BattleEvent(int Round, string? ActorId, string? TargetId);
public sealed record PhaseChangedEvent(int Round, BattlePhase From, BattlePhase To) : BattleEvent(Round, null, null);
public sealed record ActionDeclaredEvent(int Round, BattleAction Action) : BattleEvent(Round, Action.ActorId, Action.TargetId);
public sealed record DefenseSelectedEvent(int Round, string ActorId, string TargetId, DefensePatterns Defense) : BattleEvent(Round, ActorId, TargetId);
public sealed record RollResolvedEvent(int Round, string ActorId, string TargetId, string Kind, int Dice, int AutomaticSuccesses, int Successes) : BattleEvent(Round, ActorId, TargetId);
public sealed record HitResolvedEvent(int Round, string ActorId, string TargetId, bool Hit, int Hits, int Dodge) : BattleEvent(Round, ActorId, TargetId);
public sealed record DamageResolvedEvent(int Round, string ActorId, string TargetId, int Damage, int Defense) : BattleEvent(Round, ActorId, TargetId);
public sealed record EffectAppliedEvent(int Round, string? ActorId, string? TargetId, string EffectType) : BattleEvent(Round, ActorId, TargetId);
public sealed record FaintedEvent(int Round, string ActorId) : BattleEvent(Round, ActorId, null);
public sealed record RoundEndedEvent(int Round) : BattleEvent(Round, null, null);
public sealed record UnsupportedTagEvent(int Round, string OwnerId, TagBinding Tag) : BattleEvent(Round, OwnerId, null);
public sealed record EnvironmentChangedEvent(int Round, Weather Weather, Terrains Terrain) : BattleEvent(Round, null, null);
public sealed record EnvironmentCheckpointEvent(int Round, EnvironmentCheckpoint Checkpoint, string? ActorId = null, string? TargetId = null)
    : BattleEvent(Round, ActorId, TargetId);

public abstract record BattleEffect(string? SourceId, string? TargetId, EffectLifetime Lifetime);
public enum EffectLifetime
{
    Hit,
    Action,
    Round,
    Field,
    Switch
}

public sealed record ChangeWoundsEffect(string? SourceId, string TargetId, int Amount, EffectLifetime Lifetime = EffectLifetime.Hit)
    : BattleEffect(SourceId, TargetId, Lifetime);
public sealed record ChangeEnergyEffect(string? SourceId, string TargetId, int Amount, EffectLifetime Lifetime = EffectLifetime.Action)
    : BattleEffect(SourceId, TargetId, Lifetime);
public sealed record ChangePriorityEffect(string? SourceId, string TargetId, int Amount, EffectLifetime Lifetime = EffectLifetime.Round)
    : BattleEffect(SourceId, TargetId, Lifetime);
public sealed record ChangeDamageEffect(string? SourceId, string TargetId, int Amount, EffectLifetime Lifetime = EffectLifetime.Round)
    : BattleEffect(SourceId, TargetId, Lifetime);

public sealed record RollResult(IReadOnlyList<int> Values, int Successes);

public interface IBattleRoller
{
    RollResult Roll(int dice, uint dieSize, uint threshold);
}

public sealed class SeededBattleRoller(int _seed) : IBattleRoller
{
    private readonly Random _random = new(_seed);

    public RollResult Roll(int dice, uint dieSize, uint threshold)
    {
        if (dice < 0 || dieSize == 0 || threshold == 0 || threshold > dieSize)
            throw new ArgumentOutOfRangeException(nameof(dice), "Roll configuration is invalid.");

        var values = new int[dice];
        var successes = 0;
        for (var index = 0; index < dice; index++)
        {
            values[index] = _random.Next(1, checked((int)dieSize) + 1);
            if (values[index] >= threshold)
                successes++;
        }

        return new RollResult(values, successes);
    }
}

public sealed class BattleRuleContext(BattleState _state, BattleAction? _action)
{
    public BattleState State { get; } = _state;
    public BattleAction? Action { get; } = _action;

    public BattleEnvironment Environment => State.Environment;
}

public interface ITagRule
{
    IEnumerable<BattleEffect> Evaluate(BattleEvent battleEvent, BattleRuleContext context);
}

public sealed class TagRuleRegistry(IEnumerable<ITagRule>? _rules = null)
{
    private readonly List<ITagRule> _rules = [.. _rules ?? []];

    public void Add(ITagRule rule) => _rules.Add(rule);

    public IEnumerable<BattleEffect> Evaluate(BattleEvent battleEvent, BattleRuleContext context)
    {
        foreach (var rule in _rules)
            foreach (var effect in rule.Evaluate(battleEvent, context))
                yield return effect;
    }
}

public sealed class CombatTagRule : ITagRule
{
    public IEnumerable<BattleEffect> Evaluate(BattleEvent battleEvent, BattleRuleContext context)
    {
        if (context.Action is null)
            yield break;

        if (battleEvent is not ActionDeclaredEvent and not HitResolvedEvent)
            yield break;

        var actor = context.State.GetCombatant(context.Action.ActorId);
        var tags = context.Action.Move.Tags.Concat(actor.Tags).Distinct();

        foreach (var binding in tags)
        {
            switch (binding)
            {
                case { Mechanic: MechanicTag.ModifyPriority } when binding.Resolutions.Contains(ResolutionTag.Up)
                    && battleEvent is ActionDeclaredEvent:
                    yield return new ChangePriorityEffect(actor.Id, actor.Id, 1);
                    break;
                case { Mechanic: MechanicTag.ModifyDamage } when binding.Resolutions.Contains(ResolutionTag.Up)
                    && battleEvent is HitResolvedEvent hit && hit.Hit:
                    yield return new ChangeDamageEffect(actor.Id, actor.Id, 1);
                    break;
                case { Mechanic: MechanicTag.EmptyTag }:
                    break;
                default:
                    if (battleEvent is ActionDeclaredEvent)
                        context.State.Events.Add(new UnsupportedTagEvent(context.State.Round, actor.Id, binding));
                    break;
            }
        }
    }
}
