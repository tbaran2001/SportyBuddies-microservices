using BuildingBlocks.Jwt;
using BuildingBlocks.Web;

namespace Matching.API.Extensions.Infrastructure;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        builder.Services.AddJwt();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<AuthHeaderHandler>();

        builder.Services.AddAuthorization();

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        return app;
    }
}