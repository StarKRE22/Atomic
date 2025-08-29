using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class SceneEntityWorldTests
    {
        [Test]
        public void SpawnEntities()
        {
            //Arrange:
            var initBehaviour = new DummyEntityBehaviour();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(initBehaviour);

            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            var wasInit = false;

            //Act:
            entity.OnSpawned += () => wasInit = true;
            world.Spawn();

            //Assert:
            Assert.IsTrue(wasInit);
            Assert.IsTrue(initBehaviour.Spawned);
        }

        [Test]
        public void ActivateEntities()
        {
            var behaviourStub = new DummyEntityBehaviour();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);

            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            var wasEnable = false;

            //Act:
            entity.OnEnabled += () => wasEnable = true;
            world.Enable();

            //Assert:
            Assert.IsTrue(wasEnable);
            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsTrue(behaviourStub.Activated);
        }

        [Test]
        public void UpdateEntities()
        {
            var behaviourStub = new DummyEntityBehaviour();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.Spawned);
            Assert.IsFalse(behaviourStub.Activated);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasUpdate = false;
            
            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsTrue(behaviourStub.Activated);

            //Act:
            entity.OnUpdated += _ => wasUpdate = true;
            world.OnUpdate(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void FixedUpdateEntities()
        {
            var behaviourStub = new DummyEntityBehaviour();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);

            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasUpdate = false;

            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsTrue(behaviourStub.Activated);
            
            //Act:
            entity.OnFixedUpdated += _ => wasUpdate = true;
            world.OnFixedUpdate(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void LateUpdateEntities()
        {
            var behaviourStub = new DummyEntityBehaviour();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasUpdate = false;

            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsTrue(behaviourStub.Activated);

            //Act:
            entity.OnLateUpdated += _ => wasUpdate = true;
            world.OnLateUpdate(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void DeactivateEntities()
        {
            var behaviourStub = new DummyEntityBehaviour();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasDisable = false;

            //Pre-assert:
            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsTrue(behaviourStub.Activated);
            
            //Act:
            entity.OnDisabled += () => wasDisable = true;
            world.Disable();

            //Assert:
            Assert.IsTrue(wasDisable);
        }

        [Test]
        public void DespawnEntities()
        {
            var behaviourStub = new DummyEntityBehaviour();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasDispose = false;

            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsTrue(behaviourStub.Activated);
            
            //Act:
            entity.OnDespawned += () => wasDispose = true;
            world.Dispose();

            //Assert:
            Assert.IsTrue(wasDispose);
        }
    }
}