using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityCollection_OnRemoved_Tests
    {
        private EntityCollection<Entity> _collection;
        private Entity _entity1;
        private Entity _entity2;
        private int _callbackCount;

        [SetUp]
        public void SetUp()
        {
            _collection = new EntityCollection<Entity>(capacity: 4);
            _entity1 = new Entity();
            _entity2 = new Entity();
            _callbackCount = 0;
        }

        [TearDown]
        public void TearDown()
        {
            _collection.Dispose();
        }

        [Test]
        public void Remove_Should_Invoke_OnRemoved()
        {
            _collection.Add(_entity1);

            _collection.OnRemoved += entity =>
            {
                Assert.AreEqual(_entity1, entity);
                _callbackCount++;
            };

            _collection.Remove(_entity1);
            Assert.AreEqual(1, _callbackCount);
        }

        [Test]
        public void Remove_Should_Not_Invoke_OnRemoved_When_Entity_Not_Present()
        {
            _collection.OnRemoved += _ => _callbackCount++;
            _collection.Remove(_entity1);

            Assert.AreEqual(0, _callbackCount);
        }

        [Test]
        public void Clear_Should_Invoke_OnRemoved_For_Each_Entity()
        {
            _collection.Add(_entity1);
            _collection.Add(_entity2);

            _collection.OnRemoved += _ => _callbackCount++;

            _collection.Clear();
            Assert.AreEqual(2, _callbackCount);
        }

        [Test]
        public void Dispose_Should_Invoke_OnRemoved()
        {
            _collection.Add(_entity1);
            _collection.OnRemoved += _ => _callbackCount++;

            _collection.Dispose(); // event cleared before invocation

            Assert.AreEqual(1, _callbackCount);
        }
    }
}