using Microsoft.AspNetCore.Identity;
using ScheduleManager.Data.Repositories;
using System.Security.Claims;

namespace ScheduleManager.Services;

public class UserRepositoryService(UserRepository repository)
{
    public async Task<IEnumerable<IdentityUser>> GetAllAsync() => await repository.GetAllAsync();
    public async Task<IEnumerable<IdentityUser>> GetAllAsync(Func<IdentityUser, bool> predicate) => await repository.GetAllAsync(predicate);
    public async Task<IdentityUser?> GetByIdAsync(Guid id) => await repository.GetByIdAsync(id);
    public async Task<IdentityUser?> GetCurrent(ClaimsPrincipal user) => await repository.GetCurrent(user);
    public async Task<IdentityResult?> CreateAsync(IdentityUser entity) => await repository.CreateAsync(entity);
    public async Task<IdentityResult?> CreateAsync(IdentityUser entity, string password) => await repository.CreateAsync(entity, password);
    public async Task<IdentityResult?> UpdateAsync(IdentityUser entity) => await repository.UpdateAsync(entity);
    public async Task<IdentityResult?> DeleteAsync(IdentityUser entity) => await repository.DeleteAsync(entity);
}
