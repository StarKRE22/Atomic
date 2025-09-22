using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class SceneEntityWorldTests
    {

        [Test]
        public void EnableEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
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
            Assert.IsTrue(behaviourStub.Initialized);
            Assert.IsTrue(behaviourStub.Enabled);
        }

        [Test]
        public void UpdateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.Initialized);
            Assert.IsFalse(behaviourStub.Enabled);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasUpdate = false;
            
            Assert.IsTrue(behaviourStub.Initialized);
            Assert.IsTrue(behaviourStub.Enabled);

            //Act:
            entity.OnTicked += _ => wasUpdate = true;
            world.Tick(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void FixedUpdateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);

            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasUpdate = false;

            Assert.IsTrue(behaviourStub.Initialized);
            Assert.IsTrue(behaviourStub.Enabled);
            
            //Act:
            entity.OnFixedTicked += _ => wasUpdate = true;
            world.FixedTick(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void LateUpdateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasUpdate = false;

            Assert.IsTrue(behaviourStub.Initialized);
            Assert.IsTrue(behaviourStub.Enabled);

            //Act:
            entity.OnLateTicked += _ => wasUpdate = true;
            world.LateTick(0);

            //Assert:
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void DeactivateEntities()
        {
            var behaviourStub = new EntityBehaviourStub();
            var entity = SceneEntity.Create("E", useUnityLifecycle: false);
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            world.Add(entity);
            world.Enable();
            var wasDisable = false;

            //Pre-assert:
            Assert.IsTrue(behaviourStub.Initialized);
            Assert.IsTrue(behaviourStub.Enabled);
            
            //Act:
            entity.OnDisabled += () => wasDisable = true;
            world.Disable();

            //Assert:
            Assert.IsTrue(wasDisable);
        }
    }
}