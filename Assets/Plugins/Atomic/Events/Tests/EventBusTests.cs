using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace Atomic.Events
{
    public sealed class EventBusTests
    {
        [Test]
        public void Constructor()
        {
            Assert.DoesNotThrow(() =>
            {
                var unused = new EventBus();
            });
        }

        [Test]
        public void Subscribe_Then_Declared()
        {
            //Arrange:
            var eventBus = new EventBus();
            
            //Act:
            eventBus.Subscribe(1, () => {});
            
            //Assert:
            Assert.IsTrue(eventBus.IsSubscribed(1));
        }

        [Test]
        public void Invoke()
        {
            //Arrange:
            bool wasEvent = false;
            
            var eventBus = new EventBus();
            eventBus.Subscribe(1, () => wasEvent = true);
            
            //Act:
            eventBus.Invoke(1);
            
            //Assert:
            Assert.IsTrue(wasEvent);
        }

        [Test]
        public void Invoke_WithArgs()
        {
            var args = new object();
            var wasEvent = false;
            object wasTarget = null;
            
            var eventBus = new EventBus();
            eventBus.Subscribe<object>("Hello", t =>
            {
                wasTarget = t;
                wasEvent = true;
            });
            eventBus.Invoke("Hello", args);
            
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(args, wasTarget);
        }

        // ──────────────────────────────────────────────────────
        //  Hash collision handling
        // ──────────────────────────────────────────────────────

        [Test]
        public void HashCollision_TwoKeysSameBucket_BothInvoked()
        {
            //Arrange:
            bool firstCalled = false;
            bool secondCalled = false;
            var bus = new EventBus();
            // Keys 1 and 3 both hash to bucket 1 in the default capacity-2 table.
            bus.Subscribe(1, () => firstCalled = true);
            bus.Subscribe(3, () => secondCalled = true);

            //Act:
            bus.Invoke(1);
            bus.Invoke(3);

            //Assert:
            Assert.IsTrue(firstCalled);
            Assert.IsTrue(secondCalled);
        }

        [Test]
        public void HashCollision_UnsubscribeOne_OtherStillInvoked()
        {
            //Arrange:
            bool firstCalled = false;
            bool secondCalled = false;
            var bus = new EventBus();
            Action first = () => firstCalled = true;
            bus.Subscribe(1, first);
            bus.Subscribe(3, () => secondCalled = true);

            //Act:
            bus.Unsubscribe(1, first);
            bus.Invoke(1);
            bus.Invoke(3);

            //Assert:
            Assert.IsFalse(firstCalled);
            Assert.IsTrue(secondCalled);
        }

        // ──────────────────────────────────────────────────────
        //  Capacity growth (prime resizing)
        // ──────────────────────────────────────────────────────

        [Test]
        public void CapacityGrowth_ManyEvents_AllInvokedAfterResize()
        {
            //Arrange:
            const int count = 100;
            var received = new bool[count];
            var bus = new EventBus();

            for (int i = 0; i < count; i++)
            {
                int index = i;
                bus.Subscribe(i, () => received[index] = true);
            }

            //Act:
            for (int i = 0; i < count; i++)
                bus.Invoke(i);

            //Assert:
            for (int i = 0; i < count; i++)
                Assert.IsTrue(received[i], $"Event {i} was not invoked after resize.");
        }

        [Test]
        public void CapacityGrowth_MissingKeyStillNotInvoked()
        {
            //Arrange:
            const int count = 100;
            var bus = new EventBus();
            for (int i = 0; i < count; i++)
                bus.Subscribe(i, () => { });

            //Act & Assert:
            Assert.IsFalse(bus.IsSubscribed(count));
            Assert.DoesNotThrow(() => bus.Invoke(count));
        }

        // ──────────────────────────────────────────────────────
        //  Free-list reuse after unsubscribe
        // ──────────────────────────────────────────────────────

        [Test]
        public void FreeListReuse_AfterUnsubscribe_NewKeyUsesSlot()
        {
            //Arrange:
            var bus = new EventBus();
            Action oldAction = () => { };
            bus.Subscribe(1, oldAction);
            bus.Subscribe(3, () => { });
            int initialCapacity = GetEventTableCapacity(bus);

            //Act:
            bus.Unsubscribe(1, oldAction);
            bool newCalled = false;
            bus.Subscribe(2, () => newCalled = true);

            //Assert:
            Assert.AreEqual(initialCapacity, GetEventTableCapacity(bus),
                "Capacity should not grow when a freed slot is reused.");
            Assert.AreEqual(2, GetEventTableCount(bus));
            Assert.IsFalse(bus.IsSubscribed(1));
            Assert.IsTrue(bus.IsSubscribed(2));
            Assert.IsTrue(bus.IsSubscribed(3));

            bus.Invoke(2);
            Assert.IsTrue(newCalled);

            Assert.DoesNotThrow(() => bus.Invoke(1));
            Assert.Throws<KeyNotFoundException>(() => bus.InvokeUnsafe(1));
        }

        // ──────────────────────────────────────────────────────
        //  InvokeUnsafe — 0 arguments
        // ──────────────────────────────────────────────────────

        [Test]
        public void InvokeUnsafe_0Arg_MissingKey_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => bus.InvokeUnsafe(42));
        }

        [Test]
        public void InvokeUnsafe_0Arg_Subscribed_Invokes()
        {
            //Arrange:
            bool called = false;
            var bus = new EventBus();
            bus.Subscribe(1, () => called = true);

            //Act:
            bus.InvokeUnsafe(1);

            //Assert:
            Assert.IsTrue(called);
        }

        // ──────────────────────────────────────────────────────
        //  InvokeUnsafe — 1 argument
        // ──────────────────────────────────────────────────────

        [Test]
        public void InvokeUnsafe_1Arg_MissingKey_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => bus.InvokeUnsafe(42, 1));
        }

        [Test]
        public void InvokeUnsafe_1Arg_Subscribed_Invokes()
        {
            //Arrange:
            int received = 0;
            var bus = new EventBus();
            bus.Subscribe<int>(1, v => received = v);

            //Act:
            bus.InvokeUnsafe(1, 99);

            //Assert:
            Assert.AreEqual(99, received);
        }

        // ──────────────────────────────────────────────────────
        //  InvokeUnsafe — 2 arguments
        // ──────────────────────────────────────────────────────

        [Test]
        public void InvokeUnsafe_2Args_MissingKey_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => bus.InvokeUnsafe(42, 1, "x"));
        }

        [Test]
        public void InvokeUnsafe_2Args_Subscribed_Invokes()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            var bus = new EventBus();
            bus.Subscribe<int, string>(1, (a, b) =>
            {
                receivedA = a;
                receivedB = b;
            });

            //Act:
            bus.InvokeUnsafe(1, 7, "hello");

            //Assert:
            Assert.AreEqual(7, receivedA);
            Assert.AreEqual("hello", receivedB);
        }

        // ──────────────────────────────────────────────────────
        //  InvokeUnsafe — 3 arguments
        // ──────────────────────────────────────────────────────

        [Test]
        public void InvokeUnsafe_3Args_MissingKey_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => bus.InvokeUnsafe(42, 1, "x", true));
        }

        [Test]
        public void InvokeUnsafe_3Args_Subscribed_Invokes()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            bool receivedC = false;
            var bus = new EventBus();
            bus.Subscribe<int, string, bool>(1, (a, b, c) =>
            {
                receivedA = a;                receivedB = b;                receivedC = c;
            });

            //Act:
            bus.InvokeUnsafe(1, 5, "world", true);

            //Assert:
            Assert.AreEqual(5, receivedA);
            Assert.AreEqual("world", receivedB);
            Assert.IsTrue(receivedC);
        }

        // ──────────────────────────────────────────────────────
        //  Dispose()
        // ──────────────────────────────────────────────────────

        [Test]
        public void Dispose_ClearsAllEvents()
        {
            //Arrange:
            int calls1 = 0;
            int calls2 = 0;
            var bus = new EventBus();
            bus.Subscribe(1, () => calls1++);
            bus.Subscribe(2, () => calls2++);

            //Act:
            bus.Dispose();
            bus.Invoke(1);
            bus.Invoke(2);

            //Assert:
            Assert.AreEqual(0, calls1);
            Assert.AreEqual(0, calls2);
            Assert.IsFalse(bus.IsSubscribed(1));
            Assert.IsFalse(bus.IsSubscribed(2));
            Assert.Throws<KeyNotFoundException>(() => bus.InvokeUnsafe(1));
        }

        [Test]
        public void Dispose_DoesNotThrowOnEmptyBus()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Dispose());
        }

        // ──────────────────────────────────────────────────────
        //  Dispose(int key)
        // ──────────────────────────────────────────────────────

        [Test]
        public void DisposeByKey_RemovesEvent()
        {
            //Arrange:
            int calls = 0;
            var bus = new EventBus();
            bus.Subscribe(1, () => calls++);

            //Act:
            bool removed = bus.Dispose(1);
            bus.Invoke(1);

            //Assert:
            Assert.IsTrue(removed);
            Assert.AreEqual(0, calls);
            Assert.IsFalse(bus.IsSubscribed(1));
            Assert.Throws<KeyNotFoundException>(() => bus.InvokeUnsafe(1));
        }

        [Test]
        public void DisposeByKey_MissingKey_ReturnsFalse()
        {
            //Arrange:
            var bus = new EventBus();

            //Act:
            bool removed = bus.Dispose(42);

            //Assert:
            Assert.IsFalse(removed);
        }

        // ──────────────────────────────────────────────────────
        //  SubscribeUnsafe
        // ──────────────────────────────────────────────────────

        [Test]
        public void SubscribeUnsafe_0Arg_SubscribesAndInvokes()
        {
            //Arrange:
            bool called = false;
            var bus = new EventBus();

            //Act:
            bus.SubscribeUnsafe(1, () => called = true);
            bus.Invoke(1);

            //Assert:
            Assert.IsTrue(called);
            Assert.IsTrue(bus.IsSubscribed(1));
        }

        [Test]
        public void SubscribeUnsafe_0Arg_DuplicateKey_ThrowsArgumentException()
        {
            //Arrange:
            var bus = new EventBus();
            bus.SubscribeUnsafe(1, () => { });

            //Act & Assert:
            Assert.Throws<ArgumentException>(() => bus.SubscribeUnsafe(1, () => { }));
        }

        [Test]
        public void SubscribeUnsafe_1Arg_SubscribesAndInvokes()
        {
            //Arrange:
            int received = 0;
            var bus = new EventBus();

            //Act:
            bus.SubscribeUnsafe<int>(1, v => received = v);
            bus.Invoke(1, 99);

            //Assert:
            Assert.AreEqual(99, received);
        }

        [Test]
        public void SubscribeUnsafe_2Args_SubscribesAndInvokes()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            var bus = new EventBus();

            //Act:
            bus.SubscribeUnsafe<int, string>(1, (a, b) =>
            {
                receivedA = a;
                receivedB = b;
            });
            bus.Invoke(1, 7, "hello");

            //Assert:
            Assert.AreEqual(7, receivedA);
            Assert.AreEqual("hello", receivedB);
        }

        [Test]
        public void SubscribeUnsafe_3Args_SubscribesAndInvokes()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            bool receivedC = false;
            var bus = new EventBus();

            //Act:
            bus.SubscribeUnsafe<int, string, bool>(1, (a, b, c) =>
            {
                receivedA = a;
                receivedB = b;
                receivedC = c;
            });
            bus.Invoke(1, 5, "world", true);

            //Assert:
            Assert.AreEqual(5, receivedA);
            Assert.AreEqual("world", receivedB);
            Assert.IsTrue(receivedC);
        }

        // ──────────────────────────────────────────────────────
        //  UnsubscribeUnsafe
        // ──────────────────────────────────────────────────────

        [Test]
        public void UnsubscribeUnsafe_0Arg_RemovesSubscriber()
        {
            //Arrange:
            bool called = false;
            var bus = new EventBus();
            Action action = () => called = true;
            bus.SubscribeUnsafe(1, action);

            //Act:
            bus.UnsubscribeUnsafe(1, action);
            bus.Invoke(1);

            //Assert:
            Assert.IsFalse(called);
            Assert.IsFalse(bus.IsSubscribed(1));
        }

        [Test]
        public void UnsubscribeUnsafe_0Arg_MissingKey_ThrowsKeyNotFoundException()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.Throws<KeyNotFoundException>(() => bus.UnsubscribeUnsafe(1, () => { }));
        }

        [Test]
        public void UnsubscribeUnsafe_1Arg_RemovesSubscriber()
        {
            //Arrange:
            int received = 0;
            var bus = new EventBus();
            Action<int> action = v => received = v;
            bus.SubscribeUnsafe<int>(1, action);

            //Act:
            bus.UnsubscribeUnsafe(1, action);
            bus.Invoke(1, 42);

            //Assert:
            Assert.AreEqual(0, received);
        }

        [Test]
        public void UnsubscribeUnsafe_2Args_RemovesSubscriber()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            var bus = new EventBus();
            Action<int, string> action = (a, b) =>
            {
                receivedA = a;
                receivedB = b;
            };
            bus.SubscribeUnsafe<int, string>(1, action);

            //Act:
            bus.UnsubscribeUnsafe(1, action);
            bus.Invoke(1, 7, "hello");

            //Assert:
            Assert.AreEqual(0, receivedA);
            Assert.IsNull(receivedB);
        }

        [Test]
        public void UnsubscribeUnsafe_3Args_RemovesSubscriber()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            bool receivedC = false;
            var bus = new EventBus();
            Action<int, string, bool> action = (a, b, c) =>
            {
                receivedA = a;
                receivedB = b;
                receivedC = c;
            };
            bus.SubscribeUnsafe<int, string, bool>(1, action);

            //Act:
            bus.UnsubscribeUnsafe(1, action);
            bus.Invoke(1, 5, "world", true);

            //Assert:
            Assert.AreEqual(0, receivedA);
            Assert.IsNull(receivedB);
            Assert.IsFalse(receivedC);
        }

        // ──────────────────────────────────────────────────────
        //  Reflection helpers to inspect internal EventTable state
        // ──────────────────────────────────────────────────────

        private static int GetEventTableCount(EventBus bus)
        {
            FieldInfo countField = typeof(EventBus).GetField("_count",
                BindingFlags.NonPublic | BindingFlags.Instance);
            return (int)countField.GetValue(bus);
        }

        private static int GetEventTableCapacity(EventBus bus)
        {
            FieldInfo capacityField = typeof(EventBus).GetField("_capacity",
                BindingFlags.NonPublic | BindingFlags.Instance);
            return (int)capacityField.GetValue(bus);
        }
    }
}
