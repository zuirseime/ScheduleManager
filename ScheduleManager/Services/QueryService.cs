using ScheduleManager.Data.Models;
using System.Reflection;

namespace ScheduleManager.Services;

public class QueryService<T> : IQueryService<T> where T : Entity
{
    public IEnumerable<T> Sort(IEnumerable<T> values, params string[] properties)
    {
        if (properties == null || properties.Length == 0) return values;

        IOrderedEnumerable<T>? result = null;

        foreach (var property in properties)
        {
            var info = typeof(T).GetProperty(property, BindingFlags.Public | BindingFlags.Instance) 
                     ?? throw new ArgumentException($"Property '{property}' not found on type '{typeof(T).Name}'");

            if (result is null)
                result = values.OrderBy(x => info.GetValue(x, null));
            else
                result = result.ThenBy(x => info.GetValue(x, null));
        }

        return result!;
    }

    public IEnumerable<T> Find(IEnumerable<T> entities, string query)
    {
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

    public IEnumerable<T> Filter(IEnumerable<T> entities, Func<T, bool> predicate)
        => entities.Where(predicate);
}
