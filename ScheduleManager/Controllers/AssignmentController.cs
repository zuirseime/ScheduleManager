using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Data.Enums;
using ScheduleManager.Data.Models;
using ScheduleManager.Data.ViewModels;
using ScheduleManager.Services;

namespace ScheduleManager.Controllers;


[Route("assignments/")]
public class AssignmentController(IRepositoryService<Assignment> repositoryService,
                                  IQueryService<Assignment> queryService,
                                  IValidationService<Assignment> validationService,
                                  IRepositoryService<Discipline> disciplineRepository) : Controller
{
    [Route("{userId}")]
    public async Task<IActionResult> Index(string? userId, AssignmentType? type = null, Guid? disciplineId = null, bool? done = null)
    {
        IEnumerable<Assignment> assignments;

        if (userId is null || userId == (Guid.Empty.ToString())) return NotFound();

        assignments = await queryService.Filter(a => a.UserId == userId
            && (type is null || a.Type == type.Value)
            && (disciplineId is null || disciplineId == Guid.Empty || a.DisciplineId == disciplineId.Value)
            && (done is null || a.IsDone == done.Value)
        );

        queryService.Sort(assignments);

        ViewBag.Type = type;
        ViewBag.Discipline = disciplineId;
        ViewBag.Done = done;
        await PopulateDisciplines(userId);

        return View(new AssignmentsViewModel { Assignments = assignments });
    }

    [Route("{userId}/create")]
    public async Task<IActionResult> Create(string userId)
    {
        await PopulateDisciplines(userId);
        return View();
    }

    [HttpPost]
    [Route("{userId}/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string userId, Assignment assignment)
    {
        await PopulateDisciplines(userId);

        if (!ModelState.IsValid || !await validationService.ValidateAsync(assignment))
            return View(assignment);

        await repositoryService.CreateAsync(assignment);
        return RedirectToAction(nameof(Index), new { userId });
    }

    [Route("{id}/edit")]
    public async Task<IActionResult> Edit(string userId, Guid id)
    {
        var assignment = await repositoryService.GetByIdAsync(id);
        if (assignment is null) return NotFound();

        await PopulateDisciplines(userId);
        return View(assignment);
    }

    [HttpPost]
    [Route("{id}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string userId, Guid id, Assignment assignment)
    {
        if (id != assignment.Id)
            return NotFound();

        await PopulateDisciplines(userId);
        if (!ModelState.IsValid)
            return View(assignment);

        await repositoryService.UpdateAsync(assignment);
        return RedirectToAction(nameof(Index), new { userId });
    }

    [HttpPost]
    [Route("{id}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string userId, Guid id)
    {
        var assignment = await repositoryService.GetByIdAsync(id);
        if (assignment is null) return NotFound();

        if (!ModelState.IsValid)
            return View(new { userId, id });

        await repositoryService.DeleteAsync(assignment);
        return RedirectToAction(nameof(Index), new { userId });
    }

    [NonAction]
    private async Task PopulateDisciplines(string userId)
    {
        List<Discipline> disciplines = [new() { Id = Guid.Empty, Name = "Choose a discipline" }];
        disciplines.AddRange(await disciplineRepository.GetAllAsync(d => d.UserId == userId));

        ViewBag.UserId = userId;
        ViewBag.Disciplines = disciplines;
    }
}
