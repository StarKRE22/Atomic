using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class EntityTests
    {
        [Test]
        public void Equals_Object_Null_ReturnsFalse()
        {
            var entity = new Entity("Test");
            Assert.IsFalse(entity.Equals((object) null));
        }

        [Test]
        public void Equals_Object_DifferentType_ReturnsFalse()
        {
            var entity = new Entity("Test");
            Assert.IsFalse(entity.Equals("not an entity"));
        }

        [Test]
        public void Equals_Object_SameReference_ReturnsTrue()
        {
            var entity = new Entity("Test");
            Assert.IsTrue(entity.Equals((object) entity));
        }

        [Test]
        public void Equals_Object_SameInstanceID_ReturnsTrue()
        {
            var entity1 = new Entity("Test");
            var entity2 = new Entity("Test");

            // Симулируем: вручную присваиваем один и тот же ID (только если ты контролируешь EntityRegistry)
            entity2._instanceId = entity1.InstanceID;

            Assert.IsTrue(entity1.Equals((object) entity2));
        }

        [Test]
        public void Equals_Object_DifferentInstanceID_ReturnsFalse()
        {
            var entity1 = new Entity("Test1");
            var entity2 = new Entity("Test2");

            Assert.IsFalse(entity1.Equals((object) entity2));
        }

        [Test]
        public void Equals_IEntity_Null_ReturnsFalse()
        {
            var entity = new Entity("Test");
            Assert.IsFalse(entity.Equals(null));
        }

        [Test]
        public void Equals_IEntity_SameInstanceID_ReturnsTrue()
        {
            var entity1 = new Entity("Test");
            IEntity entity2 = entity1;

            Assert.IsTrue(entity1.Equals(entity2));
        }

        [Test]
        public void Equals_IEntity_DifferentInstanceID_ReturnsFalse()
        {
            IEntity entity1 = new Entity("Test1");
            IEntity entity2 = new Entity("Test2");

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [Test]
        public void Equals_ReturnsFalse_ForDifferentEntities()
        {
            // Arrange
            var entity1 = new Entity("1");
            var entity2 = new Entity("2");

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            Assert.IsFalse(result);
        }
    }
}