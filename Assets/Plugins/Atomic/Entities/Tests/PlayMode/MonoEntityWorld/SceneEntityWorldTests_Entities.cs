using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class MonoEntityWorldTests
    {
        [Test]
        public void AddEntity()
        {
            //Arrange
            var entityWorld = MonoEntityWorld.Create("Test", scanEntities: false);
            var entity = MonoEntity.Create("Test Entity");
            IEntity wasEvent = null;

            //Act
            entityWorld.OnAdded += addedEntity => wasEvent = addedEntity;
            bool success = entityWorld.Add(entity);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity, wasEvent);
        }

        [Test]
        public void RemoveEntity()
        {
            //Arrange
            var entity = MonoEntity.Create("Test Entity");
            var entityWorld = MonoEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            entityWorld.Add(entity);
            IEntity wasEvent = null;

            //Act
            entityWorld.OnRemoved += rEntity => wasEvent = rEntity;
            bool success = entityWorld.Remove(entity);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity, wasEvent);
        }

        [Test]
        public void ContainsEntity()
        {
            //Arrange
            var entity = MonoEntity.Create("Test Entity");
            var entityWorld = MonoEntityWorld.Create("Test", scanEntities: false, useUnityLifecycle: false);
            entityWorld.Add(entity);

            //Act
            bool exists = entityWorld.Contains(entity);

            //Assert
            Assert.IsTrue(exists);
        }
    }
}