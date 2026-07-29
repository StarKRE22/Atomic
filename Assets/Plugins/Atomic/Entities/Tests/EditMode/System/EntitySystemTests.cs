using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntitySystemTests
    {
        private int _nextId;

        [SetUp]
        public void SetUp()
        {
            _nextId = 1;
            EntityRegistry.Instance.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            EntityRegistry.Instance.Clear();
        }

        // ═══════════════════════════════════════════════════════════════
        //  TEST DOUBLES
        // ═══════════════════════════════════════════════════════════════

        private sealed class TestEntitySystem : EntitySystem<IEntity>
        {
            public List<(IEntity entity, float deltaTime)> UpdateLog { get; } = new();
            public int UpdateCallCount { get; private set; }
            public bool OnEnableCalled { get; private set; }
            public void ResetOnEnableCalled() => OnEnableCalled = false;
            public bool OnDisableCalled { get; private set; }

            public TestEntitySystem(IReadOnlyEntityCollection<IEntity> source, EntitySystemBase<IEntity>.Settings settings)
                : base(source, settings)
            {
            }

            protected override void Update(IEntity entity, float deltaTime)
            {
                UpdateLog.Add((entity, deltaTime));
                UpdateCallCount++;
            }

            protected override void OnEnable()
            {
                OnEnableCalled = true;
            }

            protected override void OnDisable()
            {
                OnDisableCalled = true;
            }
        }

        private static EntitySystemBase<IEntity>.Settings CreateSettings(
            float frameBudget = 1f,
            int minSize = 1,
            int maxSize = 4096,
            int stepUp = 256,
            int scaleDown = 2)
        {
            return new EntitySystemBase<IEntity>.Settings
            {
                frameBudget = frameBudget,
                batching = new EntitySystemBase<IEntity>.Settings.AdaptiveBatching
                {
                    minSize = minSize,
                    maxSize = maxSize,
                    stepUp = stepUp,
                    scaleDown = scaleDown
                }
            };
        }

        private IEntity CreateEntity(string name = null)
        {
            var entity = new EntityDummy { Name = name ?? $"Entity_{_nextId}" };
            ((IEntity)entity).InstanceID = _nextId++;
            return entity;
        }

        /// <summary>
        /// Warms up the adaptive batch size. The first Update call always starts
        /// with batchSize=0 (processes nothing) and ramps the batch size to stepUp.
        /// Call this before testing Update behavior so batchSize > 0.
        /// </summary>
        private static void WarmupBatchSize(TestEntitySystem system)
        {
            system.Update(0.001f);
        }

        // ═══════════════════════════════════════════════════════════════
        //  CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void Constructor_WithValidSourceAndSettings_CreatesSystem()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var settings = CreateSettings();

            // Act
            var system = new TestEntitySystem(source, settings);

            // Assert
            Assert.NotNull(system);
        }

        [Test]
        public void Constructor_WithNullSource_ThrowsArgumentNullException()
        {
            // Arrange
            var settings = CreateSettings();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(
                () => new TestEntitySystem(null, settings));
            Assert.AreEqual("source", ex.ParamName);
        }

        [Test]
        public void Constructor_WithNullSettings_ThrowsArgumentNullException()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(
                () => new TestEntitySystem(source, null));
            Assert.AreEqual("settings", ex.ParamName);
        }

        [Test]
        public void Constructor_WithPrePopulatedSource_DoesNotTrackEntitiesBeforeEnable()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB });
            var system = new TestEntitySystem(source, CreateSettings());

            // Act — no Enable yet
            WarmupBatchSize(system);
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — entities are not tracked until Enable
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void Constructor_WithEmptySource_CreatesSystem()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var settings = CreateSettings();

            // Act
            var system = new TestEntitySystem(source, settings);

            // Assert
            Assert.NotNull(system);
        }

        // ═══════════════════════════════════════════════════════════════
        //  ENABLE
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void Enable_WhenNotEnabled_AddsExistingEntitiesFromSource()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB });
            var system = new TestEntitySystem(source, CreateSettings());

            // Act
            system.Enable();
            WarmupBatchSize(system);

            // Assert — next full Update should process both entities
            system.UpdateLog.Clear();
            system.Update(0.016f);
            Assert.AreEqual(2, system.UpdateLog.Count);
        }

        [Test]
        public void Enable_WhenNotEnabled_SubscribesToSourceEvents()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act — add entity after Enable
            var newEntity = CreateEntity("New");
            source.Add(newEntity);

            // Assert — new entity appears in Update
            system.UpdateLog.Clear();
            system.Update(0.016f);
            Assert.AreEqual(1, system.UpdateLog.Count);
            Assert.AreSame(newEntity, system.UpdateLog[0].entity);
        }

        [Test]
        public void Enable_CalledTwice_NoOpOnSecondCall()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var source = new EntityCollection<IEntity>(new[] { entityA });
            var system = new TestEntitySystem(source, CreateSettings());

            // Act
            system.Enable();
            WarmupBatchSize(system);
            system.UpdateLog.Clear();

            system.Enable(); // second call — should be ignored

            system.Update(0.016f);

            // Assert — entityA processed exactly once (no double-add)
            Assert.AreEqual(1, system.UpdateLog.Count);
        }

        [Test]
        public void Enable_WithEmptySource_SubscribesButNoEntities()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());

            // Act
            system.Enable();
            WarmupBatchSize(system);
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void Enable_CallsOnEnable_VirtualHook()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());

            // Act
            system.Enable();

            // Assert
            Assert.IsTrue(system.OnEnableCalled);
        }

        // ═══════════════════════════════════════════════════════════════
        //  DISABLE
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void Disable_WhenEnabled_UnsubscribesFromSourceEvents()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var source = new EntityCollection<IEntity>(new[] { entityA });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);
            system.Disable();

            // Act — add entity after Disable; should NOT be tracked
            source.Add(CreateEntity("B"));
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void Disable_WhenEnabled_RemovesAllEntitiesFromSystem()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.Disable();
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void Disable_CalledTwice_NoOpOnSecondCall()
        {
            // Arrange
            var source = new EntityCollection<IEntity>(new[] { CreateEntity("A") });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act & Assert
            system.Disable();
            Assert.DoesNotThrow(() => system.Disable());
        }

        [Test]
        public void Disable_WhenNotEnabled_NoOp()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());

            // Act & Assert
            Assert.DoesNotThrow(() => system.Disable());
        }

        [Test]
        public void Disable_CallsOnDisable_VirtualHook()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();

            // Act
            system.Disable();

            // Assert
            Assert.IsTrue(system.OnDisableCalled);
        }

        // ═══════════════════════════════════════════════════════════════
        //  UPDATE
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void Update_WhenDisabled_NoOp()
        {
            // Arrange
            var source = new EntityCollection<IEntity>(new[] { CreateEntity("A") });
            var system = new TestEntitySystem(source, CreateSettings());

            // Act
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(0, system.UpdateCallCount);
        }

        [Test]
        public void Update_WhenEnabled_CallsUpdateForEntities()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var source = new EntityCollection<IEntity>(new[] { entityA });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(1, system.UpdateLog.Count);
            Assert.AreSame(entityA, system.UpdateLog[0].entity);
        }

        [Test]
        public void Update_WithNoEntities_NoOp()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void Update_PassesDeltaTimeToEntityUpdate()
        {
            // Arrange
            var source = new EntityCollection<IEntity>(new[] { CreateEntity("A") });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.025f);

            // Assert
            Assert.AreEqual(1, system.UpdateLog.Count);
            Assert.AreEqual(0.025f, system.UpdateLog[0].deltaTime, 0.0001f);
        }

        [Test]
        public void Update_RespectsBatchSize_LimitsEntitiesPerFrame()
        {
            // Arrange — batchSize=2, 5 entities → processes 2 per frame
            var settings = CreateSettings(
                stepUp: 2,
                maxSize: 2,
                minSize: 2,
                scaleDown: 2);
            var entities = new IEntity[5];
            for (int i = 0; i < 5; i++)
                entities[i] = CreateEntity($"E{i}");
            var source = new EntityCollection<IEntity>(entities);
            var system = new TestEntitySystem(source, settings);

            // Act
            system.Enable();
            WarmupBatchSize(system); // batchSize → 2

            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — processes min(batchSize, count) = min(2, 5) = 2
            Assert.AreEqual(2, system.UpdateLog.Count);
        }

        [Test]
        public void Update_MultipleCalls_EventuallyProcessAllEntities()
        {
            // Arrange — batchSize=2, 5 entities
            var settings = CreateSettings(
                stepUp: 2,
                maxSize: 2,
                minSize: 2,
                scaleDown: 2);
            var entities = new IEntity[5];
            for (int i = 0; i < 5; i++)
                entities[i] = CreateEntity($"E{i}");
            var source = new EntityCollection<IEntity>(entities);
            var system = new TestEntitySystem(source, settings);

            // Act
            system.Enable();
            WarmupBatchSize(system);

            var seenEntities = new HashSet<IEntity>();
            for (int i = 0; i < 10; i++)
            {
                system.Update(0.001f);
                foreach (var (entity, _) in system.UpdateLog)
                    seenEntities.Add(entity);
                system.UpdateLog.Clear();
            }

            // Assert — all 5 entities processed at least once
            Assert.AreEqual(5, seenEntities.Count);
        }

        [Test]
        public void Update_BatchSizeClampsToCount_WhenFewerEntitiesThanBatchSize()
        {
            // Arrange — batchSize=10, only 3 entities
            var settings = CreateSettings(
                stepUp: 10,
                maxSize: 10,
                minSize: 10,
                scaleDown: 2);
            var source = new EntityCollection<IEntity>(new[]
            {
                CreateEntity("A"), CreateEntity("B"), CreateEntity("C")
            });
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — batchSize clamped to count (3)
            Assert.AreEqual(3, system.UpdateLog.Count);
        }

        // ═══════════════════════════════════════════════════════════════
        //  ENTITY ADD / REMOVE
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void EntityAddedToSource_AfterEnable_AppearsInNextUpdate()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            var newEntity = CreateEntity("New");
            source.Add(newEntity);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(1, system.UpdateLog.Count);
            Assert.AreSame(newEntity, system.UpdateLog[0].entity);
        }

        [Test]
        public void EntityRemovedFromSource_DisappearsFromSystem()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            source.Remove(entityA);
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(1, system.UpdateLog.Count);
            Assert.AreSame(entityB, system.UpdateLog[0].entity);
        }

        [Test]
        public void EntityRemovedFromSource_OtherEntitiesStillProcessed()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var entityC = CreateEntity("C");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB, entityC });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act — remove middle entity
            source.Remove(entityB);

            var seenEntities = new HashSet<IEntity>();
            for (int i = 0; i < 10; i++)
            {
                system.Update(0.001f);
                foreach (var (entity, _) in system.UpdateLog)
                    seenEntities.Add(entity);
                system.UpdateLog.Clear();
            }

            // Assert
            Assert.IsTrue(seenEntities.Contains(entityA));
            Assert.IsFalse(seenEntities.Contains(entityB));
            Assert.IsTrue(seenEntities.Contains(entityC));
        }

        [Test]
        public void EntityAddedAndRemovedBetweenUpdates_DoesNotAppear()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act — add then remove between frames
            var tempEntity = CreateEntity("Temp");
            source.Add(tempEntity);
            source.Remove(tempEntity);

            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void AddSameEntityTwice_NoDuplicateInSystem()
        {
            // Arrange — EntityCollection prevents duplicate adds
            var entityA = CreateEntity("A");
            var source = new EntityCollection<IEntity>();
            source.Add(entityA);
            var addedAgain = source.Add(entityA); // should return false

            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — entityA processed exactly once
            Assert.IsFalse(addedAgain);
            Assert.AreEqual(1, system.UpdateLog.Count);
        }

        // ═══════════════════════════════════════════════════════════════
        //  DISPOSE
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void Dispose_ClearsAllTrackedEntities()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.Dispose();

            // Assert — no entities tracked after Dispose
            system.UpdateLog.Clear();
            system.Update(0.016f);
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            // Arrange
            var source = new EntityCollection<IEntity>(new[] { CreateEntity("A") });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                system.Dispose();
                system.Dispose();
            });
        }

        [Test]
        public void Dispose_DoesNotAffectEnabledState()
        {
            // Arrange — Dispose clears entity data but does not disable the system.
            // Subsequent source additions are still tracked.
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);
            system.Dispose();

            // Act — add entity after Dispose (event handler still subscribed)
            var newEntity = CreateEntity("AfterDispose");
            source.Add(newEntity);
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — the OnAdded handler is still active, so entity is tracked
            Assert.AreEqual(1, system.UpdateLog.Count);
            Assert.AreSame(newEntity, system.UpdateLog[0].entity);
        }

        // ═══════════════════════════════════════════════════════════════
        //  ROUND-ROBIN CURSOR
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void RoundRobinCursor_ProcessesEntitiesInInsertionOrder()
        {
            // Arrange — batchSize large enough to process all in one frame
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var entityC = CreateEntity("C");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB, entityC });
            var settings = CreateSettings(maxSize: 10, stepUp: 10, minSize: 10);
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — processed in insertion order
            Assert.AreEqual(3, system.UpdateLog.Count);
            Assert.AreEqual("A", system.UpdateLog[0].entity.Name);
            Assert.AreEqual("B", system.UpdateLog[1].entity.Name);
            Assert.AreEqual("C", system.UpdateLog[2].entity.Name);
        }

        [Test]
        public void RoundRobinCursor_WrapsAround_WhenCursorExceedsCount()
        {
            // Arrange — batchSize=2, 3 entities → processes 2 per frame, cursor wraps
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var entityC = CreateEntity("C");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB, entityC });
            var settings = CreateSettings(maxSize: 2, stepUp: 2, minSize: 2);
            var system = new TestEntitySystem(source, settings);
            system.Enable();

            // Warmup: batchSize goes 0 → 2
            WarmupBatchSize(system);

            // Act — frame 1: cursor=0, processes A, B. cursor=2.
            system.UpdateLog.Clear();
            system.Update(0.016f);
            var frame1Entities = new List<string>();
            foreach (var (entity, _) in system.UpdateLog)
                frame1Entities.Add(entity.Name);

            // Act — frame 2: cursor=2, wraps to 0. Processes C, then wraps → A.
            system.UpdateLog.Clear();
            system.Update(0.016f);
            var frame2Entities = new List<string>();
            foreach (var (entity, _) in system.UpdateLog)
                frame2Entities.Add(entity.Name);

            // Assert
            Assert.AreEqual(2, frame1Entities.Count);
            Assert.AreEqual("A", frame1Entities[0]);
            Assert.AreEqual("B", frame1Entities[1]);

            Assert.AreEqual(2, frame2Entities.Count);
            Assert.AreEqual("C", frame2Entities[0]);
            Assert.AreEqual("A", frame2Entities[1]);
        }

        [Test]
        public void RoundRobinCursor_AdvancesSequentially_WithBatchSizeOne()
        {
            // Arrange — batchSize=1, 4 entities → one entity per frame
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var entityC = CreateEntity("C");
            var entityD = CreateEntity("D");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB, entityC, entityD });
            var settings = CreateSettings(maxSize: 1, stepUp: 1, minSize: 1);
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act — 4 sequential updates
            var results = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                system.UpdateLog.Clear();
                system.Update(0.016f);
                Assert.AreEqual(1, system.UpdateLog.Count, $"Expected 1 update at frame {i}");
                results.Add(system.UpdateLog[0].entity.Name);
            }

            // Assert — cursor advances: A → B → C → D
            Assert.AreEqual("A", results[0]);
            Assert.AreEqual("B", results[1]);
            Assert.AreEqual("C", results[2]);
            Assert.AreEqual("D", results[3]);
        }

        [Test]
        public void RoundRobinCursor_ContinuesWrappingAcrossMultipleFrames()
        {
            // Arrange — batchSize=3, 4 entities → processes 3 per frame
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var entityC = CreateEntity("C");
            var entityD = CreateEntity("D");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB, entityC, entityD });
            var settings = CreateSettings(maxSize: 3, stepUp: 3, minSize: 3);
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);
            var frame1 = new List<string>();
            foreach (var (entity, _) in system.UpdateLog)
                frame1.Add(entity.Name);

            system.UpdateLog.Clear();
            system.Update(0.016f);
            var frame2 = new List<string>();
            foreach (var (entity, _) in system.UpdateLog)
                frame2.Add(entity.Name);

            // Assert — frame1: A, B, C. frame2: D, A, B (wraps).
            Assert.AreEqual(3, frame1.Count);
            Assert.AreEqual("A", frame1[0]);
            Assert.AreEqual("B", frame1[1]);
            Assert.AreEqual("C", frame1[2]);

            Assert.AreEqual(3, frame2.Count);
            Assert.AreEqual("D", frame2[0]);
            Assert.AreEqual("A", frame2[1]);
            Assert.AreEqual("B", frame2[2]);
        }

        [Test]
        public void RoundRobinCursor_SingleEntity_UpdatesEveryFrame()
        {
            // Arrange — 1 entity, batchSize=5
            var entityA = CreateEntity("A");
            var source = new EntityCollection<IEntity>(new[] { entityA });
            var settings = CreateSettings(maxSize: 5, stepUp: 5, minSize: 5);
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act — 3 updates, each should process entityA
            for (int frame = 0; frame < 3; frame++)
            {
                system.UpdateLog.Clear();
                system.Update(0.016f);

                // Assert — batchSize is clamped to count (1), so exactly 1 update per frame
                Assert.AreEqual(1, system.UpdateLog.Count, $"Frame {frame}: expected 1 update");
                Assert.AreEqual("A", system.UpdateLog[0].entity.Name);
            }
        }

        // ═══════════════════════════════════════════════════════════════
        //  SETTINGS / ADAPTIVE BATCHING
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void Settings_BatchSizeRampsUp_WhenFastFrame()
        {
            // Arrange — generous budget, fast execution → batch size grows by stepUp
            var settings = CreateSettings(
                frameBudget: 1f,
                minSize: 1,
                maxSize: 100,
                stepUp: 10,
                scaleDown: 2);
            var source = new EntityCollection<IEntity>(new[] { CreateEntity("A") });
            var system = new TestEntitySystem(source, settings);
            system.Enable();

            // Act — first call: batchSize=0, nothing processed, ramps to 10
            system.UpdateLog.Clear();
            system.Update(0.001f);
            Assert.AreEqual(0, system.UpdateLog.Count, "First call should process nothing (batchSize=0)");

            // Second call: batchSize=10, 1 entity → processes 1, ramps to 20
            system.UpdateLog.Clear();
            system.Update(0.001f);
            Assert.AreEqual(1, system.UpdateLog.Count, "Second call should process entity");

            // Third call: batchSize=20, 1 entity → processes 1
            system.UpdateLog.Clear();
            system.Update(0.001f);
            Assert.AreEqual(1, system.UpdateLog.Count, "Third call should process entity");
        }

        [Test]
        public void Settings_BatchSizeRespectsMaxSize()
        {
            // Arrange — maxSize=3, stepUp=100 → batch size never exceeds 3
            var settings = CreateSettings(
                frameBudget: 1f,
                minSize: 1,
                maxSize: 3,
                stepUp: 100,
                scaleDown: 2);
            var entities = new IEntity[10];
            for (int i = 0; i < 10; i++)
                entities[i] = CreateEntity($"E{i}");
            var source = new EntityCollection<IEntity>(entities);
            var system = new TestEntitySystem(source, settings);
            system.Enable();

            // Act — warm up and then test
            for (int i = 0; i < 5; i++)
                system.Update(0.001f);

            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — batchSize is capped at maxSize=3
            Assert.AreEqual(3, system.UpdateLog.Count);
        }

        [Test]
        public void Settings_BatchSizeRespectsMinSize()
        {
            // Arrange — minSize=8, 2 entities → batchSize min is 8, but clamped to count (2)
            var settings = CreateSettings(
                frameBudget: 1f,
                minSize: 8,
                maxSize: 100,
                stepUp: 10,
                scaleDown: 2);
            var source = new EntityCollection<IEntity>(new[]
            {
                CreateEntity("A"), CreateEntity("B")
            });
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — processes both entities (batchSize clamped to count)
            Assert.AreEqual(2, system.UpdateLog.Count);
        }

        // ═══════════════════════════════════════════════════════════════
        //  EDGE CASES & INTEGRATION
        // ═══════════════════════════════════════════════════════════════

        [Test]
        public void EnableDisableReEnable_WorksCorrectly()
        {
            // Arrange
            var entityA = CreateEntity("A");
            var source = new EntityCollection<IEntity>(new[] { entityA });
            var system = new TestEntitySystem(source, CreateSettings());

            // Act
            system.Enable();
            WarmupBatchSize(system);
            system.Disable();
            system.Enable();
            WarmupBatchSize(system);

            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(1, system.UpdateLog.Count);
            Assert.AreSame(entityA, system.UpdateLog[0].entity);
        }

        [Test]
        public void Update_AfterDispose_DoesNotProcessEntities()
        {
            // Arrange
            var source = new EntityCollection<IEntity>(new[] { CreateEntity("A") });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.Dispose();
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — entityCount is 0 after Dispose
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void RemoveEntityDuringIteration_NoException()
        {
            // Arrange — verifies swap-and-pop removal is safe during iteration
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var entityC = CreateEntity("C");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB, entityC });
            var settings = CreateSettings(maxSize: 10, stepUp: 10, minSize: 10);
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act & Assert — should not throw
            Assert.DoesNotThrow(() => system.Update(0.016f));
        }

        [Test]
        public void LargeEntityCount_HandlesGracefully()
        {
            // Arrange
            var settings = CreateSettings(maxSize: 100, stepUp: 100, minSize: 100);
            var entities = new IEntity[1000];
            for (int i = 0; i < 1000; i++)
                entities[i] = CreateEntity($"E{i}");
            var source = new EntityCollection<IEntity>(entities);
            var system = new TestEntitySystem(source, settings);

            // Act
            system.Enable();
            WarmupBatchSize(system);
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — batchSize=100, count=1000 → processes 100
            Assert.AreEqual(100, system.UpdateLog.Count);
        }

        [Test]
        public void MultipleAddRemoveCycles_MaintainsCorrectness()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act — add 5 entities
            var batch = new List<IEntity>();
            for (int i = 0; i < 5; i++)
            {
                var e = CreateEntity($"Cycle_{i}");
                source.Add(e);
                batch.Add(e);
            }

            // Verify all 5 are tracked
            var seenAfterAdd = new HashSet<IEntity>();
            for (int i = 0; i < 5; i++)
            {
                system.Update(0.001f);
                foreach (var (entity, _) in system.UpdateLog)
                    seenAfterAdd.Add(entity);
                system.UpdateLog.Clear();
            }
            Assert.AreEqual(5, seenAfterAdd.Count, "All 5 entities should be tracked");

            // Remove 3 entities
            source.Remove(batch[0]);
            source.Remove(batch[1]);
            source.Remove(batch[2]);

            // Verify only 2 remain
            var seenAfterRemove = new HashSet<IEntity>();
            for (int i = 0; i < 10; i++)
            {
                system.Update(0.001f);
                foreach (var (entity, _) in system.UpdateLog)
                    seenAfterRemove.Add(entity);
                system.UpdateLog.Clear();
            }
            Assert.AreEqual(2, seenAfterRemove.Count, "Only 2 entities should remain");
            Assert.IsTrue(seenAfterRemove.Contains(batch[3]));
            Assert.IsTrue(seenAfterRemove.Contains(batch[4]));
        }

        [Test]
        public void EntityRemovedDuringBatchSwap_CursorStaysValid()
        {
            // Arrange — remove entity before cursor position and verify no crash
            var entityA = CreateEntity("A");
            var entityB = CreateEntity("B");
            var entityC = CreateEntity("C");
            var entityD = CreateEntity("D");
            var source = new EntityCollection<IEntity>(new[] { entityA, entityB, entityC, entityD });
            var settings = CreateSettings(maxSize: 2, stepUp: 2, minSize: 2);
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Process first frame: A, B → cursor=2
            system.Update(0.016f);

            // Remove entity at cursor position
            source.Remove(entityC);

            // Act — cursor=2, but count is now 3. Should wrap.
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — no crash, processes entities
            Assert.Greater(system.UpdateLog.Count, 0);
        }

        [Test]
        public void OnEnable_CalledOnlyOnce_AfterEnable()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());

            // Act
            system.Enable();

            // Assert
            Assert.IsTrue(system.OnEnableCalled);

            // Disable and re-enable — OnEnable should be called again
            system.Disable();
            system.ResetOnEnableCalled();
            system.Enable();
            Assert.IsTrue(system.OnEnableCalled);
        }

        [Test]
        public void OnDisable_CalledOnlyWhenEnabled()
        {
            // Arrange
            var source = new EntityCollection<IEntity>();
            var system = new TestEntitySystem(source, CreateSettings());

            // Act — disable without enable
            system.Disable();

            // Assert — OnDisable should NOT be called
            Assert.IsFalse(system.OnDisableCalled);

            // Now enable then disable
            system.Enable();
            system.Disable();

            // Assert — OnDisable should be called
            Assert.IsTrue(system.OnDisableCalled);
        }

        [Test]
        public void SourceEnumerationOrder_MatchesInternalOrder()
        {
            // Arrange — verify that EntityCollection enumeration order matches system processing order
            var entityX = CreateEntity("X");
            var entityY = CreateEntity("Y");
            var entityZ = CreateEntity("Z");
            var source = new EntityCollection<IEntity>(new[] { entityZ, entityX, entityY });
            var settings = CreateSettings(maxSize: 10, stepUp: 10, minSize: 10);
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert — order matches source enumeration (insertion order)
            Assert.AreEqual(3, system.UpdateLog.Count);
            Assert.AreEqual("Z", system.UpdateLog[0].entity.Name);
            Assert.AreEqual("X", system.UpdateLog[1].entity.Name);
            Assert.AreEqual("Y", system.UpdateLog[2].entity.Name);
        }

        [Test]
        public void SystemProcessesAllEntitiesInCircularFashion()
        {
            // Arrange — batchSize equals entity count, so every frame processes all
            var entities = new IEntity[3];
            for (int i = 0; i < 3; i++)
                entities[i] = CreateEntity($"E{i}");
            var source = new EntityCollection<IEntity>(entities);
            var settings = CreateSettings(maxSize: 3, stepUp: 3, minSize: 3);
            var system = new TestEntitySystem(source, settings);
            system.Enable();
            WarmupBatchSize(system);

            // Act — 3 frames, each should process all 3 entities
            for (int frame = 0; frame < 3; frame++)
            {
                system.UpdateLog.Clear();
                system.Update(0.016f);
                Assert.AreEqual(3, system.UpdateLog.Count,
                    $"Frame {frame}: expected all 3 entities");
            }
        }

        [Test]
        public void Update_IncreasesCallCount()
        {
            // Arrange
            var source = new EntityCollection<IEntity>(new[] { CreateEntity("A") });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);

            // Act
            system.UpdateLog.Clear();
            system.Update(0.016f);
            int countAfterFirst = system.UpdateCallCount;

            system.UpdateLog.Clear();
            system.Update(0.016f);

            // Assert
            Assert.AreEqual(countAfterFirst + 1, system.UpdateCallCount);
        }

        [Test]
        public void Dispose_AfterEnableThenDisable_Works()
        {
            // Arrange
            var source = new EntityCollection<IEntity>(new[] { CreateEntity("A") });
            var system = new TestEntitySystem(source, CreateSettings());
            system.Enable();
            WarmupBatchSize(system);
            system.Disable();

            // Act & Assert
            Assert.DoesNotThrow(() => system.Dispose());
        }
    }
}
