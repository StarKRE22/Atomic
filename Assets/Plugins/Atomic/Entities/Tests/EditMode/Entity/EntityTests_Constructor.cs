using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void Constructor_SetsName()
        {
            var entity = new Entity("123");
            Assert.AreEqual("123", entity.Name);
        }

        [Test]
        public void Constructor_AddsTags()
        {
            var entity = new Entity("123", new[] {1, 2}, null, null);

            Assert.IsTrue(entity.HasTag(1));
            Assert.IsTrue(entity.HasTag(2));
            Assert.IsFalse(entity.HasTag(3));

            CollectionAssert.AreEquivalent(new[] {1, 2}, entity.GetTags());
        }

        [Test]
        public void Constructor_AddsValues()
        {
            var val1 = new object();
            const string val2 = "Test";

            var entity = new Entity("123", null, new Dictionary<int, object>
            {
                {1, val1},
                {2, val2}
            }, null);

            Assert.AreEqual(2, entity.ValueCount);

            Assert.IsTrue(entity.HasValue(1));
            Assert.IsTrue(entity.HasValue(2));
            Assert.IsFalse(entity.HasValue(3));

            CollectionAssert.AreEquivalent(new[]
            {
                new KeyValuePair<int, object>(1, val1),
                new KeyValuePair<int, object>(2, val2)
            }, entity.GetValues());

            Assert.AreEqual(val1, entity.GetValue<object>(1));
            Assert.AreEqual("Test", entity.GetValue<string>(2));
        }

        [Test]
        public void Constructor_AddsBehaviours()
        {
            var behaviour1 = new EntityBehaviourStub();
            var behaviour2 = new EntityBehaviourStub();
            var behaviour3 = new EntityBehaviourStub();

            var entity = new Entity("123", Array.Empty<string>(), null, new[]
            {
                behaviour1, behaviour2, behaviour3
            });

            Assert.IsTrue(entity.HasBehaviour(behaviour1));
            Assert.IsTrue(entity.HasBehaviour(behaviour2));
            Assert.IsTrue(entity.HasBehaviour(behaviour3));

            CollectionAssert.AreEqual(new[] {behaviour1, behaviour2, behaviour3}, entity.GetBehaviours());
        }

        [Test]
        public void Constructor_WhenNameIsNull_SetsNameToEmpty()
        {
            var entity = new Entity();
            Assert.AreEqual(string.Empty, entity.Name);
        }

        [Test]
        public void Constructor_AllCollectionsAreNull_EntityIsEmpty()
        {
            var entity = new Entity("Test", Array.Empty<string>(), null, null);

            Assert.AreEqual("Test", entity.Name);
            Assert.IsEmpty(entity.GetTags());
            Assert.IsEmpty(entity.GetValues());
            Assert.IsEmpty(entity.GetBehaviours());
        }

        [Test]
        public void Constructor_EmptyCollections_EntityHasNoData()
        {
            var entity = new Entity("Test", Array.Empty<int>(), Array.Empty<KeyValuePair<int, object>>(),
                Array.Empty<IEntityBehaviour>());

            Assert.AreEqual("Test", entity.Name);
            Assert.IsEmpty(entity.GetTags());
            Assert.IsEmpty(entity.GetValues());
            Assert.IsEmpty(entity.GetBehaviours());
        }

        [Test]
        public void Constructor_DuplicateTagsAndValues_ThrowsException()
        {
            var tags = new[] {1, 1, 2};
            var values = new[]
            {
                new KeyValuePair<int, object>(1, "A"),
                new KeyValuePair<int, object>(1, "B")
            };

            Assert.Throws<ArgumentException>(() =>
            {
                var unused = new Entity("DupTest", tags, values, null);
            }); 
        }

        [Test]
        public void Constructor_MixedValueTypes_StoredCorrectly()
        {
            var values = new[]
            {
                new KeyValuePair<int, object>(1, 42),
                new KeyValuePair<int, object>(2, "hello"),
                new KeyValuePair<int, object>(3, true)
            };

            var entity = new Entity("TypesTest", null, values, null);

            Assert.AreEqual(42, entity.GetValue<int>(1));
            Assert.AreEqual("hello", entity.GetValue<string>(2));
            Assert.AreEqual(true, entity.GetValue<bool>(3));
        }
        
        [Test]
        public void Constructor_AssignsInstanceId()
        {
            EntityRegistry.Instance.Clear();

            var entity = new Entity("Test");
            Assert.Greater(entity.InstanceID, 0); // или другой валидный критерий
        }
    }
}