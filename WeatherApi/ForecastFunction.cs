using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WeatherForecasts.Service;

namespace WeatherApi
{
    public class ForecastFunction
    {
        private ILogger<ForecastFunction> _logger;
        private IWeatherForecastService _weatherForecast;

        public ForecastFunction(ILogger<ForecastFunction> logger, IWeatherForecastService weatherForecast)
        {
            _logger = logger;
            _weatherForecast = weatherForecast;
        }

        [FunctionName("GetForecast")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var forecast = _weatherForecast.GetForecast();

            return new OkObjectResult(forecast);
        }
    }
}
