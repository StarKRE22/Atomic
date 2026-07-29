using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Atomic.Entities.Tests
{
    #region Test Helpers

    /// <summary>
    /// Tag keys used to assign priority to test entities.
    /// </summary>
    internal static class PriorityTag
    {
        public const int High = 1;
        public const int Medium = 2;
        public const int Low = 3;
    }

    /// <summary>
    /// Test double for <see cref="IEntityTrigger{T}"/> that records all calls.
    /// </summary>
    internal sealed class TestTrigger : IEntityTrigger<Entity>
    {
        public Action<Entity> CapturedAction { get; private set; }
        public bool SetActionCalled { get; private set; }
        public List<Entity> TrackedEntities { get; } = new();
        public List<Entity> UntrackedEntities { get; } = new();
        public int TrackCallCount { get; private set; }
        public int UntrackCallCount { get; private set; }

        public void SetAction(Action<Entity> action)
        {
            SetActionCalled = true;
            CapturedAction = action;
        }

        public void Track(Entity entity)
        {
            TrackCallCount++;
            TrackedEntities.Add(entity);
        }

        public void Untrack(Entity entity)
        {
            UntrackCallCount++;
            UntrackedEntities.Add(entity);
        }
    }

    /// <summary>
    /// Concrete implementation of <see cref="PriorityEntitySystem{T}"/> for testing.
    /// Records update calls, evaluates priority via a delegate, and exposes
    /// internal state for verification.
    /// </summary>
    internal sealed class TestPrioritySystem : PriorityEntitySystem<Entity>
    {
        public List<(Entity entity, float dt)> UpdateLog { get; } = new();
        public int EvaluatePriorityCallCount { get; private set; }
        public Action<Entity, float> OnUpdateCallback { get; set; }

        private readonly Func<Entity, EntityUpdatePriority> _priorityFunc;

        /// <summary>
        /// Provides access to the source collection as mutable for deferred-add tests.
        /// </summary>
        public IEntityCollection<Entity> MutableSource { get; }

        public TestPrioritySystem(
            IEntityCollection<Entity> source,
            Settings settings,
            Func<Entity, EntityUpdatePriority> priorityFunc,
            params IEntityTrigger<Entity>[] triggers
        ) : base(source, settings, triggers)
        {
            MutableSource = source;
            _priorityFunc = priorityFunc;
        }

        protected override void Update(Entity entity, float deltaTime)
        {
            UpdateLog.Add((entity, deltaTime));
            OnUpdateCallback?.Invoke(entity, deltaTime);
        }

        protected override EntityUpdatePriority EvaluatePriority(Entity entity)
        {
            EvaluatePriorityCallCount++;
            return _priorityFunc(entity);
        }
    }

    #endregion

    [TestFixture]
    public class PriorityEntitySystemTests
    {
        private const float DeltaTime = 0.016f;

        // ===== Helpers =====

        private static Entity CreateEntity(int priorityTag)
        {
            var entity = new Entity();
            entity.AddTag(priorityTag);
            return entity;
        }

        private static EntityUpdatePriority DefaultPriorityFunc(Entity entity)
        {
            if (entity.HasTag(PriorityTag.High))
                return EntityUpdatePriority.High;
            if (entity.HasTag(PriorityTag.Medium))
                return EntityUpdatePriority.Medium;
            return EntityUpdatePriority.Low;
        }

        private static PriorityEntitySystem<Entity>.Settings CreateSettings(
            int highPercent = 70,
            int midPercent = 20,
            float cooldown = 0.25f,
            int batchSize = 1000)
        {
            return new PriorityEntitySystem<Entity>.Settings
            {
                cooldown = cooldown,
                highPercent = highPercent,
                midPercent = midPercent,
                frameBudget = float.MaxValue,
                batching = new EntitySystemBase<Entity>.Settings.AdaptiveBatching
                {
                    minSize = batchSize,
                    maxSize = batchSize,
                    stepUp = batchSize,
                    scaleDown = 2
                }
            };
        }

        /// <summary>
        /// First Update call warms up the adaptive batch size from 0 to batchSize.
        /// The second call actually processes entities.
        /// </summary>
        private static void Warmup(TestPrioritySystem system)
        {
            system.Update(DeltaTime);
        }

        private static TestPrioritySystem CreateSystem(
            IEntityCollection<Entity> collection,
            int highPercent = 70,
            int midPercent = 20,
            float cooldown = 0.25f,
            int batchSize = 1000,
            Func<Entity, EntityUpdatePriority> priorityFunc = null,
            params IEntityTrigger<Entity>[] triggers)
        {
            var settings = CreateSettings(highPercent, midPercent, cooldown, batchSize);
            return new TestPrioritySystem(collection, settings, priorityFunc ?? DefaultPriorityFunc, triggers);
        }

        // =====================================================================
        // CONSTRUCTOR
        // =====================================================================

        #region Constructor

        [Test]
        public void Constructor_NullSource_ThrowsArgumentNullException()
        {
            //Arrange:
            var settings = CreateSettings();

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() =>
                new TestPrioritySystem(null, settings, DefaultPriorityFunc));
        }

        [Test]
        public void Constructor_NullSettings_ThrowsArgumentNullException()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() =>
                new TestPrioritySystem(collection, null, DefaultPriorityFunc));
        }

        [Test]
        public void Constructor_WithTriggers_CreatesSystem()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var settings = CreateSettings();
            var trigger = new TestTrigger();

            //Act:
            var system = new TestPrioritySystem(collection, settings, DefaultPriorityFunc, trigger);

            //Assert:
            Assert.IsNotNull(system);
        }

        #endregion

        // =====================================================================
        // ENABLE / DISABLE
        // =====================================================================

        #region Enable / Disable

        [Test]
        public void Enable_PlacesEntitiesInCorrectBuckets()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var mid = CreateEntity(PriorityTag.Medium);
            var low = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { high, mid, low });
            var system = CreateSystem(collection);

            //Act:
            system.Enable();

            //Assert:
            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(1, system._midEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
            Assert.AreEqual(high, system._highEntities[0]);
            Assert.AreEqual(mid, system._midEntities[0]);
            Assert.AreEqual(low, system._lowEntities[0]);
        }

        [Test]
        public void Enable_SubscribesToSourceEvents()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var system = CreateSystem(collection);
            var newEntity = CreateEntity(PriorityTag.High);

            //Act:
            system.Enable();
            collection.Add(newEntity);

            //Assert:
            Assert.AreEqual(1, system._highEntityCount);
        }

        [Test]
        public void Enable_FiresSetActionOnTriggers()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);

            //Act:
            system.Enable();

            //Assert:
            Assert.IsTrue(trigger.SetActionCalled);
            Assert.IsNotNull(trigger.CapturedAction);
        }

        [Test]
        public void Enable_TracksExistingEntitiesInTriggers()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);

            //Act:
            system.Enable();

            //Assert:
            Assert.AreEqual(1, trigger.TrackCallCount);
            Assert.AreEqual(entity, trigger.TrackedEntities[0]);
        }

        [Test]
        public void Enable_AlreadyEnabled_DoesNothing()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var system = CreateSystem(collection);
            system.Enable();
            var callCountBefore = system.EvaluatePriorityCallCount;

            //Act:
            system.Enable();

            //Assert:
            Assert.AreEqual(callCountBefore, system.EvaluatePriorityCallCount);
        }

        [Test]
        public void Disable_UnsubscribesFromSourceEvents()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var system = CreateSystem(collection);
            system.Enable();
            var newEntity = CreateEntity(PriorityTag.High);

            //Act:
            system.Disable();
            collection.Add(newEntity);

            //Assert:
            Assert.AreEqual(0, system._highEntityCount);
        }

        [Test]
        public void Disable_RemovesAllEntitiesFromBuckets()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var mid = CreateEntity(PriorityTag.Medium);
            var low = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { high, mid, low });
            var system = CreateSystem(collection);
            system.Enable();

            //Act:
            system.Disable();

            //Assert:
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(0, system._midEntityCount);
            Assert.AreEqual(0, system._lowEntityCount);
        }

        [Test]
        public void Disable_UntracksEntitiesInTriggers()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);
            system.Enable();

            //Act:
            system.Disable();

            //Assert:
            Assert.AreEqual(1, trigger.UntrackCallCount);
            Assert.AreEqual(entity, trigger.UntrackedEntities[0]);
        }

        [Test]
        public void Disable_AlreadyDisabled_DoesNothing()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var system = CreateSystem(collection);

            //Act:
            system.Disable();

            //Assert: no exception, idempotent
            Assert.AreEqual(0, system._highEntityCount);
        }

        [Test]
        public void Enable_CollectsAllSourceEntities()
        {
            //Arrange:
            var entities = new Entity[10];
            for (int i = 0; i < 10; i++)
                entities[i] = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(entities);
            var system = CreateSystem(collection);

            //Act:
            system.Enable();

            //Assert:
            Assert.AreEqual(10, system._highEntityCount);
        }

        #endregion

        // =====================================================================
        // UPDATE - GENERAL
        // =====================================================================

        #region Update General

        [Test]
        public void Update_DoesNothing_WhenDisabled()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var system = CreateSystem(collection);

            //Act:
            system.Update(DeltaTime);

            //Assert:
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void Update_PassesDeltaTimeToEntityUpdate()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var system = CreateSystem(collection);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(0.05f);

            //Assert:
            Assert.IsTrue(system.UpdateLog.Any(e => e.entity == entity));
            Assert.IsTrue(system.UpdateLog.All(e => e.dt == 0.05f));
        }

        [Test]
        public void Update_FirstCallBatchSizeZero_ProcessesNothing()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var system = CreateSystem(collection);
            system.Enable();

            //Act:
            system.Update(DeltaTime);

            //Assert: first call batchSize=0, no entities processed
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        #endregion

        // =====================================================================
        // UPDATE - PRIORITY ORDER
        // =====================================================================

        #region Update Priority Order

        [Test]
        public void Update_ProcessesHighBeforeMidBeforeLow()
        {
            //Arrange:
            var high1 = CreateEntity(PriorityTag.High);
            var high2 = CreateEntity(PriorityTag.High);
            var mid1 = CreateEntity(PriorityTag.Medium);
            var low1 = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { low1, mid1, high2, high1 });
            var system = CreateSystem(collection);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert: high entities processed before mid, mid before low
            var updatedEntities = system.UpdateLog.Select(e => e.entity).ToList();
            int highFirstIndex = updatedEntities.IndexOf(high1);
            int high2FirstIndex = updatedEntities.IndexOf(high2);
            int midIndex = updatedEntities.IndexOf(mid1);
            int lowIndex = updatedEntities.IndexOf(low1);

            Assert.Less(Math.Max(highFirstIndex, high2FirstIndex), midIndex);
            Assert.Less(midIndex, lowIndex);
        }

        [Test]
        public void Update_WithOnlyHighEntities_AllAreHigh()
        {
            //Arrange:
            var high1 = CreateEntity(PriorityTag.High);
            var high2 = CreateEntity(PriorityTag.High);
            var high3 = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high1, high2, high3 });
            var system = CreateSystem(collection);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert:
            Assert.AreEqual(3, system.UpdateLog.Count);
            Assert.IsTrue(system.UpdateLog.All(e => e.entity.HasTag(PriorityTag.High)));
        }

        [Test]
        public void Update_WithOnlyLowEntities_AllAreLow()
        {
            //Arrange:
            var low1 = CreateEntity(PriorityTag.Low);
            var low2 = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { low1, low2 });
            var system = CreateSystem(collection);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert:
            Assert.AreEqual(2, system.UpdateLog.Count);
            Assert.IsTrue(system.UpdateLog.All(e => e.entity.HasTag(PriorityTag.Low)));
        }

        #endregion

        // =====================================================================
        // UPDATE - QUOTA
        // =====================================================================

        #region Update Quota

        [Test]
        public void Update_RespectsHighPercentQuota()
        {
            //Arrange: batchSize=10, 70% high, 20% mid, 10% low
            var highEntities = new Entity[5];
            for (int i = 0; i < 5; i++)
                highEntities[i] = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(highEntities);
            var system = CreateSystem(collection, highPercent: 70, midPercent: 20, batchSize: 10);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert: highQuota = 10*70/100 = 7, but only 5 entities
            Assert.AreEqual(5, system.UpdateLog.Count);
        }

        [Test]
        public void Update_QuotaOverflowSpillsToNextBucket()
        {
            //Arrange: batchSize=5, 50% high = 2, 30% mid = 1, 20% low = 2
            // But only 1 high entity, so remaining 1 high quota spills to mid
            var high = CreateEntity(PriorityTag.High);
            var mid1 = CreateEntity(PriorityTag.Medium);
            var mid2 = CreateEntity(PriorityTag.Medium);
            var mid3 = CreateEntity(PriorityTag.Medium);
            var collection = new EntityCollection<Entity>(new[] { high, mid1, mid2, mid3 });
            var system = CreateSystem(collection, highPercent: 50, midPercent: 30, batchSize: 5);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert: highQuota=2, processes 1 high, remaining 1 spills to mid budget
            // midBudget = 1 + 1 = 2, processes 2 mids
            Assert.AreEqual(3, system.UpdateLog.Count);
        }

        [Test]
        public void Update_DefaultPercentages_ProcessesCorrectSplit()
        {
            //Arrange: batchSize=100, 70/20/10 → 70 high, 20 mid, 10 low
            var highEntities = new List<Entity>();
            var midEntities = new List<Entity>();
            var lowEntities = new List<Entity>();
            for (int i = 0; i < 80; i++) highEntities.Add(CreateEntity(PriorityTag.High));
            for (int i = 0; i < 30; i++) midEntities.Add(CreateEntity(PriorityTag.Medium));
            for (int i = 0; i < 20; i++) lowEntities.Add(CreateEntity(PriorityTag.Low));

            var all = new List<Entity>();
            all.AddRange(highEntities);
            all.AddRange(midEntities);
            all.AddRange(lowEntities);

            var collection = new EntityCollection<Entity>(all);
            var system = CreateSystem(collection, batchSize: 100);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert:
            int highCount = system.UpdateLog.Count(e => e.entity.HasTag(PriorityTag.High));
            int midCount = system.UpdateLog.Count(e => e.entity.HasTag(PriorityTag.Medium));
            int lowCount = system.UpdateLog.Count(e => e.entity.HasTag(PriorityTag.Low));

            Assert.AreEqual(70, highCount);
            Assert.AreEqual(20, midCount);
            Assert.AreEqual(10, lowCount);
            Assert.AreEqual(100, system.UpdateLog.Count);
        }

        #endregion

        // =====================================================================
        // UPDATE - COOLDOWN & RECALCULATION
        // =====================================================================

        #region Update Cooldown

        [Test]
        public void Update_RecalculatePriorities_AfterCooldownExpires()
        {
            //Arrange: cooldown=0.3, first Update deducts DeltaTime=0.016 → 0.284 > 0
            // second Update deducts → 0.268 > 0 ... need enough updates to expire
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high });
            int evalCount = 0;
            var system = CreateSystem(collection, cooldown: 0.1f, batchSize: 1000,
                priorityFunc: e =>
                {
                    evalCount++;
                    return DefaultPriorityFunc(e);
                });
            system.Enable();
            Warmup(system);

            int countAfterEnable = evalCount;

            //Act: enough updates to expire cooldown (0.1 / 0.016 ≈ 7 updates)
            for (int i = 0; i < 10; i++)
                system.Update(DeltaTime);

            //Assert: EvaluatePriority called more times than just during Enable
            Assert.Greater(evalCount, countAfterEnable);
        }

        [Test]
        public void Update_NoRecalculate_WhenCooldownNotExpired()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high });
            int evalCount = 0;
            var system = CreateSystem(collection, cooldown: 100f, batchSize: 1000,
                priorityFunc: e =>
                {
                    evalCount++;
                    return DefaultPriorityFunc(e);
                });
            system.Enable();
            Warmup(system);

            int countAfterEnable = evalCount;

            //Act: single update, cooldown is huge, no recalculation
            system.Update(DeltaTime);

            //Assert: no new evaluations
            Assert.AreEqual(countAfterEnable, evalCount);
        }

        [Test]
        public void Update_ZeroCooldown_NeverRecalculates()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high });
            int evalCount = 0;
            var system = CreateSystem(collection, cooldown: 0f, batchSize: 1000,
                priorityFunc: e =>
                {
                    evalCount++;
                    return DefaultPriorityFunc(e);
                });
            system.Enable();
            Warmup(system);

            int countAfterEnable = evalCount;

            //Act:
            for (int i = 0; i < 20; i++)
                system.Update(DeltaTime);

            //Assert: cooldown<=0 skips entire UpdateCooldown
            Assert.AreEqual(countAfterEnable, evalCount);
        }

        [Test]
        public void Update_CooldownRecalculate_MovesEntityBetweenBuckets()
        {
            //Arrange: start as High, then switch to Low on recalculation
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var system = CreateSystem(collection, cooldown: 0.01f, batchSize: 1000);
            system.Enable();
            Warmup(system);

            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(0, system._lowEntityCount);

            // Change entity tag so recalculation assigns it to Low
            entity.DelTag(PriorityTag.High);
            entity.AddTag(PriorityTag.Low);

            //Act: enough updates to trigger cooldown recalculation
            for (int i = 0; i < 10; i++)
                system.Update(DeltaTime);

            //Assert:
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
        }

        #endregion

        // =====================================================================
        // CHANGE PRIORITY
        // =====================================================================

        #region ChangePriority

        [Test]
        public void ChangePriority_OutsideUpdate_AppliesImmediately()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);
            system.Enable();

            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(0, system._lowEntityCount);

            //Act: change entity priority outside of update via trigger
            high.DelTag(PriorityTag.High);
            high.AddTag(PriorityTag.Low);
            trigger.CapturedAction?.Invoke(high);

            //Assert: entity moved immediately, not deferred
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
        }

        [Test]
        public void ChangePriority_SamePriority_DoesNothing()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);
            system.Enable();
            Warmup(system);

            int evalCountBefore = system.EvaluatePriorityCallCount;

            //Act: trigger fires but entity is still High → no bucket change
            trigger.CapturedAction?.Invoke(high);

            //Assert: EvaluatePriority was called, but entity stays in same bucket
            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(evalCountBefore + 1, system.EvaluatePriorityCallCount);
        }

        [Test]
        public void ChangePriority_EntityNotInSystem_Ignored()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>();
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);
            system.Enable();
            Warmup(system);

            //Act: trigger on entity not in collection
            trigger.CapturedAction?.Invoke(entity);

            //Assert: no crash, counts unchanged
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(0, system._midEntityCount);
            Assert.AreEqual(0, system._lowEntityCount);
        }

        [Test]
        public void ChangePriority_Deferred_WhenUpdating()
        {
            //Arrange: entity in High, will be changed to Low during Update callback
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger, batchSize: 1000);
            system.Enable();
            Warmup(system);

            // Set up: during update of any entity, trigger priority change on high
            system.OnUpdateCallback = (e, dt) =>
            {
                high.DelTag(PriorityTag.High);
                high.AddTag(PriorityTag.Low);
                trigger.CapturedAction?.Invoke(high);
            };

            //Act:
            system.Update(DeltaTime);

            //Assert: during update it was queued, after update it is applied
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
        }

        [Test]
        public void ChangePriority_MovesEntityAcrossBuckets()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);
            system.Enable();

            Assert.AreEqual(1, system._highEntityCount);

            //Act: move from High to Medium
            entity.DelTag(PriorityTag.High);
            entity.AddTag(PriorityTag.Medium);
            trigger.CapturedAction?.Invoke(entity);

            //Assert:
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(1, system._midEntityCount);

            //Act: move from Medium to Low
            entity.DelTag(PriorityTag.Medium);
            entity.AddTag(PriorityTag.Low);
            trigger.CapturedAction?.Invoke(entity);

            //Assert:
            Assert.AreEqual(0, system._midEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
        }

        #endregion

        // =====================================================================
        // RECALCULATE PRIORITIES
        // =====================================================================

        #region RecalculatePriorities

        [Test]
        public void RecalculatePriorities_IteratesAllSourceEntities()
        {
            //Arrange: cooldown triggers recalculation, verify via eval count
            var entities = new Entity[5];
            for (int i = 0; i < 5; i++)
            {
                entities[i] = CreateEntity(PriorityTag.High);
            }
            var collection = new EntityCollection<Entity>(entities);
            var system = CreateSystem(collection, cooldown: 0.01f, batchSize: 1000);
            system.Enable();
            Warmup(system);

            int evalCountAfterEnable = system.EvaluatePriorityCallCount;

            //Act: enough updates to trigger cooldown recalculation
            for (int i = 0; i < 10; i++)
                system.Update(DeltaTime);

            //Assert: at least 5 more evaluations (one per entity)
            Assert.GreaterOrEqual(system.EvaluatePriorityCallCount - evalCountAfterEnable, 5);
        }

        [Test]
        public void RecalculatePriorities_ReassignsEntities()
        {
            //Arrange: 3 entities start High, after tag change 2 become Low
            var e1 = CreateEntity(PriorityTag.High);
            var e2 = CreateEntity(PriorityTag.High);
            var e3 = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { e1, e2, e3 });
            var system = CreateSystem(collection, cooldown: 0.01f, batchSize: 1000);
            system.Enable();
            Warmup(system);

            Assert.AreEqual(3, system._highEntityCount);

            // Change tags before recalculation
            e1.DelTag(PriorityTag.High);
            e1.AddTag(PriorityTag.Low);
            e2.DelTag(PriorityTag.High);
            e2.AddTag(PriorityTag.Medium);

            //Act: enough updates to trigger cooldown
            for (int i = 0; i < 10; i++)
                system.Update(DeltaTime);

            //Assert:
            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(1, system._midEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
        }

        #endregion

        // =====================================================================
        // DEFERRED COMMANDS
        // =====================================================================

        #region Deferred Commands

        [Test]
        public void DeferredAdd_EntityAddedDuringUpdate_AppliedAfter()
        {
            //Arrange:
            var existing = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { existing });
            var system = CreateSystem(collection, batchSize: 1000);
            system.Enable();
            Warmup(system);

            var newHigh = CreateEntity(PriorityTag.High);

            // During update of Low entity, add a new High entity to the source
            system.OnUpdateCallback = (e, dt) =>
            {
                if (e.HasTag(PriorityTag.Low))
                    collection.Add(newHigh);
            };

            //Act:
            system.Update(DeltaTime);

            //Assert: new entity was deferred, now added after update
            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
        }

        [Test]
        public void DeferredRemove_EntityRemovedDuringUpdate_AppliedAfter()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var mid = CreateEntity(PriorityTag.Medium);
            var collection = new EntityCollection<Entity>(new[] { high, mid });
            var system = CreateSystem(collection, batchSize: 1000);
            system.Enable();
            Warmup(system);

            // During update of High entity, remove it from the source
            system.OnUpdateCallback = (e, dt) =>
            {
                if (e.HasTag(PriorityTag.High))
                    collection.Remove(high);
            };

            //Act:
            system.Update(DeltaTime);

            //Assert: removal was deferred and applied after update
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(1, system._midEntityCount);
        }

        [Test]
        public void DeferredPriorityChange_QueuedDuringUpdate_AppliedAfter()
        {
            //Arrange: entity starts High, during update of another entity its priority changes to Low
            var high1 = CreateEntity(PriorityTag.High);
            var high2 = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high1, high2 });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger, batchSize: 1000);
            system.Enable();
            Warmup(system);

            // During update, change high1 to Low
            system.OnUpdateCallback = (e, dt) =>
            {
                if (e == high1)
                {
                    e.DelTag(PriorityTag.High);
                    e.AddTag(PriorityTag.Low);
                    trigger.CapturedAction?.Invoke(e);
                }
            };

            //Act:
            system.Update(DeltaTime);

            //Assert: priority change was deferred and applied after update
            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
        }

        [Test]
        public void DeferredCommands_ClearedAfterProcessing()
        {
            //Arrange:
            var existing = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { existing });
            var system = CreateSystem(collection, batchSize: 1000);
            system.Enable();
            Warmup(system);

            var newEntity = CreateEntity(PriorityTag.High);
            system.OnUpdateCallback = (e, dt) => collection.Add(newEntity);

            //Act:
            system.Update(DeltaTime);

            // Second update should not re-process old commands
            int countBefore = system._highEntityCount;
            system.Update(DeltaTime);

            //Assert: entity count unchanged, commands were cleared
            Assert.AreEqual(countBefore, system._highEntityCount);
        }

        [Test]
        public void DeferredAdd_UnknownEntityIsIgnored()
        {
            //Arrange: add entity to system, then try to add a duplicate during update
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high });
            var system = CreateSystem(collection, batchSize: 1000);
            system.Enable();
            Warmup(system);

            // Try adding the same entity again during update (should be ignored)
            system.OnUpdateCallback = (e, dt) =>
            {
                if (e.HasTag(PriorityTag.High))
                    collection.Add(high); // duplicate
            };

            //Act:
            system.Update(DeltaTime);

            //Assert: entity count stays at 1
            Assert.AreEqual(1, system._highEntityCount);
        }

        [Test]
        public void DeferredCommands_MultipleCommandsAppliedInOrder()
        {
            //Arrange: have 3 low entities, during update of first one, add and remove entities
            var low1 = CreateEntity(PriorityTag.Low);
            var low2 = CreateEntity(PriorityTag.Low);
            var low3 = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { low1, low2, low3 });
            var system = CreateSystem(collection, batchSize: 1000);
            system.Enable();
            Warmup(system);

            var newHigh = CreateEntity(PriorityTag.High);
            bool added = false;

            system.OnUpdateCallback = (e, dt) =>
            {
                if (e == low1 && !added)
                {
                    added = true;
                    collection.Add(newHigh);
                    collection.Remove(low3);
                }
            };

            //Act:
            system.Update(DeltaTime);

            //Assert: high added, low3 removed
            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(2, system._lowEntityCount);
        }

        #endregion

        // =====================================================================
        // TRIGGERS
        // =====================================================================

        #region Triggers

        [Test]
        public void Trigger_TracksNewEntitiesOnAdd()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);
            system.Enable();

            var entity = CreateEntity(PriorityTag.High);

            //Act:
            collection.Add(entity);

            //Assert:
            Assert.AreEqual(1, trigger.TrackCallCount);
            Assert.AreEqual(entity, trigger.TrackedEntities.Last());
        }

        [Test]
        public void Trigger_UntracksEntitiesOnRemove()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);
            system.Enable();

            //Act:
            collection.Remove(entity);

            //Assert:
            Assert.AreEqual(1, trigger.UntrackCallCount);
            Assert.AreEqual(entity, trigger.UntrackedEntities.Last());
        }

        [Test]
        public void Trigger_CalledFromRecalculate_MovesEntity()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var trigger = new TestTrigger();
            var settings = CreateSettings(cooldown: 0.01f);
            var system = new TestPrioritySystem(collection, settings, DefaultPriorityFunc, trigger);
            system.Enable();

            Assert.AreEqual(1, system._highEntityCount);

            // Change entity priority via trigger (simulates runtime state change)
            entity.DelTag(PriorityTag.High);
            entity.AddTag(PriorityTag.Low);

            //Act:
            trigger.CapturedAction?.Invoke(entity);

            //Assert:
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);
        }

        [Test]
        public void Trigger_EnablesChangePriorityFromOutside()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var mid = CreateEntity(PriorityTag.Medium);
            var collection = new EntityCollection<Entity>(new[] { high, mid });
            var trigger = new TestTrigger();
            var system = CreateSystem(collection, triggers: trigger);
            system.Enable();

            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(1, system._midEntityCount);

            //Act: swap priorities
            high.DelTag(PriorityTag.High);
            high.AddTag(PriorityTag.Medium);
            trigger.CapturedAction?.Invoke(high);

            mid.DelTag(PriorityTag.Medium);
            mid.AddTag(PriorityTag.High);
            trigger.CapturedAction?.Invoke(mid);

            //Assert:
            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(1, system._midEntityCount);
        }

        #endregion

        // =====================================================================
        // DISPOSE
        // =====================================================================

        #region Dispose

        [Test]
        public void Dispose_ClearsAllBuckets()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var mid = CreateEntity(PriorityTag.Medium);
            var low = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { high, mid, low });
            var system = CreateSystem(collection);
            system.Enable();

            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(1, system._midEntityCount);
            Assert.AreEqual(1, system._lowEntityCount);

            //Act:
            system.Dispose();

            //Assert:
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(0, system._midEntityCount);
            Assert.AreEqual(0, system._lowEntityCount);
        }

        [Test]
        public void Dispose_ClearedArraysAreDefault()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var mid = CreateEntity(PriorityTag.Medium);
            var low = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { high, mid, low });
            var system = CreateSystem(collection);
            system.Enable();

            //Act:
            system.Dispose();

            //Assert: array slots are default (null)
            Assert.IsNull(system._highEntities[0]);
            Assert.IsNull(system._midEntities[0]);
            Assert.IsNull(system._lowEntities[0]);
        }

        [Test]
        public void Dispose_DoesNotAffectSourceCollection()
        {
            //Arrange:
            var entity = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { entity });
            var system = CreateSystem(collection);
            system.Enable();

            //Act:
            system.Dispose();

            //Assert: source collection still has the entity
            Assert.AreEqual(1, collection.Count);
            Assert.IsTrue(collection.Contains(entity));
        }

        #endregion

        // =====================================================================
        // MULTIPLE UPDATES - CURSOR ADVANCEMENT
        // =====================================================================

        #region Cursor Advancement

        [Test]
        public void MultipleUpdates_CursorAdvancesAcrossHighEntities()
        {
            //Arrange: batchSize=5, highPercent=50, midPercent=30 → highQuota=2
            var e0 = CreateEntity(PriorityTag.High);
            var e1 = CreateEntity(PriorityTag.High);
            var e2 = CreateEntity(PriorityTag.High);
            var e3 = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { e0, e1, e2, e3 });
            var system = CreateSystem(collection, highPercent: 50, midPercent: 30, batchSize: 5);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);
            var firstBatch = system.UpdateLog.Select(x => x.entity).ToList();
            system.UpdateLog.Clear();
            system.Update(DeltaTime);
            var secondBatch = system.UpdateLog.Select(x => x.entity).ToList();

            //Assert: each entity updated once across 2 calls with quota=2 (warmup advances cursor to 2)
            var allUpdated = firstBatch.Concat(secondBatch).ToList();
            Assert.AreEqual(1, allUpdated.Count(e => e == e0));
            Assert.AreEqual(1, allUpdated.Count(e => e == e1));
            Assert.AreEqual(1, allUpdated.Count(e => e == e2));
            Assert.AreEqual(1, allUpdated.Count(e => e == e3));
        }

        [Test]
        public void MultipleUpdates_CursorAdvancesAcrossMidEntities()
        {
            //Arrange: batchSize=5, highPercent=0, midPercent=60 → midQuota=3
            var e0 = CreateEntity(PriorityTag.Medium);
            var e1 = CreateEntity(PriorityTag.Medium);
            var e2 = CreateEntity(PriorityTag.Medium);
            var e3 = CreateEntity(PriorityTag.Medium);
            var e4 = CreateEntity(PriorityTag.Medium);
            var collection = new EntityCollection<Entity>(new[] { e0, e1, e2, e3, e4 });
            var system = CreateSystem(collection, highPercent: 0, midPercent: 60, batchSize: 5);
            system.Enable();
            Warmup(system);

            //Act: call Update 3 times
            for (int i = 0; i < 3; i++)
                system.Update(DeltaTime);

            //Assert: each entity updated 3 times (quota=3 per call, 5 entities, round-robin)
            int totalUpdates = system.UpdateLog.Count;
            Assert.AreEqual(9, totalUpdates);
        }

        [Test]
        public void MultipleUpdates_CursorAdvancesAcrossLowEntities()
        {
            //Arrange: batchSize=5, highPercent=0, midPercent=0 → lowQuota=5
            var e0 = CreateEntity(PriorityTag.Low);
            var e1 = CreateEntity(PriorityTag.Low);
            var e2 = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { e0, e1, e2 });
            var system = CreateSystem(collection, highPercent: 0, midPercent: 0, batchSize: 5);
            system.Enable();
            Warmup(system);

            //Act: call Update 2 times
            system.Update(DeltaTime);
            var firstBatch = system.UpdateLog.Select(x => x.entity).ToList();
            system.UpdateLog.Clear();
            system.Update(DeltaTime);
            var secondBatch = system.UpdateLog.Select(x => x.entity).ToList();

            //Assert: all entities updated in both calls (quota=5 > 3 entities)
            Assert.AreEqual(3, firstBatch.Count);
            Assert.AreEqual(3, secondBatch.Count);
        }

        [Test]
        public void MultipleUpdates_CursorWrapsAroundCorrectly()
        {
            //Arrange: 3 high entities, highQuota=2 → processes 2 per call
            var e0 = CreateEntity(PriorityTag.High);
            var e1 = CreateEntity(PriorityTag.High);
            var e2 = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { e0, e1, e2 });
            var system = CreateSystem(collection, highPercent: 50, midPercent: 30, batchSize: 5);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);
            var batch1 = system.UpdateLog.Select(x => x.entity).ToList();
            system.UpdateLog.Clear();

            system.Update(DeltaTime);
            var batch2 = system.UpdateLog.Select(x => x.entity).ToList();
            system.UpdateLog.Clear();

            system.Update(DeltaTime);
            var batch3 = system.UpdateLog.Select(x => x.entity).ToList();

            //Assert: 3 calls × 2 quota = 6 updates, each entity exactly 2 times
            var all = batch1.Concat(batch2).Concat(batch3).ToList();
            Assert.AreEqual(6, all.Count);
            Assert.AreEqual(2, all.Count(e => e == e0));
            Assert.AreEqual(2, all.Count(e => e == e1));
            Assert.AreEqual(2, all.Count(e => e == e2));
        }

        #endregion

        // =====================================================================
        // SETTINGS
        // =====================================================================

        #region Settings

        [Test]
        public void Settings_LowPercent_IsComputedCorrectly()
        {
            //Arrange & Act:
            var settings = new PriorityEntitySystem<Entity>.Settings
            {
                highPercent = 70,
                midPercent = 20
            };

            //Assert:
            Assert.AreEqual(10, settings.lowPercent);
        }

        [Test]
        public void Settings_LowPercent_AllHighZeroMid()
        {
            //Arrange & Act:
            var settings = new PriorityEntitySystem<Entity>.Settings
            {
                highPercent = 100,
                midPercent = 0
            };

            //Assert:
            Assert.AreEqual(0, settings.lowPercent);
        }

        [Test]
        public void Settings_DefaultCooldown_IsQuarterSecond()
        {
            //Arrange & Act:
            var settings = new PriorityEntitySystem<Entity>.Settings();

            //Assert:
            Assert.AreEqual(0.25f, settings.cooldown);
        }

        [Test]
        public void Settings_DefaultPercentages_AreCorrect()
        {
            //Arrange & Act:
            var settings = new PriorityEntitySystem<Entity>.Settings();

            //Assert:
            Assert.AreEqual(70, settings.highPercent);
            Assert.AreEqual(20, settings.midPercent);
            Assert.AreEqual(10, settings.lowPercent);
        }

        #endregion

        // =====================================================================
        // EDGE CASES
        // =====================================================================

        #region Edge Cases

        [Test]
        public void Update_EmptyCollection_DoesNothing()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var system = CreateSystem(collection);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert:
            Assert.AreEqual(0, system.UpdateLog.Count);
        }

        [Test]
        public void Enable_EmptyCollection_NoEntitiesInBuckets()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var system = CreateSystem(collection);

            //Act:
            system.Enable();

            //Assert:
            Assert.AreEqual(0, system._highEntityCount);
            Assert.AreEqual(0, system._midEntityCount);
            Assert.AreEqual(0, system._lowEntityCount);
        }

        [Test]
        public void RemoveFromCollection_DecreasesBucketCount()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high });
            var system = CreateSystem(collection);
            system.Enable();

            Assert.AreEqual(1, system._highEntityCount);

            //Act:
            collection.Remove(high);

            //Assert:
            Assert.AreEqual(0, system._highEntityCount);
        }

        [Test]
        public void RemoveFromCollection_DuringUpdate_IsDeferred()
        {
            //Arrange:
            var high1 = CreateEntity(PriorityTag.High);
            var high2 = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(new[] { high1, high2 });
            var system = CreateSystem(collection, batchSize: 1000);
            system.Enable();
            Warmup(system);

            // Remove high2 during the update of high1
            system.OnUpdateCallback = (e, dt) =>
            {
                if (e == high1)
                    collection.Remove(high2);
            };

            //Act:
            system.Update(DeltaTime);

            //Assert: removal deferred, applied after update
            Assert.AreEqual(1, system._highEntityCount);
        }

        [Test]
        public void AddAndRemove_DuringSameUpdate_BothDeferred()
        {
            //Arrange:
            var low = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { low });
            var system = CreateSystem(collection, batchSize: 1000);
            system.Enable();
            Warmup(system);

            var newHigh = CreateEntity(PriorityTag.High);
            system.OnUpdateCallback = (e, dt) =>
            {
                collection.Remove(low);
                collection.Add(newHigh);
            };

            //Act:
            system.Update(DeltaTime);

            //Assert:
            Assert.AreEqual(0, system._lowEntityCount);
            Assert.AreEqual(1, system._highEntityCount);
        }

        [Test]
        public void Update_MultipleEntitiesInSameBucket_AllProcessed()
        {
            //Arrange:
            var entities = new Entity[20];
            for (int i = 0; i < 20; i++)
                entities[i] = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>(entities);
            var system = CreateSystem(collection, highPercent: 100, midPercent: 0, batchSize: 1000);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert: all 20 high entities processed
            Assert.AreEqual(20, system.UpdateLog.Count);
        }

        [Test]
        public void Update_RemainingQuota_SpillsToLowerBuckets()
        {
            //Arrange: batchSize=10, 80% high=8, but only 3 high entities
            // Remaining 5 high quota spills to mid, then remaining mid spills to low
            var high1 = CreateEntity(PriorityTag.High);
            var high2 = CreateEntity(PriorityTag.High);
            var high3 = CreateEntity(PriorityTag.High);
            var mid1 = CreateEntity(PriorityTag.Medium);
            var mid2 = CreateEntity(PriorityTag.Medium);
            var collection = new EntityCollection<Entity>(new[] { high1, high2, high3, mid1, mid2 });
            var system = CreateSystem(collection, highPercent: 80, midPercent: 10, batchSize: 10);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert: highQuota=8, 3 high processed, remaining=5 → midBudget=1+5=6, 2 mid processed
            Assert.AreEqual(5, system.UpdateLog.Count);
            Assert.AreEqual(3, system.UpdateLog.Count(e => e.entity.HasTag(PriorityTag.High)));
            Assert.AreEqual(2, system.UpdateLog.Count(e => e.entity.HasTag(PriorityTag.Medium)));
        }

        [Test]
        public void Update_RemainingQuota_SpillsFromMidToLow()
        {
            //Arrange: batchSize=10, highPercent=10, midPercent=10
            // highQuota=1, midQuota=1
            // With 1 high entity: processes 1, remaining=0
            // With 0 mid entities: midBudget=1+0=1, 0 processed, remaining=1
            // Low budget = 8+1 = 9
            var high = CreateEntity(PriorityTag.High);
            var low1 = CreateEntity(PriorityTag.Low);
            var low2 = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { high, low1, low2 });
            var system = CreateSystem(collection, highPercent: 10, midPercent: 10, batchSize: 10);
            system.Enable();
            Warmup(system);

            //Act:
            system.Update(DeltaTime);

            //Assert: 1 high + 2 low = 3
            Assert.AreEqual(3, system.UpdateLog.Count);
            Assert.AreEqual(1, system.UpdateLog.Count(e => e.entity.HasTag(PriorityTag.High)));
            Assert.AreEqual(2, system.UpdateLog.Count(e => e.entity.HasTag(PriorityTag.Low)));
        }

        [Test]
        public void Add_NewEntityDuringEnable_IsPlacedCorrectly()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var collection = new EntityCollection<Entity>();
            var system = CreateSystem(collection);
            system.Enable();

            //Act: add entity after enable → triggers OnAdded → system picks it up
            collection.Add(high);

            //Assert:
            Assert.AreEqual(1, system._highEntityCount);
            Assert.AreEqual(high, system._highEntities[0]);
        }

        [Test]
        public void Constructor_MultipleTriggers_AllSetActionCalled()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            var settings = CreateSettings();
            var trigger1 = new TestTrigger();
            var trigger2 = new TestTrigger();

            //Act:
            var system = new TestPrioritySystem(collection, settings, DefaultPriorityFunc, trigger1, trigger2);
            system.Enable();

            //Assert:
            Assert.IsTrue(trigger1.SetActionCalled);
            Assert.IsTrue(trigger2.SetActionCalled);
        }

        [Test]
        public void Update_EntityReceivesSameDeltaTime()
        {
            //Arrange:
            var high = CreateEntity(PriorityTag.High);
            var mid = CreateEntity(PriorityTag.Medium);
            var low = CreateEntity(PriorityTag.Low);
            var collection = new EntityCollection<Entity>(new[] { high, mid, low });
            var system = CreateSystem(collection);
            system.Enable();
            Warmup(system);

            float expectedDt = 0.033f;

            //Act:
            system.Update(expectedDt);

            //Assert: all entities receive the same deltaTime
            Assert.IsTrue(system.UpdateLog.All(e => e.dt == expectedDt));
            Assert.AreEqual(3, system.UpdateLog.Count);
        }

        #endregion
    }
}
