﻿using Microsoft.EntityFrameworkCore;

namespace Buddies.API.Data;

public static class Extensions
{
    public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<BuddyDbContext>();

        context.Database.Migrate();

        return app;
    }
}