using Microsoft.Extensions.DependencyInjection;
using PokeBattleSim.Infra.Repositories.Interfaces;
using PokeBattleSim.Infra.Repositories.InMemory;

namespace PokeBattleSim.ConsoleUI;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IAbilityRepository, InMemoryAbilityRepository>();
        services.AddSingleton<IMoveRepository, InMemoryMoveRepository>();
        services.AddSingleton<IPokemonDexRepository, InMemoryPokemonDexRepository>();
        services.AddSingleton<ITrainerSheetRepository, InMemoryTrainerSheetRepository>();
        services.AddSingleton<ITraitRepository, InMemoryTraitRepository>();

        services.AddHostedService<ConsoleController>();
    }
}