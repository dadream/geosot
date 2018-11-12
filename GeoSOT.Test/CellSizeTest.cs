using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeoSOT.UnitTests
{
    [TestClass]
    public class CellSizeTest
    {
        private readonly CellSize _cellSize;

        public CellSizeTest()
        {
            _cellSize = new CellSize();
        }

        [TestMethod]
        public void TestGetCellSize()
        {
            int level = 0;
            var result = _cellSize.GetCellSize(level);
            Assert.AreEqual(result, 512);
        }
    }
}
