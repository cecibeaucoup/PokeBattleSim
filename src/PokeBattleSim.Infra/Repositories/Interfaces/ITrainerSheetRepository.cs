using PokeBattleSim.Data.Entities.Trainer;

namespace PokeBattleSim.Infra.Repositories.Interfaces
{
    public interface ITrainerSheetRepository : IRepository<TrainerSheet>
    {
        Task<TrainerSheet?> GetByNameAsync(string name);
    }
}
