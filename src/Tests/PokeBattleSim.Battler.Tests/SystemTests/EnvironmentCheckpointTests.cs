using PokeBattleSim.Battler.System;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;
using Xunit;

namespace PokeBattleSim.Battler.Tests.SystemTests;

public class EnvironmentCheckpointTests
{
    [Fact]
    public void BattleState_CarriesDefaultOrProvidedEnvironment()
    {
        Assert.Equal(Weather.NoWeather, new BattleState([]).Environment.Weather);

        var environment = new PokeBattleSim.Data.Entities.BattleEnvironment(Weather.Rain, Terrains.Mud);
        var state = new BattleState([], environment);

        Assert.Same(environment, state.Environment);
    }

    [Fact]
    public void ExecuteRound_PublishesCheckpointsInRoundOrder()
    {
        var (state, engine) = CreateBattle();

        engine.ExecuteRound([new BattleAction("a", "d", CreateMove())]);

        var checkpoints = state.Events.OfType<EnvironmentCheckpointEvent>().Select(e => e.Checkpoint).ToList();
        Assert.Equal(EnvironmentCheckpoint.RoundStart, checkpoints.First());
        Assert.Equal(EnvironmentCheckpoint.RoundEnd, checkpoints.Last());
        Assert.Equal(
            [EnvironmentCheckpoint.RoundStart, EnvironmentCheckpoint.SpeedPriority, EnvironmentCheckpoint.HitRoll,
             EnvironmentCheckpoint.DamageRoll, EnvironmentCheckpoint.RoundEnd],
            checkpoints.Distinct());
    }

    [Fact]
    public void ExecuteRound_ActionCheckpointsCarryActorAndTarget()
    {
        var (state, engine) = CreateBattle();

        engine.ExecuteRound([new BattleAction("a", "d", CreateMove())]);

        var hitRoll = state.Events.OfType<EnvironmentCheckpointEvent>()
            .First(e => e.Checkpoint == EnvironmentCheckpoint.HitRoll);
        Assert.Equal("a", hitRoll.ActorId);
        Assert.Equal("d", hitRoll.TargetId);
    }

    [Fact]
    public void Rules_SeeEnvironmentChangedEarlierInTheSameBattle()
    {
        var recorder = new RecordingRule();
        var (state, engine) = CreateBattle(new TagRuleRegistry([recorder]));

        engine.ExecuteRound([new BattleAction("a", "d", CreateMove())]);
        engine.ChangeWeather(Weather.Hail);
        engine.ExecuteRound([new BattleAction("a", "d", CreateMove())]);

        var changed = Assert.Single(state.Events.OfType<EnvironmentChangedEvent>());
        Assert.Equal(Weather.Hail, changed.Weather);
        Assert.Equal(Weather.NoWeather, recorder.Seen.First(s => s.Event is EnvironmentCheckpointEvent).Weather);
        Assert.Equal(Weather.Hail, recorder.Seen
            .Last(s => s.Event is EnvironmentCheckpointEvent { Checkpoint: EnvironmentCheckpoint.RoundEnd }).Weather);
    }

    private static (BattleState State, BattleEngine Engine) CreateBattle(TagRuleRegistry? rules = null)
    {
        var state = new BattleState([
            new CombatantState("a", "Attacker", 100, 6, 10, 1, 0),
            new CombatantState("d", "Defender", 100, 6, 1, 1, 0)
        ]);
        return (state, new BattleEngine(state, new SeededBattleRoller(11), _rules: rules));
    }

    private static Move CreateMove() =>
        new(1, "Tackle", PokemonTypes.Normal, "test", new MoveStats(0, 20, 0, 20, 0), true, Grades.D);

    private sealed class RecordingRule : ITagRule
    {
        public List<(BattleEvent Event, Weather Weather)> Seen { get; } = [];

        public IEnumerable<BattleEffect> Evaluate(BattleEvent battleEvent, BattleRuleContext context)
        {
            Seen.Add((battleEvent, context.Environment.Weather));
            return [];
        }
    }
}
