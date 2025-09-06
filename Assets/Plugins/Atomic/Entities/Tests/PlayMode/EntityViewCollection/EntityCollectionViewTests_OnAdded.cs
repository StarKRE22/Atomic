using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public partial class EntityCollectionViewTests
    {
        
        [UnityTest]
        public IEnumerator OnAdded_IsRaised_WhenEntityAdded()
        {
            var entity = new Entity("Player");

            IEntity receivedEntity = null;
            EntityView receivedView = null;

            _collection.OnAdded += (e, v) =>
            {
                receivedEntity = e;
                receivedView = v;
            };

            _collection.Add(entity);
            yield return null;

            Assert.AreEqual(entity, receivedEntity);
            Assert.NotNull(receivedView);
            Assert.AreEqual("Player", receivedView.Entity.Name);
        }

        [UnityTest]
        public IEnumerator OnAdded_NotRaised_WhenEntityAlreadyExists()
        {
            var entity = new Entity("Enemy");
            int callCount = 0;

            _collection.OnAdded += (_, _) => callCount++;

            _collection.Add(entity);
            _collection.Add(entity); // второй раз та же сущность
            yield return null;

            Assert.AreEqual(1, callCount, "OnAdded должно быть вызвано только один раз");
        }

        [UnityTest]
        public IEnumerator OnAdded_IsRaised_ForEachEntity_WhenShowCalled()
        {
            var entityA = new Entity("Player");
            var entityB = new Entity("Enemy");
            var source = new EntityCollection(entityA, entityB);

            int callCount = 0;
            _collection.OnAdded += (_, _) => callCount++;

            _collection.Show(source);
            yield return null;

            Assert.AreEqual(2, callCount, "Оба объекта должны вызвать OnAdded");
        }

    }
}
