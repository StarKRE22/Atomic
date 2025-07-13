using System;
using System.Collections.Generic;
using NUnit.Framework;
// ReSharper disable AssignNullToNotNullAttribute

namespace Atomic.Elements
{
    [TestFixture]
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void Overlaps_Null_ThrowsException()
        {
            var reactiveSet = new ReactiveHashSet<int> {1, 2};
            Assert.Throws<ArgumentNullException>(() => reactiveSet.Overlaps(null));
        }
        
        [Test]
        public void Overlaps_CommonElements_ReturnsTrue()
        {
            var setA = new HashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {3, 4, 5};

            Assert.IsTrue(setA.Overlaps(setB)); // ✅ {1,2,3} ∩ {3,4,5} != ∅
        }

        [Test]
        public void Overlaps_DisjointSets_ReturnsFalse()
        {
            var setA = new HashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {4, 5, 6};

            Assert.IsFalse(setA.Overlaps(setB)); // ❌ {1,2,3} ∩ {4,5,6} == ∅
        }

        [Test]
        public void Overlaps_Subset_ReturnsTrue()
        {
            var setA = new HashSet<int> {1, 2, 3, 4, 5};
            var setB = new HashSet<int> {2, 3};

            Assert.IsTrue(setA.Overlaps(setB)); // ✅ {1,2,3,4,5} ∩ {2,3} != ∅
        }

        [Test]
        public void Overlaps_EqualSets_ReturnsTrue()
        {
            var setA = new HashSet<int> {1, 2, 3};
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsTrue(setA.Overlaps(setB)); // ✅ {1,2,3} ∩ {1,2,3} != ∅
        }

        [Test]
        public void Overlaps_EmptySetVsNonEmpty_ReturnsFalse()
        {
            var setA = new HashSet<int>();
            var setB = new HashSet<int> {1, 2, 3};

            Assert.IsFalse(setA.Overlaps(setB)); // ❌ ∅ ∩ {1,2,3} == ∅
        }

        [Test]
        public void Overlaps_NonEmptyVsEmptySet_ReturnsFalse()
        {
            var setA = new HashSet<int> {1, 2, 3};
            var setB = new HashSet<int>();

            Assert.IsFalse(setA.Overlaps(setB)); // ❌ {1,2,3} ∩ ∅ == ∅
        }

        [Test]
        public void Overlaps_TwoEmptySets_ReturnsFalse()
        {
            var setA = new HashSet<int>();
            var setB = new HashSet<int>();

            Assert.IsFalse(setA.Overlaps(setB)); // ❌ ∅ ∩ ∅ == ∅
        }
    }
}