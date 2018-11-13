using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    public class LngLat
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public override string ToString()
        {
            var utils = new TileUtils();
            return string.Format("{0}, {1}",
                utils.GetLatDMS(this.Lat), 
                utils.GetLongDMS(this.Lng));
        }
    }
}
