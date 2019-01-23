using System;
using System.Collections.Generic;
using System.Text;

namespace ApiWeatherTrainsFlow.Model
{
    public class Weather
    {
        public int Id { get; set; }
        public string WeatherText { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public int RelativeHumidity { get; set; }
        public double Temperature { get; set; }
        public DateTime DateTime { get; set; }
    }
}
