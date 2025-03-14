using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker<TDbContext>(this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly = null)
        where TDbContext : DbContext
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"]!);
                    host.Password(configuration["MessageBroker:Password"]!);
                });
                configurator.ConfigureEndpoints(context);
            });

            config.AddEntityFrameworkOutbox<TDbContext>(options =>
            {
                options.QueryDelay = TimeSpan.FromSeconds(10);
                options.UseSqlServer();
                options.UseBusOutbox();
            });
        });

        return services;
    }
}