using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_EnabledProperty_Tests
    {
        [Test]
        public void Enabled_IsFalse_ByDefault()
        {
            var world = new EntityWorld<Entity>();
            Assert.IsFalse(world.Enabled);
        }

        [Test]
        public void Enabled_IsTrue_AfterEnable()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();
            Assert.IsTrue(world.Enabled);
        }

        [Test]
        public void Enabled_IsFalse_AfterDisable()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();
            world.Disable();
            Assert.IsFalse(world.Enabled);
        }

        [Test]
        public void Enabled_RemainsTrue_IfEnable_CalledAgain()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();
            world.Enable(); // повторный вызов — не должен сбросить состояние
            Assert.IsTrue(world.Enabled);
        }

        [Test]
        public void Enabled_RemainsFalse_IfDisable_CalledAgain()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();
            world.Disable();
            world.Disable(); // второй Disable не должен ничего изменить
            Assert.IsFalse(world.Enabled);
        }
    }

}