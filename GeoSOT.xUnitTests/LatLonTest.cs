using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GeoSOT.xUnitTests
{
    public class LatLonTest
    {
        [Theory]
        [InlineData(-12.345678, 12.345678, "12° 20' 44.4408\" S, 12° 20' 44.4408\" E")]
        public void GetLatDMS(double inputLat, double inputLng, string expected)
        {
            //Arrange
            var lngLat = new LngLat { Lat = inputLat, Lng = inputLng };

            //Act
            var actual = lngLat.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-12.345678, 12.345678, "-12-20-44, 12-20-44")]
        public void GetCode(double inputLat, double inputLng, string expected)
        {
            //Arrange
            var lngLat = new LngLat { Lat = inputLat, Lng = inputLng };

            //Act
            var actual = lngLat.Code;

            //Assert
            Assert.Equal(expected, actual);
        }


    }
}
