using IdentityModel.Client;

namespace ProyectoIds4.AppCore.IdentityServer4;

public interface IIdentityServer4Service
{
    Task<TokenResponse> GetToken(string apiScope);
}
