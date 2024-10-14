using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ScheduleManager.Data.Repositories;

public class UserRepository(ScheduleContext context, UserManager<IdentityUser> manager) : Repository<IdentityUser>(context)
{
    public override async Task<IEnumerable<IdentityUser>> GetAllAsync()
        => await manager.Users.ToListAsync();
    public override async Task<IEnumerable<IdentityUser>> GetAllAsync(Func<IdentityUser, bool> predicate)
        => (await manager.Users.ToListAsync()).Where(predicate);
    public override async Task<IdentityUser?> GetByIdAsync(Guid id)
        => await manager.FindByIdAsync(id.ToString());
    public async Task<IdentityUser?> GetCurrent(ClaimsPrincipal user) => await manager.GetUserAsync(user);
    public override async Task<IdentityResult?> CreateAsync(IdentityUser entity)
        => await CreateAsync(entity, "password");
    public async Task<IdentityResult?> CreateAsync(IdentityUser entity, string password)
        => await manager.CreateAsync(entity, password);
    public override async Task<IdentityResult?> UpdateAsync(IdentityUser entity) => await manager.UpdateAsync(entity);
    public override async Task<IdentityResult?> DeleteAsync(IdentityUser entity) => await manager.DeleteAsync(entity);
}
