using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadyTech.BrewCoffee2023.API.Controllers
{
    [Route("brew-coffee")]
    public class BrewCoffeeController : Controller
    {
        private static int _callCount = 0;
        private const int _callLimit = 5;

        [HttpGet]
        public IActionResult Index()
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
            return Json(result);
        }
    }
}

