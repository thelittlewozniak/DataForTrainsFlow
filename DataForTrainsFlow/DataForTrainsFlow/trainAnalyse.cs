using DataWeatherForTrainsFlow.APITrain;
using LibraryClass.Poco;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DataWeatherForTrainsFlow
{
    class TrainAnalyse
    {
        private readonly WebClient webClient;
        private readonly string url;
        private readonly DateTime now;
        public TrainAnalyse(DateTime now)
        {
            webClient = new WebClient();
            url = "http://weathertrainsflow.azurewebsites.net/api/Weather/getAll";
            this.now = now;
        }
        public void Analyse()
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.WriteLine("*********************************************************************");
                w.WriteLine("Begin the analyze time:" + now.ToString());
                try
                {
                    var weatherData = webClient.DownloadString(url);
                    List<Weather> weatherAPI = JsonConvert.DeserializeObject<List<Weather>>(weatherData);
                    weatherAPI = (from e in weatherAPI where e.DateTime.Day == now.Day && e.DateTime.Month==now.Month && e.DateTime.Year==now.Year select e).ToList();
                    for(int i = 0; i < weatherAPI.Count; i++)
                    {
                        var json = webClient.DownloadString("https://api.irail.be/connections/?from=Charleroi&to=Mons&date=" + weatherAPI[i].DateTime.ToString("ddMMy") + "&format=json&time=" + weatherAPI[i].DateTime.ToString("HHmm"));
                        var dataAPI = JsonConvert.DeserializeObject<Result>(json);
                        if (UnixTimeStampToDateTime(dataAPI.connection[0].departure.time) == weatherAPI[i].DateTime)
                        {
                            string addingData = "http://weathertrainsflow.azurewebsites.net/api/Analyze/Add?idWeather=" + weatherAPI[i].Id + "&delay=" + dataAPI.connection[0].arrival.delay + "&stationDepart=" + dataAPI.connection[0].departure.station + "&stationArrival=" + dataAPI.connection[0].arrival.station + "&vehicle=" + dataAPI.connection[0].arrival.vehicle + "&time=" + dataAPI.connection[0].departure.time;
                            json = webClient.DownloadString(addingData);
                        }
                    }
                }
                catch (Exception e)
                {
                    w.WriteLine("Error(s):" + e.Message);
                }
                w.WriteLine("End the analyze time:" + now.ToString());
                w.WriteLine("*********************************************************************");
            }
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }

}