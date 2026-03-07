using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.Infra.Repositories.InMemory
{
    public class InMemoryTraitRepository : ITraitRepository
    {
        private readonly Dictionary<uint, Trait> _store = [];

        public Task<Trait?> GetByIdAsync(uint id)
        {
            _store.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task<Trait?> GetByNameAsync(string name)
        {
            var entity = _store.Values.FirstOrDefault(t => t.Name == name);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Trait>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Trait>>(_store.Values);
        }

        public Task AddAsync(Trait entity)
        {
            _store[entity.TraitId] = entity;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Trait entity)
        {
            _store[entity.TraitId] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(uint id)
        {
            _store.Remove(id);
            return Task.CompletedTask;
        }
    }
}
