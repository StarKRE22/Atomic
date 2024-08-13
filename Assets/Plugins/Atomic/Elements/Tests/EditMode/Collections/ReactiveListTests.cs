using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ReactiveListTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");

            //Assert:
            Assert.AreEqual(3, list.Count);

            Assert.IsTrue(list.Contains("Vasya"));
            Assert.IsTrue(list.Contains("Petya"));
            Assert.IsTrue(list.Contains("Masha"));

            Assert.AreEqual(new[] {"Vasya", "Petya", "Masha"}, list);
            Assert.IsFalse(list.IsReadOnly);
        }

        [Test]
        public void GetItem()
        {
            //Arrange:
            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");

            //Act & Assert:
            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Petya", list[1]);
            Assert.AreEqual("Masha", list[2]);

            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                var _ = list[-1];
            });

            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                var _ = list[3];
            });
        }

        [Test]
        public void Add()
        {
            //Arrange:
            bool stateChanged = false;
            string addedItem = null;
            int addedIndex = -1;

            var list = new ReactiveList<string>();
            list.OnStateChanged += () => stateChanged = true;
            list.OnItemInserted += (i, v) =>
            {
                addedIndex = i;
                addedItem = v;
            };

            //Act:
            list.Add("Vasya");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Vasya", list[0]);
            Assert.IsTrue(list.Contains("Vasya"));

            Assert.AreEqual("Vasya", addedItem);
            Assert.AreEqual(0, addedIndex);
        }

        [Test]
        public void Update()
        {
            //Arrange:
            bool stateChanged = false;
            string updatedItem = null;
            int updatedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            list.OnItemChanged += (i, v) =>
            {
                updatedIndex = i;
                updatedItem = v;
            };
            list.OnStateChanged += () => stateChanged = true;

            //Act:
            list.Update(1, "Ivan");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual("Ivan", list[1]);
            Assert.IsTrue(list.Contains("Ivan"));
            Assert.IsFalse(list.Contains("Petya"));

            Assert.AreEqual("Ivan", updatedItem);
            Assert.AreEqual(1, updatedIndex);
        }

        [Test]
        public void WhenUpdateSameItemWhenNothingHappened()
        {
            //Arrange:
            bool stateChanged = false;
            string updatedItem = null;
            int updatedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            list.OnItemChanged += (i, v) =>
            {
                updatedIndex = i;
                updatedItem = v;
            };
            list.OnStateChanged += () => stateChanged = true;

            //Act:
            list.Update(1, "Petya");

            //Assert:
            Assert.IsFalse(stateChanged);
            Assert.AreEqual("Petya", list[1]);
            Assert.IsTrue(list.Contains("Petya"));

            Assert.IsNull(updatedItem);
            Assert.AreEqual(-1, updatedIndex);
        }

        [Test]
        public void WhenUpdateInvalidIndexThenThrowsArgumentOutOfRangeException()
        {
            //Arrange:
            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            Assert.Catch<ArgumentOutOfRangeException>(() => list.Update(3, "Petya"));
            Assert.Catch<ArgumentOutOfRangeException>(() => list.Update(-1, "Petya"));
        }

        [Test]
        public void WhenClearEmptyListThenNothingHappened()
        {
            //Arrange:
            var wasEvent = false;
            var stateChanged = false;
            var list = new ReactiveList<string>();

            //Act:
            list.OnStateChanged += () => stateChanged = true;
            list.OnCleared += () => wasEvent = true;
            list.Clear();

            Assert.IsFalse(wasEvent);
            Assert.IsFalse(stateChanged);
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            var wasEvent = false;
            var stateChanged = false;
            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");

            //Act:
            list.OnStateChanged += () => stateChanged = true;
            list.OnCleared += () => wasEvent = true;
            list.Clear();

            Assert.IsTrue(wasEvent);
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(Array.Empty<string>(), list);
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            bool stateChanged = false;
            string removedItem = null;
            int removedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            list.OnItemDeleted += (i, v) =>
            {
                removedIndex = i;
                removedItem = v;
            };
            list.OnStateChanged += () => stateChanged = true;

            //Act:
            list.Remove("Petya");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(2, list.Count);

            Assert.IsFalse(list.Contains("Petya"));
            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Masha", list[1]);

            Assert.AreEqual("Petya", removedItem);
            Assert.AreEqual(1, removedIndex);
        }

        [Test]
        public void WhenRemoveAbsentItemThenReturnFalse()
        {
            //Arrange:
            bool stateChanged = false;
            string removedItem = null;
            int removedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            list.OnItemDeleted += (i, v) =>
            {
                removedIndex = i;
                removedItem = v;
            };
            list.OnStateChanged += () => stateChanged = true;

            //Act:
            list.Remove("Ivan");

            //Assert:
            Assert.IsFalse(stateChanged);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Petya", list[1]);
            Assert.AreEqual("Masha", list[2]);

            Assert.IsNull(removedItem);
            Assert.AreEqual(-1, removedIndex);
        }

        [Test]
        public void RemoveAt()
        {
            //Arrange:
            bool stateChanged = false;
            string removedItem = null;
            int removedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            list.OnItemDeleted += (i, v) =>
            {
                removedIndex = i;
                removedItem = v;
            };
            list.OnStateChanged += () => stateChanged = true;

            //Act:
            list.RemoveAt(1);

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(2, list.Count);

            Assert.IsFalse(list.Contains("Petya"));
            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Masha", list[1]);

            Assert.AreEqual("Petya", removedItem);
            Assert.AreEqual(1, removedIndex);
        }

        [Test]
        public void WhenRemoveAtInvalidIndexThenNothingHappened()
        {
            //Arrange:
            bool stateChanged = false;
            string removedItem = null;
            int removedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            list.OnItemDeleted += (i, v) =>
            {
                removedIndex = i;
                removedItem = v;
            };
            list.OnStateChanged += () => stateChanged = true;

            //Act:
            list.RemoveAt(3);

            //Assert:
            Assert.IsFalse(stateChanged);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Petya", list[1]);
            Assert.AreEqual("Masha", list[2]);

            Assert.IsNull(removedItem);
            Assert.AreEqual(-1, removedIndex);

            //Act:
            list.RemoveAt(-1);

            //Assert:
            Assert.IsFalse(stateChanged);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Petya", list[1]);
            Assert.AreEqual("Masha", list[2]);

            Assert.IsNull(removedItem);
            Assert.AreEqual(-1, removedIndex);
        }

        [Test]
        public void InsertAtLast()
        {
            //Arrange:
            bool stateChanged = false;
            string addedItem = null;
            int addedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");

            list.OnStateChanged += () => stateChanged = true;
            list.OnItemInserted += (i, v) =>
            {
                addedIndex = i;
                addedItem = v;
            };

            //Act:
            list.Insert(3, "Ivan");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("Ivan", list[3]);

            Assert.AreEqual("Ivan", addedItem);
            Assert.AreEqual(3, addedIndex);
        }

        [Test]
        public void InsertAtFirst()
        {
            //Arrange:
            bool stateChanged = false;
            string addedItem = null;
            int addedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");

            list.OnStateChanged += () => stateChanged = true;
            list.OnItemInserted += (i, v) =>
            {
                addedIndex = i;
                addedItem = v;
            };

            //Act:
            list.Insert(0, "Ivan");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("Ivan", list[0]);
            Assert.AreEqual("Vasya", list[1]);
            Assert.AreEqual("Petya", list[2]);
            Assert.AreEqual("Masha", list[3]);

            Assert.AreEqual("Ivan", addedItem);
            Assert.AreEqual(0, addedIndex);
        }

        [Test]
        public void InsertInTheMiddle()
        {
            //Arrange:
            bool stateChanged = false;
            string addedItem = null;
            int addedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");

            list.OnStateChanged += () => stateChanged = true;
            list.OnItemInserted += (i, v) =>
            {
                addedIndex = i;
                addedItem = v;
            };

            //Act:
            list.Insert(2, "Ivan");

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Petya", list[1]);
            Assert.AreEqual("Ivan", list[2]);
            Assert.AreEqual("Masha", list[3]);

            Assert.AreEqual("Ivan", addedItem);
            Assert.AreEqual(2, addedIndex);
        }

        [Test]
        public void WhenInsertOnNegativeIndexThenThrowsArgumentOutOfRangeException()
        {
            //Arrange:
            bool stateChanged = false;
            string addedItem = null;
            int addedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");

            list.OnStateChanged += () => stateChanged = true;
            list.OnItemInserted += (i, v) =>
            {
                addedIndex = i;
                addedItem = v;
            };

            //Act:
            Assert.Catch<ArgumentOutOfRangeException>(() => list.Insert(-1, "Ivan"));

            //Assert:
            Assert.IsFalse(stateChanged);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Petya", list[1]);
            Assert.AreEqual("Masha", list[2]);

            Assert.IsNull(addedItem);
            Assert.AreEqual(-1, addedIndex);
        }

        [Test]
        public void WhenInsertInvalidIndexThenThrowsArgumentOutOfRangeException()
        {
            //Arrange:
            bool stateChanged = false;
            string addedItem = null;
            int addedIndex = -1;

            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");

            list.OnStateChanged += () => stateChanged = true;
            list.OnItemInserted += (i, v) =>
            {
                addedIndex = i;
                addedItem = v;
            };

            //Act:
            Assert.Catch<ArgumentOutOfRangeException>(() => list.Insert(5, "Ivan"));

            //Assert:
            Assert.IsFalse(stateChanged);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("Vasya", list[0]);
            Assert.AreEqual("Petya", list[1]);
            Assert.AreEqual("Masha", list[2]);

            Assert.IsNull(addedItem);
            Assert.AreEqual(-1, addedIndex);
        }

        [Test]
        public void CopyTo()
        {
            //Arrange:
            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            var arr = new string[3];

            //Act:
            list.CopyTo(arr);

            Assert.AreEqual(new[] {"Vasya", "Petya", "Masha"}, arr);
        }

        [Test]
        public void IndexOf()
        {
            var list = new ReactiveList<string>("Vasya", "Petya", "Masha");
            
            Assert.AreEqual(0, list.IndexOf("Vasya"));
            Assert.AreEqual(1, list.IndexOf("Petya"));
            Assert.AreEqual(2, list.IndexOf("Masha"));
            Assert.AreEqual(-1, list.IndexOf("Ivan"));
        }
    }
}