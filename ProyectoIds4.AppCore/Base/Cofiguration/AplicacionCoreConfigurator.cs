using Microsoft.Extensions.DependencyInjection;
using ProyectoIds4.AppCore.IdentityServer4;

namespace ProyectoIds4.AppCore.Base.Cofiguration;

public static class AplicacionCoreConfigurator
{
    public static void ConfigureAplicacionCore(this IServiceCollection services)
    {
        services.ConfigureIdentityServer4Services();
    }
}