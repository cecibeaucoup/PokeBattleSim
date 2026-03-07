using PokeBattleSim.Data.Entities.Pokemon.Moves;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.Infra.Repositories.InMemory
{
    public class InMemoryMoveRepository : IMoveRepository
    {
        private readonly Dictionary<uint, Move> _store = [];

        public Task<Move?> GetByIdAsync(uint id)
        {
            _store.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task<Move?> GetByNameAsync(string name)
        {
            var entity = _store.Values.FirstOrDefault(m => m.Name == name);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Move>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Move>>(_store.Values);
        }

        public Task AddAsync(Move entity)
        {
            _store[entity.MoveId] = entity;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Move entity)
        {
            _store[entity.MoveId] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(uint id)
        {
            _store.Remove(id);
            return Task.CompletedTask;
        }
    }
}
