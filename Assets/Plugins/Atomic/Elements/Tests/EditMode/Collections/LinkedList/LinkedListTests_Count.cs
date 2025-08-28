using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class LinkedListTests
    {
        [Test]
        public void Count_InitiallyIsZero()
        {
            var list = new LinkedList<int>();
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Count_IncreasesAfterAdd()
        {
            var list = new LinkedList<int>();
            list.Add(10);
            Assert.AreEqual(1, list.Count);

            list.Add(20);
            list.Add(30);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void Count_DecreasesAfterRemove()
        {
            var list = new LinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
        
            list.Remove(2);
            Assert.AreEqual(2, list.Count);

            list.RemoveAt(0);
            Assert.AreEqual(1, list.Count);

            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Count_ReflectsConstructorWithItems()
        {
            var list = new LinkedList<int>(1, 2, 3, 4);
            Assert.AreEqual(4, list.Count);
        }
    }
}