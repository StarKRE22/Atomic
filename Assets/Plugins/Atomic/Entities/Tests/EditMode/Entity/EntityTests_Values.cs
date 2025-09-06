using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        #region OnValueAdded

        [Test]
        public void OnValueAdded_EventIsInvoked_WhenStructValueIsAdded()
        {
            var entity = new Entity(valueCapacity: 4);

            int? calledKey = null;
            entity.OnValueAdded += (_, key) => calledKey = key;

            const int testKey = 42;
            entity.AddValue(testKey, 123);

            Assert.AreEqual(testKey, calledKey);
        }

        [Test]
        public void OnValueAdded_EventIsInvoked_WhenReferenceValueIsAdded()
        {
            var entity = new Entity(valueCapacity: 4);

            int? calledKey = null;
            entity.OnValueAdded += (_, key) => calledKey = key;

            const int testKey = 100;
            object testValue = "hello";
            entity.AddValue(testKey, testValue);

            Assert.AreEqual(testKey, calledKey);
        }

        [Test]
        public void AddValue_ThrowsArgumentNullException_WhenReferenceValueIsNull()
        {
            var entity = new Entity(valueCapacity: 4);

            const int testKey = 1;
            var ex = Assert.Throws<ArgumentNullException>(() => entity.AddValue(testKey, null));
            Assert.That(ex.ParamName, Is.EqualTo("value"));
        }

        [Test]
        public void AddValue_ThrowsArgumentException_WhenKeyAlreadyExists_ForStruct()
        {
            var entity = new Entity(valueCapacity: 4);

            const int testKey = 5;
            entity.AddValue(testKey, 999);

            var ex = Assert.Throws<ArgumentException>(() => entity.AddValue(testKey, 123));
            StringAssert.Contains("already has been added", ex.Message);
        }

        [Test]
        public void AddValue_ThrowsArgumentException_WhenKeyAlreadyExists_ForReference()
        {
            var entity = new Entity(valueCapacity: 4);

            const int testKey = 7;
            entity.AddValue(testKey, "first");

            var ex = Assert.Throws<ArgumentException>(() => entity.AddValue(testKey, "second"));
            StringAssert.Contains("already has been added", ex.Message);
        }

        #endregion

        #region OnValueDeleted

        [Test]
        public void OnValueDeleted_EventIsInvoked_WhenValueIsDeleted()
        {
            var entity = new Entity(valueCapacity: 4);

            int key = 101;
            entity.AddValue(key, 42);

            int? deletedKey = null;
            entity.OnValueDeleted += (e, k) => deletedKey = k;

            bool result = entity.DelValue(key);

            Assert.IsTrue(result);
            Assert.AreEqual(key, deletedKey);
        }

        [Test]
        public void OnValueDeleted_EventIsNotInvoked_WhenKeyDoesNotExist()
        {
            var entity = new Entity(valueCapacity: 4);

            bool eventCalled = false;
            entity.OnValueDeleted += (_, _) => eventCalled = true;

            bool result = entity.DelValue(999); // не существует

            Assert.IsFalse(result);
            Assert.IsFalse(eventCalled);
        }

        [Test]
        public void OnValueDeleted_IsInvoked_WhenValueExists()
        {
            var entity = new Entity(valueCapacity: 4);
            int key = 10;
            entity.AddValue(key, 123);

            int? deletedKey = null;
            entity.OnValueDeleted += (e, k) => deletedKey = k;

            bool result = entity.DelValue(key);

            Assert.IsTrue(result);
            Assert.AreEqual(key, deletedKey);
        }

        [Test]
        public void OnValueDeleted_IsNotInvoked_WhenValueDoesNotExist()
        {
            var entity = new Entity(valueCapacity: 4);
            bool wasCalled = false;

            entity.OnValueDeleted += (_, _) => wasCalled = true;

            bool result = entity.DelValue(999);

            Assert.IsFalse(result);
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnValueDeleted_IsInvoked_AfterMultipleAdds()
        {
            var entity = new Entity(valueCapacity: 4);
            List<int> deletedKeys = new();

            entity.AddValue(1, "a");
            entity.AddValue(2, "b");
            entity.AddValue(3, "c");

            entity.OnValueDeleted += (_, k) => deletedKeys.Add(k);

            entity.DelValue(2);
            entity.DelValue(3);

            CollectionAssert.AreEqual(new[] {2, 3}, deletedKeys);
        }

        [Test]
        public void OnValueDeleted_DoesNotThrow_WhenNoSubscribers()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(1, 5);

            Assert.DoesNotThrow(() => entity.DelValue(1));
        }

        [Test]
        public void OnValueDeleted_NotifiesOnlyOnce_WhenSameKeyIsDeletedTwice()
        {
            var entity = new Entity(valueCapacity: 4);
            int callCount = 0;

            entity.AddValue(77, 77);
            entity.OnValueDeleted += (_, _) => callCount++;

            entity.DelValue(77);
            entity.DelValue(77); // второй раз — не должно вызвать

            Assert.AreEqual(1, callCount);
        }

        #endregion

        #region OnValueChanged

        [Test]
        public void OnValueChanged_IsInvoked_WhenStructValueIsSet()
        {
            var entity = new Entity(valueCapacity: 4);
            int key = 1;
            entity.AddValue(key, 10);

            int? changedKey = null;
            entity.OnValueChanged += (e, k) => changedKey = k;

            entity.SetValue(key, 20);

            Assert.AreEqual(key, changedKey);
        }

        [Test]
        public void OnValueChanged_IsInvoked_WhenReferenceValueIsSet()
        {
            var entity = new Entity(valueCapacity: 4);
            int key = 2;
            entity.AddValue(key, "hello");

            int? changedKey = null;
            entity.OnValueChanged += (e, k) => changedKey = k;

            entity.SetValue(key, "world");

            Assert.AreEqual(key, changedKey);
        }

        [Test]
        public void OnValueChanged_IsInvoked_WhenStructValueTypeChanges()
        {
            var entity = new Entity(valueCapacity: 4);
            int key = 3;
            entity.AddValue(key, 123);

            int callCount = 0;
            entity.OnValueChanged += (_, _) => callCount++;

            // Меняем тип, должно вызвать событие
            entity.SetValue(key, 3.14f);

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void OnValueChanged_IsNotInvoked_WhenReferenceValueIsSame()
        {
            var entity = new Entity(valueCapacity: 4);
            int key = 4;
            string value = "repeat";
            entity.AddValue(key, value);

            bool wasCalled = false;
            entity.OnValueChanged += (_, _) => wasCalled = true;

            entity.SetValue(key, value); // То же самое значение

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnValueChanged_IsInvoked_WhenNewKeyIsAddedViaSetValue()
        {
            var entity = new Entity(valueCapacity: 4);
            int key = 5;

            int? changedKey = null;
            entity.OnValueChanged += (e, k) => changedKey = k;

            entity.SetValue(key, 999); // Ключ ещё не существует — добавится

            Assert.AreEqual(key, changedKey);
        }

        #endregion

        #region ValueCount

        [Test]
        public void ValueCount_Increases_WhenValuesAreAdded()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(1, 100);
            entity.AddValue(2, "test");

            Assert.AreEqual(2, entity.ValueCount);
        }

        [Test]
        public void ValueCount_DoesNotIncrease_WhenAddingDuplicateKey()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(1, 123);

            Assert.Throws<ArgumentException>(() => entity.AddValue(1, 456));
            Assert.AreEqual(1, entity.ValueCount);
        }

        [Test]
        public void ValueCount_Decreases_WhenValueIsDeleted()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(1, 42);
            entity.AddValue(2, 43);

            entity.DelValue(1);

            Assert.AreEqual(1, entity.ValueCount);
        }

        [Test]
        public void ValueCount_BecomesZero_WhenAllValuesCleared()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(1, "a");
            entity.AddValue(2, "b");

            entity.ClearValues();

            Assert.AreEqual(0, entity.ValueCount);
        }

        [Test]
        public void ValueCount_ReflectsCurrentState_AfterMixedOperations()
        {
            var entity = new Entity(valueCapacity: 4);

            entity.AddValue(1, 1);
            Assert.AreEqual(1, entity.ValueCount);

            entity.AddValue(2, 2);
            Assert.AreEqual(2, entity.ValueCount);

            entity.DelValue(1);
            Assert.AreEqual(1, entity.ValueCount);

            entity.AddValue(3, 3);
            Assert.AreEqual(2, entity.ValueCount);
        }

        #endregion

        #region GetValue<T>

        [Test]
        public void GetTValue_ValueIsAbsent_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var e = new Entity("123");

            //Act:
            Assert.Catch<KeyNotFoundException>(() => e.GetValue<string>(0));
        }

        [Test]
        public void GetTValue_ReturnsCorrectStructValue()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(10, 42);

            int result = entity.GetValue<int>(10);

            Assert.AreEqual(42, result);
        }

        [Test]
        public void GetTValue_ReturnsCorrectReferenceValue()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(11, "Hello");

            string result = entity.GetValue<string>(11);

            Assert.AreEqual("Hello", result);
        }

        [Test]
        public void GetTValue_Throws_WhenCollectionEmpty()
        {
            var entity = new Entity(valueCapacity: 4);

            Assert.Throws<KeyNotFoundException>(() => entity.GetValue<int>(1));
        }

        [Test]
        public void GetTValue_Throws_WhenKeyNotFound()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(1, 10);

            Assert.Throws<KeyNotFoundException>(() => entity.GetValue<int>(2));
        }

        [Test]
        public void GetTValue_Throws_WhenTypeMismatch()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(5, "text");

            Assert.Throws<InvalidCastException>(() => entity.GetValue<int>(5));
        }

        [Test]
        public void GetTValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");

            Entity e = new Entity("123");
            e.AddValues(new Dictionary<int, object>
            {
                {key, foo}
            });

            //Act:
            string value = e.GetValue<string>(key);

            //Assert:
            Assert.AreEqual("Foo", value);
        }

        #endregion

        #region GetValue

        [Test]
        public void GetValue_ReturnsBoxedStruct()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(100, 123);

            object value = entity.GetValue(100);

            Assert.AreEqual(123, value);
            Assert.IsInstanceOf<int>(value);
        }

        [Test]
        public void GetValue_ReturnsReferenceType()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(101, "test");

            object value = entity.GetValue(101);

            Assert.AreEqual("test", value);
            Assert.IsInstanceOf<string>(value);
        }

        [Test]
        public void GetValue_Throws_WhenNoValuesExist()
        {
            var entity = new Entity(valueCapacity: 4);

            Assert.Throws<KeyNotFoundException>(() => entity.GetValue(1));
        }

        [Test]
        public void GetValue_Throws_WhenKeyNotFound()
        {
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(1, 10);

            Assert.Throws<KeyNotFoundException>(() => entity.GetValue(999));
        }

        [Test]
        public void GetValue_ReturnsCorrectObject_ForCustomStruct()
        {
            var entity = new Entity(valueCapacity: 4);
            var data = new SamplePoint {X = 1, Y = 2};
            entity.AddValue(77, data);

            object result = entity.GetValue(77);

            Assert.IsInstanceOf<SamplePoint>(result);
            Assert.AreEqual(data, (SamplePoint) result);
        }

        #endregion

        #region TryGetValue<T>

        [Test]
        public void TryGetTValue_ReturnsTrue_AndOutputsStruct()
        {
            var entity = new Entity();
            entity.AddValue(1, 123);

            bool found = entity.TryGetValue<int>(1, out var result);

            Assert.IsTrue(found);
            Assert.AreEqual(123, result);
        }

        [Test]
        public void TryGetTValue_ReturnsTrue_AndOutputsReference()
        {
            var entity = new Entity();
            entity.AddValue(2, "hello");

            bool found = entity.TryGetValue<string>(2, out var result);

            Assert.IsTrue(found);
            Assert.AreEqual("hello", result);
        }

        [Test]
        public void TryGetTValue_ReturnsFalse_WhenEmpty()
        {
            var entity = new Entity();

            bool found = entity.TryGetValue<int>(1, out var result);

            Assert.IsFalse(found);
            Assert.AreEqual(0, result); // default(int)
        }

        [Test]
        public void TryGetTValue_ReturnsFalse_WhenKeyNotFound()
        {
            var entity = new Entity();
            entity.AddValue(3, 55);

            bool found = entity.TryGetValue<int>(4, out var result);

            Assert.IsFalse(found);
            Assert.AreEqual(0, result); // default(int)
        }

        [Test]
        public void TryGetTValue_Throws_WhenWrongGenericType()
        {
            var entity = new Entity();
            entity.AddValue(5, "text");

            Assert.Throws<InvalidCastException>(() => entity.TryGetValue<int>(5, out _));
        }

        [Test]
        public void TryGetTValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");

            Entity e = new Entity("123");
            e.AddValues(new Dictionary<int, object>
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
        public void TryGetTValue_ValueIsAbsent_ReturnFalse()
        {
            //Arrange:
            var e = new Entity("123");

            //Act:
            bool success = e.TryGetValue(0, out string foo);

            //Assert:
            Assert.IsFalse(success);
            Assert.IsNull(foo);
        }

        #endregion

        #region TryGetValue

        [Test]
        public void TryGetValue_ReturnsTrue_AndBoxedStruct()
        {
            var entity = new Entity();
            entity.AddValue(1, 123);

            bool found = entity.TryGetValue(1, out var result);

            Assert.IsTrue(found);
            Assert.AreEqual(123, result);
            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        public void TryGetValue_ReturnsTrue_AndReference()
        {
            var entity = new Entity();
            entity.AddValue(2, "data");

            bool found = entity.TryGetValue(2, out var result);

            Assert.IsTrue(found);
            Assert.AreEqual("data", result);
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        public void TryGetValue_ReturnsFalse_WhenEmpty()
        {
            var entity = new Entity();

            bool found = entity.TryGetValue(3, out var result);

            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        [Test]
        public void TryGetValue_ReturnsFalse_WhenKeyNotFound()
        {
            var entity = new Entity();
            entity.AddValue(4, 99);

            bool found = entity.TryGetValue(999, out var result);

            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        [Test]
        public void TryGetValue_ReturnsStruct_AsBoxedObject()
        {
            var entity = new Entity();
            var expected = new SampleStruct {A = 1, B = 2};
            entity.AddValue(5, expected);

            bool found = entity.TryGetValue(5, out var result);

            Assert.IsTrue(found);
            Assert.IsInstanceOf<SampleStruct>(result);
            Assert.AreEqual(expected, (SampleStruct) result);
        }

        #endregion

        #region GetValueUnsafe<T>

        [Test]
        public void GetValueUnsafe_ReturnsRef_ToStructValue()
        {
            var entity = new Entity();
            entity.AddValue(1, 42);

            ref int valueRef = ref entity.GetValueUnsafe<int>(1);
            Assert.AreEqual(42, valueRef);
        }

        [Test]
        public void GetValueUnsafe_ReturnsRef_ToReferenceType()
        {
            var entity = new Entity();
            var obj = new SampleText("initial");
            entity.AddValue(2, obj);

            ref SampleText refValue = ref entity.GetValueUnsafe<SampleText>(2);
            Assert.AreSame(obj, refValue);
            Assert.AreEqual("initial", refValue.Text);
        }

        [Test]
        public void GetValueUnsafe_ModifiesStructValue()
        {
            var entity = new Entity();
            entity.AddValue(3, 100);

            ref int valueRef = ref entity.GetValueUnsafe<int>(3);
            valueRef = 999;

            Assert.AreEqual(999, entity.GetValue<int>(3));
        }

        [Test]
        public void GetValueUnsafe_Throws_WhenEmpty()
        {
            var entity = new Entity();
            Assert.Throws<KeyNotFoundException>(() =>
            {
                ref var _ = ref entity.GetValueUnsafe<int>(100);
            });
        }

        [Test]
        public void GetValueUnsafe_Throws_WhenKeyNotFound()
        {
            var entity = new Entity();
            entity.AddValue(5, 777);
            Assert.Throws<KeyNotFoundException>(() =>
            {
                ref var _ = ref entity.GetValueUnsafe<int>(6);
            });
        }

        #endregion

        #region GetValueUnsafe

        [Test]
        public void TryGetValueUnsafe_ReturnsTrue_AndStruct()
        {
            var entity = new Entity();
            entity.AddValue(1, 999);

            bool result = entity.TryGetValueUnsafe<int>(1, out int value);

            Assert.IsTrue(result);
            Assert.AreEqual(999, value);
        }

        [Test]
        public void TryGetValueUnsafe_ReturnsTrue_WhenStructStoredAsBoxed()
        {
            var entity = new Entity();
            entity.SetValue(2, 123); // SetValue also accepts struct

            bool result = entity.TryGetValueUnsafe<int>(2, out int value);

            Assert.IsTrue(result);
            Assert.AreEqual(123, value);
        }

        [Test]
        public void TryGetValueUnsafe_ReturnsFalse_WhenEmpty()
        {
            var entity = new Entity();

            bool result = entity.TryGetValueUnsafe(10, out int value);

            Assert.IsFalse(result);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TryGetValueUnsafe_ReturnsFalse_WhenKeyNotFound()
        {
            var entity = new Entity();
            entity.AddValue(5, 500);

            bool result = entity.TryGetValueUnsafe(99, out int value);

            Assert.IsFalse(result);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TryGetValueUnsafe_ReturnsCustomStructCorrectly()
        {
            var entity = new Entity();
            var expected = new SampleStruct {A = 1, B = 2};
            entity.AddValue(3, expected);

            bool result = entity.TryGetValueUnsafe<SampleStruct>(3, out var value);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, value);
        }

        #endregion

        #region HasValue

        [Test]
        public void HasValue_ReturnsTrue_IfKeyExists()
        {
            var entity = new Entity();
            entity.AddValue(1, 123);

            Assert.IsTrue(entity.HasValue(1));
        }

        [Test]
        public void HasValue_ReturnsFalse_IfKeyNotExists()
        {
            var entity = new Entity();
            entity.AddValue(1, "hello");

            Assert.IsFalse(entity.HasValue(2));
        }

        [Test]
        public void HasValue_ReturnsTrue_ForMultipleKeys()
        {
            var entity = new Entity();
            entity.AddValue(10, "A");
            entity.AddValue(20, "B");

            Assert.IsTrue(entity.HasValue(10));
            Assert.IsTrue(entity.HasValue(20));
        }

        [Test]
        public void HasValue_ReturnsFalse_AfterDeletion()
        {
            var entity = new Entity();
            entity.AddValue(99, 5);
            entity.DelValue(99);

            Assert.IsFalse(entity.HasValue(99));
        }

        [Test]
        public void HasValue_ReturnsFalse_IfEntityIsEmpty()
        {
            var entity = new Entity();

            Assert.IsFalse(entity.HasValue(42));
        }

        #endregion

        #region AddValue

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

            Entity e = new Entity("123");
            e.AddValue(key, foo1);

            //Act:
            Assert.Throws<ArgumentException>(() => e.AddValue(key, foo2));

            //Assert:
            Assert.AreEqual(foo1, e.GetValue<string>(key));
            Assert.AreNotEqual(foo2, e.GetValue<string>(key));
        }

        #endregion

        #region AddValue<T>

        [Test]
        public void AddValue_AddsStructSuccessfully()
        {
            var entity = new Entity();
            entity.AddValue(1, 42);

            int value = entity.GetValue<int>(1);
            Assert.AreEqual(42, value);
        }

        [Test]
        public void AddValue_ThrowsIfKeyAlreadyExists()
        {
            var entity = new Entity();
            entity.AddValue(1, 100);

            var ex = Assert.Throws<ArgumentException>(() => entity.AddValue(1, 200));
            Assert.IsTrue(ex.Message.Contains("already has been added"));
        }

        [Test]
        public void AddValue_CallsOnValueAddedEvent()
        {
            var entity = new Entity();

            int? capturedKey = null;
            entity.OnValueAdded += (e, key) => capturedKey = key;

            entity.AddValue(5, 999);

            Assert.AreEqual(5, capturedKey);
        }

        [Test]
        public void AddValue_ValueCanBeFoundInGetValues()
        {
            var entity = new Entity();
            entity.AddValue(10, 777);

            var values = entity.GetValues();
            Assert.AreEqual(1, values.Length);
            Assert.AreEqual(10, values[0].Key);
            Assert.AreEqual(777, values[0].Value);
        }

        [Test]
        public void AddValue_UpdatesValueCount()
        {
            var entity = new Entity();
            entity.AddValue(1, 5);
            entity.AddValue(2, 10);

            Assert.AreEqual(2, entity.ValueCount);
        }

        #endregion

        #region DelValue

        [Test]
        public void DelValue_ReturnsTrue_WhenKeyExists()
        {
            var entity = new Entity();
            entity.AddValue(1, 123);

            bool result = entity.DelValue(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void DelValue_RemovesValueCompletely()
        {
            var entity = new Entity();
            entity.AddValue(2, 456);
            entity.DelValue(2);

            Assert.IsFalse(entity.HasValue(2));
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue<int>(2));
        }

        [Test]
        public void DelValue_ReturnsFalse_WhenKeyDoesNotExist()
        {
            var entity = new Entity();

            bool result = entity.DelValue(99);

            Assert.IsFalse(result);
        }

        [Test]
        public void DelValue_InvokesOnValueDeletedEvent()
        {
            var entity = new Entity();
            entity.AddValue(5, 777);

            int? deletedKey = null;
            entity.OnValueDeleted += (e, key) => deletedKey = key;

            entity.DelValue(5);

            Assert.AreEqual(5, deletedKey);
        }

        [Test]
        public void DelValue_DecreasesValueCount()
        {
            var entity = new Entity();
            entity.AddValue(1, 10);
            entity.AddValue(2, 20);

            entity.DelValue(1);

            Assert.AreEqual(1, entity.ValueCount);
        }

        [Test]
        public void DelValue()
        {
            //Arrange:
            const int key1 = 1;
            const int key2 = 2;

            object foo1 = new object();
            object foo2 = new object();


            Entity e = new Entity("123");
            e.AddValues(new Dictionary<int, object>
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
        public void DelValue_ValueIsAbsent_ReturnFalse()
        {
            //Arrange:
            const int key1 = 1;

            Entity e = new Entity("123");

            //Act:
            bool success = e.DelValue(key1);

            //Assert:
            Assert.IsFalse(success);
        }

        #endregion

        #region SetValue

        [Test]
        public void SetValue_AddsValue_IfKeyDoesNotExist()
        {
            var entity = new Entity();
            entity.SetValue(1, "Hello");

            var result = entity.GetValue(1);
            Assert.AreEqual("Hello", result);
        }

        [Test]
        public void SetValue_UpdatesExistingValue()
        {
            var entity = new Entity();
            entity.SetValue(1, "Old");
            entity.SetValue(1, "New");

            var result = entity.GetValue(1);
            Assert.AreEqual("New", result);
        }

        [Test]
        public void SetValue_SameValue_DoesNotTriggerChange()
        {
            var entity = new Entity();
            entity.SetValue(1, "Same");

            bool called = false;
            entity.OnValueChanged += (_, _) => called = true;

            entity.SetValue(1, "Same");

            Assert.IsFalse(called);
        }

        [Test]
        public void SetValue_TriggersOnValueChanged_IfDifferent()
        {
            var entity = new Entity();
            entity.SetValue(1, "A");

            int? changedKey = null;
            entity.OnValueChanged += (_, key) => changedKey = key;

            entity.SetValue(1, "B");

            Assert.AreEqual(1, changedKey);
        }

        [Test]
        public void SetValue_ThrowsIfNull()
        {
            var entity = new Entity();
            Assert.Throws<ArgumentNullException>(() => entity.SetValue(1, null));
        }

        #endregion

        #region SetValue<T>

        [Test]
        public void SetValue_AddsStructValue_IfNotExists()
        {
            var entity = new Entity();
            entity.SetValue(100, 42);

            int result = entity.GetValue<int>(100);
            Assert.AreEqual(42, result);
        }

        [Test]
        public void SetValue_UpdatesValue_SameType()
        {
            var entity = new Entity();
            entity.SetValue(1, 5);
            entity.SetValue(1, 99);

            Assert.AreEqual(99, entity.GetValue<int>(1));
        }

        [Test]
        public void SetValue_UpdatesValue_DifferentType()
        {
            var entity = new Entity();
            entity.SetValue(1, 5); // int
            entity.SetValue(1, 5f); // float

            Assert.AreEqual(5f, entity.GetValue<float>(1));
        }

        [Test]
        public void SetValue_ReplacesReferenceWithStruct()
        {
            var entity = new Entity();
            entity.SetValue(1, "hello"); // reference type
            entity.SetValue(1, 123); // struct

            int result = entity.GetValue<int>(1);
            Assert.AreEqual(123, result);
        }

        [Test]
        public void SetValue_InvokesOnValueChanged()
        {
            var entity = new Entity();
            int? changedKey = null;

            entity.OnValueChanged += (_, key) => changedKey = key;
            entity.SetValue(777, 456);

            Assert.AreEqual(777, changedKey);
        }

        [Test]
        public void SetValue_PreviousValueIsNotExists_ValueAdded()
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
        public void SetValue_OverridesAddedValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");
            string foo2 = new string("Foo2");

            var wasAddEvent = false;
            var changedKey = -1;

            Entity e = new Entity("123");
            e.AddValue(key, foo);

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

        #endregion

        #region ClearValues

        [Test]
        public void ClearValues_RemovesAllEntries()
        {
            var entity = new Entity();
            entity.SetValue(1, 10);
            entity.SetValue(2, "hello");

            entity.ClearValues();

            Assert.AreEqual(0, entity.ValueCount);
            Assert.IsFalse(entity.HasValue(1));
            Assert.IsFalse(entity.HasValue(2));
        }

        [Test]
        public void ClearValues_InvokesOnValueDeleted_ForEachKey()
        {
            var entity = new Entity();
            var deleted = new List<int>();
            entity.OnValueDeleted += (_, key) => deleted.Add(key);

            entity.SetValue(10, "a");
            entity.SetValue(20, "b");

            entity.ClearValues();

            CollectionAssert.AreEquivalent(new[] {10, 20}, deleted);
        }

        [Test]
        public void ClearValues_InvokesOnStateChanged()
        {
            var entity = new Entity();
            bool changed = false;
            entity.OnStateChanged += _ => changed = true;

            entity.SetValue(1, 123);
            entity.ClearValues();

            Assert.IsTrue(changed);
        }

        [Test]
        public void ClearValues_WhenEmpty_DoesNothing()
        {
            var entity = new Entity();

            // Should not throw
            Assert.DoesNotThrow(() => entity.ClearValues());
        }

        [Test]
        public void ClearValues_ValueCountIsZero()
        {
            var entity = new Entity();
            entity.SetValue(1, 100);
            entity.SetValue(2, 200);
            entity.ClearValues();

            Assert.AreEqual(0, entity.ValueCount);
        }

        [Test]
        public void ClearValues()
        {
            //Arrange:
            const int key1 = 1;
            const int key2 = 2;

            object foo1 = new object();
            object foo2 = new object();

            Entity e = new Entity("123");
            e.AddValues(new Dictionary<int, object>
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

        #endregion

        #region GetValues

        [Test]
        public void GetValues_ReturnsAllKeyValuePairs()
        {
            var entity = new Entity();
            entity.SetValue(1, 100);
            entity.SetValue(2, "hello");
            entity.SetValue(3, true);

            var result = entity.GetValues();

            CollectionAssert.AreEquivalent(
                new[]
                {
                    new KeyValuePair<int, object>(1, 100),
                    new KeyValuePair<int, object>(2, "hello"),
                    new KeyValuePair<int, object>(3, true),
                },
                result);
        }

        [Test]
        public void GetValues_ReturnsEmptyArray_WhenNoValues()
        {
            var entity = new Entity();

            var result = entity.GetValues();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetValues_ReturnsCorrectCount()
        {
            var entity = new Entity();
            entity.SetValue(1, 42);
            entity.SetValue(2, 84);

            var result = entity.GetValues();

            Assert.AreEqual(2, result.Length);
        }

        [Test]
        public void GetValues_DoesNotThrowWithMixedTypes()
        {
            var entity = new Entity();
            entity.SetValue(1, 123);
            entity.SetValue(2, "test");
            entity.SetValue(3, 3.14f);

            Assert.DoesNotThrow(() =>
            {
                var values = entity.GetValues();
                Assert.AreEqual(3, values.Length);
            });
        }

        [Test]
        public void GetValues_ReturnsStructValueAddedByAddValue()
        {
            var entity = new Entity();
            var value = new SampleStruct {A = 1, B = 2};

            entity.AddValue(1, value);

            var result = entity.GetValues();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(1, result[0].Key);
            Assert.AreEqual(value, result[0].Value);
        }

        [Test]
        public void GetValues_ReturnsStructValueAddedBySetValue()
        {
            var entity = new Entity();
            var value = new SampleStruct {A = 3, B = 1};

            entity.SetValue(5, value);

            var values = entity.GetValues();
            Assert.AreEqual(1, values.Length);
            Assert.AreEqual(5, values[0].Key);
            Assert.AreEqual(value, values[0].Value);
        }

        [Test]
        public void GetValues_StructValueIsBoxedCorrectly()
        {
            var entity = new Entity();
            var value = new SampleStruct {A = 10, B = 3};

            entity.AddValue(2, value);

            object boxed = entity.GetValue(2);

            Assert.IsInstanceOf<SampleStruct>(boxed);
            Assert.AreEqual(value, (SampleStruct) boxed);
        }

        [Test]
        public void GetValues_StructValueCanBeUnboxedSafely()
        {
            var entity = new Entity();
            var value = new SampleStruct {A = 7, B = 8};

            entity.AddValue(4, value);

            var allValues = entity.GetValues();

            var unboxed = (SampleStruct) allValues[0].Value;

            Assert.AreEqual(value, unboxed);
        }

        #endregion

        #region CopyValues

        [Test]
        public void CopyValues_SingleStruct_WritesCorrectly()
        {
            var entity = new Entity();
            entity.AddValue(1, 42);

            var buffer = new KeyValuePair<int, object>[1];
            int copied = entity.CopyValues(buffer);

            Assert.AreEqual(1, copied);
            Assert.AreEqual(1, buffer[0].Key);
            Assert.AreEqual(42, buffer[0].Value);
        }

        [Test]
        public void CopyValues_SingleReference_WritesCorrectly()
        {
            var entity = new Entity();
            entity.AddValue(5, "hello");

            var buffer = new KeyValuePair<int, object>[1];
            int copied = entity.CopyValues(buffer);

            Assert.AreEqual(1, copied);
            Assert.AreEqual(5, buffer[0].Key);
            Assert.AreEqual("hello", buffer[0].Value);
        }

        [Test]
        public void CopyValues_MultipleEntries_CorrectCountAndValues()
        {
            var entity = new Entity();
            entity.AddValue(1, 1.5f);
            entity.AddValue(2, "test");
            entity.AddValue(3, true);

            var buffer = new KeyValuePair<int, object>[3];
            int copied = entity.CopyValues(buffer);

            Assert.AreEqual(3, copied);
            CollectionAssert.AreEquivalent(
                new[]
                {
                    new KeyValuePair<int, object>(1, 1.5f),
                    new KeyValuePair<int, object>(2, "test"),
                    new KeyValuePair<int, object>(3, true),
                },
                buffer
            );
        }

        [Test]
        public void CopyValues_IgnoresDeletedSlots()
        {
            var entity = new Entity();
            entity.AddValue(1, 10);
            entity.AddValue(2, 20);
            entity.DelValue(1);

            var buffer = new KeyValuePair<int, object>[2];
            int copied = entity.CopyValues(buffer);

            Assert.AreEqual(1, copied);
            Assert.AreEqual(2, buffer[0].Key);
            Assert.AreEqual(20, buffer[0].Value);
        }

        [Test]
        public void CopyValues_NullBuffer_ThrowsArgumentNullException()
        {
            var entity = new Entity();
            Assert.Throws<ArgumentNullException>(() => { entity.CopyValues(null); });
        }

        #endregion

        #region GetValueEnumerator

        [Test]
        public void GetValueEnumerator_ReturnsAllAddedValues()
        {
            var entity = new Entity();
            entity.AddValue(1, "one");
            entity.AddValue(2, 2);
            entity.AddValue(3, true);

            var results = new List<KeyValuePair<int, object>>();
            
            Entity.ValueEnumerator valueEnumerator = entity.GetValueEnumerator();
            while (valueEnumerator.MoveNext()) 
                results.Add(valueEnumerator.Current);

            CollectionAssert.AreEquivalent(new[]
            {
                new KeyValuePair<int, object>(1, "one"),
                new KeyValuePair<int, object>(2, 2),
                new KeyValuePair<int, object>(3, true),
            }, results);
        }

        [Test]
        public void GetValueEnumerator_IgnoresDeletedValues()
        {
            var entity = new Entity();
            entity.AddValue(1, "alive");
            entity.AddValue(2, "dead");
            entity.DelValue(2);

            var results = new List<KeyValuePair<int, object>>();
            Entity.ValueEnumerator valueEnumerator = entity.GetValueEnumerator();
            while (valueEnumerator.MoveNext()) 
                results.Add(valueEnumerator.Current);
            
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(1, results[0].Key);
            Assert.AreEqual("alive", results[0].Value);
        }

        [Test]
        public void GetValueEnumerator_EmptyEntity_ReturnsNothing()
        {
            var entity = new Entity();
            var enumerator = ((IEntity) entity).GetValueEnumerator();

            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void GetValueEnumerator_MoveNext_CyclesCorrectly()
        {
            var entity = new Entity();
            entity.AddValue(10, 99);

            var enumerator = ((IEntity) entity).GetValueEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(10, enumerator.Current.Key);
            Assert.AreEqual(99, enumerator.Current.Value);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void GetValueEnumerator_Reset_RewindsToStart()
        {
            var entity = new Entity();
            entity.AddValue(1, "reset");

            var enumerator = entity.GetValueEnumerator();
            Assert.IsTrue(enumerator.MoveNext());

            enumerator.Reset();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(1, enumerator.Current.Key);
            Assert.AreEqual("reset", enumerator.Current.Value);
        }

        #endregion
    }
}