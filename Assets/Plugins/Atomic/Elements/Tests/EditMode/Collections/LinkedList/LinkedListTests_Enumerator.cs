using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class LinkedListTests
    {
        [Test]
        public void GetEnumerator_IteratesOverAllElements()
        {
            var list = new LinkedList<int>(1, 2, 3, 4);
            var enumerated = new List<int>();

            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerated.Add(enumerator.Current);
            }

            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, enumerated);
        }

        [Test]
        public void GetEnumerator_EmptyList_NoElements()
        {
            var list = new LinkedList<int>();
            var enumerated = new List<int>();

            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerated.Add(enumerator.Current);
            }

            Assert.AreEqual(0, enumerated.Count);
        }

        [Test]
        public void Enumerator_Reset_WorksCorrectly()
        {
            var list = new LinkedList<int>(5, 6);
            var enumerator = list.GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(5, enumerator.Current);

            enumerator.Reset();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(5, enumerator.Current);
        }
    }
}