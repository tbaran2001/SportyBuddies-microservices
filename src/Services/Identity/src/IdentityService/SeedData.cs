using System.Security.Claims;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (userMgr.Users.Any())
            return;

        var alice = userMgr.FindByNameAsync("John").Result;
        if (alice == null)
        {
            alice = new ApplicationUser
            {
                Id = new Guid("8d69a725-c1b7-45eb-8ace-982bdc21ca78"),
                UserName = "John",
                Email = "john@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(alice, "123").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(alice, [
                new Claim(JwtClaimTypes.Name, "John")
            ]).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("john created");
        }
        else
        {
            Log.Debug("john already exists");
        }

        var bob = userMgr.FindByNameAsync("Alice").Result;
        if (bob == null)
        {
            bob = new ApplicationUser
            {
                Id = new Guid("f0d08409-8f34-4f88-aba4-cc7e906f7d62"),
                UserName = "Alice",
                Email = "alice@email.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(bob, "123").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(bob, [
                new Claim(JwtClaimTypes.Name, "Alice")
            ]).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("alice created");
        }
        else
        {
            Log.Debug("alice already exists");
        }
    }
}