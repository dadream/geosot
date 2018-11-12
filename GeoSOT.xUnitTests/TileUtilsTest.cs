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
        [InlineData("12° 20' 44.4408\" S", -12.345678)]
        [InlineData("12° 20' 44.4408\" N", 12.345678)]
        public void GetLat11(string input, double expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var actual = _tileUtils.GetLat(input);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
