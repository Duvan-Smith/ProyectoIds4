using IdentityServer4;
using IdentityServer4.Models;

namespace ProyectoIds4.Empty.IdentityServer4AspNet.IdentityConfiguration;

public static class Clients
{
    public static IEnumerable<Client> Get()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "weatherApi",
                ClientName = "ASP.NET Core Weather Api",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                AllowedScopes = new List<string> {"weatherApi.read"}
            },
            new Client
            {
                ClientId = "oidcMVCApp",
                ClientName = "Sample ASP.NET Core MVC Web App",
                ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = new List<string> {
                    "https://localhost:7167/",
                    "https://localhost:7167/signin-oidc"
                },
                PostLogoutRedirectUris = { "https://localhost:7167/signout-callback-oidc" },
                FrontChannelLogoutUri = "https://localhost:7167/signin-oidc",
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "weatherApi.read"
                },
                RequirePkce = true,
                AllowPlainTextPkce = false
            }
        };
    }
}
