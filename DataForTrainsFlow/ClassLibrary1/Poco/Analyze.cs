using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryClass.Poco
{
    public class Analyze
    {
        public int Id { get; set; }
        public int Delay { get; set; }
        public string StationDepart { get; set; }
        public string StationArrival { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public string Vehicle { get; set; }
        public Weather Weather { get; set; }
    }
}
