using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    [TestFixture]
    public sealed partial class SpatialTree2DTests
    {
        [Test]
        public void Add_SinglePoint_IncreasesCount()
        {
            // Arrange
            Vector2 point = new Vector2(1, 2);

            // Act
            bool success = tree2D.Add(point, new object());

            // Assert
            Assert.IsTrue(success);
            Assert.AreEqual(1, tree2D.Count);
        }

        [Test]
        public void Add_MultiplePoints_IncreasesCountCorrectly()
        {
            // Arrange
            Vector2[] points =
            {
                new(1, 2),
                new(3, 4),
                new(5, 6)
            };

            // Act
            foreach (Vector2 point in points)
                tree2D.Add(point, new object());

            // Assert
            Assert.AreEqual(3, tree2D.Count);
        }

        [Test]
        public void Add_DuplicatePoints_AddsMultipleValues()
        {
            // Arrange
            Vector2 point = new Vector2(1, 2);

            // Act
            tree2D.Add(point, "A");
            tree2D.Add(point, "B");

            // Assert
            Assert.AreEqual(2, tree2D.Count);

            bool found = tree2D.TryGetValues(point, out var values);
            Assert.IsTrue(found);
            Assert.AreEqual(2, values.Count);
        }

        [Test]
        public void Add_PointsInDifferentQuadrants_ShouldMaintainCorrectCount()
        {
            // Arrange
            Vector2[] points =
            {
                new(-1, -2),
                new(3, 4),
                new(5, -6),
                new(-7, 8)
            };

            // Act
            foreach (Vector2 point in points)
                tree2D.Add(point, new object());

            // Assert
            Assert.AreEqual(4, tree2D.Count);
        }

        [Test]
        public void Add_CheckTreeStructureWithTwoPoints()
        {
            // Arrange
            Vector2 root = new Vector2(5, 5);
            Vector2 leftChild = new Vector2(2, 2);

            // Act
            tree2D.Add(root, "root");
            tree2D.Add(leftChild, "child");

            // Assert
            Assert.AreEqual(2, tree2D.Count);

            Assert.IsTrue(tree2D.TryGetValues(root, out var rootValues));
            Assert.IsTrue(tree2D.TryGetValues(leftChild, out var childValues));

            Assert.AreEqual(1, rootValues.Count);
            Assert.AreEqual(1, childValues.Count);
        }
    }
}