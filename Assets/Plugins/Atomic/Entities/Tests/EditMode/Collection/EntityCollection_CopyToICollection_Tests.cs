using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityCollection_CopyToICollection_Tests
    {
        [Test]
        public void CopyTo_CopiesElementsIntoTargetCollection()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();

            collection.Add(e1);
            collection.Add(e2);

            var target = new List<Entity>();
            collection.CopyTo(target);

            CollectionAssert.AreEqual(new[] {e1, e2}, target);
        }

        [Test]
        public void CopyTo_PreservesOrderOfElements()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();
            var e3 = new Entity();

            collection.Add(e1);
            collection.Add(e2);
            collection.Add(e3);

            var target = new List<Entity>();
            collection.CopyTo(target);

            // Проверка порядка
            Assert.AreEqual(e1, target[0]);
            Assert.AreEqual(e2, target[1]);
            Assert.AreEqual(e3, target[2]);
        }

        [Test]
        public void CopyTo_AddsToExistingCollection()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();

            collection.Add(e1);

            var target = new List<Entity> {new Entity()}; // уже содержит 1 элемент

            collection.CopyTo(target);

            Assert.AreEqual(2, target.Count);
            Assert.AreEqual(e1, target[1]);
        }

        [Test]
        public void CopyTo_NullTarget_ThrowsArgumentNullException()
        {
            var collection = new EntityCollection<Entity>();
            collection.Add(new Entity());

            Assert.Throws<ArgumentNullException>(() => { collection.CopyTo(null); });
        }

        [Test]
        public void CopyTo_EmptyCollection_DoesNothing()
        {
            var collection = new EntityCollection<Entity>();
            var target = new List<Entity>();

            collection.CopyTo(target);

            Assert.IsEmpty(target);
        }
    }
}