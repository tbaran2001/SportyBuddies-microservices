using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Sport.API.Data;

namespace Sport.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;
        builder.Services.AddCarter();
        builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("Database")!);
            })
            .UseLightweightSessions();
        if (builder.Environment.IsDevelopment())
            builder.Services.InitializeMartenWith<SportInitialData>();

        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder.Services.AddValidatorsFromAssembly(assembly);
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(_ => { });

        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        return app;
    }
}