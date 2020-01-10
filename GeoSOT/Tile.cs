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
        public Tile(int l, int x, int y)
        {
            var lngCode = x << (32 - l);
            var latCode = y << (32 - l);
            this.Level = l;
            this.Corner = new LngLat((uint)latCode, (uint)lngCode);
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
            var L = new LngLatSegments(this.Corner.Lng.Code >> (32 - Level) << (32 - Level), true);
            var B = new LngLatSegments(this.Corner.Lat.Code >> (32 - Level) << (32 - Level), false);
            var bbox = new LngLatBbox();
            if (L.G == 1)
            {
                bbox.East = L.Degree;
                bbox.West = bbox.East - cell;
            }
            else
            {
                bbox.West = L.Degree;
                bbox.East = bbox.West + cell;
            }
            if (B.G == 1)
            {
                bbox.North = B.Degree;
                bbox.South = bbox.North - cell;
            }
            else
            {
                bbox.South = B.Degree;
                bbox.North = bbox.South + cell;
            }
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
