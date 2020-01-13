using GeoSOT;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var input = "39° 54' 37.0098\" N, 116° 18' 54.8198\" E";

            //Arrange
            var _tile = new Tile(input, 9);

            //Act
            var actual = _tile.GetBbox().ToString();

            // 
            var batfile = @"E:\sot.json";
            // var tiles = GetGlobeDTiles();
            // ExportCsv(tiles, batfile);
            // ExportGeoJSON(tiles, batfile);

            var tileFile = @"E:\grid_data\sot_1dchina1.csv";
            var tiles = GetSpliteTiles(tileFile);
            ExportGeoJSON(tiles, batfile);
        }

        static void ExportCsv(IEnumerable<Tile> tiles, string csvFile)
        {
            int i = 0;
            using (var fs = new FileStream(csvFile, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Id,bbox,code,level,x,y");
                    foreach (var tile in tiles)
                    {
                        // 左下右上
                        var cmd = string.Format("{0},{1},{2},{3},{4},{5}",
                            i++, tile.GetBbox(), tile.ToString(), tile.Level, tile.X, tile.Y);
                        sw.WriteLine(cmd);
                        Console.WriteLine(cmd);
                    }
                }
            }
        }

        static string ToGeoJSON(int id, Tile tile)
        {
            // 左下右上
            var cmd = string.Format("{0},{1},{2},{3},{4},{5}",
                id, tile.GetBbox(), tile.ToString(), tile.Level, tile.X, tile.Y);
            var bbox = tile.GetBbox();
            var coords = string.Format("[{0}],[{1}],[{2}],[{3}],[{4}]",
                bbox.West + "," + bbox.South,
                bbox.East + "," + bbox.South,
                bbox.East + "," + bbox.North,
                bbox.West + "," + bbox.North,
                bbox.West + "," + bbox.South);
            var fmt = "{{\"type\":\"Feature\"," +
                "\"properties\":{{\"Id\":{1},\"code\":\"{2}\",\"level\":{3},\"x\":{4},\"y\":{5}}}," +
                "\"geometry\":{{\"type\":\"Polygon\",\"coordinates\":[[{0}]]}}}}";
            cmd = string.Format(fmt,
                coords, id, tile.ToString(), tile.Level, tile.X, tile.Y);
            return cmd;
        }


        static void ExportGeoJSON(IEnumerable<Tile> tiles, string jsonFile)
        {
            int i = 0;
            using (var fs = new FileStream(jsonFile, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write("{\"type\":\"FeatureCollection\",\"features\":[");
                    foreach (var tile in tiles)
                    {
                        var cmd = ToGeoJSON(i++, tile);
                        sw.Write(cmd);
                        sw.Write(",");
                        Console.WriteLine(cmd);
                    }
                    sw.Write("]}");
                }
            }
        }

        static IEnumerable<Tile> GetSpliteTiles(string tileFile)
        {
            foreach (var line in File.ReadAllLines(tileFile))
            {
                var arr = line.Split(',');
                var tile = new Tile(int.Parse(arr[0]), uint.Parse(arr[1]), uint.Parse(arr[2]));
                var lat = tile.Corner.Lat.D;
                var lon = tile.Corner.Lng.D;
                for (var row = 0; row < 60; row++)
                {
                    for (var col = 0; col < 60; col++)
                    {
                        var g0Corner = string.Format("{0}° {1}' 0\" N, {2}° {3}' 0\" E", lat, row, lon, col);
                        yield return new Tile(g0Corner, 15);
                        //var g1Corner = string.Format("{0}° 0' 0\" N, {1}° 0' 0\" W", lat, lon);
                        //yield return new Tile(g1Corner, 9);
                        //var g2Corner = string.Format("{0}° 0' 0\" S, {1}° 0' 0\" E", lat, lon);
                        //yield return new Tile(g2Corner, 9);
                        //var g3Corner = string.Format("{0}° 0' 0\" S, {1}° 0' 0\" W", lat, lon);
                        //yield return new Tile(g3Corner, 9);
                    }
                }
            }
        }

        static IEnumerable<Tile> GetGlobeDTiles()
        {
            for (var lat = 0; lat < 88; lat++)
            {
                for (var lon = 0; lon < 180; lon++)
                {
                    var g0Corner = string.Format("{0}° 0' 0\" N, {1}° 0' 0\" E", lat, lon);
                    yield return new Tile(g0Corner, 15);
                    //var g1Corner = string.Format("{0}° 0' 0\" N, {1}° 0' 0\" W", lat, lon);
                    //yield return new Tile(g1Corner, 9);
                    //var g2Corner = string.Format("{0}° 0' 0\" S, {1}° 0' 0\" E", lat, lon);
                    //yield return new Tile(g2Corner, 9);
                    //var g3Corner = string.Format("{0}° 0' 0\" S, {1}° 0' 0\" W", lat, lon);
                    //yield return new Tile(g3Corner, 9);
                }
            }
        }
    }
}
