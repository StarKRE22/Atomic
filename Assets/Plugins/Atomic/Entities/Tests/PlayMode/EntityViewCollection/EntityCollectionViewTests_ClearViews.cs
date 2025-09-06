using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void ClearViews_DoesNothing_WhenCollectionIsEmpty()
        {
            Assert.AreEqual(0, _collection.Count, "Count должен быть 0 перед очисткой");
            
            Assert.DoesNotThrow(() => _collection.Clear(), "ClearViews не должно падать на пустой коллекции");
            
            Assert.AreEqual(0, _collection.Count, "Count должен остаться 0 после ClearViews");
        }

        [Test]
        public void ClearViews_RemovesAllViews_AndRaisesOnRemoved()
        {
            var entityA = new Entity("Player");
            var entityB = new Entity("Enemy");

            _collection.Add(entityA);
            _collection.Add(entityB);

            int removedCount = 0;
            _collection.OnRemoved += (_, _) => removedCount++;

            _collection.Clear();

            Assert.AreEqual(0, _collection.Count, "После ClearViews коллекция должна быть пустой");
            Assert.AreEqual(2, removedCount, "OnRemoved должно быть вызвано для каждой сущности");
        }

        [Test]
        public void ClearViews_AllowsNewViewsAfterwards()
        {
            var entity = new Entity("Player");
            _collection.Add(entity);

            _collection.Clear();

            var newEntity = new Entity("Enemy");
            _collection.Add(newEntity);

            Assert.AreEqual(1, _collection.Count, "После ClearViews можно добавлять новые сущности");
            Assert.NotNull(_collection.Get(newEntity), "Новая сущность должна корректно добавляться");
        }
    }
}