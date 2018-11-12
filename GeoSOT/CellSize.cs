using System;

namespace GeoSOT
{
    public class CellSize
    {
        /// <summary>
        /// 获取第i级[0,32]的分块大小,单位为度
        /// </summary>
        /// <param name="i">层级</param>
        /// <returns></returns>
        public double GetCellSizeInDegree(int i)
        {
            if (i >= 0 && i <= 9)
            {
                return Math.Pow(2, 9 - i);
            }
            if (i >= 10 && i <= 15)
            {
                return Math.Pow(2, 15 - i) / 60;
            }
            if (i >= 16 && i <= 32)
            {
                return Math.Pow(2, 21 - i) / 3600;
            }

            // if (i < 0 || i > 32)
            throw new ArgumentException();
        }
    }
}
