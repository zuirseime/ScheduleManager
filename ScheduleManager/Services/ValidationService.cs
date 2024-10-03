using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;

namespace ScheduleManager.Services;

public abstract class ValidationService<T>(Repository<T> repository) : IValidationService<T> where T : Entity
{
    public virtual async Task<bool> ValidateAsync(T entity)
    {
        var entities = await repository.GetAllAsync();

        if (entities.Any(e => e.Id == entity.Id))
            throw new InvalidOperationException("Entity already exists.");

        return true;
    }
}
