using Microsoft.AspNetCore.Mvc;
using WebAppApi1.Services;

namespace WebAppApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly FileStoreService _fileStoreService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, FileStoreService fileStoreService)
        {
            _logger = logger;
            _fileStoreService= fileStoreService;

        }

        [HttpGet("File")]
        public string GetFile([FromHeader]string fileName)
        {
            return _fileStoreService.ReadFile(fileName);
        }

        [HttpPost("File")]
        public void PostFile(string fileName, string text)
        {
            _fileStoreService.SaveFile(fileName, text);
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}