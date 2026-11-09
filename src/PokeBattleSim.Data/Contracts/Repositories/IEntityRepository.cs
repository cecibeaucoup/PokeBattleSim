namespace PokeBattleSim.Data.Contracts.Repositories;

public interface IEntityRepository<T> where T : class
{
    Task<T?> GetByIdAsync(uint id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(uint id);
}
