using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public class SpatialGrid2D_Clear_Tests
    {
        private SpatialGrid2D<int> grid;

        [SetUp]
        public void Setup()
        {
            grid = new SpatialGrid2D<int>(10, 10, 1f);
        }

        [Test]
        public void Clear_ShouldRemoveAllObjects()
        {
            grid.Insert(1, new Vector2(1, 1));
            grid.Insert(2, new Vector2(2, 2));

            grid.Clear();

            for (int x = 0; x < 10; x++)
            for (int y = 0; y < 10; y++)
                Assert.AreEqual(0, grid.GetCellCount(x, y));
        }

        [Test]
        public void Clear_ShouldAllowReinsert()
        {
            grid.Insert(1, new Vector2(1, 1));
            grid.Clear();

            bool result = grid.Insert(1, new Vector2(1, 1));

            Assert.IsTrue(result);
            Assert.AreEqual(1, grid.GetCellCount(1, 1));
        }

        [Test]
        public void Clear_ShouldResetLookup()
        {
            grid.Insert(1, new Vector2(1, 1));
            grid.Clear();

            bool removed = grid.Remove(1);

            Assert.IsFalse(removed);
        }

        [Test]
        public void Clear_ShouldWorkOnEmptyGrid()
        {
            grid.Clear();

            for (int x = 0; x < 10; x++)
            for (int y = 0; y < 10; y++)
                Assert.AreEqual(0, grid.GetCellCount(x, y));
        }

        [Test]
        public void Clear_ShouldHandleManyObjects()
        {
            for (int i = 0; i < 1000; i++)
                grid.Insert(i, new Vector2(i % 10, i % 10));

            grid.Clear();

            for (int x = 0; x < 10; x++)
            for (int y = 0; y < 10; y++)
                Assert.AreEqual(0, grid.GetCellCount(x, y));
        }
    }
}