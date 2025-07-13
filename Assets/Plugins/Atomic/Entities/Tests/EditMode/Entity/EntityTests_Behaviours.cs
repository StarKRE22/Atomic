using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void GetAllBehaviours()
        {
            //Arrange:
            var updateStub = new UpdateStub();
            var initStub = new InitStub();
            var behaviourStub = new BehaviourStub();

            var entity = new Entity(behaviours: new IBehaviour[]
            {
                updateStub,
                initStub,
                behaviourStub
            });

            //Act:
            IReadOnlyCollection<IBehaviour> behaviours = entity.GetBehaviours();

            //Assert:
            Assert.AreEqual(new HashSet<IBehaviour>
            {
                updateStub,
                initStub,
                behaviourStub
            }, behaviours);
        }

        [Test]
        public void HasBehaviour()
        {
            //Arrange:
            var updateStub = new UpdateStub();
            var initStub = new InitStub();
            var behaviourStub = new BehaviourStub();

            var entity = new Entity(behaviours: new IBehaviour[]
            {
                updateStub,
                initStub
            });

            //Assert & Act:
            Assert.IsTrue(entity.HasBehaviour(updateStub));
            Assert.IsTrue(entity.HasBehaviour<InitStub>());
            Assert.IsFalse(entity.HasBehaviour(behaviourStub));
        }

        //TODO:
        [Test]
        public void AddBehaviour()
        {
            //Arrange:
            IBehaviour addedBehaviour = null;

            var updateStub = new UpdateStub();
            var initStub = new InitStub();

            var entity = new Entity(behaviours: new IBehaviour[]
            {
                updateStub
            });

            entity.OnBehaviourAdded += (_, b) => addedBehaviour = b;

            //Assert & Act:
            // Assert.IsFalse(entity.AddBehaviour(updateStub));
            // Assert.IsNull(addedBehaviour);

            // Assert.IsTrue(entity.AddBehaviour(initStub));
            // Assert.AreEqual(initStub, addedBehaviour);

            // entity.AddBehaviour<EntityBehaviourStub>();
            // Assert.IsNotNull(addedBehaviour);
        }

        [Test]
        public void DelBehaviour()
        {
            //Arrange:
            IBehaviour removedBehaviour = null;

            var updateStub = new UpdateStub();
            var initStub = new InitStub();
            var behaviourStub = new BehaviourStub();

            var entity = new Entity(behaviours: new IBehaviour[]
            {
                updateStub,
                initStub
            });

            entity.OnBehaviourDeleted += (_, b) => removedBehaviour = b;

            //Assert & Act:
            Assert.IsTrue(entity.DelBehaviour(updateStub));
            Assert.AreEqual(updateStub, removedBehaviour);

            Assert.IsTrue(entity.DelBehaviour<InitStub>());
            Assert.IsFalse(entity.HasBehaviour(initStub));

            Assert.IsFalse(entity.DelBehaviour(behaviourStub));
            Assert.IsFalse(entity.DelBehaviour<InitStub>());
        }

        [Test]
        public void WhenAddBehaviourAfterInitThenBehaviourWiilInit()
        {
            //Arrange:
            var behaviourStub = new BehaviourStub();

            var entity = new Entity();
            entity.Init();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsFalse(behaviourStub.enabled);
            Assert.AreEqual(nameof(BehaviourStub.Init), behaviourStub.invokationList[0]);
        }

        [Test]
        public void WhenAddBehaviourAfterEnableThenBehaviourWillEnable()
        {
            //Arrange:
            var behaviourStub = new BehaviourStub();

            var entity = new Entity();
            entity.Init();
            entity.Enable();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
            Assert.AreEqual(nameof(BehaviourStub.Init), behaviourStub.invokationList[0]);
            Assert.AreEqual(nameof(BehaviourStub.Enable), behaviourStub.invokationList[1]);
        }

        [Test]
        public void WhenDelBehaviourAfterInitThenBehaviourWiilDispose()
        {
            //Arrange:
            var behaviourStub = new BehaviourStub();

            var entity = new Entity(behaviours: new IBehaviour[] {behaviourStub});
            entity.Init();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.disabled);
            Assert.IsTrue(behaviourStub.disposed);
        }

        [Test]
        public void WhenDelBehaviourAfterEnableThenBehaviourWiilDisableAndDispose()
        {
            //Arrange:
            var behaviourStub = new BehaviourStub();

            var entity = new Entity(behaviours: new IBehaviour[] {behaviourStub});
            entity.Init();
            entity.Enable();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.disabled);
            Assert.IsTrue(behaviourStub.disposed);
            Assert.AreEqual(nameof(BehaviourStub.Disable), behaviourStub.invokationList[^2]);
            Assert.AreEqual(nameof(BehaviourStub.Dispose), behaviourStub.invokationList[^1]);
        }

        [Test]
        public void ClearBehaviours()
        {
            var updateStub = new UpdateStub();
            var initStub = new InitStub();

            var entity = new Entity(behaviours: new IBehaviour[]
            {
                updateStub,
                initStub
            });

            // entity.OnBehavioursCleared += _ => wasClear = true;

            //Act:
            entity.ClearBehaviours();

            //Assert:
            Assert.AreEqual(0, entity.BehaviourCount);
        }

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