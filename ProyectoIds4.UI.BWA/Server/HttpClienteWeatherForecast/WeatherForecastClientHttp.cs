using ProyectoIds4.Dto;
using System.Net.Http.Headers;

namespace ProyectoIds4.UI.BWA.Server.HttpClienteWeatherForecast;

public class WeatherForecastClientHttp : IWeatherForecastClientHttp
{
    private readonly HttpClient _client;
    public WeatherForecastClientHttp(HttpClient client)
    {
        _client = client ?? throw new Exception();
    }

    public async Task<WeatherForecastDto[]> Get(string? token = null)
    {
        if (!string.IsNullOrEmpty(token))
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //var response = await _client.GetAsync("https://localhost:7245/weatherforecast").ConfigureAwait(false);
        WeatherForecastDto[]? forecasts;
        try
        {
            forecasts = await _client.GetFromJsonAsync<WeatherForecastDto[]>("weatherforecast");
            return forecasts;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
        //var response = await _client.GetAsync("weatherforecast").ConfigureAwait(false);
        //return JsonConvert.DeserializeObject<IEnumerable<WeatherForecastDto>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
    }
}
