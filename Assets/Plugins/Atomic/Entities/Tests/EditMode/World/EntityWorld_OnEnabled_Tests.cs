using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_OnEnabled_Tests
    {
        [Test]
        public void Enable_Raises_OnEnabled()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnActivated += () => callCount++;

            world.Activate();

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Enable_DoesNotRaise_OnEnabled_IfAlreadyEnabled()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnActivated += () => callCount++;

            world.Activate();
            world.Activate(); // повторный вызов

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Enable_TriggersSpawn_IfNotAlreadySpawned()
        {
            var entity = new Entity();
            var world = new EntityWorld<Entity>(entity);

            Assert.IsFalse(entity.IsSpawned);

            world.Activate();

            Assert.IsTrue(entity.IsSpawned);
        }

        [Test]
        public void Enable_Raises_OnEnabled_WithNoEntities()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnActivated += () => called = true;

            world.Activate();

            Assert.IsTrue(called);
        }

        [Test]
        public void Enable_LateSubscriber_DoesNotGetEvent()
        {
            var world = new EntityWorld<Entity>();
            world.Activate();

            bool called = false;
            world.OnActivated += () => called = true;

            Assert.IsFalse(called);
        }
    }
}