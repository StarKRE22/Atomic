using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class SceneEntityWorldTests
    {
        [Test]
        public void InitEntities()
        {
            //Arrange:
            var initBehaviour = new BehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(initBehaviour);

            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false, entity);
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

            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            var wasEnable = false;

            //Act:
            entity.OnEnabled += () => wasEnable = true;
            entityWorld.Enable();

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
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Enable();
            var wasUpdate = false;
            
            Assert.IsTrue(behaviourStub.initialized);
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
            var behaviourStub = new BehaviourStub();
            var entity = new Entity("E");
            entity.AddBehaviour(behaviourStub);

            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Enable();
            var wasUpdate = false;

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
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Enable();
            var wasUpdate = false;

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
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Enable();
            var wasDisable = false;

            //Pre-assert:
            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
            
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
            entity.AddBehaviour(behaviourStub);
            
            var world = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            world.Enable();
            var wasDispose = false;

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
            
            //Act:
            entity.OnDisposed += () => wasDispose = true;
            world.Dispose();

            //Assert:
            Assert.IsTrue(wasDispose);
        }
    }
}