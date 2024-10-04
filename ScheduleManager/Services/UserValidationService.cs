using Microsoft.AspNetCore.Identity;

namespace ScheduleManager.Services;

public class UserValidationService : UserValidator<IdentityUser>
{
    public async override Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
    {
        var result = await base.ValidateAsync(manager, user);

        if (user.UserName!.Contains("invalid"))
        {
            var errors = result.Errors.ToList();
            errors.Add(new IdentityError { Description = "Username cannot contain 'invalid'." });
            return IdentityResult.Failed([.. errors]);
        }

        return result;
    }
}
