using BuildingBlocks;
using IdentityService.Configurations;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Extensions;

public static class IdentityServerExtensions
{
    public static WebApplicationBuilder AddCustomIdentityServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidateOptions<AuthOptions>();
        var authOptions = builder.Services.GetOptions<AuthOptions>(nameof(AuthOptions));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(config =>
            {
                config.Password.RequiredLength = 3;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var identityServerBuilder = builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.IssuerUri = authOptions.IssuerUri;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>()
            .AddResourceOwnerValidator<UserValidator>();

        //ref: https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html
        identityServerBuilder.AddDeveloperSigningCredential();

        return builder;
    }
}