﻿using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.Repositories;

public class DisciplineRepository(ScheduleContext context) : Repository<Discipline>(context)
{
    public override async Task<IEnumerable<Discipline>> GetAllAsync()
        => await context.Disciplines.Include(e => e.User).ToListAsync();

    public override async Task<Discipline?> GetByIdAsync(Guid id)
        => (await GetAllAsync()).FirstOrDefault(a => a.Id == id);

    public override async Task CreateAsync(Discipline entity)
    {
        await context.Disciplines.AddAsync(entity);
        await SaveChangesAsync();
    }

    public override async Task UpdateAsync(Discipline entity)
    {
        context.Disciplines.Update(entity);
        await SaveChangesAsync();
    }

    public override async Task DeleteAsync(Discipline entity)
    {
        context.Disciplines.Remove(entity);
        await SaveChangesAsync();
    }

    public override async Task<IEnumerable<Discipline>> GetAllAsync(Func<Discipline, bool> predicate) => (await context.Disciplines.ToListAsync()).Where(predicate);
}
