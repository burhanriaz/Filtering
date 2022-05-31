using Filtering.Custom_Filter;
using Microsoft.AspNetCore.Mvc;


namespace Filtering.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Response()]
    public class WeatherForecastController : ControllerBase
    {


        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        // [AjaxOnly] // Create custom Filter for ajax call
        public IEnumerable<WeatherForecast> Get()
        {
           // var result = Request.IsAjaxRequest();// create method for check ajax call

            if (Request.IsAjaxRequest() == true)
            {
              //  return PartialView("name Partial");
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
          .ToArray();
            }
            else
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

        // Here direct check for ajax call

        //public IEnumerable<WeatherForecast> Burhan()
        //{

        //    string requestedWith = HttpContext.Request.Headers["X-Requested-With"];
        //    if (requestedWith == "XMLHttpRequest")
        //    {
        //      return PartialView();
        //    }
        //    else
        //    {
        //        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(index),
        //            TemperatureC = Random.Shared.Next(-20, 55),
        //            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //        }) .ToArray();
        //    }
        //}
    }
}