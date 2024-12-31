using System.Security.Claims;
using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.Identity;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(
    UserManager<ApplicationUser> userManager,
    IUserStore<ApplicationUser> userStore,
    ApplicationDbContext dbContext,
    IPublishEndpoint publishEndpoint)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await CreateUserAsync(request.Email, request.Password);
        if (user == null)
            return BadRequest("User creation failed.");

        var emailConfirmationResult = await ConfirmEmailAsync(user);
        if (!emailConfirmationResult.Succeeded)
            return BadRequest(emailConfirmationResult.Errors);

        var claimsResult = await AddUserClaimsAsync(user, request.Name);
        if (!claimsResult.Succeeded)
            return BadRequest(claimsResult.Errors);

        await dbContext.SaveChangesAsync();

        var integrationEvent = new UserRegisteredIntegrationEvent
        {
            UserId = user.Id,
            Name = request.Name
        };
        await publishEndpoint.Publish(integrationEvent);

        return Ok(new { UserId = user.Id });
    }

    private async Task<ApplicationUser?> CreateUserAsync(string email, string password)
    {
        if (string.IsNullOrEmpty(email))
        {
            ModelState.AddModelError(nameof(email), "Email is required.");
            return null;
        }

        var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
        var user = new ApplicationUser();

        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded) return user;

        foreach (var error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        return null;
    }

    private async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user)
    {
        user.EmailConfirmed = true;
        return await userManager.UpdateAsync(user);
    }

    private async Task<IdentityResult> AddUserClaimsAsync(ApplicationUser user, string name)
    {
        var claims = new List<Claim>
        {
            new(JwtClaimTypes.Name, name)
        };

        return await userManager.AddClaimsAsync(user, claims);
    }
}