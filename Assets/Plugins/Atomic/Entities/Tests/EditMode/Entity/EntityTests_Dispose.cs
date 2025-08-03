using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void Dispose_ClearsTagsValuesBehaviours()
        {
            var entity = new Entity("Test",
                new[] {1, 2},
                new Dictionary<int, object> {{1, "value"}},
                new[] {new EntityBehaviourStub()});

            entity.Dispose();

            Assert.IsEmpty(entity.GetTags());
            Assert.IsEmpty(entity.GetValues());
            Assert.IsEmpty(entity.GetBehaviours());
        }

        [Test]
        public void Dispose_UnsubscribesAllEvents()
        {
            var entity = new Entity("Test");

            entity.OnStateChanged += () => Assert.Fail("Should be unsubscribed");

            entity.Dispose();

            entity.AddTag(1);
            entity.AddValue(1, 1);
        }

        [Test]
        public void Dispose_CallsDespawn()
        {
            var entity = new Entity("Test");
            entity.Spawn(); // допустим, ты это вызываешь перед Dispose
            Assert.IsTrue(entity.IsSpawned);

            entity.Dispose();
            Assert.IsFalse(entity.IsSpawned); // или другой индикатор деактивации
        }

        [Test]
        public void Dispose_UnregistersEntity()
        {
            EntityRegistry.Instance.Clear();

            var entity = new Entity("Test");
            int originalId = entity.InstanceID;
            Assert.Greater(entity.InstanceID, 0);

            entity.Dispose();

            // Проверяем, что instanceId больше не активен
            Assert.IsFalse(EntityRegistry.Instance.Contains(originalId));
        }
    }
}