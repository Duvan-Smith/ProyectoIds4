using IdentityServer4.Models;
using ProyectoIds4.IdentityServer4AspNet.Configs.App;

namespace ProyectoIds4.IdentityServer4AspNet.Configs.Clients;

public static class ClientsHelper
{
    public static IEnumerable<Client> GetClients()
    {
        var hojaDeVidaApp = new HojaDeVidaApp();

        return new List<Client>
        {
            hojaDeVidaApp.GetApiClient(),
            hojaDeVidaApp.GetWebClient(),
        };
    }
}