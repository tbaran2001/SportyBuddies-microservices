using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new("SportyBuddiesApp", "SportyBuddies app full access")
    ];

    public static IEnumerable<Client> Clients =>
    [
        new()
        {
            ClientId = "postman",
            ClientName = "Postman",
            AllowedScopes = { "openid", "profile", "SportyBuddiesApp" },
            RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
            ClientSecrets = [new Secret("NotSoSecret".Sha256())],
            AllowedGrantTypes = { GrantType.ResourceOwnerPassword }
        }
    ];
}