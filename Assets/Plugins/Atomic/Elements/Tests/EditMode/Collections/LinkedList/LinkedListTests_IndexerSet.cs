using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class LinkedListTests
    {
        [Test]
        public void Set_ValidIndex_UpdatesItem()
        {
            var list = new LinkedList<int>(10, 20, 30);

            list[0] = 100;
            list[1] = 200;
            list[2] = 300;

            Assert.AreEqual(100, list[0]);
            Assert.AreEqual(200, list[1]);
            Assert.AreEqual(300, list[2]);
        }

        [Test]
        public void Set_NegativeIndex_ThrowsArgumentOutOfRangeException()
        {
            var list = new LinkedList<int>(1, 2, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() => { list[-1] = 10; });
        }

        [Test]
        public void Set_IndexEqualToCount_ThrowsArgumentOutOfRangeException()
        {
            var list = new LinkedList<int>(1, 2, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() => { list[list.Count] = 10; });
        }

        [Test]
        public void Set_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            var list = new LinkedList<int>(1, 2, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() => { list[list.Count + 1] = 10; });
        }

        [Test]
        public void Set_SingleElementList_UpdatesItem()
        {
            var list = new LinkedList<string>("Hello");

            list[0] = "World";

            Assert.AreEqual("World", list[0]);
        }
    }
}