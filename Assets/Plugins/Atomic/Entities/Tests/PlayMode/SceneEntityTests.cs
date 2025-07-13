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

            IBehaviour behaviour1 = new BehaviourStub();
            IBehaviour behaviour2 = new BehaviourStub();
            IBehaviour behaviour3 = new BehaviourStub();

            //Act:

            var entity = SceneEntity.Create("123",
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

            Assert.AreEqual(new[] {tag1, tag2}, entity.GetTags());

            Assert.IsTrue(entity.HasValue(key1));
            Assert.IsTrue(entity.HasValue(key2));
            Assert.IsFalse(entity.HasValue(key3));
            Assert.AreEqual(new Dictionary<int, object>
            {
                {key1, val1},
                {key2, val2}
            }, entity.GetValues());

            Assert.AreEqual(val1, entity.GetValue<object>(key1));
            Assert.AreEqual("Test", entity.GetValue<string>(key2));

            Assert.IsTrue(entity.HasBehaviour(behaviour1));
            Assert.IsTrue(entity.HasBehaviour(behaviour2));
            Assert.IsTrue(entity.HasBehaviour(behaviour3));

            Assert.AreEqual(new[]
            {
                behaviour1,
                behaviour2,
                behaviour3
            }, entity.GetBehaviours());
        }

        [Test]
        public void NotEquals()
        {
            //Arrange:
            var entity1 = SceneEntity.Create("1");
            var entity2 = SceneEntity.Create("2");

            //Assert:
            Assert.IsFalse(entity1.Equals(entity2));
        }

        [Test]
        public void HashCodeTest()
        {
            var entity1 = SceneEntity.Create("1");
            Assert.AreEqual(entity1.Id, entity1.GetHashCode());
        }

        [Test]
        public void WhenInstanceIdEqualsThenEquals()
        {
            //Arrange:
            var entity1 = SceneEntity.Create("1");
            var entity2 = SceneEntity.Create("2");

            //Act:
            FieldInfo field = typeof(SceneEntity)
                .GetField("_entity", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

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

            var e = SceneEntity.Create("123", new[] {tag1, tag2});

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

            var e = SceneEntity.Create("123", new[] {tag1, tag2});
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

            var e = SceneEntity.Create("123", new[] {tag1, tag2});
            e.OnTagDeleted += (_, t) => removedTag = t;

            //Act & Assert:
            Assert.IsTrue(e.DelTag(tag1));
            Assert.AreEqual(tag1, removedTag);

            Assert.IsTrue(e.DelTag(tag2));
            Assert.AreEqual(tag2, removedTag);

            Assert.IsFalse(e.DelTag(tag3));
            Assert.AreNotEqual(tag3, removedTag);
        }

       

        #endregion

        #region Values

        [Test]
        public void GetValue()
        {
            //Arrange:
            const int key = 1;
            string foo = new string("Foo");

            SceneEntity e = SceneEntity.Create("123", values: new Dictionary<int, object>
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

            var e = SceneEntity.Create("123", values: new Dictionary<int, object>
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
            var e = SceneEntity.Create("123");

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
            var e = SceneEntity.Create("123");

            //Act:
            Assert.Catch<KeyNotFoundException>(() => e.GetValue<string>(0));
        }

        #endregion

        #region Lifecycle

        [Test]
        public void Init()
        {
            //Arrange
            var entity = SceneEntity.Create();
            var wasEvent = false;
            var behaviourStub = new BehaviourStub();

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
            var entity = SceneEntity.Create();
            var initEvent = false;
            var enabledEvent = false;
            var behaviourStub = new BehaviourStub();

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
            var entity = SceneEntity.Create();
            var wasEvent = false;
            var behaviourStub = new BehaviourStub();

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
            var entity = SceneEntity.Create();
            var wasEvent = false;
            var behaviourStub = new BehaviourStub();

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
            var entity = SceneEntity.Create();
            var behaviourStub = new BehaviourStub();
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
            var entity = SceneEntity.Create();
            var behaviourStub = new BehaviourStub();
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
            var entity = SceneEntity.Create();
            var behaviourStub = new BehaviourStub();
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
            var updateStub = new UpdateStub();
            var initStub = new InitStub();
            var behaviourStub = new BehaviourStub();

            var entity = SceneEntity.Create(behaviours: new IBehaviour[]
            {
                updateStub,
                initStub,
                behaviourStub
            });

            //Act:
            IReadOnlyCollection<IBehaviour> behaviours = entity.GetBehaviours();

            //Assert:
            Assert.AreEqual(new HashSet<IBehaviour>
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
            var updateStub = new UpdateStub();
            var initStub = new InitStub();
            var behaviourStub = new BehaviourStub();

            var entity = SceneEntity.Create(behaviours: new IBehaviour[]
            {
                updateStub,
                initStub
            });

            //Assert & Act:
            Assert.IsTrue(entity.HasBehaviour(updateStub));
            Assert.IsTrue(entity.HasBehaviour<InitStub>());
            Assert.IsFalse(entity.HasBehaviour(behaviourStub));
        }

      
        [Test]
        public void DelBehaviour()
        {
            //Arrange:
            IBehaviour removedBehaviour = null;

            var updateStub = new UpdateStub();
            var initStub = new InitStub();
            var behaviourStub = new BehaviourStub();

            var entity = SceneEntity.Create(behaviours: new IBehaviour[]
            {
                updateStub,
                initStub
            });

            entity.OnBehaviourDeleted += (_, b) => removedBehaviour = b;

            //Assert & Act:
            Assert.IsTrue(entity.DelBehaviour(updateStub));
            Assert.AreEqual(updateStub, removedBehaviour);

            Assert.IsTrue(entity.DelBehaviour<InitStub>());
            Assert.IsFalse(entity.HasBehaviour(initStub));

            Assert.IsFalse(entity.DelBehaviour(behaviourStub));
            Assert.IsFalse(entity.DelBehaviour<InitStub>());
        }

        [Test]
        public void WhenAddBehaviourAfterInitThenBehaviourWiilInit()
        {
            //Arrange:
            var behaviourStub = new BehaviourStub();

            var entity = SceneEntity.Create();
            entity.Init();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsFalse(behaviourStub.enabled);
            Assert.AreEqual(nameof(BehaviourStub.Init), behaviourStub.invokationList[0]);
        }

        [Test]
        public void WhenAddBehaviourAfterEnableThenBehaviourWillEnable()
        {
            //Arrange:
            var behaviourStub = new BehaviourStub();

            var entity = SceneEntity.Create();
            entity.Init();
            entity.Enable();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
            Assert.AreEqual(nameof(BehaviourStub.Init), behaviourStub.invokationList[0]);
            Assert.AreEqual(nameof(BehaviourStub.Enable), behaviourStub.invokationList[1]);
        }

        [Test]
        public void WhenDelBehaviourAfterInitThenBehaviourWiilDispose()
        {
            //Arrange:
            var behaviourStub = new BehaviourStub();

            var entity = SceneEntity.Create(behaviours: new IBehaviour[] {behaviourStub});
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
            var behaviourStub = new BehaviourStub();

            var entity = SceneEntity.Create(behaviours: new IBehaviour[] {behaviourStub});
            entity.Init();
            entity.Enable();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.disabled);
            Assert.IsTrue(behaviourStub.disposed);
            Assert.AreEqual(nameof(BehaviourStub.Disable), behaviourStub.invokationList[^2]);
            Assert.AreEqual(nameof(BehaviourStub.Dispose), behaviourStub.invokationList[^1]);
        }

        #endregion

        #region Static

        [UnityTest]
        public IEnumerator Cast()
        {
            var entityGO = new GameObject();
            SceneEntity sceneEntity = entityGO.AddComponent<SceneEntity>();
            SceneEntityProxy sceneEntityProxy = entityGO.AddComponent<SceneEntityProxy>();
            sceneEntityProxy._source = sceneEntity;

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
            sceneEntityProxy._source = sceneEntity;

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