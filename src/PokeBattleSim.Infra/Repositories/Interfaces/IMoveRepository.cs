using PokeBattleSim.Data.Entities.Pokemon.Moves;

namespace PokeBattleSim.Infra.Repositories.Interfaces
{
    public interface IMoveRepository : IRepository<Move>
    {
        Task<Move?> GetByNameAsync(string name);
    }
}
