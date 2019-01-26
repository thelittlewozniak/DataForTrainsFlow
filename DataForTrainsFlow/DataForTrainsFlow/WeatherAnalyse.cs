using DataWeatherForTrainsFlow.APIWeather;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DataWeatherForTrainsFlow
{
    class WeatherAnalyse
    {
        private readonly WebClient webClient;
        private readonly string url;
        private readonly DateTime now;
        public WeatherAnalyse(DateTime now)
        {
            webClient = new WebClient();
            url = "http://dataservice.accuweather.com/currentconditions/v1/28244?apikey=W9GYs6vGJ9SJGIKv2EM7hMeHSnw6xu9C";
            this.now = now;
        }
        public void Analyse()
        {
            Console.WriteLine("*********************************************************************");
            Console.WriteLine("Begin the analyze time:" + now.ToString());
            try
            {
                var json = webClient.DownloadString(url);
                var dataAPI = JsonConvert.DeserializeObject<List<Weather>>(json);
                Console.WriteLine("Adding into the database");
                Console.WriteLine("Weather:" + dataAPI[0].WeatherText);
                Console.WriteLine("Precipitaion:" + dataAPI[0].HasPrecipitation);
                Console.WriteLine("Temperature:" + dataAPI[0].Temperature.metric.Value);
                string addingData = "http://weathertrainsflow.azurewebsites.net/api/Weather/Add?weatherText=" + dataAPI[0].WeatherText + "&hasPrecipitation=" + dataAPI[0].HasPrecipitation + "&precipitationType=" + dataAPI[0].PrecipitationType + "&relativeHumidity=" + dataAPI[0].RelativeHumidity + "&temperature=" + (int)dataAPI[0].Temperature.metric.Value + "&dateTime=" + now.ToString("MM/dd/yyyy HH:mm");
                json = new WebClient().DownloadString(addingData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error(s):" + e.Message);
            }
            System.Threading.Thread.Sleep(60000);
            Console.WriteLine("End the analyze time:" + now.ToString());
            Console.WriteLine("*********************************************************************");
        }
    }
}
