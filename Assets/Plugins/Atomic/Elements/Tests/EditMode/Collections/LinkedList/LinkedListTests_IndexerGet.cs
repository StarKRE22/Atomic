using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void Get_ValidIndex_ReturnsCorrectItem()
        {
            var list = new ReactiveLinkedList<int>(10, 20, 30);

            Assert.AreEqual(10, list[0]);
            Assert.AreEqual(20, list[1]);
            Assert.AreEqual(30, list[2]);
        }

        [Test]
        public void Get_NegativeIndex_ThrowsArgumentOutOfRangeException()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var x = list[-1];
            });
        }

        [Test]
        public void Get_IndexEqualToCount_ThrowsArgumentOutOfRangeException()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var x = list[list.Count];
            });
        }

        [Test]
        public void Get_IndexGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var x = list[list.Count + 1];
            });
        }

        [Test]
        public void Get_SingleElementList_ReturnsItem()
        {
            var list = new ReactiveLinkedList<string>("Hello");

            Assert.AreEqual("Hello", list[0]);
        }
    }
}