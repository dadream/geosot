using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeoSOT.xUnitTests
{
    public class LngLatSegmentsTest
    {
        [Theory]
        [InlineData(-12.345678, 1, 12, 20, 44, 903, 44.4409)]
        [InlineData(12.345678, 0, 12, 20, 44, 903, 44.4409)]
        public void LngLatSegments(double input, uint g, uint d, uint m, uint s, uint s11, double seconds)
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
        [InlineData("39° 54' 37.0098\" N", 0, 39, 54, 37, 20, 37.0098)]
        [InlineData("116° 18' 54.8198\" E", 0, 116, 18, 54, 1679, 54.8198)]
        [InlineData("39° 54' 37.0098\" S", 1, 39, 54, 37, 20, 37.0098)]
        [InlineData("116° 18' 54.8198\" W", 1, 116, 18, 54, 1679, 54.8198)]
        public void LngLatSegments1(string input, uint g, uint d, uint m, uint s, uint s11, double seconds)
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
        [InlineData("39° 54' 37.0098\" N", "39° 54' 37.0098\" N")]
        [InlineData("116° 18' 54.8198\" E", "116° 18' 54.8198\" E")]
        [InlineData("39° 54' 37.0098\" S", "39° 54' 37.0098\" S")]
        [InlineData("116° 18' 54.8198\" W", "116° 18' 54.8198\" W")]
        public void DMS(string input, string expected)
        {
            //Arrange
            var _segmetns = new LngLatSegments(input);

            //Act
            var actual = _segmetns.DMS;

            //Assert
            Assert.Equal(expected, actual);
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
