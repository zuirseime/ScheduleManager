using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;

namespace ScheduleManager.Services;

public class LessonValidationService(Repository<Lesson> repository) 
    : ValidationService<Lesson>(repository)
{
    public async override Task<bool> ValidateAsync(Lesson entity)
    {
        var assignments = await repository.GetAllAsync();

        if (assignments.Any(a => a.Day == entity.Day && a.LessonNumber == entity.LessonNumber))
            throw new InvalidOperationException($"Lesson with the same number already exists in {entity.Day}.");

        return true;
    }
}
