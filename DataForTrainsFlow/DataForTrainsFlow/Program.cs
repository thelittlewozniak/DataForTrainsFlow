using DataWeatherForTrainsFlow.APIWeather;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace DataForTrainsFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime[] tab = new DateTime[]
            {
                new DateTime(2019,1,1,04,44,0),
                new DateTime(2019,1,1,05,44,0),
                new DateTime(2019,1,1,06,12,0),
                new DateTime(2019,1,1,06,44,0),
                new DateTime(2019,1,1,07,12,0),
                new DateTime(2019,1,1,07,44,0),
                new DateTime(2019,1,1,08,12,0),
                new DateTime(2019,1,1,08,44,0),
                new DateTime(2019,1,1,09,12,0),
                new DateTime(2019,1,1,09,44,0),
                new DateTime(2019,1,1,10,12,0),
                new DateTime(2019,1,1,10,44,0),
                new DateTime(2019,1,1,11,12,0),
                new DateTime(2019,1,1,11,44,0),
                new DateTime(2019,1,1,12,12,0),
                new DateTime(2019,1,1,12,44,0),
                new DateTime(2019,1,1,13,12,0),
                new DateTime(2019,1,1,13,44,0),
                new DateTime(2019,1,1,14,12,0),
                new DateTime(2019,1,1,14,44,0),
                new DateTime(2019,1,1,15,12,0),
                new DateTime(2019,1,1,15,44,0),
                new DateTime(2019,1,1,16,12,0),
                new DateTime(2019,1,1,16,44,0),
                new DateTime(2019,1,1,17,12,0),
                new DateTime(2019,1,1,17,44,0),
                new DateTime(2019,1,1,18,12,0),
                new DateTime(2019,1,1,18,44,0),
                new DateTime(2019,1,1,19,12,0),
                new DateTime(2019,1,1,19,44,0),
                new DateTime(2019,1,1,20,12,0),
                new DateTime(2019,1,1,20,44,0),
                new DateTime(2019,1,1,21,12,0),
                new DateTime(2019,1,1,21,44,0),
                new DateTime(2019,1,1,22,12,0),
                new DateTime(2019,1,1,22,47,0),

                //debug
                //new DateTime(2019,1,1,10,29,0),
            };
            WebClient webClient = new WebClient();
            while (true)
            {
                DateTime now = DateTime.Now;
                for(int i = 0; i < tab.Length; i++)
                {
                    if(now.Hour==tab[i].Hour && now.Minute == tab[i].Minute)
                    {
                        Console.WriteLine("*********************************************************************");
                        Console.WriteLine("Begin the analyze time:"+now.ToString());
                        string url = "http://dataservice.accuweather.com/currentconditions/v1/28244?apikey=W9GYs6vGJ9SJGIKv2EM7hMeHSnw6xu9C";
                        var json = webClient.DownloadString(url);
                        var dataAPI = JsonConvert.DeserializeObject<List<Weather>>(json);
                        Console.WriteLine("Adding into the database");
                        Console.WriteLine("Weather:" + dataAPI[0].WeatherText);
                        Console.WriteLine("Precipitaion:" + dataAPI[0].HasPrecipitation);
                        Console.WriteLine("Temperature:" + dataAPI[0].Temperature.metric.Value);
                        string addingData = "http://weathertrainsflow.azurewebsites.net/api/Weather/Add?weatherText=" + dataAPI[0].WeatherText + "&hasPrecipitation=" + dataAPI[0].HasPrecipitation + "&precipitationType=" + dataAPI[0].PrecipitationType + "&relativeHumidity=" + dataAPI[0].RelativeHumidity + "&temperature=" + (int)dataAPI[0].Temperature.metric.Value + "&dateTime=" + now.ToString("MM/dd/yyyy HH:mm");
                        json =new WebClient().DownloadString(addingData);
                        System.Threading.Thread.Sleep(60000);
                        Console.WriteLine("End the analyze time:" + now.ToString());
                        Console.WriteLine("*********************************************************************");
                    }
                }
            }
        }
    }
}