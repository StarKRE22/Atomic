using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_Dispose_Tests
    {
        [Test]
        public void Dispose_Should_DespawnEntities_IfSpawned()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Act
            world.Dispose();

            // Assert
            Assert.IsTrue(entity.WasDisposed);
        }

        [Test]
        public void Dispose_Should_DeactivateAndDespawn_IfActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable(); // Spawn + Activate

            // Act
            world.Dispose();

            // Assert
            Assert.IsTrue(entity.WasDisposed);
            Assert.IsFalse(world.Enabled);
        }

        [Test]
        public void Dispose_Should_ClearEntities()
        {
            // Arrange
            var world = new EntityWorld<Entity>(new EntityDummy(), new EntityDummy());

            // Act
            world.Dispose();

            // Assert
            Assert.AreEqual(0, world.Count);
        }
        
        [Test]
        public void Dispose_CanBeCalledMultipleTimes_Safely()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable();

            // Act
            world.Dispose();
            Assert.DoesNotThrow(() => world.Dispose());

            // Assert
            Assert.IsTrue(entity.WasDisposed);
            Assert.IsTrue(entity.WasDisabled);
        }

        [Test]
        public void Dispose_Should_UnsubscribeAllEvents()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            bool updatedCalled = false;
            world.OnUpdated += _ => updatedCalled = true;

            world.Enable();
            world.Dispose();

            // Act
            world.OnUpdate(1f); // событие не должно сработать

            // Assert
            Assert.IsFalse(updatedCalled);
        }

        [Test]
        public void Dispose_Should_ResetSpawnAndActiveFlags()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            world.Enable();

            // Act
            world.Dispose();

            // Assert
            Assert.IsFalse(world.Enabled);
        }
    }
}