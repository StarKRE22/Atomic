using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void Clear_NonEmptyList_EmptiesList()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3, 4);
            Assert.AreEqual(4, list.Count);

            list.Clear();

            Assert.AreEqual(0, list.Count);
            Assert.IsFalse(list.Contains(1));
            Assert.IsFalse(list.Contains(2));
            Assert.IsFalse(list.Contains(3));
            Assert.IsFalse(list.Contains(4));
        }

        [Test]
        public void Clear_EmptyList_StillEmpty()
        {
            var list = new ReactiveLinkedList<int>();
            Assert.AreEqual(0, list.Count);

            list.Clear();

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Clear_AddAfterClear_WorksCorrectly()
        {
            var list = new ReactiveLinkedList<int>(1, 2);
            list.Clear();
            list.Add(5);
            list.Add(10);

            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Contains(5));
            Assert.IsTrue(list.Contains(10));
        }
    }
}