using NUnit.Framework;

namespace Atomic.Entities
{
    public partial class SceneEntityTests
    {
        [Test]
        public void Equals_EntitiesAreDifferent_ReturnsFalse()
        {
            //Arrange:
            var entity1 = SceneEntity.Create("1");
            var entity2 = SceneEntity.Create("2");

            //Assert:
            Assert.IsFalse(entity1.Equals(entity2));
        }

        [Test]
        public void Equals_EntitiesHaveSameInstanceId()
        {
            //Arrange:
            var entity1 = SceneEntity.Create("1");
            var entity2 = SceneEntity.Create("2");

            //Act:
            entity2.InstanceID = entity1.InstanceID;

            //Assert:
            Assert.IsTrue(entity1.Equals(entity2));
        }

        [Test]
        public void Equals_Object_Null_ReturnsFalse()
        {
            var entity = SceneEntity.Create("Test");
            Assert.IsFalse(entity.Equals((object) null));
        }

        [Test]
        public void Equals_Object_DifferentType_ReturnsFalse()
        {
            var entity = SceneEntity.Create("Test");
            Assert.IsFalse(entity.Equals("not an entity"));
        }

        [Test]
        public void Equals_Object_SameReference_ReturnsTrue()
        {
            var entity = SceneEntity.Create("Test");
            Assert.IsTrue(entity.Equals((object) entity));
        }

        [Test]
        public void Equals_Object_SameInstanceID_ReturnsTrue()
        {
            var entity1 = SceneEntity.Create("Test");
            var entity2 = SceneEntity.Create("Test");

            // Принудительно присваиваем один и тот же InstanceID
            entity2.InstanceID = entity1.InstanceID;

            Assert.IsTrue(entity1.Equals((object) entity2));
        }

        [Test]
        public void Equals_Object_DifferentInstanceID_ReturnsFalse()
        {
            var entity1 = SceneEntity.Create("Test1");
            var entity2 = SceneEntity.Create("Test2");

            Assert.IsFalse(entity1.Equals((object) entity2));
        }

        [Test]
        public void Equals_IEntity_Null_ReturnsFalse()
        {
            var entity = SceneEntity.Create("Test");
            Assert.IsFalse(entity.Equals((IEntity) null));
        }

        [Test]
        public void Equals_IEntity_SameInstanceID_ReturnsTrue()
        {
            var entity1 = SceneEntity.Create("Test");
            IEntity entity2 = entity1;

            Assert.IsTrue(entity1.Equals(entity2));
        }

        [Test]
        public void Equals_IEntity_DifferentInstanceID_ReturnsFalse()
        {
            IEntity entity1 = SceneEntity.Create("Test1");
            IEntity entity2 = SceneEntity.Create("Test2");

            Assert.IsFalse(entity1.Equals(entity2));
        }

        [Test]
        public void Equals_ReturnsFalse_ForDifferentEntities()
        {
            var entity1 = SceneEntity.Create("1");
            var entity2 = SceneEntity.Create("2");

            var result = entity1.Equals(entity2);

            Assert.IsFalse(result);
        }
    }
}