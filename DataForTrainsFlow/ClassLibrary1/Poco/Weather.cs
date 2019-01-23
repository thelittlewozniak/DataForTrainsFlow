using System;
using System.Collections.Generic;
using System.Text;

namespace CommonClasses.Poco
{
    class Weather
    {
        public int Id { get; set; }
        public string WeatherText { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public int RelativeHumidity { get; set; }
    }
}
