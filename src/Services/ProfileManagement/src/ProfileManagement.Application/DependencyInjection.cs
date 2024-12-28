using System.Reflection;
using BuildingBlocks.Authentication;
using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using ProfileManagement.Application.Common;

namespace ProfileManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        MapsterConfig.Configure();

        services.AddFeatureManagement();

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();

        return services;
    }
}