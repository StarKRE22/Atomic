using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public sealed partial class SceneEntityTests
    {
        #region OnInitialized

        [Test]
        public void OnInitialized_IsInvoked_WhenEntityIsSpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            bool wasCalled = false;

            entity.OnInitialized += () => wasCalled = true;

            entity.Init(); // Предполагаемый метод

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnInitialized_IsNotInvoked_WhenNotSpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            bool wasCalled = false;

            entity.OnInitialized += () => wasCalled = true;

            // Spawn не вызываем

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnInitialized_IsInvokedOnlyOnce_WhenSpawnCalledMultipleTimes()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            int callCount = 0;

            entity.OnInitialized += () => callCount++;

            entity.Init();
            entity.Init();
            entity.Init();

            Assert.AreEqual(1, callCount);
        }

        #endregion

        #region OnEnabled

        [Test]
        public void OnEnabled_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            bool wasCalled = false;

            entity.OnEnabled += () => wasCalled = true;

            entity.Enable(); // Предполагаемый метод

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnEnabled_IsNotInvoked_WhenAlreadyEnabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Enable(); // уже включено

            bool wasCalled = false;
            entity.OnEnabled += () => wasCalled = true;

            entity.Enable(); // попытка включить снова

            Assert.IsFalse(wasCalled);
        }

        [UnityTest]
        public IEnumerator OnEnabled_IsNotInvoked_WhenEntityRemainsDisabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false); // по умолчанию выключен

            bool wasCalled = false;
            entity.OnEnabled += () => wasCalled = true;

            //Wait awake
            yield return null;

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region OnDisabled

        [Test]
        public void OnDisabled_IsInvoked_WhenEntityIsDisabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init(); // сначала нужно заспаунить
            entity.Enable();

            bool wasCalled = false;
            entity.OnDisabled += () => wasCalled = true;

            entity.Disable();

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnDisabled_IsNotInvoked_IfEntityNotEnabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init(); // только спаун, не включаем

            bool wasCalled = false;
            entity.OnDisabled += () => wasCalled = true;

            entity.Disable(); // вызов Disable на уже выключенном

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnDisabled_IsInvoked_OnlyOnce_WhenCalledMultipleTimes()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            int callCount = 0;
            entity.OnDisabled += () => callCount++;

            entity.Disable();
            entity.Disable();
            entity.Disable();

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Disable_ChangesEnabledStateToFalse()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            entity.Disable();

            Assert.IsFalse(entity.Enabled);
        }

        #endregion

        #region OnDisposed

        [Test]
        public void OnDisposed_IsInvoked_WhenEntityIsDisposeed()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            bool wasCalled = false;
            entity.OnDisposed += () => wasCalled = true;

            entity.Dispose();

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnDisposed_IsNotInvoked_WhenNotSpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false); // не спаунен

            bool wasCalled = false;
            entity.OnDisposed += () => wasCalled = true;

            entity.Dispose();

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnDisposed_IsInvoked_OnlyOnce_WhenDisposeCalledMultipleTimes()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            int callCount = 0;
            entity.OnDisposed += () => callCount++;

            entity.Dispose();
            entity.Dispose();
            entity.Dispose();

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Dispose_ChangesSpawnedStateToFalse()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            entity.Dispose();

            Assert.IsFalse(entity.Initialized);
        }

        #endregion

        #region OnUpdated

        [Test]
        public void OnUpdated_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            float receivedDelta = -1f;
            entity.OnTicked += dt => receivedDelta = dt;

            entity.Tick(0.123f);

            Assert.AreEqual(0.123f, receivedDelta);
        }

        [Test]
        public void OnUpdated_IsNotInvoked_WhenEntityIsDisabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            // не вызываем Enable()

            bool wasCalled = false;
            entity.OnTicked += _ => wasCalled = true;

            entity.Tick(0.05f);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnUpdated_CanBeCalledMultipleTimes()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            int callCount = 0;
            entity.OnTicked += _ => callCount++;

            entity.Tick(0.016f);
            entity.Tick(0.016f);
            entity.Tick(0.016f);

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void OnUpdate_DoesNothing_WhenNotSpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false); // не вызываем Spawn()

            bool wasCalled = false;
            entity.OnTicked += _ => wasCalled = true;

            entity.Tick(0.1f);

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region OnFixedUpdated

        [Test]
        public void OnFixedUpdated_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            float receivedDelta = -1f;
            entity.OnFixedTicked += dt => receivedDelta = dt;

            entity.FixedTick(0.02f);

            Assert.AreEqual(0.02f, receivedDelta);
        }

        [Test]
        public void OnFixedUpdated_IsNotInvoked_WhenEntityIsDisabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init(); // Не включаем

            bool wasCalled = false;
            entity.OnFixedTicked += _ => wasCalled = true;

            entity.FixedTick(0.02f);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnFixedUpdated_CanBeCalledMultipleTimes()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            int callCount = 0;
            entity.OnFixedTicked += _ => callCount++;

            entity.FixedTick(0.02f);
            entity.FixedTick(0.02f);
            entity.FixedTick(0.02f);

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void OnFixedUpdate_DoesNothing_WhenNotSpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false); // Не вызываем Spawn()

            bool wasCalled = false;
            entity.OnFixedTicked += _ => wasCalled = true;

            entity.FixedTick(0.02f);

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region OnLatedUpdate

        [Test]
        public void OnLateUpdated_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            float receivedDelta = -1f;
            entity.OnLateTicked += dt => receivedDelta = dt;

            entity.LateTick(0.033f);

            Assert.AreEqual(0.033f, receivedDelta);
        }

        [Test]
        public void OnLateUpdated_IsNotInvoked_WhenEntityIsDisabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init(); // не вызываем Enable()

            bool wasCalled = false;
            entity.OnLateTicked += _ => wasCalled = true;

            entity.LateTick(0.033f);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnLateUpdated_CanBeCalledMultipleTimes()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            int callCount = 0;
            entity.OnLateTicked += _ => callCount++;

            entity.LateTick(0.033f);
            entity.LateTick(0.033f);
            entity.LateTick(0.033f);

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void OnLateUpdate_DoesNothing_WhenNotSpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false); // не спаунен

            bool wasCalled = false;
            entity.OnLateTicked += _ => wasCalled = true;

            entity.LateTick(0.033f);

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region Spawned

        [Test]
        public void Spawned_IsFalse_ByDefault()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            Assert.IsFalse(entity.Initialized);
        }

        [Test]
        public void Spawned_IsTrue_AfterSpawn()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            Assert.IsTrue(entity.Initialized);
        }

        [Test]
        public void Spawned_IsFalse_AfterDispose()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Dispose();

            Assert.IsFalse(entity.Initialized);
        }

        [Test]
        public void Spawned_RemainsTrue_OnMultipleSpawnCalls()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Init(); // повторный вызов

            Assert.IsTrue(entity.Initialized);
        }

        #endregion

        #region Enabled

        [Test]
        public void Enabled_IsFalse_ByDefault()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Enabled_IsTrue_AfterEnable()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            Assert.IsTrue(entity.Enabled);
        }

        [Test]
        public void Enabled_IsFalse_AfterDisable()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();
            entity.Disable();

            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Enabled_IsFalse_AfterDispose()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();
            entity.Dispose(); // вызывает Disable()

            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Enabled_RemainsTrue_OnMultipleEnableCalls()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();
            entity.Enable(); // повторный вызов

            Assert.IsTrue(entity.Enabled);
        }

        [Test]
        public void Enabled_RemainsFalse_OnMultipleDisableCalls()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();
            entity.Disable();
            entity.Disable();

            Assert.IsFalse(entity.Enabled);
        }

        #endregion

        #region Spawn

        [Test]
        public void Spawn_SetsSpawnedTrue()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            Assert.IsTrue(entity.Initialized);
        }

        [Test]
        public void Spawn_DoesNothing_IfAlreadySpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            bool called = false;
            entity.OnInitialized += () => called = true;

            entity.Init(); // второй вызов

            Assert.IsFalse(called); // не должно вызваться второй раз
        }

        [Test]
        public void Spawn_InvokesOnInitialized()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);

            bool called = false;
            entity.OnInitialized += () => called = true;

            entity.Init();

            Assert.IsTrue(called);
        }

        [Test]
        public void Spawn_InvokesIEntitySpawnInterfaces()
        {
            var stub = new EntityInitStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);

            entity.AddBehaviour(stub);
            entity.Init();

            Assert.IsTrue(stub.WasInit);
        }

        [Test]
        public void Spawn_InvokesOnStateChanged()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);

            bool called = false;
            entity.OnStateChanged += _ => called = true;

            entity.Init();

            Assert.IsTrue(called);
        }

        [Test]
        public void Spawn()
        {
            //Arrange
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            var wasEvent = false;
            var behaviourStub = new EntityBehaviourStub();

            entity.AddBehaviour(behaviourStub);
            entity.OnInitialized += () => wasEvent = true;

            //Act
            entity.Init();

            //Assert
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(behaviourStub.Initialized);
        }

        #endregion

        #region Dispose

        [Test]
        public void Dispose_SetsSpawnedFalse()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            entity.Dispose();

            Assert.IsFalse(entity.Initialized);
        }

        [Test]
        public void Dispose_DoesNothing_IfNotSpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false); // Не вызывали Spawn()

            bool called = false;
            entity.OnDisposed += () => called = true;

            entity.Dispose();

            Assert.IsFalse(called); // ничего не произошло
        }

        [Test]
        public void Dispose_DisablesEntity_IfEnabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            entity.Dispose();

            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Dispose_InvokesIEntityDisposeBehaviours()
        {
            var stub = new EntityDisposeStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init();
            entity.Dispose();

            Assert.IsTrue(stub.WasDispose);
        }

        [Test]
        public void Dispose_InvokesOnDisposedEvent()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            bool called = false;
            entity.OnDisposed += () => called = true;

            entity.Dispose();

            Assert.IsTrue(called);
        }

        [Test]
        public void Dispose_InvokesOnStateChanged()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            bool stateChanged = false;
            entity.OnStateChanged += _ => stateChanged = true;

            entity.Dispose();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void Dispose_CalledMultipleTimes_OnlyFirstAffectsState()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Dispose();

            bool called = false;
            entity.OnDisposed += () => called = true;

            entity.Dispose(); // второй вызов не должен триггерить событие

            Assert.IsFalse(called);
        }

        [Test]
        public void Dispose()
        {
            //Arrange
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            var wasEvent = false;
            var behaviourStub = new EntityBehaviourStub();

            entity.AddBehaviour(behaviourStub);
            entity.OnDisposed += () => wasEvent = true;

            //Act
            entity.Init();
            entity.Enable();
            entity.Dispose();

            //Assert
            Assert.IsTrue(behaviourStub.Disposed);
            Assert.IsTrue(wasEvent);

            Assert.IsFalse(entity.Enabled);
            Assert.IsFalse(entity.Initialized);
        }

        #endregion

        #region Enable

        [Test]
        public void Enable_SetsEnabledTrue()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            entity.Enable();

            Assert.IsTrue(entity.Enabled);
        }

        [Test]
        public void Enable_CallsSpawn_IfNotSpawned()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            bool spawned = false;
            entity.OnInitialized += () => spawned = true;

            entity.Enable();

            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(spawned);
        }

        [Test]
        public void Enable_InvokesIEntityEnable()
        {
            var stub = new EntityEnableStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init();
            entity.Enable();

            Assert.IsTrue(stub.WasEnable);
        }

        [Test]
        public void Enable_InvokesOnEnabledEvent()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            bool called = false;
            entity.OnEnabled += () => called = true;

            entity.Enable();

            Assert.IsTrue(called);
        }

        [Test]
        public void Enable_InvokesOnStateChanged()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            bool stateChanged = false;
            entity.OnStateChanged += _ => stateChanged = true;

            entity.Enable();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void Enable_DoesNotCallTwice()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            bool wasCalled = false;
            entity.OnEnabled += () => wasCalled = true;

            entity.Enable(); // повторно

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void Enable()
        {
            //Arrange
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            var initEvent = false;
            var enabledEvent = false;
            var behaviourStub = new EntityBehaviourStub();

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

            Assert.IsTrue(behaviourStub.Initialized);
            Assert.IsTrue(behaviourStub.Enabled);

            Assert.AreEqual(nameof(IEntityInit.Init), behaviourStub.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityEnable.Enable), behaviourStub.InvocationList[1]);
        }

        #endregion

        #region Disable

        [Test]
        public void Disable()
        {
            //Arrange
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            var wasEvent = false;
            var behaviourStub = new EntityBehaviourStub();

            entity.AddBehaviour(behaviourStub);
            entity.OnDisabled += () => wasEvent = true;

            //Act
            entity.Init();
            entity.Enable();
            entity.Disable();

            //Assert
            Assert.IsTrue(behaviourStub.Disabled);
            Assert.IsTrue(wasEvent);
            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Disable_SetsEnabledFalse()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            entity.Disable();

            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Disable_InvokesIEntityDisable()
        {
            var stub = new EntityDisableStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init();
            entity.Enable();
            entity.Disable();

            Assert.IsTrue(stub.WasDisable);
        }

        [Test]
        public void Disable_InvokesOnDisabledEvent()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            bool called = false;
            entity.OnDisabled += () => called = true;

            entity.Disable();

            Assert.IsTrue(called);
        }

        [Test]
        public void Disable_InvokesOnStateChanged()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();

            bool stateChanged = false;
            entity.OnStateChanged += _ => stateChanged = true;

            entity.Disable();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void Disable_DoesNothing_IfNotEnabled()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();

            bool wasCalled = false;
            entity.OnDisabled += () => wasCalled = true;

            entity.Disable(); // entity.Enabled == false

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void Disable_CanBeCalledMultipleTimes_Safely()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.Init();
            entity.Enable();
            entity.Disable();

            bool called = false;
            entity.OnDisabled += () => called = true;

            entity.Disable(); // второй вызов

            Assert.IsFalse(called); // событие не должно вызываться второй раз
        }

        #endregion

        #region OnUpdate

        [Test]
        public void Update()
        {
            //Arrange
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            var behaviourStub = new EntityBehaviourStub();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnTicked += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.Tick(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.Updated);
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void OnUpdate_DoesNothing_IfEntityNotEnabled()
        {
            var stub = new EntityTickStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init(); // но не Enable()
            entity.Tick(0.5f);

            Assert.IsFalse(stub.WasUpdated);
        }

        [Test]
        public void OnUpdate_CallsUpdateOnRegisteredBehaviours()
        {
            var stub = new EntityTickStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init();
            entity.Enable();
            entity.Tick(0.25f);

            Assert.IsTrue(stub.WasUpdated);
            Assert.AreEqual(0.25f, stub.LastDeltaTime);
        }

        [Test]
        public void OnUpdate_InvokesOnUpdatedEvent()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            float calledDelta = -1f;

            entity.OnTicked += dt => calledDelta = dt;

            entity.Init();
            entity.Enable();
            entity.Tick(0.75f);

            Assert.AreEqual(0.75f, calledDelta);
        }

        [Test]
        public void OnUpdate_StopsCallingIfEntityDisabledMidLoop()
        {
            var stub1 = new DisableDuringTickStub();
            var stub2 = new EntityTickStub();

            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviours(new IEntityBehaviour[] {stub1, stub2});

            entity.Init();
            entity.Enable();

            entity.Tick(0.1f);

            Assert.IsTrue(stub1.WasUpdated);
            Assert.IsFalse(stub2.WasUpdated); // не должен вызваться, т.к. отключились
        }

        #endregion

        #region OnFixedUpdate

        [Test]
        public void OnFixedUpdate_DoesNothing_IfEntityNotEnabled()
        {
            var stub = new EntityFixedTickStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init(); // не включаем
            entity.FixedTick(0.02f);

            Assert.IsFalse(stub.WasCalled);
        }

        [Test]
        public void OnFixedUpdate_CallsRegisteredFixedUpdateBehaviours()
        {
            var stub = new EntityFixedTickStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init();
            entity.Enable();

            entity.FixedTick(0.02f);

            Assert.IsTrue(stub.WasCalled);
            Assert.AreEqual(0.02f, stub.LastDeltaTime);
        }

        [Test]
        public void OnFixedUpdate_InvokesOnFixedUpdatedEvent()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            float delta = -1f;

            entity.OnFixedTicked += dt => delta = dt;

            entity.Init();
            entity.Enable();

            entity.FixedTick(0.05f);

            Assert.AreEqual(0.05f, delta);
        }

        [Test]
        public void OnFixedUpdate_StopsIfEntityDisabledMidIteration()
        {
            var stub1 = new DisableDuringFixedTickStub();
            var stub2 = new EntityFixedTickStub();

            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviours(new IEntityBehaviour[] {stub1, stub2});

            entity.Init();
            entity.Enable();

            entity.FixedTick(0.03f);

            Assert.IsTrue(stub1.WasCalled);
            Assert.IsFalse(stub2.WasCalled); // не вызван, т.к. entity отключён во время
        }

        [Test]
        public void FixedUpdate()
        {
            //Arrange
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            var behaviourStub = new EntityBehaviourStub();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnFixedTicked += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.FixedTick(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.FixedUpdated);
            Assert.IsTrue(wasUpdate);
        }

        #endregion
        
        #region LateUpdate

        [Test]
        public void OnLateUpdate_DoesNothing_WhenEntityDisabled()
        {
            var stub = new EntityLateTickStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init();
            // not enabling
            entity.LateTick(0.04f);

            Assert.IsFalse(stub.WasCalled);
        }

        [Test]
        public void OnLateUpdate_CallsRegisteredLateUpdateBehaviours()
        {
            var stub = new EntityLateTickStub();
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviour(stub);

            entity.Init();
            entity.Enable();

            entity.LateTick(0.04f);

            Assert.IsTrue(stub.WasCalled);
            Assert.AreEqual(0.04f, stub.LastDeltaTime);
        }

        [Test]
        public void OnLateUpdate_InvokesOnLateUpdatedEvent()
        {
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            float calledDelta = -1f;

            entity.OnLateTicked += dt => calledDelta = dt;

            entity.Init();
            entity.Enable();
            entity.LateTick(0.06f);

            Assert.AreEqual(0.06f, calledDelta);
        }

        [Test]
        public void OnLateUpdate_StopsIteration_WhenDisabledMidUpdate()
        {
            var stub1 = new DisableDuringLateTickStub();
            var stub2 = new EntityLateTickStub();

            var entity = SceneEntity.Create(useUnityLifecycle: false);
            entity.AddBehaviours(new IEntityBehaviour[] {stub1, stub2});

            entity.Init();
            entity.Enable();
            entity.LateTick(0.07f);

            Assert.IsTrue(stub1.WasCalled);
            Assert.IsFalse(stub2.WasCalled);
        }


        [Test]
        public void LateUpdate()
        {
            //Arrange
            var entity = SceneEntity.Create(useUnityLifecycle: false);
            var behaviourStub = new EntityBehaviourStub();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnLateTicked += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.LateTick(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.LateUpdated);
            Assert.IsTrue(wasUpdate);
        }

        #endregion
    }
}