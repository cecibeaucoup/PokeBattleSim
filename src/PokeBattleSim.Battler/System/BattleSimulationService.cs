namespace PokeBattleSim.Battler.System;

public sealed class BattleSimulationService
{
    public BattleState ExecuteRound(
        BattleSetup setup,
        IEnumerable<BattleAction> actions)
    {
        ArgumentNullException.ThrowIfNull(setup);
        ArgumentNullException.ThrowIfNull(actions);

        var state = setup.CreateState();
        var engine = new BattleEngine(
            state,
            new SeededBattleRoller(setup.RandomSeed));
        engine.ExecuteRound(actions);
        return state;
    }
}
