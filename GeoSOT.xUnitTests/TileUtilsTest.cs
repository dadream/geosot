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
            var actual = _tileUtils.DecodeLngLat(code).Degree;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(12.345678, 12.345678)] // 12° 20' 44"
        [InlineData(-12.345678, -12.345678)] // 12° 20' 44"
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
    }
}
