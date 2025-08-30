using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed class EntityWorld_Deactivate_Tests
    {
        [Test]
        public void Deactivate_Should_TurnOffWorldAndEntities()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable();

            Assert.IsTrue(world.Enabled);

            // Act
            world.Disable();

            // Assert
            Assert.IsFalse(world.Enabled);
            Assert.IsTrue(entity.WasDisabled);
        }

        [Test]
        public void Deactivate_Should_DoNothing_When_AlreadyInactive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Ensure inactive
            Assert.IsFalse(world.Enabled);

            // Act
            world.Disable();

            // Assert
            Assert.IsFalse(world.Enabled); // Still false
            Assert.IsFalse(entity.WasDisabled); // Не вызывался
        }

        [Test]
        public void Deactivate_Should_TriggerOnDeactivatedEvent()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            world.Enable(); // Активируем перед деактивацией
            bool wasCalled = false;
            world.OnDisabled += () => wasCalled = true;

            // Act
            world.Disable();

            // Assert
            Assert.IsTrue(wasCalled);
        }
    }
}