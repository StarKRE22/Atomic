using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed class SceneEntityTests
    {
        #region Main

        [Test]
        public void NewEntity()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            const int key1 = 1;
            const int key2 = 2;
            const int key3 = 3;

            object val1 = new object();
            string val2 = new string("Test");

            IEntityBehaviour behaviour1 = new EntityBehaviourStub();
            IEntityBehaviour behaviour2 = new EntityBehaviourStub();
            IEntityBehaviour behaviour3 = new EntityBehaviourStub();

            //Act:

            var entity = SceneEntity.Instantiate("123",
                new[] {tag1, tag2},
                new Dictionary<int, object>
                {
                    {key1, val1},
                    {key2, val2},
                }, new[]
                {
                    behaviour1,
                    behaviour2,
                    behaviour3
                });

            //Assert:
            Assert.AreEqual("123", entity.Name);

            Assert.IsTrue(entity.HasTag(tag1));
            Assert.IsTrue(entity.HasTag(tag2));
            Assert.IsFalse(entity.HasTag(tag3));

            Assert.AreEqual(new[] {tag1, tag2}, entity.Tags);

            Assert.IsTrue(entity.HasValue(key1));
            Assert.IsTrue(entity.HasValue(key2));
            Assert.IsFalse(entity.HasValue(key3));
            Assert.AreEqual(new Dictionary<int, object>
            {
                {key1, val1},
                {key2, val2}
            }, entity.Values);

            Assert.AreEqual(val1, entity.GetValue(key1));
            Assert.AreEqual("Test", entity.GetValue<string>(key2));

            Assert.IsTrue(entity.HasBehaviour(behaviour1));
            Assert.IsTrue(entity.HasBehaviour(behaviour2));
            Assert.IsTrue(entity.HasBehaviour(behaviour3));

            Assert.AreEqual(new[]
            {
                behaviour1,
                behaviour2,
                behaviour3
            }, entity.Behaviours);
        }

        [Test]
        public void NotEquals()
        {
            //Arrange:
            var entity1 = SceneEntity.Instantiate("1");
            var entity2 = SceneEntity.Instantiate("2");

            //Assert:
            Assert.IsFalse(entity1.Equals(entity2));
        }

        [Test]
        public void HashCodeTest()
        {
            var entity1 = SceneEntity.Instantiate("1");
            Assert.AreEqual(entity1.InstanceId, entity1.GetHashCode());
        }

        [Test]
        public void WhenInstanceIdEqualsThenEquals()
        {
            //Arrange:
            var entity1 = SceneEntity.Instantiate("1");
            var entity2 = SceneEntity.Instantiate("2");

            //Act:
            FieldInfo field = typeof(SceneEntity)
                .GetField("entity", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            field!.SetValue(entity2, field.GetValue(entity1));

            //Assert:
            Assert.IsTrue(entity1.Equals(entity2));
        }

        #endregion

        #region Tags

        [Test]
        public void HasTag()
        {
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            var e = SceneEntity.Instantiate("123", new[] {tag1, tag2});

            Assert.IsTrue(e.HasTag(tag1));
            Assert.IsTrue(e.HasTag(tag2));
            Assert.IsFalse(e.HasTag(tag3));
        }

        [Test]
        public void AddTag()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            int addedTag = -1;

            var e = SceneEntity.Instantiate("123", new[] {tag1, tag2});
            e.OnTagAdded += (_, t) => addedTag = t;

            //Act:
            Assert.IsFalse(e.AddTag(tag1));
            Assert.AreEqual(-1, addedTag);

            Assert.IsFalse(e.AddTag(tag2));
            Assert.AreEqual(-1, addedTag);

            Assert.IsTrue(e.AddTag(tag3));
            Assert.AreEqual(tag3, addedTag);
        }

        [Test]
        public void DelTag()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            int removedTag = -1;

            var e = SceneEntity.Instantiate("123", new[] {tag1, tag2});
            e.OnTagDeleted += (_, t) => removedTag = t;

            //Act & Assert:
            Assert.IsTrue(e.DelTag(tag1));
            Assert.AreEqual(tag1, removedTag);

            Assert.IsTrue(e.DelTag(tag2));
            Assert.AreEqual(tag2, removedTag);

            Assert.IsFalse(e.DelTag(tag3));
            Assert.AreNotEqual(tag3, removedTag);
        }

        [Test]
        public void ClearTags()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            bool wasClear = false;

            var e = SceneEntity.Instantiate("123", new[] {tag1, tag2});
            e.OnTagsCleared += _ => wasClear = true;

            //Act:
            e.ClearTags();

            //Assert:
            Assert.IsTrue(wasClear);
            Assert.IsFalse(e.HasTag(tag1));
            Assert.IsFalse(e.HasTag(tag2));
        }

        [Test]
        public void WhenClearEntityWithEmptyTagsThenFalse()
        {
            //Arrange:
            bool wasClear = false;

            var e = SceneEntity.Instantiate("123");
            e.OnTagsCleared += _ => wasClear = true;

            //Act:
            e.ClearTags();

            //Act:
            Assert.IsFalse(wasClear);
        }

        #endregion

        #region Values

        [Test]
        public void GetValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");

            SceneEntity e = SceneEntity.Instantiate("123", values: new Dictionary<int, object>
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

            var e = SceneEntity.Instantiate("123", values: new Dictionary<int, object>
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
            var e = SceneEntity.Instantiate("123");

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
            var e = SceneEntity.Instantiate("123");

            //Act:
            Assert.Catch<KeyNotFoundException>(() => e.GetValue<string>(0));
        }

        [Test]
        public void WhenSetValueThatPreviousNotExistsInEntityThenAdded()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");
            var e = SceneEntity.Instantiate("123");

            var wasChangeEvent = false;
            object addedValue = null;
            int addedKey = -1;

            //Act:
            e.OnValueAdded += (_, k, val) =>
            {
                addedKey = k;
                addedValue = val;
            };
            e.OnValueChanged += (_, _, _) => { wasChangeEvent = false; };

            e.SetValue(key, foo);

            //Assert:
            Assert.AreEqual(addedKey, key);
            Assert.AreEqual(addedValue, foo);
            Assert.IsTrue(e.HasValue(key));
            Assert.IsFalse(wasChangeEvent);
            Assert.AreEqual(foo, e.GetValue(key));
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
            object changedValue = null;

            SceneEntity e = SceneEntity.Instantiate("123", values: new Dictionary<int, object>
            {
                {key, foo}
            });

            e.OnValueAdded += (_, _, _) => { wasAddEvent = false; };
            e.OnValueChanged += (_, k, v) =>
            {
                changedKey = k;
                changedValue = v;
            };

            //Act:

            e.SetValue(key, foo2);

            //Assert:
            Assert.AreEqual(changedKey, key);
            Assert.AreEqual(changedValue, foo2);
            Assert.IsTrue(e.HasValue(key));
            Assert.IsFalse(wasAddEvent);
            Assert.AreEqual(foo2, e.GetValue(key));
        }

        [Test]
        public void AddValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");

            bool wasAddEvent = false;
            int addedKey = -1;
            object addedValue = null;

            SceneEntity e = SceneEntity.Instantiate("123");

            e.OnValueAdded += (_, k, v) =>
            {
                wasAddEvent = true;
                addedKey = k;
                addedValue = v;
            };

            //Act:

            bool success = e.AddValue(key, foo);

            //Assert:
            Assert.IsTrue(success);

            Assert.IsTrue(wasAddEvent);
            Assert.AreEqual(addedKey, key);
            Assert.AreEqual(addedValue, foo);
            Assert.IsTrue(e.HasValue(key));
        }

        [Test]
        public void WhenValueIsAlreadyAddedThenReturnFalse()
        {
            //Arrange:
            const int key = 1;
            string foo1 = new string("Foo1");
            string foo2 = new string("Foo2");

            SceneEntity e = SceneEntity.Instantiate("123", values: new Dictionary<int, object>
            {
                {key, foo1}
            });

            //Act:
            bool success = e.AddValue(key, foo2);

            //Assert:
            Assert.IsFalse(success);
            Assert.AreEqual(foo1, e.GetValue(key));
            Assert.AreNotEqual(foo2, e.GetValue(key));
        }

        [Test]
        public void DelValue()
        {
            //Arrange:
            const int key1 = 1;
            const int key2 = 2;

            object foo1 = new object();
            object foo2 = new object();

            SceneEntity e = SceneEntity.Instantiate("123", values: new Dictionary<int, object>
            {
                {key1, foo1},
                {key2, foo2}
            });

            //Act:
            bool success = e.DelValue(key1);

            //Assert:
            Assert.IsTrue(success);
            Assert.IsFalse(e.HasValue(key1));
            Assert.IsFalse(e.TryGetValue(key1, out _));
            Assert.Catch<KeyNotFoundException>(() => e.GetValue(key1));
        }

        [Test]
        public void DelValueOut()
        {
            //Arrange:
            const int key1 = 1;
            const int key2 = 2;

            object foo1 = new object();
            object foo2 = new object();

            SceneEntity e = SceneEntity.Instantiate("123", values: new Dictionary<int, object>
            {
                {key1, foo1},
                {key2, foo2}
            });

            //Act:
            bool success = e.DelValue(key1, out object removed);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(foo1, removed);
            Assert.IsFalse(e.HasValue(key1));
            Assert.IsFalse(e.TryGetValue(key1, out _));
            Assert.Catch<KeyNotFoundException>(() => e.GetValue(key1));
        }

        [Test]
        public void WhenDelAbsentValueThenReturnFalse()
        {
            //Arrange:
            const int key1 = 1;

            SceneEntity e = SceneEntity.Instantiate("123");

            //Act:
            bool success = e.DelValue(key1, out object removed);

            //Assert:
            Assert.IsFalse(success);
            Assert.IsNull(removed);
        }

        [Test]
        public void ClearValues()
        {
            //Arrange:
            const int key1 = 1;
            const int key2 = 2;

            object foo1 = new object();
            object foo2 = new object();

            SceneEntity e = SceneEntity.Instantiate("123", values: new Dictionary<int, object>
            {
                {key1, foo1},
                {key2, foo2}
            });

            //Act:
            bool success = e.ClearValues();

            //Assert:
            Assert.IsTrue(success);
            Assert.IsFalse(e.HasValue(key1));
            Assert.IsFalse(e.HasValue(key2));
            Assert.Catch<KeyNotFoundException>(() => e.GetValue(key1));
            Assert.Catch<KeyNotFoundException>(() => e.GetValue(key2));
        }

        [Test]
        public void WhenClearEmptyValuesThenNothingHappened()
        {
            //Arrange:
            SceneEntity e = SceneEntity.Instantiate("123");

            //Act:
            bool success = e.ClearValues();

            //Assert:
            Assert.IsFalse(success);
        }

        #endregion

        #region Lifecycle

        [Test]
        public void Init()
        {
            //Arrange
            var entity = SceneEntity.Instantiate();
            var wasEvent = false;
            var behaviourStub = new EntityBehaviourStub();

            entity.AddBehaviour(behaviourStub);
            entity.OnInitialized += () => wasEvent = true;

            //Act
            entity.Init();

            //Assert
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(behaviourStub.initialized);
        }


        [Test]
        public void Enable()
        {
            //Arrange
            var entity = SceneEntity.Instantiate();
            var initEvent = false;
            var enabledEvent = false;
            var behaviourStub = new EntityBehaviourStub();

            entity.AddBehaviour(behaviourStub);
            entity.OnInitialized += () => initEvent = true;
            entity.OnEnabled += () => enabledEvent = true;

            //Act
            entity.Init();
            entity.Enable();

            //Assert
            Assert.IsTrue(initEvent);
            Assert.IsTrue(enabledEvent);

            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(entity.Enabled);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            Assert.AreEqual(nameof(entity.Init), behaviourStub.invokationList[0]);
            Assert.AreEqual(nameof(entity.Enable), behaviourStub.invokationList[1]);
        }

        [Test]
        public void Disable()
        {
            //Arrange
            var entity = SceneEntity.Instantiate();
            var wasEvent = false;
            var behaviourStub = new EntityBehaviourStub();

            entity.AddBehaviour(behaviourStub);
            entity.OnDisabled += () => wasEvent = true;

            //Act
            entity.Init();
            entity.Enable();
            entity.Disable();

            //Assert
            Assert.IsTrue(behaviourStub.disabled);
            Assert.IsTrue(wasEvent);
            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Dispose()
        {
            //Arrange
            var entity = SceneEntity.Instantiate();
            var wasEvent = false;
            var behaviourStub = new EntityBehaviourStub();

            entity.AddBehaviour(behaviourStub);
            entity.OnDisposed += () => wasEvent = true;

            //Act
            entity.Init();
            entity.Enable();
            entity.Dispose();

            //Assert
            Assert.IsTrue(behaviourStub.disposed);
            Assert.IsTrue(wasEvent);

            Assert.IsFalse(entity.Enabled);
            Assert.IsFalse(entity.Initialized);
        }

        [Test]
        public void Update()
        {
            //Arrange
            var entity = SceneEntity.Instantiate();
            var behaviourStub = new EntityBehaviourStub();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnUpdated += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.OnUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.updated);
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void FixedUpdate()
        {
            //Arrange
            var entity = SceneEntity.Instantiate();
            var behaviourStub = new EntityBehaviourStub();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnFixedUpdated += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.OnFixedUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.fixedUpdated);
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void LateUpdate()
        {
            //Arrange
            var entity = SceneEntity.Instantiate();
            var behaviourStub = new EntityBehaviourStub();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnLateUpdated += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.OnLateUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.lateUpdated);
            Assert.IsTrue(wasUpdate);
        }

        #endregion

        #region Behaviours

        [Test]
        public void GetAllBehaviours()
        {
            //Arrange:
            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();
            var behaviourStub = new EntityBehaviourStub();

            var entity = SceneEntity.Instantiate(behaviours: new IEntityBehaviour[]
            {
                updateStub,
                initStub,
                behaviourStub
            });

            //Act:
            IReadOnlyCollection<IEntityBehaviour> behaviours = entity.Behaviours;

            //Assert:
            Assert.AreEqual(new HashSet<IEntityBehaviour>
            {
                updateStub,
                initStub,
                behaviourStub
            }, behaviours);
        }

        [Test]
        public void HasBehaviour()
        {
            //Arrange:
            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();
            var behaviourStub = new EntityBehaviourStub();

            var entity = SceneEntity.Instantiate(behaviours: new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            //Assert & Act:
            Assert.IsTrue(entity.HasBehaviour(updateStub));
            Assert.IsTrue(entity.HasBehaviour<EntityInitStub>());
            Assert.IsFalse(entity.HasBehaviour(behaviourStub));
        }

        [Test]
        public void AddBehaviour()
        {
            //Arrange:
            IEntityBehaviour addedBehaviour = null;

            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();

            var entity = SceneEntity.Instantiate(behaviours: new IEntityBehaviour[]
            {
                updateStub
            });

            entity.OnBehaviourAdded += (_, b) => addedBehaviour = b;

            //Assert & Act:
            Assert.IsFalse(entity.AddBehaviour(updateStub));
            Assert.IsNull(addedBehaviour);

            Assert.IsTrue(entity.AddBehaviour(initStub));
            Assert.AreEqual(initStub, addedBehaviour);

            entity.AddBehaviour<EntityBehaviourStub>();
            Assert.IsNotNull(addedBehaviour);
        }

        [Test]
        public void DelBehaviour()
        {
            //Arrange:
            IEntityBehaviour removedBehaviour = null;

            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();
            var behaviourStub = new EntityBehaviourStub();

            var entity = SceneEntity.Instantiate(behaviours: new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            entity.OnBehaviourDeleted += (_, b) => removedBehaviour = b;

            //Assert & Act:
            Assert.IsTrue(entity.DelBehaviour(updateStub));
            Assert.AreEqual(updateStub, removedBehaviour);

            Assert.IsTrue(entity.DelBehaviour<EntityInitStub>());
            Assert.IsFalse(entity.HasBehaviour(initStub));

            Assert.IsFalse(entity.DelBehaviour(behaviourStub));
            Assert.IsFalse(entity.DelBehaviour<EntityInitStub>());
        }

        [Test]
        public void WhenAddBehaviourAfterInitThenBehaviourWiilInit()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = SceneEntity.Instantiate();
            entity.Init();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsFalse(behaviourStub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Init), behaviourStub.invokationList[0]);
        }

        [Test]
        public void WhenAddBehaviourAfterEnableThenBehaviourWillEnable()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = SceneEntity.Instantiate();
            entity.Init();
            entity.Enable();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Init), behaviourStub.invokationList[0]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Enable), behaviourStub.invokationList[1]);
        }

        [Test]
        public void WhenDelBehaviourAfterInitThenBehaviourWiilDispose()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = SceneEntity.Instantiate(behaviours: new IEntityBehaviour[] {behaviourStub});
            entity.Init();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.disabled);
            Assert.IsTrue(behaviourStub.disposed);
        }

        [Test]
        public void WhenDelBehaviourAfterEnableThenBehaviourWiilDisableAndDispose()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = SceneEntity.Instantiate(behaviours: new IEntityBehaviour[] {behaviourStub});
            entity.Init();
            entity.Enable();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.disabled);
            Assert.IsTrue(behaviourStub.disposed);
            Assert.AreEqual(nameof(EntityBehaviourStub.Disable), behaviourStub.invokationList[^2]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Dispose), behaviourStub.invokationList[^1]);
        }

        [Test]
        public void ClearBehaviours()
        {
            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();
            var wasClear = false;

            var entity = SceneEntity.Instantiate(behaviours: new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            entity.OnBehavioursCleared += _ => wasClear = true;

            //Act:
            bool success = entity.ClearBehaviours();

            Assert.IsTrue(success);
            Assert.IsTrue(wasClear);
        }

        [Test]
        public void WhenClearEmptyBehavioursThenFalse()
        {
            //Arrange:
            var wasClear = false;
            var entity = SceneEntity.Instantiate();

            entity.OnBehavioursCleared += _ => wasClear = true;

            //Act:
            bool success = entity.ClearBehaviours();

            Assert.IsFalse(success);
            Assert.IsFalse(wasClear);
        }

        #endregion

        #region Static

        [UnityTest]
        public IEnumerator Cast()
        {
            var entityGO = new GameObject();
            SceneEntity sceneEntity = entityGO.AddComponent<SceneEntity>();
            SceneEntityProxy sceneEntityProxy = entityGO.AddComponent<SceneEntityProxy>();
            sceneEntityProxy.source = sceneEntity;

            //Wait awake:
            yield return null;

            Assert.AreEqual(sceneEntity, SceneEntity.Cast(sceneEntity.Entity));
            Assert.AreEqual(sceneEntity, SceneEntity.Cast(sceneEntity));
            Assert.AreEqual(sceneEntity, SceneEntity.Cast(sceneEntityProxy));
            Assert.AreNotEqual(sceneEntity, SceneEntity.Cast(new Entity()));
            Assert.AreEqual(null, SceneEntity.Cast(null));
        }

        [UnityTest]
        public IEnumerator TryCast()
        {
            var entityGO = new GameObject();
            SceneEntity sceneEntity = entityGO.AddComponent<SceneEntity>();
            SceneEntityProxy sceneEntityProxy = entityGO.AddComponent<SceneEntityProxy>();
            sceneEntityProxy.source = sceneEntity;

            //Wait awake:
            yield return null;

            bool success = SceneEntity.TryCast(sceneEntity.Entity, out SceneEntity result);
            Assert.IsTrue(success);
            Assert.AreEqual(sceneEntity, result);

            success = SceneEntity.TryCast(sceneEntity, out result);
            Assert.IsTrue(success);
            Assert.AreEqual(sceneEntity, result);

            success = SceneEntity.TryCast(sceneEntityProxy, out result);
            Assert.IsTrue(success);
            Assert.AreEqual(sceneEntity, result);

            success = SceneEntity.TryCast(null, out result);
            Assert.IsFalse(success);
            Assert.AreNotEqual(sceneEntity, result);
            
            success = SceneEntity.TryCast(new Entity(), out result);
            Assert.IsFalse(success);
            Assert.AreNotEqual(sceneEntity, result);
        }

        #endregion
    }
}