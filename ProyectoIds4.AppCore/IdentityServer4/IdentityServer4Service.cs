using IdentityModel.Client;

namespace ProyectoIds4.AppCore.IdentityServer4;

public class IdentityServer4Service : IIdentityServer4Service
{
    private DiscoveryDocumentResponse _discoveryDocument { get; set; }

    public IdentityServer4Service()
    {
        using (var client = new HttpClient())
        {
            _discoveryDocument = client.GetDiscoveryDocumentAsync("https://localhost:7164/.well-known/openid-configuration").Result;
        }
    }

    public async Task<TokenResponse> GetToken(string apiScope)
    {
        using (var client = new HttpClient())
        {
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,
                ClientId = "weatherApi",
                Scope = apiScope,
                ClientSecret = "ProCodeGuide"
            });
            if (tokenResponse.IsError)
            {
                throw new Exception("Token Error");
            }
            return tokenResponse;
        }
    }
}
