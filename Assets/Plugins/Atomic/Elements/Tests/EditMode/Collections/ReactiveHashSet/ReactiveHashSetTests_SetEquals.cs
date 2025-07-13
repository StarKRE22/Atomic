using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void SetEquals_EqualSets_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsTrue(reactiveSet.SetEquals(setB)); // ✅ {1,2,3} == {1,2,3}
        }

        [Test]
        public void SetEquals_ViceVersa()
        {
            var other = new HashSet<int> {1, 2, 3};
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3};
            Assert.IsTrue(other.SetEquals(reactiveSet));
        }

        [Test]
        public void SetEquals_EqualSetsDifferentOrder_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int> {3, 2, 1};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsTrue(reactiveSet.SetEquals(setB)); // ✅ {3,2,1} == {1,2,3}
        }

        [Test]
        public void SetEquals_Superset_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2, 3, 4};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsFalse(reactiveSet.SetEquals(setB)); // ❌ {1,2,3,4} != {1,2,3}
        }

        [Test]
        public void SetEquals_Subset_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsFalse(reactiveSet.SetEquals(setB)); // ❌ {1,2} != {1,2,3}
        }

        [Test]
        public void SetEquals_DisjointSets_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            var setB = new HashSet<int> {3, 4};

            Assert.IsFalse(reactiveSet.SetEquals(setB)); // ❌ {1,2} != {3,4}
        }

        [Test]
        public void SetEquals_EmptySets_ReturnsTrue()
        {
            var reactiveSet = new ReactiveHashSet<int>();
            var setB = new HashSet<int>();

            Assert.IsTrue(reactiveSet.SetEquals(setB)); // ✅ ∅ == ∅
        }

        [Test]
        public void SetEquals_EmptySetVsNonEmpty_ReturnsFalse()
        {
            var reactiveSet = new ReactiveHashSet<int>();
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsFalse(reactiveSet.SetEquals(setB)); // ❌ ∅ != {1,2,3}
        }
    }
}