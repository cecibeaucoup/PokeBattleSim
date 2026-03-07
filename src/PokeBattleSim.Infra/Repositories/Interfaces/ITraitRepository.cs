using PokeBattleSim.Data.Entities.Trainer;

namespace PokeBattleSim.Infra.Repositories.Interfaces
{
    public interface ITraitRepository : IRepository<Trait>
    {
        Task<Trait?> GetByNameAsync(string name);
    }
}
