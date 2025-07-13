using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable AssignNullToNotNullAttribute

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void IsSupersetOf_Null_ThrowsException()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            Assert.Throws<ArgumentNullException>(() => reactiveSet.IsSupersetOf(null));
        }

        [Test]
        public void IsSupersetOf_StrictSuperset_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2};
            Assert.IsTrue(reactiveSet.IsSupersetOf(setB)); // ✅ {1,2,3} ⊇ {1,2}
        }

        [Test]
        public void IsSupersetOf_EqualSets_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2, 3};
            Assert.IsTrue(reactiveSet.IsSupersetOf(setB)); // ✅ {1,2,3} ⊇ {1,2,3}
        }

        [Test]
        public void IsSupersetOf_Subset_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {1, 2, 3};
            Assert.IsFalse(reactiveSet.IsSupersetOf(setB)); // ❌ {1,2} ⊉ {1,2,3}
        }

        [Test]
        public void IsSupersetOf_DisjointSets_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {3, 4};
            Assert.IsFalse(reactiveSet.IsSupersetOf(setB)); // ❌ {1,2} ⊉ {3,4}
        }

        [Test]
        public void IsSupersetOf_EmptySet_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int>(); // Пустое множество
            Assert.IsTrue(reactiveSet.IsSupersetOf(setB)); // ✅ {1,2,3} ⊇ ∅
        }

        [Test]
        public void IsSupersetOf_NonEmptyVsEmptySet_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int>(); // Пустое множество
            var setB = new HashSet<int> {1, 2, 3};
            Assert.IsFalse(reactiveSet.IsSupersetOf(setB)); // ❌ ∅ ⊉ {1,2,3}
        }

        [Test]
        public void IsSupersetOf_Self_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            Assert.IsTrue(reactiveSet.IsSupersetOf(reactiveSet)); // ✅ Любое множество ⊇ само себе
        }
    }
}