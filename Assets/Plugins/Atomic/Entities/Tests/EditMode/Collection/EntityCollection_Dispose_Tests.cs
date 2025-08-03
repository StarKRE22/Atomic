using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityCollection_Dispose_Tests
    {
        [Test]
        public void Dispose_ClearsAllItems()
        {
            var collection = new EntityCollection<Entity>();
            collection.Add(new Entity());
            collection.Add(new Entity());

            collection.Dispose();

            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void Dispose_UnsubscribesAllEvents()
        {
            var collection = new EntityCollection<Entity>();

            bool removedCalled = false;
            bool addedCalled = false;
            bool stateChangedCalled = false;

            collection.OnRemoved += _ => removedCalled = true;
            collection.OnAdded += _ => addedCalled = true;
            collection.OnStateChanged += () => stateChangedCalled = true;

            collection.Dispose();

            // Попробуем что-то добавить/удалить, чтобы проверить, вызываются ли события
            var entity = new Entity();
            collection.Add(entity);
            collection.Remove(entity);

            Assert.IsFalse(removedCalled);
            Assert.IsFalse(addedCalled);
            Assert.IsFalse(stateChangedCalled);
        }

        [Test]
        public void Dispose_CanBeCalledMultipleTimes_WithoutError()
        {
            var collection = new EntityCollection<Entity>();
            collection.Add(new Entity());

            Assert.DoesNotThrow(() =>
            {
                collection.Dispose();
                collection.Dispose();
            });

            Assert.AreEqual(0, collection.Count);
        }
    }
}