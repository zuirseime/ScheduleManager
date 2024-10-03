using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;

namespace ScheduleManager.Services.Assignments;

public class AssignmentValidationService(Repository<Assignment> repository) : IValidationService<Assignment>
{
    public async Task<bool> ValidateAsync(Assignment entity)
    {
        var assignments = await repository.GetAllAsync();

        if (assignments.Any(a => a.Title == entity.Title && a.DisciplineId == entity.DisciplineId))
            throw new InvalidOperationException("Assignment with the same name already exists.");

        return true;
    }
}
