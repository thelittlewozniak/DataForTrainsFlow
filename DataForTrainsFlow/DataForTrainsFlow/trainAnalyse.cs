using DataWeatherForTrainsFlow.APITrain;
using LibraryClass.Poco;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DataWeatherForTrainsFlow
{
    class TrainAnalyse
    {
        private readonly WebClient webClient;
        private readonly string url;
        private readonly DateTime now;
        public TrainAnalyse()
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
                    for(int i = 0; i < weatherAPI.Count; i++)
                    {
                        var json = webClient.DownloadString("https://api.irail.be/connections/?from=Charleroi&to=Mons&date=" + weatherAPI[i].DateTime.ToString("ddMMy") + "&format=json&time=" + weatherAPI[i].DateTime.ToString("HHmm"));
                        var dataAPI = JsonConvert.DeserializeObject<Result>(json);
                        if (UnixTimeStampToDateTime(dataAPI.connection[0].departure.time) == weatherAPI[i].DateTime)
                        {
                            string addingData = "http://weathertrainsflow.azurewebsites.net/api/Weather/Add?weatherText=";
                            json = webClient.DownloadString(addingData);

                        }
                    }
                }
                catch (Exception e)
                {
                    w.WriteLine("Error(s):" + e.Message);
                }
                System.Threading.Thread.Sleep(60000);
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
    }

}