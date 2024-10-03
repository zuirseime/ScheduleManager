using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data.Models;

namespace ScheduleManager.Data;

public class ScheduleContext(DbContextOptions<ScheduleContext> options) : IdentityDbContext(options)
{
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonDuration> LessonDurations { get; set; }
    public DbSet<Ring> Rings { get; set; }
}
