namespace ScheduleManager.Services;

public interface IQueryService<T> where T : class
{
    public Task<IEnumerable<T>> Sort(IEnumerable<T> entities);
    public Task<IEnumerable<T>> Filter(IEnumerable<T> entities, Func<T, bool> predicate);
    public Task<IEnumerable<T>> Find(IEnumerable<T> entities, string query);
}
