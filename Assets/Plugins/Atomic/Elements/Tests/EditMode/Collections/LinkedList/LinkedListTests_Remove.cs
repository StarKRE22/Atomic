using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void Remove_ItemExists_RemovesItemAndReturnsTrue()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);
            bool result = list.Remove(2);

            Assert.IsTrue(result);
            Assert.AreEqual(2, list.Count);
            CollectionAssert.AreEqual(new int[] { 1, 3 }, new int[] { list[0], list[1] });
        }

        [Test]
        public void Remove_ItemDoesNotExist_ReturnsFalse()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);
            bool result = list.Remove(5);

            Assert.IsFalse(result);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void Remove_NullItem_ReturnsFalse()
        {
            var list = new ReactiveLinkedList<string>("a", "b", "c");
            bool result = list.Remove(null);

            Assert.IsFalse(result);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void Remove_HeadItem_UpdatesHeadCorrectly()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);
            bool result = list.Remove(1);

            Assert.IsTrue(result);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(2, list[0]);
            Assert.AreEqual(3, list[1]);
        }

        [Test]
        public void Remove_TailItem_UpdatesTailCorrectly()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);
            bool result = list.Remove(3);

            Assert.IsTrue(result);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
        }

        [Test]
        public void Remove_EmptyList_ReturnsFalse()
        {
            var list = new ReactiveLinkedList<int>();
            bool result = list.Remove(1);

            Assert.IsFalse(result);
            Assert.AreEqual(0, list.Count);
        }
    }
}