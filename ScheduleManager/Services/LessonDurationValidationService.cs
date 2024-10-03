using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;

namespace ScheduleManager.Services;

public class LessonDurationValidationService(Repository<LessonDuration> repository) 
    : ValidationService<LessonDuration>(repository)
{
    public async override Task<bool> ValidateAsync(LessonDuration entity)
    {
        var assignments = await repository.GetAllAsync();

        if (assignments.Any(a => a.LessonType == entity.LessonType))
            throw new InvalidOperationException("Lesson type already exists.");

        return true;
    }
}
