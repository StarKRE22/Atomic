using System;
using System.Collections.Generic;
using NUnit.Framework;
// ReSharper disable AssignNullToNotNullAttribute

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void IsSubsetOf_Null_ThrowsException()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            Assert.Throws<ArgumentNullException>(() => reactiveSet.IsProperSubsetOf(null));
        }
        
        [Test]
        public void IsSubsetOf_StrictSubset_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsTrue(reactiveSet.IsSubsetOf(setB)); // ✅ {1,2} ⊆ {1,2,3}
        }

        [Test]
        public void IsSubsetOf_EqualSets_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsTrue(reactiveSet.IsSubsetOf(setB)); // ✅ {1,2,3} ⊆ {1,2,3}
        }

        [Test]
        public void IsSubsetOf_SuperSet_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2};

            Assert.IsFalse(reactiveSet.IsSubsetOf(setB)); // ❌ {1,2,3} ⊈ {1,2}
        }

        [Test]
        public void IsSubsetOf_DisjointSets_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {3, 4};

            Assert.IsFalse(reactiveSet.IsSubsetOf(setB)); // ❌ {1,2} ⊈ {3,4}
        }

        [Test]
        public void IsSubsetOf_EmptySet_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int>(); // Пустое множество
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsTrue(reactiveSet.IsSubsetOf(setB)); // ✅ ∅ ⊆ {1,2,3}
        }

        [Test]
        public void IsSubsetOf_NonEmptyVsEmptySet_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int>(); // Пустое множество

            Assert.IsFalse(reactiveSet.IsSubsetOf(setB)); // ❌ {1,2,3} ⊈ ∅
        }

        [Test]
        public void IsSubsetOf_Self_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            Assert.IsTrue(reactiveSet.IsSubsetOf(reactiveSet)); // ✅ Любое множество ⊆ самому себе
        }
    }
}