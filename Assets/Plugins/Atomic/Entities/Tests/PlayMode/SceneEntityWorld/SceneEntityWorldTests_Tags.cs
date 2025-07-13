using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class SceneEntityWorldTests
    {
        [Test]
        public void GetEntityWithTag()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});

            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false, entity2, entity1, entity3);

            //Act
            bool success = entityWorld.GetWithTag(0, out IEntity entityWithTag);

            //Assert
            Assert.IsTrue(success);
            Assert.AreEqual(entity2, entityWithTag);
        }
        
        [Test]
        public void GetEntitiesWithTag()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});
            var entity4 = new Entity("4", new[] {1});

            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false,
                entity2, entity1, entity4, entity3);

            //Act
            IReadOnlyList<IEntity> entitiesWithTag = entityWorld.GetAllWithTag(0);

            //Assert
            Assert.AreEqual(3, entitiesWithTag.Count);
            Assert.AreEqual(entity2, entitiesWithTag[0]);
            Assert.AreEqual(entity1, entitiesWithTag[1]);
            Assert.AreEqual(entity3, entitiesWithTag[2]);
        }

        [Test]
        public void GetEntitiesWithTagNonAlloc()
        {
            var entity1 = new Entity("1", new[] {0});
            var entity2 = new Entity("2", new[] {0});
            var entity3 = new Entity("3", new[] {0});
            var entity4 = new Entity("4", new[] {1});

            var entityWorld = SceneEntityWorld.Create("Test", scanEntities: false,
                entity2, entity1, entity4, entity3);

            //Act
            IEntity[] buffer = new IEntity[10];
            int count = entityWorld.GetAllWithTag(0, buffer);

            //Assert
            Assert.AreEqual(3, count);
            Assert.AreEqual(entity2, buffer[0]);
            Assert.AreEqual(entity1, buffer[1]);
            Assert.AreEqual(entity3, buffer[2]);
        }
    }
}