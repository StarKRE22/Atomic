using NUnit.Framework;
// ReSharper disable CollectionNeverUpdated.Local

namespace Atomic.Entities
{
    public class EntityCollection_Contains_Tests
    {
        [Test]
        public void Contains_Should_Return_True_For_Existing_Item()
        {
            var e = new Entity();
            var collection = new EntityCollection<Entity>(e);

            Assert.IsTrue(collection.Contains(e));
        }

        [Test]
        public void Contains_Should_Return_False_For_NonExisting_Item()
        {
            var collection = new EntityCollection<Entity>();
            var e = new Entity();

            Assert.IsFalse(collection.Contains(e));
        }

        [Test]
        public void Contains_Should_Return_False_For_Null()
        {
            var collection = new EntityCollection<Entity>();
            Assert.IsFalse(collection.Contains(null));
        }

        [Test]
        public void Contains_Should_Return_True_Only_For_Same_Reference()
        {
            var e1 = new Entity();
            var e2 = new Entity();
            var collection = new EntityCollection<Entity>(e1);

            Assert.IsFalse(collection.Contains(e2));
        }

        [Test]
        public void Contains_Should_Return_False_After_Removal()
        {
            var e = new Entity();
            var collection = new EntityCollection<Entity>(e);

            collection.Remove(e);

            Assert.IsFalse(collection.Contains(e));
        }
        
        [Test]
        public void Contains_Should_Work_Correctly_For_1000_Items()
        {
            var collection = new EntityCollection<Entity>();
            var target = new Entity();

            for (int i = 0; i < 999; i++)
                collection.Add(new Entity());

            collection.Add(target);

            Assert.IsTrue(collection.Contains(target));
        }

        [Test]
        public void Contains_Should_Return_False_For_Absent_Item_Among_10000()
        {
            var collection = new EntityCollection<Entity>();
            for (int i = 0; i < 10000; i++)
                collection.Add(new Entity());

            var absent = new Entity();

            Assert.IsFalse(collection.Contains(absent));
        }
    }
}