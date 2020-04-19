using System.Collections.Generic;
using WeatherForecasts.Shared;

namespace WeatherForecasts.Service
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetForecast();
    }
}
