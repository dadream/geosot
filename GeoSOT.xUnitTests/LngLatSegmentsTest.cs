using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeoSOT.xUnitTests
{
    public class LngLatSegmentsTest
    {
        [Theory]
        [InlineData(-12.345678, 1, 12, 20, 44, 902, 44.4408)]
        [InlineData(12.345678, 0, 12, 20, 44, 902, 44.4408)]
        public void LngLatSegments(double input, UInt32 g, UInt32 d, UInt32 m, UInt32 s, UInt32 s11, double seconds)
        {
            //Arrange
            var _segmetns = new LngLatSegments(input);

            //Assert
            Assert.Equal(g, _segmetns.G);
            Assert.Equal(d, _segmetns.D);
            Assert.Equal(m, _segmetns.M);
            Assert.Equal(s, _segmetns.S);
            Assert.Equal(s11, _segmetns.S11);
            Assert.Equal(seconds, _segmetns.Seconds);
        }

        [Theory]
        [InlineData(-12.345678, -12.345678)]
        [InlineData(12.345678, 12.345678)]
        public void Degree(double input, double expected)
        {
            //Arrange
            var _segmetns = new LngLatSegments(input);

            //Act
            var actual = _segmetns.Degree;

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
