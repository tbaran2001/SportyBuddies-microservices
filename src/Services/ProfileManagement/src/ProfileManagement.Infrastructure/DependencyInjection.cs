using BuildingBlocks.Jwt;
using BuildingBlocks.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfileManagement.Domain.Common;
using ProfileManagement.Domain.Interfaces;
using ProfileManagement.Infrastructure.Interceptors;
using ProfileManagement.Infrastructure.Repositories;

namespace ProfileManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IProfilesRepository, ProfilesRepository>();

        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());

        services.AddIdentityServices();

        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        services.AddJwt();
        services.AddHttpContextAccessor();
        services.AddTransient<AuthHeaderHandler>();

        return services;
    }

}