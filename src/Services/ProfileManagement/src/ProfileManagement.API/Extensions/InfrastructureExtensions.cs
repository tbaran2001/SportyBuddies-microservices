namespace ProfileManagement.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;
        builder.AddCustomSerilog();
        builder.Services.AddCarter();
        builder.Services.AddGrpc();
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
        });
        builder.Services.Configure<MongoOptions>(builder.Configuration.GetSection("MongoOptions"));
        builder.Services.AddSingleton<ApplicationReadDbContext>();
        builder.Services.AddScoped<IProfilesRepository, ProfilesRepository>();
        builder.Services.AddScoped<ISportsRepository, SportsRepository>();
        builder.Services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        //builder.Services.Decorate<IProfilesRepository, CachedProfilesRepository>();
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
        });

        builder.Services.AddValidatorsFromAssembly(assembly);
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

        app.MapGrpcService<ProfileService>();

        app.InitializeDatabase();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Profile API");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}