using IdentityModel.Client;

namespace Cliente.Services;

public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}
