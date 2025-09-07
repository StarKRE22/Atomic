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
                new[] {new DummyEntityBehaviour()});

            entity.Dispose();

            Assert.IsEmpty(entity.GetTags());
            Assert.IsEmpty(entity.GetValues());
            Assert.IsEmpty(entity.GetBehaviours());
        }

        [Test]
        public void Dispose_UnsubscribesAllEvents()
        {
            var entity = new Entity("Test");

            entity.OnInitialized += () => Assert.Fail("Should be unsubscribed");
            entity.OnEnabled += () => Assert.Fail("Should be unsubscribed");
            entity.OnDisabled += () => Assert.Fail("Should be unsubscribed");
            entity.OnUpdated += _ => Assert.Fail("Should be unsubscribed");
            entity.OnFixedUpdated += _ => Assert.Fail("Should be unsubscribed");
            entity.OnLateUpdated += _ => Assert.Fail("Should be unsubscribed");

            entity.Dispose();

            //Assert:
            entity.Init();
            entity.Enable();
            entity.Tick(1);
            entity.Tick(1);
            entity.Disable();
        }

        [Test]
        public void Dispose_Entity_Will_Not_Initialized()
        {
            var entity = new Entity("Test");
            entity.Init(); 
            Assert.IsTrue(entity.Initialized);

            entity.Dispose();
            Assert.IsFalse(entity.Initialized);
        }

        [Test]
        public void Dispose_UnregistersEntity()
        {
            EntityRegistry.Instance.Clear();

            var entity = new Entity("Test");
            int originalId = entity.InstanceID;
            Assert.Greater(entity.InstanceID, 0);

            entity.Dispose();
            
            Assert.IsFalse(EntityRegistry.Instance.Contains(originalId));
        }
    }
}