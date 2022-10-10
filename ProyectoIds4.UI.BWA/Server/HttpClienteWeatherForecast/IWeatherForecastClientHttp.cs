using ProyectoIds4.Dto;

namespace ProyectoIds4.UI.BWA.Server.HttpClienteWeatherForecast;

public interface IWeatherForecastClientHttp
{
    Task<WeatherForecast[]> Get(string? token = null);
}
