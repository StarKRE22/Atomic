using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        // ── Test helpers ──────────────────────────────────────────

        private sealed class DisposableValue : IDisposable
        {
            public bool IsDisposed { get; private set; }
            public void Dispose() => IsDisposed = true;
        }

        private sealed class DisposableValueA : IDisposable
        {
            public bool IsDisposed { get; private set; }
            public void Dispose() => IsDisposed = true;
        }

        private sealed class DisposableValueB : IDisposable
        {
            public bool IsDisposed { get; private set; }
            public void Dispose() => IsDisposed = true;
        }

        // ══════════════════════════════════════════════════════════
        //  AddValue extensions
        // ══════════════════════════════════════════════════════════

        #region AddValue(string, object)

        [Test]
        public void AddValue_ByString_AddsValueToEntity()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            entity.AddValue("health", 100);

            //Assert:
            Assert.IsTrue(entity.HasValue("health"));
            Assert.AreEqual(100, entity.GetValue<int>("health"));
        }

        [Test]
        public void AddValue_ByString_CorrectIdIsUsed()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int expectedId = EntityKeyStore.NameToId("mana");

            //Act:
            entity.AddValue("mana", 50);

            //Assert:
            Assert.IsTrue(entity.HasValue(expectedId));
            Assert.AreEqual(50, entity.GetValue<int>(expectedId));
        }

        [Test]
        public void AddValue_ByString_StoresReferenceType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            entity.AddValue("name", "Alice");

            //Assert:
            Assert.AreEqual("Alice", entity.GetValue<string>("name"));
        }

        [Test]
        public void AddValue_ByString_StoresStructType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var point = new TestPoint { X = 3, Y = 7 };

            //Act:
            entity.AddValue("position", point);

            //Assert:
            Assert.AreEqual(point, entity.GetValue("position"));
        }

        [Test]
        public void AddValue_ByString_FiresOnValueAddedEvent()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int? eventKey = null;
            object eventValue = null;
            entity.OnValueAdded += (_, k, v) => { eventKey = k; eventValue = v; };

            //Act:
            entity.AddValue("speed", 9.8f);

            //Assert:
            Assert.AreEqual(EntityKeyStore.NameToId("speed"), eventKey);
            Assert.AreEqual(9.8f, eventValue);
        }

        [Test]
        public void AddValue_ByString_IncrementsValueCount()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            entity.AddValue("a", 1);
            entity.AddValue("b", 2);

            //Assert:
            Assert.AreEqual(2, entity.ValueCount);
        }

        [Test]
        public void AddValue_ByString_ThrowsArgumentException_WhenKeyAlreadyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("health", 100);

            //Act & Assert:
            var ex = Assert.Throws<ArgumentException>(() => entity.AddValue("health", 200));
            StringAssert.Contains("already has been added", ex.Message);
        }

        [Test]
        public void AddValue_ByString_ThrowsArgumentNullException_WhenValueIsNull()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() => entity.AddValue("key", null));
        }

        #endregion

        #region AddValue<T>(ValueKey<T>, T)

        [Test]
        public void AddValue_ByValueKey_AddsValueToEntity()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");

            //Act:
            entity.AddValue(key, 100);

            //Assert:
            Assert.IsTrue(entity.HasValue(key));
            Assert.AreEqual(100, entity.GetValue(key));
        }

        [Test]
        public void AddValue_ByValueKey_UsesKeyCorrectId()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("playerName");
            int expectedId = EntityKeyStore.NameToId("playerName");

            //Act:
            entity.AddValue(key, "Hero");

            //Assert:
            Assert.IsTrue(entity.HasValue(expectedId));
            Assert.AreEqual("Hero", entity.GetValue<string>(expectedId));
        }

        [Test]
        public void AddValue_ByValueKey_StoresDifferentTypes()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var intKey = new ValueKey<int>("score");
            var floatKey = new ValueKey<float>("speed");
            var stringKey = new ValueKey<string>("name");

            //Act:
            entity.AddValue(intKey, 42);
            entity.AddValue(floatKey, 3.14f);
            entity.AddValue(stringKey, "test");

            //Assert:
            Assert.AreEqual(42, entity.GetValue(intKey));
            Assert.AreEqual(3.14f, entity.GetValue(floatKey));
            Assert.AreEqual("test", entity.GetValue(stringKey));
        }

        [Test]
        public void AddValue_ByValueKey_FiresOnValueAddedEvent()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("gold");
            int? eventKey = null;
            entity.OnValueAdded += (_, k, _) => eventKey = k;

            //Act:
            entity.AddValue(key, 999);

            //Assert:
            Assert.AreEqual(key.Id, eventKey);
        }

        [Test]
        public void AddValue_ByValueKey_ThrowsArgumentException_WhenDuplicateKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");
            entity.AddValue(key, 50);

            //Act & Assert:
            Assert.Throws<ArgumentException>(() => entity.AddValue(key, 100));
        }

        [Test]
        public void AddValue_ByValueKey_ThrowsArgumentNullException_WhenValueIsNull()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("data");

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() => entity.AddValue(key, null));
        }

        #endregion

        #region AddValue<E,T>(ValueKey<E,T>, T)

        [Test]
        public void AddValue_ByEntityValueKey_AddsValueToEntity()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("mana");

            //Act:
            entity.AddValue(key, 75);

            //Assert:
            Assert.AreEqual(75, entity.GetValue(key));
        }

        [Test]
        public void AddValue_ByEntityValueKey_UsesKeyCorrectId()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, string>("className");
            int expectedId = EntityKeyStore.NameToId("className");

            //Act:
            entity.AddValue(key, "Warrior");

            //Assert:
            Assert.AreEqual("Warrior", entity.GetValue<string>(expectedId));
        }

        [Test]
        public void AddValue_ByEntityValueKey_FiresOnValueAddedEvent()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("level");
            int? eventKey = null;
            entity.OnValueAdded += (_, k, _) => eventKey = k;

            //Act:
            entity.AddValue(key, 10);

            //Assert:
            Assert.AreEqual(key.Id, eventKey);
        }

        [Test]
        public void AddValue_ByEntityValueKey_ThrowsArgumentException_WhenDuplicateKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("mana");
            entity.AddValue(key, 10);

            //Act & Assert:
            Assert.Throws<ArgumentException>(() => entity.AddValue(key, 20));
        }

        [Test]
        public void AddValue_ByEntityValueKey_StoresStruct()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, TestPoint>("pos");
            var point = new TestPoint { X = 5, Y = 10 };

            //Act:
            entity.AddValue(key, point);

            //Assert:
            Assert.AreEqual(point, entity.GetValue(key));
        }

        #endregion

        #region AddValue(string, object, out int id)

        [Test]
        public void AddValue_ByString_OutId_ReturnsCorrectId()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int expectedId = EntityKeyStore.NameToId("strength");

            //Act:
            entity.AddValue("strength", 50, out int id);

            //Assert:
            Assert.AreEqual(expectedId, id);
        }

        [Test]
        public void AddValue_ByString_OutId_StoresValueWithReturnedId()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            entity.AddValue("defense", 30, out int id);

            //Assert:
            Assert.IsTrue(entity.HasValue(id));
            Assert.AreEqual(30, entity.GetValue<int>(id));
        }

        [Test]
        public void AddValue_ByString_OutId_FiresOnValueAddedEvent()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int? eventKey = null;
            entity.OnValueAdded += (_, k, _) => eventKey = k;

            //Act:
            entity.AddValue("agility", 20, out int id);

            //Assert:
            Assert.AreEqual(id, eventKey);
        }

        [Test]
        public void AddValue_ByString_OutId_ConsistentWithDirectNameToId()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            entity.AddValue("luck", 7, out int id);

            //Assert:
            Assert.AreEqual(EntityKeyStore.NameToId("luck"), id);
        }

        [Test]
        public void AddValue_ByString_OutId_CanBeUsedToRetrieve()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            entity.AddValue("wisdom", 88, out int id);

            //Assert:
            Assert.AreEqual(88, entity.GetValue<int>(id));
        }

        #endregion

        // ══════════════════════════════════════════════════════════
        //  AddValues extensions
        // ══════════════════════════════════════════════════════════

        #region AddValues(IEnumerable<KeyValuePair<int, object>>)

        [Test]
        public void AddValues_ByIntKeyPairs_AddsAllValues()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var values = new Dictionary<int, object>
            {
                { 1, 100 },
                { 2, "hello" },
                { 3, 3.14f }
            };

            //Act:
            entity.AddValues((IEnumerable<KeyValuePair<int, object>>)values);

            //Assert:
            Assert.AreEqual(3, entity.ValueCount);
            Assert.AreEqual(100, entity.GetValue<int>(1));
            Assert.AreEqual("hello", entity.GetValue<string>(2));
            Assert.AreEqual(3.14f, entity.GetValue<float>(3));
        }

        [Test]
        public void AddValues_ByIntKeyPairs_NullCollection_DoesNotThrow()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act & Assert:
            Assert.DoesNotThrow(() => entity.AddValues((IEnumerable<KeyValuePair<int, object>>)null));
        }

        [Test]
        public void AddValues_ByIntKeyPairs_EmptyCollection_DoesNotThrow()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var values = Array.Empty<KeyValuePair<int, object>>();

            //Act:
            entity.AddValues((IEnumerable<KeyValuePair<int, object>>)values);

            //Assert:
            Assert.AreEqual(0, entity.ValueCount);
        }

        [Test]
        public void AddValues_ByIntKeyPairs_ThrowsArgumentException_WhenDuplicateKeys()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var values = new[]
            {
                new KeyValuePair<int, object>(1, "a"),
                new KeyValuePair<int, object>(1, "b")
            };

            //Act & Assert:
            Assert.Throws<ArgumentException>(() =>
                entity.AddValues((IEnumerable<KeyValuePair<int, object>>)values));
        }

        #endregion

        #region AddValues(IEnumerable<KeyValuePair<string, object>>)

        [Test]
        public void AddValues_ByStringKeyPairs_AddsAllValues()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var values = new Dictionary<string, object>
            {
                { "health", 100 },
                { "name", "Hero" },
                { "speed", 5.5f }
            };

            //Act:
            entity.AddValues((IEnumerable<KeyValuePair<string, object>>)values);

            //Assert:
            Assert.AreEqual(3, entity.ValueCount);
            Assert.AreEqual(100, entity.GetValue<int>("health"));
            Assert.AreEqual("Hero", entity.GetValue<string>("name"));
            Assert.AreEqual(5.5f, entity.GetValue<float>("speed"));
        }

        [Test]
        public void AddValues_ByStringKeyPairs_UsesStringToIdMapping()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int expectedId = EntityKeyStore.NameToId("gold");
            var values = new Dictionary<string, object>
            {
                { "gold", 500 }
            };

            //Act:
            entity.AddValues((IEnumerable<KeyValuePair<string, object>>)values);

            //Assert:
            Assert.IsTrue(entity.HasValue(expectedId));
            Assert.AreEqual(500, entity.GetValue<int>(expectedId));
        }

        [Test]
        public void AddValues_ByStringKeyPairs_NullCollection_DoesNotThrow()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act & Assert:
            Assert.DoesNotThrow(() => entity.AddValues((IEnumerable<KeyValuePair<string, object>>)null));
        }

        [Test]
        public void AddValues_ByStringKeyPairs_EmptyCollection_DoesNotThrow()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var values = Array.Empty<KeyValuePair<string, object>>();

            //Act:
            entity.AddValues((IEnumerable<KeyValuePair<string, object>>)values);

            //Assert:
            Assert.AreEqual(0, entity.ValueCount);
        }

        [Test]
        public void AddValues_ByStringKeyPairs_FiresOnValueAddedForEach()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var addedKeys = new List<int>();
            entity.OnValueAdded += (_, k, _) => addedKeys.Add(k);

            var values = new Dictionary<string, object>
            {
                { "a", 1 },
                { "b", 2 }
            };

            //Act:
            entity.AddValues((IEnumerable<KeyValuePair<string, object>>)values);

            //Assert:
            Assert.AreEqual(2, addedKeys.Count);
            Assert.IsTrue(addedKeys.Contains(EntityKeyStore.NameToId("a")));
            Assert.IsTrue(addedKeys.Contains(EntityKeyStore.NameToId("b")));
        }

        [Test]
        public void AddValues_ByStringKeyPairs_ThrowsArgumentException_WhenDuplicateStringKeys()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            // Note: Dictionary<string, object> won't allow duplicate keys,
            // but a raw list could. Using a HashSet<KeyValuePair> won't help
            // either. We'll test with the same name mapped via two entries.
            var values = new List<KeyValuePair<string, object>>
            {
                new("x", 1),
            };
            // First add the value, then try to add again through AddValues
            entity.AddValue("x", 1);

            var duplicateValues = new List<KeyValuePair<string, object>>
            {
                new("x", 2)
            };

            //Act & Assert:
            Assert.Throws<ArgumentException>(() =>
                entity.AddValues((IEnumerable<KeyValuePair<string, object>>)duplicateValues));
        }

        #endregion

        // ══════════════════════════════════════════════════════════
        //  DelValue extensions
        // ══════════════════════════════════════════════════════════

        #region DelValue(string)

        [Test]
        public void DelValue_ByString_RemovesValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("health", 100);

            //Act:
            bool result = entity.DelValue("health");

            //Assert:
            Assert.IsTrue(result);
            Assert.IsFalse(entity.HasValue("health"));
        }

        [Test]
        public void DelValue_ByString_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("mana", 50);

            //Act:
            bool result = entity.DelValue("mana");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void DelValue_ByString_ReturnsFalse_WhenKeyDoesNotExist()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            bool result = entity.DelValue("nonexistent");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void DelValue_ByString_FiresOnValueDeletedEvent()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("speed", 9.8f);
            int? deletedKey = null;
            object deletedValue = null;
            entity.OnValueDeleted += (_, k, v) => { deletedKey = k; deletedValue = v; };

            //Act:
            entity.DelValue("speed");

            //Assert:
            Assert.AreEqual(EntityKeyStore.NameToId("speed"), deletedKey);
            Assert.AreEqual(9.8f, deletedValue);
        }

        [Test]
        public void DelValue_ByString_DecreasesValueCount()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("a", 1);
            entity.AddValue("b", 2);

            //Act:
            entity.DelValue("a");

            //Assert:
            Assert.AreEqual(1, entity.ValueCount);
        }

        [Test]
        public void DelValue_ByString_ThrowsKeyNotFoundException_OnGetValueAfterDelete()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("data", 42);
            entity.DelValue("data");

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue("data"));
        }

        [Test]
        public void DelValue_ByString_DoesNotAffectOtherKeys()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("a", 1);
            entity.AddValue("b", 2);

            //Act:
            entity.DelValue("a");

            //Assert:
            Assert.AreEqual(2, entity.GetValue<int>("b"));
        }

        #endregion

        #region DelValue<T>(ValueKey<T>)

        [Test]
        public void DelValue_ByValueKey_RemovesValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");
            entity.AddValue(key, 100);

            //Act:
            bool result = entity.DelValue(key);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsFalse(entity.HasValue(key));
        }

        [Test]
        public void DelValue_ByValueKey_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("name");
            entity.AddValue(key, "Hero");

            //Act:
            bool result = entity.DelValue(key);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void DelValue_ByValueKey_ReturnsFalse_WhenKeyDoesNotExist()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("missing");

            //Act:
            bool result = entity.DelValue(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void DelValue_ByValueKey_FiresOnValueDeletedEvent()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<float>("speed");
            entity.AddValue(key, 5.5f);
            int? deletedKey = null;
            entity.OnValueDeleted += (_, k, _) => deletedKey = k;

            //Act:
            entity.DelValue(key);

            //Assert:
            Assert.AreEqual(key.Id, deletedKey);
        }

        [Test]
        public void DelValue_ByValueKey_ThrowsKeyNotFoundException_OnGetValueAfterDelete()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("data");
            entity.AddValue(key, 42);
            entity.DelValue(key);

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue(key));
        }

        [Test]
        public void DelValue_ByValueKey_DecreasesValueCount()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key1 = new ValueKey<int>("a");
            var key2 = new ValueKey<int>("b");
            entity.AddValue(key1, 1);
            entity.AddValue(key2, 2);

            //Act:
            entity.DelValue(key1);

            //Assert:
            Assert.AreEqual(1, entity.ValueCount);
        }

        #endregion

        #region DelValue<E,T>(ValueKey<E,T>)

        [Test]
        public void DelValue_ByEntityValueKey_RemovesValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("mana");
            entity.AddValue(key, 75);

            //Act:
            bool result = entity.DelValue(key);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsFalse(entity.HasValue(key));
        }

        [Test]
        public void DelValue_ByEntityValueKey_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, string>("className");
            entity.AddValue(key, "Mage");

            //Act:
            bool result = entity.DelValue(key);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void DelValue_ByEntityValueKey_ReturnsFalse_WhenKeyDoesNotExist()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("nonexistent");

            //Act:
            bool result = entity.DelValue(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void DelValue_ByEntityValueKey_FiresOnValueDeletedEvent()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, float>("speed");
            entity.AddValue(key, 3.3f);
            int? deletedKey = null;
            entity.OnValueDeleted += (_, k, _) => deletedKey = k;

            //Act:
            entity.DelValue(key);

            //Assert:
            Assert.AreEqual(key.Id, deletedKey);
        }

        [Test]
        public void DelValue_ByEntityValueKey_DecreasesValueCount()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key1 = new ValueKey<Entity, int>("a");
            var key2 = new ValueKey<Entity, int>("b");
            entity.AddValue(key1, 1);
            entity.AddValue(key2, 2);

            //Act:
            entity.DelValue(key1);

            //Assert:
            Assert.AreEqual(1, entity.ValueCount);
        }

        #endregion

        // ══════════════════════════════════════════════════════════
        //  GetValue extensions
        // ══════════════════════════════════════════════════════════

        #region GetValue<T>(string)

        [Test]
        public void GetValue_ByStringTyped_ReturnsStructValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("health", 100);

            //Act:
            int result = entity.GetValue<int>("health");

            //Assert:
            Assert.AreEqual(100, result);
        }

        [Test]
        public void GetValue_ByStringTyped_ReturnsReferenceValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("name", "Alice");

            //Act:
            string result = entity.GetValue<string>("name");

            //Assert:
            Assert.AreEqual("Alice", result);
        }

        [Test]
        public void GetValue_ByStringTyped_ReturnsCorrectType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("speed", 3.14f);

            //Act:
            float result = entity.GetValue<float>("speed");

            //Assert:
            Assert.AreEqual(3.14f, result);
        }

        [Test]
        public void GetValue_ByStringTyped_ThrowsKeyNotFoundException_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue<int>("missing"));
        }

        [Test]
        public void GetValue_ByStringTyped_ThrowsInvalidCastException_WhenTypeMismatch()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("data", "text");

            //Act & Assert:
            Assert.Throws<InvalidCastException>(() => entity.GetValue<int>("data"));
        }

        [Test]
        public void GetValue_ByStringTyped_AfterDelete_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("key", 42);
            entity.DelValue("key");

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue<int>("key"));
        }

        #endregion

        #region GetValue(string) — object overload

        [Test]
        public void GetValue_ByStringObject_ReturnsBoxedStruct()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("health", 100);

            //Act:
            object result = entity.GetValue("health");

            //Assert:
            Assert.AreEqual(100, result);
            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        public void GetValue_ByStringObject_ReturnsReferenceType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("name", "Bob");

            //Act:
            object result = entity.GetValue("name");

            //Assert:
            Assert.AreEqual("Bob", result);
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        public void GetValue_ByStringObject_ThrowsKeyNotFoundException_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue("missing"));
        }

        [Test]
        public void GetValue_ByStringObject_ReturnsCustomStruct()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var point = new TestPoint { X = 10, Y = 20 };
            entity.AddValue("pos", point);

            //Act:
            object result = entity.GetValue("pos");

            //Assert:
            Assert.IsInstanceOf<TestPoint>(result);
            Assert.AreEqual(point, (TestPoint)result);
        }

        [Test]
        public void GetValue_ByStringObject_UsesCorrectIdMapping()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int id = EntityKeyStore.NameToId("score");
            entity.AddValue(id, 999);

            //Act:
            object result = entity.GetValue("score");

            //Assert:
            Assert.AreEqual(999, result);
        }

        #endregion

        #region GetValue<T>(ValueKey<T>)

        [Test]
        public void GetValue_ByValueKey_ReturnsStructValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");
            entity.AddValue(key, 100);

            //Act:
            int result = entity.GetValue(key);

            //Assert:
            Assert.AreEqual(100, result);
        }

        [Test]
        public void GetValue_ByValueKey_ReturnsReferenceValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("name");
            entity.AddValue(key, "Hero");

            //Act:
            string result = entity.GetValue(key);

            //Assert:
            Assert.AreEqual("Hero", result);
        }

        [Test]
        public void GetValue_ByValueKey_ThrowsKeyNotFoundException_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("missing");

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue(key));
        }

        [Test]
        public void GetValue_ByValueKey_ThrowsInvalidCastException_WhenTypeMismatch()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int id = EntityKeyStore.NameToId("data");
            entity.AddValue(id, "text"); // stored as string via int-key overload

            //Act & Assert:
            Assert.Throws<InvalidCastException>(() =>
            {
                var intKey = new ValueKey<int>("data");
                _ = entity.GetValue(intKey);
            });
        }

        [Test]
        public void GetValue_ByValueKey_AfterDelete_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("temp");
            entity.AddValue(key, 42);
            entity.DelValue(key);

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue(key));
        }

        [Test]
        public void GetValue_ByValueKey_ReturnsFloatValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<float>("speed");
            entity.AddValue(key, 7.5f);

            //Act:
            float result = entity.GetValue(key);

            //Assert:
            Assert.AreEqual(7.5f, result);
        }

        #endregion

        #region GetValue<E,T>(ValueKey<E,T>)

        [Test]
        public void GetValue_ByEntityValueKey_ReturnsStructValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("mana");
            entity.AddValue(key, 75);

            //Act:
            int result = entity.GetValue(key);

            //Assert:
            Assert.AreEqual(75, result);
        }

        [Test]
        public void GetValue_ByEntityValueKey_ReturnsReferenceValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, string>("className");
            entity.AddValue(key, "Warrior");

            //Act:
            string result = entity.GetValue(key);

            //Assert:
            Assert.AreEqual("Warrior", result);
        }

        [Test]
        public void GetValue_ByEntityValueKey_ThrowsKeyNotFoundException_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("missing");

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue(key));
        }

        [Test]
        public void GetValue_ByEntityValueKey_AfterDelete_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, float>("speed");
            entity.AddValue(key, 5.0f);
            entity.DelValue(key);

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => entity.GetValue(key));
        }

        [Test]
        public void GetValue_ByEntityValueKey_ReturnsCorrectValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, TestPoint>("pos");
            var point = new TestPoint { X = 1, Y = 2 };
            entity.AddValue(key, point);

            //Act:
            TestPoint result = entity.GetValue(key);

            //Assert:
            Assert.AreEqual(point, result);
        }

        #endregion

        // ══════════════════════════════════════════════════════════
        //  TryGetValue extensions
        // ══════════════════════════════════════════════════════════

        #region TryGetValue<T>(string, out T)

        [Test]
        public void TryGetValue_ByStringTyped_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("health", 100);

            //Act:
            bool found = entity.TryGetValue<int>("health", out int result);

            //Assert:
            Assert.IsTrue(found);
            Assert.AreEqual(100, result);
        }

        [Test]
        public void TryGetValue_ByStringTyped_ReturnsFalse_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            bool found = entity.TryGetValue<int>("missing", out int result);

            //Assert:
            Assert.IsFalse(found);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TryGetValue_ByStringTyped_ReturnsReferenceValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("name", "Alice");

            //Act:
            bool found = entity.TryGetValue<string>("name", out string result);

            //Assert:
            Assert.IsTrue(found);
            Assert.AreEqual("Alice", result);
        }

        [Test]
        public void TryGetValue_ByStringTyped_ReturnsNull_ForMissingReferenceType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            bool found = entity.TryGetValue<string>("missing", out string result);

            //Assert:
            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        [Test]
        public void TryGetValue_ByStringTyped_ThrowsInvalidCastException_WhenTypeMismatch()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("data", "text");

            //Act & Assert:
            Assert.Throws<InvalidCastException>(() =>
                entity.TryGetValue<int>("data", out _));
        }

        [Test]
        public void TryGetValue_ByStringTyped_AfterDelete_ReturnsFalse()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("key", 42);
            entity.DelValue("key");

            //Act:
            bool found = entity.TryGetValue<int>("key", out int result);

            //Assert:
            Assert.IsFalse(found);
            Assert.AreEqual(0, result);
        }

        #endregion

        #region TryGetValue(string, out object)

        [Test]
        public void TryGetValue_ByStringObject_ReturnsTrue_AndBoxedStruct()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("score", 99);

            //Act:
            bool found = entity.TryGetValue("score", out object result);

            //Assert:
            Assert.IsTrue(found);
            Assert.AreEqual(99, result);
            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        public void TryGetValue_ByStringObject_ReturnsTrue_AndReference()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("name", "Bob");

            //Act:
            bool found = entity.TryGetValue("name", out object result);

            //Assert:
            Assert.IsTrue(found);
            Assert.AreEqual("Bob", result);
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        public void TryGetValue_ByStringObject_ReturnsFalse_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            bool found = entity.TryGetValue("missing", out object result);

            //Assert:
            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        [Test]
        public void TryGetValue_ByStringObject_ReturnsNullReference_WhenMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            bool found = entity.TryGetValue("absent", out object value);

            //Assert:
            Assert.IsFalse(found);
            Assert.IsNull(value);
        }

        [Test]
        public void TryGetValue_ByStringObject_AfterDelete_ReturnsFalse()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("key", 42);
            entity.DelValue("key");

            //Act:
            bool found = entity.TryGetValue("key", out object result);

            //Assert:
            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        [Test]
        public void TryGetValue_ByStringObject_ReturnsCustomStruct()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var point = new TestPoint { X = 5, Y = 10 };
            entity.AddValue("pos", point);

            //Act:
            bool found = entity.TryGetValue("pos", out object result);

            //Assert:
            Assert.IsTrue(found);
            Assert.IsInstanceOf<TestPoint>(result);
            Assert.AreEqual(point, (TestPoint)result);
        }

        #endregion

        #region TryGetValue<T>(ValueKey<T>, out T)

        [Test]
        public void TryGetValue_ByValueKey_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");
            entity.AddValue(key, 100);

            //Act:
            bool found = entity.TryGetValue(key, out int result);

            //Assert:
            Assert.IsTrue(found);
            Assert.AreEqual(100, result);
        }

        [Test]
        public void TryGetValue_ByValueKey_ReturnsFalse_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("missing");

            //Act:
            bool found = entity.TryGetValue(key, out int result);

            //Assert:
            Assert.IsFalse(found);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TryGetValue_ByValueKey_ReturnsReferenceValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("name");
            entity.AddValue(key, "Hero");

            //Act:
            bool found = entity.TryGetValue(key, out string result);

            //Assert:
            Assert.IsTrue(found);
            Assert.AreEqual("Hero", result);
        }

        [Test]
        public void TryGetValue_ByValueKey_ReturnsNull_ForMissingReferenceType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("missing");

            //Act:
            bool found = entity.TryGetValue(key, out string result);

            //Assert:
            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        [Test]
        public void TryGetValue_ByValueKey_ThrowsInvalidCastException_WhenTypeMismatch()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int id = EntityKeyStore.NameToId("data");
            entity.AddValue(id, "text"); // stored as string via int-key overload

            //Act & Assert:
            Assert.Throws<InvalidCastException>(() =>
            {
                var intKey = new ValueKey<int>("data");
                _ = entity.TryGetValue(intKey, out int _);
            });
        }

        [Test]
        public void TryGetValue_ByValueKey_AfterDelete_ReturnsFalse()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<float>("speed");
            entity.AddValue(key, 7.7f);
            entity.DelValue(key);

            //Act:
            bool found = entity.TryGetValue(key, out float result);

            //Assert:
            Assert.IsFalse(found);
            Assert.AreEqual(0f, result);
        }

        #endregion

        #region TryGetValue<E,T>(ValueKey<E,T>, out T)

        [Test]
        public void TryGetValue_ByEntityValueKey_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("mana");
            entity.AddValue(key, 75);

            //Act:
            bool found = entity.TryGetValue(key, out int result);

            //Assert:
            Assert.IsTrue(found);
            Assert.AreEqual(75, result);
        }

        [Test]
        public void TryGetValue_ByEntityValueKey_ReturnsFalse_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("missing");

            //Act:
            bool found = entity.TryGetValue(key, out int result);

            //Assert:
            Assert.IsFalse(found);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TryGetValue_ByEntityValueKey_ReturnsReferenceValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, string>("className");
            entity.AddValue(key, "Mage");

            //Act:
            bool found = entity.TryGetValue(key, out string result);

            //Assert:
            Assert.IsTrue(found);
            Assert.AreEqual("Mage", result);
        }

        [Test]
        public void TryGetValue_ByEntityValueKey_AfterDelete_ReturnsFalse()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, float>("speed");
            entity.AddValue(key, 2.5f);
            entity.DelValue(key);

            //Act:
            bool found = entity.TryGetValue(key, out float result);

            //Assert:
            Assert.IsFalse(found);
            Assert.AreEqual(0f, result);
        }

        [Test]
        public void TryGetValue_ByEntityValueKey_ReturnsNull_ForMissingReferenceType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, string>("missing");

            //Act:
            bool found = entity.TryGetValue(key, out string result);

            //Assert:
            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        #endregion

        // ══════════════════════════════════════════════════════════
        //  SetValue extensions
        // ══════════════════════════════════════════════════════════

        #region SetValue(string, object)

        [Test]
        public void SetValue_ByString_AddsNewValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            entity.SetValue("health", 100);

            //Assert:
            Assert.IsTrue(entity.HasValue("health"));
            Assert.AreEqual(100, entity.GetValue("health"));
        }

        [Test]
        public void SetValue_ByString_UpdatesExistingValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.SetValue("health", 100);

            //Act:
            entity.SetValue("health", 200);

            //Assert:
            Assert.AreEqual(200, entity.GetValue("health"));
        }

        [Test]
        public void SetValue_ByString_FiresOnValueAdded_WhenNewKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int? addedKey = null;
            entity.OnValueAdded += (_, k, _) => addedKey = k;

            //Act:
            entity.SetValue("newKey", 42);

            //Assert:
            Assert.AreEqual(EntityKeyStore.NameToId("newKey"), addedKey);
        }

        [Test]
        public void SetValue_ByString_FiresOnValueChanged_WhenExistingKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.SetValue("key", 10);
            int? changedKey = null;
            object changedValue = null;
            entity.OnValueChanged += (_, k, v) => { changedKey = k; changedValue = v; };

            //Act:
            entity.SetValue("key", 20);

            //Assert:
            Assert.AreEqual(EntityKeyStore.NameToId("key"), changedKey);
            Assert.AreEqual(20, changedValue);
        }

        [Test]
        public void SetValue_ByString_DoesNotFireChanged_WhenSameReferenceValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            string value = "same";
            entity.SetValue("key", value);
            bool changed = false;
            entity.OnValueChanged += (_, _, _) => changed = true;

            //Act:
            entity.SetValue("key", value);

            //Assert:
            Assert.IsFalse(changed);
        }

        [Test]
        public void SetValue_ByString_IncrementsValueCount_WhenNewKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            entity.SetValue("a", 1);
            entity.SetValue("b", 2);

            //Assert:
            Assert.AreEqual(2, entity.ValueCount);
        }

        [Test]
        public void SetValue_ByString_DoesNotIncrementCount_WhenExistingKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.SetValue("a", 1);

            //Act:
            entity.SetValue("a", 2);

            //Assert:
            Assert.AreEqual(1, entity.ValueCount);
        }

        [Test]
        public void SetValue_ByString_ThrowsArgumentNullException_WhenValueIsNull()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() => entity.SetValue("key", null));
        }

        #endregion

        #region SetValue<T>(ValueKey<T>, T)

        [Test]
        public void SetValue_ByValueKey_AddsNewValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");

            //Act:
            entity.SetValue(key, 100);

            //Assert:
            Assert.IsTrue(entity.HasValue(key));
            Assert.AreEqual(100, entity.GetValue(key));
        }

        [Test]
        public void SetValue_ByValueKey_UpdatesExistingValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");
            entity.SetValue(key, 100);

            //Act:
            entity.SetValue(key, 200);

            //Assert:
            Assert.AreEqual(200, entity.GetValue(key));
        }

        [Test]
        public void SetValue_ByValueKey_FiresOnValueAdded_WhenNewKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("score");
            int? addedKey = null;
            entity.OnValueAdded += (_, k, _) => addedKey = k;

            //Act:
            entity.SetValue(key, 999);

            //Assert:
            Assert.AreEqual(key.Id, addedKey);
        }

        [Test]
        public void SetValue_ByValueKey_FiresOnValueChanged_WhenExistingKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("score");
            entity.SetValue(key, 10);
            int? changedKey = null;
            object changedValue = null;
            entity.OnValueChanged += (_, k, v) => { changedKey = k; changedValue = v; };

            //Act:
            entity.SetValue(key, 20);

            //Assert:
            Assert.AreEqual(key.Id, changedKey);
            Assert.AreEqual(20, changedValue);
        }

        [Test]
        public void SetValue_ByValueKey_StoresReferenceType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("name");

            //Act:
            entity.SetValue(key, "Hero");

            //Assert:
            Assert.AreEqual("Hero", entity.GetValue(key));
        }

        [Test]
        public void SetValue_ByValueKey_UpdatesReferenceType()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("name");
            entity.SetValue(key, "Hero");

            //Act:
            entity.SetValue(key, "Legend");

            //Assert:
            Assert.AreEqual("Legend", entity.GetValue(key));
        }

        [Test]
        public void SetValue_ByValueKey_DoesNotFireChanged_WhenSameValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");
            entity.SetValue(key, 50);
            bool changed = false;
            entity.OnValueChanged += (_, _, _) => changed = true;

            //Act:
            entity.SetValue(key, 50);

            //Assert:
            Assert.IsFalse(changed);
        }

        [Test]
        public void SetValue_ByValueKey_ThrowsArgumentNullException_WhenValueIsNull()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("data");

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() => entity.SetValue(key, null));
        }

        #endregion

        #region SetValue<E,T>(ValueKey<E,T>, T)

        [Test]
        public void SetValue_ByEntityValueKey_AddsNewValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("mana");

            //Act:
            entity.SetValue(key, 75);

            //Assert:
            Assert.IsTrue(entity.HasValue(key));
            Assert.AreEqual(75, entity.GetValue(key));
        }

        [Test]
        public void SetValue_ByEntityValueKey_UpdatesExistingValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("mana");
            entity.SetValue(key, 75);

            //Act:
            entity.SetValue(key, 150);

            //Assert:
            Assert.AreEqual(150, entity.GetValue(key));
        }

        [Test]
        public void SetValue_ByEntityValueKey_FiresOnValueAdded_WhenNewKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("level");
            int? addedKey = null;
            entity.OnValueAdded += (_, k, _) => addedKey = k;

            //Act:
            entity.SetValue(key, 5);

            //Assert:
            Assert.AreEqual(key.Id, addedKey);
        }

        [Test]
        public void SetValue_ByEntityValueKey_FiresOnValueChanged_WhenExistingKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("level");
            entity.SetValue(key, 1);
            int? changedKey = null;
            object changedValue = null;
            entity.OnValueChanged += (_, k, v) => { changedKey = k; changedValue = v; };

            //Act:
            entity.SetValue(key, 2);

            //Assert:
            Assert.AreEqual(key.Id, changedKey);
            Assert.AreEqual(2, changedValue);
        }

        [Test]
        public void SetValue_ByEntityValueKey_DoesNotFireChanged_WhenSameValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, float>("speed");
            entity.SetValue(key, 5.0f);
            bool changed = false;
            entity.OnValueChanged += (_, _, _) => changed = true;

            //Act:
            entity.SetValue(key, 5.0f);

            //Assert:
            Assert.IsFalse(changed);
        }

        [Test]
        public void SetValue_ByEntityValueKey_StoresCustomStruct()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, TestPoint>("pos");
            var point = new TestPoint { X = 1, Y = 2 };

            //Act:
            entity.SetValue(key, point);

            //Assert:
            Assert.AreEqual(point, entity.GetValue(key));
        }

        [Test]
        public void SetValue_ByEntityValueKey_ThrowsArgumentNullException_WhenValueIsNull()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, string>("data");

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() => entity.SetValue(key, null));
        }

        #endregion

        // ══════════════════════════════════════════════════════════
        //  HasValue extensions
        // ══════════════════════════════════════════════════════════

        #region HasValue(string)

        [Test]
        public void HasValue_ByString_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("health", 100);

            //Act:
            bool result = entity.HasValue("health");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasValue_ByString_ReturnsFalse_WhenKeyDoesNotExist()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act:
            bool result = entity.HasValue("missing");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasValue_ByString_ReturnsFalse_AfterDeletion()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("key", 42);
            entity.DelValue("key");

            //Act:
            bool result = entity.HasValue("key");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasValue_ByString_ReturnsTrue_ForMultipleKeys()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("a", 1);
            entity.AddValue("b", 2);

            //Act & Assert:
            Assert.IsTrue(entity.HasValue("a"));
            Assert.IsTrue(entity.HasValue("b"));
        }

        [Test]
        public void HasValue_ByString_ReturnsFalse_OnEmptyEntity()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act & Assert:
            Assert.IsFalse(entity.HasValue("anything"));
        }

        [Test]
        public void HasValue_ByString_UsesCorrectIdMapping()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            int id = EntityKeyStore.NameToId("score");
            entity.AddValue(id, 999);

            //Act & Assert:
            Assert.IsTrue(entity.HasValue("score"));
        }

        #endregion

        #region HasValue<T>(ValueKey<T>)

        [Test]
        public void HasValue_ByValueKey_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");
            entity.AddValue(key, 100);

            //Act:
            bool result = entity.HasValue(key);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasValue_ByValueKey_ReturnsFalse_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("missing");

            //Act:
            bool result = entity.HasValue(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasValue_ByValueKey_ReturnsFalse_AfterDeletion()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<string>("data");
            entity.AddValue(key, "value");
            entity.DelValue(key);

            //Act:
            bool result = entity.HasValue(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasValue_ByValueKey_ReturnsTrue_AfterSetValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<float>("speed");
            entity.SetValue(key, 5.5f);

            //Act:
            bool result = entity.HasValue(key);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasValue_ByValueKey_DifferentGenericTypes_Independent()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var intKey = new ValueKey<int>("shared");
            entity.AddValue(intKey, 42);

            //Act:
            var stringKey = new ValueKey<string>("shared");

            //Assert:
            // Same name => same Id, so HasValue should return true
            Assert.IsTrue(entity.HasValue(intKey));
            Assert.IsTrue(entity.HasValue(stringKey));
        }

        #endregion

        #region HasValue<E,T>(ValueKey<E,T>)

        [Test]
        public void HasValue_ByEntityValueKey_ReturnsTrue_WhenKeyExists()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("mana");
            entity.AddValue(key, 75);

            //Act:
            bool result = entity.HasValue(key);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasValue_ByEntityValueKey_ReturnsFalse_WhenKeyMissing()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("missing");

            //Act:
            bool result = entity.HasValue(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasValue_ByEntityValueKey_ReturnsFalse_AfterDeletion()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, string>("data");
            entity.AddValue(key, "value");
            entity.DelValue(key);

            //Act:
            bool result = entity.HasValue(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasValue_ByEntityValueKey_ReturnsTrue_AfterSetValue()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, float>("speed");
            entity.SetValue(key, 5.5f);

            //Act:
            bool result = entity.HasValue(key);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasValue_ByEntityValueKey_OnEmptyEntity_ReturnsFalse()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<Entity, int>("any");

            //Act & Assert:
            Assert.IsFalse(entity.HasValue(key));
        }

        #endregion

        // ══════════════════════════════════════════════════════════
        //  DisposeValues
        // ══════════════════════════════════════════════════════════

        #region DisposeValues

        [Test]
        public void DisposeValues_DisposesAllIDisposableValues()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var disposable1 = new DisposableValue();
            var disposable2 = new DisposableValue();
            entity.AddValue(1, disposable1);
            entity.AddValue(2, disposable2);

            //Act:
            entity.DisposeValues();

            //Assert:
            Assert.IsTrue(disposable1.IsDisposed);
            Assert.IsTrue(disposable2.IsDisposed);
        }

        [Test]
        public void DisposeValues_IgnoresNonIDisposableValues()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue(1, 42); // int is not IDisposable
            entity.AddValue(2, "text"); // string is not IDisposable

            //Act & Assert:
            Assert.DoesNotThrow(() => entity.DisposeValues());
        }

        [Test]
        public void DisposeValues_DisposesMixedDisposableAndNonDisposable()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var disposable = new DisposableValue();
            entity.AddValue(1, disposable);
            entity.AddValue(2, 42);
            entity.AddValue(3, "text");

            //Act:
            entity.DisposeValues();

            //Assert:
            Assert.IsTrue(disposable.IsDisposed);
        }

        [Test]
        public void DisposeValues_NoValues_DoesNotThrow()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);

            //Act & Assert:
            Assert.DoesNotThrow(() => entity.DisposeValues());
        }

        [Test]
        public void DisposeValues_OnlyDisposesIDisposableNotAllValues()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var disposable = new DisposableValue();
            entity.AddValue(1, disposable);
            entity.AddValue(2, 42);

            //Act:
            entity.DisposeValues();

            //Assert:
            Assert.IsTrue(disposable.IsDisposed);
            // Non-disposable value should still be accessible
            Assert.AreEqual(42, entity.GetValue<int>(2));
        }

        [Test]
        public void DisposeValues_DisposesDifferentDisposableTypes()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var disposableA = new DisposableValueA();
            var disposableB = new DisposableValueB();
            entity.AddValue(1, disposableA);
            entity.AddValue(2, disposableB);

            //Act:
            entity.DisposeValues();

            //Assert:
            Assert.IsTrue(disposableA.IsDisposed);
            Assert.IsTrue(disposableB.IsDisposed);
        }

        [Test]
        public void DisposeValues_AfterDeletion_OnlyDisposesRemaining()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var disposed = new DisposableValue();
            var notDisposed = new DisposableValue();
            entity.AddValue(1, disposed);
            entity.AddValue(2, notDisposed);
            entity.DelValue(1);

            //Act:
            entity.DisposeValues();

            //Assert:
            Assert.IsFalse(disposed.IsDisposed); // deleted, not iterated
            Assert.IsTrue(notDisposed.IsDisposed);
        }

        [Test]
        public void DisposeValues_DoesNotClearValues_AfterDisposal()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var disposable = new DisposableValue();
            entity.AddValue(1, disposable);

            //Act:
            entity.DisposeValues();

            //Assert:
            // Values remain in the entity even after disposal
            Assert.AreEqual(1, entity.ValueCount);
        }

        #endregion

        // ══════════════════════════════════════════════════════════
        //  Cross-method integration tests
        // ══════════════════════════════════════════════════════════

        #region Cross-method integration

        [Test]
        public void AddValue_ByString_CanBeRetrieved_ByValueKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.AddValue("health", 100);
            var key = new ValueKey<int>("health");

            //Act:
            int result = entity.GetValue(key);

            //Assert:
            Assert.AreEqual(100, result);
        }

        [Test]
        public void AddValue_ByValueKey_CanBeRetrieved_ByString()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");
            entity.AddValue(key, 100);

            //Act:
            int result = entity.GetValue<int>("health");

            //Assert:
            Assert.AreEqual(100, result);
        }

        [Test]
        public void SetValue_ByString_CanBeDeleted_ByValueKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            entity.SetValue("health", 100);
            var key = new ValueKey<int>("health");

            //Act:
            bool result = entity.DelValue(key);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsFalse(entity.HasValue("health"));
        }

        [Test]
        public void SetValue_ByValueKey_CanBeChecked_ByString()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("score");
            entity.SetValue(key, 500);

            //Act:
            bool result = entity.HasValue("score");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void AddValues_ByStringPairs_CanBeRetrieved_ByValueKeys()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var values = new Dictionary<string, object>
            {
                { "health", 100 },
                { "mana", 50 }
            };
            var healthKey = new ValueKey<int>("health");
            var manaKey = new ValueKey<int>("mana");

            //Act:
            entity.AddValues((IEnumerable<KeyValuePair<string, object>>)values);

            //Assert:
            Assert.AreEqual(100, entity.GetValue(healthKey));
            Assert.AreEqual(50, entity.GetValue(manaKey));
        }

        [Test]
        public void FullLifecycle_AddSetTryGetDelete_ByStringAndValueKey()
        {
            //Arrange:
            var entity = new Entity(valueCapacity: 4);
            var key = new ValueKey<int>("health");

            //Act & Assert: Add via string
            entity.AddValue("health", 100);
            Assert.AreEqual(100, entity.GetValue(key));

            //Act & Assert: TryGetValue via ValueKey
            bool found = entity.TryGetValue(key, out int val);
            Assert.IsTrue(found);
            Assert.AreEqual(100, val);

            //Act & Assert: SetValue via ValueKey
            entity.SetValue(key, 200);
            Assert.AreEqual(200, entity.GetValue<int>("health"));

            //Act & Assert: HasValue via string
            Assert.IsTrue(entity.HasValue("health"));

            //Act & Assert: DelValue via string
            entity.DelValue("health");
            Assert.IsFalse(entity.HasValue(key));
        }

        #endregion
    }
}
