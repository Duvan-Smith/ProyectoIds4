using ProyectoIds4.Dto;

namespace ProyectoIds4.UI.BWA.Client.WeatherForecastService;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecast();
}
