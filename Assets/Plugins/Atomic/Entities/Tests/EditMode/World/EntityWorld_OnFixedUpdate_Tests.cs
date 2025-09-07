using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_OnFixedUpdate_Tests
    {
        [Test]
        public void OnFixedUpdate_Should_CallEntityFixedUpdate_WhenActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable();

            float delta = 0.02f;

            // Act
            world.FixedTick(delta);

            // Assert
            Assert.IsTrue(entity.WasFixedUpdated);
            Assert.AreEqual(delta, entity.LastFixedDeltaTime);
        }

        [Test]
        public void OnFixedUpdate_Should_TriggerOnFixedUpdatedEvent()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            world.Enable();

            float calledDelta = -1f;
            world.OnFixedTicked += dt => calledDelta = dt;

            float delta = 0.02f;

            // Act
            world.FixedTick(delta);

            // Assert
            Assert.AreEqual(delta, calledDelta);
        }

        [Test]
        public void OnFixedUpdate_Should_DoNothing_WhenNotActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Act
            world.FixedTick(0.02f);

            // Assert
            Assert.IsFalse(entity.WasFixedUpdated);
        }
    }
}