using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.ConsoleUI.Controllers;

public class AbilityDexController(IAbilityRepository _repository) : IEntityController
{
    public string EntityName => "AbilityDex";

    public async Task RunController()
    {
        int selection = -1;

        while (selection != 0)
        {
            Console.WriteLine("\n=== AbilityDex ===");
            Console.WriteLine("1. List all abilities");
            Console.WriteLine("2. Search by ID");
            Console.WriteLine("3. Search by name");
            Console.WriteLine("4. Add an ability");
            Console.WriteLine("5. Delete an ability");
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
        var abilities = await _repository.GetAllAsync();

        if (!abilities.Any())
        {
            Console.WriteLine("No abilities found.");
            return;
        }

        foreach (var ability in abilities)
            Console.WriteLine($"[{ability.AbilityId}] {ability.Name}");
    }

    private async Task SearchById()
    {
        Console.Write("Enter ability ID: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var ability = await _repository.GetByIdAsync(id);
        if (ability is null)
        {
            Console.WriteLine("Ability not found.");
            return;
        }

        Console.WriteLine(ability.ToDex());
    }

    private async Task SearchByName()
    {
        Console.Write("Enter ability name: ");
        var name = Console.ReadLine() ?? string.Empty;

        var ability = await _repository.GetByNameAsync(name);
        if (ability is null)
        {
            Console.WriteLine("Ability not found.");
            return;
        }

        Console.WriteLine(ability.ToDex());
    }

    private async Task Add()
    {
        Console.Write("Ability ID: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.Write("Description: ");
        var description = Console.ReadLine() ?? string.Empty;

        var ability = new Ability(id, name, description);
        await _repository.AddAsync(ability);
        Console.WriteLine($"Ability '{name}' added successfully.");
    }

    private async Task Delete()
    {
        Console.Write("Enter ability ID to delete: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var ability = await _repository.GetByIdAsync(id);
        if (ability is null)
        {
            Console.WriteLine("Ability not found.");
            return;
        }

        await _repository.DeleteAsync(id);
        Console.WriteLine($"Ability '{ability.Name}' deleted.");
    }
}
