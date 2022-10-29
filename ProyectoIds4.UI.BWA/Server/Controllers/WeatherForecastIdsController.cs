

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoIds4.AppCore.IdentityServer4;
using ProyectoIds4.Dto;
using ProyectoIds4.UI.BWA.Server.HttpClienteWeatherForecast;

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

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var OAuth2Token = await _identityServer4Service.GetToken("weatherApi.read");

        var weatherForecast = await _weatherForecastClientHttp.Get(OAuth2Token.AccessToken).ConfigureAwait(false);
        if (weatherForecast == null)
            _logger.LogError("Error" + "IEnumerable<WeatherForecast> null");

        return weatherForecast;
    }
}