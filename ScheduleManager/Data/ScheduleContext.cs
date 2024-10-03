using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ScheduleManager.Data;

public class ScheduleContext(DbContextOptions<ScheduleContext> options) : IdentityDbContext(options)
{
}
