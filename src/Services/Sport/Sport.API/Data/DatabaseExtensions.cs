using Sport.API.Sports.Features.CreateSport;

namespace Sport.API.Data;

public static class DatabaseExtensions
{
    public static void InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;


        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        var sender = services.GetRequiredService<ISender>();

        SeedSportAsync(context, sender);
    }

    private static void SeedSportAsync(ApplicationDbContext context, ISender sender)
    {
        if (context.Sports.Any())
            return;

        //context.Sports.AddRange(SportInitialData.GetInitialSports());
        //context.SaveChangesAsync();

        var command = new CreateSportCommand(Name.Of("Football"), Description.Of("Football description"));
        sender.Send(command);

        context.SaveChangesAsync();
    }
}