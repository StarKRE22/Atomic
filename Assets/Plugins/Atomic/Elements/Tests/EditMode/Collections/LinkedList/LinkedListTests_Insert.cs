using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void Insert_AtBeginning_ListIsUpdated()
        {
            var list = new ReactiveLinkedList<int>();
            list.Add(2);
            list.Add(3);

            list.Insert(0, 1);

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, new[] { list[0], list[1], list[2] });
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void Insert_AtMiddle_ListIsUpdated()
        {
            var list = new ReactiveLinkedList<int>();
            list.Add(1);
            list.Add(3);

            list.Insert(1, 2);

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, new[] { list[0], list[1], list[2] });
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void Insert_AtEnd_ListIsUpdated()
        {
            var list = new ReactiveLinkedList<int>();
            list.Add(1);
            list.Add(2);

            list.Insert(2, 3);

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, new[] { list[0], list[1], list[2] });
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void Insert_NullItem_DoesNotIncreaseCount()
        {
            var list = new ReactiveLinkedList<string>();
            list.Insert(0, null);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Insert_InvalidIndex_ThrowsArgumentOutOfRangeException()
        {
            var list = new ReactiveLinkedList<int>();
            list.Add(1);

            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(-1, 10));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(2, 10));
        }

        [Test]
        public void Insert_WhenCapacityExceeded_ResizesInternalArray()
        {
            var list = new ReactiveLinkedList<int>(1); // initial capacity = 1
            list.Add(1);

            list.Insert(1, 2); // should trigger resize

            CollectionAssert.AreEqual(new[] { 1, 2 }, new[] { list[0], list[1] });
            Assert.AreEqual(2, list.Count);
        }
    }
}