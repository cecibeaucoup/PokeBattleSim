using System;
using Microsoft.Extensions.Hosting;

namespace PokeBattleSim.ConsoleUI;

public class ConsoleController : IHostedService
{
    int userSelection = 0;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Welcome to the Pokemon Battle Simulator!");
        Console.WriteLine("This is a console application to simulate Pokemon battles in Red Flag's quest system.");

        RunController(cancellationToken);        

        return Task.CompletedTask;
    }

    public Task RunController(CancellationToken cancellationToken)
    {
       while (userSelection != 7)
        {
            Console.WriteLine("Please select an option below:");

            Console.WriteLine("1. Battle - Choose trainers and put them into battle");
            Console.WriteLine("2. Trainerdex - View, add or edit sheets for trainers and their pokemon");
            Console.WriteLine("3. Pokedex - View, add or edit the basic info for pokemon");
            Console.WriteLine("4. MoveDex - View, add or edit the basic info for pokemon moves");
            Console.WriteLine("5. AbilityDex - View, add or edit the basic info for pokemon abilities");
            Console.WriteLine("6. System - View or edit the system configurations.");
            Console.WriteLine("7. Exit");

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
                    // Start a new battle
                   Console.WriteLine(new NotImplementedException("Battle functionality is not yet implemented."));
                    break;
                case 2:
                    // Trainerdex
                    Console.WriteLine(new NotImplementedException("Trainerdex functionality is not yet implemented."));
                    break;

                case 3:
                    // Pokedex
                    Console.WriteLine(new NotImplementedException("Pokedex functionality is not yet implemented."));
                    break;

                case 4:
                    // MoveDex
                    Console.WriteLine(new NotImplementedException("MoveDex functionality is not yet implemented."));
                    break;

                case 5:
                    // AbilityDex
                    Console.WriteLine(new NotImplementedException("AbilityDex functionality is not yet implemented."));
                    break;

                case 6:
                    // System
                    Console.WriteLine(new NotImplementedException("System functionality is not yet implemented."));
                    break;

                case 7:
                    // Exit
                    Console.WriteLine("Thank you for using the Pokemon Battle Simulator! Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }

            Console.Clear();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
