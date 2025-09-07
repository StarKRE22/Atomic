using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_Dispose_Tests
    {

        [Test]
        public void Dispose_Not_Should_DisposeEntity()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable();

            // Act
            world.Dispose();

            // Assert
            Assert.IsTrue(entity.WasInitialized);
            Assert.IsFalse(entity.WasDisposed);
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
        public void Dispose_Should_DisableEntity()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);
            world.Enable();

            // Act
            world.Dispose();

            // Assert
            Assert.IsTrue(entity.WasDisabled);
        }

        [Test]
        public void Dispose_Should_UnsubscribeAllEvents()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            bool updatedCalled = false;
            world.OnTicked += _ => updatedCalled = true;

            world.Enable();
            world.Dispose();

            // Act
            world.Tick(1f); // событие не должно сработать

            // Assert
            Assert.IsFalse(updatedCalled);
        }

        [Test]
        public void Dispose_Should_DisableWorld()
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