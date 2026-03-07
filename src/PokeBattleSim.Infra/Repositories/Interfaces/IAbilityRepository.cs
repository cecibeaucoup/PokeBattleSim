using PokeBattleSim.Data.Entities.Pokemon;

namespace PokeBattleSim.Infra.Repositories.Interfaces
{
    public interface IAbilityRepository : IRepository<Ability>
    {
        Task<Ability?> GetByNameAsync(string name);
    }
}
