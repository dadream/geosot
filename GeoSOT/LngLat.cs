using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    /// <summary>
    /// 经纬度
    /// </summary>
    public class LngLat
    {
        public LngLatSegments Lng { get; private set; }
        public LngLatSegments Lat { get; private set; }

        public LngLat() { }

        public LngLat(double lat, double lng)
        {
            this.Lat = new LngLatSegments(lat);
            this.Lng = new LngLatSegments(lng);
        }

        public LngLat(string dms)
        {
            var str = dms.Split(",");
            this.Lat = new LngLatSegments(str[0]);
            this.Lng = new LngLatSegments(str[1]);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="b">SOT角点二维编码下侧</param>
        /// <param name="l">SOT角点二维编码左侧</param>
        public LngLat(UInt32 b, UInt32 l)
        {
            this.Lat = new LngLatSegments(b, false);
            this.Lng = new LngLatSegments(l, true);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}",
                this.Lat.DMS, this.Lng.DMS);
        }
    }
}
