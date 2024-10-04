using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;
using System.Reflection;

namespace ScheduleManager.Services;

public class UserRepositoryService(UserManager<IdentityUser> manager)
{
    public async Task<IEnumerable<IdentityUser>> GetAllAsync() 
        => await manager.Users.ToListAsync();

    public async Task<IdentityUser?> GetByIdAsync(string id) 
        => await manager.FindByIdAsync(id);

    public async Task CreateAsync(IdentityUser entity, string password) 
        => await manager.CreateAsync(entity, password);

    public async Task UpdateAsync(IdentityUser entity)
    {
        await manager.UpdateAsync(entity);
    }

    public async Task DeleteAsync(string id)
    {
        var user = await manager.FindByIdAsync(id);

        if (user is not null)
            await manager.DeleteAsync(user);
    }
}
