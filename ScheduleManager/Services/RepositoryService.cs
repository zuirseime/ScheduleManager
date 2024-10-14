using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;
using System.Reflection;

namespace ScheduleManager.Services;

public class RepositoryService<T>(Repository<T> repository) : IRepositoryService<T> where T : Entity
{
    public async Task<IEnumerable<T>> GetAllAsync() => await repository.GetAllAsync();
    public async Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate) => await repository.GetAllAsync(predicate);

    public async Task<T?> GetByIdAsync(Guid id) => await repository.GetByIdAsync(id);

    public async Task CreateAsync(T entity) => await repository.CreateAsync(entity);

    public async Task UpdateAsync(T entity)
    {
        var found = await repository.GetByIdAsync(entity.Id)
            ?? throw new InvalidOperationException("Entity not found");

        foreach (var property in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (property.Name == nameof(Entity.Id) || 
                property.Name == nameof(Entity.UserId) || 
                property.Name == nameof(Entity.User) ||
                property.PropertyType.IsSubclassOf(typeof(Entity)))
                continue;

            if (!property.CanWrite) continue;

            var value = property.GetValue(entity);
            property.SetValue(found, value);
        }

        await repository.UpdateAsync(found);
    }

    public async Task DeleteAsync(T entity)
    {
        var found = await repository.GetByIdAsync(entity.Id)
            ?? throw new InvalidOperationException("Entity not found");

        await repository.DeleteAsync(found);
    }
}
