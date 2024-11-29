using Kube.Lib;
using Microsoft.AspNetCore.Mvc;

namespace Kube.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Getting WeatherForecast From Service Lib...");
            return new WeatherService().GetWeatherForecast();
        }
    }
}
