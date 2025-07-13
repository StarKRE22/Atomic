using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void ExceptWith()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Ivan");

            //Act:
            set.ExceptWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.AreEqual(2, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsFalse(set.Contains("Petya"));
            Assert.IsFalse(set.Contains("Ivan"));
        }

        [Test]
        public void ExceptWith_OnStateChanged()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Ivan");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.ExceptWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void ExceptWith_OnItemRemoved()
        {
            //Arrange:
            var removedItems = new HashSet<string>();
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Ivan");
            set.OnItemRemoved += item => removedItems.Add(item);

            //Act:
            set.ExceptWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.AreEqual(2, removedItems.Count);

            Assert.IsTrue(removedItems.Contains("Petya"));
            Assert.IsTrue(removedItems.Contains("Ivan"));
        }

        [Test]
        public void ExceptWith_NoOnStateChanged()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Ivan");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.ExceptWith(new[] {"Max", "Oleg", "John"});

            //Assert:
            Assert.IsFalse(stateChanged);
        }

        [Test]
        public void ExceptWith_NoOnItemRemoved()
        {
            //Arrange:
            var removed = false;
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Ivan");
            set.OnItemRemoved += _ => removed = true;

            //Act:
            set.ExceptWith(new[] {"Max", "Oleg", "John"});

            //Assert:
            Assert.IsFalse(removed);
        }
    }
}