using System.Reflection;
using BuildingBlocks.Authentication;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using ProfileManagement.Application.Common;

namespace ProfileManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(serviceConfiguration =>
        {
            serviceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            serviceConfiguration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            serviceConfiguration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        MapsterConfig.Configure();

        services.AddFeatureManagement();

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();

        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}