using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;

namespace ScheduleManager.Services;

public class DisciplineValidationService(Repository<Discipline> repository) 
    : ValidationService<Discipline>(repository)
{
    public async override Task<bool> ValidateAsync(Discipline entity)
    {
        var assignments = await repository.GetAllAsync();

        if (assignments.Any(a => a.UserId == entity.UserId && a.Name == entity.Name))
            throw new InvalidOperationException("Discipline with the same name already exists.");

        return true;
    }
}
