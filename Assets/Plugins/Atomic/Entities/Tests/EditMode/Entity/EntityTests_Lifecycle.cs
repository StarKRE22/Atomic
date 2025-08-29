using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        #region OnSpawned

        [Test]
        public void OnSpawned_IsInvoked_WhenEntityIsSpawned()
        {
            var entity = new Entity();
            bool wasCalled = false;

            entity.OnInitialized += () => wasCalled = true;

            entity.Spawn(); // Предполагаемый метод

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnSpawned_IsNotInvoked_WhenNotSpawned()
        {
            var entity = new Entity();
            bool wasCalled = false;

            entity.OnInitialized += () => wasCalled = true;

            // Spawn не вызываем

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnSpawned_IsInvokedOnlyOnce_WhenSpawnCalledMultipleTimes()
        {
            var entity = new Entity();
            int callCount = 0;

            entity.OnInitialized += () => callCount++;

            entity.Spawn();
            entity.Spawn();
            entity.Spawn();

            Assert.AreEqual(1, callCount);
        }

        #endregion

        #region OnEnabled

        [Test]
        public void OnEnabled_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = new Entity();
            bool wasCalled = false;

            entity.OnEnabled += () => wasCalled = true;

            entity.Enable(); // Предполагаемый метод

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnEnabled_IsNotInvoked_WhenAlreadyEnabled()
        {
            var entity = new Entity();
            entity.Enable(); // уже включено

            bool wasCalled = false;
            entity.OnEnabled += () => wasCalled = true;

            entity.Enable(); // попытка включить снова

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnEnabled_IsNotInvoked_WhenEntityRemainsDisabled()
        {
            var entity = new Entity(); // по умолчанию выключен?

            bool wasCalled = false;
            entity.OnEnabled += () => wasCalled = true;

            // не включаем

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region OnDisabled

        [Test]
        public void OnDisabled_IsInvoked_WhenEntityIsDisabled()
        {
            var entity = new Entity();
            entity.Spawn(); // сначала нужно заспаунить
            entity.Enable();

            bool wasCalled = false;
            entity.OnDisabled += () => wasCalled = true;

            entity.Disable();

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnDisabled_IsNotInvoked_IfEntityNotEnabled()
        {
            var entity = new Entity();
            entity.Spawn(); // только спаун, не включаем

            bool wasCalled = false;
            entity.OnDisabled += () => wasCalled = true;

            entity.Disable(); // вызов Disable на уже выключенном

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnDisabled_IsInvoked_OnlyOnce_WhenCalledMultipleTimes()
        {
            var entity = new Entity();
            entity.Spawn();
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
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            entity.Disable();

            Assert.IsFalse(entity.Enabled);
        }

        #endregion

        #region OnDespawned

        [Test]
        public void OnDespawned_IsInvoked_WhenEntityIsDespawned()
        {
            var entity = new Entity();
            entity.Spawn();

            bool wasCalled = false;
            entity.OnDespawned += () => wasCalled = true;

            entity.Despawn();

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnDespawned_IsNotInvoked_WhenNotSpawned()
        {
            var entity = new Entity(); // не спаунен

            bool wasCalled = false;
            entity.OnDespawned += () => wasCalled = true;

            entity.Despawn();

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnDespawned_IsInvoked_OnlyOnce_WhenDespawnCalledMultipleTimes()
        {
            var entity = new Entity();
            entity.Spawn();

            int callCount = 0;
            entity.OnDespawned += () => callCount++;

            entity.Despawn();
            entity.Despawn();
            entity.Despawn();

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Despawn_ChangesSpawnedStateToFalse()
        {
            var entity = new Entity();
            entity.Spawn();

            entity.Despawn();

            Assert.IsFalse(entity.Initialized);
        }

        #endregion

        #region OnUpdated

        [Test]
        public void OnUpdated_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            float receivedDelta = -1f;
            entity.OnUpdated += dt => receivedDelta = dt;

            entity.OnUpdate(0.123f);

            Assert.AreEqual(0.123f, receivedDelta);
        }

        [Test]
        public void OnUpdated_IsNotInvoked_WhenEntityIsDisabled()
        {
            var entity = new Entity();
            entity.Spawn();
            // не вызываем Enable()

            bool wasCalled = false;
            entity.OnUpdated += _ => wasCalled = true;

            entity.OnUpdate(0.05f);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnUpdated_CanBeCalledMultipleTimes()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            int callCount = 0;
            entity.OnUpdated += _ => callCount++;

            entity.OnUpdate(0.016f);
            entity.OnUpdate(0.016f);
            entity.OnUpdate(0.016f);

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void OnUpdate_DoesNothing_WhenNotSpawned()
        {
            var entity = new Entity(); // не вызываем Spawn()

            bool wasCalled = false;
            entity.OnUpdated += _ => wasCalled = true;

            entity.OnUpdate(0.1f);

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region OnFixedUpdated

        [Test]
        public void OnFixedUpdated_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            float receivedDelta = -1f;
            entity.OnFixedUpdated += dt => receivedDelta = dt;

            entity.OnFixedUpdate(0.02f);

            Assert.AreEqual(0.02f, receivedDelta);
        }

        [Test]
        public void OnFixedUpdated_IsNotInvoked_WhenEntityIsDisabled()
        {
            var entity = new Entity();
            entity.Spawn(); // Не включаем

            bool wasCalled = false;
            entity.OnFixedUpdated += _ => wasCalled = true;

            entity.OnFixedUpdate(0.02f);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnFixedUpdated_CanBeCalledMultipleTimes()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            int callCount = 0;
            entity.OnFixedUpdated += _ => callCount++;

            entity.OnFixedUpdate(0.02f);
            entity.OnFixedUpdate(0.02f);
            entity.OnFixedUpdate(0.02f);

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void OnFixedUpdate_DoesNothing_WhenNotSpawned()
        {
            var entity = new Entity(); // Не вызываем Spawn()

            bool wasCalled = false;
            entity.OnFixedUpdated += _ => wasCalled = true;

            entity.OnFixedUpdate(0.02f);

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region OnLatedUpdate

        [Test]
        public void OnLateUpdated_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            float receivedDelta = -1f;
            entity.OnLateUpdated += dt => receivedDelta = dt;

            entity.OnLateUpdate(0.033f);

            Assert.AreEqual(0.033f, receivedDelta);
        }

        [Test]
        public void OnLateUpdated_IsNotInvoked_WhenEntityIsDisabled()
        {
            var entity = new Entity();
            entity.Spawn(); // не вызываем Enable()

            bool wasCalled = false;
            entity.OnLateUpdated += _ => wasCalled = true;

            entity.OnLateUpdate(0.033f);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnLateUpdated_CanBeCalledMultipleTimes()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            int callCount = 0;
            entity.OnLateUpdated += _ => callCount++;

            entity.OnLateUpdate(0.033f);
            entity.OnLateUpdate(0.033f);
            entity.OnLateUpdate(0.033f);

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void OnLateUpdate_DoesNothing_WhenNotSpawned()
        {
            var entity = new Entity(); // не спаунен

            bool wasCalled = false;
            entity.OnLateUpdated += _ => wasCalled = true;

            entity.OnLateUpdate(0.033f);

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region Spawned

        [Test]
        public void Spawned_IsFalse_ByDefault()
        {
            var entity = new Entity();
            Assert.IsFalse(entity.Initialized);
        }

        [Test]
        public void Spawned_IsTrue_AfterSpawn()
        {
            var entity = new Entity();
            entity.Spawn();

            Assert.IsTrue(entity.Initialized);
        }

        [Test]
        public void Spawned_IsFalse_AfterDespawn()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Despawn();

            Assert.IsFalse(entity.Initialized);
        }

        [Test]
        public void Spawned_RemainsTrue_OnMultipleSpawnCalls()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Spawn(); // повторный вызов

            Assert.IsTrue(entity.Initialized);
        }

        #endregion

        #region Enabled

        [Test]
        public void Enabled_IsFalse_ByDefault()
        {
            var entity = new Entity();
            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Enabled_IsTrue_AfterEnable()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            Assert.IsTrue(entity.Enabled);
        }

        [Test]
        public void Enabled_IsFalse_AfterDisable()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();
            entity.Disable();

            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Enabled_IsFalse_AfterDespawn()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();
            entity.Despawn(); // вызывает Disable()

            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Enabled_RemainsTrue_OnMultipleEnableCalls()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();
            entity.Enable(); // повторный вызов

            Assert.IsTrue(entity.Enabled);
        }

        [Test]
        public void Enabled_RemainsFalse_OnMultipleDisableCalls()
        {
            var entity = new Entity();
            entity.Spawn();
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
            var entity = new Entity();
            entity.Spawn();

            Assert.IsTrue(entity.Initialized);
        }

        [Test]
        public void Spawn_DoesNothing_IfAlreadySpawned()
        {
            var entity = new Entity();
            entity.Spawn();

            bool called = false;
            entity.OnInitialized += () => called = true;

            entity.Spawn(); // второй вызов

            Assert.IsFalse(called); // не должно вызваться второй раз
        }

        [Test]
        public void Spawn_InvokesOnSpawned()
        {
            var entity = new Entity();

            bool called = false;
            entity.OnInitialized += () => called = true;

            entity.Spawn();

            Assert.IsTrue(called);
        }

        [Test]
        public void Spawn_InvokesIEntitySpawnInterfaces()
        {
            var stub = new EntityInitStub();
            var entity = new Entity();

            entity.AddBehaviour(stub);
            entity.Spawn();

            Assert.IsTrue(stub.WasSpawn);
        }

        [Test]
        public void Spawn_InvokesOnStateChanged()
        {
            var entity = new Entity();

            bool called = false;
            entity.OnStateChanged += () => called = true;

            entity.Spawn();

            Assert.IsTrue(called);
        }

        [Test]
        public void Spawn()
        {
            //Arrange
            var entity = new Entity();
            var wasEvent = false;
            var behaviourStub = new DummyEntityBehaviour();

            entity.AddBehaviour(behaviourStub);
            entity.OnInitialized += () => wasEvent = true;

            //Act
            entity.Spawn();

            //Assert
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(behaviourStub.Initialized);
        }

        #endregion

        #region Despawn

        [Test]
        public void Despawn_SetsSpawnedFalse()
        {
            var entity = new Entity();
            entity.Spawn();

            entity.Despawn();

            Assert.IsFalse(entity.Initialized);
        }

        [Test]
        public void Despawn_DoesNothing_IfNotSpawned()
        {
            var entity = new Entity(); // Не вызывали Spawn()

            bool called = false;
            entity.OnDespawned += () => called = true;

            entity.Despawn();

            Assert.IsFalse(called); // ничего не произошло
        }

        [Test]
        public void Despawn_DisablesEntity_IfEnabled()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            entity.Despawn();

            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Despawn_InvokesIEntityDespawnBehaviours()
        {
            var stub = new EntityDisposeStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            entity.Despawn();

            Assert.IsTrue(stub.WasDespawn);
        }

        [Test]
        public void Despawn_InvokesOnDespawnedEvent()
        {
            var entity = new Entity();
            entity.Spawn();

            bool called = false;
            entity.OnDespawned += () => called = true;

            entity.Despawn();

            Assert.IsTrue(called);
        }

        [Test]
        public void Despawn_InvokesOnStateChanged()
        {
            var entity = new Entity();
            entity.Spawn();

            bool stateChanged = false;
            entity.OnStateChanged += () => stateChanged = true;

            entity.Despawn();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void Despawn_CalledMultipleTimes_OnlyFirstAffectsState()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Despawn();

            bool called = false;
            entity.OnDespawned += () => called = true;

            entity.Despawn(); // второй вызов не должен триггерить событие

            Assert.IsFalse(called);
        }

        [Test]
        public void Despawn()
        {
            //Arrange
            var entity = new Entity();
            var wasEvent = false;
            var behaviourStub = new DummyEntityBehaviour();

            entity.AddBehaviour(behaviourStub);
            entity.OnDespawned += () => wasEvent = true;

            //Act
            entity.Spawn();
            entity.Enable();
            entity.Despawn();

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
            var entity = new Entity();
            entity.Spawn();

            entity.Enable();

            Assert.IsTrue(entity.Enabled);
        }

        [Test]
        public void Enable_CallsSpawn_IfNotSpawned()
        {
            var entity = new Entity();
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
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            entity.Enable();

            Assert.IsTrue(stub.WasEnable);
        }

        [Test]
        public void Enable_InvokesOnEnabledEvent()
        {
            var entity = new Entity();
            entity.Spawn();

            bool called = false;
            entity.OnEnabled += () => called = true;

            entity.Enable();

            Assert.IsTrue(called);
        }

        [Test]
        public void Enable_InvokesOnStateChanged()
        {
            var entity = new Entity();
            entity.Spawn();

            bool stateChanged = false;
            entity.OnStateChanged += () => stateChanged = true;

            entity.Enable();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void Enable_DoesNotCallTwice()
        {
            var entity = new Entity();
            entity.Spawn();
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
            var entity = new Entity();
            var initEvent = false;
            var enabledEvent = false;
            var behaviourStub = new DummyEntityBehaviour();

            entity.AddBehaviour(behaviourStub);
            entity.OnInitialized += () => initEvent = true;
            entity.OnEnabled += () => enabledEvent = true;

            //Act
            entity.Spawn();
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
            var entity = new Entity();
            var wasEvent = false;
            var behaviourStub = new DummyEntityBehaviour();

            entity.AddBehaviour(behaviourStub);
            entity.OnDisabled += () => wasEvent = true;

            //Act
            entity.Spawn();
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
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            entity.Disable();

            Assert.IsFalse(entity.Enabled);
        }

        [Test]
        public void Disable_InvokesIEntityDisable()
        {
            var stub = new EntityDisableStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            entity.Enable();
            entity.Disable();

            Assert.IsTrue(stub.WasDisable);
        }

        [Test]
        public void Disable_InvokesOnDisabledEvent()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            bool called = false;
            entity.OnDisabled += () => called = true;

            entity.Disable();

            Assert.IsTrue(called);
        }

        [Test]
        public void Disable_InvokesOnStateChanged()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            bool stateChanged = false;
            entity.OnStateChanged += () => stateChanged = true;

            entity.Disable();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void Disable_DoesNothing_IfNotEnabled()
        {
            var entity = new Entity();
            entity.Spawn();

            bool wasCalled = false;
            entity.OnDisabled += () => wasCalled = true;

            entity.Disable(); // entity.Enabled == false

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void Disable_CanBeCalledMultipleTimes_Safely()
        {
            var entity = new Entity();
            entity.Spawn();
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
            var entity = new Entity();
            var behaviourStub = new DummyEntityBehaviour();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnUpdated += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.OnUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.Updated);
            Assert.IsTrue(wasUpdate);
        }

        [Test]
        public void OnUpdate_DoesNothing_IfEntityNotEnabled()
        {
            var stub = new EntityUpdateStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn(); // но не Enable()
            entity.OnUpdate(0.5f);

            Assert.IsFalse(stub.WasUpdated);
        }

        [Test]
        public void OnUpdate_CallsUpdateOnRegisteredBehaviours()
        {
            var stub = new EntityUpdateStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            entity.Enable();
            entity.OnUpdate(0.25f);

            Assert.IsTrue(stub.WasUpdated);
            Assert.AreEqual(0.25f, stub.LastDeltaTime);
        }

        [Test]
        public void OnUpdate_InvokesOnUpdatedEvent()
        {
            var entity = new Entity();
            float calledDelta = -1f;

            entity.OnUpdated += dt => calledDelta = dt;

            entity.Spawn();
            entity.Enable();
            entity.OnUpdate(0.75f);

            Assert.AreEqual(0.75f, calledDelta);
        }

        [Test]
        public void OnUpdate_StopsCallingIfEntityDisabledMidLoop()
        {
            var stub1 = new DisableDuringUpdateStub();
            var stub2 = new EntityUpdateStub();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {stub1, stub2});

            entity.Spawn();
            entity.Enable();

            entity.OnUpdate(0.1f);

            Assert.IsTrue(stub1.WasUpdated);
            Assert.IsFalse(stub2.WasUpdated); // не должен вызваться, т.к. отключились
        }

        #endregion

        #region OnFixedUpdate

        [Test]
        public void OnFixedUpdate_DoesNothing_IfEntityNotEnabled()
        {
            var stub = new EntityFixedUpdateStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn(); // не включаем
            entity.OnFixedUpdate(0.02f);

            Assert.IsFalse(stub.WasCalled);
        }

        [Test]
        public void OnFixedUpdate_CallsRegisteredFixedUpdateBehaviours()
        {
            var stub = new EntityFixedUpdateStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            entity.Enable();

            entity.OnFixedUpdate(0.02f);

            Assert.IsTrue(stub.WasCalled);
            Assert.AreEqual(0.02f, stub.LastDeltaTime);
        }

        [Test]
        public void OnFixedUpdate_InvokesOnFixedUpdatedEvent()
        {
            var entity = new Entity();
            float delta = -1f;

            entity.OnFixedUpdated += dt => delta = dt;

            entity.Spawn();
            entity.Enable();

            entity.OnFixedUpdate(0.05f);

            Assert.AreEqual(0.05f, delta);
        }

        [Test]
        public void OnFixedUpdate_StopsIfEntityDisabledMidIteration()
        {
            var stub1 = new DisableDuringFixedUpdateStub();
            var stub2 = new EntityFixedUpdateStub();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {stub1, stub2});

            entity.Spawn();
            entity.Enable();

            entity.OnFixedUpdate(0.03f);

            Assert.IsTrue(stub1.WasCalled);
            Assert.IsFalse(stub2.WasCalled); // не вызван, т.к. entity отключён во время
        }

        [Test]
        public void FixedUpdate()
        {
            //Arrange
            var entity = new Entity();
            var behaviourStub = new DummyEntityBehaviour();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnFixedUpdated += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.OnFixedUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.FixedUpdated);
            Assert.IsTrue(wasUpdate);
        }

        #endregion

        #region LateUpdate

        [Test]
        public void OnLateUpdate_DoesNothing_WhenEntityDisabled()
        {
            var stub = new EntityLateUpdateStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            // not enabling
            entity.OnLateUpdate(0.04f);

            Assert.IsFalse(stub.WasCalled);
        }

        [Test]
        public void OnLateUpdate_CallsRegisteredLateUpdateBehaviours()
        {
            var stub = new EntityLateUpdateStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            entity.Enable();

            entity.OnLateUpdate(0.04f);

            Assert.IsTrue(stub.WasCalled);
            Assert.AreEqual(0.04f, stub.LastDeltaTime);
        }

        [Test]
        public void OnLateUpdate_InvokesOnLateUpdatedEvent()
        {
            var entity = new Entity();
            float calledDelta = -1f;

            entity.OnLateUpdated += dt => calledDelta = dt;

            entity.Spawn();
            entity.Enable();
            entity.OnLateUpdate(0.06f);

            Assert.AreEqual(0.06f, calledDelta);
        }

        [Test]
        public void OnLateUpdate_StopsIteration_WhenDisabledMidUpdate()
        {
            var stub1 = new DisableDuringLateUpdateStub();
            var stub2 = new EntityLateUpdateStub();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {stub1, stub2});

            entity.Spawn();
            entity.Enable();
            entity.OnLateUpdate(0.07f);

            Assert.IsTrue(stub1.WasCalled);
            Assert.IsFalse(stub2.WasCalled);
        }


        [Test]
        public void LateUpdate()
        {
            //Arrange
            var entity = new Entity();
            var behaviourStub = new DummyEntityBehaviour();
            var wasUpdate = false;

            entity.AddBehaviour(behaviourStub);
            entity.OnLateUpdated += _ => wasUpdate = true;

            //Act
            entity.Enable();
            entity.OnLateUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.LateUpdated);
            Assert.IsTrue(wasUpdate);
        }

        #endregion
    }
}