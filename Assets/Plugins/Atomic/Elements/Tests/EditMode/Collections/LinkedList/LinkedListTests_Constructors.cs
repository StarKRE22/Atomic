using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class LinkedListTests
    {
        [Test]
        public void Constructor_WithCapacity_InitializesEmptyList()
        {
            var list = new LinkedList<int>(10);

            Assert.AreEqual(0, list.Count);
            list.Add(5);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Constructor_WithParams_AddsAllItems()
        {
            var list = new LinkedList<int>(1, 2, 3, 4);

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
            Assert.AreEqual(4, list[3]);
        }

        [Test]
        public void Constructor_WithEnumerable_AddsAllItems()
        {
            var source = new List<int> {10, 20, 30};
            var list = new LinkedList<int>(source);

            Assert.AreEqual(source.Count, list.Count);
            for (int i = 0; i < source.Count; i++)
            {
                Assert.AreEqual(source[i], list[i]);
            }
        }

        [Test]
        public void Constructor_WithEnumerable_EmptyCollection_InitializesEmptyList()
        {
            var emptySource = new List<int>();
            var list = new LinkedList<int>(emptySource);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Constructor_DefaultCapacity_InitializesEmptyList()
        {
            var list = new LinkedList<int>();

            Assert.AreEqual(0, list.Count);
            list.Add(42);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void Constructor_WithArray_InitializesListWithItems()
        {
            var list = new LinkedList<int>(1, 2, 3);

            Assert.AreEqual(3, list.Count, "Count should match the number of items.");
            CollectionAssert.AreEqual(new[] {1, 2, 3}, list.ToArray());
        }

        [Test]
        public void Constructor_WithEnumerable_InitializesListWithItems()
        {
            IEnumerable<int> source = new List<int> {10, 20, 30};
            var list = new LinkedList<int>(source);

            Assert.AreEqual(3, list.Count, "Count should match the number of items in enumerable.");
            CollectionAssert.AreEqual(source.ToArray(), list.ToArray());
        }

        [Test]
        public void Constructor_WithEmptyArrayOrEnumerable_InitializesEmptyList()
        {
            var list1 = new LinkedList<int>(Array.Empty<int>());
            var list2 = new LinkedList<int>(Enumerable.Empty<int>());

            Assert.AreEqual(0, list1.Count);
            Assert.AreEqual(0, list2.Count);
        }
        
        [Test]
        public void Constructor_WithDefaultCapacity_InitializesEmptyList()
        {
            var list = new LinkedList<int>();

            Assert.AreEqual(0, list.Count, "Count should be 0 for a new list.");
            CollectionAssert.IsEmpty(list.ToArray(), "List should be empty.");
        }


        [Test]
        public void Constructor_WithEmptyArray_InitializesEmptyList()
        {
            var list = new LinkedList<int>(Array.Empty<int>());

            Assert.AreEqual(0, list.Count, "Count should be 0 for empty array.");
            CollectionAssert.IsEmpty(list.ToArray(), "List should be empty.");
        }


        [Test]
        public void Constructor_WithEmptyEnumerable_InitializesEmptyList()
        {
            var list = new LinkedList<int>(Enumerable.Empty<int>());

            Assert.AreEqual(0, list.Count, "Count should be 0 for empty enumerable.");
            CollectionAssert.IsEmpty(list.ToArray(), "List should be empty.");
        }

        [Test]
        public void Constructor_WithNullEnumerable_ThrowsArgumentNullException()
        {
            IEnumerable<int> nullEnumerable = null;
            Assert.Throws<ArgumentNullException>(() => new LinkedList<int>(nullEnumerable));
        }

        [Test]
        public void Constructor_WithSingleItemArray_InitializesList()
        {
            var list = new LinkedList<string>("Hello");

            Assert.AreEqual(1, list.Count);
            CollectionAssert.AreEqual(new[] {"Hello"}, list.ToArray());
        }
    }
}