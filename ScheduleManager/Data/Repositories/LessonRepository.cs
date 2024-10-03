using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.Repositories;

public class LessonRepository(ScheduleContext context) : Repository<Lesson>(context)
{
    public override async Task<IEnumerable<Lesson>> GetAllAsync()
        => await context.Lessons.ToListAsync();

    public override async Task<Lesson?> GetByIdAsync(Guid id)
        => (await GetAllAsync()).FirstOrDefault(a => a.Id == id);

    public override async Task CreateSync(Lesson entity)
    {
        await context.Lessons.AddAsync(entity);
        await SaveChangesAsync();
    }

    public override async Task UpdateAsync(Lesson entity)
    {
        context.Lessons.Update(entity);
        await SaveChangesAsync();
    }

    public override async Task DeleteAsync(Lesson entity)
    {
        context.Lessons.Remove(entity);
        await SaveChangesAsync();
    }
}
