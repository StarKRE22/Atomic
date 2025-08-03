using System;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityCollection_CopyTo_Tests
    {
        [Test]
        public void CopyTo_CopiesElementsInOrder()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();
            var e3 = new Entity();

            collection.Add(e1);
            collection.Add(e2);
            collection.Add(e3);

            var target = new Entity[3];
            collection.CopyTo(target, 0);

            // Порядок должен сохраняться (в порядке добавления)
            CollectionAssert.AreEqual(new[] {e1, e2, e3}, target);
        }

        [Test]
        public void CopyTo_WithOffset_CopiesToCorrectPosition()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();

            collection.Add(e1);
            collection.Add(e2);

            var target = new Entity[5];
            collection.CopyTo(target, 2);

            Assert.IsNull(target[0]);
            Assert.IsNull(target[1]);
            Assert.AreEqual(e1, target[2]);
            Assert.AreEqual(e2, target[3]);
            Assert.IsNull(target[4]);
        }

        [Test]
        public void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            var collection = new EntityCollection<Entity>();
            collection.Add(new Entity());

            Assert.Throws<ArgumentNullException>(() => { collection.CopyTo(null, 0); });
        }

        [Test]
        public void CopyTo_NegativeIndex_ThrowsArgumentOutOfRangeException()
        {
            var collection = new EntityCollection<Entity>();
            collection.Add(new Entity());

            var array = new Entity[1];
            Assert.Throws<ArgumentOutOfRangeException>(() => { collection.CopyTo(array, -1); });
        }

        [Test]
        public void CopyTo_ArrayTooSmall_ThrowsArgumentException()
        {
            var collection = new EntityCollection<Entity>();
            collection.Add(new Entity());
            collection.Add(new Entity());

            var array = new Entity[1];
            Assert.Throws<ArgumentException>(() => { collection.CopyTo(array, 0); });
        }
    }
}