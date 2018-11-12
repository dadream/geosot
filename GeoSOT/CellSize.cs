using System;

namespace GeoSOT
{
    public class CellSize
    {
        /// <summary>
        /// 获取level级别的分块大小,单位为经纬度
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public double GetCellSize(int level)
        {
            double cellSize = 0;
            if (level >= 0 && level <= 9)
            {
                cellSize = Math.Pow(2, 9 - level);
            }
            else
            {
                throw new NotImplementedException();
            }
            return cellSize;
        }
    }
}
