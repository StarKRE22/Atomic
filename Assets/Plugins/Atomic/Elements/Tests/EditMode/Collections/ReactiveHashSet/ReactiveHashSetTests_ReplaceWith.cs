using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        private ReactiveHashSet<int> _set;
        private HashSet<int> _addedItems;
        private HashSet<int> _removedItems;
        private bool _stateChanged;

        [SetUp]
        public void Setup()
        {
            _set = new ReactiveHashSet<int> {1, 2, 3};
            _addedItems = new HashSet<int>();
            _removedItems = new HashSet<int>();
            _stateChanged = false;

            _set.OnItemAdded += item => _addedItems.Add(item);
            _set.OnItemRemoved += item => _removedItems.Add(item);
            _set.OnStateChanged += () => _stateChanged = true;
        }

        [Test]
        public void ReplaceWith_ReplacesElements_Correctly()
        {
            var newElements = new HashSet<int> {3, 4, 5};

            _set.ReplaceWith(newElements);

            Assert.IsTrue(newElements.SetEquals(_set));
            Assert.IsTrue(_addedItems.SetEquals(new List<int>{4, 5}));
            Assert.IsTrue(_removedItems.SetEquals(new List<int>{1, 2}));
            
            Assert.IsTrue(_stateChanged); // Было изменение состояния
        }

        [Test]
        public void ReplaceWith_OnlyRemovesElements()
        {
            var newElements = new List<int> {2, 3};

            _set.ReplaceWith(newElements);

            CollectionAssert.AreEquivalent(newElements, _set);
            CollectionAssert.IsEmpty(_addedItems);
            CollectionAssert.AreEquivalent(new List<int> {1}, _removedItems);
            Assert.IsTrue(_stateChanged);
        }

        [Test]
        public void ReplaceWith_OnlyAddsElements()
        {
            var newElements = new List<int> {1, 2, 3, 4, 5};

            _set.ReplaceWith(newElements);

            CollectionAssert.AreEquivalent(newElements, _set);
            CollectionAssert.AreEquivalent(new List<int> {4, 5}, _addedItems);
            CollectionAssert.IsEmpty(_removedItems);
            Assert.IsTrue(_stateChanged);
        }

        [Test]
        public void ReplaceWith_NoChanges_DoesNotTriggerEvents()
        {
            var newElements = new List<int> {1, 2, 3};

            _set.ReplaceWith(newElements);

            CollectionAssert.AreEquivalent(newElements, _set);
            CollectionAssert.IsEmpty(_addedItems);
            CollectionAssert.IsEmpty(_removedItems);
            Assert.IsFalse(_stateChanged);
        }

        [Test]
        public void ReplaceWith_EmptySet_ClearsAll()
        {
            _set.ReplaceWith(new List<int>());

            CollectionAssert.IsEmpty(_set);
            CollectionAssert.AreEquivalent(new List<int> {1, 2, 3}, _removedItems);
            CollectionAssert.IsEmpty(_addedItems);
            Assert.IsTrue(_stateChanged);
        }

        [Test]
        public void ReplaceWith_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _set.ReplaceWith(null));
        }
    }
}