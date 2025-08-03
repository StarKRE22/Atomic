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
            Assert.IsFalse(world.IsActive);
        }

        [Test]
        public void Enabled_IsTrue_AfterEnable()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();
            Assert.IsTrue(world.IsActive);
        }

        [Test]
        public void Enabled_IsFalse_AfterDisable()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();
            world.Inactivate();
            Assert.IsFalse(world.IsActive);
        }

        [Test]
        public void Enabled_RemainsTrue_IfEnable_CalledAgain()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();
            world.Activate(); // повторный вызов — не должен сбросить состояние
            Assert.IsTrue(world.IsActive);
        }

        [Test]
        public void Enabled_RemainsFalse_IfDisable_CalledAgain()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();
            world.Inactivate();
            world.Inactivate(); // второй Disable не должен ничего изменить
            Assert.IsFalse(world.IsActive);
        }
    }

}