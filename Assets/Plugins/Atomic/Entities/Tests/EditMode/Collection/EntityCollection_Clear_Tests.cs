using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityCollection_Clear_Tests
    {
        [Test]
        public void Clear_OnEmptyCollection_DoesNothing()
        {
            var collection = new EntityCollection<Entity>();

            // Should not throw or change state
            collection.Clear();

            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void Clear_RemovesAllElements()
        {
            var collection = new EntityCollection<Entity>();
            var e1 = new Entity();
            var e2 = new Entity();

            collection.Add(e1);
            collection.Add(e2);

            collection.Clear();

            Assert.AreEqual(0, collection.Count);
            Assert.IsFalse(collection.Contains(e1));
            Assert.IsFalse(collection.Contains(e2));
        }

        [Test]
        public void Clear_Invokes_OnRemoved_ForEachEntity()
        {
            var collection = new EntityCollection<Entity>();
            var removed = new List<Entity>();

            collection.OnRemoved += entity => removed.Add(entity);

            var e1 = new Entity();
            var e2 = new Entity();

            collection.Add(e1);
            collection.Add(e2);

            collection.Clear();

            CollectionAssert.AreEquivalent(new[] {e1, e2}, removed);
        }

        [Test]
        public void Clear_Invokes_OnStateChanged_Once()
        {
            //Arrange:
            var collection = new EntityCollection<Entity>();
            collection.Add(new Entity());
            collection.Add(new Entity());

            //Act:
            int stateChangedCount = 0;
            collection.OnStateChanged += () => stateChangedCount++;
            collection.Clear();

            //Assert:
            Assert.AreEqual(1, stateChangedCount);
        }
    }
}