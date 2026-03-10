using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.ConsoleUI.Controllers;

public class TrainerController(ITrainerSheetRepository _repository) : IEntityController
{
    public string EntityName => "Trainer";

    public async Task RunController()
    {
        int selection = -1;

        while (selection != 0)
        {
            Console.WriteLine("\n=== Trainerdex ===");
            Console.WriteLine("1. List all trainers");
            Console.WriteLine("2. Search by ID");
            Console.WriteLine("3. Search by name");
            Console.WriteLine("4. Add a trainer");
            Console.WriteLine("5. Delete a trainer");
            Console.WriteLine("0. Back");

            try
            {
                selection = int.Parse(Console.ReadLine() ?? string.Empty);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            switch (selection)
            {
                case 1: await ListAll(); break;
                case 2: await SearchById(); break;
                case 3: await SearchByName(); break;
                case 4: await Add(); break;
                case 5: await Delete(); break;
                case 0: break;
                default: Console.WriteLine("Invalid selection."); break;
            }
        }
    }

    private async Task ListAll()
    {
        var trainers = await _repository.GetAllAsync();

        if (!trainers.Any())
        {
            Console.WriteLine("No trainers found.");
            return;
        }

        foreach (var trainer in trainers)
            Console.WriteLine($"[{trainer.TrainerId}] {trainer.Name} - Lv.{trainer.TrainerLevel} {trainer.Rank} ({trainer.CurrentRegion})");
    }

    private async Task SearchById()
    {
        Console.Write("Enter trainer ID: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var trainer = await _repository.GetByIdAsync(id);
        if (trainer is null)
        {
            Console.WriteLine("Trainer not found.");
            return;
        }

        DisplayTrainer(trainer);
    }

    private async Task SearchByName()
    {
        Console.Write("Enter trainer name: ");
        var name = Console.ReadLine() ?? string.Empty;

        var trainer = await _repository.GetByNameAsync(name);
        if (trainer is null)
        {
            Console.WriteLine("Trainer not found.");
            return;
        }

        DisplayTrainer(trainer);
    }

    private async Task Add()
    {
        Console.Write("Trainer ID: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.Write("Age: ");
        if (!uint.TryParse(Console.ReadLine(), out var age))
        {
            Console.WriteLine("Invalid age.");
            return;
        }

        Console.WriteLine($"Regions: {string.Join(", ", Enum.GetNames<Regions>())}");
        Console.Write("Region: ");
        if (!Enum.TryParse<Regions>(Console.ReadLine(), true, out var region))
            region = Regions.None;

        var trainer = new TrainerSheet(id, name, age, region);
        await _repository.AddAsync(trainer);
        Console.WriteLine($"Trainer '{name}' added successfully.");
    }

    private async Task Delete()
    {
        Console.Write("Enter trainer ID to delete: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var trainer = await _repository.GetByIdAsync(id);
        if (trainer is null)
        {
            Console.WriteLine("Trainer not found.");
            return;
        }

        await _repository.DeleteAsync(id);
        Console.WriteLine($"Trainer '{trainer.Name}' deleted.");
    }

    private static void DisplayTrainer(TrainerSheet trainer)
    {
        Console.WriteLine($"\n--- {trainer.Name} (ID: {trainer.TrainerId}) ---");
        Console.WriteLine($"Age: {trainer.Age}");
        Console.WriteLine($"Region: {trainer.CurrentRegion}");
        Console.WriteLine($"Level: {trainer.TrainerLevel} | Rank: {trainer.Rank}");
        Console.WriteLine($"Carry Limit: {trainer.CarryLimit} | Pokemon Limit: {trainer.PokemonLimit}");
        Console.WriteLine($"Badges: {(trainer.Badges.Any() ? string.Join(", ", trainer.Badges.Select(b => b.Name)) : "None")}");
        Console.WriteLine($"Traits: {(trainer.Traits.Any() ? string.Join(", ", trainer.Traits.Select(t => t.Name)) : "None")}");
        Console.WriteLine($"Party: {trainer.PokemonParty.Count()} | Box: {trainer.PokemonBox.Count()}");
    }
}
