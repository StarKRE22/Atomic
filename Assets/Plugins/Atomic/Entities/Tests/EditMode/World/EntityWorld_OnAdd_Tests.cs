using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityWorld_OnAdd_Tests
    {
        [Test]
        public void OnAdd_Should_DoNothing_WhenWorldIsInactiveAndNotSpawned()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>();

            // Act
            world.Add(entity);

            // Assert
            Assert.IsFalse(entity.WasInitialized);
            Assert.IsFalse(entity.WasEnabled);
        }

        [Test]
        public void OnAdd_Should_SpawnAndActivateEntity_WhenWorldIsActive()
        {
            // Arrange
            var entity = new EntityDummy();
            var world = new EntityWorld<Entity>();
            world.Enable(); // Spawn + Activate

            // Act
            world.Add(entity);

            // Assert
            Assert.IsTrue(entity.WasInitialized);
            Assert.IsTrue(entity.WasEnabled);
        }
    }
}