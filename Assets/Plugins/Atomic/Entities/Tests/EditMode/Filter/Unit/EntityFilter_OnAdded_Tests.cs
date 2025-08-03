using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityFilter_Tests
    {
        [Test]
        public void OnAdded_Should_Fire_WhenEntityMatchesPredicate_AndAdded()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity();
            entity.AddTag("Enemy");

            IEntity addedEntity = null;
            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));
            filter.OnAdded += e => addedEntity = e;

            // Act
            source.Add(entity);

            // Assert
            Assert.AreSame(entity, addedEntity);
        }

        [Test]
        public void OnAdded_Should_NotFire_WhenEntityDoesNotMatchPredicate()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity(); // No tag "Enemy"

            bool wasCalled = false;
            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));
            filter.OnAdded += _ => wasCalled = true;

            // Act
            source.Add(entity);

            // Assert
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnAdded_Should_Fire_WhenEntityMatchesPredicate_AfterSynchronize()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity(); // No tag yet

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));

            source.Add(entity); // Initially doesn't match
            IEntity added = null;
            filter.OnAdded += e => added = e;

            // Simulate state change to satisfy predicate
            entity.AddTag("Enemy");

            // Act
            filter.Synchronize(entity);

            // Assert
            Assert.AreSame(entity, added);
        }

        [Test]
        public void OnAdded_Should_NotFire_WhenEntityAlreadyPresent()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity();
            entity.AddTag("Enemy");

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));
            source.Add(entity); // First time added

            bool wasCalled = false;
            filter.OnAdded += _ => wasCalled = true;

            // Act
            filter.Synchronize(entity); // Should not re-add

            // Assert
            Assert.IsFalse(wasCalled);
        }
    }
}