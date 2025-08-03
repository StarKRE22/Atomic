using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        #region OnBehaviourAdded

        [Test]
        public void OnBehaviourAdded_IsInvoked_WhenBehaviourIsAdded()
        {
            var entity = new Entity();
            var behaviour = new DummyEntityBehaviour();

            IEntity calledEntity = null;
            IEntityBehaviour calledBehaviour = null;

            entity.OnBehaviourAdded += (e, b) =>
            {
                calledEntity = e;
                calledBehaviour = b;
            };

            entity.AddBehaviour(behaviour);

            Assert.AreSame(entity, calledEntity);
            Assert.AreSame(behaviour, calledBehaviour);
        }

        [Test]
        public void OnBehaviourAdded_IsNotInvoked_WhenBehaviourAlreadyExists()
        {
            var entity = new Entity();
            var behaviour = new DummyEntityBehaviour();
            entity.AddBehaviour(behaviour);

            bool wasCalled = false;
            entity.OnBehaviourAdded += (_, _) => wasCalled = true;

            // Повторное добавление не должно вызвать событие
            entity.AddBehaviour(behaviour);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnBehaviourAdded_IsNotInvoked_WhenNullBehaviour()
        {
            var entity = new Entity();
            bool wasCalled = false;

            entity.OnBehaviourAdded += (_, _) => wasCalled = true;

            Assert.Throws<ArgumentNullException>(() => { entity.AddBehaviour(null); });

            Assert.IsFalse(wasCalled);
        }

        #endregion

        #region OnBehaviourDeleted

        [Test]
        public void OnBehaviourDeleted_IsInvoked_WhenBehaviourIsDeleted()
        {
            var entity = new Entity();
            var behaviour = new DummyEntityBehaviour();
            entity.AddBehaviour(behaviour);

            IEntity calledEntity = null;
            IEntityBehaviour calledBehaviour = null;

            entity.OnBehaviourDeleted += (e, b) =>
            {
                calledEntity = e;
                calledBehaviour = b;
            };

            entity.DelBehaviour(behaviour);

            Assert.AreSame(entity, calledEntity);
            Assert.AreSame(behaviour, calledBehaviour);
        }

        [Test]
        public void OnBehaviourDeleted_IsNotInvoked_WhenBehaviourDoesNotExist()
        {
            var entity = new Entity();
            var behaviour = new DummyEntityBehaviour();

            bool wasCalled = false;
            entity.OnBehaviourDeleted += (_, _) => wasCalled = true;

            // Удаление несуществующего поведения
            entity.DelBehaviour(behaviour);

            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnBehaviourDeleted_IsInvoked_ForEach_WhenClearBehavioursCalled()
        {
            var a = new DummyEntityBehaviour();
            var b = new DummyEntityBehaviour();

            var deleted = new List<IEntityBehaviour>();
            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b});

            entity.OnBehaviourDeleted += (_, behaviour) => { deleted.Add(behaviour); };

            entity.ClearBehaviours();

            CollectionAssert.AreEquivalent(new[] {a, b}, deleted);
        }

        #endregion

        #region BehaviourCount

        [Test]
        public void BehaviourCount_ReturnsZero_WhenNoBehaviours()
        {
            var entity = new Entity();

            Assert.AreEqual(0, entity.BehaviourCount);
        }

        [Test]
        public void BehaviourCount_Increases_WhenBehavioursAdded()
        {
            var entity = new Entity();
            entity.AddBehaviour(new DummyEntityBehaviour());
            entity.AddBehaviour(new DummyEntityBehaviour());

            Assert.AreEqual(2, entity.BehaviourCount);
        }

        [Test]
        public void BehaviourCount_Decreases_WhenBehaviourRemoved()
        {
            var a = new DummyEntityBehaviour();
            var b = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b});

            entity.DelBehaviour(a);

            Assert.AreEqual(1, entity.BehaviourCount);
        }

        [Test]
        public void BehaviourCount_Zero_AfterClear()
        {
            var entity = new Entity();
            entity.AddBehaviour(new DummyEntityBehaviour());

            entity.ClearBehaviours();

            Assert.AreEqual(0, entity.BehaviourCount);
        }

        #endregion

        #region GetAllBehaviours

        [Test]
        public void GetAllBehaviours_ReturnsAllAttachedBehaviours()
        {
            // Arrange
            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();
            var behaviourStub = new DummyEntityBehaviour();

            var expectedBehaviours = new IEntityBehaviour[]
            {
                updateStub,
                initStub,
                behaviourStub
            };

            var entity = new Entity(null, null, null, expectedBehaviours);

            // Act
            var actualBehaviours = entity.GetBehaviours();

            // Assert
            CollectionAssert.AreEquivalent(expectedBehaviours, actualBehaviours);
        }

        [Test]
        public void GetBehaviours_ReturnsAllAddedBehaviours_InOrder()
        {
            var a = new DummyEntityBehaviour();
            var b = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b});

            var result = entity.GetBehaviours();

            Assert.AreEqual(2, result.Length);
            Assert.AreSame(a, result[0]);
            Assert.AreSame(b, result[1]);
        }

        [Test]
        public void GetBehaviours_ReturnsEmptyArray_WhenNoBehaviours()
        {
            var entity = new Entity();

            var result = entity.GetBehaviours();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetBehaviours_ReturnedArrayIsIndependentCopy()
        {
            var behaviour = new DummyEntityBehaviour();
            var entity = new Entity();
            entity.AddBehaviour(behaviour);

            var result = entity.GetBehaviours();

            // Clear internal behaviours
            entity.ClearBehaviours();

            // The returned array should still contain the item
            Assert.AreEqual(1, result.Length);
            Assert.AreSame(behaviour, result[0]);
        }

        #endregion

        #region HasBehaviour

        [Test]
        public void HasBehaviour()
        {
            //Arrange:
            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();
            var behaviourStub = new DummyEntityBehaviour();

            var entity = new Entity(null, null, null, new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            //Assert & Act:
            Assert.IsTrue(entity.HasBehaviour(updateStub));
            Assert.IsTrue(entity.HasBehaviour<EntitySpawnedStub>());
            Assert.IsFalse(entity.HasBehaviour(behaviourStub));
        }

        [Test]
        public void HasBehaviour_ReturnsTrue_WhenBehaviourOfTypeExists()
        {
            var entity = new Entity();
            entity.AddBehaviour(new DummyEntityBehaviour());

            bool result = entity.HasBehaviour<DummyEntityBehaviour>();

            Assert.IsTrue(result);
        }

        [Test]
        public void HasBehaviour_ReturnsFalse_WhenNoBehaviours()
        {
            var entity = new Entity();

            bool result = entity.HasBehaviour<DummyEntityBehaviour>();

            Assert.IsFalse(result);
        }

        [Test]
        public void HasBehaviour_ReturnsTrue_WhenMultipleExist()
        {
            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[]
            {
                new DummyEntityBehaviour(),
                new DummyEntityBehaviour()
            });

            bool result = entity.HasBehaviour<DummyEntityBehaviour>();

            Assert.IsTrue(result);
        }

        [Test]
        public void HasBehaviour_ReturnsFalse_WhenTypeDoesNotMatch()
        {
            var entity = new Entity();
            entity.AddBehaviour(new EntityUpdateStub());

            bool result = entity.HasBehaviour<IEntitySpawned>();

            Assert.IsFalse(result);
        }

        #endregion

        #region AddBehaviour

        [Test]
        public void AddBehaviour_WhenAlreadyPresent_DoesNotRaiseEvent()
        {
            // Arrange
            var updateStub = new EntityUpdateStub();
            IEntityBehaviour addedBehaviour = null;

            var entity = new Entity(null, null, null, new IEntityBehaviour[] {updateStub});
            entity.OnBehaviourAdded += (_, b) => addedBehaviour = b;

            // Act
            entity.AddBehaviour(updateStub);

            // Assert
            Assert.IsNull(addedBehaviour);
        }

        [Test]
        public void AddBehaviour_WhenNew_RaisesEvent()
        {
            // Arrange
            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();
            IEntityBehaviour addedBehaviour = null;

            var entity = new Entity(null, null, null, new IEntityBehaviour[] {updateStub});
            entity.OnBehaviourAdded += (_, b) => addedBehaviour = b;

            // Act
            entity.AddBehaviour(initStub);

            // Assert
            Assert.AreEqual(initStub, addedBehaviour);
        }

        [Test]
        public void AddBehaviour_Generic_AddsInstanceAndRaisesEvent()
        {
            // Arrange
            IEntityBehaviour addedBehaviour = null;

            var entity = new Entity();
            entity.OnBehaviourAdded += (_, b) => addedBehaviour = b;

            // Act
            entity.AddBehaviour<DummyEntityBehaviour>();

            // Assert
            Assert.IsNotNull(addedBehaviour);
            Assert.IsInstanceOf<DummyEntityBehaviour>(addedBehaviour);
            Assert.IsTrue(entity.HasBehaviour<DummyEntityBehaviour>());
        }

        [Test]
        public void AddBehaviour_AfterSpawn_BehaviourSpawned()
        {
            //Arrange:
            var behaviourStub = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.Spawn();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsFalse(behaviourStub.Activated);
            Assert.AreEqual(nameof(DummyEntityBehaviour.OnSpawn), behaviourStub.InvocationList[0]);
        }

        [Test]
        public void AddBehaviour_AfterEnable_BehaviourSpawnedAndEnabled()
        {
            //Arrange:
            var behaviourStub = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.Spawn();
            entity.Activate();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.Spawned);
            Assert.IsTrue(behaviourStub.Activated);
            Assert.AreEqual(nameof(DummyEntityBehaviour.OnSpawn), behaviourStub.InvocationList[0]);
            Assert.AreEqual(nameof(DummyEntityBehaviour.OnActive), behaviourStub.InvocationList[1]);
        }

        #endregion

        #region DelBehaviour

        [Test]
        public void DelBehaviour()
        {
            //Arrange:
            IEntityBehaviour removedBehaviour = null;

            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();
            var behaviourStub = new DummyEntityBehaviour();

            var entity = new Entity(null, null, null, new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            entity.OnBehaviourDeleted += (_, b) => removedBehaviour = b;

            //Assert & Act:
            Assert.IsTrue(entity.DelBehaviour(updateStub));
            Assert.AreEqual(updateStub, removedBehaviour);

            Assert.IsTrue(entity.DelBehaviour<EntitySpawnedStub>());
            Assert.IsFalse(entity.HasBehaviour(initStub));

            Assert.IsFalse(entity.DelBehaviour(behaviourStub));
            Assert.IsFalse(entity.DelBehaviour<EntitySpawnedStub>());
        }

        [Test]
        public void DelBehaviour_BeforeSpawn_NotDisabledAndDespawned()
        {
            //Arrange:
            var behaviourStub = new DummyEntityBehaviour();
            var entity = new Entity();
            entity.AddBehaviour(behaviourStub);

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.Deactivated);
            Assert.IsFalse(behaviourStub.Despawned);
        }

        [Test]
        public void DelBehaviour_AfterEnable_BehaviourDisabledAndDespawned()
        {
            //Arrange:
            var behaviourStub = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviour(behaviourStub);
            entity.Spawn();
            entity.Activate();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.Deactivated);
            Assert.IsTrue(behaviourStub.Despawned);
            Assert.AreEqual(nameof(DummyEntityBehaviour.OnInactive), behaviourStub.InvocationList[^2]);
            Assert.AreEqual(nameof(DummyEntityBehaviour.OnDespawn), behaviourStub.InvocationList[^1]);
        }

        [Test]
        public void DelBehaviour_RemovesFirstMatch_ReturnsTrue()
        {
            var behaviour1 = new DummyEntityBehaviour();
            var behaviour2 = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {behaviour1, behaviour2});

            var result = entity.DelBehaviour<DummyEntityBehaviour>();

            Assert.IsTrue(result);
            Assert.AreEqual(1, entity.BehaviourCount);
            Assert.IsFalse(entity.HasBehaviour(behaviour1)); // первый удалён
            Assert.IsTrue(entity.HasBehaviour(behaviour2));
        }

        [Test]
        public void DelBehaviour_ReturnsFalse_WhenNoMatch()
        {
            var entity = new Entity(); // пусто

            var result = entity.DelBehaviour<DummyEntityBehaviour>();

            Assert.IsFalse(result);
            Assert.AreEqual(0, entity.BehaviourCount);
        }

        [Test]
        public void DelBehaviour_OnlyRemovesFirstMatchingInstance()
        {
            var a = new DummyEntityBehaviour();
            var b = new DummyEntityBehaviour();
            var c = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b, c});

            entity.DelBehaviour<DummyEntityBehaviour>();

            Assert.AreEqual(2, entity.BehaviourCount);
            Assert.IsFalse(entity.HasBehaviour(a));
            Assert.IsTrue(entity.HasBehaviour(b));
            Assert.IsTrue(entity.HasBehaviour(c));
        }

        #endregion

        #region ClearBehaviours

        [Test]
        public void ClearBehaviours_BehaviourCountIsZero()
        {
            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            //Act:
            entity.ClearBehaviours();

            //Assert:
            Assert.AreEqual(0, entity.BehaviourCount);
        }

        [Test]
        public void ClearBehaviours_OnBehaviourDeleted_IsRaisedForEach()
        {
            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();

            var entity = new Entity();
            var deleted = new List<IEntityBehaviour>();

            entity.OnBehaviourDeleted += (e, b) => deleted.Add(b);

            entity.AddBehaviours(new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            // Act:
            entity.ClearBehaviours();

            // Assert:
            CollectionAssert.AreEquivalent(new IEntityBehaviour[] {updateStub, initStub}, deleted);
        }

        [Test]
        public void ClearBehaviours_OnStateChanged_IsRaised()
        {
            var stub = new EntityUpdateStub();
            var stateChanged = false;

            var entity = new Entity();
            entity.OnStateChanged += () => stateChanged = true;

            entity.AddBehaviour(stub);

            // Act:
            entity.ClearBehaviours();

            // Assert:
            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void ClearBehaviours_DoesNothing_WhenNoBehaviours()
        {
            var entity = new Entity();
            var stateChanged = false;
            var behaviourDeletedCalled = false;

            entity.OnStateChanged += () => stateChanged = true;
            entity.OnBehaviourDeleted += (_, _) => behaviourDeletedCalled = true;

            // Act:
            entity.ClearBehaviours();

            // Assert:
            Assert.AreEqual(0, entity.BehaviourCount);
            Assert.IsFalse(stateChanged);
            Assert.IsFalse(behaviourDeletedCalled);
        }

        #endregion

        #region GetBehaviourEnumerator

        [Test]
        public void GetBehaviourEnumerator_ManualMoveNext_AndCurrent_Works()
        {
            var a = new DummyEntityBehaviour();
            var b = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b});

            var enumerator = entity.GetBehaviourEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreSame(a, enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreSame(b, enumerator.Current);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void GetBehaviourEnumerator_Reset_ResetsState()
        {
            var a = new DummyEntityBehaviour();
            var b = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b});

            var enumerator = entity.GetBehaviourEnumerator();

            enumerator.MoveNext(); // a
            enumerator.MoveNext(); // b

            enumerator.Reset();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreSame(a, enumerator.Current);
        }

        [Test]
        public void GetBehaviourEnumerator_YieldsSameValues()
        {
            var a = new DummyEntityBehaviour();
            var b = new DummyEntityBehaviour();

            IEntity entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b});

            var list = new List<IEntityBehaviour>();
            var enumerator = entity.GetBehaviourEnumerator();
            while (enumerator.MoveNext())
                list.Add(enumerator.Current);

            CollectionAssert.AreEqual(new[] {a, b}, list);
        }

        #endregion

        #region CopyBehaviours

        [Test]
        public void CopyBehaviours_CopiesAllBehavioursInOrder()
        {
            var a = new DummyEntityBehaviour();
            var b = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b});

            var target = new IEntityBehaviour[2];

            int count = entity.CopyBehaviours(target);

            Assert.AreEqual(2, count);
            Assert.AreSame(a, target[0]);
            Assert.AreSame(b, target[1]);
        }

        [Test]
        public void CopyBehaviours_ReturnsZero_WhenNoBehaviours()
        {
            var entity = new Entity();
            var target = Array.Empty<IEntityBehaviour>();

            int count = entity.CopyBehaviours(target);

            Assert.AreEqual(0, count);
        }

        [Test]
        public void CopyBehaviours_ThrowsArgumentNullException_WhenArrayIsNull()
        {
            var entity = new Entity();
            entity.AddBehaviour(new DummyEntityBehaviour());

            Assert.Throws<ArgumentNullException>(() => { entity.CopyBehaviours(null); });
        }

        [Test]
        public void CopyBehaviours_ThrowsArgumentException_WhenArrayTooSmall()
        {
            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[]
            {
                new DummyEntityBehaviour(),
                new DummyEntityBehaviour()
            });

            var tooSmall = new IEntityBehaviour[1]; // < 2

            Assert.Throws<ArgumentException>(() => { entity.CopyBehaviours(tooSmall); });
        }

        #endregion

        #region GetBehaviourAt

        [Test]
        public void GetBehaviourAt_ReturnsCorrectBehaviour()
        {
            var behaviour1 = new DummyEntityBehaviour();
            var behaviour2 = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {behaviour1, behaviour2});

            var result0 = entity.GetBehaviourAt(0);
            var result1 = entity.GetBehaviourAt(1);

            Assert.AreSame(behaviour1, result0);
            Assert.AreSame(behaviour2, result1);
        }

        [Test]
        public void GetBehaviourAt_Throws_WhenIndexIsNegative()
        {
            var entity = new Entity();
            entity.AddBehaviour(new DummyEntityBehaviour());

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                int index = -1;
                entity.GetBehaviourAt(index);
            });
        }

        [Test]
        public void GetBehaviourAt_Throws_WhenIndexIsTooLarge()
        {
            var entity = new Entity();
            entity.AddBehaviour(new DummyEntityBehaviour());

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                int index = 1;
                entity.GetBehaviourAt(index);
            });
        }

        [Test]
        public void GetBehaviourAt_Throws_WhenEmpty()
        {
            var entity = new Entity();

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                int index = 0;
                entity.GetBehaviourAt(index);
            });
        }

        #endregion

        #region TryGetBehaviour

        [Test]
        public void TryGetBehaviour_ReturnsTrue_WhenBehaviourExists()
        {
            var behaviour = new DummyEntityBehaviour();
            var entity = new Entity();
            entity.AddBehaviour(behaviour);

            var result = entity.TryGetBehaviour<DummyEntityBehaviour>(out var found);

            Assert.IsTrue(result);
            Assert.AreSame(behaviour, found);
        }

        [Test]
        public void TryGetBehaviour_ReturnsFalse_WhenNoBehaviours()
        {
            var entity = new Entity();

            var result = entity.TryGetBehaviour<DummyEntityBehaviour>(out var found);

            Assert.IsFalse(result);
            Assert.IsNull(found);
        }

        [Test]
        public void TryGetBehaviour_ReturnsFirstMatch_WhenMultipleExist()
        {
            var first = new DummyEntityBehaviour();
            var second = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {first, second});

            var result = entity.TryGetBehaviour<DummyEntityBehaviour>(out var found);

            Assert.IsTrue(result);
            Assert.AreSame(first, found);
        }

        #endregion

        #region GetBehaviour

        [Test]
        public void GetBehaviour_ReturnsInstance_WhenExists()
        {
            var behaviour = new DummyEntityBehaviour();
            var entity = new Entity();
            entity.AddBehaviour(behaviour);

            var result = entity.GetBehaviour<DummyEntityBehaviour>();

            Assert.AreSame(behaviour, result);
        }

        [Test]
        public void GetBehaviour_ReturnsFirstMatch_WhenMultipleExist()
        {
            var first = new DummyEntityBehaviour();
            var second = new DummyEntityBehaviour();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {first, second});

            var result = entity.GetBehaviour<DummyEntityBehaviour>();

            Assert.AreSame(first, result);
        }

        [Test]
        public void GetBehaviour_ThrowsException_WhenNotFound()
        {
            var entity = new Entity();

            var ex = Assert.Throws<Exception>(() => { entity.GetBehaviour<DummyEntityBehaviour>(); });

            Assert.That(ex.Message, Does.Contain("EntityBehaviourStub"));
        }

        #endregion
    }
}