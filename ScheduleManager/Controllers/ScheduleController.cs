using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Data.Enums;
using ScheduleManager.Data.Models;
using ScheduleManager.Data.ViewModels;
using ScheduleManager.Services;

namespace ScheduleManager.Controllers;

[Route("schedule/")]
public class ScheduleController(IRepositoryService<Lesson> lessonRepository, 
                                IRepositoryService<Discipline> disciplineRepository,
                                IRepositoryService<LessonDuration> lessonDurationRepository,
                                IRepositoryService<Ring> ringRepository,
                                IValidationService<Lesson> lessonValidator,
                                IQueryService<Lesson> queryService) : Controller
{
    [Route("{day}/{userId?}")]
    public async Task<IActionResult> Index(DayOfWeek day, string? userId)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        Repetition week = ScheduleViewModel.GetWeek(DateTime.Now) % 2 == 0 ? Repetition.Numerator : Repetition.Denominator;

        var lessons = await lessonRepository.GetAllAsync(l => l.UserId == userId && (l.Repetition == Repetition.Always || l.Repetition == week) && l.Day == day);

        List<LessonViewModel> lessonVMs = [];

        foreach (var lesson in lessons)
        {
            var vm = new LessonViewModel
            {
                Lesson = lesson,
                Start = (await ringRepository.GetAllAsync(r => r.UserId == userId && r.Number == lesson.LessonNumber)).First().Time,
                Duration = (await lessonDurationRepository.GetAllAsync(d => d.UserId == userId && d.LessonType == lesson.LessonType)).First().Duration
            };
            lessonVMs.Add(vm);
        }

        var scheduleVM = new ScheduleViewModel
        {
            Day = day,
            StartHour = 6,
            EndHour = 18,
            Lessons = lessonVMs
        };

        ViewBag.UserId = userId;
        return View(scheduleVM);
    }

    [Route("{day}/{userId}/create")]
    public async Task<IActionResult> Create(DayOfWeek day, string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        await PopulateDisciplines(userId);
        await PopulateClasses(userId);
        ViewBag.Today = day;

        return View();
    }

    [HttpPost]
    [Route("{day}/{userId}/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DayOfWeek day, string userId, Lesson lesson)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        await PopulateDisciplines(userId);
        await PopulateClasses(userId);

        if (!ModelState.IsValid || !(await lessonValidator.ValidateAsync(lesson)))
            return View(lesson);

        await lessonRepository.CreateAsync(lesson);
        return RedirectToAction(nameof(Index), new { day, userId });
    }

    [Route("{id}/edit")]
    public async Task<IActionResult> Edit(string userId, Guid id)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        var lesson = await lessonRepository.GetByIdAsync(id);
        if (lesson is null)
            return NotFound();

        await PopulateDisciplines(userId);
        await PopulateClasses(userId);
        return View(lesson);
    }

    [HttpPost]
    [Route("{id}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string userId, Guid id, Lesson lesson)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        if (id != lesson.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            await lessonRepository.UpdateAsync(lesson);
            return RedirectToAction(nameof(Index), new { lesson.Day, userId });
        }

        await PopulateDisciplines(userId);
        await PopulateClasses(userId);
        return View(lesson);
    }

    [HttpPost]
    [Route("{id}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string userId, Guid id)
    {
        var lesson = await lessonRepository.GetByIdAsync(id);
        if (lesson is null) return NotFound();

        if (!ModelState.IsValid)
            return View(new { userId, id });

        await lessonRepository.DeleteAsync(lesson);
        return RedirectToAction(nameof(Index), new { lesson.Day, userId });
    }

    [NonAction]
    private async Task PopulateDisciplines(string userId)
    {
        List<Discipline> disciplines = [new() { Id = Guid.Empty, Name = "Choose a discipline" }];
        disciplines.AddRange(await disciplineRepository.GetAllAsync(d => d.UserId == userId));

        ViewBag.UserId = userId;
        ViewBag.Disciplines = disciplines;
    }

    [NonAction]
    private async Task PopulateClasses(string userId)
    {
        ViewBag.Classes = (await ringRepository.GetAllAsync(r => r.UserId == userId)).Select(r => r.Number);
    }
}
