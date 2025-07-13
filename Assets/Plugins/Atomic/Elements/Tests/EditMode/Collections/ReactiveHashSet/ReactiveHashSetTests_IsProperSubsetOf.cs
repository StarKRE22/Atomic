using System;
using System.Collections.Generic;
using NUnit.Framework;
// ReSharper disable AssignNullToNotNullAttribute

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void IsProperSubsetOf_Null_ThrowsException()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            Assert.Throws<ArgumentNullException>(() => reactiveSet.IsProperSubsetOf(null));
        }
        
        [Test]
        public void IsProperSubsetOf_StrictSubset_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsTrue(reactiveSet.IsProperSubsetOf(setB)); // ✅ {1,2} ⊂ {1,2,3}
        }

        [Test]
        public void IsProperSubsetOf_EqualSets_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsFalse(reactiveSet.IsProperSubsetOf(setB)); // ❌ {1,2,3} ⊄ {1,2,3}
        }

        [Test]
        public void IsProperSubsetOf_SuperSet_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2};

            Assert.IsFalse(reactiveSet.IsProperSubsetOf(setB)); // ❌ {1,2,3} ⊄ {1,2}
        }

        [Test]
        public void IsProperSubsetOf_DisjointSets_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {3, 4};

            Assert.IsFalse(reactiveSet.IsProperSubsetOf(setB)); // ❌ {1,2} ⊄ {3,4}
        }

        [Test]
        public void IsProperSubsetOf_EmptySet_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int>(); // Пустое множество
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsTrue(reactiveSet.IsProperSubsetOf(setB)); // ✅ ∅ ⊂ {1,2,3}
        }

        [Test]
        public void IsProperSubsetOf_NonEmptyVsEmptySet_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int>(); // Пустое множество

            Assert.IsFalse(reactiveSet.IsProperSubsetOf(setB)); // ❌ {1,2,3} ⊄ ∅
        }
    }
}