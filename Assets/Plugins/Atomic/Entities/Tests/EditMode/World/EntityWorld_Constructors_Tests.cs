using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_Constructors_Tests
    {
        [Test]
        public void DefaultConstructor_SetsEmptyName_AndEmptyCollection()
        {
            var world = new EntityWorld<Entity>();

            Assert.AreEqual(string.Empty, world.Name);
            Assert.AreEqual(0, world.Count);
        }

        [Test]
        public void Constructor_WithEntities_SetsEmptyName_AndAddsEntities()
        {
            var e1 = new Entity();
            var e2 = new Entity();

            var world = new EntityWorld<Entity>(e1, e2);

            Assert.AreEqual(string.Empty, world.Name);
            Assert.AreEqual(2, world.Count);
            Assert.IsTrue(world.Contains(e1));
            Assert.IsTrue(world.Contains(e2));
        }

        [Test]
        public void Constructor_WithNameAndEntitiesArray_SetsName_AndAddsEntities()
        {
            var e1 = new Entity();
            var e2 = new Entity();

            var world = new EntityWorld<Entity>("GameWorld", e1, e2);

            Assert.AreEqual("GameWorld", world.Name);
            Assert.AreEqual(2, world.Count);
            Assert.IsTrue(world.Contains(e1));
            Assert.IsTrue(world.Contains(e2));
        }

        [Test]
        public void Constructor_WithNameAndEntityCollection_SetsName_AndAddsEntities()
        {
            var e1 = new Entity();
            var e2 = new Entity();
            var list = new List<Entity> {e1, e2};

            var world = new EntityWorld<Entity>("FromCollection", list);

            Assert.AreEqual("FromCollection", world.Name);
            Assert.AreEqual(2, world.Count);
            Assert.IsTrue(world.Contains(e1));
            Assert.IsTrue(world.Contains(e2));
        }

        [Test]
        public void Constructor_WithNullName_AllowsNull_AndAddsEntities()
        {
            var e1 = new Entity();
            var world = new EntityWorld<Entity>(name: null, e1);

            Assert.IsNull(world.Name);
            Assert.AreEqual(1, world.Count);
            Assert.IsTrue(world.Contains(e1));
        }

        [Test]
        public void Constructor_WithEntities()
        {
            //Arrange:
            var e1 = new Entity();
            var e2 = new Entity();
            var e3 = new Entity();
            var e4 = new Entity();
            var world = new EntityWorld("Test", e1, e2, e3);

            //Assert:
            Assert.AreEqual("Test", world.Name);

            Assert.AreEqual(3, world.Count);
            Assert.IsTrue(world.Contains(e1));
            Assert.IsTrue(world.Contains(e2));
            Assert.IsTrue(world.Contains(e3));

            Assert.IsFalse(world.Contains(e4));

            CollectionAssert.AreEqual(new List<IEntity> {e1, e2, e3}, world);
        }
    }
}