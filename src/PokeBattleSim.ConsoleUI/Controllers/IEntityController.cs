namespace PokeBattleSim.ConsoleUI.Controllers;

public interface IEntityController
{
    string EntityName { get; }
    Task RunController();
}
