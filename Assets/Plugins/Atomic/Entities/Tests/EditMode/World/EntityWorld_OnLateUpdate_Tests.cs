using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_OnLateUpdate_Tests
    {
        [Test]
        public void OnLateUpdate_Should_CallEntityLateUpdate_WhenActive()
        {
            // Arrange
            var entity = new DummyEntity();
            var world = new EntityWorld<Entity>(entity);
            world.Activate();

            float delta = 0.033f;

            // Act
            world.OnLateUpdate(delta);

            // Assert
            Assert.IsTrue(entity.WasLateUpdated);
            Assert.AreEqual(delta, entity.LastLateDeltaTime);
        }

        [Test]
        public void OnLateUpdate_Should_TriggerOnLateUpdatedEvent()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            world.Activate();

            float calledDelta = -1f;
            world.OnLateUpdated += dt => calledDelta = dt;

            float delta = 0.033f;

            // Act
            world.OnLateUpdate(delta);

            // Assert
            Assert.AreEqual(delta, calledDelta);
        }

        [Test]
        public void OnLateUpdate_Should_DoNothing_WhenNotActive()
        {
            // Arrange
            var entity = new DummyEntity();
            var world = new EntityWorld<Entity>(entity);

            // Act
            world.OnLateUpdate(0.033f);

            // Assert
            Assert.IsFalse(entity.WasLateUpdated);
        }
    }
}