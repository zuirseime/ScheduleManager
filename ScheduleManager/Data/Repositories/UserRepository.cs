using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ScheduleManager.Data.Repositories;

public class UserRepository(ScheduleContext context) : Repository<IdentityUser>(context)
{
    public override async Task<IEnumerable<IdentityUser>> GetAllAsync()
        => await context.Users.ToListAsync();

    public override async Task<IdentityUser?> GetByIdAsync(Guid id) 
        => (await GetAllAsync()).FirstOrDefault(u => u.Id == id.ToString());

    public override async Task CreateAsync(IdentityUser entity)
    {
        await context.Users.AddAsync(entity);
        await SaveChangesAsync();
    }

    public override async Task UpdateAsync(IdentityUser entity)
    {

        context.Users.Update(entity);
        await SaveChangesAsync();
    }

    public override async Task DeleteAsync(IdentityUser entity)
    {

        context.Users.Remove(entity);
        await SaveChangesAsync();
    }

}
