namespace ScheduleManager.Services;

public interface IQueryService<T> where T : class
{
    public void Sort(IEnumerable<T> values);
    public Task<IEnumerable<T>> Filter(Func<T, bool> predicate);
    public Task<IEnumerable<T>> Find(string query);
}
