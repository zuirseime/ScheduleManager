namespace ScheduleManager.Data.Repositories;

public interface IDeletable<T> where T : class
{
    public Task DeleteAsync(T entity);
}
