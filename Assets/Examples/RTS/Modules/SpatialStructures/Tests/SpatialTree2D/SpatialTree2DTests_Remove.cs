using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2DTests
    {
        [Test]
        public void RemoveRoot1()
        {
            var a = new object();
            var b = new object();
            var c = new object();

            tree2D.Add(new Vector2(4, 6), a);
            tree2D.Add(new Vector2(-1, -2), b);
            tree2D.Add(new Vector2(5, 3), c);

            bool removed = tree2D.Remove(new Vector2(4, 6), a);

            Assert.IsTrue(removed);
            Assert.AreEqual(2, tree2D.Count);
        }

        [Test]
        public void RemoveRoot2()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            var d = new object();
            var e = new object();
            var f = new object();

            tree2D.Add(new Vector2(4, 6), a);
            tree2D.Add(new Vector2(-1, -2), b);
            tree2D.Add(new Vector2(5, 3), c);
            tree2D.Add(new Vector2(-2, 3), d);
            tree2D.Add(new Vector2(7, 2), e);
            tree2D.Add(new Vector2(7, 3), f);

            bool removed = tree2D.Remove(new Vector2(4, 6), a);

            Assert.IsTrue(removed);
            Assert.AreEqual(5, tree2D.Count);
        }

        [Test]
        public void Remove_NodeExists_DecreasesCount()
        {
            Vector2 point = new Vector2(5, 5);
            var value = new object();

            tree2D.Add(point, value);

            bool removed = tree2D.Remove(point, value);

            Assert.IsTrue(removed);
            Assert.AreEqual(0, tree2D.Count);
        }

        [Test]
        public void Remove_NodeDoesNotExist_ReturnsFalse()
        {
            var value = new object();
            tree2D.Add(new Vector2(5, 5), value);

            bool removed = tree2D.Remove(new Vector2(10, 10), new object());

            Assert.IsFalse(removed);
            Assert.AreEqual(1, tree2D.Count);
        }

        [Test]
        public void Remove_LeafNode_DeletesItSuccessfully()
        {
            var a = new object();
            var b = new object();

            tree2D.Add(new Vector2(5, 5), a);
            tree2D.Add(new Vector2(3, 3), b);

            bool removed = tree2D.Remove(new Vector2(3, 3), b);

            Assert.IsTrue(removed);
            Assert.AreEqual(1, tree2D.Count);
        }

        [Test]
        public void Remove_NodeWithOneChild_ReplacesWithChild()
        {
            var a = new object();
            var b = new object();
            var c = new object();

            tree2D.Add(new Vector2(5, 5), a);
            tree2D.Add(new Vector2(3, 3), b);
            tree2D.Add(new Vector2(2, 2), c);

            bool removed = tree2D.Remove(new Vector2(3, 3), b);

            Assert.IsTrue(removed);
            Assert.AreEqual(2, tree2D.Count);
        }

        [Test]
        public void Remove_NodeWithTwoChildren_ReplacesWithMinimumFromRightSubtree()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            var d = new object();
            var e = new object();

            tree2D.Add(new Vector2(5, 5), a);
            tree2D.Add(new Vector2(3, 3), b);
            tree2D.Add(new Vector2(7, 7), c);
            tree2D.Add(new Vector2(6, 6), d);
            tree2D.Add(new Vector2(8, 8), e);

            bool removed = tree2D.Remove(new Vector2(7, 7), c);

            Assert.IsTrue(removed);
            Assert.AreEqual(4, tree2D.Count);
        }

        [Test]
        public void Remove_AllNodes_LeavesEmptyTree()
        {
            var a = new object();
            var b = new object();
            var c = new object();

            tree2D.Add(new Vector2(5, 5), a);
            tree2D.Add(new Vector2(3, 3), b);
            tree2D.Add(new Vector2(7, 7), c);

            tree2D.Remove(new Vector2(5, 5), a);
            tree2D.Remove(new Vector2(3, 3), b);
            tree2D.Remove(new Vector2(7, 7), c);

            Assert.AreEqual(0, tree2D.Count);
        }
    }
}