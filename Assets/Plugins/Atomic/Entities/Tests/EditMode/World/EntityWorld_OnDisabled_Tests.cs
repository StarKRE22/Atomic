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
            world.OnDisabled += () => callCount++;

            world.Enable();   // сначала включаем
            world.Disable();  // теперь можно выключить

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Disable_DoesNotRaise_OnDisabled_IfAlreadyDisabled()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnDisabled += () => callCount++;

            world.Enable();
            world.Disable();
            world.Disable(); // второй вызов — не должен вызывать событие

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Disable_DoesNotRaise_OnDisabled_IfNeverEnabled()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnDisabled += () => called = true;

            world.Disable(); // без Enable

            Assert.IsFalse(called);
        }

        [Test]
        public void Disable_Raises_OnDisabled_EvenWithoutEntities()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();

            bool called = false;
            world.OnDisabled += () => called = true;

            world.Disable();

            Assert.IsTrue(called);
        }

        [Test]
        public void Disable_LateSubscriber_DoesNotGetEvent()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();
            world.Disable();

            bool called = false;
            world.OnDisabled += () => called = true;

            Assert.IsFalse(called);
        }
    }
}