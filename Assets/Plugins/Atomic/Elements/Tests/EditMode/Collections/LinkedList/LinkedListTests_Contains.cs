using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void Contains_ItemExists_ReturnsTrue()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3, 4);
            Assert.IsTrue(list.Contains(3));
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(4));
        }

        [Test]
        public void Contains_ItemDoesNotExist_ReturnsFalse()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);
            Assert.IsFalse(list.Contains(10));
            Assert.IsFalse(list.Contains(0));
        }

        [Test]
        public void Contains_EmptyList_ReturnsFalse()
        {
            var list = new ReactiveLinkedList<int>();
            Assert.IsFalse(list.Contains(1));
        }

        [Test]
        public void Contains_NullItem_ReturnsFalse()
        {
            var list = new ReactiveLinkedList<string>("a", "b", "c");
            Assert.IsFalse(list.Contains(null));
        }

        [Test]
        public void Contains_AllItems_ReturnsTrue()
        {
            var list = new ReactiveLinkedList<string>("x", "y", "z");
            foreach (var item in new[] { "x", "y", "z" })
            {
                Assert.IsTrue(list.Contains(item));
            }
        }
    }
}