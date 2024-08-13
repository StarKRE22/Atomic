using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Contexts
{
    [TestFixture]
    public sealed class ContextTests
    {
        #region Lifecycle

        [Test]
        public void Initialize()
        {
            //Arrange
            var context = new Context();
            var wasEvent = new Reference<bool>();
            var initSystem = new InitSystemStub();

            context.AddSystem(initSystem);
            context.OnInitiazized += () => wasEvent.value = true;

            //Act
            context.Init();

            //Assert
            Assert.IsTrue(context.Initialized);
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(initSystem.initialized);
        }

        [Test]
        public void Enable()
        {
            //Arrange
            var context = new Context();
            var initEvent = new Reference<bool>();
            var enabledEvent = new Reference<bool>();
            var systemStub = new CommonSystemStub();

            context.AddSystem(systemStub);
            context.OnInitiazized += () => initEvent.value = true;
            context.OnEnabled += () => enabledEvent.value = true;

            //Act
            context.Init();
            context.Enable();

            //Assert
            Assert.IsTrue(initEvent.value);
            Assert.IsTrue(enabledEvent.value);

            Assert.IsTrue(context.Initialized);
            Assert.IsTrue(context.Enabled);

            Assert.IsTrue(systemStub.initialized);
            Assert.IsTrue(systemStub.enabled);
            Assert.AreEqual("IE", systemStub.flowQueue.ToString());
        }

        [Test]
        public void Disable()
        {
            //Arrange
            var context = new Context();
            var wasEvent = new Reference<bool>();
            var disableSystem = new DisableSystemStub();

            context.AddSystem(disableSystem);
            context.OnDisabled += () => wasEvent.value = true;

            //Act
            context.Init();
            context.Enable();
            context.Disable();

            //Assert
            Assert.IsTrue(disableSystem.disabled);
            Assert.IsTrue(wasEvent.value);
            Assert.IsFalse(context.Enabled);
        }
        
        [Test]
        public void Dispose()
        {
            //Arrange
            var context = new Context();
            var wasEvent = new Reference<bool>();
            var systemStub = new DisposeSystemStub();

            context.AddSystem(systemStub);
            context.OnDisposed += () => wasEvent.value = true;

            //Act
            context.Init();
            context.Enable();
            context.Dispose();

            //Assert
            Assert.IsTrue(systemStub.destroyed);
            Assert.IsTrue(wasEvent.value);
            
            Assert.IsFalse(context.Enabled);
            Assert.IsFalse(context.Initialized);
        }

        [Test]
        public void Update()
        {
            //Arrange
            var context = new Context();
            var updateSystem = new UpdateSystemStub();
            context.AddSystem(updateSystem);

            //Act
            context.Init();
            context.Enable();

            context.OnUpdate(deltaTime: 0);
            context.OnFixedUpdate(deltaTime: 0);
            context.OnLateUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(updateSystem.updated);
            Assert.IsTrue(updateSystem.fixedUpdated);
            Assert.IsTrue(updateSystem.lateUpdated);
        }

        [Test]
        public void WhenEnableNotInitializedContextThenFailed()
        {
            //Arrange
            var context = new Context("123");
            var wasEvent = new Reference<bool>();
            var enableSystem = new EnableSystemStub();

            context.AddSystem(enableSystem);
            context.OnEnabled += () => wasEvent.value = true;

            //Act
            context.Enable();

            //Assert
            Assert.IsFalse(context.Initialized);
            Assert.IsFalse(context.Enabled);
            Assert.IsFalse(wasEvent.value);
            Assert.IsFalse(enableSystem.enabled);
            LogAssert.Expect(LogType.Error, $"You can enable context only after initialize! Context: {context.Name}");
        }

        [Test]
        public void WhenUpdateNotEnabledContextThenFailed()
        {
            //Arrange
            var context = new Context("123");
            var updateSystem = new UpdateSystemStub();
            context.AddSystem(updateSystem);

            //Act
            context.OnUpdate(deltaTime: 0);
            context.OnFixedUpdate(deltaTime: 0);
            context.OnLateUpdate(deltaTime: 0);

            //Assert
            Assert.IsFalse(updateSystem.updated);
            Assert.IsFalse(updateSystem.fixedUpdated);
            Assert.IsFalse(updateSystem.lateUpdated);
            
            LogAssert.Expect(LogType.Error, $"You can update context if it's enabled! Context {context.Name}");
            LogAssert.Expect(LogType.Error, $"You can update context if it's enabled! Context {context.Name}");
            LogAssert.Expect(LogType.Error, $"You can update context if it's enabled! Context {context.Name}");
        }

        #endregion

        #region Values

        [Test]
        public void AddValue()
        {
            //Arrange:
            var context = new Context();
            var wasEvent = new Reference<(int, object)>();
            context.OnValueAdded += (key, value) => wasEvent.value = (key, value);

            //Act:
            bool success = context.AddValue(1, "Apple");

            //Assert: 
            Assert.IsTrue(success);
            Assert.IsTrue(context.HasValue(1));
            Assert.AreEqual(1, wasEvent.value.Item1);
            Assert.AreEqual("Apple", wasEvent.value.Item2);
        }

        [Test]
        public void GetValue()
        {
            //Arrange:
            var context = new Context();
            context.AddValue(1, "Apple");

            //Act:
            string data = context.GetValue<string>(1);

            //Assert: 
            Assert.AreEqual("Apple", data);
        }

        [Test]
        public void GetNullValue()
        {
            //Arrange:
            var context = new Context();

            //Act
            string data = context.GetValue<string>(1);

            //Assert
            Assert.IsNull(data);
        }

        [Test]
        public void WhenAddValueThatAlreadyExistsThenFailed()
        {
            //Arrange:
            var context = new Context();
            context.AddValue(1, "Apple");

            var wasEvent = new Reference<bool>();
            context.OnValueAdded += (_, _) => wasEvent.value = true;

            //Act:
            bool success = context.AddValue(1, "Apple");

            //Assert: 
            Assert.IsFalse(success);
            Assert.IsFalse(wasEvent.value);
        }

        #endregion

        #region Systems

        [Test]
        public void AddSystem()
        {
            //Arrange:
            var context = new Context();
            var wasEvent = new Reference<IContextSystem>();
            var systemStub = new SystemStub();
            context.OnSystemAdded += system => wasEvent.value = system;

            //Act:
            bool success = context.AddSystem(systemStub);

            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(context.HasSystem(systemStub));
            Assert.IsTrue(context.HasSystem<SystemStub>());

            Assert.AreEqual(systemStub, wasEvent.value);
            Assert.AreEqual(systemStub, context.GetSystem<SystemStub>());
        }

        [Test]
        public void RemoveSystem()
        {
            //Arrange:
            var context = new Context();
            var wasEvent = new Reference<IContextSystem>();
            var systemStub = new SystemStub();
            context.AddSystem(systemStub);
            context.OnSystemRemoved += system => wasEvent.value = system;

            //Act:
            bool removed = context.DelSystem<SystemStub>();

            //Assert:
            Assert.IsTrue(removed);
            Assert.IsFalse(context.HasSystem<SystemStub>());
            Assert.AreEqual(systemStub, wasEvent.value);
        }

        [Test]
        public void WhenAddAndRemoveSystemOnEnableContextThenSystemWillListenCallbacks()
        {
            //Arrange:
            var context = new Context();
            context.Init();
            context.Enable();

            //Act:
            var systemStub = new CommonSystemStub();
            context.AddSystem(systemStub);

            context.OnUpdate(deltaTime: 0);
            context.OnFixedUpdate(deltaTime: 0);
            context.OnLateUpdate(deltaTime: 0);

            context.DelSystem<CommonSystemStub>();

            //Assert:
            Assert.IsTrue(systemStub.initialized);
            Assert.IsTrue(systemStub.enabled);
            Assert.IsTrue(systemStub.updated);
            Assert.IsTrue(systemStub.fixedUpdated);
            Assert.IsTrue(systemStub.lateUpdated);
            Assert.IsTrue(systemStub.disabled);
            Assert.IsTrue(systemStub.destroyed);
        }

        #endregion

        #region Parent

        [Test]
        public void SetParent()
        {
            //Arrange:
            var parent = new Context();

            //Act:
            var child = new Context(null, parent);

            //Assert:
            Assert.IsTrue(child.IsParent(parent));
        }

        [Test]
        public void ChangeParent()
        {
            //Arrange:
            var parent = new Context();
            var parent2 = new Context();
            var child = new Context(null, parent);

            //Act:
            child.Parent = parent2;

            //Assert:
            Assert.IsTrue(child.IsParent(parent2));
            Assert.IsFalse(child.IsParent(parent));
        }

     //

        #endregion

        #region Resolve

        [Test]
        public void ResolveValueInParent()
        {
            //Arrange:
            var parent = new Context();
            parent.AddValue(1, "Apple");

            var child = new Context(null, parent);

            //Act:
            string data = child.ResolveValue<string>(1);

            //Assert:
            Assert.AreEqual("Apple", data);
        }

        [Test]
        public void GetValueInParentAndSelf()
        {
            //Arrange:
            var context = new Context();
            context.AddValue(1, "Apple");

            //Act:
            string data = context.ResolveValue<string>(1);

            //Assert:
            Assert.AreEqual("Apple", data);
        }

        [Test]
        public void ResolveValueInParentFailed()
        {
            //Arrange:
            var parent = new Context();
            var child = new Context(null, parent);
            
            //Act:
            string data = child.ResolveValue<string>(1);

            //Assert:
            Assert.IsNull(data);
        }

        #endregion

        #region Inject

        [Test]
        public void InjectByField()
        {
            //Arrange:
            var ctx = new Context("123");
            ctx.AddValue(1, "Vasya");

            var stub = new InjectStub1();

            //Act:
            ctx.Inject(stub);
            
            //Assert:
            Assert.AreEqual("Vasya", stub.name);
        }

        [Test]
        public void InjectByMethod()
        {
            //Arrange:
            var ctx = new Context("123");
            ctx.AddValue(1, "Vasya");

            var stub = new InjectStub2();

            //Act:
            ctx.Inject(stub);
            
            //Assert:
            Assert.AreEqual("Vasya", stub.name);
        }
        
        [Test]
        public void InjectByProperty()
        {
            //Arrange:
            var ctx = new Context("123");
            ctx.AddValue(1, "Vasya");

            var stub = new InjectStub3();

            //Act:
            ctx.Inject(stub);
            
            //Assert:
            Assert.AreEqual("Vasya", stub.name);
        }

        #endregion
    }
}
