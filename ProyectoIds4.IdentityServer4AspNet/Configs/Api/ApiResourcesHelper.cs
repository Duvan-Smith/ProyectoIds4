using IdentityServer4.Models;
using ProyectoIds4.IdentityServer4AspNet.Configs.App;

namespace ProyectoIds4.IdentityServer4AspNet.Configs.Api;

public static class ApiResourcesHelper
{
    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new HojaDeVidaApp().GetApiResource(),
        };
}