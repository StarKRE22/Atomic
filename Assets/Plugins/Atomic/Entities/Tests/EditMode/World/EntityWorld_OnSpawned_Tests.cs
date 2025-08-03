using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_OnSpawned_Tests
    {
        [Test]
        public void Spawn_Raises_OnSpawned_Once()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnSpawned += () => callCount++;

            world.Spawn();

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Spawn_DoesNotRaise_OnSpawned_IfAlreadySpawned()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnSpawned += () => callCount++;

            world.Spawn();
            world.Spawn(); // второй вызов не должен вызвать событие

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Spawn_Raises_OnSpawned_EvenWithNoEntities()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnSpawned += () => called = true;

            world.Spawn();

            Assert.IsTrue(called);
        }

        [Test]
        public void Spawn_DoesNotCall_OnSpawned_ForLateSubscribers()
        {
            var world = new EntityWorld<Entity>();
            world.Spawn();

            bool called = false;
            world.OnSpawned += () => called = true;

            Assert.IsFalse(called);
        }

        [Test]
        public void Spawn_CalledAgainAfterDespawn_Raises_OnSpawned()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnSpawned += () => callCount++;

            world.Spawn();
            world.Despawn();
            world.Spawn();

            Assert.AreEqual(2, callCount);
        }
    }
}