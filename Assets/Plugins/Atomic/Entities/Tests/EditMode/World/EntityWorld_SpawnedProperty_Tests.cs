using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_SpawnedProperty_Tests
    {
        [Test]
        public void Spawned_IsFalse_ByDefault()
        {
            var world = new EntityWorld<Entity>();
            Assert.IsFalse(world.IsSpawned);
        }

        [Test]
        public void Spawned_IsTrue_AfterSpawn()
        {
            var world = new EntityWorld<Entity>();
            world.Spawn();
            Assert.IsTrue(world.IsSpawned);
        }

        [Test]
        public void Spawned_IsFalse_AfterDespawn()
        {
            var world = new EntityWorld<Entity>();
            world.Spawn();
            world.Despawn();
            Assert.IsFalse(world.IsSpawned);
        }

        [Test]
        public void Spawned_IsTrue_AfterEnable()
        {
            var world = new EntityWorld<Entity>();
            world.Enable(); // Enable triggers Spawn if not already spawned
            Assert.IsTrue(world.IsSpawned);
        }

        [Test]
        public void Spawned_RemainsTrue_IfSpawnCalledAgain()
        {
            var world = new EntityWorld<Entity>();
            world.Spawn();
            world.Spawn(); // second call should be ignored internally
            Assert.IsTrue(world.IsSpawned);
        }
    }
}