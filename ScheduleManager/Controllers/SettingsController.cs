using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Data.Models;
using ScheduleManager.Data.ViewModels;
using ScheduleManager.Services;

namespace ScheduleManager.Controllers;

[Route("settings/")]
public class SettingsController(IRepositoryService<Ring> ringRepository, 
                                IRepositoryService<LessonDuration> lessonDurationRepository,
                                IValidationService<Ring> ringValidator,
                                IValidationService<LessonDuration> lessonDurationValidator) : Controller
{
    [Route("{userId?}")]
    public async Task<IActionResult> Index(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();
        ViewBag.UserId = userId;

        var lessonDurations = await lessonDurationRepository.GetAllAsync();
        var rings = await ringRepository.GetAllAsync();

        var viewModel = new SettingsViewModel
        {
            LessonDurations = lessonDurations,
            Rings = rings
        };

        return View(viewModel);
    }

    [Route("{userId?}/create-lesson-duration")]
    public IActionResult CreateLessonDuration(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        return View();
    }

    [HttpPost]
    [Route("{userId?}/create-lesson-duration")]
    public async Task<IActionResult> CreateLessonDuration(string? userId, LessonDuration model)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        if (!ModelState.IsValid || !(await lessonDurationValidator.ValidateAsync(model)))
            return View(model);

        await lessonDurationRepository.CreateAsync(model);
        return RedirectToAction(nameof(Index), new { userId });
    }

    [Route("{userId?}/create-ring")]
    public IActionResult CreateRing(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        ViewBag.UserId = userId;
        return View();
    }

    [HttpPost]
    [Route("{userId?}/create-ring")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRing(string? userId, Ring model)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        if (!ModelState.IsValid || !(await ringValidator.ValidateAsync(model)))
            return View(model);

        await ringRepository.CreateAsync(model);
        return RedirectToAction(nameof(Index), new { userId });
    }

    [Route("{userId?}/edit-lesson-duration")]
    public async Task<IActionResult> EditLessonDuration(string? userId, Guid id)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();
        ViewBag.UserId = userId;

        var lessonDuration = await lessonDurationRepository.GetByIdAsync(id);
        if (lessonDuration is null)
            return NotFound();

        return View(lessonDuration);
    }

    [HttpPost]
    [Route("{userId?}/edit-lesson-duration")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditLessonDuration(string? userId, Guid id, LessonDuration model)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();
        ViewBag.UserId = userId;

        if (id != model.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(model);

        await lessonDurationRepository.UpdateAsync(model);
        return RedirectToAction(nameof(Index), new { userId });
    }

    [Route("{userId?}/edit-ring")]
    public async Task<IActionResult> EditRing(string? userId, Guid id)
    {
        if (string.IsNullOrEmpty(userId))
            return NotFound();

        var ring = await ringRepository.GetByIdAsync(id);
        if (ring is null)
            return NotFound();

        return View(ring);
    }

    [HttpPost]
    [Route("{userId?}/edit-ring")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRing(string? userId, Guid id, Ring model)
    {
        if (id != model.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(model);

        await ringRepository.UpdateAsync(model);
        return RedirectToAction(nameof(Index), new { userId });
    }

    [HttpPost]
    [Route("{userId?}/delete-lesson-duration")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteLessonDuration(string? userId, Guid id, LessonDuration model)
    {
        if (id != model.Id)
            return NotFound();

        await lessonDurationRepository.DeleteAsync(model);
        return RedirectToAction(nameof(Index), new { userId });
    }

    [HttpPost]
    [Route("{userId?}/delete-ring")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRing(string? userId, Guid id, Ring model)
    {
        if (id != model.Id)
            return NotFound();

        await ringRepository.DeleteAsync(model);
        return RedirectToAction(nameof(Index), new { userId });
    }
}
