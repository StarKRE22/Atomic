using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public class SpatialGrid2D_Remove_Tests
    {
        private SpatialGrid2D<int> grid;

        [SetUp]
        public void Setup()
        {
            grid = new SpatialGrid2D<int>(10, 10, 1f);
        }

        [Test]
        public void Remove_ShouldReturnTrue_WhenExists()
        {
            grid.Insert(1, new Vector2(1, 1));

            bool result = grid.Remove(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void Remove_ShouldReturnFalse_WhenNotExists()
        {
            bool result = grid.Remove(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void Remove_ShouldRemoveObjectFromCell()
        {
            grid.Insert(1, new Vector2(2, 3));

            grid.Remove(1);

            int count = grid.GetCellCount(2, 3);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Remove_ShouldNotAffectOtherObjects()
        {
            grid.Insert(1, new Vector2(2, 3));
            grid.Insert(2, new Vector2(2, 3));

            grid.Remove(1);

            int count = grid.GetCellCount(2, 3);

            Assert.AreEqual(1, count);
            Assert.AreEqual(2, grid.GetCellValue(2, 3, 0));
        }

        [Test]
        public void Remove_ShouldHandleSwapBackCorrectly()
        {
            grid.Insert(1, new Vector2(2, 3));
            grid.Insert(2, new Vector2(2, 3));

            grid.Remove(1);

            int count = grid.GetCellCount(2, 3);

            Assert.AreEqual(1, count);
            Assert.AreEqual(2, grid.GetCellValue(2, 3, 0));

            bool removedSecond = grid.Remove(2);

            Assert.IsTrue(removedSecond);
            Assert.AreEqual(0, grid.GetCellCount(2, 3));
        }

        [Test]
        public void Remove_ShouldAllowReinsertAfterRemoval()
        {
            grid.Insert(1, new Vector2(1, 1));
            grid.Remove(1);

            bool result = grid.Insert(1, new Vector2(1, 1));

            Assert.IsTrue(result);
            Assert.AreEqual(1, grid.GetCellCount(1, 1));
        }

        [Test]
        public void Remove_ShouldHandleManyObjects()
        {
            for (int i = 0; i < 1000; i++)
                grid.Insert(i, new Vector2(i % 10, i % 10));

            for (int i = 0; i < 1000; i++)
            {
                bool result = grid.Remove(i);
                Assert.IsTrue(result);
            }

            for (int x = 0; x < 10; x++)
            for (int y = 0; y < 10; y++)
                Assert.AreEqual(0, grid.GetCellCount(x, y));
        }
    }
}