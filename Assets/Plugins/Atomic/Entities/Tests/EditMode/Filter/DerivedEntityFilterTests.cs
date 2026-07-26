using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class DerivedEntityFilter_Tests
    {
        // ──────────────────────────────────────────────
        //  Test doubles
        // ──────────────────────────────────────────────

        private class WarriorEntity : Entity
        {
            public int Power { get; set; }

            public WarriorEntity(string name = null, int power = 0) : base(name ?? nameof(WarriorEntity))
            {
                Power = power;
            }
        }

        private class MageEntity : Entity
        {
            public int Mana { get; set; }

            public MageEntity(string name = null, int mana = 0) : base(name ?? nameof(MageEntity))
            {
                Mana = mana;
            }
        }

        private class DerivedTriggerTestDouble<T> : IEntityTrigger<T> where T : IEntity
        {
            public Action<T> Action;
            public bool SetActionCalled;

            public readonly HashSet<T> Tracked = new();

            public void SetAction(Action<T> action)
            {
                SetActionCalled = true;
                Action = action;
            }

            public void Track(T entity) => Tracked.Add(entity);

            public void Untrack(T entity) => Tracked.Remove(entity);
        }

        // ──────────────────────────────────────────────
        //  Constructor tests
        // ──────────────────────────────────────────────

        [Test]
        public void Constructor_Should_Throw_WhenSourceIsNull()
        {
            //Arrange:
            Predicate<WarriorEntity> predicate = e => true;

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() =>
                _ = new DerivedEntityFilter<WarriorEntity, IEntity>(null, predicate));
        }

        [Test]
        public void Constructor_Should_Throw_WhenPredicateIsNull()
        {
            //Arrange:
            var source = new EntityCollection();

            //Act & Assert:
            Assert.Throws<ArgumentNullException>(() =>
                _ = new DerivedEntityFilter<WarriorEntity, IEntity>(source, null));
        }

        [Test]
        public void Constructor_Should_CallSetAction_OnAllTriggers()
        {
            //Arrange:
            var source = new EntityCollection();
            var trigger1 = new DerivedTriggerTestDouble<WarriorEntity>();
            var trigger2 = new DerivedTriggerTestDouble<WarriorEntity>();

            //Act:
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true, trigger1, trigger2);

            //Assert:
            Assert.IsTrue(trigger1.SetActionCalled);
            Assert.IsTrue(trigger2.SetActionCalled);
            Assert.IsNotNull(trigger1.Action);
            Assert.IsNotNull(trigger2.Action);
        }

        [Test]
        public void Constructor_Should_ContainMatchingEntities_WhenSourceHasExistingEntities()
        {
            //Arrange:
            var warrior = new WarriorEntity("Alex", 10);
            var source = new EntityCollection();
            source.Add(warrior);

            //Act:
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            //Assert:
            Assert.AreEqual(1, filter.Count);
            Assert.IsTrue(filter.Contains(warrior));
        }

        [Test]
        public void Constructor_Should_IgnoreNonMatchingTypeEntities()
        {
            //Arrange:
            var mage = new MageEntity("Gandalf");
            var source = new EntityCollection();
            source.Add(mage);

            //Act:
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Constructor_Should_IgnoreMatchingType_PredicateFalse()
        {
            //Arrange:
            var weakWarrior = new WarriorEntity("Peasant", 1);
            var source = new EntityCollection();
            source.Add(weakWarrior);

            //Act:
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            //Assert:
            Assert.AreEqual(0, filter.Count);
            Assert.IsFalse(filter.Contains(weakWarrior));
        }

        [Test]
        public void Constructor_Should_SubscribeToSourceEvents()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            var warrior = new WarriorEntity("Bob");
            WarriorEntity observed = null;
            filter.OnAdded += e => observed = e;

            //Act:
            source.Add(warrior);

            //Assert:
            Assert.AreSame(warrior, observed);
            Assert.AreEqual(1, filter.Count);
        }

        [Test]
        public void Constructor_Should_TrackEntitiesWithTriggers()
        {
            //Arrange:
            var warrior = new WarriorEntity("Alex");
            var source = new EntityCollection();
            source.Add(warrior);

            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();

            //Act:
            _ = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true, trigger);

            //Assert:
            Assert.AreEqual(1, trigger.Tracked.Count);
            Assert.IsTrue(trigger.Tracked.Contains(warrior));
        }

        [Test]
        public void Constructor_Should_MixMatchingAndNonMatchingEntities()
        {
            //Arrange:
            var warrior1 = new WarriorEntity("Alex", 10);
            var warrior2 = new WarriorEntity("Bob", 2);
            var mage = new MageEntity("Gandalf");
            var source = new EntityCollection();
            source.Add(warrior1);
            source.Add(mage);
            source.Add(warrior2);

            //Act:
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            //Assert:
            Assert.AreEqual(1, filter.Count);
            Assert.IsTrue(filter.Contains(warrior1));
            Assert.IsFalse(filter.Contains(warrior2));
            // MageEntity is not a WarriorEntity so it cannot enter the filter
        }

        // ──────────────────────────────────────────────
        //  Source add tests
        // ──────────────────────────────────────────────

        [Test]
        public void SourceAdd_Should_AddMatchingEntityToFilter()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            var warrior = new WarriorEntity("Alex", 10);

            //Act:
            source.Add(warrior);

            //Assert:
            Assert.AreEqual(1, filter.Count);
            Assert.IsTrue(filter.Contains(warrior));
        }

        [Test]
        public void SourceAdd_Should_FireOnAdded_WhenMatchingEntityAdded()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            WarriorEntity added = null;
            filter.OnAdded += e => added = e;

            var warrior = new WarriorEntity("Alex", 10);

            //Act:
            source.Add(warrior);

            //Assert:
            Assert.AreSame(warrior, added);
        }

        [Test]
        public void SourceAdd_Should_FireOnStateChanged_WhenMatchingEntityAdded()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            bool stateChanged = false;
            filter.OnStateChanged += () => stateChanged = true;

            //Act:
            source.Add(new WarriorEntity("Alex", 10));

            //Assert:
            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void SourceAdd_Should_IgnoreNonMatchingTypeEntity()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            bool addedFired = false;
            filter.OnAdded += _ => addedFired = true;

            var mage = new MageEntity("Gandalf");

            //Act:
            source.Add(mage);

            //Assert:
            Assert.AreEqual(0, filter.Count);
            Assert.IsFalse(addedFired);
        }

        [Test]
        public void SourceAdd_Should_IgnoreMatchingType_PredicateFalse()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 10);

            bool addedFired = false;
            filter.OnAdded += _ => addedFired = true;

            var warrior = new WarriorEntity("Weakling", 3);

            //Act:
            source.Add(warrior);

            //Assert:
            Assert.AreEqual(0, filter.Count);
            Assert.IsFalse(addedFired);
        }

        [Test]
        public void SourceAdd_Should_NotFireOnStateChanged_WhenPredicateFalse()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 10);

            bool stateChanged = false;
            filter.OnStateChanged += () => stateChanged = true;

            //Act:
            source.Add(new WarriorEntity("Weakling", 3));

            //Assert:
            Assert.IsFalse(stateChanged);
        }

        [Test]
        public void SourceAdd_Should_AddMultipleMatchingEntities()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            var w1 = new WarriorEntity("W1", 1);
            var w2 = new WarriorEntity("W2", 2);
            var w3 = new WarriorEntity("W3", 3);

            //Act:
            source.Add(w1);
            source.Add(w2);
            source.Add(w3);

            //Assert:
            Assert.AreEqual(3, filter.Count);
            Assert.IsTrue(filter.Contains(w1));
            Assert.IsTrue(filter.Contains(w2));
            Assert.IsTrue(filter.Contains(w3));
        }

        // ──────────────────────────────────────────────
        //  Source remove tests
        // ──────────────────────────────────────────────

        [Test]
        public void SourceRemove_Should_RemoveEntityFromFilter()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            Assert.AreEqual(1, filter.Count);

            //Act:
            source.Remove(warrior);

            //Assert:
            Assert.AreEqual(0, filter.Count);
            Assert.IsFalse(filter.Contains(warrior));
        }

        [Test]
        public void SourceRemove_Should_FireOnRemoved_WhenEntityRemoved()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            WarriorEntity removed = null;
            filter.OnRemoved += e => removed = e;

            //Act:
            source.Remove(warrior);

            //Assert:
            Assert.AreSame(warrior, removed);
        }

        [Test]
        public void SourceRemove_Should_FireOnStateChanged_WhenEntityRemoved()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            bool stateChanged = false;
            filter.OnStateChanged += () => stateChanged = true;

            //Act:
            source.Remove(warrior);

            //Assert:
            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void SourceRemove_Should_NotFireOnRemoved_WhenEntityNotInFilter()
        {
            //Arrange:
            var source = new EntityCollection();
            var weakWarrior = new WarriorEntity("Weakling", 1);
            source.Add(weakWarrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 10);

            bool removedFired = false;
            filter.OnRemoved += _ => removedFired = true;

            //Act:
            source.Remove(weakWarrior);

            //Assert:
            Assert.IsFalse(removedFired);
        }

        [Test]
        public void SourceRemove_Should_NotFireOnStateChanged_WhenEntityNotInFilter()
        {
            //Arrange:
            var source = new EntityCollection();
            var weakWarrior = new WarriorEntity("Weakling", 1);
            source.Add(weakWarrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 10);

            bool stateChanged = false;
            filter.OnStateChanged += () => stateChanged = true;

            //Act:
            source.Remove(weakWarrior);

            //Assert:
            Assert.IsFalse(stateChanged);
        }

        [Test]
        public void SourceRemove_Should_NotFireEvents_WhenNonMatchingTypeRemoved()
        {
            //Arrange:
            var source = new EntityCollection();
            var mage = new MageEntity("Gandalf");
            source.Add(mage);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            bool removedFired = false;
            filter.OnRemoved += _ => removedFired = true;

            //Act:
            source.Remove(mage);

            //Assert:
            Assert.IsFalse(removedFired);
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void SourceRemove_Should_UntrackEntity_WhenRemoved()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            _ = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true, trigger);

            Assert.AreEqual(1, trigger.Tracked.Count);

            //Act:
            source.Remove(warrior);

            //Assert:
            Assert.AreEqual(0, trigger.Tracked.Count);
        }

        // ──────────────────────────────────────────────
        //  Count tests
        // ──────────────────────────────────────────────

        [Test]
        public void Count_Should_BeZero_WhenFilterIsEmpty()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Count_Should_ReflectFilteredEntities()
        {
            //Arrange:
            var source = new EntityCollection();
            source.Add(new WarriorEntity("W1", 10));
            source.Add(new MageEntity("M1"));
            source.Add(new WarriorEntity("W2", 20));
            source.Add(new MageEntity("M2"));

            //Act:
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.AreEqual(2, filter.Count);
        }

        [Test]
        public void Count_Should_Decrease_WhenEntityRemoved()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            Assert.AreEqual(1, filter.Count);

            //Act:
            source.Remove(warrior);

            //Assert:
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Count_Should_Update_WhenPredicateChanges_ThroughSynchronize()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            Assert.AreEqual(1, filter.Count);

            //Act:
            warrior.Power = 1;
            filter.Synchronize(warrior);

            //Assert:
            Assert.AreEqual(0, filter.Count);
        }

        // ──────────────────────────────────────────────
        //  Indexer tests
        // ──────────────────────────────────────────────

        [Test]
        public void Indexer_Should_ReturnCorrectEntity_AtIndex0()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.AreSame(warrior, filter[0]);
        }

        [Test]
        public void Indexer_Should_ReturnCorrectEntity_AtIndex1()
        {
            //Arrange:
            var source = new EntityCollection();
            var w1 = new WarriorEntity("W1", 10);
            var w2 = new WarriorEntity("W2", 20);
            source.Add(w1);
            source.Add(w2);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.AreSame(w1, filter[0]);
            Assert.AreSame(w2, filter[1]);
        }

        [Test]
        public void Indexer_Should_Throw_WhenIndexNegative()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = filter[-1]);
        }

        [Test]
        public void Indexer_Should_Throw_WhenIndexExceedsCount()
        {
            //Arrange:
            var source = new EntityCollection();
            source.Add(new WarriorEntity("W1", 10));

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = filter[1]);
        }

        // ──────────────────────────────────────────────
        //  TryGetAt tests
        // ──────────────────────────────────────────────

        [Test]
        public void TryGetAt_Should_ReturnTrueAndEntity_WhenIndexValid()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act:
            bool result = filter.TryGetAt(0, out var entity);

            //Assert:
            Assert.IsTrue(result);
            Assert.AreSame(warrior, entity);
        }

        [Test]
        public void TryGetAt_Should_ReturnFalse_WhenIndexOutOfRange()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act:
            bool result = filter.TryGetAt(0, out var entity);

            //Assert:
            Assert.IsFalse(result);
            Assert.IsNull(entity);
        }

        [Test]
        public void TryGetAt_Should_ReturnFalse_WhenIndexNegative()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act:
            bool result = filter.TryGetAt(-1, out var entity);

            //Assert:
            Assert.IsFalse(result);
            Assert.IsNull(entity);
        }

        [Test]
        public void TryGetAt_Should_ReturnFalse_WhenIndexExceedsCount()
        {
            //Arrange:
            var source = new EntityCollection();
            source.Add(new WarriorEntity("W1", 10));

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act:
            bool result = filter.TryGetAt(5, out var entity);

            //Assert:
            Assert.IsFalse(result);
            Assert.IsNull(entity);
        }

        // ──────────────────────────────────────────────
        //  Contains tests
        // ──────────────────────────────────────────────

        [Test]
        public void Contains_Should_ReturnTrue_WhenEntityInFilter()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.IsTrue(filter.Contains(warrior));
        }

        [Test]
        public void Contains_Should_ReturnFalse_WhenEntityNotInFilter_PredicateFalse()
        {
            //Arrange:
            var source = new EntityCollection();
            var weakWarrior = new WarriorEntity("Weakling", 1);
            source.Add(weakWarrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            //Assert:
            Assert.IsFalse(filter.Contains(weakWarrior));
        }

        [Test]
        public void Contains_Should_ReturnFalse_WhenEntityNotInSource()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            var orphan = new WarriorEntity("Orphan", 10);

            //Assert:
            Assert.IsFalse(filter.Contains(orphan));
        }

        [Test]
        public void Contains_Should_ReturnFalse_WhenFilterDisposed()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            filter.Dispose();

            //Assert:
            Assert.IsFalse(filter.Contains(warrior));
        }

        // ──────────────────────────────────────────────
        //  CopyTo tests
        // ──────────────────────────────────────────────

        [Test]
        public void CopyTo_Should_CopyAllEntitiesToICollection()
        {
            //Arrange:
            var source = new EntityCollection();
            var w1 = new WarriorEntity("W1", 10);
            var w2 = new WarriorEntity("W2", 20);
            source.Add(w1);
            source.Add(w2);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            var results = new List<WarriorEntity>();

            //Act:
            filter.CopyTo(results);

            //Assert:
            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Contains(w1));
            Assert.IsTrue(results.Contains(w2));
        }

        [Test]
        public void CopyTo_Should_CopyToCorrectArrayIndex()
        {
            //Arrange:
            var source = new EntityCollection();
            var w1 = new WarriorEntity("W1", 10);
            var w2 = new WarriorEntity("W2", 20);
            source.Add(w1);
            source.Add(w2);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            var array = new WarriorEntity[4];

            //Act:
            filter.CopyTo(array, 1);

            //Assert:
            Assert.IsNull(array[0]);
            Assert.AreSame(w1, array[1]);
            Assert.AreSame(w2, array[2]);
            Assert.IsNull(array[3]);
        }

        [Test]
        public void CopyTo_Should_CopyEmpty_WhenFilterIsEmpty()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            var results = new List<WarriorEntity>();

            //Act:
            filter.CopyTo(results);

            //Assert:
            Assert.AreEqual(0, results.Count);
        }

        // ──────────────────────────────────────────────
        //  GetEnumerator tests
        // ──────────────────────────────────────────────

        [Test]
        public void GetEnumerator_Should_IterateAllFilteredEntities()
        {
            //Arrange:
            var source = new EntityCollection();
            var w1 = new WarriorEntity("W1", 10);
            var w2 = new WarriorEntity("W2", 20);
            source.Add(w1);
            source.Add(w2);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act:
            var collected = new List<WarriorEntity>();
            using var enumerator = filter.GetEnumerator();
            while (enumerator.MoveNext())
                collected.Add(enumerator.Current);

            //Assert:
            Assert.AreEqual(2, collected.Count);
            Assert.IsTrue(collected.Contains(w1));
            Assert.IsTrue(collected.Contains(w2));
        }

        [Test]
        public void GetEnumerator_Should_IterateEmpty_WhenFilterIsEmpty()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act:
            var collected = new List<WarriorEntity>();
            using var enumerator = filter.GetEnumerator();
            while (enumerator.MoveNext())
                collected.Add(enumerator.Current);

            //Assert:
            Assert.AreEqual(0, collected.Count);
        }

        [Test]
        public void GetEnumerator_Should_SkipNonMatchingType()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("W1", 10);
            var mage = new MageEntity("M1");
            source.Add(warrior);
            source.Add(mage);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act:
            var collected = new List<WarriorEntity>();
            using var enumerator = filter.GetEnumerator();
            while (enumerator.MoveNext())
                collected.Add(enumerator.Current);

            //Assert:
            Assert.AreEqual(1, collected.Count);
            Assert.AreSame(warrior, collected[0]);
        }

        [Test]
        public void IEnumerable_GetEnumerator_Should_Work()
        {
            //Arrange:
            var source = new EntityCollection();
            var w1 = new WarriorEntity("W1", 10);
            source.Add(w1);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act:
            var collected = new List<WarriorEntity>();
            System.Collections.IEnumerator nonGeneric = ((System.Collections.IEnumerable)filter).GetEnumerator();
            while (nonGeneric.MoveNext())
                collected.Add((WarriorEntity)nonGeneric.Current);

            //Assert:
            Assert.AreEqual(1, collected.Count);
            Assert.AreSame(w1, collected[0]);
        }

        // ──────────────────────────────────────────────
        //  Dispose tests
        // ──────────────────────────────────────────────

        [Test]
        public void Dispose_Should_ClearFilterState()
        {
            //Arrange:
            var source = new EntityCollection();
            source.Add(new WarriorEntity("W1", 10));
            source.Add(new WarriorEntity("W2", 20));

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            Assert.AreEqual(2, filter.Count);

            //Act:
            filter.Dispose();

            //Assert:
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Dispose_Should_UnsubscribeFromSourceEvents()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);
            filter.Dispose();

            bool addedFired = false;
            filter.OnAdded += _ => addedFired = true;

            //Act:
            source.Add(new WarriorEntity("W1", 10));

            //Assert:
            Assert.IsFalse(addedFired);
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Dispose_Should_StopReceivingSourceRemovals()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("W1", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            filter.Dispose();

            bool removedFired = false;
            filter.OnRemoved += _ => removedFired = true;

            //Act:
            source.Remove(warrior);

            //Assert:
            Assert.IsFalse(removedFired);
        }

        [Test]
        public void Dispose_Should_CallUntrack_OnAllTriggers()
        {
            //Arrange:
            var w1 = new WarriorEntity("W1", 10);
            var w2 = new WarriorEntity("W2", 20);

            var source = new EntityCollection();
            source.Add(w1);
            source.Add(w2);

            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true, trigger);

            Assert.AreEqual(2, trigger.Tracked.Count);

            //Act:
            filter.Dispose();

            //Assert:
            Assert.AreEqual(0, trigger.Tracked.Count);
        }

        [Test]
        public void Dispose_CalledTwice_Should_NotThrow()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Act & Assert:
            Assert.DoesNotThrow(() =>
            {
                filter.Dispose();
                filter.Dispose();
            });
        }

        // ──────────────────────────────────────────────
        //  Synchronize / Trigger integration tests
        // ──────────────────────────────────────────────

        [Test]
        public void Synchronize_Should_AddEntity_WhenPredicateBecomesTrue()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 1);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            Assert.AreEqual(0, filter.Count);

            WarriorEntity added = null;
            filter.OnAdded += e => added = e;

            //Act:
            warrior.Power = 10;
            filter.Synchronize(warrior);

            //Assert:
            Assert.AreEqual(1, filter.Count);
            Assert.IsTrue(filter.Contains(warrior));
            Assert.AreSame(warrior, added);
        }

        [Test]
        public void Synchronize_Should_RemoveEntity_WhenPredicateBecomesFalse()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            Assert.AreEqual(1, filter.Count);

            WarriorEntity removed = null;
            filter.OnRemoved += e => removed = e;

            //Act:
            warrior.Power = 1;
            filter.Synchronize(warrior);

            //Assert:
            Assert.AreEqual(0, filter.Count);
            Assert.IsFalse(filter.Contains(warrior));
            Assert.AreSame(warrior, removed);
        }

        [Test]
        public void Synchronize_Should_NotFireEvents_WhenNoChange()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            bool addedFired = false;
            bool removedFired = false;
            bool stateChanged = false;
            filter.OnAdded += _ => addedFired = true;
            filter.OnRemoved += _ => removedFired = true;
            filter.OnStateChanged += () => stateChanged = true;

            //Act:
            filter.Synchronize(warrior);

            //Assert:
            Assert.IsFalse(addedFired);
            Assert.IsFalse(removedFired);
            Assert.IsFalse(stateChanged);
        }

        [Test]
        public void Synchronize_Should_NotFireOnRemoved_WhenEntityWasNeverInFilter()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 1);
            source.Add(warrior);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 10);

            bool removedFired = false;
            filter.OnRemoved += _ => removedFired = true;

            //Act: entity already doesn't match, synchronize should be a no-op
            filter.Synchronize(warrior);

            //Assert:
            Assert.IsFalse(removedFired);
        }

        [Test]
        public void Trigger_Should_CauseSynchronize_WhenInvoked()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 1);
            source.Add(warrior);

            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5, trigger);

            Assert.AreEqual(0, filter.Count);

            //Act: simulate the trigger firing its synchronize action
            warrior.Power = 10;
            trigger.Action?.Invoke(warrior);

            //Assert:
            Assert.AreEqual(1, filter.Count);
            Assert.IsTrue(filter.Contains(warrior));
        }

        [Test]
        public void Trigger_Should_RemoveEntity_WhenStateChangeCausesPredicateFalse()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5, trigger);

            Assert.AreEqual(1, filter.Count);

            WarriorEntity removed = null;
            filter.OnRemoved += e => removed = e;

            //Act:
            warrior.Power = 1;
            trigger.Action?.Invoke(warrior);

            //Assert:
            Assert.AreEqual(0, filter.Count);
            Assert.AreSame(warrior, removed);
        }

        [Test]
        public void Trigger_Should_TrackNewEntity_WhenAddedToSource()
        {
            //Arrange:
            var source = new EntityCollection();
            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true, trigger);

            var warrior = new WarriorEntity("Alex", 10);

            //Act:
            source.Add(warrior);

            //Assert:
            Assert.IsTrue(trigger.Tracked.Contains(warrior));
        }

        [Test]
        public void Trigger_Should_UntrackEntity_WhenRemovedFromSource()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            _ = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true, trigger);

            Assert.IsTrue(trigger.Tracked.Contains(warrior));

            //Act:
            source.Remove(warrior);

            //Assert:
            Assert.IsFalse(trigger.Tracked.Contains(warrior));
        }

        [Test]
        public void Trigger_Should_NotTrack_WhenNonMatchingTypeAdded()
        {
            //Arrange:
            var source = new EntityCollection();
            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true, trigger);

            var mage = new MageEntity("Gandalf");

            //Act:
            source.Add(mage);

            //Assert:
            Assert.AreEqual(0, trigger.Tracked.Count);
        }

        // ──────────────────────────────────────────────
        //  Combined scenario tests
        // ──────────────────────────────────────────────

        [Test]
        public void FullScenario_AddMatchRemoveMatch_Synchronize()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 3);
            var addedLog = new List<string>();
            var removedLog = new List<string>();

            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power >= 5, trigger);

            filter.OnAdded += e => addedLog.Add(e.Name);
            filter.OnRemoved += e => removedLog.Add(e.Name);

            //Act 1: Add warrior (power=3, doesn't match)
            source.Add(warrior);
            Assert.AreEqual(0, filter.Count);

            //Act 2: Power up to 10 via trigger
            warrior.Power = 10;
            trigger.Action.Invoke(warrior);
            Assert.AreEqual(1, filter.Count);
            Assert.AreEqual(1, addedLog.Count);

            //Act 3: Power down to 2 via trigger
            warrior.Power = 2;
            trigger.Action.Invoke(warrior);
            Assert.AreEqual(0, filter.Count);
            Assert.AreEqual(1, removedLog.Count);

            //Assert:
            Assert.AreEqual("Alex", addedLog[0]);
            Assert.AreEqual("Alex", removedLog[0]);
        }

        [Test]
        public void FullScenario_MultipleEntities_IndependentFiltering()
        {
            //Arrange:
            var source = new EntityCollection();
            var w1 = new WarriorEntity("W1", 3);
            var w2 = new WarriorEntity("W2", 7);
            var w3 = new WarriorEntity("W3", 12);
            var mage = new MageEntity("M1");

            source.Add(w1);
            source.Add(w2);
            source.Add(w3);
            source.Add(mage);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            //Assert initial state:
            Assert.AreEqual(2, filter.Count);
            Assert.IsFalse(filter.Contains(w1));
            Assert.IsTrue(filter.Contains(w2));
            Assert.IsTrue(filter.Contains(w3));

            //Act: remove matching entity from source
            source.Remove(w2);
            Assert.AreEqual(1, filter.Count);
            Assert.IsTrue(filter.Contains(w3));

            //Act: remove last matching entity
            source.Remove(w3);
            Assert.AreEqual(0, filter.Count);

            //Act: add new matching entity
            var w4 = new WarriorEntity("W4", 20);
            source.Add(w4);
            Assert.AreEqual(1, filter.Count);
            Assert.IsTrue(filter.Contains(w4));

            //Assert: non-matching warriors never entered filter
            Assert.IsFalse(filter.Contains(w1));
            // MageEntity is not a WarriorEntity so it cannot enter the filter
        }

        [Test]
        public void FullScenario_Dispose_StopsAllTracking()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);
            source.Add(warrior);

            var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0, trigger);

            Assert.AreEqual(1, filter.Count);
            Assert.AreEqual(1, trigger.Tracked.Count);

            bool addedFired = false;
            bool removedFired = false;
            filter.OnAdded += _ => addedFired = true;
            filter.OnRemoved += _ => removedFired = true;

            //Act:
            filter.Dispose();

            //Assert: state cleared
            Assert.AreEqual(0, filter.Count);

            //Assert: untracked
            Assert.AreEqual(0, trigger.Tracked.Count);

            //Reset flags — Dispose intentionally fires OnRemoved during cleanup
            addedFired = false;
            removedFired = false;

            //Assert: source add no longer observed
            var newWarrior = new WarriorEntity("Bob", 10);
            source.Add(newWarrior);
            Assert.IsFalse(addedFired);

            //Assert: source remove no longer observed
            source.Remove(warrior);
            Assert.IsFalse(removedFired);
        }

        // ──────────────────────────────────────────────
        //  DerivedEntityFilter<T> (single type param) tests
        // ──────────────────────────────────────────────

        [TestFixture]
        public class DerivedEntityFilter_SingleParam_Tests
        {
            [Test]
            public void Constructor_Should_Throw_WhenSourceIsNull()
            {
                //Arrange:
                Predicate<WarriorEntity> predicate = e => true;

                //Act & Assert:
                Assert.Throws<ArgumentNullException>(() =>
                    _ = new DerivedEntityFilter<WarriorEntity>(null, predicate));
            }

            [Test]
            public void Constructor_Should_Throw_WhenPredicateIsNull()
            {
                //Arrange:
                var source = new EntityCollection();

                //Act & Assert:
                Assert.Throws<ArgumentNullException>(() =>
                    _ = new DerivedEntityFilter<WarriorEntity>(source, null));
            }

            [Test]
            public void Constructor_Should_ContainMatchingEntities()
            {
                //Arrange:
                var warrior = new WarriorEntity("Alex", 10);
                var source = new EntityCollection();
                source.Add(warrior);

                //Act:
                var filter = new DerivedEntityFilter<WarriorEntity>(
                    source, e => e.Power > 5);

                //Assert:
                Assert.AreEqual(1, filter.Count);
                Assert.IsTrue(filter.Contains(warrior));
            }

            [Test]
            public void Constructor_Should_IgnoreNonMatchingTypes()
            {
                //Arrange:
                var source = new EntityCollection();
                source.Add(new MageEntity("Gandalf"));

                //Act:
                var filter = new DerivedEntityFilter<WarriorEntity>(source, e => true);

                //Assert:
                Assert.AreEqual(0, filter.Count);
            }

            [Test]
            public void SourceAdd_Should_FireOnAdded_WhenMatchingEntityAdded()
            {
                //Arrange:
                var source = new EntityCollection();
                var filter = new DerivedEntityFilter<WarriorEntity>(source, e => true);

                WarriorEntity added = null;
                filter.OnAdded += e => added = e;

                var warrior = new WarriorEntity("Alex", 10);

                //Act:
                source.Add(warrior);

                //Assert:
                Assert.AreSame(warrior, added);
                Assert.AreEqual(1, filter.Count);
            }

            [Test]
            public void SourceRemove_Should_FireOnRemoved()
            {
                //Arrange:
                var source = new EntityCollection();
                var warrior = new WarriorEntity("Alex", 10);
                source.Add(warrior);

                var filter = new DerivedEntityFilter<WarriorEntity>(source, e => true);

                WarriorEntity removed = null;
                filter.OnRemoved += e => removed = e;

                //Act:
                source.Remove(warrior);

                //Assert:
                Assert.AreSame(warrior, removed);
                Assert.AreEqual(0, filter.Count);
            }

            [Test]
            public void Trigger_Should_ReEvaluateEntity()
            {
                //Arrange:
                var source = new EntityCollection();
                var warrior = new WarriorEntity("Alex", 1);
                source.Add(warrior);

                var trigger = new DerivedTriggerTestDouble<WarriorEntity>();
                var filter = new DerivedEntityFilter<WarriorEntity>(
                    source, e => e.Power > 5, trigger);

                Assert.AreEqual(0, filter.Count);

                //Act:
                warrior.Power = 10;
                trigger.Action.Invoke(warrior);

                //Assert:
                Assert.AreEqual(1, filter.Count);
                Assert.IsTrue(filter.Contains(warrior));
            }

            [Test]
            public void Dispose_Should_ClearAndUnsubscribe()
            {
                //Arrange:
                var source = new EntityCollection();
                source.Add(new WarriorEntity("W1", 10));

                var filter = new DerivedEntityFilter<WarriorEntity>(
                    source, e => true);

                Assert.AreEqual(1, filter.Count);

                //Act:
                filter.Dispose();

                //Assert:
                Assert.AreEqual(0, filter.Count);

                bool addedFired = false;
                filter.OnAdded += _ => addedFired = true;
                source.Add(new WarriorEntity("W2", 20));
                Assert.IsFalse(addedFired);
            }
        }

        // ──────────────────────────────────────────────
        //  Edge case tests
        // ──────────────────────────────────────────────

        [Test]
        public void SourceAdd_DuplicateEntity_ShouldNotAddTwice()
        {
            //Arrange:
            var source = new EntityCollection();
            var warrior = new WarriorEntity("Alex", 10);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            source.Add(warrior);
            Assert.AreEqual(1, filter.Count);

            bool addedFired = false;
            filter.OnAdded += _ => addedFired = true;

            //Act: add same entity again to source
            source.Add(warrior);

            //Assert: EntityCollection.Add returns false for duplicates, so filter won't get OnAdded
            Assert.AreEqual(1, filter.Count);
            Assert.IsFalse(addedFired, "OnAdded must not fire for duplicate add");
        }

        [Test]
        public void Indexer_Should_ReflectDynamicChanges()
        {
            //Arrange:
            var source = new EntityCollection();
            var w1 = new WarriorEntity("W1", 10);
            var w2 = new WarriorEntity("W2", 20);
            source.Add(w1);
            source.Add(w2);

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);

            Assert.AreEqual(2, filter.Count);

            //Act: remove w2 from source
            source.Remove(w2);

            //Assert: indexer should only have w1
            Assert.AreEqual(1, filter.Count);
            Assert.AreSame(w1, filter[0]);
        }

        [Test]
        public void CopyTo_ShouldWork_AfterDispose()
        {
            //Arrange:
            var source = new EntityCollection();
            source.Add(new WarriorEntity("W1", 10));

            var filter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            filter.Dispose();

            var results = new List<WarriorEntity>();

            //Act:
            filter.CopyTo(results);

            //Assert:
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void MultipleFilters_SameSource_ShouldBeIndependent()
        {
            //Arrange:
            var source = new EntityCollection();
            var w1 = new WarriorEntity("W1", 3);
            var w2 = new WarriorEntity("W2", 10);
            source.Add(w1);
            source.Add(w2);

            var strongFilter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);
            var allFilter = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => true);

            //Assert:
            Assert.AreEqual(1, strongFilter.Count);
            Assert.AreEqual(2, allFilter.Count);
            Assert.IsFalse(strongFilter.Contains(w1));
            Assert.IsTrue(strongFilter.Contains(w2));
            Assert.IsTrue(allFilter.Contains(w1));
            Assert.IsTrue(allFilter.Contains(w2));
        }

        [Test]
        public void MultipleFilters_ShouldBothReceiveSourceEvents()
        {
            //Arrange:
            var source = new EntityCollection();
            var filter1 = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 5);
            var filter2 = new DerivedEntityFilter<WarriorEntity, IEntity>(
                source, e => e.Power > 0);

            WarriorEntity added1 = null;
            WarriorEntity added2 = null;
            filter1.OnAdded += e => added1 = e;
            filter2.OnAdded += e => added2 = e;

            var warrior = new WarriorEntity("Alex", 10);

            //Act:
            source.Add(warrior);

            //Assert:
            Assert.AreSame(warrior, added1);
            Assert.AreSame(warrior, added2);
            Assert.AreEqual(1, filter1.Count);
            Assert.AreEqual(1, filter2.Count);
        }
    }
}
