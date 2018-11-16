using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeoSOT.xUnitTests
{
    public class TileTest
    {
        [Theory]
        [InlineData("39° 54' 37.0098\" N, 116° 18' 54.8198\" E", "")]
        [InlineData("39° 54' 37.0098\" S, 116° 18' 54.8198\" E", "")]
        [InlineData("39° 54' 37.0098\" N, 116° 18' 54.8198\" W", "")]
        [InlineData("39° 54' 37.0098\" S, 116° 18' 54.8198\" W", "")]
        public void TileBbox(string input, string expected)
        {
            //Arrange
            var _tile = new Tile(input, 9);

            //Act
            var actual = _tile.GetBbox().ToString();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
