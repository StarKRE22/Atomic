using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityCollection_Count_Tests
    {
        private EntityCollection<Entity> _collection;
        private Entity _entity1;
        private Entity _entity2;

        [SetUp]
        public void SetUp()
        {
            _collection = new EntityCollection<Entity>(capacity: 4);
            _entity1 = new Entity();
            _entity2 = new Entity();
        }

        [TearDown]
        public void TearDown()
        {
            _collection.Dispose();
        }

        [Test]
        public void Count_Should_Be_Zero_After_Creation()
        {
            Assert.AreEqual(0, _collection.Count);
        }

        [Test]
        public void Count_Should_Increase_When_Entity_Added()
        {
            _collection.Add(_entity1);
            Assert.AreEqual(1, _collection.Count);

            _collection.Add(_entity2);
            Assert.AreEqual(2, _collection.Count);
        }

        [Test]
        public void Count_Should_Not_Change_When_Adding_Duplicate()
        {
            _collection.Add(_entity1);
            bool result = _collection.Add(_entity1); // should not add again

            Assert.IsFalse(result);
            Assert.AreEqual(1, _collection.Count);
        }

        [Test]
        public void Count_Should_Decrease_When_Entity_Removed()
        {
            _collection.Add(_entity1);
            _collection.Add(_entity2);

            _collection.Remove(_entity1);
            Assert.AreEqual(1, _collection.Count);

            _collection.Remove(_entity2);
            Assert.AreEqual(0, _collection.Count);
        }

        [Test]
        public void Count_Should_Be_Zero_After_Clear()
        {
            _collection.Add(_entity1);
            _collection.Add(_entity2);
            _collection.Clear();

            Assert.AreEqual(0, _collection.Count);
        }

        [Test]
        public void Count_After_AddRange_Should_Match_Number_Of_Entities()
        {
            var collection = new EntityCollection<Entity>();
            collection.AddRange(new Entity(), new Entity(), new Entity(), new Entity(), new Entity());

            Assert.AreEqual(5, collection.Count);
        }

        [Test]
        public void Count_Should_Not_Change_When_Removing_NonExisting_Entity()
        {
            var collection = new EntityCollection<Entity>();
            var existing = new Entity();
            var nonExisting = new Entity();

            collection.Add(existing);
            collection.Remove(nonExisting);

            Assert.AreEqual(1, collection.Count);
        }

        [Test]
        public void Count_Should_Reflect_ReAdding_Removed_Entity()
        {
            var collection = new EntityCollection<Entity>();
            var entity = new Entity();

            collection.Add(entity);
            collection.Remove(entity);
            collection.Add(entity);

            Assert.AreEqual(1, collection.Count);
        }

        [Test]
        public void Count_Should_Be_Zero_After_Dispose()
        {
            var collection = new EntityCollection<Entity>();
            collection.Add(new Entity());
            collection.Add(new Entity());
            collection.Add(new Entity());

            collection.Dispose();

            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void Count_Should_Be_Correct_After_Interleaved_Add_Remove()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();
            var e3 = new Entity();
            var e4 = new Entity();

            collection.Add(e1);
            collection.Add(e2);
            collection.Add(e3);
            collection.Add(e4);
            collection.Remove(e2);
            collection.Remove(e3);

            Assert.AreEqual(2, collection.Count);
        }
    }
}