using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_Despawn_Tests
    {
        [Test]
        public void Despawn_SetsSpawnedFalse()
        {
            var world = new EntityWorld<Entity>();
            world.Spawn();
            world.Despawn();

            Assert.IsFalse(world.IsSpawned);
        }

        [Test]
        public void Despawn_CallsDespawn_OnAllEntities()
        {
            var e1 = new Entity();
            var e2 = new Entity();
            var world = new EntityWorld<Entity>(e1, e2);
            
            var despawn1 = false;
            var despawn2 = false;

            e1.WhenDispose(() => despawn1 = true);
            e2.WhenDispose(() => despawn2 = true);
            
            world.Spawn();   // must be spawned before Despawn
            world.Despawn();

            Assert.IsTrue(despawn1);
            Assert.IsTrue(despawn2);
        }

        [Test]
        public void Despawn_DisablesWorld_IfEnabled()
        {
            var world = new EntityWorld<Entity>();
            world.Enable();  // this will spawn as well

            world.Despawn();

            Assert.IsFalse(world.Enabled);
        }

        [Test]
        public void Despawn_Raises_OnDespawned_Event()
        {
            var world = new EntityWorld<Entity>();
            world.Spawn();

            bool eventCalled = false;
            world.OnDespawned += () => eventCalled = true;

            world.Despawn();

            Assert.IsTrue(eventCalled);
        }

        [Test]
        public void Despawn_Calls_NotifyAboutStateChanged()
        {
            var world = new EntityWorld();
            world.Spawn();

            var wasDespawn = false;
            world.WhenDespawn(() => wasDespawn = true);
            world.Despawn();

            Assert.IsTrue(wasDespawn);
        }

        [Test]
        public void Despawn_WhenNotSpawned_DoesNothing()
        {
            var world = new EntityWorld<Entity>();

            bool called = false;
            world.OnDespawned += () => called = true;

            world.Despawn(); // should silently exit

            Assert.IsFalse(world.IsSpawned);
            Assert.IsFalse(called);
        }
    }
}