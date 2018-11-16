using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeoSOT.xUnitTests
{
    public class TileUtilsTest
    {
        [Theory]
        [InlineData(-12.345678, "12° 20' 44.4408\" S")]
        [InlineData(12.345678, "12° 20' 44.4408\" N")]
        public void GetLatDMS(double input, string expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var actual = _tileUtils.GetLatDMS(input);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-12.345678, "12° 20' 44.4408\" W")]
        [InlineData(12.345678, "12° 20' 44.4408\" E")]
        public void GetLongDMS(double input, string expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var actual = _tileUtils.GetLngDMS(input);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("12° 20' 44.4408\" S", -12.345678)]
        [InlineData("12° 20' 44.4408\" N", 12.345678)]
        [InlineData("39° 54' 37\" N", 39.910278)]
        public void GetLat(string input, double expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var actual = _tileUtils.GetLat(input);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("12° 20' 44.4408\" W", -12.345678)]
        [InlineData("12° 20' 44.4408\" E", 12.345678)]
        public void GetLong(string input, double expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var actual = _tileUtils.GetLng(input);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("12° 20' 44.4408\" W")]
        public void GetLat_In_InValid_Tile_Level_Will_Throw_ArgumentException(string input)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            Action actual = () => _tileUtils.GetLat(input);

            //Assert
            Assert.Throws<ArgumentException>(actual);
        }

        [Theory]
        [InlineData("12° 20' 44.4408\" S")]
        public void GetLon_In_InValid_Tile_Level_Will_Throw_ArgumentException(string input)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            Action actual = () => _tileUtils.GetLng(input);

            //Assert
            Assert.Throws<ArgumentException>(actual);
        }

        [Theory]
        [InlineData(12.345678, 12.345678)] // 12° 20' 44"
        [InlineData(-12.345678, -12.345678)] // 12° 20' 44"
        public void EncodeDecodeLngLat(double input, double expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var code = _tileUtils.EncodeLngLat(input);
            var actual = new LngLatSegments(code).Degree;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(12.345678, 12)] // 12° 20' 44"
        [InlineData(-12.345678, -12)] // 12° 20' 44"
        public void DecodeLngLat(double inputLat, double inputLng)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var code = _tileUtils.EncodeLngLat(inputLat, inputLng);
            double expectedLat = 0;
            double expectedLng = 0;
            _tileUtils.DecodeLngLat(code, ref expectedLat, ref expectedLng);

            //Assert
            Assert.Equal(expectedLat, inputLat);
            Assert.Equal(expectedLng, inputLng);
        }

        [Theory]
        [InlineData(1, 1, 1, "G0")]
        [InlineData(1, -1, 1, "G1")]
        [InlineData(-1, 1, 1, "G2")]
        [InlineData(-1, -1, 1, "G3")]
        public void GetLngLatCode(double inputLat, double inputLng, int inputLevel, string expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var actual = _tileUtils.GetLngLatCode(inputLat, inputLng, inputLevel);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("39° 54' 37.01\" N, 116° 18' 54.82\" E", 9, "G001310322")]
        [InlineData("39° 54' 37.01\" N, 116° 18' 54.82\" E", 15, "G001310322-230230")]
        [InlineData("39° 54' 37.01\" N, 116° 18' 54.82\" E", 21, "G001310322-230230-310312")]
        public void GetLngLat(string input, int inputLevel, string expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var str = input.Split(",");
            var lat = _tileUtils.GetLat(str[0]);
            var lng = _tileUtils.GetLng(str[1]);
            var actual = _tileUtils.GetLngLatCode(lat, lng, inputLevel);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
