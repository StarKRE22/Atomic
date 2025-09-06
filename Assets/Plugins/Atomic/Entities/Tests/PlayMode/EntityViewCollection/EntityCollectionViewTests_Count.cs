using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void Count_Increases_WhenEntityAdded()
        {
            var entity = new Entity("Player");

            Assert.AreEqual(0, _collection.Count, "Изначально коллекция должна быть пустой");

            _collection.Add(entity);

            Assert.AreEqual(1, _collection.Count, "После добавления одной сущности Count должен быть равен 1");
        }

        [Test]
        public void Count_Decreases_WhenEntityRemoved()
        {
            var entityA = new Entity("Player");
            var entityB = new Entity("Enemy");

            _collection.Add(entityA);
            _collection.Add(entityB);

            Assert.AreEqual(2, _collection.Count, "После добавления двух сущностей Count должен быть равен 2");

            _collection.Remove(entityA);

            Assert.AreEqual(1, _collection.Count, "После удаления одной сущности Count должен уменьшиться до 1");
        }

        [Test]
        public void Count_ReturnsZero_WhenCleared()
        {
            var entityA = new Entity("Player");
            var entityB = new Entity("Enemy");

            _collection.Add(entityA);
            _collection.Add(entityB);

            Assert.AreEqual(2, _collection.Count, "Count должен быть равен 2 перед очисткой");

            _collection.Clear();

            Assert.AreEqual(0, _collection.Count, "После ClearViews Count должен быть равен 0");
        }
    }
}