using System;
using System.Collections.Generic;
using System.Text;

namespace DataWeatherForTrainsFlow.APIWeather
{
    class Weather
    {
        public string WeatherText { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public Temperature Temperature { get; set; }
        public double RelativeHumidity { get; set; }
    }
}
