﻿using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data.Models;

namespace ScheduleManager.Data.Repositories;

public class RingRepository(ScheduleContext context) : Repository<Ring>(context)
{
    public override async Task<IEnumerable<Ring>> GetAllAsync()
        => await context.Rings.Include(e => e.User).ToListAsync();

    public override async Task<Ring?> GetByIdAsync(Guid id)
        => (await GetAllAsync()).FirstOrDefault(e => e.Id == id);

    public override async Task CreateAsync(Ring entity)
    {
        await context.Rings.AddAsync(entity);
        await SaveChangesAsync();
    }

    public override async Task UpdateAsync(Ring entity)
    {
        context.Rings.Update(entity);
        await SaveChangesAsync();
    }

    public override async Task DeleteAsync(Ring entity)
    {
        context.Rings.Remove(entity);
        await SaveChangesAsync();
    }

    public override async Task<IEnumerable<Ring>> GetAllAsync(Func<Ring, bool> predicate) => (await context.Rings.ToListAsync()).Where(predicate);
}
