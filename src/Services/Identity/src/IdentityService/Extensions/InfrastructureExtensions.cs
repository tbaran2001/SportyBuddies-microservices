using BuildingBlocks.Web;
using IdentityService.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

        builder.Services.AddAuthorization();

        builder.AddCustomIdentityServer();

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseIdentityServer();

        return app;
    }
}