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

    public async Task<WeatherForecast[]> Get(string? token = null)
    {
        if (!string.IsNullOrEmpty(token))
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //var response = await _client.GetAsync("https://localhost:7245/weatherforecast").ConfigureAwait(false);
        WeatherForecast[]? forecasts;
        try
        {
            forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("weatherforecast");
            return forecasts;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
        //var response = await _client.GetAsync("weatherforecast").ConfigureAwait(false);
        //return JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
    }
}
