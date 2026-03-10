using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokeBattleSim.ConsoleUI;


namespace PokeBattleSim.ConsoleUI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(Startup.ConfigureServices)
            .Build();

        await host.RunAsync();
    }
}
