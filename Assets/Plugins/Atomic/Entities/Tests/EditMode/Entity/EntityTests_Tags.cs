using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void HasTag()
        {
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            Entity e = new Entity("123", new[] {tag1, tag2});

            Assert.IsTrue(e.HasTag(tag1));
            Assert.IsTrue(e.HasTag(tag2));
            Assert.IsFalse(e.HasTag(tag3));
        }

        [Test]
        public void AddTag()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            int addedTag = -1;

            Entity e = new Entity("123", new[] {tag1, tag2});
            e.OnTagAdded += (_, t) => addedTag = t;

            //Act:
            Assert.IsFalse(e.AddTag(tag1));
            Assert.AreEqual(-1, addedTag);

            Assert.IsFalse(e.AddTag(tag2));
            Assert.AreEqual(-1, addedTag);

            Assert.IsTrue(e.AddTag(tag3));
            Assert.AreEqual(tag3, addedTag);
        }

        [Test]
        public void DelTag()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;
            const int tag3 = 3;

            int removedTag = -1;

            Entity e = new Entity("123", new[] {tag1, tag2});
            e.OnTagDeleted += (_, t) => removedTag = t;

            //Act & Assert:
            Assert.IsTrue(e.DelTag(tag1));
            Assert.AreEqual(tag1, removedTag);

            Assert.IsTrue(e.DelTag(tag2));
            Assert.AreEqual(tag2, removedTag);

            Assert.IsFalse(e.DelTag(tag3));
            Assert.AreNotEqual(tag3, removedTag);
        }

        [Test]
        public void ClearTags()
        {
            //Arrange:
            const int tag1 = 1;
            const int tag2 = 2;

            Entity e = new Entity("123", new[] {tag1, tag2});
            // e.OnTagsCleared += _ => wasClear = true;
            //TODO:

            //Act:
            e.ClearTags();

            //Assert:
            Assert.IsFalse(e.HasTag(tag1));
            Assert.IsFalse(e.HasTag(tag2));
        }

        [Test]
        public void WhenClearEntityWithEmptyTagsThenFalse()
        {
            //Arrange:
            bool wasClear = false;

            Entity e = new Entity("123");
            // e.OnTagsCleared += _ => wasClear = true;
            //TODO:

            //Act:
            e.ClearTags();

            //Act:
            Assert.IsFalse(wasClear);
        }
    }
}