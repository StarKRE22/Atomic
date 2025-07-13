using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable AssignNullToNotNullAttribute

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void IsProperSupersetOf_Null_ThrowsException()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            Assert.Throws<ArgumentNullException>(() => reactiveSet.IsProperSupersetOf(null));
        }

        [Test]
        public void IsProperSupersetOf_StrictSuperset_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2};

            Assert.IsTrue(reactiveSet.IsProperSupersetOf(setB)); // ✅ {1,2,3} ⊃ {1,2}
        }

        [Test]
        public void IsProperSupersetOf_EqualSets_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsFalse(reactiveSet.IsProperSupersetOf(setB)); // ❌ {1,2,3} ⊅ {1,2,3}
        }

        [Test]
        public void IsProperSupersetOf_Subset_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsFalse(reactiveSet.IsProperSupersetOf(setB)); // ❌ {1,2} ⊅ {1,2,3}
        }

        [Test]
        public void IsProperSupersetOf_DisjointSets_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {3, 4};

            Assert.IsFalse(reactiveSet.IsProperSupersetOf(setB)); // ❌ {1,2} ⊅ {3,4}
        }

        [Test]
        public void IsProperSupersetOf_EmptySet_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int>(); // Пустое множество

            Assert.IsTrue(reactiveSet.IsProperSupersetOf(setB)); // ✅ {1,2,3} ⊃ ∅
        }

        [Test]
        public void IsProperSupersetOf_NonEmptyVsEmptySet_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int>(); // Пустое множество
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsFalse(reactiveSet.IsProperSupersetOf(setB)); // ❌ ∅ ⊅ {1,2,3}
        }

        [Test]
        public void IsProperSupersetOf_Self_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};

            Assert.IsFalse(reactiveSet.IsProperSupersetOf(reactiveSet)); // ❌ Любое множество ⊅ само себе
        }
    }
}