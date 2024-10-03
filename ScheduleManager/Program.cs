using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Data;
using ScheduleManager.Data.Models;
using ScheduleManager.Data.Repositories;
using ScheduleManager.Services;

namespace ScheduleManager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<ScheduleContext>(options 
            => options.UseNpgsql(builder.Configuration.GetConnectionString("Dev")));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ScheduleContext>().AddDefaultTokenProviders();

        AddScopes<Assignment, AssignmentRepository, AssignmentValidationService>(builder.Services);
        AddScopes<Lesson, LessonRepository, LessonValidationService>(builder.Services);
        AddScopes<Discipline, DisciplineRepository, DisciplineValidationService>(builder.Services);
        AddScopes<LessonDuration, LessonDurationRepository, ValidationService<LessonDuration>>(builder.Services);
        AddScopes<Ring, RingRepository, ValidationService<Ring>>(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static void AddScopes<T, R, V>(IServiceCollection services) 
        where T : Entity
        where R : Repository<T>
        where V : ValidationService<T>
    {
        services.AddScoped<Repository<T>, R>();
        services.AddScoped<IRepositoryService<T>, RepositoryService<T>>();
        services.AddScoped<IQueryService<T>, QueryService<T>>();
        services.AddScoped<IValidationService<T>, V>();
    }
}