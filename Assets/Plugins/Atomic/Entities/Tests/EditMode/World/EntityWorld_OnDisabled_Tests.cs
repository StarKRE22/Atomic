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
            world.OnInactivated += () => callCount++;

            world.Activate();   // сначала включаем
            world.Inactivate();  // теперь можно выключить

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Disable_DoesNotRaise_OnDisabled_IfAlreadyDisabled()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnInactivated += () => callCount++;

            world.Activate();
            world.Inactivate();
            world.Inactivate(); // второй вызов — не должен вызывать событие

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Disable_DoesNotRaise_OnDisabled_IfNeverEnabled()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnInactivated += () => called = true;

            world.Inactivate(); // без Enable

            Assert.IsFalse(called);
        }

        [Test]
        public void Disable_Raises_OnDisabled_EvenWithoutEntities()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            bool called = false;
            world.OnInactivated += () => called = true;

            world.Inactivate();

            Assert.IsTrue(called);
        }

        [Test]
        public void Disable_LateSubscriber_DoesNotGetEvent()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();
            world.Inactivate();

            bool called = false;
            world.OnInactivated += () => called = true;

            Assert.IsFalse(called);
        }
    }
}