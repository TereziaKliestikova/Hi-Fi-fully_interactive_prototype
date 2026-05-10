using Duende.IdentityServer.Models;

namespace HIPA_BE
{
    public class Config
    {
        // here we can configure the scopes that will be used in the application
        // for different resources (APIs, IdentityResources, ...)
        // also can be used for data seeding
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        // here are the clients that will be able to access the application
        // we need two clients for authorization flow without redirecting to identityserver4 login page
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "hipa_be",
                    ClientSecrets = { new Secret(Environment.GetEnvironmentVariable("CLIENT_SECRET_DEVELOPMENT").Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequirePkce = true,
                    AllowedScopes = { "openid", "profile", "email" },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 300, //set to 300 seconds because you cant invalidate a jwt, consider using reference tokens
                    AlwaysIncludeUserClaimsInIdToken = true
                },

                new Client
                {
                    ClientId = "hipa_fe",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    RedirectUris = { $"{Environment.GetEnvironmentVariable("FRONTEND_BASEURL")}/home" },
                    AllowedCorsOrigins = { Environment.GetEnvironmentVariable("FRONTEND_BASEURL") },
                    AllowedScopes = { "openid", "profile", "email" },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 7200, // use this to set token expiration on FE login
                    AlwaysIncludeUserClaimsInIdToken = true
                }
            };
    }
}