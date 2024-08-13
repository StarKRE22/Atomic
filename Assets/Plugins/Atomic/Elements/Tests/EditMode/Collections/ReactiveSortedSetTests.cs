using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ReactiveSortedSetTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha");

            //Assert:
            Assert.AreEqual(3, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Masha"));

            Assert.AreEqual(new[] {"Masha", "Petya", "Vasya"}, set);
            Assert.IsFalse(set.IsReadOnly);
            Assert.IsFalse(set.IsEmpty());
            Assert.IsTrue(set.IsNotEmpty());
        }

        [Test]
        public void Add()
        {
            //Arrange:
            bool stateChanged = false;
            string addedItem = null;

            var set = new ReactiveSortedSet<string>();
            set.OnStateChanged += () => stateChanged = true;
            set.OnItemAdded += v => { addedItem = v; };

            //Act:
            set.Add("Vasya");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(1, set.Count);
            Assert.IsTrue(set.Contains("Vasya"));

            Assert.AreEqual("Vasya", addedItem);
        }

        [Test]
        public void WhenAddItemThatAlreadyExistsThenNothingHappened()
        {
            //Arrange:
            bool stateChanged = false;
            string addedItem = null;

            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha");

            set.OnStateChanged += () => stateChanged = true;
            set.OnItemAdded += v => { addedItem = v; };

            //Act:
            bool success = set.Add("Vasya");

            //Assert:
            Assert.IsFalse(success);
            Assert.IsFalse(stateChanged);
            Assert.AreEqual(3, set.Count);

            Assert.IsNull(addedItem);
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            bool stateChanged = false;
            string removedItem = null;

            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha");
            set.OnItemRemoved += v => removedItem = v;
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            bool success = set.Remove("Petya");

            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(2, set.Count);

            Assert.IsFalse(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Masha"));

            Assert.AreEqual("Petya", removedItem);
        }

        [Test]
        public void WhenRemoveAbsentItemThenReturnFalse()
        {
            //Arrange:
            bool stateChanged = false;
            string removedItem = null;

            var set = new ReactiveSortedSet<string>("Vasya", "Masha");
            set.OnItemRemoved += v => removedItem = v;
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            bool success = set.Remove("Petya");

            //Assert:
            Assert.IsFalse(success);
            Assert.IsFalse(stateChanged);
            Assert.AreEqual(2, set.Count);

            Assert.IsFalse(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Masha"));

            Assert.IsNull(removedItem);
        }

        [Test]
        public void ExceptWith()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha", "Ivan");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.ExceptWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(2, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsFalse(set.Contains("Petya"));
            Assert.IsFalse(set.Contains("Ivan"));
        }

        [Test]
        public void IntersectWith()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha", "Ivan", "Willy");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.IntersectWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(2, set.Count);

            Assert.IsFalse(set.Contains("Vasya"));
            Assert.IsFalse(set.Contains("Masha"));

            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Ivan"));
        }

        [Test]
        public void SymmetricExceptWith()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha", "Ivan", "Willy");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.SymmetricExceptWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(4, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsTrue(set.Contains("Willy"));
            Assert.IsTrue(set.Contains("John"));

            Assert.IsFalse(set.Contains("Petya"));
            Assert.IsFalse(set.Contains("Ivan"));
        }

        [Test]
        public void UnionWith()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha", "Willy");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.UnionWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(6, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsTrue(set.Contains("Willy"));
            Assert.IsTrue(set.Contains("John"));
            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Ivan"));
        }

        [Test]
        public void CopyTo()
        {
            //Arrange:
            var list = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha");
            var arr = new string[3];

            //Act:
            list.CopyTo(arr);

            Assert.AreEqual(new[] {"Masha", "Petya", "Vasya"}, arr);
        }


        [Test]
        public void Clear()
        {
            //Arrange:
            var wasEvent = false;
            var stateChanged = false;
            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha");

            //Act:
            set.OnStateChanged += () => stateChanged = true;
            set.OnCleared += () => wasEvent = true;
            set.Clear();

            Assert.IsTrue(wasEvent);
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(0, set.Count);
            Assert.AreEqual(Array.Empty<string>(), set);
        }

        [Test]
        public void WhenClearEmptyListThenNothingHappened()
        {
            //Arrange:
            var wasEvent = false;
            var stateChanged = false;
            var set = new ReactiveSortedSet<string>();

            //Act:
            set.OnStateChanged += () => stateChanged = true;
            set.OnCleared += () => wasEvent = true;
            set.Clear();

            Assert.IsFalse(wasEvent);
            Assert.IsFalse(stateChanged);
        }


        [Test]
        public void ReplaceWith()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha", "Willy");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.ReplaceWith("Petya", "Ivan", "John");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(3, set.Count);

            Assert.IsFalse(set.Contains("Vasya"));
            Assert.IsFalse(set.Contains("Masha"));
            Assert.IsFalse(set.Contains("Willy"));

            Assert.IsTrue(set.Contains("John"));
            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Ivan"));
        }

        [Test]
        public void ReplaceWithItem()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveSortedSet<string>("Vasya", "Petya", "Masha", "Willy");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.ReplaceWith("Petya");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(1, set.Count);

            Assert.IsFalse(set.Contains("Vasya"));
            Assert.IsFalse(set.Contains("Masha"));
            Assert.IsFalse(set.Contains("Willy"));

            Assert.IsTrue(set.Contains("Petya"));
        }

        [Test]
        public void OnBeforSerialize()
        {
            //Arrange:
            var set = new ReactiveSortedSet<string>
            {
                "Milk",
                "Bread",
                "Butter"
            };

            //Act:
            set.OnBeforeSerialize();

            //Assert:
            List<string> items = typeof(ReactiveSortedSet<string>)
                .GetField("items", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!
                .GetValue(set) as List<string>;

            Assert.AreEqual(new List<string>
            {
                "Bread",
                "Butter",
                "Milk"
            }, items);
        }

        [Test]
        public void OnAfterDeserialize()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveSortedSet<string>();
            set.OnStateChanged += () => stateChanged = true;

            typeof(ReactiveSortedSet<string>)
                .GetField("items", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!
                .SetValue(set, new List<string>
                {
                    "Milk",
                    "Bread",
                    "Butter"
                });

            //Pre-assert:
            Assert.AreEqual(0, set.Count);

            //Act:
            set.OnAfterDeserialize();

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(3, set.Count);

            Assert.IsTrue(set.Contains("Milk"));
            Assert.IsTrue(set.Contains("Bread"));
            Assert.IsTrue(set.Contains("Butter"));
        }
    }
}