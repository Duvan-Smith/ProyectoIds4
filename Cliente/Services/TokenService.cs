using IdentityModel.Client;

namespace Cliente.Services;

public class TokenService : ITokenService
{
    public async Task<TokenResponse> GetToken(string scope = "weatherApi.read")
    {
        var client = new HttpClient();
        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = "https://localhost:7132/connect/token",
            ClientId = "weatherApi",
            ClientSecret = "weatherApiProCodeGuide123456789987654321",
            Scope = scope
        });
        if (tokenResponse.IsError)
            throw new Exception("Error token");
        else
            return tokenResponse;
    }
}
