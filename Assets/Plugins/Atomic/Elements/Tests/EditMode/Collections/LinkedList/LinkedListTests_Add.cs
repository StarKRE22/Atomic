using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void Add_SingleItem_ListContainsItem()
        {
            var list = new ReactiveLinkedList<int>();
            list.Add(5);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(5, list[0]);
        }

        [Test]
        public void Add_MultipleItems_ListContainsAllItemsInOrder()
        {
            var list = new ReactiveLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Assert.AreEqual(3, list.Count);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, new[] { list[0], list[1], list[2] });
        }

        [Test]
        public void Add_NullItem_DoesNotIncreaseCount()
        {
            var list = new ReactiveLinkedList<string>();
            list.Add(null);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Add_WhenCapacityExceeded_ResizesInternalArray()
        {
            var list = new ReactiveLinkedList<int>(1); // initial capacity = 1
            list.Add(1);
            list.Add(2); // should trigger resize

            Assert.AreEqual(2, list.Count);
            CollectionAssert.AreEqual(new[] { 1, 2 }, new[] { list[0], list[1] });
        }

        [Test]
        public void Add_FirstItem_UpdatesHeadAndTail()
        {
            var list = new ReactiveLinkedList<int>();
            list.Add(10);

            Assert.AreEqual(10, list[0]);
        }

        [Test]
        public void Add_SubsequentItems_UpdatesTailCorrectly()
        {
            var list = new ReactiveLinkedList<int>();
            list.Add(1);
            list.Add(2);

            Assert.AreEqual(2, list[1]);
        }
    }
}