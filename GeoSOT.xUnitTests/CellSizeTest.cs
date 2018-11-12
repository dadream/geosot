using System;
using Xunit;

namespace GeoSOT.xUnitTests
{
    public class CellSizeTest
    {
        [Theory]
        [InlineData(0, 512)]
        [InlineData(9, 1)]
        [InlineData(10, 32.0 / 60)]
        [InlineData(15, 1.0 / 60)]
        [InlineData(16, 32.0 / 3600)]
        [InlineData(21, 1.0 / 3600)]
        [InlineData(32, 1.0 / (3600 * 2048))]
        public void Get_CellSize_In_Valid_Tile_Level(int input, double expected)
        {
            //Arrange
            var _cellSize = new CellSize();

            //Act
            var result = _cellSize.GetCellSizeInDegree(input);

            //Assert
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(33)]
        public void Get_CellSize_In_InValid_Tile_Level_Will_Throw_ArgumentException(int input)
        {
            //Arrange
            var _cellSize = new CellSize();

            //Act
            Action actual = () => _cellSize.GetCellSizeInDegree(input);

            //Assert
            Assert.Throws<ArgumentException>(actual);
        }
    }
}
