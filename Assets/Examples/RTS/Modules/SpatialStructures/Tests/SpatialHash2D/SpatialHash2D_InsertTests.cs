using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures.Tests
{
    [TestFixture]
    public class SpatialHash2D_Insert_Tests
    {
        private SpatialHash2D<string> _hash;

        [SetUp]
        public void Setup()
        {
            _hash = new SpatialHash2D<string>(1f);
        }

        [Test]
        public void Insert_SingleElement_CanBeQueried()
        {
            // Arrange
            var pos = new Vector2(1, 1);

            // Act
            _hash.Insert("A", pos);

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(pos, 0.1f, buffer);

            Assert.AreEqual(1, count);
            Assert.AreEqual("A", buffer[0]);
        }

        [Test]
        public void Insert_MultipleElements_SameCell_AllStored()
        {
            // Arrange
            var pos = new Vector2(1.2f, 1.3f);

            // Act
            _hash.Insert("A", pos);
            _hash.Insert("B", pos);
            _hash.Insert("C", pos);

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(pos, 0.5f, buffer);

            Assert.AreEqual(3, count);
            CollectionAssert.Contains(buffer, "A");
            CollectionAssert.Contains(buffer, "B");
            CollectionAssert.Contains(buffer, "C");
        }

        [Test]
        public void Insert_DifferentCells_StoredSeparately()
        {
            // Arrange
            _hash.Insert("A", new Vector2(0, 0));
            _hash.Insert("B", new Vector2(10, 10));

            // Act
            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(0, 0), 1f, buffer);

            // Assert
            Assert.AreEqual(1, count);
            Assert.AreEqual("A", buffer[0]);
        }

        [Test]
        public void Insert_NegativeCoordinates_WorksCorrectly()
        {
            // Arrange
            var pos = new Vector2(-1.2f, -3.4f);

            // Act
            _hash.Insert("A", pos);

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(pos, 0.5f, buffer);

            Assert.AreEqual(1, count);
            Assert.AreEqual("A", buffer[0]);
        }

      
        [Test]
        public void Insert_SameObject_ReturnsFalse()
        {
            var pos1 = new Vector2(1, 1);
            var pos2 = new Vector2(2, 2);

            bool first = _hash.Insert("A", pos1);
            bool second = _hash.Insert("A", pos2);

            Assert.IsTrue(first);
            Assert.IsFalse(second);

            var buffer = new string[10];

            int count = _hash.QueryRadius(pos1, 0.1f, buffer);

            Assert.AreEqual(1, count);
            Assert.AreEqual("A", buffer[0]);
        }

        [Test]
        public void Insert_BufferOverflow_IsClamped()
        {
            // Arrange
            var pos = new Vector2(1, 1);

            _hash.Insert("A", pos);
            _hash.Insert("B", pos);
            _hash.Insert("C", pos);

            var buffer = new string[2];

            // Act
            int count = _hash.QueryRadius(pos, 1f, buffer);

            // Assert
            Assert.AreEqual(2, count);
        }
    }
}