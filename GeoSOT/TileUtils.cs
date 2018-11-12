using System;
using System.Collections.Generic;
using System.Text;

namespace GeoSOT
{
    public class TileUtils
    {
        /// <summary>
        /// 将十进制纬度转换为度分秒表达
        /// 其中，秒按四舍五入保留4位小数
        /// </summary>
        /// <param name="latitude">维度</param>
        /// <returns>*度*分*秒</returns>
        public string GetLatDMS(double latitude)
        {
            var degrees = (int)latitude;
            var minutes = (latitude - degrees) * 60;
            var seconds = ((minutes - (int)minutes) * 60);
            return string.Format("{0}° {1}' {2}\" {3}",
                Math.Abs(degrees), Math.Abs((int)minutes), 
                Math.Round(Math.Abs(seconds), 4),
                degrees < 0 ? "S" : "N");
        }

        /// <summary>
        /// 将十进制经度转换为度分秒表达
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <returns>*度*分*秒</returns>
        public string GetLongDMS(double longitude)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 将度分秒维度转换为十进制表达
        /// </summary>
        /// <param name="lat"></param>
        /// <returns></returns>
        public double GetLat(string latDMS)
        {
            Dim degrees As Double
   Dim minutes As Double
   Dim seconds As Double
   ' Set degree to value before "°" of Argument Passed.
   degrees = Val(Left(Degree_Deg, InStr(1, Degree_Deg, "°") - 1))
   ' Set minutes to the value between the "°" and the "'"
   ' of the text string for the variable Degree_Deg divided by
   ' 60. The Val function converts the text string to a number.
   minutes = Val(Mid(Degree_Deg, InStr(1, Degree_Deg, "°") + 2, _
             InStr(1, Degree_Deg, "'") - InStr(1, Degree_Deg, _
             "°") - 2)) / 60
    ' Set seconds to the number to the right of "'" that is
    ' converted to a value and then divided by 3600.
    seconds = Val(Mid(Degree_Deg, InStr(1, Degree_Deg, "'") + _
            2, Len(Degree_Deg) - InStr(1, Degree_Deg, "'") - 2)) _
            / 3600
   Convert_Decimal = degrees + minutes + seconds
            throw new NotImplementedException();
        }

        /// <summary>
        /// 将度分秒经度转换为十进制表达
        /// </summary>
        /// <param name="long">经度</param>
        /// <returns></returns>
        public double GetLong(string longDMS)
        {
            throw new NotImplementedException();
        }

        public Int32 EncodeLat(double latitude)
        {
            throw new NotImplementedException();
        }

        public Int32 EncodeLong(double Longitude)
        {
            throw new NotImplementedException();
        }

        public double DecodeLat(Int32 latKey)
        {
            throw new NotImplementedException();
        }

        public double DecodeLon(Int32 lonKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 北京世纪坛（39°54′37″N，116°18′54″E）
        /// 第 9 级其剖分编码为（ 39,116 ）
        /// 第 15 级为（ 39-54,116-18 ）
        /// 第 21 级为（39-54-37,116-18-54）
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public Int64 Get1DId(double lat, double lon)
        {
            throw new NotImplementedException();
        }

        public Int32 GetLatId(Int64 quadKey)
        {
            throw new NotImplementedException();
        }

        public Int32 GetLonId(Int64 quadKey)
        {
            throw new NotImplementedException();
        }

        public Int64 Get1DId(Int32 latKey, Int32 lonKey)
        {
            throw new NotImplementedException();
        }
    }
}
