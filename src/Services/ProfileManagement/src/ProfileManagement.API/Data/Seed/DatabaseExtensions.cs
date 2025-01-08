using Microsoft.EntityFrameworkCore;

namespace ProfileManagement.API.Data.Seed;

public static class DatabaseExtensions
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;


        var context = services.GetRequiredService<ProfileDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        SeedAsync(context);
    }

    private static void SeedAsync(ProfileDbContext context)
    {
        SeedSport(context);
        //await SeedProfileWithSportsAsync(context);
    }

    private static void SeedSport(ProfileDbContext context)
    {
        if (context.Sports.Any())
            return;

        context.Sports.AddRange(InitialData.Sports);
        context.SaveChanges();
    }

    private static async Task SeedProfileWithSportsAsync(ProfileDbContext context)
    {
        if(!await context.Profiles.AnyAsync())
        {
            await context.Profiles.AddRangeAsync(InitialData.ProfilesWithSports);
            await context.SaveChangesAsync();
        }
    }
}