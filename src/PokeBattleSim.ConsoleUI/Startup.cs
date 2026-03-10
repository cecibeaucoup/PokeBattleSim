using Microsoft.Extensions.DependencyInjection;
using PokeBattleSim.Infra.Repositories.Interfaces;
using PokeBattleSim.Infra.Repositories.InMemory;
using PokeBattleSim.ConsoleUI.Controllers;

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

        services.AddSingleton<IEntityController, TrainerController>();
        services.AddSingleton<IEntityController, PokeDexController>();
        services.AddSingleton<IEntityController, MoveDexController>();
        services.AddSingleton<IEntityController, AbilityDexController>();
        services.AddSingleton<IEntityController, TraitDexController>();

        services.AddHostedService<ConsoleController>();
    }
}