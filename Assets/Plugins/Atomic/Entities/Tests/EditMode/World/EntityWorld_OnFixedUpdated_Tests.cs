using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_OnFixedUpdated_Tests
    {
        [Test]
        public void OnFixedUpdate_Raises_OnFixedUpdated_WithCorrectDeltaTime()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();

            float receivedDelta = -1;
            world.OnFixedTicked += dt => receivedDelta = dt;

            world.FixedTick(0.02f);

            Assert.AreEqual(0.02f, receivedDelta, 1e-6);
        }

        [Test]
        public void OnFixedUpdate_DoesNotRaise_OnFixedUpdated_IfNotEnabled()
        {
            var world = new EntityWorld<Entity>();

            bool called = false;
            world.OnFixedTicked += _ => called = true;

            world.FixedTick(0.02f);

            Assert.IsFalse(called);
        }

        [Test]
        public void OnFixedUpdate_CallsHandler_OncePerCall()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();

            int count = 0;
            world.OnFixedTicked += _ => count++;

            world.FixedTick(0.01f);
            world.FixedTick(0.01f);

            Assert.AreEqual(2, count);
        }

        [Test]
        public void OnFixedUpdate_LateSubscriber_DoesNotGetCalled()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();

            world.FixedTick(0.03f);

            bool called = false;
            world.OnFixedTicked += _ => called = true;

            Assert.IsFalse(called);
        }

        [Test]
        public void OnFixedUpdate_DoesNotThrow_WhenNoEntities()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();

            Assert.DoesNotThrow(() => world.FixedTick(0.015f));
        }
    }
}