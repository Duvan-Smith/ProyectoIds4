using IdentityServer4.Models;
using ProyectoIds4.IdentityServer4AspNet.Configs.App;

namespace ProyectoIds4.IdentityServer4AspNet.Configs.Scopes;

public static class ScopesHelper
{
    public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>
        {
            new HojaDeVidaApp().GetApiScope(),
        };
}