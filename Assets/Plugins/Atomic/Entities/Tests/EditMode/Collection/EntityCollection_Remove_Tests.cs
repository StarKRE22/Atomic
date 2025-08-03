using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityCollection_Remove_Tests
    {
        [Test]
        public void Remove_ExistingItem_ReturnsTrueAndDecreasesCount()
        {
            var collection = new EntityCollection<Entity>();
            var entity = new Entity();

            collection.Add(entity);

            bool result = collection.Remove(entity);

            Assert.IsTrue(result);
            Assert.AreEqual(0, collection.Count);
            Assert.IsFalse(collection.Contains(entity));
        }

        [Test]
        public void Remove_NonExistentItem_ReturnsFalse()
        {
            var collection = new EntityCollection<Entity>();
            var entity = new Entity(); // not added

            bool result = collection.Remove(entity);

            Assert.IsFalse(result);
            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void Remove_NullItem_ReturnsFalse()
        {
            var collection = new EntityCollection<Entity>();

            bool result = collection.Remove(null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Remove_ItemFromMiddleOfCollection_UpdatesLinksCorrectly()
        {
            var collection = new EntityCollection<Entity>();
            var first = new Entity();
            var middle = new Entity();
            var last = new Entity();

            collection.Add(first);
            collection.Add(middle);
            collection.Add(last);

            bool removed = collection.Remove(middle);

            Assert.IsTrue(removed);
            Assert.AreEqual(2, collection.Count);
            Assert.IsFalse(collection.Contains(middle));
            Assert.IsTrue(collection.Contains(first));
            Assert.IsTrue(collection.Contains(last));
        }
    }
}