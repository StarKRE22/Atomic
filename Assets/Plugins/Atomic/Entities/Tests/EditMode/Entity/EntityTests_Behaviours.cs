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
            var initStub = new EntityInitStub();
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
            var initStub = new EntityInitStub();
            var behaviourStub = new EntityBehaviourStub();
        
            var entity = new Entity(null, null, null, new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });
        
            //Assert & Act:
            Assert.IsTrue(entity.HasBehaviour(updateStub));
            Assert.IsTrue(entity.HasBehaviour<EntityInitStub>());
            Assert.IsFalse(entity.HasBehaviour(behaviourStub));
        }
   
        [Test]
        public void AddBehaviour_WhenAlreadyPresent_DoesNotRaiseEvent()
        {
            // Arrange
            var updateStub = new EntityUpdateStub();
            IEntityBehaviour addedBehaviour = null;

            var entity = new Entity(null, null, null, new IEntityBehaviour[] { updateStub });
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
            var initStub = new EntityInitStub();
            IEntityBehaviour addedBehaviour = null;

            var entity = new Entity(null, null, null, new IEntityBehaviour[] { updateStub });
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
            var initStub = new EntityInitStub();
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
        
            Assert.IsTrue(entity.DelBehaviour<EntityInitStub>());
            Assert.IsFalse(entity.HasBehaviour(initStub));
        
            Assert.IsFalse(entity.DelBehaviour(behaviourStub));
            Assert.IsFalse(entity.DelBehaviour<EntityInitStub>());
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
        
            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsFalse(behaviourStub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Spawn), behaviourStub.invokationList[0]);
        }
        //
        // [Test]
        // public void WhenAddBehaviourAfterEnableThenBehaviourWillEnable()
        // {
        //     //Arrange:
        //     var behaviourStub = new BehaviourStub();
        //
        //     var entity = new Entity();
        //     entity.Init();
        //     entity.Enable();
        //
        //     //Act
        //     entity.AddBehaviour(behaviourStub);
        //
        //     Assert.IsTrue(behaviourStub.initialized);
        //     Assert.IsTrue(behaviourStub.enabled);
        //     Assert.AreEqual(nameof(BehaviourStub.Init), behaviourStub.invokationList[0]);
        //     Assert.AreEqual(nameof(BehaviourStub.Enable), behaviourStub.invokationList[1]);
        // }
        //
        // [Test]
        // public void WhenDelBehaviourAfterInitThenBehaviourWiilDispose()
        // {
        //     //Arrange:
        //     var behaviourStub = new BehaviourStub();
        //
        //     var entity = new Entity(behaviours: new IEntityBehaviour[] {behaviourStub});
        //     entity.Init();
        //
        //     //Act
        //     entity.DelBehaviour(behaviourStub);
        //
        //     Assert.IsFalse(behaviourStub.disabled);
        //     Assert.IsTrue(behaviourStub.disposed);
        // }
        //
        // [Test]
        // public void WhenDelBehaviourAfterEnableThenBehaviourWiilDisableAndDispose()
        // {
        //     //Arrange:
        //     var behaviourStub = new BehaviourStub();
        //
        //     var entity = new Entity(behaviours: new IEntityBehaviour[] {behaviourStub});
        //     entity.Init();
        //     entity.Enable();
        //
        //     //Act
        //     entity.DelBehaviour(behaviourStub);
        //
        //     Assert.IsTrue(behaviourStub.disabled);
        //     Assert.IsTrue(behaviourStub.disposed);
        //     Assert.AreEqual(nameof(BehaviourStub.Disable), behaviourStub.invokationList[^2]);
        //     Assert.AreEqual(nameof(BehaviourStub.Dispose), behaviourStub.invokationList[^1]);
        // }
        //
        // [Test]
        // public void ClearBehaviours()
        // {
        //     var updateStub = new EntityUpdateStub();
        //     var initStub = new EntityInitStub();
        //
        //     var entity = new Entity(behaviours: new IEntityBehaviour[]
        //     {
        //         updateStub,
        //         initStub
        //     });
        //
        //     // entity.OnBehavioursCleared += _ => wasClear = true;
        //
        //     //Act:
        //     entity.ClearBehaviours();
        //
        //     //Assert:
        //     Assert.AreEqual(0, entity.BehaviourCount);
        // }

        //TODO:
        // [Test]
        // public void WhenClearEmptyBehavioursThenFalse()
        // {
        //     //Arrange:
        //     // var wasClear = false;
        //     var entity = new Entity();
        //
        //     // entity.OnBehavioursCleared += _ => wasClear = true;
        //
        //     //Act:
        //     bool success = entity.ClearBehaviours();
        //
        //     Assert.IsFalse(success);
        //     // Assert.IsFalse(wasClear);
        // }
    }
}