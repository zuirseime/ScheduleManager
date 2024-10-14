using Microsoft.AspNetCore.Identity;

namespace ScheduleManager.Data.Models;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public IdentityUser? User { get; set; }
}
