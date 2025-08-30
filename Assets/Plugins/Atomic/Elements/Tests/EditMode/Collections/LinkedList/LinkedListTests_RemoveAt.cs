using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void RemoveAt_ValidIndex_RemovesCorrectElement()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3, 4);
            list.RemoveAt(1); // Remove element 2

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(3, list[1]);
            Assert.AreEqual(4, list[2]);
        }

        [Test]
        public void RemoveAt_Head_RemovesFirstElement()
        {
            var list = new ReactiveLinkedList<int>(10, 20, 30);
            list.RemoveAt(0);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(20, list[0]);
            Assert.AreEqual(30, list[1]);
        }

        [Test]
        public void RemoveAt_Tail_RemovesLastElement()
        {
            var list = new ReactiveLinkedList<int>(5, 6, 7);
            list.RemoveAt(2);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(6, list[1]);
        }

        [Test]
        public void RemoveAt_IndexOutOfRange_ThrowsException()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(3));
        }

        [Test]
        public void RemoveAt_EmptyList_ThrowsException()
        {
            var list = new ReactiveLinkedList<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        }
    }
}