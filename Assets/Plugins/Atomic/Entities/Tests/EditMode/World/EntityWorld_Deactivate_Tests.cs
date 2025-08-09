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
            world.Activate();

            Assert.IsTrue(world.IsActive);

            // Act
            world.Deactivate();

            // Assert
            Assert.IsFalse(world.IsActive);
            Assert.IsTrue(entity.WasDeactivated);
        }

        [Test]
        public void Deactivate_Should_DoNothing_When_AlreadyInactive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Ensure inactive
            Assert.IsFalse(world.IsActive);

            // Act
            world.Deactivate();

            // Assert
            Assert.IsFalse(world.IsActive); // Still false
            Assert.IsFalse(entity.WasDeactivated); // Не вызывался
        }

        [Test]
        public void Deactivate_Should_TriggerOnDeactivatedEvent()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            world.Activate(); // Активируем перед деактивацией
            bool wasCalled = false;
            world.OnDeactivated += () => wasCalled = true;

            // Act
            world.Deactivate();

            // Assert
            Assert.IsTrue(wasCalled);
        }
    }
}