using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoIds4.AppCore.IdentityServer4;
using ProyectoIds4.Dto;

namespace ProyectoIds4.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IIdentityServer4Service _identityServer4Service;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IIdentityServer4Service identityServer4Service)
    {
        _logger = logger;
        _identityServer4Service = identityServer4Service;
    }

    [AllowAnonymous]
    [HttpGet(nameof(GetToken))]
    public async Task<TokenResponse> GetToken()
    {
        return await _identityServer4Service.GetToken("weatherApi.read").ConfigureAwait(false);
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecastDto> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}