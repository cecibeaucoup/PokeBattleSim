using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Data.Enums;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.ConsoleUI.Controllers;

public class MoveDexController(IMoveRepository _repository) : IEntityController
{
    public string EntityName => "MoveDex";

    public async Task RunController()
    {
        int selection = -1;

        while (selection != 0)
        {
            Console.WriteLine("\n=== MoveDex ===");
            Console.WriteLine("1. List all moves");
            Console.WriteLine("2. Search by ID");
            Console.WriteLine("3. Search by name");
            Console.WriteLine("4. Add a move");
            Console.WriteLine("5. Delete a move");
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
        var moves = await _repository.GetAllAsync();

        if (!moves.Any())
        {
            Console.WriteLine("No moves found.");
            return;
        }

        foreach (var move in moves)
            Console.WriteLine($"[{move.MoveId}] {move.Name} ({move.Type}{(move.SecondaryType != PokemonTypes.None ? $"/{move.SecondaryType}" : "")}) - Grade {move.MoveGrade}");
    }

    private async Task SearchById()
    {
        Console.Write("Enter move ID: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var move = await _repository.GetByIdAsync(id);
        if (move is null)
        {
            Console.WriteLine("Move not found.");
            return;
        }

        Console.WriteLine(move.ToDex());
    }

    private async Task SearchByName()
    {
        Console.Write("Enter move name: ");
        var name = Console.ReadLine() ?? string.Empty;

        var move = await _repository.GetByNameAsync(name);
        if (move is null)
        {
            Console.WriteLine("Move not found.");
            return;
        }

        Console.WriteLine(move.ToDex());
    }

    private async Task Add()
    {
        Console.Write("Move ID: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.WriteLine($"Types: {string.Join(", ", Enum.GetNames<PokemonTypes>())}");
        Console.Write("Type: ");
        if (!Enum.TryParse<PokemonTypes>(Console.ReadLine(), true, out var type))
        {
            Console.WriteLine("Invalid type.");
            return;
        }

        Console.Write("Description: ");
        var description = Console.ReadLine() ?? string.Empty;

        Console.Write("Is clashable (true/false): ");
        if (!bool.TryParse(Console.ReadLine(), out var isClashable))
            isClashable = false;

        Console.WriteLine($"Grades: {string.Join(", ", Enum.GetNames<Grades>())}");
        Console.Write("Grade: ");
        if (!Enum.TryParse<Grades>(Console.ReadLine(), true, out var grade))
            grade = Grades.E;

        Console.Write("Priority: ");
        int.TryParse(Console.ReadLine(), out var priority);

        Console.Write("Hit Dice: ");
        int.TryParse(Console.ReadLine(), out var hitDice);

        Console.Write("Hit Autos: ");
        int.TryParse(Console.ReadLine(), out var hitAutos);

        Console.Write("Damage Dice: ");
        int.TryParse(Console.ReadLine(), out var damageDice);

        Console.Write("Damage Autos: ");
        int.TryParse(Console.ReadLine(), out var damageAutos);

        var moveStats = new MoveStats(priority, hitDice, hitAutos, damageDice, damageAutos);
        var move = new Move(id, name, type, description, moveStats, isClashable, grade);
        await _repository.AddAsync(move);
        Console.WriteLine($"Move '{name}' added successfully.");
    }

    private async Task Delete()
    {
        Console.Write("Enter move ID to delete: ");
        if (!uint.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var move = await _repository.GetByIdAsync(id);
        if (move is null)
        {
            Console.WriteLine("Move not found.");
            return;
        }

        await _repository.DeleteAsync(id);
        Console.WriteLine($"Move '{move.Name}' deleted.");
    }
}
