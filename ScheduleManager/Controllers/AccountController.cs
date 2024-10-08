using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Data.ViewModels;
using ScheduleManager.Services;

namespace ScheduleManager.Controllers;

[Route("account/")]
public class AccountController(UserRepositoryService repositoryService,
                               SignInManager<IdentityUser> signInManager) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await repositoryService.GetCurrent(User));
    }


    [Route("sign-up")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("sign-up")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser
            {
                UserName = model.Email[..model.Email.IndexOf('@')],
                Email = model.Email
            };
            var result = await repositoryService.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [Route("log-in")]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [Route("log-in")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = (await repositoryService.GetAllAsync(u => u.Email == model.Email)).FirstOrDefault();
            
            if (user is not null)
            {
                var result = await signInManager.PasswordSignInAsync(user.UserName!, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    [HttpPost]
    [Route("log-out")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}
