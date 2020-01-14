using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    public class Tile
    {
        /// <summary>
        /// 分块近原点的角点
        /// </summary>
        public LngLat Corner { get; private set; }
        /// <summary>
        /// 一维编码
        /// </summary>
        public ulong Id
        {
            get
            {
                var morton = new Morton2D();
                var L = this.Corner.Lng.Code;
                var B = this.Corner.Lat.Code;
                return morton.Magicbits(L, B);
            }
        }

        /// <summary>
        /// 层号
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 列号
        /// </summary>
        public uint X
        {
            get
            {
                return this.Corner.Lng.Code >> (32 - Level);
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        public uint Y
        {
            get
            {
                return this.Corner.Lat.Code >> (32 - Level);
            }
        }


        /// <summary>
        /// 角点经度
        /// </summary>
        public double CornerLng
        {
            get
            {
                var LCode = this.X << (32 - Level);
                var L = new LngLatSegments(LCode, true);
                return L.Degree;
            }
        }

        /// <summary>
        /// 角点纬度
        /// </summary>
        public double CornerLat
        {
            get
            {
                var BCode = this.Y << (32 - Level);
                var B = new LngLatSegments(this.Corner.Lat.Code >> (32 - Level) << (32 - Level), false);
                return B.Degree;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dms">经纬度</param>
        /// <param name="l">层级</param>
        public Tile(string dms, int l)
        {
            this.Level = l;
            this.Corner = new LngLat(dms);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="l">层号</param>
        /// <param name="x">列号</param>
        /// <param name="y">行号</param>
        public Tile(int l, uint x, uint y)
        {
            var lngCode = x << (32 - l);
            var latCode = y << (32 - l);
            this.Level = l;
            this.Corner = new LngLat(latCode, lngCode);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">SOT一维编码</param>
        public Tile(string code)
        {
            ulong id = 0L;
            var level = 0;
            foreach (var c in code)
            {
                if (char.IsDigit(c))
                {
                    var v = DecodeChar(c);
                    var shift = ((31 - level) * 2);
                    id = id | ((ulong)v << shift);
                    level++;
                }
            }

            var morton = new Morton2D();
            uint l = 0;
            uint b = 0;
            morton.Magicbits(id, ref l, ref b);
            this.Level = level;
            this.Corner = new LngLat(b, l);
        }

        private static byte DecodeChar(char c)
        {
            if (c == '1') return 1;
            if (c == '2') return 2;
            if (c == '3') return 3;
            return 0;
        }

        /// <summary>
        /// 获取Tile的范围四至
        /// </summary>
        /// <returns></returns>
        public LngLatBbox GetBbox()
        {
            var cellSize = new CellSize();
            var cell = cellSize.GetCellSizeInDegree(Level);

            var trtile = new Tile(this.Level, this.X + 1, this.Y + 1);
            var bbox = new LngLatBbox
            {
                West = Math.Min(this.CornerLng, trtile.CornerLng),
                South = Math.Min(this.CornerLat, trtile.CornerLat),
                East = Math.Max(this.CornerLng, trtile.CornerLng),
                North = Math.Max(this.CornerLat, trtile.CornerLat),
            };

            return bbox;
        }

        /// <summary>
        /// 一维编码字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var code = this.Id;
            StringBuilder sb = new StringBuilder();
            sb.Append("G");
            for (int i = 31; i > 31 - this.Level; i--)
            {
                var v = (code >> i * 2) & 0x3;
                sb.Append(v);
                if (i > 32 - this.Level)
                {
                    if (i == 23 || i == 17)
                    {
                        sb.Append("-");
                    }
                    if (i == 11)
                    {
                        sb.Append(".");
                    }
                }

            }
            return sb.ToString();
        }

    }
}
