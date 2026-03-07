using PokeBattleSim.Data.Entities.Pokemon;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.Infra.Repositories.InMemory
{
    public class InMemoryPokemonDexRepository : IPokemonDexRepository
    {
        private readonly Dictionary<uint, PokemonDex> _store = [];

        public Task<PokemonDex?> GetByIdAsync(uint id)
        {
            _store.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task<PokemonDex?> GetByNameAsync(string name)
        {
            var entity = _store.Values.FirstOrDefault(p => p.BaseInfo.Name == name);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<PokemonDex>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<PokemonDex>>(_store.Values);
        }

        public Task AddAsync(PokemonDex entity)
        {
            _store[entity.BaseInfo.DexNumber] = entity;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(PokemonDex entity)
        {
            _store[entity.BaseInfo.DexNumber] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(uint id)
        {
            _store.Remove(id);
            return Task.CompletedTask;
        }
    }
}
