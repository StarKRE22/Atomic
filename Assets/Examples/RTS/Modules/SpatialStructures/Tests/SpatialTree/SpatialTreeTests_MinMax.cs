using NUnit.Framework;
using UnityEngine;
using static Modules.SpatialStructures.SpatialTree<int>;

namespace Modules.SpatialStructures
{
    [TestFixture]
    public class SpatialTreeTests_MinMax
    {
        private SpatialTree<int> tree;

        [SetUp]
        public void Setup()
        {
            tree = new SpatialTree<int>();

            tree.Add(new Vector3(3, 6, 7), 1);
            tree.Add(new Vector3(17, 15, 20), 2);
            tree.Add(new Vector3(10, 11, 12), 3);
            tree.Add(new Vector3(5, 8, 9), 4);
            tree.Add(new Vector3(12, 10, 14), 5);
        }

        [Test]
        public void Test_FindMin_X_Axis()
        {
            var minNode = tree.FindMin(tree._root, Axis.X, 0);

            Assert.AreEqual(new Vector3(3, 6, 7), minNode.point);
            Assert.Contains(1, minNode.values);
        }

        [Test]
        public void Test_FindMax_X_Axis()
        {
            var maxNode = tree.FindMax(tree._root, Axis.X, 0);

            Assert.AreEqual(new Vector3(17, 15, 20), maxNode.point);
            Assert.Contains(2, maxNode.values);
        }

        [Test]
        public void Test_FindMin_Y_Axis()
        {
            var minNode = tree.FindMin(tree._root, Axis.Y, 0);

            Assert.AreEqual(new Vector3(3, 6, 7), minNode.point);
            Assert.Contains(1, minNode.values);
        }

        [Test]
        public void Test_FindMax_Y_Axis()
        {
            var maxNode = tree.FindMax(tree._root, Axis.Y, 0);

            Assert.AreEqual(new Vector3(17, 15, 20), maxNode.point);
            Assert.Contains(2, maxNode.values);
        }

        [Test]
        public void Test_FindMin_Z_Axis()
        {
            var minNode = tree.FindMin(tree._root, Axis.Z, 0);

            Assert.AreEqual(new Vector3(3, 6, 7), minNode.point);
            Assert.Contains(1, minNode.values);
        }

        [Test]
        public void Test_FindMax_Z_Axis()
        {
            var maxNode = tree.FindMax(tree._root, Axis.Z, 0);

            Assert.AreEqual(new Vector3(17, 15, 20), maxNode.point);
            Assert.Contains(2, maxNode.values);
        }

        [Test]
        public void Test_FindMin_EmptyTree()
        {
            var emptyTree = new SpatialTree<int>();
            var minNode = emptyTree.FindMin(emptyTree._root, Axis.X, 0);

            Assert.IsNull(minNode);
        }

        [Test]
        public void Test_FindMax_EmptyTree()
        {
            var emptyTree = new SpatialTree<int>();
            var maxNode = emptyTree.FindMax(emptyTree._root, Axis.X, 0);

            Assert.IsNull(maxNode);
        }
    }
}