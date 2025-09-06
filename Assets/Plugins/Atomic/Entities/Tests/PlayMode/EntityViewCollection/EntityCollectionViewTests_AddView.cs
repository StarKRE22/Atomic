using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void AddView_CreatesView_AndRaisesOnAdded()
        {
            var entity = new Entity("Player");

            IEntity addedEntity = null;
            EntityView addedView = null;
            _collection.OnAdded += (e, v) =>
            {
                addedEntity = e;
                addedView = v;
            };

            _collection.Add(entity);

            Assert.AreEqual(entity, addedEntity, "OnAdded должно быть вызвано для добавленной сущности");
            Assert.NotNull(addedView, "Созданная вьюшка не должна быть null");
            Assert.AreEqual(_collection.viewport, addedView.transform.parent, "Вьюшка должна быть привязана к viewport");
            Assert.AreEqual(entity, addedView.Entity, "Вьюшка должна быть связана с правильной сущностью");
        }

        [Test]
        public void AddView_DoesNothing_IfEntityAlreadyExists()
        {
            var entity = new Entity("Enemy");
            int callCount = 0;

            _collection.OnAdded += (_, _) => callCount++;

            _collection.Add(entity);
            _collection.Add(entity); // повторное добавление той же сущности

            Assert.AreEqual(1, callCount, "OnAdded должно быть вызвано только один раз");
            Assert.AreEqual(1, _collection.Count, "Count не должен увеличиваться при повторном добавлении");
        }
    }
}