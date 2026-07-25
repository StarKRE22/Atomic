using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2DTests
    {
        [Test]
        public void FindMax_EmptyTree_ReturnsNull()
        {
            var maxNodeX = tree2D.FindMax(null, SpatialTree2D<object>.Axis.X, 0);
            var maxNodeY = tree2D.FindMax(null, SpatialTree2D<object>.Axis.Y, 0);

            Assert.IsNull(maxNodeX);
            Assert.IsNull(maxNodeY);
        }

        [Test]
        public void FindMax_SingleNode_ReturnsSameNode()
        {
            Vector2 point = new Vector2(5, 5);
            tree2D.Add(point, new object());

            var maxNodeX = tree2D.FindMax(tree2D._root, SpatialTree2D<object>.Axis.X, 0);
            var maxNodeY = tree2D.FindMax(tree2D._root, SpatialTree2D<object>.Axis.Y, 0);

            Assert.IsNotNull(maxNodeX);
            Assert.AreEqual(point, maxNodeX.point);

            Assert.IsNotNull(maxNodeY);
            Assert.AreEqual(point, maxNodeY.point);
        }

        [Test]
        public void FindMax_MultipleNodes_FindsMaximumOnXAxis()
        {
            tree2D.Add(new Vector2(7, 2), new object());
            tree2D.Add(new Vector2(3, 6), new object());
            tree2D.Add(new Vector2(10, 8), new object());
            tree2D.Add(new Vector2(5, 4), new object());
            tree2D.Add(new Vector2(9, 1), new object());

            var maxNodeX = tree2D.FindMax(tree2D._root, SpatialTree2D<object>.Axis.X, 0);

            Assert.IsNotNull(maxNodeX);
            Assert.AreEqual(new Vector2(10, 8), maxNodeX.point);
        }

        [Test]
        public void FindMax_MultipleNodes_FindsMaximumOnYAxis()
        {
            tree2D.Add(new Vector2(7, 2), new object());
            tree2D.Add(new Vector2(3, 6), new object());
            tree2D.Add(new Vector2(10, 8), new object());
            tree2D.Add(new Vector2(5, 4), new object());
            tree2D.Add(new Vector2(9, 9), new object());

            var maxNodeY = tree2D.FindMax(tree2D._root, SpatialTree2D<object>.Axis.Y, 0);

            Assert.IsNotNull(maxNodeY);
            Assert.AreEqual(new Vector2(9, 9), maxNodeY.point);
        }

        [Test]
        public void FindMax_UnbalancedTree_FindsCorrectMaximum()
        {
            tree2D.Add(new Vector2(5, 5), new object());
            tree2D.Add(new Vector2(3, 4), new object());
            tree2D.Add(new Vector2(2, 3), new object());
            tree2D.Add(new Vector2(8, 7), new object());
            tree2D.Add(new Vector2(6, 9), new object());

            var maxNodeX = tree2D.FindMax(tree2D._root, SpatialTree2D<object>.Axis.X, 0);
            var maxNodeY = tree2D.FindMax(tree2D._root, SpatialTree2D<object>.Axis.Y, 0);

            Assert.AreEqual(new Vector2(8, 7), maxNodeX.point);
            Assert.AreEqual(new Vector2(6, 9), maxNodeY.point);
        }
    }
}