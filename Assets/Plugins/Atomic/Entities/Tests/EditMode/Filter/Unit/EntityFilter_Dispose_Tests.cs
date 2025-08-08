using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityFilter_Tests
    {
        [Test]
        public void Dispose_Should_Unobserve_AllEntities()
        {
            // Arrange
            var source = new EntityCollection();
            var entity1 = new Entity();
            var entity2 = new Entity();
            entity1.AddTag("Enemy");
            entity2.AddTag("Enemy");

            source.Add(entity1);
            source.Add(entity2);

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));
            Assert.AreEqual(2, filter.Count);

            // Act
            filter.Dispose();

            // Assert
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Dispose_Should_Unsubscribe_FromSourceEvents()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity();
            entity.AddTag("Enemy");

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));
            filter.Dispose();

            bool wasCalled = false;
            filter.OnAdded += _ => wasCalled = true;

            // Act — добавление после Dispose
            source.Add(entity);

            // Assert — ничего не происходит
            Assert.IsFalse(wasCalled);
            Assert.AreEqual(0, filter.Count);
        }

        [Test]
        public void Dispose_CalledTwice_Should_NotThrow()
        {
            // Arrange
            var source = new EntityCollection();
            var filter = new EntityFilter(source, e => true);

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                filter.Dispose();
                filter.Dispose(); // повторный вызов
            });
        }

        [Test]
        public void Dispose_Should_CallUntrack_OnTriggers()
        {
            // Arrange
            var entity = new Entity();
            entity.AddTag("Enemy");

            var source = new EntityCollection();
            source.Add(entity);

            var trigger = new EntityTriggerStub();
            var filter = new EntityFilter(source, e => e.HasTag("Enemy"), trigger);

            // Pre-check: tracked
            Assert.AreEqual(1, trigger.Tracked.Count);

            // Act
            filter.Dispose();

            // Assert
            Assert.AreEqual(0, trigger.Tracked.Count);
        }
    }
}