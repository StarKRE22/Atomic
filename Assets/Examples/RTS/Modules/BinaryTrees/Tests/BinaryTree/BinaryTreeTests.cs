using System;
using NUnit.Framework;

namespace Modules.BinaryTrees
{
    [TestFixture]
    public class BinaryTreeTests
    {
        private BinaryTree<int> _tree;

        [SetUp]
        public void Setup()
        {
            _tree = new BinaryTree<int>();
        }

        [Test]
        public void Add_Contains_ReturnsTrue()
        {
            _tree.Add(50);
            _tree.Add(30);
            _tree.Add(70);

            Assert.IsTrue(_tree.Contains(50));
            Assert.IsTrue(_tree.Contains(30));
            Assert.IsTrue(_tree.Contains(70));
            Assert.AreEqual(3, _tree.Count);
        }

        [Test]
        public void Add_DuplicateValue_Ignored()
        {
            bool first = _tree.Add(50);
            bool second = _tree.Add(50);

            Assert.IsTrue(first);
            Assert.IsFalse(second);
            Assert.AreEqual(1, _tree.Count);
        }

        [Test]
        public void Contains_EmptyTree_ReturnsFalse()
        {
            Assert.IsFalse(_tree.Contains(10));
        }

        [Test]
        public void Remove_NodeWithoutChildren_RemovesSuccessfully()
        {
            _tree.Add(50);
            _tree.Add(30);
            _tree.Add(70);

            bool removed = _tree.Remove(30);

            Assert.IsTrue(removed);
            Assert.IsFalse(_tree.Contains(30));
            Assert.AreEqual(2, _tree.Count);
        }

        [Test]
        public void Remove_NodeWithOneChild_ReplacesWithChild()
        {
            _tree.Add(50);
            _tree.Add(30);
            _tree.Add(20);

            bool removed = _tree.Remove(30);

            Assert.IsTrue(removed);
            Assert.IsFalse(_tree.Contains(30));
            Assert.IsTrue(_tree.Contains(20));
            Assert.AreEqual(2, _tree.Count);
        }

        [Test]
        public void Remove_NodeWithTwoChildren_ReplacesWithMinRightSubtree()
        {
            _tree.Add(50);
            _tree.Add(30);
            _tree.Add(70);
            _tree.Add(60);
            _tree.Add(80);

            bool removed = _tree.Remove(70);

            Assert.IsTrue(removed);
            Assert.IsFalse(_tree.Contains(70));
            Assert.IsTrue(_tree.Contains(60));
            Assert.IsTrue(_tree.Contains(80));
            Assert.AreEqual(4, _tree.Count);
        }

        [Test]
        public void Remove_RootNodeWithTwoChildren_ReplacesCorrectly()
        {
            _tree.Add(50);
            _tree.Add(30);
            _tree.Add(70);
            _tree.Add(60);
            _tree.Add(80);

            bool removed = _tree.Remove(50);

            Assert.IsTrue(removed);
            Assert.IsFalse(_tree.Contains(50));
            Assert.IsTrue(_tree.Contains(30));
            Assert.IsTrue(_tree.Contains(70));
            Assert.IsTrue(_tree.Contains(60));
            Assert.IsTrue(_tree.Contains(80));
            Assert.AreEqual(4, _tree.Count);
        }

        [Test]
        public void Remove_NodeNotInTree_ReturnsFalse()
        {
            _tree.Add(50);
            _tree.Add(30);
            _tree.Add(70);

            bool removed = _tree.Remove(100);

            Assert.IsFalse(removed);
            Assert.AreEqual(3, _tree.Count);
        }

        [Test]
        public void Remove_LastNode_MakesTreeEmpty()
        {
            _tree.Add(50);

            bool removed = _tree.Remove(50);

            Assert.IsTrue(removed);
            Assert.IsFalse(_tree.Contains(50));
            Assert.AreEqual(0, _tree.Count);
        }

        [Test]
        public void Clear_RemovesAllNodes()
        {
            _tree.Add(10);
            _tree.Add(20);
            _tree.Add(5);

            _tree.Clear();

            Assert.AreEqual(0, _tree.Count);
            Assert.IsFalse(_tree.Contains(10));
            Assert.IsFalse(_tree.Contains(20));
            Assert.IsFalse(_tree.Contains(5));
        }
    }
}