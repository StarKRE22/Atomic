using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class SceneEntityWorldTests
    {
        [Test]
        public void AddEntity()
        {
            //Arrange
            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false);
            var entity = new Entity("Test Entity");
            IEntity wasEvent = null;

            //Act
            entityWorld.OnAdded += addedEntity => wasEvent = addedEntity;
            bool success = entityWorld.Add(entity);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity, wasEvent);
        }

        [Test]
        public void DelEntity()
        {
            //Arrange
            var entity = new Entity("Test Entity");
            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false, entity);
            IEntity wasEvent = null;

            //Act
            entityWorld.OnDeleted += rEntity => wasEvent = rEntity;
            bool success = entityWorld.Del(entity);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity, wasEvent);
        }

        [Test]
        public void HasEntity()
        {
            //Arrange
            var entity = new Entity("Test Entity");
            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false, entity);

            //Act
            bool exists = entityWorld.Has(entity);

            //Assert
            Assert.IsTrue(exists);
        }
    }
}