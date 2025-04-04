﻿namespace Matching.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;
        builder.AddCustomSerilog();
        builder.Services.AddCarter();
        builder.Services.AddValidatorsFromAssembly(assembly);
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            options.AddInterceptors(interceptor);
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
        });
        builder.Services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<IMatchesRepository, MatchesRepository>();
        builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        //builder.Services.Decorate<IMatchesRepository, CachedMatchesRepository>();
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
        });

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("Database")!)
            .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
        builder.Services.AddMessageBroker<ApplicationDbContext>(builder.Configuration, Assembly.GetExecutingAssembly());
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
        builder.Services.AddGrpcClient<ProfileProtoService.ProfileProtoServiceClient>(options =>
            {
                options.Address = new Uri(builder.Configuration["GrpcSettings:ProfileUrl"]!);
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

        // Identity
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        builder.Services.AddJwt();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<AuthHeaderHandler>();
        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddBackgroundJobs();

        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.InitializeDatabaseAsync();
        }

        app.MapCarter();
        app.UseExceptionHandler(_ => { });
        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Matching API");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}