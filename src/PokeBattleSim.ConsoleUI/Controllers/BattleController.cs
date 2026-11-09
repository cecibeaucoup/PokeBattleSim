using PokeBattleSim.Battler.System;
using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;

namespace PokeBattleSim.ConsoleUI.Controllers;

public sealed class BattleController(BattleSimulationService _simulationService) : IEntityController
{
    public string EntityName => "Battle";

    public async Task RunController()
    {
        Console.WriteLine("\n=== Battle Setup ===");
        Console.WriteLine("1. Run deterministic sample battle");
        Console.WriteLine("0. Back");
        Console.Write("Selection: ");

        if (!int.TryParse(Console.ReadLine(), out var selection) || selection == 0)
            return;

        if (selection != 1)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        var setup = new BattleSetup(
            EncounterKind.TrainerVsTrainer,
            [
                new BattleSideSetup("left", "trainer-1",
                    [new CombatantState("left-pokemon", "Sample Alpha", 10, 4, 10, 1, 0)],
                    ["left-pokemon"]),
                new BattleSideSetup("right", "trainer-2",
                    [new CombatantState("right-pokemon", "Sample Delta", 10, 4, 1, 1, 0)],
                    ["right-pokemon"])
            ],
            17);
        var move = new Move(
            1,
            "Tackle",
            PokemonTypes.Normal,
            "A deterministic sample action.",
            new MoveStats(0, 20, 0, 20, 0),
            true,
            Grades.D);

        var state = _simulationService.ExecuteRound(
            setup,
            [new BattleAction("left-pokemon", "right-pokemon", move)]);

        Console.WriteLine(BattleLogProjection.Render(state.Events, state));
        await Task.CompletedTask;
    }
}
