using ProyectoIds4.Dto;

namespace ProyectoIds4.UI.BWA.Server.HttpClienteWeatherForecast;

public interface IWeatherForecastClientHttp
{
    Task<WeatherForecastDto[]> Get(string? token = null);
}
