using Buddies.API.Data;
using Buddies.API.GrpcServer.Services;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Jwt;
using BuildingBlocks.Web;
using Carter;
using Microsoft.EntityFrameworkCore;

namespace Buddies.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;

        builder.Services.AddCarter();
        builder.Services.AddGrpc();
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder.Services.AddDbContext<BuddyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        // Identity
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        builder.Services.AddJwt();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<AuthHeaderHandler>();
        builder.Services.AddAuthorization();

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.InitializeDatabaseAsync();
        }
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        app.MapGrpcService<BuddiesService>();

        return app;
    }
}