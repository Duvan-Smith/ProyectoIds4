using IdentityServer4.Models;

namespace ProyectoIds4.Empty.IdentityServer4AspNet.IdentityConfiguration;

public static class Scopes
{
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
            new ApiScope("weatherApi.read", "Read Access to Weather API"),
            new ApiScope("weatherApi.write", "Write Access to Weather API"),
        };
    }
}
