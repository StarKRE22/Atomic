using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed class SceneEntityProxyTests
    {
        [Test]
        public void NewEntityProxy()
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

            SceneEntity entity = SceneEntity.Create("123",
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
                }
            );

            SceneEntityProxy proxy = entity.gameObject.AddComponent<SceneEntityProxy>();
            proxy._source = entity;

            //Assert:
            Assert.AreEqual("123", proxy.Name);

            Assert.IsTrue(proxy.HasTag(tag1));
            Assert.IsTrue(proxy.HasTag(tag2));
            Assert.IsFalse(proxy.HasTag(tag3));

            Assert.AreEqual(new[] {tag1, tag2}, entity.GetTags());

            Assert.IsTrue(proxy.HasValue(key1));
            Assert.IsTrue(proxy.HasValue(key2));
            Assert.IsFalse(proxy.HasValue(key3));

            Assert.AreEqual(
                new Dictionary<int, object>
                {
                    {key1, val1},
                    {key2, val2}
                },
                proxy.GetValues()
            );

            Assert.AreEqual(val1, proxy.GetValue<object>(key1));
            Assert.AreEqual("Test", proxy.GetValue<string>(key2));

            Assert.IsTrue(proxy.HasBehaviour(behaviour1));
            Assert.IsTrue(proxy.HasBehaviour(behaviour2));
            Assert.IsTrue(proxy.HasBehaviour(behaviour3));

            Assert.AreEqual(
                new[]
                {
                    behaviour1,
                    behaviour2,
                    behaviour3
                },
                proxy.GetBehaviours()
            );
        }

        [Test]
        public void NotEquals()
        {
            //Arrange:
            var entity1 = SceneEntity.Create("1");
            var proxy1 = entity1.gameObject.AddComponent<SceneEntityProxy>();
            proxy1._source = entity1;
            
            var entity2 = SceneEntity.Create("2");
            var proxy2 = entity2.gameObject.AddComponent<SceneEntityProxy>();
            proxy2._source = entity2;
        
            //Assert:
            Assert.IsFalse(proxy1.Equals(proxy2));
            Assert.IsFalse(proxy2.Equals(proxy1));
        }

        [Test]
        public void HashCodeTest()
        {
            var entity1 = SceneEntity.Create("1");
            var proxy1 = entity1.gameObject.AddComponent<SceneEntityProxy>();
            proxy1._source = entity1;

            Assert.AreEqual(entity1.Id, proxy1.GetHashCode());
        }
        
        [Test]
        public void WhenSourcesEqualsThenEquals()
        {
            //Arrange:
            var entity1 = SceneEntity.Create("1");
            var proxy1 = entity1.gameObject.AddComponent<SceneEntityProxy>();
            proxy1._source = entity1;
            
            var entity2 = SceneEntity.Create("2");
            var proxy2 = entity2.gameObject.AddComponent<SceneEntityProxy>();
            proxy2._source = entity1;
            
            Assert.IsTrue(proxy1.Equals(proxy2));
        }
    }
}