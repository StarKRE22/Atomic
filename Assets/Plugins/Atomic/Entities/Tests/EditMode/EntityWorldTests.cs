using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed class EntityWorldTests
    {
        #region Main

        [Test]
        public void Instantiate()
        {
            //Arrange:
            var e1 = new Entity();
            var e2 = new Entity();
            var e3 = new Entity();
            var e4 = new Entity();
            var world = new EntityWorld("Test", e1, e2, e3);

            //Assert:
            Assert.AreEqual("Test", world.Name);

            Assert.AreEqual(3, world.Count);
            Assert.IsTrue(world.Has(e1));
            Assert.IsTrue(world.Has(e2));
            Assert.IsTrue(world.Has(e3));
            Assert.IsFalse(world.Has(e4));

            Assert.AreEqual(new List<IEntity>
            {
                e1, e2, e3
            }, world.All);
        }

        #endregion

        #region Lifecycle

        [Test]
        public void InitEntities()
        {
            //Arrange:
            var initBehaviour = new BehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(initBehaviour);

            var entityWorld = new EntityWorld("Test", entity);
            var wasInit = false;

            //Act:
            entity.OnInitialized += () => wasInit = true;
            entityWorld.Init();

            //Assert:
            Assert.IsTrue(wasInit);
            Assert.IsTrue(initBehaviour.initialized);
        }

        [Test]
        public void EnableEntities()
        {
            var behaviourStub = new BehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(behaviourStub);

            var entityRunner = new EntityWorld("Test", entity);
            var wasEnable = false;

            //Act:
            entity.OnEnabled += () => wasEnable = true;
            entityRunner.Enable();

            //Assert:
            Assert.IsTrue(wasEnable);
            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
        }

        [Test]
        public void UpdateEntities()
        {
            var behaviourStub = new BehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.initialized);
            Assert.IsFalse(behaviourStub.enabled);

            var world = new EntityWorld("Test", entity);
            world.Enable();

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
            
            //Act:
            var wasUpdate = false;
            entity.OnUpdated += _ => wasUpdate = true;
            world.OnUpdate(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }


        [Test]
        public void FixedUpdateEntities()
        {
            var behaviourStub = new BehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(behaviourStub);

            
            var world = new EntityWorld("Test", entity);
            world.Enable();
            var wasUpdate = false;

            //Pre-assert:
            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
            
            //Act:
            entity.OnFixedUpdated += _ => wasUpdate = true;
            world.OnFixedUpdate(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void LateUpdateEntities()
        {
            var behaviourStub = new BehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(behaviourStub);

            var world = new EntityWorld("Test", entity);
            world.Enable();
            var wasUpdate = false;
            
            //Pre-assert:
            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            //Act:
            entity.OnLateUpdated += _ => wasUpdate = true;
            world.OnLateUpdate(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }


        [Test]
        public void DisableEntities()
        {
            var behaviourStub = new BehaviourStub();
            var entity = new Entity("E");
            entity.Enable();
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            var world = new EntityWorld("Test", entity);
            world.Enable();
            var wasDisable = false;

            //Act:
            entity.OnDisabled += () => wasDisable = true;
            world.Disable();

            //Assert:
            Assert.IsTrue(wasDisable);
        }

        [Test]
        public void DisposeEntities()
        {
            var behaviourStub = new BehaviourStub();
            var entity = new Entity("E");
            entity.Enable();
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);

            var world = new EntityWorld("Test", entity);
            world.Init();
            var wasDispose = false;

            //Act:
            entity.OnDisposed += () => wasDispose = true;
            world.Dispose();

            //Assert:
            Assert.IsTrue(wasDispose);
        }

        #endregion

        #region Entities

        [Test]
        public void AddEntity()
        {
            //Arrange
            var entityWorld = new EntityWorld("Test World");
            var entity = new Entity("Test Entity");
            IEntity wasEvent = null;
            bool stateChanged = false;

            //Act
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.OnAdded += addedEntity => wasEvent = addedEntity;
            bool success = entityWorld.Add(entity);

            //Assert
            Assert.IsTrue(success);
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(entity, wasEvent);
        }

        [Test]
        public void WhenAddEntityThatAlreadyAddedThenFalse()
        {
            //Arrange
            var entity = new Entity();
            var entityWorld = new EntityWorld(entity);
            IEntity wasEvent = null;
            bool stateChanged = false;

            //Act
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.OnAdded += addedEntity => wasEvent = addedEntity;
            bool success = entityWorld.Add(entity);

            //Assert
            Assert.IsFalse(success);
            Assert.IsFalse(stateChanged);
            Assert.IsNull(wasEvent);
        }

        [Test]
        public void DelEntity()
        {
            //Arrange
            var entity = new Entity("Test Entity");
            var entityWorld = new EntityWorld("Test World", entity);
            IEntity wasEvent = null;
            bool stateChanged = false;

            //Act
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.OnDeleted += rEntity => wasEvent = rEntity;
            bool success = entityWorld.Del(entity);

            //Assert
            Assert.IsTrue(success);
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(entity, wasEvent);
        }

        [Test]
        public void WhenDelEntityThatAbsentThenFalse()
        {
            //Arrange
            var entity = new Entity();
            var entityWorld = new EntityWorld("Test World");
            IEntity wasEvent = null;
            bool stateChanged = false;

            //Act
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.OnDeleted += rEntity => wasEvent = rEntity;
            bool success = entityWorld.Del(entity);

            //Assert
            Assert.IsFalse(success);
            Assert.IsFalse(stateChanged);
            Assert.IsNull(wasEvent);
        }

        [Test]
        public void HasEntity()
        {
            //Arrange
            var entity1 = new Entity();
            var entity2 = new Entity();
            var entityWorld = new EntityWorld("Test World", entity1);

            //Assert
            Assert.IsTrue(entityWorld.Has(entity1));
            Assert.IsFalse(entityWorld.Has(entity2));
        }

        [Test]
        public void ClearEntities()
        {
            //Arrange:
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity3);
            
            Debug.Log($"COUNT {entityWorld.Count}");
            foreach (IEntity entity in entityWorld)
            {
                Debug.Log($"Entity {entity.Name}");
            }
            
            var stateChanged = false;
            
            //Act:
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.Clear();
            
            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(0, entityWorld.Count);
            Assert.IsFalse(entityWorld.Has(entity1));
            Assert.IsFalse(entityWorld.Has(entity2));
            Assert.IsFalse(entityWorld.Has(entity3));
        }

        [Test]
        public void WhenClearEmptyThenStateNotChanged()
        {
            //Arrange:
            var entityWorld = new EntityWorld("Test World");
            var stateChanged = false;
            
            //Act:
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.Clear();
            
            //Assert:
            Assert.IsFalse(stateChanged);
        }

        [Test]
        public void GetEntityWithTag()
        {
            //Arrange:
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity3);

            //Act
            
            bool success = entityWorld.GetWithTag(0, out IEntity entityWithTag);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity2, entityWithTag);
        }

        [Test]
        public void GetEntitiesWithTag()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});
            var entity4 = new Entity("4", new[] {1});

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity4, entity3);

            //Act
            IReadOnlyList<IEntity> entitiesWithTag = entityWorld.GetAllWithTag(0);

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

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity4, entity3);

            //Act
            IEntity[] buffer = new IEntity[10];
            int count = entityWorld.GetAllWithTag(0, buffer);

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

            var entityWorld = new EntityWorld("Test World", entity1, entity2, entity3);

            //Act
            
             bool success = entityWorld.GetWithValue(1, out IEntity entityWithValue);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity2, entityWithValue);
        }

        [Test]
        public void GetAll()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});
            var entity4 = new Entity("4", new[] {1});

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity4, entity3);

            //Act
            var result = entityWorld.GetAll();

            //Assert
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(entity2, result[0]);
            Assert.AreEqual(entity1, result[1]);
            Assert.AreEqual(entity4, result[2]);
            Assert.AreEqual(entity3, result[3]);
        }
        
        [Test]
        public void GetAllNonAlloc()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});
            var entity4 = new Entity("4", new[] {1});

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity4, entity3);

            //Act
            var buffer = new IEntity[32];
            int count = entityWorld.GetAll(buffer);

            //Assert
            Assert.AreEqual(4, count);
            Assert.AreEqual(entity2, buffer[0]);
            Assert.AreEqual(entity1, buffer[1]);
            Assert.AreEqual(entity4, buffer[2]);
            Assert.AreEqual(entity3, buffer[3]);
        }

        #endregion
    }
}