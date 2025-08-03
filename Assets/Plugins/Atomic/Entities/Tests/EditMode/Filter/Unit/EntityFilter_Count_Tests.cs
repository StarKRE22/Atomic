using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityFilter_Tests
    {
        [Test]
        public void Count_Should_BeZero_WhenFilterIsEmpty()
        {
            // Arrange
            var source = new EntityCollection();
            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));

            // Act & Assert
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Count_Should_ReflectEntitiesMatchingPredicate()
        {
            // Arrange
            var source = new EntityCollection();
            var entity1 = new Entity();
            var entity2 = new Entity();
            entity1.AddTag("Enemy");

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));

            // Act
            source.Add(entity1); // подходит
            source.Add(entity2); // не подходит

            // Assert
            Assert.AreEqual(1, filter.Count);
        }

        [Test]
        public void Count_Should_Decrease_WhenEntityRemoved()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity();
            entity.AddTag("Enemy");

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));

            source.Add(entity);
            Assert.AreEqual(1, filter.Count);

            // Act
            source.Remove(entity);

            // Assert
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Count_Should_Update_WhenPredicateChanges_ThroughSynchronize()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity();
            entity.AddTag("Enemy");

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));
            source.Add(entity);
            Assert.AreEqual(1, filter.Count);

            // Act — состояние меняется, больше не соответствует
            entity.DelTag("Enemy");
            filter.Synchronize(entity);

            // Assert
            Assert.AreEqual(0, filter.Count);
        }
    }
}