using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_UnsubscribeAll
    {
        [Test]
        public void OnSpawned_Should_BeUnsubscribed()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnSpawned += () => called = true;

            world.UnsubscribeAll();
            world.Spawn();

            Assert.IsFalse(called);
        }

        [Test]
        public void OnActivated_Should_BeUnsubscribed()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnActivated += () => called = true;

            world.UnsubscribeAll();
            world.Activate();

            Assert.IsFalse(called);
        }

        [Test]
        public void OnDeactivated_Should_BeUnsubscribed()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnDeactivated += () => called = true;

            world.Activate();
            world.UnsubscribeAll();
            world.Deactivate();

            Assert.IsFalse(called);
        }

        [Test]
        public void OnDespawned_Should_BeUnsubscribed()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnDespawned += () => called = true;

            world.Spawn();
            world.UnsubscribeAll();
            world.Despawn();

            Assert.IsFalse(called);
        }

        [Test]
        public void OnUpdated_Should_BeUnsubscribed()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnUpdated += _ => called = true;

            world.UnsubscribeAll();
            world.OnUpdate(0.1f);

            Assert.IsFalse(called);
        }

        [Test]
        public void OnFixedUpdated_Should_BeUnsubscribed()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnFixedUpdated += _ => called = true;

            world.UnsubscribeAll();
            world.OnFixedUpdate(0.1f);

            Assert.IsFalse(called);
        }

        [Test]
        public void OnLateUpdated_Should_BeUnsubscribed()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnLateUpdated += _ => called = true;

            world.UnsubscribeAll();
            world.OnLateUpdate(0.1f);

            Assert.IsFalse(called);
        }
    }
}