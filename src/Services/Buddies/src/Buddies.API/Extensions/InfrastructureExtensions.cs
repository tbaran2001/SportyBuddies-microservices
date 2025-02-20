using BuildingBlocks.Logging;

namespace Buddies.API.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;
        builder.AddCustomSerilog();
        builder.Services.AddCarter();
        builder.Services.AddGrpc();
        builder.Services.AddValidatorsFromAssembly(assembly);
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

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
        if (app.Environment.IsDevelopment())
        {
            app.InitializeDatabaseAsync();
        }

        app.MapCarter();
        app.UseExceptionHandler(_ => { });
        app.MapGrpcService<BuddiesService>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Buddies API");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}