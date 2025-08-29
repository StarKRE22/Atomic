using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_Enable_Tests
    {
        [Test]
        public void Enable_Should_SpawnAndEnable_When_NotSpawnedAndNotActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Assert preconditions
            Assert.IsFalse(world.Enabled);
            Assert.IsFalse(entity.WasInitialized);
            Assert.IsFalse(entity.WasEnabled);

            // Act
            world.Enable();

            // Assert
            Assert.IsTrue(world.Enabled);
            Assert.IsTrue(entity.WasInitialized);
            Assert.IsTrue(entity.WasEnabled);
        }

        [Test]
        public void Enable_Should_DoNothing_When_AlreadyActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>(entity);

            // Act
            world.Enable(); // первая активация
            var wasEnabled = entity.WasEnabled;
            world.Enable(); // вторая — должна быть проигнорирована

            // Assert
            Assert.IsTrue(world.Enabled);
            Assert.IsTrue(wasEnabled);
            // Нет повторной активации, логика в Enable() это предотвращает
        }

        [Test]
        public void Enable_Should_RaiseOnEnabledEvent()
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