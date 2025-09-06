using NUnit.Framework;
using System.Linq;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void GetEnumerator_ReturnsAllEntityViewPairs()
        {
            var entityA = new Entity("Player");
            var entityB = new Entity("Enemy");

            _collection.Add(entityA);
            _collection.Add(entityB);

            var pairs = _collection.ToList();

            Assert.AreEqual(2, pairs.Count, "Итератор должен вернуть все добавленные пары");
            Assert.IsTrue(pairs.Any(p => p.Key == entityA && p.Value.Entity == entityA), "Пара с entityA должна присутствовать");
            Assert.IsTrue(pairs.Any(p => p.Key == entityB && p.Value.Entity == entityB), "Пара с entityB должна присутствовать");
        }

        [Test]
        public void GetEnumerator_ReflectsRemovalOfEntities()
        {
            var entityA = new Entity("Player");
            var entityB = new Entity("Enemy");

            _collection.Add(entityA);
            _collection.Add(entityB);

            _collection.Remove(entityA);

            var pairs = _collection.ToList();

            Assert.AreEqual(1, pairs.Count, "Итератор должен вернуть только оставшиеся пары");
            Assert.AreEqual(entityB, pairs[0].Key, "Оставшаяся пара должна быть entityB");
        }

        [Test]
        public void Foreach_WorksCorrectly()
        {
            var entity = new Entity("Player");
            _collection.Add(entity);

            int count = 0;
            foreach (var pair in _collection)
            {
                Assert.AreEqual(entity, pair.Key);
                Assert.AreEqual(entity, pair.Value.Entity);
                count++;
            }

            Assert.AreEqual(1, count, "Foreach должен пройтись по одной добавленной паре");
        }
    }
}