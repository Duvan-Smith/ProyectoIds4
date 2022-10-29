using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoIds4.AppCore.IdentityServer4;
using ProyectoIds4.Dto;

namespace ProyectoIds4.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private IIdentityServer4Service _identityServer4Service = null;
    public WeatherController(IIdentityServer4Service identityServer4Service)
    {
        _identityServer4Service = identityServer4Service;
    }
    [HttpGet]
    public async Task<IEnumerable<WeatherForecastDto>> Get()
    {
        var OAuth2Token = await _identityServer4Service.GetToken("weatherApi.read");
        using (var client = new HttpClient())
        {
            client.SetBearerToken(OAuth2Token.AccessToken);
            var result = await client.GetAsync("https://localhost:7245/weatherforecast");
            if (result.IsSuccessStatusCode)
            {
                var model = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<WeatherForecastDto>>(model);
            }
            else
            {
                throw new Exception("Some Error while fetching Data");
            }
        }
    }
}