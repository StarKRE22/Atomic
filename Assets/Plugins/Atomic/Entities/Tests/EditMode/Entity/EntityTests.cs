using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed partial class EntityTests
    {
        [Test]
        public void Constructor()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            const int key1 = 1;
            const int key2 = 2;
            const int key3 = 3;

            object val1 = new object();
            string val2 = new string("Test");

            IBehaviour behaviour1 = new BehaviourStub();
            IBehaviour behaviour2 = new BehaviourStub();
            IBehaviour behaviour3 = new BehaviourStub();

            //Act:
            var entity = new Entity("123",
                new[] {tag1, tag2},
                new Dictionary<int, object>
                {
                    {key1, val1},
                    {key2, val2},
                }, new[]
                {
                    behaviour1,
                    behaviour2,
                    behaviour3
                });

            //Assert:
            Assert.AreEqual("123", entity.Name);

            Assert.IsTrue(entity.HasTag(tag1));
            Assert.IsTrue(entity.HasTag(tag2));
            Assert.IsFalse(entity.HasTag(tag3));

            Assert.AreEqual(new[] {tag1, tag2}, entity.GetTags());

            Assert.AreEqual(2, entity.ValueCount);
            
            Assert.IsTrue(entity.HasValue(key1));
            Assert.IsTrue(entity.HasValue(key2));
            Assert.IsFalse(entity.HasValue(key3));
            
            
            
            CollectionAssert.AreEquivalent(new KeyValuePair<int, object>[]
            {
                new(key1, val1),
                new(key2, val2)
            }, entity.GetValues());

            Assert.AreEqual(val1, entity.GetValue<object>(key1));
            Assert.AreEqual("Test", entity.GetValue<string>(key2));

            Assert.IsTrue(entity.HasBehaviour(behaviour1));
            Assert.IsTrue(entity.HasBehaviour(behaviour2));
            Assert.IsTrue(entity.HasBehaviour(behaviour3));

            Assert.AreEqual(new[]
            {
                behaviour1,
                behaviour2,
                behaviour3
            }, entity.GetBehaviours());
        }

        [Test]
        public void NotEquals()
        {
            //Arrange:
            var entity1 = new Entity("1");
            var entity2 = new Entity("2");

            //Assert:
            Assert.IsFalse(entity1.Equals(entity2));
        }

        [Test]
        public void HashCodeTest()
        {
            var entity1 = new Entity("1");
            Assert.AreEqual(entity1.Id, entity1.GetHashCode());
        }

        [Test]
        public void WhenInstanceIdEqualsThenEquals()
        {
            //Arrange:
            var entity1 = new Entity("1");
            var entity2 = new Entity("2");

            //Act:
            entity2.Id = entity1.Id;

            //Assert:
            Assert.IsTrue(entity1.Equals(entity2));
        }
    }
}