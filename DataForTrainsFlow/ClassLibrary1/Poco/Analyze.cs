using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryClass.Poco
{
    public class Analyze
    {
        public int Id { get; set; }
        public int delay { get; set; }
        public string station { get; set; }
        public int time { get; set; }
        public string vehicle { get; set; }
        public Weather weather { get; set; }
    }
}
