using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    [TestFixture]
    public sealed partial class SpatialTree2DTests
    {
        [Test]
        public void FindMin_EmptyTree_ReturnsNull()
        {
            var minNodeX = tree2D.FindMin(null, SpatialTree2D<object>.Axis.X, 0);
            var minNodeY = tree2D.FindMin(null, SpatialTree2D<object>.Axis.Y, 0);

            Assert.IsNull(minNodeX);
            Assert.IsNull(minNodeY);
        }

        [Test]
        public void FindMin_SingleNode_ReturnsSameNode()
        {
            Vector2 point = new Vector2(5, 5);
            tree2D.Add(point, new object());

            var minNodeX = tree2D.FindMin(tree2D._root, SpatialTree2D<object>.Axis.X, 0);
            var minNodeY = tree2D.FindMin(tree2D._root, SpatialTree2D<object>.Axis.Y, 0);

            Assert.IsNotNull(minNodeX);
            Assert.AreEqual(point, minNodeX.point);

            Assert.IsNotNull(minNodeY);
            Assert.AreEqual(point, minNodeY.point);
        }

        [Test]
        public void FindMin_MultipleNodes_FindsMinimumOnXAxis()
        {
            tree2D.Add(new Vector2(7, 2), new object());
            tree2D.Add(new Vector2(3, 6), new object());
            tree2D.Add(new Vector2(2, 8), new object());
            tree2D.Add(new Vector2(5, 4), new object());
            tree2D.Add(new Vector2(9, 1), new object());

            var minNodeX = tree2D.FindMin(tree2D._root, SpatialTree2D<object>.Axis.X, 0);

            Assert.IsNotNull(minNodeX);
            Assert.AreEqual(new Vector2(2, 8), minNodeX.point);
        }

        [Test]
        public void FindMin_MultipleNodes_FindsMinimumOnYAxis()
        {
            tree2D.Add(new Vector2(7, 2), new object());
            tree2D.Add(new Vector2(3, 6), new object());
            tree2D.Add(new Vector2(2, 8), new object());
            tree2D.Add(new Vector2(5, 4), new object());
            tree2D.Add(new Vector2(9, 1), new object());

            var minNodeY = tree2D.FindMin(tree2D._root, SpatialTree2D<object>.Axis.Y, 0);

            Assert.IsNotNull(minNodeY);
            Assert.AreEqual(new Vector2(9, 1), minNodeY.point);
        }

        [Test]
        public void FindMin_UnbalancedTree_FindsCorrectMinimum()
        {
            tree2D.Add(new Vector2(5, 5), new object());
            tree2D.Add(new Vector2(3, 4), new object());
            tree2D.Add(new Vector2(2, 3), new object());
            tree2D.Add(new Vector2(1, 2), new object());
            tree2D.Add(new Vector2(6, 1), new object());

            var minNodeX = tree2D.FindMin(tree2D._root, SpatialTree2D<object>.Axis.X, 0);
            var minNodeY = tree2D.FindMin(tree2D._root, SpatialTree2D<object>.Axis.Y, 0);

            Assert.AreEqual(new Vector2(1, 2), minNodeX.point);
            Assert.AreEqual(new Vector2(6, 1), minNodeY.point);
        }
    }
}