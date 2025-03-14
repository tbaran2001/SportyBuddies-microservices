namespace Sport.API.Data;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;


        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        await SeedSportAsync(context);
    }

    private static async Task SeedSportAsync(ApplicationDbContext context)
    {
        if (context.Sports.Any())
            return;

        context.Sports.AddRange(SportInitialData.GetInitialSports());
        await context.SaveChangesAsync();
    }
}