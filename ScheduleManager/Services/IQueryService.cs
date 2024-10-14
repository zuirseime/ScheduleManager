using ScheduleManager.Data.Models;

namespace ScheduleManager.Services;

public interface IQueryService<T> where T : Entity
{
    public IEnumerable<T> Sort(IEnumerable<T> values, params string[] properties);
    public IEnumerable<T> Filter(IEnumerable<T> values, Func<T, bool> predicate);
    public IEnumerable<T> Find(IEnumerable<T> values, string query);
}
