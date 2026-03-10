using System;
using Microsoft.Extensions.Hosting;

namespace PokeBattleSim.ConsoleUI.Controllers;

public class ConsoleController(IEnumerable<IEntityController> _controllers) : IHostedService
{
    int userSelection = -1;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Welcome to the Pokemon Battle Simulator!");
        Console.WriteLine("This is a console application to simulate Pokemon battles in Red Flag's quest system.");

        _ = RunController(cancellationToken);        

        return Task.CompletedTask;
    }

    public async Task RunController(CancellationToken cancellationToken)
    {
        var controllerList = _controllers.ToList();

        while (userSelection != 0)
        {
            Console.WriteLine("\nPlease select an option below:");

            Console.WriteLine("1. Battle - Choose trainers and put them into battle");
            Console.WriteLine("2. Trainerdex - View, add or edit sheets for trainers and their pokemon");
            Console.WriteLine("3. Pokedex - View, add or edit the basic info for pokemon");
            Console.WriteLine("4. MoveDex - View, add or edit the basic info for pokemon moves");
            Console.WriteLine("5. AbilityDex - View, add or edit the basic info for pokemon abilities");
            Console.WriteLine("6. TraitDex - View, add or edit the basic info for trainer traits");
            Console.WriteLine("7. System - View or edit the system configurations.");
            Console.WriteLine("0. Exit");

            try
            {
                userSelection = int.Parse(Console.ReadLine() ?? string.Empty);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number corresponding to the options above.");
                continue;
            }

            switch(userSelection)
            {
                case 1:
                    Console.WriteLine("Battle functionality is not yet implemented.");
                    break;

                case 2:
                    await GetController("Trainer").RunController();
                    break;

                case 3:
                    await GetController("PokeDex").RunController();
                    break;

                case 4:
                    await GetController("MoveDex").RunController();
                    break;

                case 5:
                    await GetController("AbilityDex").RunController();
                    break;

                case 6:
                    await GetController("TraitDex").RunController();
                    break;

                case 7:
                    Console.WriteLine("System functionality is not yet implemented.");
                    break;

                case 0:
                    Console.WriteLine("Thank you for using the Pokemon Battle Simulator! Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }

    private IEntityController GetController(string name) =>
        _controllers.First(c => c.EntityName == name);

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
