using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDataTrainsFlow.Model;
using LibraryClass.Poco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDataTrainsFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeController : ControllerBase
    {
        private Context _context;
        public AnalyzeController(Context context)
        {
            _context = context;
        }
        [Route("GetAll")]
        [HttpGet]
        public ActionResult<List<Analyze>> GetAll(string id)
        {
            return _context.Analyzes.Include(e => e.Weather).ToList();
        }
        [Route("Add")]
        [HttpGet]
        public ActionResult<Analyze> Add(int idWeather, string stationDepart, string stationArrival, int time, int delay,string vehicle)
        {
            Analyze newAnalyze = new Analyze
            {
                Weather = (from e in _context.Weathers where e.Id == idWeather select e).FirstOrDefault(),
                StationDepart = stationDepart,
                StationArrival = stationArrival,
                Day = UnixTimeStampToDateTime(time).DayOfWeek,
                Month = UnixTimeStampToDateTime(time).Month,
                Delay =delay,
                Vehicle=vehicle
            };
            _context.Analyzes.Add(newAnalyze);
            _context.SaveChanges();
            return newAnalyze;
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}