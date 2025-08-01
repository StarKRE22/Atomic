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
    }
}