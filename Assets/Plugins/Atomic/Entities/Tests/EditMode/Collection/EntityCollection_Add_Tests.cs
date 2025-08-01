using NUnit.Framework;
using System;

namespace Atomic.Entities
{
    public class EntityCollection_Add_Tests
    {
        private EntityCollection<Entity> _collection;
        private Entity _addedEntity;
        private bool _stateChanged;

        [SetUp]
        public void SetUp()
        {
            _addedEntity = null;
            _stateChanged = false;
            
            _collection = new EntityCollection<Entity>();
            _collection.OnAdded += e => _addedEntity = e;
            _collection.OnStateChanged += () => _stateChanged = true;
        }

        [TearDown]
        public void TearDown()
        {
            EntityRegistry.Instance.Clear();
        }

        [Test]
        public void Add_NewEntity_ShouldReturnTrue_AndTriggerEvents()
        {
            var entity = new Entity();
            bool result = _collection.Add(entity);

            Assert.IsTrue(result, "Add should return true for new entity");
            Assert.AreEqual(entity, _addedEntity, "OnAdded should be called with the entity");
            Assert.IsTrue(_stateChanged, "OnStateChanged should be triggered");
        }

        [Test]
        public void Add_DuplicateEntity_ShouldReturnFalse_AndNotTriggerEvents()
        {
            var entity = new Entity();
            _collection.Add(entity);
            
            _addedEntity = null;
            _stateChanged = false;
            
            bool result = _collection.Add(entity);

            Assert.IsFalse(result, "Add should return false for duplicate entity");
            Assert.IsNull(_addedEntity, "OnAdded should not be triggered");
            Assert.IsFalse(_stateChanged, "OnStateChanged should not be triggered");
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _collection.Add(null));
            Assert.IsNull(_addedEntity, "OnAdded should not be triggered on null");
            Assert.IsFalse(_stateChanged, "OnStateChanged should not be triggered on null");
        }
        
        [Test]
        public void Add_1000_Entities_Successfully()
        {
            // Arrange
            var collection = new EntityCollection<Entity>();

            // Act
            for (int i = 0; i < 1000; i++)
            {
                var entity = new Entity();
                bool added = collection.Add(entity);

                // Assert each add succeeds
                Assert.IsTrue(added, $"Failed to add entity at index {i}");
                Assert.IsTrue(collection.Contains(entity), $"Entity not found after adding at index {i}");
            }

            // Assert total count
            Assert.AreEqual(1000, collection.Count);
        }
    }
}