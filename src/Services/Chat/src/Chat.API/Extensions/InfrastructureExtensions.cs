namespace Chat.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var assembly = typeof(Program).Assembly;
        var services = builder.Services;

        // Logging
        builder.AddCustomSerilog();

        // Carter
        services.AddCarter();

        // Validation & MediatR Behaviors
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        // Exception Handling
        services.AddExceptionHandler<CustomExceptionHandler>();
        
        // Mapster
        TypeAdapterConfig.GlobalSettings.Scan(assembly);

        // Identity & Auth
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        services.AddJwt();
        services.AddHttpContextAccessor();
        services.AddTransient<AuthHeaderHandler>();
        services.AddAuthorization();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Quartz & Background Jobs
        services.AddBackgroundJobs();

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        // Middleware and endpoints
        app.MapCarter();
        app.UseExceptionHandler(_ => { });

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Profile API");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}