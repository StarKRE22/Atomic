using System;
using NUnit.Framework;

namespace Atomic.Events
{
    [TestFixture]
    public sealed class EventBusTests_Extended
    {
        // ──────────────────────────────────────────────────────
        //  Zero-arg Subscribe / Invoke
        // ──────────────────────────────────────────────────────

        [Test]
        public void Subscribe_MultipleSubscribers_AllInvoked()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            Action callback = () => callCount++;

            //Act:
            bus.Subscribe(1, callback);
            bus.Subscribe(1, callback);
            bus.Subscribe(1, callback);
            bus.Invoke(1);

            //Assert:
            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void Unsubscribe_RemovesSpecificSubscriber_OthersRemain()
        {
            //Arrange:
            int callCountA = 0;
            int callCountB = 0;
            var bus = new EventBus();
            Action a = () => callCountA++;
            Action b = () => callCountB++;

            bus.Subscribe(1, a);
            bus.Subscribe(1, b);

            //Act:
            bus.Unsubscribe(1, a);
            bus.Invoke(1);

            //Assert:
            Assert.AreEqual(0, callCountA);
            Assert.AreEqual(1, callCountB);
        }

        [Test]
        public void Unsubscribe_NonExistentAction_NoOpNoException()
        {
            //Arrange:
            var bus = new EventBus();
            bus.Subscribe(1, () => { });

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Unsubscribe(1, () => { }));
        }

        [Test]
        public void Unsubscribe_KeyNeverSubscribed_NoOpNoException()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Unsubscribe(99, () => { }));
        }

        [Test]
        public void Invoke_NonSubscribedKey_NoOpNoException()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Invoke(42));
        }

        [Test]
        public void Subscribe_SameDelegateTwice_InvocationListDoubles()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            Action callback = () => callCount++;

            //Act:
            bus.Subscribe(1, callback);
            bus.Subscribe(1, callback);
            bus.Invoke(1);

            //Assert:
            // Delegate.Combine appends the same delegate, producing a multicast
            // with two entries — invoking fires callback twice.
            Assert.AreEqual(2, callCount);
        }

        [Test]
        public void ReSubscribe_AfterUnsubscribe_Works()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            Action callback = () => callCount++;

            bus.Subscribe(1, callback);
            bus.Unsubscribe(1, callback);

            //Act:
            bus.Subscribe(1, callback);
            bus.Invoke(1);

            //Assert:
            Assert.AreEqual(1, callCount);
        }

        // ──────────────────────────────────────────────────────
        //  IsSubscribed
        // ──────────────────────────────────────────────────────

        [Test]
        public void IsSubscribed_BeforeSubscribe_ReturnsFalse()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.IsFalse(bus.IsSubscribed(1));
        }

        [Test]
        public void IsSubscribed_AfterSubscribe_ReturnsTrue()
        {
            //Arrange:
            var bus = new EventBus();

            //Act:
            bus.Subscribe(1, () => { });

            //Assert:
            Assert.IsTrue(bus.IsSubscribed(1));
        }

        [Test]
        public void IsSubscribed_AfterLastSubscriberRemoved_ReturnsFalse()
        {
            //Arrange:
            var bus = new EventBus();
            Action callback = () => { };
            bus.Subscribe(1, callback);

            //Act:
            bus.Unsubscribe(1, callback);

            //Assert:
            Assert.IsFalse(bus.IsSubscribed(1));
        }

        // ──────────────────────────────────────────────────────
        //  Subscribe<T> / Invoke<T> — 1 parameter
        // ──────────────────────────────────────────────────────

        [Test]
        public void Subscribe_1Arg_SubscriberReceivesArg()
        {
            //Arrange:
            int received = 0;
            var bus = new EventBus();

            //Act:
            bus.Subscribe<int>(1, v => received = v);
            bus.Invoke(1, 42);

            //Assert:
            Assert.AreEqual(42, received);
        }

        [Test]
        public void Unsubscribe_1Arg_RemovesCorrectSubscriber()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            Action<int> a = v => callCount++;

            bus.Subscribe(1, a);
            bus.Subscribe<int>(1, v => { });

            //Act:
            bus.Unsubscribe(1, a);
            bus.Invoke(1, 10);

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void Unsubscribe_1Arg_NonExistentAction_NoOp()
        {
            //Arrange:
            var bus = new EventBus();
            bus.Subscribe<int>(1, _ => { });

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Unsubscribe<int>(1, _ => { }));
        }

        [Test]
        public void Invoke_1Arg_NonSubscribedKey_NoOp()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Invoke(99, "hello"));
        }

        [Test]
        public void Subscribe_1Arg_SameDelegateTwice_InvocationListDoubles()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            Action<int> callback = _ => callCount++;

            //Act:
            bus.Subscribe(1, callback);
            bus.Subscribe(1, callback);
            bus.Invoke(1, 0);

            //Assert:
            // Delegate.Combine appends the same delegate, producing a multicast
            // with two entries — invoking fires callback twice.
            Assert.AreEqual(2, callCount);
        }

        [Test]
        public void Subscribe_1Arg_MultipleSubscribers_AllInvoked()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();

            //Act:
            bus.Subscribe<int>(1, _ => callCount++);
            bus.Subscribe<int>(1, _ => callCount++);
            bus.Invoke(1, 0);

            //Assert:
            Assert.AreEqual(2, callCount);
        }

        [Test]
        public void IsSubscribed_1Arg_RemovedLastSubscription_ReturnsFalse()
        {
            //Arrange:
            var bus = new EventBus();
            Action<int> callback = _ => { };
            bus.Subscribe(1, callback);

            //Act:
            bus.Unsubscribe(1, callback);

            //Assert:
            Assert.IsFalse(bus.IsSubscribed(1));
        }

        // ──────────────────────────────────────────────────────
        //  Subscribe<T1,T2> / Invoke<T1,T2> — 2 parameters
        // ──────────────────────────────────────────────────────

        [Test]
        public void Subscribe_2Args_SubscribersReceiveArgs()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            var bus = new EventBus();

            //Act:
            bus.Subscribe<int, string>(1, (a, b) =>
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
        public void Unsubscribe_2Args_RemovesCorrectSubscriber()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            Action<int, string> a = (_, _) => callCount++;

            bus.Subscribe(1, a);
            bus.Subscribe<int, string>(1, (_, _) => callCount++);

            //Act:
            bus.Unsubscribe(1, a);
            bus.Invoke(1, 0, "");

            //Assert:
            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Unsubscribe_2Args_NonExistentAction_NoOp()
        {
            //Arrange:
            var bus = new EventBus();
            bus.Subscribe<int, string>(1, (_, _) => { });

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Unsubscribe<int, string>(1, (_, _) => { }));
        }

        [Test]
        public void Invoke_2Args_NonSubscribedKey_NoOp()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Invoke(99, 1, "x"));
        }

        [Test]
        public void Subscribe_2Args_MultipleSubscribers_AllInvoked()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();

            //Act:
            bus.Subscribe<int, string>(1, (_, _) => callCount++);
            bus.Subscribe<int, string>(1, (_, _) => callCount++);
            bus.Subscribe<int, string>(1, (_, _) => callCount++);
            bus.Invoke(1, 0, "");

            //Assert:
            Assert.AreEqual(3, callCount);
        }

        // ──────────────────────────────────────────────────────
        //  Subscribe<T1,T2,T3> / Invoke<T1,T2,T3> — 3 params
        // ──────────────────────────────────────────────────────

        [Test]
        public void Subscribe_3Args_SubscribersReceiveArgs()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            bool receivedC = false;
            var bus = new EventBus();

            //Act:
            bus.Subscribe<int, string, bool>(1, (a, b, c) =>
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

        [Test]
        public void Unsubscribe_3Args_RemovesCorrectSubscriber()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            Action<int, string, bool> a = (_, _, _) => callCount++;

            bus.Subscribe(1, a);
            bus.Subscribe<int, string, bool>(1, (_, _, _) => callCount++);

            //Act:
            bus.Unsubscribe(1, a);
            bus.Invoke(1, 0, "", false);

            //Assert:
            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Unsubscribe_3Args_NonExistentAction_NoOp()
        {
            //Arrange:
            var bus = new EventBus();
            bus.Subscribe<int, string, bool>(1, (_, _, _) => { });

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Unsubscribe<int, string, bool>(1, (_, _, _) => { }));
        }

        [Test]
        public void Invoke_3Args_NonSubscribedKey_NoOp()
        {
            //Arrange:
            var bus = new EventBus();

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Invoke(99, 1, "x", true));
        }

        [Test]
        public void Subscribe_3Args_MultipleSubscribers_AllInvoked()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();

            //Act:
            bus.Subscribe<int, string, bool>(1, (_, _, _) => callCount++);
            bus.Subscribe<int, string, bool>(1, (_, _, _) => callCount++);
            bus.Invoke(1, 0, "", false);

            //Assert:
            Assert.AreEqual(2, callCount);
        }

        [Test]
        public void Subscribe_3Args_SameDelegateTwice_InvocationListDoubles()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            Action<int, string, bool> callback = (_, _, _) => callCount++;

            //Act:
            bus.Subscribe(1, callback);
            bus.Subscribe(1, callback);
            bus.Invoke(1, 0, "", false);

            //Assert:
            // Delegate.Combine appends the same delegate, producing a multicast
            // with two entries — invoking fires callback twice.
            Assert.AreEqual(2, callCount);
        }

        // ──────────────────────────────────────────────────────
        //  Dispose(int key)
        // ──────────────────────────────────────────────────────

        [Test]
        public void DisposeByKey_ExistingKey_ReturnsTrue()
        {
            //Arrange:
            var bus = new EventBus();
            bus.Subscribe(1, () => { });

            //Act:
            bool result = bus.Dispose(1);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void DisposeByKey_NonExistentKey_ReturnsFalse()
        {
            //Arrange:
            var bus = new EventBus();

            //Act:
            bool result = bus.Dispose(42);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void DisposeByKey_RemovesSubscription_NoLongerInvoked()
        {
            //Arrange:
            int callCount = 0;
            var bus = new EventBus();
            bus.Subscribe(1, () => callCount++);

            //Act:
            bus.Dispose(1);
            bus.Invoke(1);

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void DisposeByKey_IsSubscribedBecomesFalse()
        {
            //Arrange:
            var bus = new EventBus();
            bus.Subscribe(1, () => { });

            //Act:
            bus.Dispose(1);

            //Assert:
            Assert.IsFalse(bus.IsSubscribed(1));
        }

        [Test]
        public void DisposeByKey_OtherKeysUnaffected()
        {
            //Arrange:
            int key1Calls = 0;
            int key2Calls = 0;
            var bus = new EventBus();
            bus.Subscribe(1, () => key1Calls++);
            bus.Subscribe(2, () => key2Calls++);

            //Act:
            bus.Dispose(1);
            bus.Invoke(2);

            //Assert:
            Assert.AreEqual(0, key1Calls);
            Assert.AreEqual(1, key2Calls);
        }

        // ──────────────────────────────────────────────────────
        //  Dispose() — parameterless
        // ──────────────────────────────────────────────────────

        [Test]
        public void Dispose_ClearsAllSubscriptions()
        {
            //Arrange:
            int key1Calls = 0;
            int key2Calls = 0;
            int key3Calls = 0;
            var bus = new EventBus();
            bus.Subscribe(1, () => key1Calls++);
            bus.Subscribe(2, () => key2Calls++);
            bus.Subscribe(3, () => key3Calls++);

            //Act:
            bus.Dispose();
            bus.Invoke(1);
            bus.Invoke(2);
            bus.Invoke(3);

            //Assert:
            Assert.AreEqual(0, key1Calls);
            Assert.AreEqual(0, key2Calls);
            Assert.AreEqual(0, key3Calls);
        }

        [Test]
        public void Dispose_IsSubscribedAllFalse()
        {
            //Arrange:
            var bus = new EventBus();
            bus.Subscribe(1, () => { });
            bus.Subscribe(2, () => { });

            //Act:
            bus.Dispose();

            //Assert:
            Assert.IsFalse(bus.IsSubscribed(1));
            Assert.IsFalse(bus.IsSubscribed(2));
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
        //  Cross-key isolation
        // ──────────────────────────────────────────────────────

        [Test]
        public void Invoke_DifferentKeys_DoesNotCrossFire()
        {
            //Arrange:
            int key1Calls = 0;
            int key2Calls = 0;
            var bus = new EventBus();
            bus.Subscribe(1, () => key1Calls++);
            bus.Subscribe(2, () => key2Calls++);

            //Act:
            bus.Invoke(1);

            //Assert:
            Assert.AreEqual(1, key1Calls);
            Assert.AreEqual(0, key2Calls);
        }

        [Test]
        public void Subscribe_DifferentKeys_IndependentTracking()
        {
            //Arrange:
            var bus = new EventBus();

            //Act:
            bus.Subscribe(1, () => { });
            bus.Subscribe(2, () => { });

            //Assert:
            Assert.IsTrue(bus.IsSubscribed(1));
            Assert.IsTrue(bus.IsSubscribed(2));
            bus.Dispose(1);
            Assert.IsFalse(bus.IsSubscribed(1));
            Assert.IsTrue(bus.IsSubscribed(2));
        }
    }
}
