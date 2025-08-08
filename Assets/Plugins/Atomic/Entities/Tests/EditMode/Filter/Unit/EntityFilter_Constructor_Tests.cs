using System;
using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityFilter_Tests
    {
        [Test]
        public void Constructor_Should_Throw_WhenSourceIsNull()
        {
            // Arrange
            Predicate<IEntity> predicate = e => true;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new EntityFilter(null, predicate));
        }

        [Test]
        public void Constructor_Should_Throw_WhenPredicateIsNull()
        {
            // Arrange
            var source = new EntityCollection();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                _ = new EntityFilter(source, null));
        }

        [Test]
        public void Constructor_Should_CallSetAction_OnAllTriggers()
        {
            // Arrange
            var source = new EntityCollection();
            var trigger1 = new EntityTriggerStub();
            var trigger2 = new EntityTriggerStub();

            // Act
            var filter = new EntityFilter(source, e => true, trigger1, trigger2);

            // Assert
            Assert.IsTrue(trigger1.SetActionCalled);
            Assert.IsTrue(trigger2.SetActionCalled);
            Assert.IsNotNull(trigger1.Action);
            Assert.IsNotNull(trigger2.Action);
        }

        [Test]
        public void Constructor_Should_ContainsEntities()
        {
            // Arrange
            var entity = new EntityDummy();
            var source = new EntityCollection();
            source.Add(entity);

            var filter = new EntityFilter(source, e => true);

            // Act is constructor — Assert:
            Assert.IsTrue(filter.Contains(entity));
            Assert.AreEqual(1, filter.Count);
        }

        [Test]
        public void Constructor_Should_SubscribeToSourceEvents()
        {
            // Arrange
            var source = new EntityCollection();
            var filter = new EntityFilter(source, e => true);

            IEntity entity = new EntityDummy();
            IEntity observed = null;
            filter.OnAdded += e => observed = e;

            // Act
            source.Add(entity);

            // Assert
            Assert.AreSame(entity, observed);
            Assert.AreEqual(1, filter.Count);
        }
    }
}