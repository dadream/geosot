using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    /// <summary>
    /// 地理坐标的分段表达
    /// </summary>
    public class LngLatSegments
    {
        public readonly int DegreePresion = 6;
        public readonly int SecondPresion = 4;

        private Dictionary<string, bool> LngDic = new Dictionary<string, bool>
        {
            { "N", false},
            { "E", true},
            { "W", true },
            { "S", false}
        };

        private Dictionary<string, int> GDic = new Dictionary<string, int>
        {
            { "N", 1},
            { "E", 1},
            { "W", 0 },
            { "S", 0}
        };

        /// <summary>
        /// 标识经纬度的正负，1 表示负，0表示正
        /// </summary>
        public UInt32 G { get; private set; }
        /// <summary>
        /// 是否为经度，经度为真，维度为假
        /// </summary>
        public bool IsLng { get; private set; }
        /// <summary>
        /// 十进制度
        /// </summary>
        public UInt32 D { get; private set; }
        /// <summary>
        /// 十进制分
        /// </summary>
        public UInt32 M { get; private set; }
        /// <summary>
        /// 十进制秒
        /// </summary>
        public UInt32 S { get; private set; }
        /// <summary>
        /// 十进制1/2048秒，2^11
        /// </summary>
        public UInt32 S11 { get; private set; }
        /// <summary>
        /// 浮点数秒
        /// </summary>
        public double Seconds
        {
            get
            {
                var s11 = Math.Round(this.S11 / 2048.0, SecondPresion);
                return this.S + s11;
            }
        }

        /// <summary>
        /// 十进制经纬度表达
        /// </summary>
        public double Degree
        {
            get
            {
                var degree = Math.Round(this.D + this.M / 60.0 + this.Seconds / 3600.0, DegreePresion);
                if (this.G > 0)
                {
                    degree = -degree;
                }
                return degree;
            }
        }

        /// <summary>
        /// 度分秒表达
        /// </summary>
        public string DMS
        {
            get
            {
                return string.Format("{0}° {1}' {2}\" {3}",
                D, M, Seconds, GetHalfCharactor());
            }
        }

        /// <summary>
        /// 经纬度1d编码
        /// </summary>
        public UInt32 Code
        {
            get
            {
                return G << 31 | D << 23 | M << 17 | S << 11 | S11;
            }
        }

        public LngLatSegments() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="degree">经纬度</param>
        public LngLatSegments(double degree)
        {
            G = (UInt32)(degree < 0 ? 1 : 0);
            var rd = Math.Abs(Math.Round(degree, DegreePresion));
            D = (UInt32)rd;
            var minutes = (rd - D) * 60;
            M = (UInt32)minutes;
            var seconds = Math.Round((minutes - M) * 60, SecondPresion);
            S = (UInt32)seconds;
            var dotSeconds = (seconds - S) * 2048;
            S11 = (UInt32)Math.Round(dotSeconds);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dms">度分秒</param>
        public LngLatSegments(string dms)
        {
            var list = dms.Split(new char[] { '°', '\'', '\"' });
            D = UInt32.Parse(list[0].Trim(' '));
            M = UInt32.Parse(list[1].Trim(' '));
            var seconds = double.Parse(list[2].Trim(' '));
            S = (UInt32)seconds;
            var dotSeconds = (seconds - S) * 2048;
            S11 = (UInt32)Math.Round(dotSeconds);
            var half = list[3].Trim(' ');
            if (half == "S" || half == "W") { G = 1; }
            else if (half == "N" || half == "E") { }
            else { throw new ArgumentException(); }
            IsLng = half == "W" || half == "E";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x">2d编码</param>
        /// <param name="isL"></param>
        public LngLatSegments(UInt32 x, bool isL)
        {
            IsLng = isL;
            G = x >> 31; // 1b
            D = (x >> 23) & 0xFF; // 8b
            M = (x >> 17) & 0x3F; // 6b
            S = (x >> 11) & 0x3F; // 6b
            S11 = x & 0x7FF; // 11b
        }

        private string GetHalfCharactor()
        {
            if (IsLng)
            {
                if (G == 1) return "W";
                else return "E";
            }
            else
            {
                if (G == 1) return "S";
                else return "N";
            }
        }

        public override string ToString()
        {
            return DMS;
        }
    }
}
