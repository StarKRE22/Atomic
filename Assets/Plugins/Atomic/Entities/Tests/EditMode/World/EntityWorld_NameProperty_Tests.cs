using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_NameProperty_Tests
    {
        [Test]
        public void Name_DefaultValue_IsEmptyString()
        {
            var world = new EntityWorld<Entity>();
            Assert.AreEqual(string.Empty, world.Name);
        }

        [Test]
        public void Name_SetValue_StoresCorrectly()
        {
            var world = new EntityWorld<Entity>();
            world.Name = "TestWorld";
            Assert.AreEqual("TestWorld", world.Name);
        }

        [Test]
        public void Name_CanBeSetToNull()
        {
            var world = new EntityWorld<Entity>();
            world.Name = null;
            Assert.IsNull(world.Name);
        }

        [Test]
        public void Name_MultipleAssignments_WorkCorrectly()
        {
            var world = new EntityWorld<Entity>();
            world.Name = "A";
            world.Name = "B";
            Assert.AreEqual("B", world.Name);
        }

        [Test]
        public void Name_SetThroughConstructor_Works()
        {
            var world = new EntityWorld<Entity>("FromCtor");
            Assert.AreEqual("FromCtor", world.Name);
        }
    }
}