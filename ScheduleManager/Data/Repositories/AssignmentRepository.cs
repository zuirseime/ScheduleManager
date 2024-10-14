using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.Repositories;

public class AssignmentRepository(ScheduleContext context) : Repository<Assignment>(context)
{
    public override async Task<IEnumerable<Assignment>> GetAllAsync()
        => await context.Assignments.Include(e => e.User).ToListAsync();

    public override async Task<Assignment?> GetByIdAsync(Guid id)
        => (await GetAllAsync()).FirstOrDefault(e => e.Id == id);

    public override async Task CreateAsync(Assignment entity)
    {
        await context.Assignments.AddAsync(entity);
        await SaveChangesAsync();
    }

    public override async Task UpdateAsync(Assignment entity)
    {
        context.Assignments.Update(entity);
        await SaveChangesAsync();
    }

    public override async Task DeleteAsync(Assignment entity)
    {
        context.Assignments.Remove(entity);
        await SaveChangesAsync();
    }

    public override async Task<IEnumerable<Assignment>> GetAllAsync(Func<Assignment, bool> predicate) => (await context.Assignments.ToListAsync()).Where(predicate);
}
