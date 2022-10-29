﻿using ProyectoIds4.Dto;
using System.Net.Http.Json;

namespace ProyectoIds4.UI.BWA.Client.WeatherForecastService;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(HttpClient httpClient, ILogger<WeatherForecastService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<WeatherForecastDto>> GetWeatherForecast()
    {
        var weatherForecast = await _httpClient.GetFromJsonAsync<WeatherForecastDto[]>("WeatherForecastIds");

        if (weatherForecast == null)
            _logger.LogError("Error" + "IEnumerable<WeatherForecastDto> null");

        return weatherForecast;
    }
}
