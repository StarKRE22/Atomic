using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures.Tests
{
    [TestFixture]
    public class SpatialHash2D_Query_Tests
    {
        private SpatialHash2D<string> _hash;

        [SetUp]
        public void Setup()
        {
            _hash = new SpatialHash2D<string>(1f);
        }

        [Test]
        public void Query_Empty_ReturnsZero()
        {
            var buffer = new string[10];

            int count = _hash.QueryRadius(Vector2.zero, 1f, buffer);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Query_SingleInside_ReturnsElement()
        {
            _hash.Insert("A", new Vector2(1, 1));

            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(1, 1), 0.1f, buffer);

            Assert.AreEqual(1, count);
            Assert.AreEqual("A", buffer[0]);
        }

        [Test]
        public void Query_SingleOutside_ReturnsZero()
        {
            _hash.Insert("A", new Vector2(5, 5));

            var buffer = new string[10];
            int count = _hash.QueryRadius(Vector2.zero, 1f, buffer);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Query_MultiplePoints_FindsOnlyInsideRadius()
        {
            _hash.Insert("A", new Vector2(0, 0));
            _hash.Insert("B", new Vector2(1, 1));
            _hash.Insert("C", new Vector2(3, 3));

            var buffer = new string[10];
            int count = _hash.QueryRadius(Vector2.zero, 2f, buffer);

            Assert.AreEqual(2, count);
            CollectionAssert.Contains(buffer, "A");
            CollectionAssert.Contains(buffer, "B");
        }

        [Test]
        public void Query_IncludesBoundary()
        {
            _hash.Insert("A", new Vector2(0, 0));
            _hash.Insert("B", new Vector2(3, 0)); // ровно на границе

            var buffer = new string[10];
            int count = _hash.QueryRadius(Vector2.zero, 3f, buffer);

            Assert.AreEqual(2, count);
            CollectionAssert.Contains(buffer, "B");
        }

        [Test]
        public void Query_MultipleCells_WorksAcrossGrid()
        {
            _hash.Insert("A", new Vector2(0, 0));
            _hash.Insert("B", new Vector2(5, 5));
            _hash.Insert("C", new Vector2(-5, -5));

            var buffer = new string[10];
            int count = _hash.QueryRadius(Vector2.zero, 10f, buffer);

            Assert.AreEqual(3, count);
        }

        [Test]
        public void Query_BufferLimit_ClampsResult()
        {
            _hash.Insert("A", new Vector2(0, 0));
            _hash.Insert("B", new Vector2(0, 0));
            _hash.Insert("C", new Vector2(0, 0));

            var buffer = new string[2];
            int count = _hash.QueryRadius(Vector2.zero, 1f, buffer);

            Assert.AreEqual(2, count);
        }

        [Test]
        public void Query_NegativeCoordinates_Works()
        {
            _hash.Insert("A", new Vector2(-2, -2));

            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(-2, -2), 0.5f, buffer);

            Assert.AreEqual(1, count);
            Assert.AreEqual("A", buffer[0]);
        }

        [Test]
        public void Query_ZeroRadius_ReturnsExactMatchesOnly()
        {
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Insert("B", new Vector2(1.01f, 1.01f));

            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(1, 1), 0f, buffer);

            Assert.AreEqual(1, count);
            Assert.AreEqual("A", buffer[0]);
        }

        [Test]
        public void Query_ManyCalls_NoStateCorruption()
        {
            _hash.Insert("A", new Vector2(0, 0));
            _hash.Insert("B", new Vector2(5, 5));

            var buffer = new string[10];

            for (int i = 0; i < 20; i++)
            {
                int count = _hash.QueryRadius(Vector2.zero, 10f, buffer);
                Assert.AreEqual(2, count);
            }
        }
    }
}