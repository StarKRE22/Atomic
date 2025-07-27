using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable CollectionNeverQueried.Local

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void UnionWithNull()
        {
            var set = new ReactiveHashSet<string>();
            Assert.Throws<ArgumentNullException>(() => set.UnionWith(null));
        }

        [Test]
        public void UnionWith_OneItem()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>();

            //Act:
            set.UnionWith(new[] {"Vasya"});

            //Assert:
            Assert.AreEqual(1, set.Count);
            Assert.IsTrue(set.Contains("Vasya"));
        }

        [Test]
        public void UnionWith_TwoItems()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>();

            //Act:
            set.UnionWith(new[] {"Vasya", "Petya"});

            //Assert:
            Assert.AreEqual(2, set.Count);
            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Petya"));
        }

        [Test]
        public void UnionWith_And_SetIsEmpty()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>();

            //Act:
            set.UnionWith(new[] {"Vasya", "Petya", "Masha", "Willy"});

            //Assert:
            Assert.AreEqual(4, set.Count);
            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsTrue(set.Contains("Willy"));
        }

        [Test]
        public void UnionWith_And_SetIsNotEmpty()
        {
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Willy");

            //Act:
            set.UnionWith(new[] {"Petya", "Ivan", "John"});

            Assert.AreEqual(6, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsTrue(set.Contains("Willy"));
            Assert.IsTrue(set.Contains("John"));
            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Ivan"));
        }

        [TestCaseSource(nameof(UnionWith_OnStateChangedCases))]
        public bool UnionWith_OnStateChanged(Func<ReactiveHashSet<string>> setFactory, IEnumerable<string> items)
        {
            // Arrange:
            bool stateChanged = false;
            var set = setFactory();
            set.OnStateChanged += () => stateChanged = true;

            // Act:
            set.UnionWith(items);

            // Assert:
            return stateChanged;
        }
        
        private static IEnumerable<TestCaseData> UnionWith_OnStateChangedCases()
        {
            yield return new TestCaseData(
                    new Func<ReactiveHashSet<string>>(() => new ReactiveHashSet<string>()),
                    Array.Empty<string>())
                .SetName("Empty Items")
                .Returns(false);

            yield return new TestCaseData(
                    new Func<ReactiveHashSet<string>>(() => new ReactiveHashSet<string>()),
                    new[] { "Petya", "Ivan", "John" })
                .SetName("Empty Set")
                .Returns(true);

            yield return new TestCaseData(
                    new Func<ReactiveHashSet<string>>(() => {
                        var set = new ReactiveHashSet<string>();
                        set.UnionWith(new[] { "Petya", "Ivan", "John" });
                        return set;
                    }),
                    new[] { "Petya", "Ivan", "John" })
                .SetName("All exists")
                .Returns(false);

            yield return new TestCaseData(
                    new Func<ReactiveHashSet<string>>(() => {
                        var set = new ReactiveHashSet<string>();
                        set.UnionWith(new[] { "Petya", "Ivan" });
                        return set;
                    }),
                    new[] { "Petya", "Ivan", "John" })
                .SetName("Partial Insert")
                .Returns(true);
        }

        [Test]
        public void UnionWith_OnItemsAdded()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Willy");
            var addedItems = new HashSet<string>();
            set.OnItemAdded += added => addedItems.Add(added);

            //Act:
            set.UnionWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.AreEqual(2, addedItems.Count);
            
            Assert.IsFalse(addedItems.Contains("Vasya"));
            Assert.IsFalse(addedItems.Contains("Masha"));
            Assert.IsFalse(addedItems.Contains("Petya"));
            Assert.IsFalse(addedItems.Contains("Willy"));
            Assert.IsTrue(addedItems.Contains("Ivan"));
            Assert.IsTrue(addedItems.Contains("John"));
        }
    }
}