using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeoSOT.xUnitTests
{
    public class TileTest
    {
        [Theory]
        [InlineData("39° 54' 37.0098\" N, 116° 18' 54.8198\" E", "116 39 117 40")]
        public void TileBboxLevel9(string input, string expected)
        {
            //Arrange
            var _tile = new Tile(input, 9);

            //Act
            var actual = _tile.GetBbox().ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("39° 54' 37.0098\" N, 116° 18' 54.8198\" E", "116.3 39.9 116.316667 39.916667")]
        public void TileBboxLevel15(string input, string expected)
        {
            //Arrange
            var _tile = new Tile(input, 15);

            //Act
            var actual = _tile.GetBbox().ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

    }
}
