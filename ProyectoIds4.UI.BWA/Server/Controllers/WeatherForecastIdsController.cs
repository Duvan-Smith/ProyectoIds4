

using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoIds4.AppCore.IdentityServer4;
using ProyectoIds4.Dto;
using ProyectoIds4.UI.BWA.Server.HttpClienteWeatherForecast;
using System.Security.Claims;

namespace ProyectoIds4.UI.BWA.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WeatherForecastIdsController : ControllerBase
{
    private readonly ILogger<WeatherForecastIdsController> _logger;
    private readonly IIdentityServer4Service _identityServer4Service;
    private readonly IWeatherForecastClientHttp _weatherForecastClientHttp;

    public WeatherForecastIdsController(ILogger<WeatherForecastIdsController> logger,
        IIdentityServer4Service identityServer4Service,
        IWeatherForecastClientHttp weatherForecastClientHttp)
    {
        _logger = logger;
        _identityServer4Service = identityServer4Service;
        _weatherForecastClientHttp = weatherForecastClientHttp;
    }

    [HttpGet(nameof(Login))]
    public async Task Login()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "duvan"),
            new Claim("UserName", "duvan"),
            new Claim(ClaimTypes.Email, "dsmith.mr@gmail.com"),
            new Claim(ClaimTypes.Role, "Administrator"),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = false,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.

            IsPersistent = false,
            // Whether the authentication session is persisted across 
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            RedirectUri = "https://localhost:7167/counter"
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.LoginPath,
            new ClaimsPrincipal(claimsIdentity),
            authProperties).ConfigureAwait(false);
    }

    [HttpGet(nameof(GetToken))]
    public async Task<string> GetToken()
    {
        return await HttpContext.GetTokenAsync("access_token").ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var OAuth2Token = await _identityServer4Service.GetToken("weatherApi.read");

        var weatherForecast = await _weatherForecastClientHttp.Get(OAuth2Token.AccessToken).ConfigureAwait(false);
        if (weatherForecast == null)
            _logger.LogError("Error" + "IEnumerable<WeatherForecast> null");

        return weatherForecast;
    }

    [HttpGet(nameof(GetIds))]
    public async Task<IEnumerable<WeatherForecast>> GetIds()
    {
        //var token = await HttpContext.GetTokenAsync("access_token");

        //var weatherForecast = await _weatherForecastClientHttp.Get(token).ConfigureAwait(false);
        //if (weatherForecast == null)
        //    _logger.LogError("Error" + "IEnumerable<WeatherForecastDto> null");
        using var _client = new HttpClient();

        var token = await HttpContext.GetTokenAsync("access_token");

        _client.SetBearerToken(token);

        var weatherForecast = await _client.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7245/weatherforecast");

        return weatherForecast;
    }
}