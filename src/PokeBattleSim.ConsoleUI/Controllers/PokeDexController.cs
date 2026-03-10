using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Data.Entities.Pokemon.Dex;
using PokeBattleSim.Data.Entities.Pokemon.Stats;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.ConsoleUI.Controllers;

public class PokeDexController(IPokemonDexRepository _repository) : IEntityController
{
    public string EntityName => "PokeDex";

    public async Task RunController()
    {
        int selection = -1;

        while (selection != 0)
        {
            Console.WriteLine("\n=== Pokedex ===");
            Console.WriteLine("1. List all pokemon");
            Console.WriteLine("2. Search by ID");
            Console.WriteLine("3. Search by name");
            Console.WriteLine("4. Add a pokemon");
            Console.WriteLine("5. Delete a pokemon");
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
        var pokemon = await _repository.GetAllAsync();

        if (!pokemon.Any())
        {
            Console.WriteLine("No pokemon found.");
            return;
        }

        foreach (var p in pokemon)
            Console.WriteLine($"#{p.BaseInfo.DexNumber} {p.BaseInfo.Name} ({p.BaseInfo.PrimaryType}{(p.BaseInfo.SecondaryType != PokemonTypes.None ? $"/{p.BaseInfo.SecondaryType}" : "")})");
    }

    private async Task SearchById()
    {
        Console.Write("Enter dex number: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid dex number.");
            return;
        }

        var pokemon = await _repository.GetByIdAsync(id);
        if (pokemon is null)
        {
            Console.WriteLine("Pokemon not found.");
            return;
        }

        Console.WriteLine(pokemon.ToDex());
    }

    private async Task SearchByName()
    {
        Console.Write("Enter pokemon name: ");
        var name = Console.ReadLine() ?? string.Empty;

        var pokemon = await _repository.GetByNameAsync(name);
        if (pokemon is null)
        {
            Console.WriteLine("Pokemon not found.");
            return;
        }

        Console.WriteLine(pokemon.ToDex());
    }

    private async Task Add()
    {
        Console.Write("Dex Number: ");
        if (!uint.TryParse(Console.ReadLine(), out var dexNumber))
        {
            Console.WriteLine("Invalid dex number.");
            return;
        }

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.Write("Length (m): ");
        if (!uint.TryParse(Console.ReadLine(), out var length))
        {
            Console.WriteLine("Invalid length.");
            return;
        }

        Console.Write("Weight (kg): ");
        if (!uint.TryParse(Console.ReadLine(), out var weight))
        {
            Console.WriteLine("Invalid weight.");
            return;
        }

        Console.WriteLine($"Morphologies: {string.Join(", ", Enum.GetNames<Morphologies>())}");
        Console.Write("Morphology: ");
        if (!Enum.TryParse<Morphologies>(Console.ReadLine(), true, out var morphology))
        {
            Console.WriteLine("Invalid morphology.");
            return;
        }

        Console.WriteLine($"Types: {string.Join(", ", Enum.GetNames<PokemonTypes>())}");
        Console.Write("Primary Type: ");
        if (!Enum.TryParse<PokemonTypes>(Console.ReadLine(), true, out var priType))
        {
            Console.WriteLine("Invalid type.");
            return;
        }

        Console.Write("Secondary Type (leave empty for None): ");
        var secTypeInput = Console.ReadLine();
        var secType = PokemonTypes.None;
        if (!string.IsNullOrWhiteSpace(secTypeInput))
            Enum.TryParse(secTypeInput, true, out secType);

        Console.WriteLine($"Egg Groups: {string.Join(", ", Enum.GetNames<EggGroups>())}");
        Console.Write("Egg Group 1: ");
        if (!Enum.TryParse<EggGroups>(Console.ReadLine(), true, out var egg1))
            egg1 = EggGroups.NoGroup;

        Console.Write("Egg Group 2 (leave empty for none): ");
        var egg2Input = Console.ReadLine();
        var egg2 = EggGroups.NoGroup;
        if (!string.IsNullOrWhiteSpace(egg2Input))
            Enum.TryParse(egg2Input, true, out egg2);

        var baseInfo = new PokemonDexBaseInfo(name, dexNumber, length, weight, morphology, priType, secType, egg1, egg2);
        var gameInfo = new PokemonDexGameInfo(
            PokeAttribute.GetDefaultStats(),
            PokeSkill.GetDefaultStats(),
            [MobilityTypes.Ground],
            [Senses.Vision, Senses.Hearing]
        );

        var pokemon = new PokemonDex(baseInfo, gameInfo);
        await _repository.AddAsync(pokemon);
        Console.WriteLine($"Pokemon '{name}' added successfully.");
    }

    private async Task Delete()
    {
        Console.Write("Enter dex number to delete: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid dex number.");
            return;
        }

        var pokemon = await _repository.GetByIdAsync(id);
        if (pokemon is null)
        {
            Console.WriteLine("Pokemon not found.");
            return;
        }

        await _repository.DeleteAsync(id);
        Console.WriteLine($"Pokemon '{pokemon.BaseInfo.Name}' deleted.");
    }
}
