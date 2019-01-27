using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDataTrainsFlow.Model;
using LibraryClass.Poco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return _context.Analyzes.ToList();
        }
        [Route("Add")]
        [HttpGet]
        public ActionResult<Analyze> Add(int idWeather, string stationDepart, string stationArrival, int time, int delay,string vehicle)
        {
            Analyze newAnalyze = new Analyze
            {
                Weather = (from e in _context.Weathers where e.Id == idWeather select e).FirstOrDefault(),
                StationDepart = stationDepart,
                StationArrival=stationArrival,
                Time=time,
                Delay=delay,
                Vehicle=vehicle
            };
            _context.Analyzes.Add(newAnalyze);
            _context.SaveChanges();
            return newAnalyze;
        }
    }
}