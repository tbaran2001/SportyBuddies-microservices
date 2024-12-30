using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services;

public class CustomProfileService(UserManager<ApplicationUser> userManager, ILogger<CustomProfileService> logger)
    : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
        if (user == null)
        {
            logger.LogWarning("User not found for subject: {Subject}", context.Subject.Identity?.Name);
            return;
        }

        var existingClaims = await userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new("username", user.UserName!)
        };

        var nameClaim = existingClaims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name);
        if (nameClaim != null)
            claims.Add(nameClaim);

        context.IssuedClaims.AddRange(claims);
        logger.LogInformation("Claims issued for user: {UserId}", user.Id);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
        context.IsActive = user != null;
        logger.LogDebug("IsActive check for subject: {Subject}, active: {IsActive}", context.Subject.Identity?.Name, context.IsActive);
    }
}