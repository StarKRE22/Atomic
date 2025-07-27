using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void GetAllBehaviours_ReturnsAllAttachedBehaviours()
        {
            // Arrange
            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();
            var behaviourStub = new EntityBehaviourStub();

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
        public void HasBehaviour()
        {
            //Arrange:
            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();
            var behaviourStub = new EntityBehaviourStub();

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
            entity.AddBehaviour<EntityBehaviourStub>();

            // Assert
            Assert.IsNotNull(addedBehaviour);
            Assert.IsInstanceOf<EntityBehaviourStub>(addedBehaviour);
            Assert.IsTrue(entity.HasBehaviour<EntityBehaviourStub>());
        }


        [Test]
        public void DelBehaviour()
        {
            //Arrange:
            IEntityBehaviour removedBehaviour = null;

            var updateStub = new EntityUpdateStub();
            var initStub = new EntitySpawnedStub();
            var behaviourStub = new EntityBehaviourStub();

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
        public void AddBehaviour_AfterSpawn_BehaviourSpawned()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity();
            entity.Spawn();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.spawned);
            Assert.IsFalse(behaviourStub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Spawn), behaviourStub.invokationList[0]);
        }

        [Test]
        public void AddBehaviour_AfterEnable_BehaviourSpawnedAndEnabled()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity();
            entity.Spawn();
            entity.Enable();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.spawned);
            Assert.IsTrue(behaviourStub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Spawn), behaviourStub.invokationList[0]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Enable), behaviourStub.invokationList[1]);
        }

        [Test]
        public void DelBehaviour_BeforeSpawn_NotDisabledAndDespawned()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();
            var entity = new Entity();
            entity.AddBehaviour(behaviourStub);

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.disabled);
            Assert.IsFalse(behaviourStub.despawned);
        }

        [Test]
        public void DelBehaviour_AfterEnable_BehaviourDisabledAndDespawned()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity();
            entity.AddBehaviour(behaviourStub);
            entity.Spawn();
            entity.Enable();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.disabled);
            Assert.IsTrue(behaviourStub.despawned);
            Assert.AreEqual(nameof(EntityBehaviourStub.Disable), behaviourStub.invokationList[^2]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Despawn), behaviourStub.invokationList[^1]);
        }

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

        [Test]
        public void GetBehaviourEnumerator_ManualMoveNext_AndCurrent_Works()
        {
            var a = new EntityBehaviourStub();
            var b = new EntityBehaviourStub();
            
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
        public void Enumerator_Reset_ResetsState()
        {
            var a = new EntityBehaviourStub();
            var b = new EntityBehaviourStub();

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
            var a = new EntityBehaviourStub();
            var b = new EntityBehaviourStub();

            IEntity entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] {a, b});

            var list = new List<IEntityBehaviour>();
            var enumerator = entity.GetBehaviourEnumerator();
            while (enumerator.MoveNext()) 
                list.Add(enumerator.Current);

            CollectionAssert.AreEqual(new[] {a, b}, list);
        }
        
        [Test]
        public void CopyBehaviours_CopiesAllBehavioursInOrder()
        {
            var a = new EntityBehaviourStub();
            var b = new EntityBehaviourStub();

            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[] { a, b });

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
            entity.AddBehaviour(new EntityBehaviourStub());

            Assert.Throws<ArgumentNullException>(() =>
            {
                entity.CopyBehaviours(null);
            });
        }

        [Test]
        public void CopyBehaviours_ThrowsArgumentException_WhenArrayTooSmall()
        {
            var entity = new Entity();
            entity.AddBehaviours(new IEntityBehaviour[]
            {
                new EntityBehaviourStub(),
                new EntityBehaviourStub()
            });

            var tooSmall = new IEntityBehaviour[1]; // < 2

            Assert.Throws<ArgumentException>(() =>
            {
                entity.CopyBehaviours(tooSmall);
            });
        }
    }
}