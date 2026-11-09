using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Battler.System;
using Xunit;

namespace PokeBattleSim.Data.Tests;

public class BattleEngineTests
{
    [Fact]
    public void ExecuteRound_AdvancesToCompletedAndRecordsLifecycle()
    {
        var move = CreateMove("Tackle", new MoveStats(0, 20, 0, 20, 0));
        var attacker = new CombatantState("a", "Attacker", 10, 4, 10, 1, 0);
        var defender = new CombatantState("d", "Defender", 10, 4, 1, 1, 0);
        var state = new BattleState([attacker, defender]);
        var engine = new BattleEngine(state, new SeededBattleRoller(7));

        engine.ExecuteRound([new BattleAction("a", "d", move)]);

        Assert.False(state.IsComplete);
        Assert.Equal(BattlePhase.EndOfRound, state.Phase);
        Assert.Equal(1, state.Round);
        Assert.Contains(state.Events, battleEvent => battleEvent is ActionDeclaredEvent);
        Assert.Contains(state.Events, battleEvent => battleEvent is HitResolvedEvent { Hit: true });
        Assert.Contains(state.Events, battleEvent => battleEvent is RoundEndedEvent);
        Assert.Equal(3, attacker.Energy);
        Assert.True(defender.Wounds < defender.MaxWounds);
    }

    [Fact]
    public void ExecuteRound_TagRulesProduceEffectsAndUnsupportedTagsAreVisible()
    {
        var move = CreateMove("Empowered", new MoveStats(0, 20, 0, 1, 0),
            new TagBinding(MechanicTag.ModifyPriority, ResolutionTag.Up),
            new TagBinding(MechanicTag.ModifyDamage, ResolutionTag.Up),
            new TagBinding(MechanicTag.STAB, ResolutionTag.NormalType));
        var state = new BattleState([
            new CombatantState("a", "Attacker", 10, 4, 10, 1, 0),
            new CombatantState("d", "Defender", 10, 4, 1, 1, 0)
        ]);
        var engine = new BattleEngine(state, new SeededBattleRoller(11));

        engine.ExecuteRound([new BattleAction("a", "d", move)]);

        Assert.Contains(state.Events, battleEvent =>
            battleEvent is EffectAppliedEvent { EffectType: nameof(ChangePriorityEffect) });
        Assert.Contains(state.Events, battleEvent =>
            battleEvent is EffectAppliedEvent { EffectType: nameof(ChangeDamageEffect) });
        Assert.Contains(state.Events, battleEvent => battleEvent is UnsupportedTagEvent
            { Tag: { Mechanic: MechanicTag.STAB } });
        Assert.Equal(1, state.GetCombatant("a").RoundPriorityModifier);
    }

    [Fact]
    public void SeededRoller_ReplaysIdenticalValues()
    {
        var first = new SeededBattleRoller(42).Roll(8, 6, 4);
        var second = new SeededBattleRoller(42).Roll(8, 6, 4);

        Assert.Equal(first.Values, second.Values);
        Assert.Equal(first.Successes, second.Successes);
    }

    [Fact]
    public void AnotherLifeSlice_ProjectsStructuredEventsToBattleLog()
    {
        var move = CreateMove("Hydro Pump", new MoveStats(0, 20, 0, 20, 0));
        var state = new BattleState([
            new CombatantState("milotic", "Milotic", 20, 4, 10, 1, 0),
            new CombatantState("magnezone", "Magnezone", 10, 4, 1, 1, 0)
        ]);
        var engine = new BattleEngine(state, new SeededBattleRoller(3));

        engine.ExecuteRound([new BattleAction("milotic", "magnezone", move)]);

        var log = BattleLogProjection.Render(state.Events, state);
        Assert.Contains("Milotic uses Hydro Pump.", log);
        Assert.Contains("Magnezone suffers", log);
        Assert.Contains("Round 1 ended.", log);
    }

    [Fact]
    public void BattleSetup_UsesOneModelForTrainerAndWildEncounters()
    {
        var trainerPokemon = new CombatantState("a", "Trainer Pokemon", 10, 4, 10, 1, 0);
        var wildPokemon = new CombatantState("w", "Wild Pokemon", 10, 4, 1, 1, 0);
        var setup = new BattleSetup(
            EncounterKind.TrainerVsWild,
            [
                new BattleSideSetup("trainer", "trainer-1", [trainerPokemon], ["a"]),
                new BattleSideSetup("wild", null, [wildPokemon], ["w"])
            ],
            5);

        var state = setup.CreateState();

        Assert.Equal(2, state.Combatants.Count);
        Assert.Equal(EncounterKind.TrainerVsWild, setup.EncounterKind);
    }

    [Fact]
    public void BattleSimulationService_CreatesAndRunsConfiguredBattle()
    {
        var setup = new BattleSetup(
            EncounterKind.TrainerVsTrainer,
            [
                new BattleSideSetup("left", "trainer-1",
                    [new CombatantState("a", "Alpha", 10, 4, 10, 1, 0)], ["a"]),
                new BattleSideSetup("right", "trainer-2",
                    [new CombatantState("d", "Delta", 10, 4, 1, 1, 0)], ["d"])
            ],
            9);
        var move = CreateMove("Tackle", new MoveStats(0, 20, 0, 20, 0));

        var state = new BattleSimulationService().ExecuteRound(
            setup,
            [new BattleAction("a", "d", move)]);

        Assert.Equal(1, state.Round);
        Assert.Equal(BattlePhase.EndOfRound, state.Phase);
    }

    private static Move CreateMove(string name, MoveStats stats, params TagBinding[] tags) =>
        new(1, name, PokemonTypes.Normal, "test", stats, true, Grades.D, _tags: tags);
}
