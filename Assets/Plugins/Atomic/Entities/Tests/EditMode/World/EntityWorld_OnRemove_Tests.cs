using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_OnRemove_Tests
    {
        [Test]
        public void OnRemove_Should_DoNothing_WhenWorldIsInactiveAndNotSpawned()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Act
            world.Remove(entity);

            // Assert
            Assert.IsFalse(entity.WasDisabled);
            Assert.IsFalse(entity.WasDisposed);
        }

        [Test]
        public void OnRemove_Should_Disable_But_Not_Dispose_WhenWorldIsActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable();

            // Act
            world.Remove(entity);

            // Assert
            Assert.IsTrue(entity.WasDisabled);
            Assert.IsFalse(entity.WasDisposed);
        }
    }
}