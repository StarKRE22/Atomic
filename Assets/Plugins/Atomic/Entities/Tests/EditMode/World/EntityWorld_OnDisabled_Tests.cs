using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_OnDisabled_Tests
    {
        [Test]
        public void Disable_Raises_OnDisabled_WhenEnabled()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnDeactivated += () => callCount++;

            world.Activate();   // сначала включаем
            world.Deactivate();  // теперь можно выключить

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Disable_DoesNotRaise_OnDisabled_IfAlreadyDisabled()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnDeactivated += () => callCount++;

            world.Activate();
            world.Deactivate();
            world.Deactivate(); // второй вызов — не должен вызывать событие

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Disable_DoesNotRaise_OnDisabled_IfNeverEnabled()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnDeactivated += () => called = true;

            world.Deactivate(); // без Enable

            Assert.IsFalse(called);
        }

        [Test]
        public void Disable_Raises_OnDisabled_EvenWithoutEntities()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            bool called = false;
            world.OnDeactivated += () => called = true;

            world.Deactivate();

            Assert.IsTrue(called);
        }

        [Test]
        public void Disable_LateSubscriber_DoesNotGetEvent()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();
            world.Deactivate();

            bool called = false;
            world.OnDeactivated += () => called = true;

            Assert.IsFalse(called);
        }
    }
}