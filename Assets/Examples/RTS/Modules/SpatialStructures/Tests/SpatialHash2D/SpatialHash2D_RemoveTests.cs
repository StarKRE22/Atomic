using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures.Tests
{
    [TestFixture]
    public class SpatialHash2D_Remove_Tests
    {
        private SpatialHash2D<string> _hash;

        [SetUp]
        public void Setup()
        {
            _hash = new SpatialHash2D<string>(1f);
        }

        [Test]
        public void Remove_ExistingObject_RemovesSuccessfully()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));

            // Act
            bool removed = _hash.Remove("A");

            // Assert
            Assert.IsTrue(removed);

            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(1, 1), 0.1f, buffer);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Remove_ObjectDoesNotExist_ReturnsFalse()
        {
            // Act
            bool removed = _hash.Remove("A");

            // Assert
            Assert.IsFalse(removed);
        }

        [Test]
        public void Remove_FromSingleElementCell_RemovesCell()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));

            // Act
            _hash.Remove("A");

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(1, 1), 1f, buffer);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Remove_MultipleElements_OnlyRemovesTarget()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Insert("B", new Vector2(1, 1));
            _hash.Insert("C", new Vector2(1, 1));

            // Act
            _hash.Remove("B");

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(1, 1), 1f, buffer);

            Assert.AreEqual(2, count);
            CollectionAssert.Contains(buffer, "A");
            CollectionAssert.Contains(buffer, "C");
        }

        [Test]
        public void Remove_SwapLogic_KeepsStructureValid()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Insert("B", new Vector2(1, 1));
            _hash.Insert("C", new Vector2(1, 1));

            // Act
            _hash.Remove("A"); // триггер swap

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(1, 1), 1f, buffer);

            Assert.AreEqual(2, count);
            CollectionAssert.DoesNotContain(buffer, "A");
        }

        [Test]
        public void Remove_AllElements_LeavesEmptyStructure()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Insert("B", new Vector2(2, 2));

            // Act
            _hash.Remove("A");
            _hash.Remove("B");

            // Assert
            var buffer = new string[10];

            int count1 = _hash.QueryRadius(new Vector2(1, 1), 1f, buffer);
            int count2 = _hash.QueryRadius(new Vector2(2, 2), 1f, buffer);

            Assert.AreEqual(0, count1);
            Assert.AreEqual(0, count2);
        }

        [Test]
        public void Remove_ThenInsertAgain_WorksCorrectly()
        {
            // Arrange
            _hash.Insert("A", new Vector2(1, 1));
            _hash.Remove("A");

            // Act
            _hash.Insert("A", new Vector2(2, 2));

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(2, 2), 0.1f, buffer);

            Assert.AreEqual(1, count);
            Assert.AreEqual("A", buffer[0]);
        }

        [Test]
        public void Remove_ManyOperations_NoGhostEntries()
        {
            // Arrange
            _hash.Insert("A", new Vector2(0, 0));

            // Act
            for (int i = 0; i < 10; i++)
            {
                _hash.Move("A", new Vector2(i, i));
                _hash.Remove("A");
                _hash.Insert("A", new Vector2(i, i));
            }

            // Assert
            var buffer = new string[10];
            int count = _hash.QueryRadius(new Vector2(9, 9), 0.1f, buffer);

            Assert.AreEqual(1, count);
        }
    }
}