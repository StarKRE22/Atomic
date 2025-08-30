using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void IndexOf_ItemExists_ReturnsCorrectIndex()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3, 4);

            Assert.AreEqual(0, list.IndexOf(1));
            Assert.AreEqual(2, list.IndexOf(3));
            Assert.AreEqual(3, list.IndexOf(4));
        }

        [Test]
        public void IndexOf_ItemDoesNotExist_ReturnsMinusOne()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);

            Assert.AreEqual(-1, list.IndexOf(5));
        }

        [Test]
        public void IndexOf_NullItem_ReturnsMinusOne()
        {
            var list = new ReactiveLinkedList<string>("a", "b", "c");

            Assert.AreEqual(-1, list.IndexOf(null));
        }

        [Test]
        public void IndexOf_EmptyList_ReturnsMinusOne()
        {
            var list = new ReactiveLinkedList<int>();

            Assert.AreEqual(-1, list.IndexOf(1));
        }

        [Test]
        public void IndexOf_Duplicates_ReturnsFirstOccurrence()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3, 2, 4);

            Assert.AreEqual(1, list.IndexOf(2));
        }
    }
}