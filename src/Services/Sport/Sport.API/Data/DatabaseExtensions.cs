namespace Sport.API.Data;

public static class DatabaseExtensions
{
    public static void InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;


        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        SeedSportAsync(context);
    }

    private static void SeedSportAsync(ApplicationDbContext context)
    {
        if (context.Sports.Any())
            return;

        context.Sports.AddRange(SportInitialData.GetInitialSports());
        context.SaveChanges();
    }
}