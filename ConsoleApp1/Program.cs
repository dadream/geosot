using GeoSOT;
using System;

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
        }
    }
}
