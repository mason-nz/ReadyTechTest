using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReadyTech.BrewCoffee2023New.API.Common;
using ReadyTech.BrewCoffee2023New.API.Properties;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadyTech.BrewCoffee2023New.API.Controllers
{
    [Route("brew-coffee")]
    public class BrewCoffeeController : Controller
    {
        private readonly AppSettings _settings;
        private static int _callCount = 0;
        private const int _callLimit = 5;
        private const double _tempVal = 30 + 273.15;// kelvin = celsius + 273

        private OpenWeatherHelper _openWeatherHelper;

        public BrewCoffeeController(IOptions<AppSettings> options)
        {
            _settings = options.Value;
            _openWeatherHelper = new OpenWeatherHelper(_settings.openWeatherAPI);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _callCount++; //add call count 1

            var currentLocalDateTime = DateTime.Now;

            // if today was 1st April
            if (currentLocalDateTime.Day == 1 && currentLocalDateTime.Month == 4)
            {
                return StatusCode(StatusCodes.Status418ImATeapot, "");
            }

            // if called count == 5 times
            if (_callCount >= _callLimit)
            {
                _callCount = 0;
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "");
            }

            dynamic result = new ExpandoObject();
            result.message = "Your piping hot coffee is ready";
            //result.callCount = _callCount;
            /*
             * format: zzz +13:00
             * format: zz  +13
             */
            result.prepared = currentLocalDateTime.ToString("yyyy-MM-ddTHH:mm:sszz00");


            /*
             * if the current temperature is > 30°C (30 + 273.15 kelvin), 
             * then the returned message should be changed to “Your refreshing iced coffee is ready”;
             */
            //result.OpenWeatherAPISettings = _settings.openWeatherAPI;
            var openWeatherResult = await _openWeatherHelper.GetCurrentCityWeather();

            //result.OpenWeatherAPIResult = openWeatherResult;

            if (openWeatherResult.Cod == "200" && openWeatherResult.List.Count > 0
                && openWeatherResult.List[0].openWeatherMain.Temp > _tempVal)
            //.Any(i => i.DtDate.ToString("yyyy-MM-dd HH") == currentLocalDateTime.ToString("yyyy-MM-dd HH") && i.openWeatherMain.Temp > _tempVal))
            {
                result.message = "Your refreshing iced coffee is ready";
            }

            return Json(result);
        }
    }
}

