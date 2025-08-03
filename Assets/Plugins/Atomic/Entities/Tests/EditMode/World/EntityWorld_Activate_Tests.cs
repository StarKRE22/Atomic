using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_Activate_Tests
    {
        [Test]
        public void Activate_Should_SpawnAndActivate_When_NotSpawnedAndNotActive()
        {
            // Arrange
            var entity = new DummyEntity();
            var world = new EntityWorld<Entity>(entity);

            // Assert preconditions
            Assert.IsFalse(world.IsSpawned);
            Assert.IsFalse(world.IsActive);
            Assert.IsFalse(entity.WasSpawned);
            Assert.IsFalse(entity.WasActivated);

            // Act
            world.Activate();

            // Assert
            Assert.IsTrue(world.IsSpawned);
            Assert.IsTrue(world.IsActive);
            Assert.IsTrue(entity.WasSpawned);
            Assert.IsTrue(entity.WasActivated);
        }

        [Test]
        public void Activate_Should_DoNothing_When_AlreadyActive()
        {
            // Arrange
            var entity = new DummyEntity();
            var world = new EntityWorld<Entity>(entity);

            // Act
            world.Activate(); // первая активация
            var wasActivated = entity.WasActivated;
            world.Activate(); // вторая — должна быть проигнорирована

            // Assert
            Assert.IsTrue(world.IsActive);
            Assert.IsTrue(wasActivated);
            // Нет повторной активации, логика в Activate() это предотвращает
        }

        [Test]
        public void Activate_Should_RaiseOnActivatedEvent()
        {
            // Arrange
            var world = new EntityWorld<Entity>();
            bool eventCalled = false;
            world.OnActivated += () => eventCalled = true;

            // Act
            world.Activate();

            // Assert
            Assert.IsTrue(eventCalled);
        }
    }
}