using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    public class LngLatBbox
    {
        public double West { get; set; }
        public double South { get; set; }
        public double East { get; set; }
        public double North { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", West, South, East, North);
        }
    }
}
