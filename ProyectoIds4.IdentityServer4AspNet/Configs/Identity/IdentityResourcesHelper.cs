using IdentityServer4.Models;

namespace ProyectoIds4.IdentityServer4AspNet.Configs.Identity;

public static class IdentityResourcesHelper
{
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
    new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource
        {
            Name = "role",
            UserClaims = new List<string> {"role"}
        }
    };
}
