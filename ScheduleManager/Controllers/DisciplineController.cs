using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Data.Models;
using ScheduleManager.Services;

namespace ScheduleManager.Controllers;

[Route("disciplines/")]
public class DisciplineController(IRepositoryService<Discipline> repositoryService, 
                                  IQueryService<Discipline> queryService,
                                  IValidationService<Discipline> validationService) : Controller
{
    [Route("{userId}")]
    public async Task<IActionResult> Index(string userId)
    {
        ViewBag.UserId = userId;
        return View(await repositoryService.GetAllAsync(d => d.UserId == userId));
    }

    [Route("{userId}/create")]
    public IActionResult Create(string userId)
    {
        return View();
    }

    [HttpPost]
    [Route("{userId}/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string userId, Discipline discipline)
    {
        if (ModelState.IsValid && await validationService.ValidateAsync(discipline))
        {
            await repositoryService.CreateAsync(discipline);
            return RedirectToAction(nameof(Index), new { userId });
        }
        return View(discipline);
    }

    [Route("{id}/edit")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var discipline = await repositoryService.GetByIdAsync(id);
        if (discipline == null)
            return NotFound();
        return View(discipline);
    }

    [HttpPost]
    [Route("{id}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string userId, Guid id, Discipline discipline)
    {
        if (id != discipline.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            await repositoryService.UpdateAsync(discipline);
            return RedirectToAction(nameof(Index), new { userId });
        }
        return View(discipline);
    }

    [Route("{id}/delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var discipline = await repositoryService.GetByIdAsync(id);

        if (discipline == null)
            return NotFound();

        return View(discipline);
    }

    [HttpPost, ActionName("Delete")]
    [Route("{id}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var discipline = await repositoryService.GetByIdAsync(id);

        if (discipline == null)
            return NotFound();

        await repositoryService.DeleteAsync(discipline);
        return RedirectToAction(nameof(Index));
    }
}
