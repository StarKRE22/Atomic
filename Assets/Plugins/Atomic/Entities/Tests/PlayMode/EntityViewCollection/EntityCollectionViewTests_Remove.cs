using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void RemoveView_HidesView_AndRaisesOnRemoved()
        {
            var entity = new Entity("Player");

            _collection.Add(entity);

            IEntity removedEntity = null;
            EntityView removedView = null;
            _collection.OnRemoved += (e, v) =>
            {
                removedEntity = e;
                removedView = v;
            };

            _collection.Remove(entity);

            Assert.AreEqual(entity, removedEntity, "OnRemoved должно быть вызвано для удаленной сущности");
            Assert.NotNull(removedView, "View для удаленной сущности не должно быть null");
            Assert.IsFalse(removedView.gameObject.activeSelf, "View должно быть скрыто (Hide)");
        }

        [Test]
        public void RemoveView_DoesNothing_IfEntityNotInCollection()
        {
            var entity = new Entity("Enemy");
            int callCount = 0;

            _collection.OnRemoved += (_, _) => callCount++;

            _collection.Remove(entity);

            Assert.AreEqual(0, callCount, "OnRemoved не должно вызываться для несуществующей сущности");
        }

        [Test]
        public void RemoveView_CalledOnce_WhenEntityRemovedTwice()
        {
            var entity = new Entity("Enemy");
            int callCount = 0;

            _collection.Add(entity);
            _collection.OnRemoved += (_, _) => callCount++;

            _collection.Remove(entity);
            _collection.Remove(entity); // повторное удаление

            Assert.AreEqual(1, callCount, "OnRemoved должно быть вызвано только один раз");
        }
    }
}