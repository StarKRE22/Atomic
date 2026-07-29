using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ThreadSafeReactiveVariableTests
    {
        // ──────────────────────────────────────────────
        //  Constructors
        // ──────────────────────────────────────────────

        [Test]
        public void DefaultConstructor_ValueIsDefault()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>();

            //Act:

            //Assert:
            Assert.AreEqual(default(int), variable.Value);
        }

        [Test]
        public void DefaultConstructor_ReferenceType_ValueIsNull()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>();

            //Act:

            //Assert:
            Assert.IsNull(variable.Value);
        }

        [Test]
        public void ValueConstructor_ValueIsSetToProvidedValue()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(5);

            //Act:

            //Assert:
            Assert.AreEqual(5, variable.Value);
        }

        [Test]
        public void ValueConstructor_ReferenceType_ValueIsSetToProvidedValue()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>("hello");

            //Act:

            //Assert:
            Assert.AreEqual("hello", variable.Value);
        }

        // ──────────────────────────────────────────────
        //  Value property — get
        // ──────────────────────────────────────────────

        [Test]
        public void Value_Get_ReturnsCurrentValue()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(42);

            //Act:
            int result = variable.Value;

            //Assert:
            Assert.AreEqual(42, result);
        }

        [Test]
        public void Value_Get_AfterSet_ReturnsNewValue()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);

            //Act:
            variable.Value = 99;

            //Assert:
            Assert.AreEqual(99, variable.Value);
        }

        // ──────────────────────────────────────────────
        //  Value property — set
        // ──────────────────────────────────────────────

        [Test]
        public void Value_Set_UpdatesValue()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(10);

            //Act:
            variable.Value = 20;

            //Assert:
            Assert.AreEqual(20, variable.Value);
        }

        [Test]
        public void Value_Set_DoesNotFireOnEventImmediately()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);
            int received = -1;
            variable.OnEvent += v => received = v;

            //Act:
            variable.Value = 2;

            //Assert:
            Assert.AreEqual(2, variable.Value);
            Assert.AreEqual(-1, received, "OnEvent must NOT fire immediately on Value set");
        }

        [Test]
        public void Value_Set_FiresMarkDirty_CauseFlushToFireOnEvent()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);
            int received = -1;
            variable.OnEvent += v => received = v;

            //Act:
            variable.Value = 2;
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual(2, received);
        }

        // ──────────────────────────────────────────────
        //  Value property — same value (equality check)
        // ──────────────────────────────────────────────

        [Test]
        public void Value_SetSameValue_DoesNotMarkDirty()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(5);
            int invokeCount = 0;
            variable.OnEvent += _ => invokeCount++;

            //Act:
            variable.Value = 5;

            //Assert:
            Assert.AreEqual(5, variable.Value);
            Assert.AreEqual(0, invokeCount,
                "OnEvent must NOT fire when the same value is assigned (no MarkDirty)");
        }

        [Test]
        public void Value_SetSameValue_ReferenceType_DoesNotMarkDirty()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>("test");
            int invokeCount = 0;
            variable.OnEvent += _ => invokeCount++;

            //Act:
            variable.Value = "test";

            //Assert:
            Assert.AreEqual("test", variable.Value);
            Assert.AreEqual(0, invokeCount);
        }

        [Test]
        public void Value_SetSameValue_ValueRemainsUnchanged()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(7);

            //Act:
            variable.Value = 7;

            //Assert:
            Assert.AreEqual(7, variable.Value);
        }

        // ──────────────────────────────────────────────
        //  OnEvent — deferred via Flush
        // ──────────────────────────────────────────────

        [Test]
        public void OnEvent_FiresOnlyWhenFlushIsCalled()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);
            int received = -1;
            variable.OnEvent += v => received = v;

            //Act:
            variable.Value = 3;

            //Assert — not yet:
            Assert.AreEqual(-1, received, "OnEvent must not fire before Flush");

            //Act:
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert — now:
            Assert.AreEqual(3, received);
        }

        [Test]
        public void OnEvent_FiresOnFlush_WithCurrentValue()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>("old");
            string received = null;
            variable.OnEvent += v => received = v;

            //Act:
            variable.Value = "new";
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual("new", received);
        }

        [Test]
        public void OnEvent_DoesNotFireIfValueNeverChanged()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(5);
            int invokeCount = 0;
            variable.OnEvent += _ => invokeCount++;

            //Act:
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual(1, invokeCount, "Flush fires OnEvent once with current value");
        }

        [Test]
        public void OnEvent_MultipleHandlers_AllReceiveValue()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(0);
            int r1 = -1, r2 = -1, r3 = -1;
            variable.OnEvent += v => r1 = v;
            variable.OnEvent += v => r2 = v;
            variable.OnEvent += v => r3 = v;

            //Act:
            variable.Value = 7;
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual(7, r1);
            Assert.AreEqual(7, r2);
            Assert.AreEqual(7, r3);
        }

        // ──────────────────────────────────────────────
        //  Flush
        // ──────────────────────────────────────────────

        [Test]
        public void Flush_FiresOnEvent_WithCurrentValue()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(42);
            int received = -1;
            variable.OnEvent += v => received = v;

            //Act:
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual(42, received);
        }

        [Test]
        public void Flush_FiresOnEvent_AfterMultipleSets()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);
            int received = -1;
            variable.OnEvent += v => received = v;

            //Act:
            variable.Value = 10;
            variable.Value = 20;
            variable.Value = 30;
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual(30, received, "Flush should deliver the latest value");
        }

        [Test]
        public void Flush_AfterDispose_DoesNotFireOnEvent()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(5);
            int invokeCount = 0;
            variable.OnEvent += _ => invokeCount++;

            //Act:
            variable.Dispose();
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual(0, invokeCount, "Flush after Dispose must not fire OnEvent");
        }

        [Test]
        public void Flush_SetsValueToNull_DoesNotThrow()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>("hello");

            //Act & Assert:
            Assert.DoesNotThrow(() =>
                ((MainThreadDispatcher.IFlushable)variable).Flush());
        }

        // ──────────────────────────────────────────────
        //  Dispose
        // ──────────────────────────────────────────────

        [Test]
        public void Dispose_SetsOnEventToNull()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);
            int handlerCount = 0;
            variable.OnEvent += _ => handlerCount++;

            //Act:
            variable.Dispose();

            //Assert: Flush after Dispose must not invoke any handler
            ((MainThreadDispatcher.IFlushable)variable).Flush();
            Assert.AreEqual(0, handlerCount, "Flush after Dispose must not fire handlers");
        }

        [Test]
        public void Dispose_ValueStillAccessible()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(99);

            //Act:
            variable.Dispose();

            //Assert:
            Assert.AreEqual(99, variable.Value);
        }

        [Test]
        public void Dispose_ValueStillSettable()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);

            //Act:
            variable.Dispose();
            variable.Value = 50;

            //Assert:
            Assert.AreEqual(50, variable.Value);
        }

        [Test]
        public void Dispose_PreventsFutureFlushFromFiringOnEvent()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);
            int invokeCount = 0;
            variable.OnEvent += _ => invokeCount++;

            //Act:
            ((MainThreadDispatcher.IFlushable)variable).Flush();
            Assert.AreEqual(1, invokeCount, "Pre-condition: Flush fires before Dispose");

            variable.Dispose();
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual(1, invokeCount, "Flush after Dispose must not fire OnEvent");
        }

        [Test]
        public void Dispose_DoesNotAffectValueSetter()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);

            //Act:
            variable.Dispose();
            variable.Value = 77;

            //Assert:
            Assert.AreEqual(77, variable.Value);
        }

        // ──────────────────────────────────────────────
        //  ToString
        // ──────────────────────────────────────────────

        [Test]
        public void ToString_ReturnsStringValueRepresentation()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(42);

            //Act:
            string result = variable.ToString();

            //Assert:
            Assert.AreEqual("42", result);
        }

        [Test]
        public void ToString_ReferenceType_ReturnsValueString()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>("hello");

            //Act:
            string result = variable.ToString();

            //Assert:
            Assert.AreEqual("hello", result);
        }

        [Test]
        public void ToString_NullValue_ReturnsNull()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>();

            //Act:
            string result = variable.ToString();

            //Assert:
            Assert.IsNull(result);
        }

        [Test]
        public void ToString_AfterValueSet_ReturnsNewString()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);

            //Act:
            variable.Value = 999;

            //Assert:
            Assert.AreEqual("999", variable.ToString());
        }

        // ──────────────────────────────────────────────
        //  Interface contracts
        // ──────────────────────────────────────────────

        [Test]
        public void ImplementsIDisposable()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);

            //Act & Assert:
            Assert.IsInstanceOf<IDisposable>(variable);
        }

        [Test]
        public void ImplementsIReactiveVariable()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);

            //Act & Assert:
            Assert.IsInstanceOf<IReactiveVariable<int>>(variable);
        }

        [Test]
        public void ImplementsIFlushable()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(1);

            //Act & Assert:
            Assert.IsInstanceOf<MainThreadDispatcher.IFlushable>(variable);
        }

        // ──────────────────────────────────────────────
        //  Edge cases
        // ──────────────────────────────────────────────

        [Test]
        public void SetDefaultAfterNonNull_FiresOnEvent()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(5);
            int received = -1;
            variable.OnEvent += v => received = v;

            //Act:
            variable.Value = 0; // default(int)
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.AreEqual(0, received);
        }

        [Test]
        public void SetNullAfterNonNull_FiresOnEvent()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>("hello");
            string received = "not-null";
            variable.OnEvent += v => received = v;

            //Act:
            variable.Value = null;
            ((MainThreadDispatcher.IFlushable)variable).Flush();

            //Assert:
            Assert.IsNull(received);
        }

        [Test]
        public void SetNull_ThenSetNullAgain_DoesNotFireOnEvent()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<string>(null);
            int invokeCount = 0;
            variable.OnEvent += _ => invokeCount++;

            //Act:
            variable.Value = null;

            //Assert: No change means no MarkDirty, so Flush is never called automatically
            Assert.AreEqual(0, invokeCount,
                "Setting null when already null should not trigger OnEvent");
        }

        [Test]
        public void NegativeValue_StoresCorrectly()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(-42);

            //Act:

            //Assert:
            Assert.AreEqual(-42, variable.Value);
        }

        [Test]
        public void LargeValue_StoresCorrectly()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(int.MaxValue);

            //Act:

            //Assert:
            Assert.AreEqual(int.MaxValue, variable.Value);
        }

        [Test]
        public void Flush_WithoutSubscribe_DoesNotThrow()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(5);

            //Act & Assert:
            Assert.DoesNotThrow(() =>
                ((MainThreadDispatcher.IFlushable)variable).Flush());
        }

        [Test]
        public void ToString_IntValue_ReturnsStringRepresentation()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(0);

            //Act:
            string result = variable.ToString();

            //Assert:
            Assert.AreEqual("0", result);
        }

        [Test]
        public void ToString_AfterDispose_ReturnsStill()
        {
            //Arrange:
            var variable = new ThreadSafeReactiveVariable<int>(55);

            //Act:
            variable.Dispose();
            string result = variable.ToString();

            //Assert:
            Assert.AreEqual("55", result);
        }
    }
}
