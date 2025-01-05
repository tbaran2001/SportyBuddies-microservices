using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Configurations;

public class UserValidator(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager)
    : IResourceOwnerPasswordValidator
{
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await userManager.FindByNameAsync(context.UserName);

        var signIn = await signInManager.PasswordSignInAsync(
            user,
            context.Password,
            isPersistent: true,
            lockoutOnFailure: true);

        if (signIn.Succeeded)
        {
            var userId = user!.Id.ToString();

            context.Result = new GrantValidationResult(
                subject: userId,
                authenticationMethod: "custom",
                claims:
                [
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, user.UserName)
                ]
            );

            return;
        }

        context.Result = new GrantValidationResult(
            TokenRequestErrors.UnauthorizedClient, "Invalid Credentials");
    }
}