using ScheduleManager.Data.Repositories;
using System.Reflection;

namespace ScheduleManager.Services;

public class QueryService<T>(Repository<T> repository) : IQueryService<T> where T : class
{
    public void Sort(IEnumerable<T> values)
    {
        var sorted = values.ToList();
        sorted.Sort();
    }

    public async Task<IEnumerable<T>> Find(string query)
    {
        var entities = await repository.GetAllAsync();
        IEnumerable<T> list = [];

        foreach (var entity in entities)
        {
            foreach (var property in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.PropertyType != typeof(string))
                    continue;

                if (!property.CanRead)
                    continue;

                if ($"{property.GetValue(entity)}".Contains(query))
                    _ = list.Append(entity);
            }
        }

        return list;
    }

    public async Task<IEnumerable<T>> Filter(Func<T, bool> predicate)
        => (await repository.GetAllAsync()).Where(predicate);
}
