using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LibraryClass.Poco;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TrainsFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public ModelController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        [Route("Get")]
        [HttpGet]
        public ActionResult<string> Createdata()
        {
            try
            {
                using (FileStream s = System.IO.File.Create(Path.Combine(hostingEnvironment.WebRootPath, "data/data" + DateTime.Now.ToString("ddMMyyyHHmm")+".txt")))
                {
                    var data = new WebClient().DownloadString("http://weathertrainsflow.azurewebsites.net/api/Analyze/GetAll");
                    List<Analyze> analyzes = JsonConvert.DeserializeObject<List<Analyze>>(data);
                    foreach (Analyze item in analyzes)
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(item.StationDepart + "," + item.StationArrival + "," + item.Day + "," + item.Vehicle + "," + item.Weather.WeatherText + "," + item.Weather.Temperature + "," + item.Weather.RelativeHumidity + "," + item.Weather.HasPrecipitation + "," + item.Weather.PrecipitationType+","+item.Delay);
                        s.Write(info, 0, info.Length);
                        byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                        s.Write(newline, 0, newline.Length);
                    }
                    return "ok";
                }
            }
            catch (IOException e)
            {

                return e.Message;
            }
        }
    }
}