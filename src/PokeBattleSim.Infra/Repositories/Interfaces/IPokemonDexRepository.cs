using PokeBattleSim.Data.Entities.Pokemon;

namespace PokeBattleSim.Infra.Repositories.Interfaces
{
    public interface IPokemonDexRepository : IRepository<PokemonDex>
    {
        Task<PokemonDex?> GetByNameAsync(string name);
    }
}
