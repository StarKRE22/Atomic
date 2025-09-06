using NUnit.Framework;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        [Test]
        public void GetView_ReturnsView_ForExistingEntity()
        {
            var entity = new Entity("Player");

            _collection.Add(entity);

            var view = _collection.Get(entity);

            Assert.NotNull(view, "GetView должен вернуть вьюшку для существующей сущности");
            Assert.AreEqual(entity, view.Entity, "Вьюшка должна быть связана с правильной сущностью");
        }

        [Test]
        public void GetView_ThrowsKeyNotFoundException_ForNonExistingEntity()
        {
            var entity = new Entity("Enemy");

            Assert.Throws<KeyNotFoundException>(
                () => _collection.Get(entity),
                "Если сущности нет в коллекции, должно выбрасываться KeyNotFoundException"
            );
        }
    }
}