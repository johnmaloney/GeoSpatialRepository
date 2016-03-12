using System;
using System.Drawing;
using Domain.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests
{
    [TestClass]
    public class IndexCellTests
    {
        [TestMethod]
        public void test_CellXY_expect_proper_results()
        {
            var bounds = new RectangleF(-18168474, 10500483, (float)1E-06, (float)1E-06);
            var cell = new MockIndexCell();
            var cellXY = cell.CellIndexExpose(bounds, -20225308, -7553189, 20430528, 18187932);

            Assert.AreEqual(0, cellXY.X);
            Assert.AreEqual(0, cellXY.Y);
        }
    }

    public class MockIndexCell : AIndexCell<int>
    {
        public SingleCellXY CellIndexExpose(RectangleF bounds, double cellx1, double celly1, double cellx2, double celly2)
        {
            return CellIndex(bounds, cellx1, cellx2, celly1, celly2);
        }
    }
}
