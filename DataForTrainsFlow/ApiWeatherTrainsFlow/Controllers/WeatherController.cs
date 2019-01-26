using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWeatherTrainsFlow.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryClass.Poco;


namespace ApiWeatherTrainsFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private Context _context;
        public WeatherController(Context context)
        {
            _context = context;
        }
        [Route("GetAll")]
        [HttpGet]
        public ActionResult<List<Weather>> GetAll(string id)
        {
            return _context.Weathers.ToList();
        }
        [Route("Add")]
        [HttpGet]
        public ActionResult<Weather> Add(string weatherText,string hasPrecipitation,string precipitationType, int relativeHumidity,double temperature,string dateTime)
        {
            Weather newWeather = new Weather
            {
                DateTime = DateTime.Parse(dateTime),
                WeatherText=weatherText,
                HasPrecipitation=bool.Parse(hasPrecipitation),
                PrecipitationType=precipitationType,
                RelativeHumidity=relativeHumidity,
                Temperature=temperature
            };
            _context.Weathers.Add(newWeather);
            _context.SaveChanges();
            return newWeather;
        }
    }
}