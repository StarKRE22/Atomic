using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityRegistryTests
    {
        [SetUp]
        public void SetUp()
        {
            EntityRegistry.Instance.Clear();
        }

        [Test]
        public void Register_Should_AssignUniqueId_And_AddToRegistry()
        {
            // Arrange
            var entity = new EntityStub();

            // Act
            EntityRegistry.Instance.Register(entity, out int id);

            // Assert
            Assert.Greater(id, 0);
            Assert.IsTrue(EntityRegistry.Instance.Contains(id));
            Assert.IsTrue(EntityRegistry.Instance.Contains(entity));
        }

        [Test]
        public void Unregister_Should_RemoveEntity_And_ResetId()
        {
            // Arrange
            var entity = new EntityStub();
            EntityRegistry.Instance.Register(entity, out int id);

            // Act
            EntityRegistry.Instance.Unregister(ref id);

            // Assert
            Assert.AreEqual(-1, id);
            Assert.IsFalse(EntityRegistry.Instance.Contains(entity));
        }

        [Test]
        public void OnAdded_Should_Fire_WhenEntityRegistered()
        {
            // Arrange
            var entity = new EntityStub();
            IEntity added = null;
            EntityRegistry.Instance.OnAdded += e => added = e;

            // Act
            EntityRegistry.Instance.Register(entity, out _);

            // Assert
            Assert.AreSame(entity, added);
        }

        [Test]
        public void OnRemoved_Should_Fire_WhenEntityUnregistered()
        {
            // Arrange
            var entity = new EntityStub();
            EntityRegistry.Instance.Register(entity, out int id);

            IEntity removed = null;
            EntityRegistry.Instance.OnRemoved += e => removed = e;

            // Act
            EntityRegistry.Instance.Unregister(ref id);

            // Assert
            Assert.AreSame(entity, removed);
        }

        [Test]
        public void TryGet_Should_ReturnTrue_WhenIdExists()
        {
            // Arrange
            var entity = new EntityStub();
            EntityRegistry.Instance.Register(entity, out int id);

            // Act
            bool found = EntityRegistry.Instance.TryGet(id, out var result);

            // Assert
            Assert.IsTrue(found);
            Assert.AreSame(entity, result);
        }

        [Test]
        public void Get_Should_Throw_WhenIdInvalid()
        {
            // Arrange & Act & Assert
            Assert.Throws<KeyNotFoundException>(() => { _ = EntityRegistry.Instance.Get(999); });
        }

        [Test]
        public void CopyTo_Should_PopulateCollection()
        {
            // Arrange
            var e1 = new EntityStub();
            var e2 = new EntityStub();
            EntityRegistry.Instance.Register(e1, out _);
            EntityRegistry.Instance.Register(e2, out _);

            var list = new List<IEntity>();

            // Act
            EntityRegistry.Instance.CopyTo(list);

            // Assert
            CollectionAssert.Contains(list, e1);
            CollectionAssert.Contains(list, e2);
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void Clear_Should_RemoveAllEntities()
        {
            // Arrange
            EntityRegistry.Instance.Register(new EntityStub(), out _);
            EntityRegistry.Instance.Register(new EntityStub(), out _);

            // Act
            EntityRegistry.Instance.Clear();

            // Assert
            Assert.AreEqual(0, EntityRegistry.Instance.Count);
        }

        [Test]
        public void Singleton_Instance_Should_NotBeNull()
        {
            // Act
            var instance = EntityRegistry.Instance;

            // Assert
            Assert.NotNull(instance);
        }

        [Test]
        public void Enumerator_Should_Iterate_Entities()
        {
            // Arrange
            var e1 = new EntityStub();
            var e2 = new EntityStub();
            EntityRegistry.Instance.Register(e1, out _);
            EntityRegistry.Instance.Register(e2, out _);

            var seen = new HashSet<IEntity>();

            // Act
            foreach (var e in EntityRegistry.Instance)
                seen.Add(e);

            // Assert
            CollectionAssert.Contains(seen, e1);
            CollectionAssert.Contains(seen, e2);
        }

        [Test]
        public void Register_Should_AssignUniqueIncrementingIds()
        {
            // Arrange
            var e1 = new EntityStub();
            var e2 = new EntityStub();

            // Act
            EntityRegistry.Instance.Register(e1, out int id1);
            EntityRegistry.Instance.Register(e2, out int id2);

            // Assert
            Assert.AreEqual(id1 + 1, id2);
        }

        [Test]
        public void CopyTo_Array_Should_CopyEntitiesCorrectly()
        {
            Debug.Log($"ENTITY COUNTйаййц {EntityRegistry.Instance.Count}");
            
            // Arrange
            var e1 = new EntityStub();
            var e2 = new EntityStub();
            EntityRegistry.Instance.Register(e1, out _);
            EntityRegistry.Instance.Register(e2, out _);

            var array = new IEntity[5];

            Debug.Log($"ENTITY COUNT {EntityRegistry.Instance.Count}");

            // Act
            EntityRegistry.Instance.CopyTo(array, 2);

            // Assert
            Assert.IsNull(array[0]);
            Assert.IsNull(array[1]);
            Assert.Contains(e1, array);
            Assert.Contains(e2, array);
        }

        [Test]
        public void Contains_Should_ReturnFalse_AfterClear()
        {
            // Arrange
            var entity = new EntityStub();
            EntityRegistry.Instance.Register(entity, out int id);

            // Act
            EntityRegistry.Instance.Clear();

            // Assert
            Assert.IsFalse(EntityRegistry.Instance.Contains(id));
            Assert.IsFalse(EntityRegistry.Instance.Contains(entity));
        }

        [Test]
        public void Unregister_Should_NotThrow_WhenIdInvalid()
        {
            // Arrange
            int id = 999;

            // Act & Assert
            Assert.DoesNotThrow(() => { EntityRegistry.Instance.Unregister(ref id); });
            Assert.AreEqual(999, id); // id не меняется, так как не найден
        }

        [Test]
        public void ResetAll_Should_ClearSingletonInstance()
        {
            // Arrange
            var entity = new EntityStub();
            EntityRegistry.Instance.Register(entity, out int id);
            Assert.IsTrue(EntityRegistry.Instance.Contains(id));

            EntityRegistry.ResetAll();

            // Assert
            Assert.AreEqual(0, EntityRegistry.Instance.Count);
        }
    }
}