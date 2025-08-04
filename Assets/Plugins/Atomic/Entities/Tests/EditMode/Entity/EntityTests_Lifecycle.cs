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

            entity.OnSpawned += () => wasCalled = true;

            entity.Spawn(); // Предполагаемый метод

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnSpawned_IsNotInvoked_WhenNotSpawned()
        {
            var entity = new Entity();
            bool wasCalled = false;

            entity.OnSpawned += () => wasCalled = true;

            // Spawn не вызываем

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnSpawned_IsInvokedOnlyOnce_WhenSpawnCalledMultipleTimes()
        {
            var entity = new Entity();
            int callCount = 0;

            entity.OnSpawned += () => callCount++;

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

            entity.OnActivated += () => wasCalled = true;

            entity.Activate(); // Предполагаемый метод

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnEnabled_IsNotInvoked_WhenAlreadyEnabled()
        {
            var entity = new Entity();
            entity.Activate(); // уже включено

            bool wasCalled = false;
            entity.OnActivated += () => wasCalled = true;

            entity.Activate(); // попытка включить снова

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnEnabled_IsNotInvoked_WhenEntityRemainsDisabled()
        {
            var entity = new Entity(); // по умолчанию выключен?

            bool wasCalled = false;
            entity.OnActivated += () => wasCalled = true;

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
            entity.Activate();

            bool wasCalled = false;
            entity.OnInactivated += () => wasCalled = true;

            entity.Inactivate();

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void OnDisabled_IsNotInvoked_IfEntityNotEnabled()
        {
            var entity = new Entity();
            entity.Spawn(); // только спаун, не включаем

            bool wasCalled = false;
            entity.OnInactivated += () => wasCalled = true;

            entity.Inactivate(); // вызов Disable на уже выключенном

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnDisabled_IsInvoked_OnlyOnce_WhenCalledMultipleTimes()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

            int callCount = 0;
            entity.OnInactivated += () => callCount++;

            entity.Inactivate();
            entity.Inactivate();
            entity.Inactivate();

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Disable_ChangesEnabledStateToFalse()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

            entity.Inactivate();

            Assert.IsFalse(entity.IsActive);
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

            Assert.IsFalse(entity.IsSpawned);
        }

        #endregion

        #region OnUpdated

        [Test]
        public void OnUpdated_IsInvoked_WhenEntityIsEnabled()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

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
            entity.Activate();

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
            entity.Activate();

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
            entity.Activate();

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
            entity.Activate();

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
            entity.Activate();

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
            Assert.IsFalse(entity.IsSpawned);
        }

        [Test]
        public void Spawned_IsTrue_AfterSpawn()
        {
            var entity = new Entity();
            entity.Spawn();

            Assert.IsTrue(entity.IsSpawned);
        }

        [Test]
        public void Spawned_IsFalse_AfterDespawn()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Despawn();

            Assert.IsFalse(entity.IsSpawned);
        }

        [Test]
        public void Spawned_RemainsTrue_OnMultipleSpawnCalls()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Spawn(); // повторный вызов

            Assert.IsTrue(entity.IsSpawned);
        }

        #endregion

        #region Enabled

        [Test]
        public void Enabled_IsFalse_ByDefault()
        {
            var entity = new Entity();
            Assert.IsFalse(entity.IsActive);
        }

        [Test]
        public void Enabled_IsTrue_AfterEnable()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

            Assert.IsTrue(entity.IsActive);
        }

        [Test]
        public void Enabled_IsFalse_AfterDisable()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();
            entity.Inactivate();

            Assert.IsFalse(entity.IsActive);
        }

        [Test]
        public void Enabled_IsFalse_AfterDespawn()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();
            entity.Despawn(); // вызывает Disable()

            Assert.IsFalse(entity.IsActive);
        }

        [Test]
        public void Enabled_RemainsTrue_OnMultipleEnableCalls()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();
            entity.Activate(); // повторный вызов

            Assert.IsTrue(entity.IsActive);
        }

        [Test]
        public void Enabled_RemainsFalse_OnMultipleDisableCalls()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();
            entity.Inactivate();
            entity.Inactivate();

            Assert.IsFalse(entity.IsActive);
        }

        #endregion

        #region Spawn

        [Test]
        public void Spawn_SetsSpawnedTrue()
        {
            var entity = new Entity();
            entity.Spawn();

            Assert.IsTrue(entity.IsSpawned);
        }

        [Test]
        public void Spawn_DoesNothing_IfAlreadySpawned()
        {
            var entity = new Entity();
            entity.Spawn();

            bool called = false;
            entity.OnSpawned += () => called = true;

            entity.Spawn(); // второй вызов

            Assert.IsFalse(called); // не должно вызваться второй раз
        }

        [Test]
        public void Spawn_InvokesOnSpawned()
        {
            var entity = new Entity();

            bool called = false;
            entity.OnSpawned += () => called = true;

            entity.Spawn();

            Assert.IsTrue(called);
        }

        [Test]
        public void Spawn_InvokesIEntitySpawnInterfaces()
        {
            var stub = new EntitySpawnStub();
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
            entity.OnSpawned += () => wasEvent = true;

            //Act
            entity.Spawn();

            //Assert
            Assert.IsTrue(entity.IsSpawned);
            Assert.IsTrue(wasEvent);
            Assert.IsTrue(behaviourStub.Spawned);
        }

        #endregion

        #region Despawn

        [Test]
        public void Despawn_SetsSpawnedFalse()
        {
            var entity = new Entity();
            entity.Spawn();

            entity.Despawn();

            Assert.IsFalse(entity.IsSpawned);
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
            entity.Activate();

            entity.Despawn();

            Assert.IsFalse(entity.IsActive);
        }

        [Test]
        public void Despawn_InvokesIEntityDespawnBehaviours()
        {
            var stub = new EntityDespawnStub();
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
            entity.Activate();
            entity.Despawn();

            //Assert
            Assert.IsTrue(behaviourStub.Despawned);
            Assert.IsTrue(wasEvent);

            Assert.IsFalse(entity.IsActive);
            Assert.IsFalse(entity.IsSpawned);
        }

        #endregion

        #region Enable

        [Test]
        public void Enable_SetsEnabledTrue()
        {
            var entity = new Entity();
            entity.Spawn();

            entity.Activate();

            Assert.IsTrue(entity.IsActive);
        }

        [Test]
        public void Enable_CallsSpawn_IfNotSpawned()
        {
            var entity = new Entity();
            bool spawned = false;
            entity.OnSpawned += () => spawned = true;

            entity.Activate();

            Assert.IsTrue(entity.IsSpawned);
            Assert.IsTrue(spawned);
        }

        [Test]
        public void Enable_InvokesIEntityEnable()
        {
            var stub = new EntityActiveStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            entity.Activate();

            Assert.IsTrue(stub.WasEnable);
        }

        [Test]
        public void Enable_InvokesOnEnabledEvent()
        {
            var entity = new Entity();
            entity.Spawn();

            bool called = false;
            entity.OnActivated += () => called = true;

            entity.Activate();

            Assert.IsTrue(called);
        }

        [Test]
        public void Enable_InvokesOnStateChanged()
        {
            var entity = new Entity();
            entity.Spawn();

            bool stateChanged = false;
            entity.OnStateChanged += () => stateChanged = true;

            entity.Activate();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void Enable_DoesNotCallTwice()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

            bool wasCalled = false;
            entity.OnActivated += () => wasCalled = true;

            entity.Activate(); // повторно

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
            entity.OnSpawned += () => initEvent = true;
            entity.OnActivated += () => enabledEvent = true;

            //Act
            entity.Spawn();
            entity.Activate();

            //Assert
            Assert.IsTrue(initEvent);
            Assert.IsTrue(enabledEvent);

            Assert.IsTrue(entity.IsSpawned);
            Assert.IsTrue(entity.IsActive);

            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsTrue(behaviourStub.Activated);

            Assert.AreEqual(nameof(IEntitySpawn.OnSpawn), behaviourStub.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityActive.OnActive), behaviourStub.InvocationList[1]);
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
            entity.OnInactivated += () => wasEvent = true;

            //Act
            entity.Spawn();
            entity.Activate();
            entity.Inactivate();

            //Assert
            Assert.IsTrue(behaviourStub.Deactivated);
            Assert.IsTrue(wasEvent);
            Assert.IsFalse(entity.IsActive);
        }

        [Test]
        public void Disable_SetsEnabledFalse()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

            entity.Inactivate();

            Assert.IsFalse(entity.IsActive);
        }

        [Test]
        public void Disable_InvokesIEntityDisable()
        {
            var stub = new EntityInactiveStub();
            var entity = new Entity();
            entity.AddBehaviour(stub);

            entity.Spawn();
            entity.Activate();
            entity.Inactivate();

            Assert.IsTrue(stub.WasDisable);
        }

        [Test]
        public void Disable_InvokesOnDisabledEvent()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

            bool called = false;
            entity.OnInactivated += () => called = true;

            entity.Inactivate();

            Assert.IsTrue(called);
        }

        [Test]
        public void Disable_InvokesOnStateChanged()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

            bool stateChanged = false;
            entity.OnStateChanged += () => stateChanged = true;

            entity.Inactivate();

            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void Disable_DoesNothing_IfNotEnabled()
        {
            var entity = new Entity();
            entity.Spawn();

            bool wasCalled = false;
            entity.OnInactivated += () => wasCalled = true;

            entity.Inactivate(); // entity.Enabled == false

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void Disable_CanBeCalledMultipleTimes_Safely()
        {
            var entity = new Entity();
            entity.Spawn();
            entity.Activate();
            entity.Inactivate();

            bool called = false;
            entity.OnInactivated += () => called = true;

            entity.Inactivate(); // второй вызов

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
            entity.Activate();
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
            entity.Activate();
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
            entity.Activate();
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
            entity.Activate();

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
            entity.Activate();

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
            entity.Activate();

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
            entity.Activate();

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
            entity.Activate();
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
            entity.Activate();

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
            entity.Activate();
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
            entity.Activate();
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
            entity.Activate();
            entity.OnLateUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(behaviourStub.LateUpdated);
            Assert.IsTrue(wasUpdate);
        }

        #endregion
    }
}