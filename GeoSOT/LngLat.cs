using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    public class LngLat
    {
        public double Lng { get; set; }
        public double Lat { get; set; }

        public LngLat() { }

        public LngLat(string dms)
        {
            var str = dms.Split(",");
            var tileUtils = new TileUtils();
            var lat = tileUtils.GetLat(str[0]);
            var lng = tileUtils.GetLng(str[1]);
        }

        public string Code
        {
            get
            {
                var utils = new TileUtils();
                return string.Format("{0}, {1}",
                    new LngLatSegments(this.Lat).ToString(),
                    new LngLatSegments(this.Lng).ToString());
            }
        }

        public override string ToString()
        {
            var utils = new TileUtils();
            return string.Format("{0}, {1}",
                utils.GetLatDMS(this.Lat),
                utils.GetLngDMS(this.Lng));
        }
    }
}
