using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace Atomic.Events
{
    [TestFixture]
    public sealed class ThreadSafeEventBusTests
    {
        // ──────────────────────────────────────────────────────
        //  Constructor
        // ──────────────────────────────────────────────────────

        [Test]
        public void Constructor_Default_DoesNotThrow()
        {
            //Arrange & Act & Assert:
            Assert.DoesNotThrow(() =>
            {
                var unused = new ThreadSafeEventBus();
            });
        }

        [Test]
        public void Constructor_WithInner_DoesNotThrow()
        {
            //Arrange & Act & Assert:
            var inner = new EventBus();
            Assert.DoesNotThrow(() =>
            {
                var unused = new ThreadSafeEventBus(inner);
            });
        }

        // ──────────────────────────────────────────────────────
        //  Subscribe
        // ──────────────────────────────────────────────────────

        [Test]
        public void Subscribe_IsSubscribedReturnsTrue()
        {
            //Arrange:
            var bus = new ThreadSafeEventBus();

            //Act:
            bus.Subscribe(1, () => { });

            //Assert:
            Assert.IsTrue(bus.IsSubscribed(1));
        }

        [Test]
        public void Subscribe_BeforeSubscribe_IsSubscribedReturnsFalse()
        {
            //Arrange:
            var bus = new ThreadSafeEventBus();

            //Act & Assert:
            Assert.IsFalse(bus.IsSubscribed(1));
        }

        // ──────────────────────────────────────────────────────
        //  Invoke (enqueues, does NOT fire immediately)
        // ──────────────────────────────────────────────────────

        [Test]
        public void Invoke_DoesNotFireImmediately()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe(1, () => callCount++);

            //Act:
            bus.Invoke(1);

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void Invoke_1Arg_DoesNotFireImmediately()
        {
            //Arrange:
            int received = 0;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe<int>(1, v => received = v);

            //Act:
            bus.Invoke(1, 42);

            //Assert:
            Assert.AreEqual(0, received);
        }

        [Test]
        public void Invoke_2Args_DoesNotFireImmediately()
        {
            //Arrange:
            int received = 0;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe<int, string>(1, (v, _) => received = v);

            //Act:
            bus.Invoke(1, 42, "hello");

            //Assert:
            Assert.AreEqual(0, received);
        }

        [Test]
        public void Invoke_3Args_DoesNotFireImmediately()
        {
            //Arrange:
            bool fired = false;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe<int, string, bool>(1, (_, _, _) => fired = true);

            //Act:
            bus.Invoke(1, 1, "x", true);

            //Assert:
            Assert.IsFalse(fired);
        }

        // ──────────────────────────────────────────────────────
        //  Flush
        // ──────────────────────────────────────────────────────

        [Test]
        public void Flush_FiresQueuedEvents()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe(1, () => callCount++);
            bus.Invoke(1);

            //Act:
            bus.Flush();

            //Assert:
            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void Flush_EmptyQueue_NoOpNoException()
        {
            //Arrange:
            var bus = new ThreadSafeEventBus();

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Flush());
        }

        [Test]
        public void Flush_1Arg_DeliversArg()
        {
            //Arrange:
            int received = 0;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe<int>(1, v => received = v);
            bus.Invoke(1, 42);

            //Act:
            bus.Flush();

            //Assert:
            Assert.AreEqual(42, received);
        }

        [Test]
        public void Flush_2Args_DeliversArgs()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe<int, string>(1, (a, b) =>
            {
                receivedA = a;
                receivedB = b;
            });
            bus.Invoke(1, 7, "hello");

            //Act:
            bus.Flush();

            //Assert:
            Assert.AreEqual(7, receivedA);
            Assert.AreEqual("hello", receivedB);
        }

        [Test]
        public void Flush_3Args_DeliversArgs()
        {
            //Arrange:
            int receivedA = 0;
            string receivedB = null;
            bool receivedC = false;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe<int, string, bool>(1, (a, b, c) =>
            {
                receivedA = a;
                receivedB = b;
                receivedC = c;
            });
            bus.Invoke(1, 5, "world", true);

            //Act:
            bus.Flush();

            //Assert:
            Assert.AreEqual(5, receivedA);
            Assert.AreEqual("world", receivedB);
            Assert.IsTrue(receivedC);
        }

        // ──────────────────────────────────────────────────────
        //  Multiple invokes before Flush — order preserved
        // ──────────────────────────────────────────────────────

        [Test]
        public void MultipleInvoke_BeforeFlush_AllDeliveredInOrder()
        {
            //Arrange:
            var received = new List<int>();
            var bus = new ThreadSafeEventBus();
            bus.Subscribe<int>(1, v => received.Add(v));

            //Act:
            bus.Invoke(1, 10);
            bus.Invoke(1, 20);
            bus.Invoke(1, 30);
            bus.Flush();

            //Assert:
            Assert.AreEqual(3, received.Count);
            Assert.AreEqual(10, received[0]);
            Assert.AreEqual(20, received[1]);
            Assert.AreEqual(30, received[2]);
        }

        [Test]
        public void MultipleInvoke_DifferentOverloads_AllDeliveredInOrder()
        {
            //Arrange:
            var log = new List<string>();
            var bus = new ThreadSafeEventBus();

            bus.Subscribe(10, () => log.Add("zero"));
            bus.Subscribe<int>(11, v => log.Add($"1:{v}"));
            bus.Subscribe<int, string>(12, (a, b) => log.Add($"2:{a},{b}"));
            bus.Subscribe<int, string, bool>(13, (a, b, c) => log.Add($"3:{a},{b},{c}"));

            //Act:
            bus.Invoke(10);
            bus.Invoke(11, 1);
            bus.Invoke(12, 2, "x");
            bus.Invoke(13, 3, "y", true);
            bus.Flush();

            //Assert:
            Assert.AreEqual(4, log.Count);
            Assert.AreEqual("zero", log[0]);
            Assert.AreEqual("1:1", log[1]);
            Assert.AreEqual("2:2,x", log[2]);
            Assert.AreEqual("3:3,y,True", log[3]);
        }

        // ──────────────────────────────────────────────────────
        //  Subscribe / Unsubscribe delegate to inner bus
        // ──────────────────────────────────────────────────────

        [Test]
        public void Unsubscribe_RemovesSubscriber()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();
            Action callback = () => callCount++;

            bus.Subscribe(1, callback);
            bus.Unsubscribe(1, callback);

            //Act:
            bus.Invoke(1);
            bus.Flush();

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void Unsubscribe_1Arg_RemovesCorrectSubscriber()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();
            Action<int> a = _ => callCount++;

            bus.Subscribe(1, a);
            bus.Unsubscribe(1, a);

            //Act:
            bus.Invoke(1, 42);
            bus.Flush();

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void Unsubscribe_2Args_RemovesCorrectSubscriber()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();
            Action<int, string> a = (_, _) => callCount++;

            bus.Subscribe(1, a);
            bus.Unsubscribe(1, a);

            //Act:
            bus.Invoke(1, 1, "x");
            bus.Flush();

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void Unsubscribe_3Args_RemovesCorrectSubscriber()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();
            Action<int, string, bool> a = (_, _, _) => callCount++;

            bus.Subscribe(1, a);
            bus.Unsubscribe(1, a);

            //Act:
            bus.Invoke(1, 1, "x", true);
            bus.Flush();

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void Unsubscribe_NonExistentAction_NoOpNoException()
        {
            //Arrange:
            var bus = new ThreadSafeEventBus();
            bus.Subscribe(1, () => { });

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Unsubscribe(1, () => { }));
        }

        [Test]
        public void Subscribe_MultipleSubscribers_AllInvokedOnFlush()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();

            //Act:
            bus.Subscribe(1, () => callCount++);
            bus.Subscribe(1, () => callCount++);
            bus.Invoke(1);
            bus.Flush();

            //Assert:
            Assert.AreEqual(2, callCount);
        }

        // ──────────────────────────────────────────────────────
        //  IsSubscribed through ThreadSafeEventBus
        // ──────────────────────────────────────────────────────

        [Test]
        public void IsSubscribed_AfterDisposeByKey_ReturnsFalse()
        {
            //Arrange:
            var bus = new ThreadSafeEventBus();
            bus.Subscribe(1, () => { });

            //Act:
            bool removed = bus.Dispose(1);

            //Assert:
            Assert.IsTrue(removed);
            Assert.IsFalse(bus.IsSubscribed(1));
        }

        [Test]
        public void DisposeByKey_NonExistentKey_ReturnsFalse()
        {
            //Arrange:
            var bus = new ThreadSafeEventBus();

            //Act:
            bool result = bus.Dispose(42);

            //Assert:
            Assert.IsFalse(result);
        }

        // ──────────────────────────────────────────────────────
        //  Dispose()
        // ──────────────────────────────────────────────────────

        [Test]
        public void Dispose_ClearsInnerBus()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe(1, () => callCount++);

            //Act:
            bus.Dispose();
            bus.Invoke(1);
            bus.Flush();

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void Dispose_ClearsQueue()
        {
            //Arrange:
            int callCount = 0;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe(1, () => callCount++);
            bus.Invoke(1);

            //Act:
            bus.Dispose();
            bus.Flush();

            //Assert:
            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void Dispose_DoesNotThrowOnEmptyBus()
        {
            //Arrange:
            var bus = new ThreadSafeEventBus();

            //Act & Assert:
            Assert.DoesNotThrow(() => bus.Dispose());
        }

        // ──────────────────────────────────────────────────────
        //  Cross-thread safety (producer / consumer)
        // ──────────────────────────────────────────────────────

        [Test]
        public void CrossThread_InvokeFromBackgroundThread_FlushOnMainThread()
        {
            //Arrange:
            var received = new List<int>();
            var bus = new ThreadSafeEventBus();
            bus.Subscribe<int>(1, v => received.Add(v));

            var done = new ManualResetEventSlim(false);
            int invokeCount = 0;
            const int total = 10;

            //Act:
            var producer = new Thread(() =>
            {
                for (int i = 0; i < total; i++)
                {
                    bus.Invoke(1, i);
                    Interlocked.Increment(ref invokeCount);
                }
                done.Set();
            });
            producer.Start();

            // Wait for all invocations to be enqueued
            done.Wait();
            bus.Flush();

            //Assert:
            Assert.AreEqual(total, invokeCount);
            Assert.AreEqual(total, received.Count);
            for (int i = 0; i < total; i++)
            {
                Assert.AreEqual(i, received[i]);
            }
        }

        [Test]
        public void CrossThread_MultipleProducers_EventsNotLost()
        {
            //Arrange:
            int totalCalls = 0;
            var bus = new ThreadSafeEventBus();
            bus.Subscribe(1, () => Interlocked.Increment(ref totalCalls));

            const int threadCount = 4;
            const int perThread = 100;
            var done = new CountdownEvent(threadCount);

            //Act:
            for (int t = 0; t < threadCount; t++)
            {
                var thread = new Thread(() =>
                {
                    for (int i = 0; i < perThread; i++)
                        bus.Invoke(1);
                    done.Signal();
                });
                thread.Start();
            }

            done.Wait();
            bus.Flush();

            //Assert:
            Assert.AreEqual(threadCount * perThread, totalCalls);
        }
    }
}
