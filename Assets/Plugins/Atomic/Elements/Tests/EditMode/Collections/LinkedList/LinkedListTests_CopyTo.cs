using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveLinkedListTests
    {
        [Test]
        public void CopyTo_ValidArray_CopiesElementsCorrectly()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);
            var array = new int[5];

            list.CopyTo(array, 1);

            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(1, array[1]);
            Assert.AreEqual(2, array[2]);
            Assert.AreEqual(3, array[3]);
            Assert.AreEqual(0, array[4]);
        }

        [Test]
        public void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);

            Assert.Throws<ArgumentNullException>(() => list.CopyTo(null, 0));
        }

        [Test]
        public void CopyTo_NegativeArrayIndex_ThrowsArgumentOutOfRangeException()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);
            var array = new int[5];

            Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(array, -1));
        }

        [Test]
        public void CopyTo_ArrayTooSmall_ThrowsArgumentException()
        {
            var list = new ReactiveLinkedList<int>(1, 2, 3);
            var array = new int[3];

            // arrayIndex 1 + count 3 > array.Length 3
            Assert.Throws<ArgumentException>(() => list.CopyTo(array, 1));
        }

        [Test]
        public void CopyTo_EmptyList_DoesNotModifyArray()
        {
            var list = new ReactiveLinkedList<int>();
            var array = new int[3] {10, 20, 30};

            list.CopyTo(array, 0);

            CollectionAssert.AreEqual(new int[] {10, 20, 30}, array);
        }
    }
}