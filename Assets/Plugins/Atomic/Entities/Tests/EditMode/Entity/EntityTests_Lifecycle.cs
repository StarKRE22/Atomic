using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void Init()
        {
            //Arrange
            var entity = new Entity();
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
            var entity = new Entity();
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
            var entity = new Entity();
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
            var entity = new Entity();
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
            var entity = new Entity();
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
            var entity = new Entity();
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
            var entity = new Entity();
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
    }
}