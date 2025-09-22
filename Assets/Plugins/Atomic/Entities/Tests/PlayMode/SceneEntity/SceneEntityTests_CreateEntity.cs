using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class SceneEntityTests_CreateEntity
    {
        #region CreateWithArgs

        [Test]
        public void CreateEntity_SetsNameCorrectly()
        {
            var entity = SceneEntity.Create("123");
            Assert.AreEqual("123", entity.Name);
        }

        [Test]
        public void CreateEntity_SetsTagsCorrectly()
        {
            var entity = SceneEntity.Create("123", new[] { 1, 2 });

            Assert.IsTrue(entity.HasTag(1));
            Assert.IsTrue(entity.HasTag(2));
            Assert.IsFalse(entity.HasTag(3));
            CollectionAssert.AreEquivalent(new[] { 1, 2 }, entity.GetTags());
        }

        [Test]
        public void CreateEntity_SetsValuesCorrectly()
        {
            var obj = new object();
            var str = "Test";
            var values = new Dictionary<int, object>
            {
                { 1, obj },
                { 2, str }
            };

            var entity = SceneEntity.Create("123", null, values);

            Assert.IsTrue(entity.HasValue(1));
            Assert.IsTrue(entity.HasValue(2));
            Assert.IsFalse(entity.HasValue(3));
            Assert.AreEqual(obj, entity.GetValue<object>(1));
            Assert.AreEqual("Test", entity.GetValue<string>(2));
            CollectionAssert.AreEquivalent(values, entity.GetValues());
        }

        [Test]
        public void CreateEntity_SetsBehavioursCorrectly()
        {
            IEntityBehaviour b1 = new EntityBehaviourStub();
            IEntityBehaviour b2 = new EntityBehaviourStub();
            IEntityBehaviour b3 = new EntityBehaviourStub();

            var entity = SceneEntity.Create("123", null, null, new[] { b1, b2, b3 });

            Assert.IsTrue(entity.HasBehaviour(b1));
            Assert.IsTrue(entity.HasBehaviour(b2));
            Assert.IsTrue(entity.HasBehaviour(b3));
            CollectionAssert.AreEqual(new[] { b1, b2, b3 }, entity.GetBehaviours());
        }

        #endregion

        #region CreateTWithArgs

        [Test]
        public void CreateTEntity_SetsName()
        {
            var entity = SceneEntity.Create<SceneEntityDummy>("MyEntity");

            Assert.AreEqual("MyEntity", entity.Name);
            Assert.AreEqual("MyEntity", entity.gameObject.name);
        }

        [Test]
        public void CreateTEntity_AssignsTags()
        {
            var entity = SceneEntity.Create<SceneEntityDummy>("Entity", new[] { 10, 20 });

            Assert.IsTrue(entity.HasTag(10));
            Assert.IsTrue(entity.HasTag(20));
        }

        [Test]
        public void CreateTEntity_AssignsValues()
        {
            var values = new Dictionary<int, object>
            {
                { 1, "value" },
                { 2, 42 }
            };

            var entity = SceneEntity.Create<SceneEntityDummy>("Entity", values: values);

            Assert.AreEqual("value", entity.GetValue<string>(1));
            Assert.AreEqual(42, entity.GetValue<int>(2));
        }

        [Test]
        public void CreateTEntity_AssignsBehaviours()
        {
            var b1 = new EntityBehaviourStub();
            var b2 = new EntityBehaviourStub();

            var entity = SceneEntity.Create<SceneEntityDummy>("Entity", behaviours: new[] { b1, b2 });

            Assert.IsTrue(entity.HasBehaviour(b1));
            Assert.IsTrue(entity.HasBehaviour(b2));
            CollectionAssert.AreEquivalent(new[] { b1, b2 }, entity.GetBehaviours());
        }

        [Test]
        public void CreateTEntity_CreatesGameObjectWithComponent()
        {
            var entity = SceneEntity.Create<SceneEntityDummy>("GameObjectCheck");

            Assert.IsNotNull(entity);
            Assert.IsInstanceOf<SceneEntityDummy>(entity);
            Assert.AreEqual("GameObjectCheck", entity.gameObject.name);
        }

        #endregion

        #region CreateByPrefab

        [Test]
        public void CreateEntity_InstantiatesPrefab_UnderParent()
        {
            // Arrange
            var parent = new GameObject("Parent").transform;
            var prefab = new GameObject("Prefab").AddComponent<SceneEntity>();

            // Act
            var instance = SceneEntity.Create(prefab, parent);

            // Assert
            Assert.NotNull(instance);
            Assert.AreNotSame(prefab, instance);
            Assert.AreEqual(parent, instance.transform.parent);
        }

        [Test]
        public void CreateEntities_CallsInstallMethod()
        {
            // Arrange
            var parent = new GameObject("Parent").transform;
            var prefabObj = new GameObject("Prefab");
            var prefab = prefabObj.AddComponent<SceneEntity>();

            // Act
            var instance = SceneEntity.Create(prefab, parent);

            // Assert
            Assert.IsTrue(instance.Installed);
        }

        #endregion
    }
}