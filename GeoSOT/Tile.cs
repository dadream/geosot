using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    public class Tile
    {
        public LngLat Corner { get; private set; }

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
        /// 层级
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
        /// 角点维度
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
        /// <param name="l"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
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
        /// <param name="code"></param>
        public Tile(string code)
        {
            this.Level = code.Length - 1;
            throw new NotImplementedException();
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
        /// 四进制编码
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
