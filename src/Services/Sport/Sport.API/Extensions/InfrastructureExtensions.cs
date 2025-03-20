using System.Reflection;
using BuildingBlocks.EFCore;
using BuildingBlocks.EFCore.Interceptors;
using BuildingBlocks.Logging;
using BuildingBlocks.MassTransit;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sport.API.Data.Seed;
using Sport.API.Sports.Mapster;

namespace Sport.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;
        builder.AddCustomSerilog();
        builder.Services.AddCarter();
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
        });
        builder.Services.AddScoped<ISportsRepository, SportsRepository>();
        builder.Services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        builder.Services.AddScoped<IDataSeeder, SportDataSeeder>();

        builder.Services.AddValidatorsFromAssembly(assembly);
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder.Services.AddValidatorsFromAssembly(assembly);
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddMessageBroker<ApplicationDbContext>(builder.Configuration, Assembly.GetExecutingAssembly());
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

        app.UseMigration<ApplicationDbContext>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sport API");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}