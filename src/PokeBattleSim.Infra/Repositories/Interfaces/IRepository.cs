using PokeBattleSim.Data.Contracts.Repositories;

namespace PokeBattleSim.Infra.Repositories.Interfaces
{
    public interface IRepository<T> : IEntityRepository<T> where T : class
    {
    }
}
