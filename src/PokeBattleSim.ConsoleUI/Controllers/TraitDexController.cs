using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.ConsoleUI.Controllers;

public class TraitDexController(ITraitRepository _repository) : IEntityController
{
    public string EntityName => "TraitDex";

    public async Task RunController()
    {
        int selection = -1;

        while (selection != 0)
        {
            Console.WriteLine("\n=== TraitDex ===");
            Console.WriteLine("1. List all traits");
            Console.WriteLine("2. Search by ID");
            Console.WriteLine("3. Search by name");
            Console.WriteLine("4. Add a trait");
            Console.WriteLine("5. Delete a trait");
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
        var traits = await _repository.GetAllAsync();

        if (!traits.Any())
        {
            Console.WriteLine("No traits found.");
            return;
        }

        foreach (var trait in traits)
            Console.WriteLine($"[{trait.TraitId}] {trait.Name}");
    }

    private async Task SearchById()
    {
        Console.Write("Enter trait ID: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var trait = await _repository.GetByIdAsync(id);
        if (trait is null)
        {
            Console.WriteLine("Trait not found.");
            return;
        }

        Console.WriteLine(trait.ToDex());
    }

    private async Task SearchByName()
    {
        Console.Write("Enter trait name: ");
        var name = Console.ReadLine() ?? string.Empty;

        var trait = await _repository.GetByNameAsync(name);
        if (trait is null)
        {
            Console.WriteLine("Trait not found.");
            return;
        }

        Console.WriteLine(trait.ToDex());
    }

    private async Task Add()
    {
        Console.Write("Trait ID: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.Write("Description: ");
        var description = Console.ReadLine() ?? string.Empty;

        var trait = new Trait(id, name, description);
        await _repository.AddAsync(trait);
        Console.WriteLine($"Trait '{name}' added successfully.");
    }

    private async Task Delete()
    {
        Console.Write("Enter trait ID to delete: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var trait = await _repository.GetByIdAsync(id);
        if (trait is null)
        {
            Console.WriteLine("Trait not found.");
            return;
        }

        await _repository.DeleteAsync(id);
        Console.WriteLine($"Trait '{trait.Name}' deleted.");
    }
}
