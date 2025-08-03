using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_OnDespawned_Tests
    {
        [Test]
        public void Despawn_Raises_OnDespawned_IfPreviouslySpawned()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnDespawned += () => callCount++;

            world.Spawn(); // make it spawned
            world.Despawn(); // should raise

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Despawn_DoesNotRaise_OnDespawned_IfNotSpawned()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnDespawned += () => called = true;

            world.Despawn(); // not yet spawned

            Assert.IsFalse(called);
        }

        [Test]
        public void Despawn_DoesNotRaise_OnDespawned_IfAlreadyDespawned()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnDespawned += () => callCount++;

            world.Spawn();
            world.Despawn();
            world.Despawn(); // second call should do nothing

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Despawn_AfterEnable_StillRaises_OnDespawned()
        {
            var world = new EntityWorld<Entity>();
            int callCount = 0;
            world.OnDespawned += () => callCount++;

            world.Activate(); // triggers Spawn + Enable
            world.Despawn(); // should still raise

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Despawn_LateSubscriber_DoesNotGetNotified()
        {
            var world = new EntityWorld<Entity>();

            world.Spawn();
            world.Despawn();

            bool called = false;
            world.OnDespawned += () => called = true;

            Assert.IsFalse(called);
        }
    }
}