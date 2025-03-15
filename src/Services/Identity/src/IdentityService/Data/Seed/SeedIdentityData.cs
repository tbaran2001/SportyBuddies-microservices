using BuildingBlocks.Events.Identity;
using IdentityService.Identity;
using IdentityService.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data.Seed;

public class SeedIdentityData
{
    public static async Task EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (await roleManager.RoleExistsAsync(Constants.Role.Admin) == false)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Name = Constants.Role.Admin });
        }

        if (await roleManager.RoleExistsAsync(Constants.Role.User) == false)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Name = Constants.Role.User });
        }

        if (await userManager.FindByNameAsync("admin") == null)
        {
            var result = await userManager.CreateAsync(InitialData.Users.First(), "123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(InitialData.Users.First(), Constants.Role.Admin);

                var integrationEvent = new UserRegisteredIntegrationEvent
                {
                    UserId = InitialData.Users.First().Id,
                    Name = "admin"
                };
                await publishEndpoint.Publish(integrationEvent);
            }
        }

        if (await userManager.FindByNameAsync("user") == null)
        {
            var result = await userManager.CreateAsync(InitialData.Users.Last(), "123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(InitialData.Users.Last(), Constants.Role.User);

                var integrationEvent = new UserRegisteredIntegrationEvent
                {
                    UserId = InitialData.Users.Last().Id,
                    Name = "user"
                };
                await publishEndpoint.Publish(integrationEvent);
            }
        }
        
        await context.SaveChangesAsync();
    }
}