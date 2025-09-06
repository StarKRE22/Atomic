using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void OnRemoved_IsRaised_WhenEntityRemoved()
        {
            var entity = new Entity("Player");
            IEntity removedEntity = null;
            EntityView removedView = null;

            _collection.Add(entity);
            _collection.OnRemoved += (e, v) =>
            {
                removedEntity = e;
                removedView = v;
            };

            _collection.Remove(entity);

            Assert.AreEqual(entity, removedEntity);
            Assert.NotNull(removedView);
            Assert.AreEqual("Player(Clone)", removedView.name);
        }

        [Test]
        public void OnRemoved_NotRaised_WhenEntityNotInCollection()
        {
            var entity = new Entity("Enemy");
            int callCount = 0;

            _collection.OnRemoved += (_, _) => callCount++;

            _collection.Remove(entity);

            Assert.AreEqual(0, callCount, "OnRemoved должно вызываться только при реальном удалении");
        }

        [Test]
        public void OnRemoved_CalledOnce_WhenEntityRemovedTwice()
        {
            var entity = new Entity("Enemy");
            int callCount = 0;

            _collection.Add(entity);
            _collection.OnRemoved += (_, _) => callCount++;

            _collection.Remove(entity);
            _collection.Remove(entity); // повторное удаление

            Assert.AreEqual(1, callCount, "OnRemoved должно вызваться только один раз");
        }
    }
}