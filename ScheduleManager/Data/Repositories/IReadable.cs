namespace ScheduleManager.Data.Repositories;

public interface IReadable<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(Guid id);
}
