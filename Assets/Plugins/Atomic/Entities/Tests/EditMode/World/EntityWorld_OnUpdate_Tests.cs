using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_OnUpdate_Tests
    {
        [Test]
        public void OnUpdate_Should_CallEntityUpdate_WhenActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable();

            float delta = 0.16f;

            // Act
            world.OnUpdate(delta);

            // Assert
            Assert.IsTrue(entity.WasUpdated);
            Assert.AreEqual(delta, entity.LastDeltaTime);
        }

        [Test]
        public void OnUpdate_Should_TriggerOnUpdatedEvent()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            world.Enable();

            float calledDelta = -1f;
            world.OnUpdated += dt => calledDelta = dt;

            float delta = 0.1f;

            // Act
            world.OnUpdate(delta);

            // Assert
            Assert.AreEqual(delta, calledDelta);
        }

        [Test]
        public void OnUpdate_Should_DoNothing_WhenNotActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Act
            world.OnUpdate(0.2f);

            // Assert
            Assert.IsFalse(entity.WasUpdated);
        }
    }
}