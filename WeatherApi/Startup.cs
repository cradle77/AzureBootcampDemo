using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecasts.Service;

[assembly: FunctionsStartup(typeof(WeatherApi.Startup))]

namespace WeatherApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
        }
    }
}
