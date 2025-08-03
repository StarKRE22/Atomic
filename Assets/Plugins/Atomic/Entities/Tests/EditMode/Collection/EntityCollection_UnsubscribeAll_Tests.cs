using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityCollection_UnsubscribeAll_Tests
    {
        [Test]
        public void UnsubscribeAll_Removes_OnAdded_Handler()
        {
            var collection = new EntityCollection<Entity>();
            bool addedCalled = false;

            collection.OnAdded += _ => addedCalled = true;

            collection.UnsubscribeAll();

            collection.Add(new Entity());

            Assert.IsFalse(addedCalled, "OnAdded handler should have been unsubscribed.");
        }

        [Test]
        public void UnsubscribeAll_Removes_OnRemoved_Handler()
        {
            var collection = new EntityCollection<Entity>();
            bool removedCalled = false;
            var entity = new Entity();

            collection.Add(entity);
            collection.OnRemoved += _ => removedCalled = true;

            collection.UnsubscribeAll();
            collection.Remove(entity);

            Assert.IsFalse(removedCalled, "OnRemoved handler should have been unsubscribed.");
        }

        [Test]
        public void UnsubscribeAll_Removes_OnStateChanged_Handler()
        {
            var collection = new EntityCollection<Entity>();
            bool stateChangedCalled = false;

            collection.OnStateChanged += () => stateChangedCalled = true;

            collection.UnsubscribeAll();
            collection.Add(new Entity());

            Assert.IsFalse(stateChangedCalled, "OnStateChanged handler should have been unsubscribed.");
        }

        [Test]
        public void UnsubscribeAll_CanBeCalledMultipleTimes()
        {
            var collection = new EntityCollection<Entity>();

            collection.OnAdded += _ => { };
            collection.OnRemoved += _ => { };
            collection.OnStateChanged += () => { };

            Assert.DoesNotThrow(() =>
            {
                collection.UnsubscribeAll();
                collection.UnsubscribeAll(); // calling again should not throw
            });
        }

        [Test]
        public void UnsubscribeAll_WithoutSubscribers_DoesNotThrow()
        {
            var collection = new EntityCollection<Entity>();
            Assert.DoesNotThrow(() => collection.UnsubscribeAll());
        }
    }
}