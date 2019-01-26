using DataWeatherForTrainsFlow;
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
                //new DateTime(2019,1,1,13,53,0),
            };
            WebClient webClient = new WebClient();
            while (true)
            {
                DateTime now = DateTime.Now;
                for(int i = 0; i < tab.Length; i++)
                {
                    if(now.Hour==tab[i].Hour && now.Minute == tab[i].Minute)
                    {
                        new WeatherAnalyse(now).Analyse();
                    }
                }
            }
        }
    }
}