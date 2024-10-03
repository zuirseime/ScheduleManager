using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.Repositories;

public class AssignmentRepository(ScheduleContext context) : Repository<Assignment>(context)
{
    public override async Task<IEnumerable<Assignment>> GetAllAsync()
        => await context.Assignments.ToListAsync();

    public override async Task<Assignment?> GetByIdAsync(Guid id)
        => (await GetAllAsync()).FirstOrDefault(a => a.Id == id);

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
}
