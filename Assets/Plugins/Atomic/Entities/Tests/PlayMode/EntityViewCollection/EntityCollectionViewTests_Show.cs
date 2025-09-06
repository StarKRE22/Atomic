using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void Show_Throws_WhenSourceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _collection.Show(null));
        }

        [Test]
        public void Show_AddsEntities_FromSource()
        {
            var entityA = new Entity("Player");
            var entityB = new Entity("Enemy");
            var source = new EntityCollection(entityA, entityB);

            _collection.Show(source);

            Assert.AreEqual(2, _collection.Count, "Все сущности из источника должны быть добавлены");
            Assert.NotNull(_collection.Get(entityA));
            Assert.NotNull(_collection.Get(entityB));
        }

        [Test]
        public void Show_SubscribesToSourceEvents()
        {
            var entity = new Entity("Player");
            var source = new EntityCollection(entity);

            _collection.Show(source);

            var newEntity = new Entity("Enemy");
            source.Add(newEntity); // должно подтянуть вьюшку автоматически

            Assert.AreEqual(2, _collection.Count, "После добавления в источник коллекция должна обновиться");
            Assert.NotNull(_collection.Get(newEntity));
        }

        [Test]
        public void Show_ReplacesPreviousSource()
        {
            var entityA = new Entity("Player");
            var source1 = new EntityCollection(entityA);

            var entityB = new Entity("Enemy");
            var source2 = new EntityCollection(entityB);

            _collection.Show(source1);
            _collection.Show(source2); // должно вызвать Hide() и очистить старые вьюшки

            Assert.AreEqual(1, _collection.Count, "После смены источника должны остаться только сущности из нового источника");
            Assert.IsTrue(_collection.IsVisible);
            Assert.NotNull(_collection.Get(entityB));
            Assert.Throws<KeyNotFoundException>(() => _collection.Get(entityA), "Старые сущности должны быть удалены");
        }
    }
}
