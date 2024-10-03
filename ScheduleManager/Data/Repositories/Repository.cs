
namespace ScheduleManager.Data.Repositories;

public abstract class Repository<T>(ScheduleContext context) : ICreatable<T>, IReadable<T>, IUpdatable<T>, IDeletable<T> where T : class
{
    public abstract Task<IEnumerable<T>> GetAllAsync();
    public abstract Task<T?> GetByIdAsync(Guid id);
    public abstract Task CreateSync(T entity);
    public abstract Task UpdateAsync(T entity);
    public abstract Task DeleteAsync(T entity);
    protected Task SaveChangesAsync() => context.SaveChangesAsync();
}
