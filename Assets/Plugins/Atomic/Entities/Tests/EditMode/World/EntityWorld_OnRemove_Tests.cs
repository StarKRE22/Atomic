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
        public void OnRemove_Should_Despawn_WhenWorldIsSpawned()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Spawn();

            // Act
            world.Remove(entity);

            // Assert
            Assert.IsFalse(entity.WasDisabled);
            Assert.IsTrue(entity.WasDisposed);
        }

        [Test]
        public void OnRemove_Should_DeactivateAndDespawn_WhenWorldIsActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable(); // Spawn + Activate

            // Act
            world.Remove(entity);

            // Assert
            Assert.IsTrue(entity.WasDisabled);
            Assert.IsTrue(entity.WasDisposed);
        }
    }
}