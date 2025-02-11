namespace ProfileManagement.API.Data.Seed;

public static class DatabaseExtensions
{
    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;


        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        SeedAsync(context);
    }

    private static void SeedAsync(ApplicationDbContext context)
    {
        SeedSport(context);
        //await SeedProfileWithSportsAsync(context);
    }

    private static void SeedSport(ApplicationDbContext context)
    {
        if (context.Sports.Any())
            return;

        context.Sports.AddRange(InitialData.Sports);
        context.SaveChanges();
    }

    private static async Task SeedProfileWithSportsAsync(ApplicationDbContext context)
    {
        if (!await context.Profiles.AnyAsync())
        {
            await context.Profiles.AddRangeAsync(InitialData.ProfilesWithSports);
            await context.SaveChangesAsync();
        }
    }
}