using System;
using Xunit;

namespace GeoSOT.xUnitTests
{
    public class CellSizeTest
    {
        [Theory]
        [InlineData(0, 512)]
        public void Test1(int input, double expected)
        {
            //Arrange
            var _cellSize = new CellSize();

            //Act
            var result = _cellSize.GetCellSize(input);

            //Assert
            Assert.Equal(result, expected);
        }
    }
}
