using Buddies.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Buddies.API.Extensions;

public static class DatabaseExtensions
{
    public static Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<BuddyDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        return Task.CompletedTask;
    }
}