using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.Repositories;

public class LessonDurationRepository(ScheduleContext context) : Repository<LessonDuration>(context)
{
    public override async Task<IEnumerable<LessonDuration>> GetAllAsync()
        => await context.LessonDurations.ToListAsync();

    public override async Task<LessonDuration?> GetByIdAsync(Guid id)
        => (await GetAllAsync()).FirstOrDefault(a => a.Id == id);

    public override async Task CreateSync(LessonDuration entity)
    {
        await context.LessonDurations.AddAsync(entity);
        await SaveChangesAsync();
    }

    public override async Task UpdateAsync(LessonDuration entity)
    {
        context.LessonDurations.Update(entity);
        await SaveChangesAsync();
    }

    public override async Task DeleteAsync(LessonDuration entity)
    {
        context.LessonDurations.Remove(entity);
        await SaveChangesAsync();
    }
}
