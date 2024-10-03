using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;
using System.Reflection;

namespace ScheduleManager.Services.Assignments;

public class AssignmentRepositoryService(Repository<Assignment> repository) : IRepositoryService<Assignment>
{
    public async Task<IEnumerable<Assignment>> GetAllAsync()
        => await repository.GetAllAsync();

    public async Task<Assignment?> GetByIdAsync(Guid id)
    {
        var assignment = await repository.GetByIdAsync(id);

        if (assignment is null)
        {
            return null;
        }

        return assignment;
    }

    public async Task CreateAsync(Assignment entity)
        => await repository.CreateSync(entity);

    public async Task UpdateAsync(Assignment entity)
    {
        var assignment = await repository.GetByIdAsync(entity.Id)
            ?? throw new InvalidOperationException("Assignment not found.");

        foreach (var property in typeof(Assignment).GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (property.Name == nameof(Assignment.Id))
                continue;

            if (!property.CanWrite)
                continue;

            var value = property.GetValue(entity);
            property.SetValue(assignment, value);
        }

        await repository.UpdateAsync(assignment);
    }

    public async Task DeleteAsync(Assignment entity)
    {
        var assignment = await repository.GetByIdAsync(entity.Id)
            ?? throw new InvalidOperationException("Assignment not found.");

        await repository.DeleteAsync(assignment);
    }
}
