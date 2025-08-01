using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityCollection_OnAdded_Tests
    {
        private EntityCollection<Entity> _collection;
        private Entity _received;

        [SetUp]
        public void Setup()
        {
            _collection = new EntityCollection<Entity>();
            _received = null;
            _collection.OnAdded += e => _received = e;
        }

        [Test]
        public void Add_Should_Invoke_OnAdded_With_Entity()
        {
            var entity = new Entity();
            _collection.Add(entity);
            Assert.AreEqual(entity, _received);
        }

        [Test]
        public void Add_Duplicate_Should_Not_Invoke_OnAdded()
        {
            var entity = new Entity();
            _collection.Add(entity);
            _received = null;

            _collection.Add(entity);
            Assert.IsNull(_received, "OnAdded should not fire when adding duplicate");
        }

        [Test]
        public void Add_Null_Should_Throw_And_Not_Trigger_OnAdded()
        {
            Assert.Throws<ArgumentNullException>(() => _collection.Add(null));
            Assert.IsNull(_received, "OnAdded should not fire on null");
        }

        [Test]
        public void AddRange_Should_Invoke_OnAdded_Multiple_Times()
        {
            var added = new List<Entity>();
            _collection.OnAdded += added.Add;

            var a = new Entity();
            var b = new Entity();
            var c = new Entity();

            _collection.AddRange(a, b, c);

            CollectionAssert.AreEquivalent(new[] { a, b, c }, added);
        }
    }
}