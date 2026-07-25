using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2DTests
    {
        [Test]
        public void Contains_EmptyTree_ReturnsFalse()
        {
            bool result = tree2D.Contains(new Vector2(5, 5));
            Assert.IsFalse(result);
        }

        [Test]
        public void Contains_SingleNode_ReturnsTrue()
        {
            Vector2 point = new Vector2(3, 3);
            tree2D.Add(point, new object());

            bool result = tree2D.Contains(point);

            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_SingleNode_DifferentPoint_ReturnsFalse()
        {
            tree2D.Add(new Vector2(3, 3), new object());

            bool result = tree2D.Contains(new Vector2(5, 5));

            Assert.IsFalse(result);
        }

        [Test]
        public void Contains_MultipleNodes_ExistingPoints_ReturnsTrue()
        {
            tree2D.Add(new Vector2(5, 5), new object());
            tree2D.Add(new Vector2(3, 3), new object());
            tree2D.Add(new Vector2(8, 8), new object());
            tree2D.Add(new Vector2(1, 2), new object());

            Assert.IsTrue(tree2D.Contains(new Vector2(5, 5)));
            Assert.IsTrue(tree2D.Contains(new Vector2(3, 3)));
            Assert.IsTrue(tree2D.Contains(new Vector2(8, 8)));
            Assert.IsTrue(tree2D.Contains(new Vector2(1, 2)));
        }

        [Test]
        public void Contains_MultipleNodes_NonExistingPoints_ReturnsFalse()
        {
            tree2D.Add(new Vector2(5, 5), new object());
            tree2D.Add(new Vector2(3, 3), new object());
            tree2D.Add(new Vector2(8, 8), new object());
            tree2D.Add(new Vector2(1, 2), new object());

            Assert.IsFalse(tree2D.Contains(new Vector2(7, 7)));
            Assert.IsFalse(tree2D.Contains(new Vector2(2, 2)));
            Assert.IsFalse(tree2D.Contains(new Vector2(6, 6)));
        }

        [Test]
        public void Contains_PointAtRoot_ReturnsTrue()
        {
            Vector2 rootPoint = new Vector2(5, 5);
            tree2D.Add(rootPoint, new object());

            bool result = tree2D.Contains(rootPoint);

            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_PointInDifferentDepths_ReturnsTrue()
        {
            tree2D.Add(new Vector2(5, 5), new object());
            tree2D.Add(new Vector2(3, 3), new object());
            tree2D.Add(new Vector2(8, 8), new object());
            tree2D.Add(new Vector2(2, 2), new object());
            tree2D.Add(new Vector2(4, 4), new object());
            tree2D.Add(new Vector2(7, 7), new object());
            tree2D.Add(new Vector2(9, 9), new object());

            Assert.IsTrue(tree2D.Contains(new Vector2(5, 5)));
            Assert.IsTrue(tree2D.Contains(new Vector2(3, 3)));
            Assert.IsTrue(tree2D.Contains(new Vector2(8, 8)));
            Assert.IsTrue(tree2D.Contains(new Vector2(2, 2)));
            Assert.IsTrue(tree2D.Contains(new Vector2(4, 4)));
            Assert.IsTrue(tree2D.Contains(new Vector2(7, 7)));
            Assert.IsTrue(tree2D.Contains(new Vector2(9, 9)));
        }
    }
}