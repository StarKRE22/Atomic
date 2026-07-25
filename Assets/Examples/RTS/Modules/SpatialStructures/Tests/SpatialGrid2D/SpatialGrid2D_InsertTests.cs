using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public class SpatialGrid2D_Insert_Tests
    {
        private SpatialGrid2D<int> grid;

        [SetUp]
        public void Setup()
        {
            grid = new SpatialGrid2D<int>(10, 10, 1f);
        }

        [Test]
        public void Insert_ShouldReturnTrue_WhenValid()
        {
            bool result = grid.Insert(1, new Vector2(2.5f, 3.5f));
            Assert.IsTrue(result);
        }

        [Test]
        public void Insert_ShouldPlaceObjectInCorrectCell()
        {
            grid.Insert(42, new Vector2(2.1f, 3.9f));

            int count = grid.GetCellCount(2, 3);

            Assert.AreEqual(1, count);
            Assert.AreEqual(42, grid.GetCellValue(2, 3, 0));
        }

        [Test]
        public void Insert_ShouldReturnFalse_WhenDuplicate()
        {
            grid.Insert(1, new Vector2(1, 1));
            bool result = grid.Insert(1, new Vector2(2, 2));

            Assert.IsFalse(result);
        }

        [Test]
        public void Insert_ShouldNotModifyGrid_WhenDuplicate()
        {
            grid.Insert(1, new Vector2(1, 1));
            grid.Insert(1, new Vector2(2, 2));

            int count = grid.GetCellCount(1, 1);

            Assert.AreEqual(1, count);
            Assert.AreEqual(1, grid.GetCellValue(1, 1, 0));
        }

        [Test]
        public void Insert_ShouldReturnFalse_WhenOutOfBounds()
        {
            bool result = grid.Insert(1, new Vector2(-1, 0));
            Assert.IsFalse(result);
        }

        [Test]
        public void Insert_ShouldNotAddAnything_WhenOutOfBounds()
        {
            grid.Insert(1, new Vector2(-1, 0));

            for (int x = 0; x < 10; x++)
            for (int y = 0; y < 10; y++)
                Assert.AreEqual(0, grid.GetCellCount(x, y));
        }

        [Test]
        public void Insert_ShouldHandleCellBoundaryCorrectly()
        {
            grid.Insert(1, new Vector2(1.999f, 1.999f));

            int count = grid.GetCellCount(1, 1);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void Insert_ShouldGoToNextCell_WhenCrossingBoundary()
        {
            grid.Insert(1, new Vector2(2.0f, 2.0f));

            int count = grid.GetCellCount(2, 2);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void Insert_ShouldHandleManyObjects()
        {
            for (int i = 0; i < 1000; i++)
            {
                bool result = grid.Insert(i, new Vector2(i % 10, i % 10));
                Assert.IsTrue(result);
            }
        }
    }
}