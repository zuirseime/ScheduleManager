using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;

namespace ScheduleManager.Services;

public class RingValidationService(Repository<Ring> repository)
    : ValidationService<Ring>(repository)
{
    public async override Task<bool> ValidateAsync(Ring entity)
    {
        var assignments = await repository.GetAllAsync();

        if (assignments.Any(a => a.Number == entity.Number))
            throw new InvalidOperationException("Lesson number already taken.");

        return true;
    }
}
