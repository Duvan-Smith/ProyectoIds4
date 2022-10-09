using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ProyectoIds4.AppCore.IdentityServer4;

public static class IdentityServer4ServicesConfigurator
{
    public static void ConfigureIdentityServer4Services(this IServiceCollection services)
    {
        services.TryAddSingleton<IIdentityServer4Service, IdentityServer4Service>();
    }
}