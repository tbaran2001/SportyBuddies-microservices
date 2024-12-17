using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProfileManagement.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;


        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedSportAsync(context);
        await SeedProfileWithSportsAsync(context);
    }

    private static async Task SeedSportAsync(ApplicationDbContext context)
    {
        if(!await context.Sports.AnyAsync())
        {
            await context.Sports.AddRangeAsync(InitialData.Sports);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProfileWithSportsAsync(ApplicationDbContext context)
    {
        if(!await context.Profiles.AnyAsync())
        {
            await context.Profiles.AddRangeAsync(InitialData.ProfilesWithSports);
            await context.SaveChangesAsync();
        }
    }
}