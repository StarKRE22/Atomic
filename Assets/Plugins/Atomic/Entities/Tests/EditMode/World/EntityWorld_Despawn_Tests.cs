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

            Assert.IsFalse(world.Spawned);
        }

        [Test]
        public void Despawn_CallsDespawn_OnAllEntities()
        {
            var e1 = new EntitySpy();
            var e2 = new EntitySpy();
            var world = new EntityWorld<EntitySpy>(e1, e2);

            world.Spawn();   // must be spawned before Despawn
            world.Despawn();

            Assert.IsTrue(e1.DespawnCalled);
            Assert.IsTrue(e2.DespawnCalled);
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
            var world = new DespawnTrackingWorld();
            world.Spawn();

            world.Despawn();

            Assert.IsTrue(world.NotifyWasCalled);
        }

        [Test]
        public void Despawn_WhenNotSpawned_DoesNothing()
        {
            var world = new EntityWorld<Entity>();

            bool called = false;
            world.OnDespawned += () => called = true;

            world.Despawn(); // should silently exit

            Assert.IsFalse(world.Spawned);
            Assert.IsFalse(called);
        }
        
        private class EntitySpy : Entity
        {
            public bool DespawnCalled { get; private set; }
            public override void Despawn() => DespawnCalled = true;
        }
    }
}