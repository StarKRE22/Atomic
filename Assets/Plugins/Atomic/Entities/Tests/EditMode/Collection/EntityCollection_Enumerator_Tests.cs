using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityCollection_Enumerator_Tests
    {
        [Test]
        public void Enumerator_IteratesOverAllElementsInOrder()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();
            var e3 = new Entity();

            collection.Add(e1);
            collection.Add(e2);
            collection.Add(e3);

            var result = new List<Entity>();
            using var enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
                result.Add(enumerator.Current);

            CollectionAssert.AreEqual(new[] {e1, e2, e3}, result);
        }

        [Test]
        public void Enumerator_MoveNext_ReturnsFalse_WhenEmpty()
        {
            var collection = new EntityCollection<Entity>();
            using var enumerator = collection.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Enumerator_Current_Throws_NoMoveNext()
        {
            var collection = new EntityCollection<Entity>();
            using var enumerator = collection.GetEnumerator();

            // До MoveNext() – _current == default
            Assert.AreEqual(null, enumerator.Current);
        }

        [Test]
        public void Enumerator_Reset_ResetsEnumeration()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();

            collection.Add(e1);
            collection.Add(e2);

            using var enumerator = collection.GetEnumerator();

            enumerator.MoveNext(); // e1
            enumerator.MoveNext(); // e2

            enumerator.Reset();

            var result = new List<Entity>();
            while (enumerator.MoveNext())
                result.Add(enumerator.Current);

            CollectionAssert.AreEqual(new[] {e1, e2}, result);
        }

        [Test]
        public void Enumerator_WorksInForeach()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();

            collection.Add(e1);
            collection.Add(e2);

            var seen = new List<Entity>();
            foreach (var entity in collection)
            {
                seen.Add(entity);
            }

            CollectionAssert.AreEqual(new[] {e1, e2}, seen);
        }
    }
}