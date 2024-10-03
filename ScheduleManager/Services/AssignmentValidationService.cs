using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;

namespace ScheduleManager.Services;

public class AssignmentValidationService(Repository<Assignment> repository) : ValidationService<Assignment>(repository)
{
    public async override Task<bool> ValidateAsync(Assignment entity)
    {
        var assignments = await repository.GetAllAsync();

        if (assignments.Any(a => a.Title == entity.Title && a.DisciplineId == entity.DisciplineId))
            throw new InvalidOperationException("Assignment with the same name already exists.");

        return true;
    }
}
