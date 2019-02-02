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
                        Byte[] info = new UTF8Encoding(true).GetBytes(item.StationDepart + "," + item.StationArrival + "," + item.Day + "," + item.Vehicle + "," + item.Weather.WeatherText + "," + (float)item.Weather.Temperature + "," + (float)item.Weather.RelativeHumidity + "," + item.Weather.HasPrecipitation + "," + item.Weather.PrecipitationType+","+item.Weather.DateTime.ToString("HHmm")+","+(float)item.Delay);
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
        public ActionResult<string> Train()
        {
            try
            {
                var mlContext = new MLContext();
                var reader = mlContext.Data.CreateTextReader<TrainData>(separatorChar: ',', hasHeader: false);
                var trainData = _context.Datas.Last();
                IDataView trainingdataView = reader.Read(Path.Combine(hostingEnvironment.WebRootPath, trainData.Path + ".txt"));
                var pipeline = mlContext.Transforms.CopyColumns("Delay", "Label")
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("StationDepart"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("StationArrival"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Day"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Train"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("WeatherText"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("HasPrecipitation"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("PrecipitationType"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Time"))
                    .Append(mlContext.Transforms.Concatenate("Features", "StationDepart", "StationArrival", "Day", "Train", "WeatherText", "Temperature", "Humidity", "HasPrecipitation", "PrecipitationType", "Time"))
                    .Append(mlContext.Regression.Trainers.FastTree());
                var model = pipeline.Fit(trainingdataView);
                DateTime now = DateTime.Now;
                var path = "model/model" + now.ToString("ddMMyyyHHmm");
                using (var stream = System.IO.File.Create(Path.Combine(hostingEnvironment.WebRootPath, path)))
                {
                    mlContext.Model.Save(model, stream);
                }
                _context.Models.Add(
                    new Model()
                    {
                        DateTime = now,
                        Path = path
                    });
                _context.SaveChanges();
                var predictions = model.Transform(trainingdataView);
                var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
                return metrics.RSquared.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [Route("Predict")]
        [HttpGet]
        public ActionResult<float> Predict()
        {
            using (var stream=System.IO.File.OpenRead(Path.Combine(hostingEnvironment.WebRootPath, _context.Models.Last().Path)))
            {
                MLContext mlContext = new MLContext();
                var model = mlContext.Model.Load(stream);
                var prediction = model.CreatePredictionEngine<TrainData, TrainPrediction>(mlContext).Predict(
                    new TrainData()
                    {
                        StationDepart = "Charleroi-Sud",
                        StationArrival = "Mons",
                        Day = "Wednesday",
                        Train = "BE.NMBS.IC3839",
                        WeatherText = "Mostly clear",
                        Temperature = 0,
                        Humidity = 0,
                        HasPrecipitation = false,
                        PrecipitationType = null,
                        Time = "1844",
                        Delay=0
                    });
                return prediction.PredictedTimes;
            }
        }
    }
}