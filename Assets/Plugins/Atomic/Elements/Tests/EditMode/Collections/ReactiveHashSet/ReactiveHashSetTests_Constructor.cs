using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable CollectionNeverUpdated.Local

namespace Atomic.Elements
{
    public partial class ReactiveHashSetTests
    {
        [Test]
        public void Constructor()
        {
            Assert.DoesNotThrow(() =>
            {
                var unused = new ReactiveHashSet<string>();
            });
        }

        [Test]
        public void Constructor_Count()
        {
            var set = new ReactiveHashSet<string>();
            Assert.AreEqual(0, set.Count);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-100)]
        public void Constructor_NegativeCapacity_Exception(int capacity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var unused = new ReactiveHashSet<string>(capacity);
            });
        }

        [Test]
        public void Constructor_IsReadOnly()
        {
            var set = new ReactiveHashSet<string>();
            Assert.IsFalse(set.IsReadOnly);
        }

        [Test]
        public void Constructor_Params()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");

            //Assert:
            Assert.AreEqual(3, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsFalse(set.Contains("Ivan"));

            Assert.AreEqual(new[] {"Vasya", "Petya", "Masha"}, set);

            Assert.IsTrue(set.IsNotEmpty());
            Assert.IsFalse(set.IsEmpty());
        }

        [Test]
        public void Constructor_IEnumerable()
        {
            //Arrange:
            IEnumerable<string> enumerable = new[] {"Vasya", "Petya", "Masha"};
            var set = new ReactiveHashSet<string>(enumerable);

            //Assert:
            Assert.AreEqual(3, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsFalse(set.Contains("Ivan"));

            Assert.AreEqual(new[] {"Vasya", "Petya", "Masha"}, set);

            Assert.IsTrue(set.IsNotEmpty());
            Assert.IsFalse(set.IsEmpty());
        }
        
        [Test]
        public void Constructor_IReadOnlyCollection()
        {
            //Arrange:
            IReadOnlyCollection<string> enumerable = new[] {"Vasya", "Petya", "Masha"};
            var set = new ReactiveHashSet<string>(enumerable);

            //Assert:
            Assert.AreEqual(3, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsFalse(set.Contains("Ivan"));

            Assert.AreEqual(new[] {"Vasya", "Petya", "Masha"}, set);

            Assert.IsTrue(set.IsNotEmpty());
            Assert.IsFalse(set.IsEmpty());
        }
    }
}