using System.Collections.Generic;
using NUnit.Framework;

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

            Assert.AreEqual(3, world.EntityCount);
            Assert.IsTrue(world.HasEntity(e1));
            Assert.IsTrue(world.HasEntity(e2));
            Assert.IsTrue(world.HasEntity(e3));
            Assert.IsFalse(world.HasEntity(e4));

            Assert.AreEqual(new List<IEntity>
            {
                e1, e2, e3
            }, world.Entities);
        }

        #endregion

        #region Lifecycle

        [Test]
        public void InitEntities()
        {
            //Arrange:
            var initBehaviour = new EntityBehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(initBehaviour);

            var entityWorld = new EntityWorld("Test", entity);

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

            var entityRunner = new EntityWorld("Test", entity);
            var wasEnable = false;

            //Act:
            entity.OnEnabled += () => wasEnable = true;
            entityRunner.EnableEntities();

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

            var world = new EntityWorld("Test", entity);
            var wasUpdate = false;

            //Act:
            entity.OnUpdated += _ => wasUpdate = true;
            world.UpdateEntities(0);

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

            var world = new EntityWorld("Test", entity);
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

            var world = new EntityWorld("Test", entity);
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

            var world = new EntityWorld("Test", entity);
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

            var world = new EntityWorld("Test", entity);
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
            var entityWorld = new EntityWorld("Test World");
            var entity = new Entity("Test Entity");
            IEntity wasEvent = null;
            bool stateChanged = false;

            //Act
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.OnEntityAdded += addedEntity => wasEvent = addedEntity;
            bool success = entityWorld.AddEntity(entity);

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
            entityWorld.OnEntityAdded += addedEntity => wasEvent = addedEntity;
            bool success = entityWorld.AddEntity(entity);

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
            entityWorld.OnEntityDeleted += rEntity => wasEvent = rEntity;
            bool success = entityWorld.DelEntity(entity);

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
            entityWorld.OnEntityDeleted += rEntity => wasEvent = rEntity;
            bool success = entityWorld.DelEntity(entity);

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
            Assert.IsTrue(entityWorld.HasEntity(entity1));
            Assert.IsFalse(entityWorld.HasEntity(entity2));
        }

        [Test]
        public void ClearEntities()
        {
            //Arrange:
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity3);
            
            var stateChanged = false;
            
            //Act:
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.ClearEntities();
            
            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(0, entityWorld.EntityCount);
            Assert.IsFalse(entityWorld.HasEntity(entity1));
            Assert.IsFalse(entityWorld.HasEntity(entity2));
            Assert.IsFalse(entityWorld.HasEntity(entity3));
        }

        [Test]
        public void WhenClearEmptyThenStateNotChanged()
        {
            //Arrange:
            var entityWorld = new EntityWorld("Test World");
            var stateChanged = false;
            
            //Act:
            entityWorld.OnStateChanged += () => stateChanged = true;
            entityWorld.ClearEntities();
            
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

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity4, entity3);

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

            var entityWorld = new EntityWorld("Test World", entity2, entity1, entity4, entity3);

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

            var entityWorld = new EntityWorld("Test World", entity1, entity2, entity3);

            //Act
            IEntity entityWithValue = entityWorld.GetEntityWithValue(1);

            //Assert
            Assert.AreEqual(entity2, entityWithValue);
        }

        #endregion
    }
}