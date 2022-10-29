using ProyectoIds4.Dto;

namespace SeguridadToken.Presentacion.WeatherForecast;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecastDto>> GetWeatherForecast();
}
