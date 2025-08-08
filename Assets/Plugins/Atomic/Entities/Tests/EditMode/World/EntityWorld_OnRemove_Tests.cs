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
            Assert.IsFalse(entity.WasDeactivated);
            Assert.IsFalse(entity.WasDespawned);
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
            Assert.IsFalse(entity.WasDeactivated);
            Assert.IsTrue(entity.WasDespawned);
        }

        [Test]
        public void OnRemove_Should_DeactivateAndDespawn_WhenWorldIsActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Activate(); // Spawn + Activate

            // Act
            world.Remove(entity);

            // Assert
            Assert.IsTrue(entity.WasDeactivated);
            Assert.IsTrue(entity.WasDespawned);
        }
    }
}