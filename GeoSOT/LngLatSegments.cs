using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    /// <summary>
    /// 经纬度的分段表达
    /// </summary>
    public class LngLatSegments
    {
        public readonly int DegreePresion = 6;
        public readonly int SecondPresion = 4;
        /// <summary>
        /// 标识经纬度的正负，1 表示负，0表示正
        /// </summary>
        public int G { get; set; }
        public int D { get; set; }
        public int M { get; set; }
        public double Seconds { get; set; }
        public int S { get; set; }
        public int S11 { get; set; } // 1/2048秒，2^11

        public double Degree
        {
            get
            {
                var s11 = Math.Round(this.S11 / 2048.0, SecondPresion);
                var degree = Math.Round(this.D + this.M / 60.0 + (this.S + s11) / 3600.0, DegreePresion);
                if(this.G > 0)
                {
                    degree = -degree;
                }
                return degree;
            }
        }

        public LngLatSegments(double degree)
        {
            G = degree < 0 ? 1 : 0;
            var rd = Math.Abs(Math.Round(degree, DegreePresion));
            D = (int)rd;
            var minutes = (rd - D) * 60;
            M = (int)minutes;
            Seconds = Math.Round((minutes - M) * 60, SecondPresion);
            S = (int)Seconds;
            var dotSeconds = (Seconds - S) * 2048;
            S11 = (int)dotSeconds;
        }
    }
}
