using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class SceneEntityTests
    {
        [Test]
        public void Create()
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

            IEntityBehaviour behaviour1 = new EntityBehaviourStub();
            IEntityBehaviour behaviour2 = new EntityBehaviourStub();
            IEntityBehaviour behaviour3 = new EntityBehaviourStub();

            //Act:
            var entity = SceneEntity.Create(
                "123",
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

            Assert.IsTrue(entity.HasValue(key1));
            Assert.IsTrue(entity.HasValue(key2));
            Assert.IsFalse(entity.HasValue(key3));
            Assert.AreEqual(new Dictionary<int, object>
            {
                {key1, val1},
                {key2, val2}
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
    }
}