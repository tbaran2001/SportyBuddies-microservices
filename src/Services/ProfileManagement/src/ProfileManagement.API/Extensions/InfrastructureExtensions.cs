using BuildingBlocks.Behaviors;
using BuildingBlocks.Core.Model;
using BuildingBlocks.EFCore.Interceptors;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Jwt;
using BuildingBlocks.MassTransit;
using BuildingBlocks.Web;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.FeatureManagement;
using ProfileManagement.API.Data;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Data.Seed;
using ProfileManagement.API.GrpcServer.Services;
using ProfileManagement.API.Profiles.Mapster;

namespace ProfileManagement.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;
        builder.Services.AddCarter();
        builder.Services.AddGrpc();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ProfileDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
        });
        builder.Services.AddScoped<IProfilesRepository, ProfilesRepository>();
        builder.Services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ProfileDbContext>());
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddMediatR(serviceConfiguration =>
        {
            serviceConfiguration.RegisterServicesFromAssembly(assembly);
            serviceConfiguration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            serviceConfiguration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("Database")!);
        builder.Services.AddFeatureManagement();
        builder.Services.AddMessageBroker(builder.Configuration, assembly);
        MapsterConfig.Configure();

        // Identity
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        builder.Services.AddJwt();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<AuthHeaderHandler>();
        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(_ => { });
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapGrpcService<ProfileService>();

        app.InitializeDatabase();

        app.UseSwagger();
        app.UseSwaggerUI();


        return app;
    }
}