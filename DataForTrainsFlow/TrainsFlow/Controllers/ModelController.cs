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
using Microsoft.ML;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using TrainsFlow.Models;

namespace TrainsFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private Context _context;
        public ModelController(IHostingEnvironment environment,Context context)
        {
            hostingEnvironment = environment;
            _context = context;
        }
        [Route("CreateData")]
        [HttpGet]
        public ActionResult<string> CreateData()
        {
            try
            {
                DateTime now = DateTime.Now;
                string path = "data/data" + now.ToString("ddMMyyyHHmm");
                using (FileStream s = System.IO.File.Create(Path.Combine(hostingEnvironment.WebRootPath, path+".txt")))
                {
                    var data = new WebClient().DownloadString("http://weathertrainsflow.azurewebsites.net/api/Analyze/GetAll");
                    List<Analyze> analyzes = JsonConvert.DeserializeObject<List<Analyze>>(data);
                    foreach (Analyze item in analyzes)
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(item.StationDepart + "," + item.StationArrival + "," + item.Day + "," + item.Vehicle + "," + item.Weather.WeatherText + "," + item.Weather.Temperature + "," + item.Weather.RelativeHumidity + "," + item.Weather.HasPrecipitation + "," + item.Weather.PrecipitationType+","+item.Weather.DateTime.ToString("HHmm")+","+item.Delay);
                        s.Write(info, 0, info.Length);
                        byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                        s.Write(newline, 0, newline.Length);
                    }
                    _context.Datas.Add(new Data { DateTime = now, Path = path });
                    _context.SaveChanges();

                    return "ok";
                }
            }
            catch (IOException e)
            {

                return e.Message;
            }
        }
        [Route("Train")]
        [HttpGet]
        public ActionResult<string> train()
        {
            try
            {
                var mlContext = new MLContext();
                var reader = mlContext.Data.CreateTextReader<TrainData>(separatorChar: ',', hasHeader: false);
                var trainData = _context.Datas.Last();
                IDataView trainingdataView = reader.Read(trainData.Path + ".txt");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}