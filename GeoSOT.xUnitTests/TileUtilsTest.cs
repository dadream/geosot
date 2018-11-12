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
        public void GetLatDMS(double input, string expected)
        {
            //Arrange
            var _tileUtils = new TileUtils();

            //Act
            var actual = _tileUtils.GetLatDMS(input);

            //Assert
            Assert.Equal(actual, expected);
        }
    }
}
