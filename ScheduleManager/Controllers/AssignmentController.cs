using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Data.Enums;
using ScheduleManager.Data.Models;
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

        ViewBag.Amount = GetAssignmnetAmount(assignments);
        ViewBag.Type = type;
        ViewBag.Discipline = disciplineId;
        ViewBag.Done = done;
        await PopulateDisciplines(userId);

        return View(assignments);
    }

    private (int, int) GetAssignmnetAmount(IEnumerable<Assignment> assignments)
    {
        int all = assignments.Count();
        int done = assignments.Where(a => a.IsDone).Count();

        return (done, all);
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

        if (!ModelState.IsValid && !(await validationService.ValidateAsync(assignment)))
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

        if (ModelState.IsValid)
        {
            await repositoryService.UpdateAsync(assignment);
            return RedirectToAction(nameof(Index), new { userId });
        }

        await PopulateDisciplines(userId);
        return View(assignment);
    }

    [HttpPost, ActionName("Delete")]
    [Route("{id}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var assignment = await repositoryService.GetByIdAsync(id);
        if (assignment is null) return NotFound();

        await repositoryService.DeleteAsync(assignment);
        return RedirectToAction(nameof(Index));
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
