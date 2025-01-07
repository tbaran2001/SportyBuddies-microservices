using System.Reflection;
using Buddies.Grpc;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Jwt;
using BuildingBlocks.Messaging.MassTransit;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

namespace Matching.API.Extensions.Infrastructure;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;

        builder.Services.AddCarter();

        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
        });
        builder.Services.AddScoped<IMatchesRepository, MatchesRepository>();
        //builder.Services.Decorate<IMatchesRepository, CachedMatchesRepository>();
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
        });

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("Database")!)
            .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
        builder.Services.AddGrpcClient<BuddiesProtoService.BuddiesProtoServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["GrpcSettings:BuddiesUrl"]!);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                return handler;
            });
        builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

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
        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        return app;
    }
}