using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class SceneEntityWorldTests
    {
        [Test]
        public void SpawnEntities()
        {
            //Arrange:
            var initBehaviour = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(initBehaviour);

            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            var wasInit = false;

            //Act:
            entity.OnSpawned += () => wasInit = true;
            entityWorld.Spawn();

            //Assert:
            Assert.IsTrue(wasInit);
            Assert.IsTrue(initBehaviour.spawned);
        }

        [Test]
        public void ActivateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);

            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            var wasEnable = false;

            //Act:
            entity.OnActivated += () => wasEnable = true;
            entityWorld.Activate();

            //Assert:
            Assert.IsTrue(wasEnable);
            Assert.IsTrue(behaviourStub.spawned);
            Assert.IsTrue(behaviourStub.enabled);
        }

        [Test]
        public void UpdateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.spawned);
            Assert.IsFalse(behaviourStub.enabled);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Activate();
            var wasUpdate = false;
            
            Assert.IsTrue(behaviourStub.spawned);
            Assert.IsTrue(behaviourStub.enabled);

            //Act:
            entity.OnUpdated += _ => wasUpdate = true;
            world.OnUpdate(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void FixedUpdateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);

            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Activate();
            var wasUpdate = false;

            Assert.IsTrue(behaviourStub.spawned);
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
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Activate();
            var wasUpdate = false;

            Assert.IsTrue(behaviourStub.spawned);
            Assert.IsTrue(behaviourStub.enabled);

            //Act:
            entity.OnLateUpdated += _ => wasUpdate = true;
            world.OnLateUpdate(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void DeactivateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Activate();
            var wasDisable = false;

            //Pre-assert:
            Assert.IsTrue(behaviourStub.spawned);
            Assert.IsTrue(behaviourStub.enabled);
            
            //Act:
            entity.OnDeactivated += () => wasDisable = true;
            world.Deactivate();

            //Assert:
            Assert.IsTrue(wasDisable);
        }

        [Test]
        public void DespawnEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Activate();
            var wasDispose = false;

            Assert.IsTrue(behaviourStub.spawned);
            Assert.IsTrue(behaviourStub.enabled);
            
            //Act:
            entity.OnDespawned += () => wasDispose = true;
            world.Dispose();

            //Assert:
            Assert.IsTrue(wasDispose);
        }
    }
}