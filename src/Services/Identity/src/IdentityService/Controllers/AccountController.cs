using System.Security.Claims;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore,ApplicationDbContext dbContext)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
        var email = request.Email;

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required");
        }

        var user = new ApplicationUser();

        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        result = userManager.AddClaimsAsync(user, [
            new Claim(JwtClaimTypes.Name, request.Name)
        ]).Result;

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await dbContext.SaveChangesAsync();

        return Ok(user.Id);
    }
}