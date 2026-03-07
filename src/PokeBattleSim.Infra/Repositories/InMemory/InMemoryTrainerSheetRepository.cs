using PokeBattleSim.Data.Entities.Trainer;
using PokeBattleSim.Infra.Repositories.Interfaces;

namespace PokeBattleSim.Infra.Repositories.InMemory
{
    public class InMemoryTrainerSheetRepository : ITrainerSheetRepository
    {
        private readonly Dictionary<uint, TrainerSheet> _store = [];

        public Task<TrainerSheet?> GetByIdAsync(uint id)
        {
            _store.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task<TrainerSheet?> GetByNameAsync(string name)
        {
            var entity = _store.Values.FirstOrDefault(t => t.Name == name);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<TrainerSheet>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TrainerSheet>>(_store.Values);
        }

        public Task AddAsync(TrainerSheet entity)
        {
            _store[entity.TrainerId] = entity;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TrainerSheet entity)
        {
            _store[entity.TrainerId] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(uint id)
        {
            _store.Remove(id);
            return Task.CompletedTask;
        }
    }
}
