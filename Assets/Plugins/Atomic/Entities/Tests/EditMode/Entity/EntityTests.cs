using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed partial class EntityTests
    {
        [Test]
        public void GetHashCode_Equals_InstanceId()
        {
            var entity1 = new Entity("1");
            Assert.AreEqual(entity1.InstanceID, entity1.GetHashCode());
        }

        [Test]
        public void UnsubscribeAll_PreventsEventsFromFiring()
        {
            // Arrange
            var entity = new Entity("Test");

            bool tagAddedCalled = false;
            bool valueAddedCalled = false;
            bool behaviourAddedCalled = false;

            entity.OnTagAdded += (_, _) => tagAddedCalled = true;
            entity.OnValueAdded += (_, _) => valueAddedCalled = true;
            entity.OnBehaviourAdded += (_, _) => behaviourAddedCalled = true;

            entity.UnsubscribeAll();

            // Act
            entity.AddTag(42);
            entity.AddValue(1, "value");
            entity.AddBehaviour(new EntityBehaviourStub());

            // Assert
            Assert.IsFalse(tagAddedCalled, "OnTagAdded should not be called after UnsubscribeAll()");
            Assert.IsFalse(valueAddedCalled, "OnValueAdded should not be called after UnsubscribeAll()");
            Assert.IsFalse(behaviourAddedCalled, "OnBehaviourAdded should not be called after UnsubscribeAll()");
        }
    }
}