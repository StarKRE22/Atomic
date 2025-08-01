using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityCollection_Constructor_Tests
    {
        [Test]
        public void Constructor_With_Capacity_Should_Initialize_Empty_Collection()
        {
            var collection = new EntityCollection<Entity>(16);

            Assert.IsNotNull(collection);
            Assert.AreEqual(0, collection.Count);
            Assert.IsFalse(collection.IsReadOnly);
        }

        [Test]
        public void Constructor_With_Zero_Capacity_Should_Not_Throw()
        {
            Assert.DoesNotThrow(() =>
            {
                var collection = new EntityCollection<Entity>(0);
                Assert.IsNotNull(collection);
                Assert.AreEqual(0, collection.Count);
            });
        }

        [Test]
        public void Constructor_With_Negative_Capacity_Should_Throw()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
            {
                _ = new EntityCollection<Entity>(-1);
            });
        }
    }
}