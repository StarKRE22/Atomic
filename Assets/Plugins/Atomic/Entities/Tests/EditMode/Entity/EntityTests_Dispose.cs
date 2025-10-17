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

            entity.OnInitialized += () => Assert.Fail("Should be unsubscribed");
            entity.OnEnabled += () => Assert.Fail("Should be unsubscribed");
            entity.OnDisabled += () => Assert.Fail("Should be unsubscribed");
            entity.OnTicked += _ => Assert.Fail("Should be unsubscribed");
            entity.OnFixedTicked += _ => Assert.Fail("Should be unsubscribed");
            entity.OnLateTicked += _ => Assert.Fail("Should be unsubscribed");

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
            //Arrange:
            EntityRegistry.Instance.Clear();

            var entity = new Entity("Test");
            int id = entity.InstanceID;
            
            //Check for entity is registered
            Assert.Greater(id, 0);

            //Act:
            entity.Dispose();
            
            //Assert:
            Assert.IsFalse(EntityRegistry.Instance.Contains(entity));
            Assert.IsFalse(EntityRegistry.Instance.Contains(id));
        }
    }
}