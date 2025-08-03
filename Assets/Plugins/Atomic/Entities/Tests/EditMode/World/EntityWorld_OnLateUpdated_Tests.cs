using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_OnLateUpdated_Tests
    {
        [Test]
        public void OnLateUpdate_Raises_OnLateUpdated_WithCorrectDeltaTime()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            float receivedDelta = -1;
            world.OnLateUpdated += dt => receivedDelta = dt;

            world.OnLateUpdate(0.033f);

            Assert.AreEqual(0.033f, receivedDelta, 1e-6);
        }

        [Test]
        public void OnLateUpdate_DoesNotRaise_OnLateUpdated_IfNotEnabled()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;

            world.OnLateUpdated += _ => called = true;
            world.OnLateUpdate(0.033f);

            Assert.IsFalse(called);
        }

        [Test]
        public void OnLateUpdate_CallsHandler_EachTime()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            int callCount = 0;
            world.OnLateUpdated += _ => callCount++;

            world.OnLateUpdate(0.01f);
            world.OnLateUpdate(0.01f);

            Assert.AreEqual(2, callCount);
        }

        [Test]
        public void OnLateUpdate_LateSubscriber_DoesNotReceiveEvent()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            world.OnLateUpdate(0.016f);

            bool called = false;
            world.OnLateUpdated += _ => called = true;

            Assert.IsFalse(called);
        }

        [Test]
        public void OnLateUpdate_DoesNotThrow_WithoutEntities()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            Assert.DoesNotThrow(() => world.OnLateUpdate(0.02f));
        }
    }
}