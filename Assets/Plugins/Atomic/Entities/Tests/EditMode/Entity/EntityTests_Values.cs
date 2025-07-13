using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void GetValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");

            Entity e = new Entity("123", values: new Dictionary<int, object>
            {
                {key, foo}
            });

            //Act:
            string value = e.GetValue<string>(key);

            //Assert:
            Assert.AreEqual("Foo", value);
        }

        [Test]
        public void TryGetValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");

            Entity e = new Entity("123", values: new Dictionary<int, object>
            {
                {key, foo}
            });

            //Act:
            bool success = e.TryGetValue(key, out string value);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual("Foo", value);
        }

        [Test]
        public void WhenTryGetAbsentValueThenReturnFalse()
        {
            //Arrange:
            var e = new Entity("123");

            //Act:
            bool success = e.TryGetValue(0, out string foo);

            //Assert:
            Assert.IsFalse(success);
            Assert.IsNull(foo);
        }

        [Test]
        public void WhenGetAbsentValueThenThrowKeyNotFoundException()
        {
            //Arrange:
            var e = new Entity("123");

            //Act:
            Assert.Catch<KeyNotFoundException>(() => e.GetValue<string>(0));
        }

        [Test]
        public void WhenSetValueThatPreviousNotExistsInEntityThenAdded()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");
            var e = new Entity("123");

            var wasChangeEvent = false;
            int addedKey = -1;

            //Act:
            e.OnValueAdded += (_, k) => { addedKey = k; };
            e.OnValueChanged += (_, _) => { wasChangeEvent = false; };

            e.SetValue(key, foo);

            //Assert:
            Assert.AreEqual(addedKey, key);
            Assert.IsTrue(e.HasValue(key));
            Assert.IsFalse(wasChangeEvent);
            Assert.AreEqual(foo, e.GetValue<string>(key));
        }

        [Test]
        public void OverrideValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");
            string foo2 = new string("Foo2");

            var wasAddEvent = false;
            var changedKey = -1;

            Entity e = new Entity("123", values: new Dictionary<int, object>
            {
                {key, foo}
            });

            e.OnValueAdded += (_, _) => { wasAddEvent = false; };
            e.OnValueChanged += (_, k) => { changedKey = k; };

            //Act:

            e.SetValue(key, foo2);

            //Assert:
            Assert.AreEqual(changedKey, key);
            Assert.IsTrue(e.HasValue(key));
            Assert.IsFalse(wasAddEvent);
            Assert.AreEqual(foo2, e.GetValue<string>(key));
        }

        [Test]
        public void AddValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");

            bool wasAddEvent = false;
            int addedKey = -1;

            Entity e = new Entity("123");

            e.OnValueAdded += (_, k) =>
            {
                wasAddEvent = true;
                addedKey = k;
            };

            //Act:

            e.AddValue(key, foo);

            //Assert:

            Assert.IsTrue(wasAddEvent);
            Assert.AreEqual(addedKey, key);
            Assert.IsTrue(e.HasValue(key));
        }

        [Test]
        public void AddValue_AlreadyAdded_ThrowsException()
        {
            //Arrange:
            const int key = 1;
            string foo1 = new string("Foo1");
            string foo2 = new string("Foo2");

            Entity e = new Entity("123", values: new Dictionary<int, object>
            {
                {key, foo1}
            });

            //Act:
            Assert.Throws<ArgumentException>(() => e.AddValue(key, foo2));
            
            //Assert:
            Assert.AreEqual(foo1, e.GetValue<string>(key));
            Assert.AreNotEqual(foo2, e.GetValue<string>(key));
        }

        [Test]
        public void DelValue()
        {
            //Arrange:
            const int key1 = 1;
            const int key2 = 2;

            object foo1 = new object();
            object foo2 = new object();


            Entity e = new Entity("123", values: new Dictionary<int, object>
            {
                {key1, foo1},
                {key2, foo2}
            });

            //Act:
            bool success = e.DelValue(key1);

            //Assert:
            Assert.IsTrue(success);
            Assert.IsFalse(e.HasValue(key1));
            Assert.IsFalse(e.TryGetValue<object>(key1, out _));
            Assert.Throws<KeyNotFoundException>(() => e.GetValue<object>(key1));
        }

        [Test]
        public void WhenDelAbsentValueThenReturnFalse()
        {
            //Arrange:
            const int key1 = 1;

            Entity e = new Entity("123");

            //Act:
            bool success = e.DelValue(key1);

            //Assert:
            Assert.IsFalse(success);
        }

        [Test]
        public void ClearValues()
        {
            //Arrange:
            const int key1 = 1;
            const int key2 = 2;

            object foo1 = new object();
            object foo2 = new object();

            Entity e = new Entity("123", values: new Dictionary<int, object>
            {
                {key1, foo1},
                {key2, foo2}
            });


            var removedItems = new HashSet<object>();
            e.OnValueDeleted += (_, k) => removedItems.Add(k);

            //Act:
            e.ClearValues();

            //Assert:
            Assert.IsFalse(e.HasValue(key1));
            Assert.IsFalse(e.HasValue(key2));
            Assert.Throws<KeyNotFoundException>(() => e.GetValue<object>(key1));
            Assert.Throws<KeyNotFoundException>(() => e.GetValue<object>(key2));

            Assert.IsTrue(removedItems.Contains(key1));
            Assert.IsTrue(removedItems.Contains(key2));
        }
    }
}