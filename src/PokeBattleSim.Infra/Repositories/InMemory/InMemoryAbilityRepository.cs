using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.Infra.Repositories.InMemory
{
    public class InMemoryAbilityRepository : IAbilityRepository
    {
        private readonly Dictionary<uint, Ability> _store = [];

        public Task<Ability?> GetByIdAsync(uint id)
        {
            _store.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task<Ability?> GetByNameAsync(string name)
        {
            var entity = _store.Values.FirstOrDefault(a => a.Name == name);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Ability>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Ability>>(_store.Values);
        }

        public Task AddAsync(Ability entity)
        {
            _store[entity.AbilityId] = entity;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Ability entity)
        {
            _store[entity.AbilityId] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(uint id)
        {
            _store.Remove(id);
            return Task.CompletedTask;
        }
    }
}
