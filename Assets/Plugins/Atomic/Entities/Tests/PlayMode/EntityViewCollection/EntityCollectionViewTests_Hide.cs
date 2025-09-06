using NUnit.Framework;
using System;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void Hide_DoesNothing_WhenNoSource()
        {
            Assert.DoesNotThrow(() => _collection.Hide(), "Hide не должен падать, если источник не задан");
            Assert.AreEqual(0, _collection.Count, "Count должен оставаться 0");
            Assert.IsFalse(_collection.IsVisible, "IsVisible должен быть false");
        }

        [Test]
        public void Hide_UnsubscribesFromSourceEvents_AndClearsViews()
        {
            var entityA = new Entity("Player");
            var entityB = new Entity("Enemy");
            var source = new EntityCollection(entityA, entityB);

            _collection.Show(source);

            Assert.AreEqual(2, _collection.Count, "Перед Hide коллекция должна содержать 2 вьюшки");

            _collection.Hide();

            // Count обнуляется
            Assert.AreEqual(0, _collection.Count, "После Hide все views должны быть удалены");

            // IsVisible должен стать false
            Assert.IsFalse(_collection.IsVisible, "После Hide коллекция не видима");

            // Проверяем, что новые сущности не добавляются автоматически
            var newEntity = new Entity("NPC");
            source.Add(newEntity);

            Assert.AreEqual(0, _collection.Count, "После Hide новые сущности в коллекцию не добавляются");
        }
    }
}