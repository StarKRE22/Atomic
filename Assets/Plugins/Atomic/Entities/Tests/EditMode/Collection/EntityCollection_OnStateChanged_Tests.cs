using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityCollection_OnStateChanged_Tests
    {
        private EntityCollection<Entity> _collection;
        private bool _wasCalled;

        [SetUp]
        public void Setup()
        {
            _collection = new EntityCollection<Entity>();
            _wasCalled = false;
            _collection.OnStateChanged += () => _wasCalled = true;
        }

        [Test]
        public void Add_Should_Trigger_OnStateChanged()
        {
            _collection.Add(new Entity("Test"));
            Assert.IsTrue(_wasCalled, "OnStateChanged was not triggered after Add()");
        }

        [Test]
        public void Add_Duplicate_Should_Not_Trigger_OnStateChanged()
        {
            var entity = new Entity("Test");
            _collection.Add(entity);
            _wasCalled = false;
            _collection.Add(entity);
            Assert.IsFalse(_wasCalled, "OnStateChanged should not trigger when adding duplicate");
        }

        [Test]
        public void Remove_Should_Trigger_OnStateChanged()
        {
            var entity = new Entity("Test");
            _collection.Add(entity);
            _wasCalled = false;
            _collection.Remove(entity);
            Assert.IsTrue(_wasCalled, "OnStateChanged was not triggered after Remove()");
        }

        [Test]
        public void Remove_NonExistent_Should_Not_Trigger_OnStateChanged()
        {
            _collection.Remove(new Entity("None"));
            Assert.IsFalse(_wasCalled, "OnStateChanged should not trigger when removing nonexistent");
        }

        [Test]
        public void Clear_Should_Trigger_OnStateChanged()
        {
            _collection.Add(new Entity("Test"));
            _wasCalled = false;
            _collection.Clear();
            Assert.IsTrue(_wasCalled, "OnStateChanged was not triggered after Clear()");
        }

        [Test]
        public void Clear_EmptyCollection_Should_Not_Trigger_OnStateChanged()
        {
            _collection.Clear();
            Assert.IsFalse(_wasCalled, "OnStateChanged should not trigger when clearing empty collection");
        }
    }
}