namespace Matching.API.Extensions;

public static class DatabaseExtensions
{
    public static Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        return Task.CompletedTask;
    }
}