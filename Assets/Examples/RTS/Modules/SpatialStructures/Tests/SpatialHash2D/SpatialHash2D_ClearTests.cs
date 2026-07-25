using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures.Tests
{
    [TestFixture]
    public class SpatialHash2D_Clear_Tests
    {
        private SpatialHash2D<string> _hash;

        [SetUp]
        public void Setup()
        {
            _hash = new SpatialHash2D<string>(1f);
        }

        [Test]
        public void Clear_AfterInsert_RemovesAllData()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Insert("B", new Vector2(2, 2));

            // Act
            _hash.Clear();

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(Vector2.zero, 10f, buffer);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Clear_AllowsReinsert()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Clear();

            // Act
            _hash.Insert("B", new Vector2(2, 2));

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(2, 2), 0.1f, buffer);

            Assert.AreEqual(1, count);
            Assert.AreEqual("B", buffer[0]);
        }

        [Test]
        public void Clear_MultipleTimes_NoErrors()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));

            // Act
            _hash.Clear();
            _hash.Clear();
            _hash.Clear();

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(Vector2.zero, 10f, buffer);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Clear_RemovesLookupReferences()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Clear();

            // Act
            bool removed = _hash.Remove("A");

            // Assert
            Assert.IsFalse(removed, "После Clear объект не должен существовать в lookup");
        }

        [Test]
        public void Clear_AfterMoveAndRemove_LeavesCleanState()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Move("A", new Vector2(2, 2));
            _hash.Remove("A");

            // Act
            _hash.Clear();

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(Vector2.zero, 10f, buffer);

            Assert.AreEqual(0, count);
        }
    }
}