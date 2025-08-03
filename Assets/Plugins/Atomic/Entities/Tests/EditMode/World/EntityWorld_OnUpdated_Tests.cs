using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_OnUpdated_Tests
    {
        [Test]
        public void OnUpdate_Raises_OnUpdated_WithCorrectDeltaTime()
        {
            var world = new EntityWorld<Entity>();
            world.Activate(); // must be enabled for OnUpdate to run

            float receivedDelta = -1;
            world.OnUpdated += dt => receivedDelta = dt;

            world.OnUpdate(0.123f);

            Assert.AreEqual(0.123f, receivedDelta, 1e-6);
        }

        [Test]
        public void OnUpdate_DoesNotRaise_OnUpdated_IfNotEnabled()
        {
            var world = new EntityWorld<Entity>();

            bool called = false;
            world.OnUpdated += _ => called = true;

            world.OnUpdate(0.5f);

            Assert.IsFalse(called);
        }

        [Test]
        public void OnUpdate_CallsHandler_OncePerCall()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            int callCount = 0;
            world.OnUpdated += _ => callCount++;

            world.OnUpdate(0.1f);
            world.OnUpdate(0.2f);

            Assert.AreEqual(2, callCount);
        }

        [Test]
        public void OnUpdate_LateSubscriber_DoesNotGetPreviousUpdate()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            world.OnUpdate(0.25f);

            bool called = false;
            world.OnUpdated += _ => called = true;

            Assert.IsFalse(called);
        }

        [Test]
        public void OnUpdate_Works_WithNoEntities()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            Assert.DoesNotThrow(() => world.OnUpdate(0.016f));
        }
    }

}