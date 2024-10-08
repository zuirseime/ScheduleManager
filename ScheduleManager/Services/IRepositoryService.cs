namespace ScheduleManager.Services;

public interface IRepositoryService<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate);
    public Task<T?> GetByIdAsync(Guid id);
    public Task CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
}
