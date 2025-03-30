namespace ProfileManagement.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var assembly = typeof(Program).Assembly;
        var services = builder.Services;

        // Logging
        builder.AddCustomSerilog();

        // Carter + gRPC
        services.AddCarter();
        services.AddGrpc();

        // EF Core DbContext & Interceptors
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            options.AddInterceptors(interceptor);
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });

        // MongoDB Config & ReadDbContext
        services.Configure<MongoOptions>(configuration.GetSection("MongoOptions"));
        services.AddSingleton<ApplicationReadDbContext>();

        // Repositories
        services.AddScoped<IProfilesRepository, ProfilesRepository>();
        services.AddScoped<IProfilesReadRepository, ProfilesReadRepository>();
        services.AddScoped<ISportsRepository, SportsRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        //services.Decorate<IProfilesRepository, CachedProfilesRepository>();

        // Caching
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis")!;
        });

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

        // Health Checks
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

        // Feature Flags
        services.AddFeatureManagement();

        // Messaging / Outbox
        services.AddMessageBroker<ApplicationDbContext>(configuration, assembly);

        // Mapster
        MapsterConfig.Configure();
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

        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapGrpcService<ProfileService>();
        app.InitializeDatabase();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Profile API");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.ConfigureOptions<ProcessOutboxMessagesJobSetup<ApplicationDbContext>>();

        return services;
    }
}