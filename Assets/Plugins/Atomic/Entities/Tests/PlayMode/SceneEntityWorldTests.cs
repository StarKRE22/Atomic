using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed class SceneEntityWorldTests
    {
        #region Lifecycle

        [Test]
        public void InitEntities()
        {
            //Arrange:
            var initBehaviour = new EntityBehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(initBehaviour);

            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);
            var wasInit = false;

            //Act:
            entity.OnInitialized += () => wasInit = true;
            entityWorld.InitEntities();

            //Assert:
            Assert.IsTrue(wasInit);
            Assert.IsTrue(initBehaviour.initialized);
        }

        [Test]
        public void EnableEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(behaviourStub);

            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);
            var wasEnable = false;

            //Act:
            entity.OnEnabled += () => wasEnable = true;
            entityWorld.EnableEntities();

            //Assert:
            Assert.IsTrue(wasEnable);
            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
        }

        [Test]
        public void UpdateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = new Entity("E");
            entity.Enable();
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);
            var wasUpdate = false;

            //Act:
            entity.OnUpdated += _ => wasUpdate = true;
            entityWorld.UpdateEntities(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void FixedUpdateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = new Entity("E");
            entity.Enable();
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            var world = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);
            var wasUpdate = false;

            //Act:
            entity.OnFixedUpdated += _ => wasUpdate = true;
            world.FixedUpdateEntities(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void LateUpdateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = new Entity("E");
            entity.Enable();
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            var world = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);
            var wasUpdate = false;

            //Act:
            entity.OnLateUpdated += _ => wasUpdate = true;
            world.LateUpdateEntities(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void DisableEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = new Entity("E");
            entity.Enable();
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            var world = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);
            var wasDisable = false;

            //Act:
            entity.OnDisabled += () => wasDisable = true;
            world.DisableEntities();

            //Assert:
            Assert.IsTrue(wasDisable);
        }

        [Test]
        public void DisposeEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = new Entity("E");
            entity.Enable();
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            var world = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);
            var wasDispose = false;

            //Act:
            entity.OnDisposed += () => wasDispose = true;
            world.DisposeEntities();

            //Assert:
            Assert.IsTrue(wasDispose);
        }

        #endregion

        #region Entities

        [Test]
        public void AddEntity()
        {
            //Arrange
            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false);
            var entity = new Entity("Test Entity");
            IEntity wasEvent = null;

            //Act
            entityWorld.OnEntityAdded += addedEntity => wasEvent = addedEntity;
            bool success = entityWorld.AddEntity(entity);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity, wasEvent);
        }

        [Test]
        public void DelEntity()
        {
            //Arrange
            var entity = new Entity("Test Entity");
            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);
            IEntity wasEvent = null;

            //Act
            entityWorld.OnEntityDeleted += rEntity => wasEvent = rEntity;
            bool success = entityWorld.DelEntity(entity);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity, wasEvent);
        }

        [Test]
        public void HasEntity()
        {
            //Arrange
            var entity = new Entity("Test Entity");
            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity);

            //Act
            bool exists = entityWorld.HasEntity(entity);

            //Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public void GetEntityWithTag()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});

            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false, entity2, entity1, entity3);

            //Act
            IEntity entityWithTag = entityWorld.GetEntityWithTag(0);

            //Assert
            Assert.AreEqual(entity2, entityWithTag);
        }

        [Test]
        public void GetEntitiesWithTag()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});
            var entity4 = new Entity("4", new[] {1});
            
            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false,
                entity2, entity1, entity4, entity3);

            //Act
            IReadOnlyList<IEntity> entitiesWithTag = entityWorld.GetEntitiesWithTag(0);

            //Assert
            Assert.AreEqual(3, entitiesWithTag.Count);
            Assert.AreEqual(entity2, entitiesWithTag[0]);
            Assert.AreEqual(entity1, entitiesWithTag[1]);
            Assert.AreEqual(entity3, entitiesWithTag[2]);
        }

        [Test]
        public void GetEntitiesWithTagNonAlloc()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});
            var entity4 = new Entity("4", new[] {1});

            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false,
                entity2, entity1, entity4, entity3);

            //Act
            IEntity[] buffer = new IEntity[10];
            int count = entityWorld.GetEntitiesWithTag(0, buffer);

            //Assert
            Assert.AreEqual(3, count);
            Assert.AreEqual(entity2, buffer[0]);
            Assert.AreEqual(entity1, buffer[1]);
            Assert.AreEqual(entity3, buffer[2]);
        }

        [Test]
        public void GetEntityWithValue()
        {
            var entity1 = new Entity("1", values: new Dictionary<int, object>
            {
                {0, new object()}
            });
            var entity2 = new Entity("2", values: new Dictionary<int, object>
            {
                {1, new object()}
            });
            var entity3 = new Entity("3", values: new Dictionary<int, object>
            {
                {1, new object()}
            });

            var entityWorld = SceneEntityWorld.Instantiate("Test", scanEntities: false,
                entity2, entity1, entity3);

            //Act
            IEntity entityWithValue = entityWorld.GetEntityWithValue(1);

            //Assert
            Assert.AreEqual(entity2, entityWithValue);
        }

        #endregion

        // #region Unity
        //
        // private const string SCENE_PATH = "Assets/Plugins/Atomic/Entities/Tests/Assets/Scenes/EntityWorld.unity";
        //
        // [UnityTest]
        // public IEnumerator AutoScanEntities()
        // {
        //     AsyncOperation operation = EditorSceneManager
        //         .LoadSceneAsyncInPlayMode(SCENE_PATH, new LoadSceneParameters(LoadSceneMode.Additive));
        //
        //     yield return operation;
        //     yield return new WaitForEndOfFrame();
        //
        //     SceneEntityWorld entityWorld = GameObject.FindObjectOfType<SceneEntityWorld>();
        //
        //     yield return null;
        //     
        //     Assert.AreEqual(4, entityWorld.EntityCount);
        // }
        //
        // #endregion
    }
}