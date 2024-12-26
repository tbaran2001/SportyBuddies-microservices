using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YarpApiGateway.Identity.Data;

namespace YarpApiGateway.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<IdentityApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = false;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
        });

        services.AddAuthorizationBuilder();

        services.AddIdentityApiEndpoints<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<IdentityApplicationDbContext>();

        services.AddScoped<UserManager<ApplicationUser>, CustomUserManager>();
        services.AddScoped<IdentityEventsHandler>();

        return services;
    }

    public static IEndpointRouteBuilder MapIdentityApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api")
            .WithTags("Identity")
            .MapCustomIdentityApi<ApplicationUser>();

        endpoints.MapPost("/api/logout", async (SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.Ok();
            })
            .WithTags("Identity");

        return endpoints;
    }
}