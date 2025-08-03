using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityFilter_Tests
    {
        [Test]
        public void OnRemoved_Should_Fire_WhenEntityRemovedFromSource()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity();
            entity.AddTag("Enemy");

            IEntity removed = null;
            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));

            source.Add(entity); // Сначала добавим, чтобы он попал в фильтр
            filter.OnRemoved += e => removed = e;

            // Act
            source.Remove(entity);

            // Assert
            Assert.AreSame(entity, removed);
        }

        [Test]
        public void OnRemoved_Should_Fire_WhenPredicateBecomesFalse_AfterSynchronize()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity();
            entity.AddTag("Enemy");

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));

            source.Add(entity); // Сначала добавим
            IEntity removed = null;
            filter.OnRemoved += e => removed = e;

            // Состояние изменяется — больше не соответствует
            entity.DelTag("Enemy");

            // Act
            filter.Synchronize(entity);

            // Assert
            Assert.AreSame(entity, removed);
        }

        [Test]
        public void OnRemoved_Should_NotFire_WhenEntityWasNotInFilter()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity(); // нет тега "Enemy"

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));

            source.Add(entity); // не попадёт в фильтр
            bool wasCalled = false;
            filter.OnRemoved += _ => wasCalled = true;

            // Act
            source.Remove(entity);

            // Assert
            Assert.IsFalse(wasCalled);
        }

        [Test]
        public void OnRemoved_Should_NotFire_WhenPredicateStillTrue_AfterSynchronize()
        {
            // Arrange
            var source = new EntityCollection();
            var entity = new Entity();
            entity.AddTag("Enemy");

            var filter = new EntityFilter(source, e => e.HasTag("Enemy"));
            source.Add(entity);

            bool wasCalled = false;
            filter.OnRemoved += _ => wasCalled = true;

            // Act
            filter.Synchronize(entity); // без изменений

            // Assert
            Assert.IsFalse(wasCalled);
        }
    }
}