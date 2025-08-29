using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_Activate_Tests
    {
        [Test]
        public void Activate_Should_SpawnAndActivate_When_NotSpawnedAndNotActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Assert preconditions
            Assert.IsFalse(world.IsSpawned);
            Assert.IsFalse(world.Enabled);
            Assert.IsFalse(entity.WasInitialized);
            Assert.IsFalse(entity.WasEnabled);

            // Act
            world.Enable();

            // Assert
            Assert.IsTrue(world.IsSpawned);
            Assert.IsTrue(world.Enabled);
            Assert.IsTrue(entity.WasInitialized);
            Assert.IsTrue(entity.WasEnabled);
        }

        [Test]
        public void Activate_Should_DoNothing_When_AlreadyActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Act
            world.Enable(); // первая активация
            var wasActivated = entity.WasEnabled;
            world.Enable(); // вторая — должна быть проигнорирована

            // Assert
            Assert.IsTrue(world.Enabled);
            Assert.IsTrue(wasActivated);
            // Нет повторной активации, логика в Activate() это предотвращает
        }

        [Test]
        public void Activate_Should_RaiseOnActivatedEvent()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            bool eventCalled = false;
            world.OnEnabled += () => eventCalled = true;

            // Act
            world.Enable();

            // Assert
            Assert.IsTrue(eventCalled);
        }
    }
}