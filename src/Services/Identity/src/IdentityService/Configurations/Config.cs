using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityService.Identity;

namespace IdentityService.Configurations;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResources.Phone(),
        new IdentityResources.Address(),
        new(Constants.StandardScopes.Roles, new List<string> { "role" })
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new(Constants.StandardScopes.ProfileManagementApi),
        new(Constants.StandardScopes.SportApi),
        new(Constants.StandardScopes.MatchingApi),
        new(Constants.StandardScopes.BuddiesApi),
    ];

    public static IList<ApiResource> ApiResources =>
    [
        new(Constants.StandardScopes.ProfileManagementApi),
        new(Constants.StandardScopes.SportApi),
        new(Constants.StandardScopes.MatchingApi),
        new(Constants.StandardScopes.BuddiesApi),
    ];

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new()
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    Constants.StandardScopes.ProfileManagementApi,
                    Constants.StandardScopes.SportApi,
                    Constants.StandardScopes.MatchingApi,
                    Constants.StandardScopes.BuddiesApi,
                    Constants.StandardScopes.IdentityApi
                },
                AccessTokenLifetime = 3600,
                IdentityTokenLifetime = 3600
            }
        };
}