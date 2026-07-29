using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    // ========================================================================
    // Command (no parameters)
    // ========================================================================
    [TestFixture]
    public sealed class CommandTests
    {
        // --- Constructor ---

        [Test]
        public void Constructor_DefaultState_CanInvokeReturnsTrue()
        {
            //Arrange:
            var command = new Command();

            //Act:
            bool result = command.CanInvoke();

            //Assert:
            Assert.IsTrue(result);
        }

        // --- CanInvoke ---

        [Test]
        public void CanInvoke_NoConditions_ReturnsTrue()
        {
            //Arrange:
            var command = new Command();

            //Act:
            bool result = command.CanInvoke();

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithPassingCondition_ReturnsTrue()
        {
            //Arrange:
            var command = new Command();
            command.AddCondition(() => true);

            //Act:
            bool result = command.CanInvoke();

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithFailingCondition_ReturnsFalse()
        {
            //Arrange:
            var command = new Command();
            command.AddCondition(() => false);

            //Act:
            bool result = command.CanInvoke();

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsAllPass_ReturnsTrue()
        {
            //Arrange:
            var command = new Command();
            command.AddCondition(() => true);
            command.AddCondition(() => true);
            command.AddCondition(() => true);

            //Act:
            bool result = command.CanInvoke();

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsOneFails_ReturnsFalse()
        {
            //Arrange:
            var command = new Command();
            command.AddCondition(() => true);
            command.AddCondition(() => false);
            command.AddCondition(() => true);

            //Act:
            bool result = command.CanInvoke();

            //Assert:
            Assert.IsFalse(result);
        }

        // --- TryInvoke ---

        [Test]
        public void TryInvoke_NoConditionsFiresActionAndOnEvent_ReturnsTrue()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command();
            command.AddAction(() => actionFired = true);
            command.OnEvent += () => onEventFired = true;

            //Act:
            bool result = command.TryInvoke();

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(actionFired);
            Assert.IsTrue(onEventFired);
        }

        [Test]
        public void TryInvoke_FailingConditionDoesNotFire_ReturnsFalse()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command();
            command.AddCondition(() => false);
            command.AddAction(() => actionFired = true);
            command.OnEvent += () => onEventFired = true;

            //Act:
            bool result = command.TryInvoke();

            //Assert:
            Assert.IsFalse(result);
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void TryInvoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.TryInvoke());
        }

        // --- Invoke ---

        [Test]
        public void Invoke_NoConditionsFiresActionAndOnEvent()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command();
            command.AddAction(() => actionFired = true);
            command.OnEvent += () => onEventFired = true;

            //Act:
            command.Invoke();

            //Assert:
            Assert.IsTrue(actionFired);
            Assert.IsTrue(onEventFired);
        }

        [Test]
        public void Invoke_FailingConditionDoesNotFire()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command();
            command.AddCondition(() => false);
            command.AddAction(() => actionFired = true);
            command.OnEvent += () => onEventFired = true;

            //Act:
            command.Invoke();

            //Assert:
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void Invoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.Invoke());
        }

        // --- AddCondition ---

        [Test]
        public void AddCondition_AffectsCanInvoke()
        {
            //Arrange:
            var command = new Command();

            //Act:
            command.AddCondition(() => false);

            //Assert:
            Assert.IsFalse(command.CanInvoke());
        }

        [Test]
        public void AddCondition_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command();

            //Act:
            ICommand result = command.AddCondition(() => true);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void AddCondition_ResizeBeyondInitialCapacityOfFour()
        {
            //Arrange:
            var command = new Command();

            //Act:
            command.AddCondition(() => true);
            command.AddCondition(() => true);
            command.AddCondition(() => true);
            command.AddCondition(() => true);
            command.AddCondition(() => true);

            //Assert:
            Assert.IsTrue(command.CanInvoke());
        }

        [Test]
        public void AddCondition_ResizeBeyondCapacity_OneConditionFails()
        {
            //Arrange:
            var command = new Command();
            command.AddCondition(() => true);
            command.AddCondition(() => true);
            command.AddCondition(() => true);
            command.AddCondition(() => true);
            command.AddCondition(() => false);

            //Act:
            bool result = command.CanInvoke();

            //Assert:
            Assert.IsFalse(result);
        }

        // --- RemoveCondition ---

        [Test]
        public void RemoveCondition_RemovesSpecificCondition()
        {
            //Arrange:
            Func<bool> failingCondition = () => false;
            var command = new Command();
            command.AddCondition(failingCondition);
            Assert.IsFalse(command.CanInvoke());

            //Act:
            command.RemoveCondition(failingCondition);

            //Assert:
            Assert.IsTrue(command.CanInvoke());
        }

        [Test]
        public void RemoveCondition_RemovingNonExistentIsNoOp()
        {
            //Arrange:
            Func<bool> failingCondition = () => false;
            var command = new Command();
            command.AddCondition(failingCondition);
            Assert.IsFalse(command.CanInvoke());

            //Act:
            command.RemoveCondition(() => false);

            //Assert:
            Assert.IsFalse(command.CanInvoke());
        }

        [Test]
        public void RemoveCondition_ReturnsThisForChaining()
        {
            //Arrange:
            Func<bool> condition = () => true;
            var command = new Command();
            command.AddCondition(condition);

            //Act:
            ICommand result = command.RemoveCondition(condition);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void RemoveCondition_FromMultipleConditionsOnlyRemovesTarget()
        {
            //Arrange:
            Func<bool> condition1 = () => true;
            Func<bool> condition2 = () => false;
            Func<bool> condition3 = () => true;

            var command = new Command();
            command.AddCondition(condition1);
            command.AddCondition(condition2);
            command.AddCondition(condition3);

            //Act:
            command.RemoveCondition(condition2);

            //Assert:
            Assert.IsTrue(command.CanInvoke());
        }

        // --- AddAction ---

        [Test]
        public void AddAction_ActionFiresOnInvoke()
        {
            //Arrange:
            bool wasFired = false;
            var command = new Command();
            command.AddAction(() => wasFired = true);

            //Act:
            command.Invoke();

            //Assert:
            Assert.IsTrue(wasFired);
        }

        [Test]
        public void AddAction_MultipleActionsAllFire()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command();
            command.AddAction(() => fireCount++);
            command.AddAction(() => fireCount++);
            command.AddAction(() => fireCount++);

            //Act:
            command.Invoke();

            //Assert:
            Assert.AreEqual(3, fireCount);
        }

        [Test]
        public void AddAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command();

            //Act:
            ICommand result = command.AddAction(() => { });

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- RemoveAction ---

        [Test]
        public void RemoveAction_RemovesSpecificAction()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command();
            Action action = () => fireCount++;
            command.AddAction(action);
            command.AddAction(() => fireCount++);

            //Act:
            command.RemoveAction(action);
            command.Invoke();

            //Assert:
            Assert.AreEqual(1, fireCount);
        }

        [Test]
        public void RemoveAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command();
            Action action = () => { };
            command.AddAction(action);

            //Act:
            ICommand result = command.RemoveAction(action);

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- OnEvent ---

        [Test]
        public void OnEvent_FiresOnTryInvokeSuccess()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command();
            command.OnEvent += () => onEventFired = true;

            //Act:
            command.TryInvoke();

            //Assert:
            Assert.IsTrue(onEventFired);
        }

        [Test]
        public void OnEvent_DoesNotFireWhenCanInvokeFails()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command();
            command.AddCondition(() => false);
            command.OnEvent += () => onEventFired = true;

            //Act:
            command.TryInvoke();

            //Assert:
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void OnEvent_FiresOnInvokeSuccess()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command();
            command.OnEvent += () => onEventFired = true;

            //Act:
            command.Invoke();

            //Assert:
            Assert.IsTrue(onEventFired);
        }

        [Test]
        public void OnEvent_DoesNotFireOnInvokeWhenCanInvokeFails()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command();
            command.AddCondition(() => false);
            command.OnEvent += () => onEventFired = true;

            //Act:
            command.Invoke();

            //Assert:
            Assert.IsFalse(onEventFired);
        }

        // --- Interface compliance ---

        [Test]
        public void Command_ImplementsICommand()
        {
            //Arrange & Act:
            ICommand command = new Command();

            //Assert:
            Assert.IsNotNull(command);
        }
    }

    // ========================================================================
    // Command<T1>
    // ========================================================================
    [TestFixture]
    public sealed class CommandT1Tests
    {
        // --- Constructor ---

        [Test]
        public void Constructor_DefaultState_CanInvokeReturnsTrue()
        {
            //Arrange:
            var command = new Command<int>();

            //Act:
            bool result = command.CanInvoke(42);

            //Assert:
            Assert.IsTrue(result);
        }

        // --- CanInvoke ---

        [Test]
        public void CanInvoke_NoConditions_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<string>();

            //Act:
            bool result = command.CanInvoke("hello");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithPassingCondition_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int>();
            command.AddCondition(v => v > 0);

            //Act:
            bool result = command.CanInvoke(5);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithFailingCondition_ReturnsFalse()
        {
            //Arrange:
            var command = new Command<int>();
            command.AddCondition(v => v > 10);

            //Act:
            bool result = command.CanInvoke(5);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsAllPass_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int>();
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v < 100);
            command.AddCondition(v => v != 50);

            //Act:
            bool result = command.CanInvoke(42);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsOneFails_ReturnsFalse()
        {
            //Arrange:
            var command = new Command<int>();
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v < 10);
            command.AddCondition(v => v != 5);

            //Act:
            bool result = command.CanInvoke(42);

            //Assert:
            Assert.IsFalse(result);
        }

        // --- TryInvoke ---

        [Test]
        public void TryInvoke_NoConditionsFiresActionAndOnEvent_ReturnsTrue()
        {
            //Arrange:
            int capturedValue = 0;
            bool onEventFired = false;

            var command = new Command<int>();
            command.AddAction(v => capturedValue = v);
            command.OnEvent += v => { onEventFired = true; capturedValue = v; };

            //Act:
            bool result = command.TryInvoke(77);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(onEventFired);
            Assert.AreEqual(77, capturedValue);
        }

        [Test]
        public void TryInvoke_FailingConditionDoesNotFire_ReturnsFalse()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command<int>();
            command.AddCondition(v => v > 100);
            command.AddAction(v => actionFired = true);
            command.OnEvent += v => onEventFired = true;

            //Act:
            bool result = command.TryInvoke(50);

            //Assert:
            Assert.IsFalse(result);
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void TryInvoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command<int>();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.TryInvoke(42));
        }

        // --- Invoke ---

        [Test]
        public void Invoke_NoConditionsFiresActionAndOnEvent()
        {
            //Arrange:
            int capturedValue = 0;
            bool onEventFired = false;

            var command = new Command<int>();
            command.AddAction(v => capturedValue = v);
            command.OnEvent += v => { onEventFired = true; };

            //Act:
            command.Invoke(77);

            //Assert:
            Assert.IsTrue(onEventFired);
            Assert.AreEqual(77, capturedValue);
        }

        [Test]
        public void Invoke_FailingConditionDoesNotFire()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command<int>();
            command.AddCondition(v => false);
            command.AddAction(v => actionFired = true);
            command.OnEvent += v => onEventFired = true;

            //Act:
            command.Invoke(42);

            //Assert:
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void Invoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command<int>();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.Invoke(42));
        }

        // --- AddCondition ---

        [Test]
        public void AddCondition_AffectsCanInvoke()
        {
            //Arrange:
            var command = new Command<string>();

            //Act:
            command.AddCondition(s => s != null);

            //Assert:
            Assert.IsTrue(command.CanInvoke("test"));
            Assert.IsFalse(command.CanInvoke(null));
        }

        [Test]
        public void AddCondition_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int>();

            //Act:
            ICommand<int> result = command.AddCondition(v => true);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void AddCondition_ResizeBeyondInitialCapacityOfFour()
        {
            //Arrange:
            var command = new Command<int>();

            //Act:
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v > 0);

            //Assert:
            Assert.IsTrue(command.CanInvoke(5));
        }

        [Test]
        public void AddCondition_ResizeBeyondCapacity_OneConditionFails()
        {
            //Arrange:
            var command = new Command<int>();
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v > 0);
            command.AddCondition(v => v > 100);

            //Act:
            bool result = command.CanInvoke(5);

            //Assert:
            Assert.IsFalse(result);
        }

        // --- RemoveCondition ---

        [Test]
        public void RemoveCondition_RemovesSpecificCondition()
        {
            //Arrange:
            Func<int, bool> failingCondition = v => v > 100;
            var command = new Command<int>();
            command.AddCondition(failingCondition);
            Assert.IsFalse(command.CanInvoke(50));

            //Act:
            command.RemoveCondition(failingCondition);

            //Assert:
            Assert.IsTrue(command.CanInvoke(50));
        }

        [Test]
        public void RemoveCondition_RemovingNonExistentIsNoOp()
        {
            //Arrange:
            Func<int, bool> failingCondition = v => v > 100;
            var command = new Command<int>();
            command.AddCondition(failingCondition);

            //Act:
            command.RemoveCondition(v => v > 200);

            //Assert:
            Assert.IsFalse(command.CanInvoke(50));
        }

        [Test]
        public void RemoveCondition_ReturnsThisForChaining()
        {
            //Arrange:
            Func<int, bool> condition = v => true;
            var command = new Command<int>();
            command.AddCondition(condition);

            //Act:
            ICommand<int> result = command.RemoveCondition(condition);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void RemoveCondition_FromMultipleConditionsOnlyRemovesTarget()
        {
            //Arrange:
            Func<int, bool> condition1 = v => v > 0;
            Func<int, bool> condition2 = v => v > 100;
            Func<int, bool> condition3 = v => v != 50;

            var command = new Command<int>();
            command.AddCondition(condition1);
            command.AddCondition(condition2);
            command.AddCondition(condition3);

            //Act:
            command.RemoveCondition(condition2);

            //Assert:
            Assert.IsTrue(command.CanInvoke(42));
        }

        // --- AddAction ---

        [Test]
        public void AddAction_ActionFiresOnInvoke()
        {
            //Arrange:
            int capturedValue = 0;
            var command = new Command<int>();
            command.AddAction(v => capturedValue = v);

            //Act:
            command.Invoke(99);

            //Assert:
            Assert.AreEqual(99, capturedValue);
        }

        [Test]
        public void AddAction_MultipleActionsAllFire()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command<int>();
            command.AddAction(v => fireCount++);
            command.AddAction(v => fireCount++);
            command.AddAction(v => fireCount++);

            //Act:
            command.Invoke(1);

            //Assert:
            Assert.AreEqual(3, fireCount);
        }

        [Test]
        public void AddAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int>();

            //Act:
            ICommand<int> result = command.AddAction(v => { });

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- RemoveAction ---

        [Test]
        public void RemoveAction_RemovesSpecificAction()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command<int>();
            Action<int> action = v => fireCount++;
            command.AddAction(action);
            command.AddAction(v => fireCount++);

            //Act:
            command.RemoveAction(action);
            command.Invoke(1);

            //Assert:
            Assert.AreEqual(1, fireCount);
        }

        [Test]
        public void RemoveAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int>();
            Action<int> action = v => { };
            command.AddAction(action);

            //Act:
            ICommand<int> result = command.RemoveAction(action);

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- OnEvent ---

        [Test]
        public void OnEvent_FiresOnTryInvokeSuccess()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command<int>();
            command.OnEvent += v => onEventFired = true;

            //Act:
            command.TryInvoke(42);

            //Assert:
            Assert.IsTrue(onEventFired);
        }

        [Test]
        public void OnEvent_DoesNotFireWhenCanInvokeFails()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command<int>();
            command.AddCondition(v => false);
            command.OnEvent += v => onEventFired = true;

            //Act:
            command.TryInvoke(42);

            //Assert:
            Assert.IsFalse(onEventFired);
        }

        // --- Parameter passing ---

        [Test]
        public void TryInvoke_ParameterPassedToCondition()
        {
            //Arrange:
            int capturedParam = -1;
            var command = new Command<int>();
            command.AddCondition(v => { capturedParam = v; return true; });

            //Act:
            command.TryInvoke(42);

            //Assert:
            Assert.AreEqual(42, capturedParam);
        }

        [Test]
        public void TryInvoke_ParameterPassedToAction()
        {
            //Arrange:
            int capturedParam = -1;
            var command = new Command<int>();
            command.AddAction(v => capturedParam = v);

            //Act:
            command.TryInvoke(42);

            //Assert:
            Assert.AreEqual(42, capturedParam);
        }

        [Test]
        public void TryInvoke_ParameterPassedToOnEvent()
        {
            //Arrange:
            int capturedParam = -1;
            var command = new Command<int>();
            command.OnEvent += v => capturedParam = v;

            //Act:
            command.TryInvoke(42);

            //Assert:
            Assert.AreEqual(42, capturedParam);
        }

        // --- Interface compliance ---

        [Test]
        public void Command_ImplementsICommandT1()
        {
            //Arrange & Act:
            ICommand<int> command = new Command<int>();

            //Assert:
            Assert.IsNotNull(command);
        }
    }

    // ========================================================================
    // Command<T1, T2>
    // ========================================================================
    [TestFixture]
    public sealed class CommandT2Tests
    {
        // --- Constructor ---

        [Test]
        public void Constructor_DefaultState_CanInvokeReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string>();

            //Act:
            bool result = command.CanInvoke(1, "hello");

            //Assert:
            Assert.IsTrue(result);
        }

        // --- CanInvoke ---

        [Test]
        public void CanInvoke_NoConditions_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string>();

            //Act:
            bool result = command.CanInvoke(1, "hello");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithPassingCondition_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string>();
            command.AddCondition((i, s) => i > 0 && s != null);

            //Act:
            bool result = command.CanInvoke(5, "test");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithFailingCondition_ReturnsFalse()
        {
            //Arrange:
            var command = new Command<int, string>();
            command.AddCondition((i, s) => i > 100);

            //Act:
            bool result = command.CanInvoke(5, "test");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsAllPass_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string>();
            command.AddCondition((i, s) => i > 0);
            command.AddCondition((i, s) => s != null);
            command.AddCondition((i, s) => s.Length > 0);

            //Act:
            bool result = command.CanInvoke(5, "hello");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsOneFails_ReturnsFalse()
        {
            //Arrange:
            var command = new Command<int, string>();
            command.AddCondition((i, s) => i > 0);
            command.AddCondition((i, s) => s == null);
            command.AddCondition((i, s) => s.Length > 0);

            //Act:
            bool result = command.CanInvoke(5, "hello");

            //Assert:
            Assert.IsFalse(result);
        }

        // --- TryInvoke ---

        [Test]
        public void TryInvoke_NoConditionsFiresActionAndOnEvent_ReturnsTrue()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool onEventFired = false;

            var command = new Command<int, string>();
            command.AddAction((i, s) => { capturedI = i; capturedS = s; });
            command.OnEvent += (i, s) => { onEventFired = true; };

            //Act:
            bool result = command.TryInvoke(77, "world");

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(onEventFired);
            Assert.AreEqual(77, capturedI);
            Assert.AreEqual("world", capturedS);
        }

        [Test]
        public void TryInvoke_FailingConditionDoesNotFire_ReturnsFalse()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command<int, string>();
            command.AddCondition((i, s) => false);
            command.AddAction((i, s) => actionFired = true);
            command.OnEvent += (i, s) => onEventFired = true;

            //Act:
            bool result = command.TryInvoke(50, "test");

            //Assert:
            Assert.IsFalse(result);
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void TryInvoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command<int, string>();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.TryInvoke(1, "a"));
        }

        // --- Invoke ---

        [Test]
        public void Invoke_NoConditionsFiresActionAndOnEvent()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool onEventFired = false;

            var command = new Command<int, string>();
            command.AddAction((i, s) => { capturedI = i; capturedS = s; });
            command.OnEvent += (i, s) => { onEventFired = true; };

            //Act:
            command.Invoke(77, "world");

            //Assert:
            Assert.IsTrue(onEventFired);
            Assert.AreEqual(77, capturedI);
            Assert.AreEqual("world", capturedS);
        }

        [Test]
        public void Invoke_FailingConditionDoesNotFire()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command<int, string>();
            command.AddCondition((i, s) => false);
            command.AddAction((i, s) => actionFired = true);
            command.OnEvent += (i, s) => onEventFired = true;

            //Act:
            command.Invoke(42, "test");

            //Assert:
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void Invoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command<int, string>();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.Invoke(1, "a"));
        }

        // --- AddCondition ---

        [Test]
        public void AddCondition_AffectsCanInvoke()
        {
            //Arrange:
            var command = new Command<int, string>();

            //Act:
            command.AddCondition((i, s) => i > 0 && s != null);

            //Assert:
            Assert.IsTrue(command.CanInvoke(5, "test"));
            Assert.IsFalse(command.CanInvoke(0, "test"));
            Assert.IsFalse(command.CanInvoke(5, null));
        }

        [Test]
        public void AddCondition_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string>();

            //Act:
            ICommand<int, string> result = command.AddCondition((i, s) => true);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void AddCondition_ResizeBeyondInitialCapacityOfFour()
        {
            //Arrange:
            var command = new Command<int, string>();

            //Act:
            command.AddCondition((i, s) => true);
            command.AddCondition((i, s) => true);
            command.AddCondition((i, s) => true);
            command.AddCondition((i, s) => true);
            command.AddCondition((i, s) => true);

            //Assert:
            Assert.IsTrue(command.CanInvoke(1, "a"));
        }

        [Test]
        public void AddCondition_ResizeBeyondCapacity_OneConditionFails()
        {
            //Arrange:
            var command = new Command<int, string>();
            command.AddCondition((i, s) => true);
            command.AddCondition((i, s) => true);
            command.AddCondition((i, s) => true);
            command.AddCondition((i, s) => true);
            command.AddCondition((i, s) => false);

            //Act:
            bool result = command.CanInvoke(1, "a");

            //Assert:
            Assert.IsFalse(result);
        }

        // --- RemoveCondition ---

        [Test]
        public void RemoveCondition_RemovesSpecificCondition()
        {
            //Arrange:
            Func<int, string, bool> failingCondition = (i, s) => false;
            var command = new Command<int, string>();
            command.AddCondition(failingCondition);

            //Act:
            command.RemoveCondition(failingCondition);

            //Assert:
            Assert.IsTrue(command.CanInvoke(1, "a"));
        }

        [Test]
        public void RemoveCondition_RemovingNonExistentIsNoOp()
        {
            //Arrange:
            Func<int, string, bool> failingCondition = (i, s) => false;
            var command = new Command<int, string>();
            command.AddCondition(failingCondition);

            //Act:
            command.RemoveCondition((i, s) => false);

            //Assert:
            Assert.IsFalse(command.CanInvoke(1, "a"));
        }

        [Test]
        public void RemoveCondition_ReturnsThisForChaining()
        {
            //Arrange:
            Func<int, string, bool> condition = (i, s) => true;
            var command = new Command<int, string>();
            command.AddCondition(condition);

            //Act:
            ICommand<int, string> result = command.RemoveCondition(condition);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void RemoveCondition_FromMultipleConditionsOnlyRemovesTarget()
        {
            //Arrange:
            Func<int, string, bool> c1 = (i, s) => i > 0;
            Func<int, string, bool> c2 = (i, s) => false;
            Func<int, string, bool> c3 = (i, s) => s != null;

            var command = new Command<int, string>();
            command.AddCondition(c1);
            command.AddCondition(c2);
            command.AddCondition(c3);

            //Act:
            command.RemoveCondition(c2);

            //Assert:
            Assert.IsTrue(command.CanInvoke(42, "hello"));
        }

        // --- AddAction ---

        [Test]
        public void AddAction_ActionFiresOnInvoke()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            var command = new Command<int, string>();
            command.AddAction((i, s) => { capturedI = i; capturedS = s; });

            //Act:
            command.Invoke(99, "hello");

            //Assert:
            Assert.AreEqual(99, capturedI);
            Assert.AreEqual("hello", capturedS);
        }

        [Test]
        public void AddAction_MultipleActionsAllFire()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command<int, string>();
            command.AddAction((i, s) => fireCount++);
            command.AddAction((i, s) => fireCount++);
            command.AddAction((i, s) => fireCount++);

            //Act:
            command.Invoke(1, "a");

            //Assert:
            Assert.AreEqual(3, fireCount);
        }

        [Test]
        public void AddAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string>();

            //Act:
            ICommand<int, string> result = command.AddAction((i, s) => { });

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- RemoveAction ---

        [Test]
        public void RemoveAction_RemovesSpecificAction()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command<int, string>();
            Action<int, string> action = (i, s) => fireCount++;
            command.AddAction(action);
            command.AddAction((i, s) => fireCount++);

            //Act:
            command.RemoveAction(action);
            command.Invoke(1, "a");

            //Assert:
            Assert.AreEqual(1, fireCount);
        }

        [Test]
        public void RemoveAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string>();
            Action<int, string> action = (i, s) => { };
            command.AddAction(action);

            //Act:
            ICommand<int, string> result = command.RemoveAction(action);

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- OnEvent ---

        [Test]
        public void OnEvent_FiresOnTryInvokeSuccess()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command<int, string>();
            command.OnEvent += (i, s) => onEventFired = true;

            //Act:
            command.TryInvoke(42, "hello");

            //Assert:
            Assert.IsTrue(onEventFired);
        }

        [Test]
        public void OnEvent_DoesNotFireWhenCanInvokeFails()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command<int, string>();
            command.AddCondition((i, s) => false);
            command.OnEvent += (i, s) => onEventFired = true;

            //Act:
            command.TryInvoke(42, "hello");

            //Assert:
            Assert.IsFalse(onEventFired);
        }

        // --- Parameter passing ---

        [Test]
        public void TryInvoke_ParametersPassedToCondition()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            var command = new Command<int, string>();
            command.AddCondition((i, s) => { capturedI = i; capturedS = s; return true; });

            //Act:
            command.TryInvoke(42, "hello");

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
        }

        [Test]
        public void TryInvoke_ParametersPassedToAction()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            var command = new Command<int, string>();
            command.AddAction((i, s) => { capturedI = i; capturedS = s; });

            //Act:
            command.TryInvoke(42, "hello");

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
        }

        [Test]
        public void TryInvoke_ParametersPassedToOnEvent()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            var command = new Command<int, string>();
            command.OnEvent += (i, s) => { capturedI = i; capturedS = s; };

            //Act:
            command.TryInvoke(42, "hello");

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
        }

        // --- Interface compliance ---

        [Test]
        public void Command_ImplementsICommandT2()
        {
            //Arrange & Act:
            ICommand<int, string> command = new Command<int, string>();

            //Assert:
            Assert.IsNotNull(command);
        }
    }

    // ========================================================================
    // Command<T1, T2, T3>
    // ========================================================================
    [TestFixture]
    public sealed class CommandT3Tests
    {
        // --- Constructor ---

        [Test]
        public void Constructor_DefaultState_CanInvokeReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string, bool>();

            //Act:
            bool result = command.CanInvoke(1, "hello", true);

            //Assert:
            Assert.IsTrue(result);
        }

        // --- CanInvoke ---

        [Test]
        public void CanInvoke_NoConditions_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string, bool>();

            //Act:
            bool result = command.CanInvoke(1, "hello", true);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithPassingCondition_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) => i > 0 && s != null && b);

            //Act:
            bool result = command.CanInvoke(5, "test", true);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithFailingCondition_ReturnsFalse()
        {
            //Arrange:
            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) => b == false);

            //Act:
            bool result = command.CanInvoke(5, "test", true);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsAllPass_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) => i > 0);
            command.AddCondition((i, s, b) => s != null);
            command.AddCondition((i, s, b) => b);

            //Act:
            bool result = command.CanInvoke(5, "hello", true);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsOneFails_ReturnsFalse()
        {
            //Arrange:
            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) => i > 0);
            command.AddCondition((i, s, b) => b == false);
            command.AddCondition((i, s, b) => s != null);

            //Act:
            bool result = command.CanInvoke(5, "hello", true);

            //Assert:
            Assert.IsFalse(result);
        }

        // --- TryInvoke ---

        [Test]
        public void TryInvoke_NoConditionsFiresActionAndOnEvent_ReturnsTrue()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            bool onEventFired = false;

            var command = new Command<int, string, bool>();
            command.AddAction((i, s, b) => { capturedI = i; capturedS = s; capturedB = b; });
            command.OnEvent += (i, s, b) => { onEventFired = true; };

            //Act:
            bool result = command.TryInvoke(77, "world", true);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(onEventFired);
            Assert.AreEqual(77, capturedI);
            Assert.AreEqual("world", capturedS);
            Assert.IsTrue(capturedB);
        }

        [Test]
        public void TryInvoke_FailingConditionDoesNotFire_ReturnsFalse()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) => false);
            command.AddAction((i, s, b) => actionFired = true);
            command.OnEvent += (i, s, b) => onEventFired = true;

            //Act:
            bool result = command.TryInvoke(50, "test", true);

            //Assert:
            Assert.IsFalse(result);
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void TryInvoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command<int, string, bool>();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.TryInvoke(1, "a", true));
        }

        // --- Invoke ---

        [Test]
        public void Invoke_NoConditionsFiresActionAndOnEvent()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            bool onEventFired = false;

            var command = new Command<int, string, bool>();
            command.AddAction((i, s, b) => { capturedI = i; capturedS = s; capturedB = b; });
            command.OnEvent += (i, s, b) => { onEventFired = true; };

            //Act:
            command.Invoke(77, "world", true);

            //Assert:
            Assert.IsTrue(onEventFired);
            Assert.AreEqual(77, capturedI);
            Assert.AreEqual("world", capturedS);
            Assert.IsTrue(capturedB);
        }

        [Test]
        public void Invoke_FailingConditionDoesNotFire()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) => false);
            command.AddAction((i, s, b) => actionFired = true);
            command.OnEvent += (i, s, b) => onEventFired = true;

            //Act:
            command.Invoke(42, "test", false);

            //Assert:
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void Invoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command<int, string, bool>();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.Invoke(1, "a", true));
        }

        // --- AddCondition ---

        [Test]
        public void AddCondition_AffectsCanInvoke()
        {
            //Arrange:
            var command = new Command<int, string, bool>();

            //Act:
            command.AddCondition((i, s, b) => b);

            //Assert:
            Assert.IsTrue(command.CanInvoke(1, "a", true));
            Assert.IsFalse(command.CanInvoke(1, "a", false));
        }

        [Test]
        public void AddCondition_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string, bool>();

            //Act:
            ICommand<int, string, bool> result = command.AddCondition((i, s, b) => true);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void AddCondition_ResizeBeyondInitialCapacityOfFour()
        {
            //Arrange:
            var command = new Command<int, string, bool>();

            //Act:
            command.AddCondition((i, s, b) => true);
            command.AddCondition((i, s, b) => true);
            command.AddCondition((i, s, b) => true);
            command.AddCondition((i, s, b) => true);
            command.AddCondition((i, s, b) => true);

            //Assert:
            Assert.IsTrue(command.CanInvoke(1, "a", true));
        }

        [Test]
        public void AddCondition_ResizeBeyondCapacity_OneConditionFails()
        {
            //Arrange:
            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) => true);
            command.AddCondition((i, s, b) => true);
            command.AddCondition((i, s, b) => true);
            command.AddCondition((i, s, b) => true);
            command.AddCondition((i, s, b) => false);

            //Act:
            bool result = command.CanInvoke(1, "a", true);

            //Assert:
            Assert.IsFalse(result);
        }

        // --- RemoveCondition ---

        [Test]
        public void RemoveCondition_RemovesSpecificCondition()
        {
            //Arrange:
            Func<int, string, bool, bool> failingCondition = (i, s, b) => false;
            var command = new Command<int, string, bool>();
            command.AddCondition(failingCondition);

            //Act:
            command.RemoveCondition(failingCondition);

            //Assert:
            Assert.IsTrue(command.CanInvoke(1, "a", true));
        }

        [Test]
        public void RemoveCondition_RemovingNonExistentIsNoOp()
        {
            //Arrange:
            Func<int, string, bool, bool> failingCondition = (i, s, b) => false;
            var command = new Command<int, string, bool>();
            command.AddCondition(failingCondition);

            //Act:
            command.RemoveCondition((i, s, b) => false);

            //Assert:
            Assert.IsFalse(command.CanInvoke(1, "a", true));
        }

        [Test]
        public void RemoveCondition_ReturnsThisForChaining()
        {
            //Arrange:
            Func<int, string, bool, bool> condition = (i, s, b) => true;
            var command = new Command<int, string, bool>();
            command.AddCondition(condition);

            //Act:
            ICommand<int, string, bool> result = command.RemoveCondition(condition);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void RemoveCondition_FromMultipleConditionsOnlyRemovesTarget()
        {
            //Arrange:
            Func<int, string, bool, bool> c1 = (i, s, b) => i > 0;
            Func<int, string, bool, bool> c2 = (i, s, b) => false;
            Func<int, string, bool, bool> c3 = (i, s, b) => b;

            var command = new Command<int, string, bool>();
            command.AddCondition(c1);
            command.AddCondition(c2);
            command.AddCondition(c3);

            //Act:
            command.RemoveCondition(c2);

            //Assert:
            Assert.IsTrue(command.CanInvoke(42, "hello", true));
        }

        // --- AddAction ---

        [Test]
        public void AddAction_ActionFiresOnInvoke()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            var command = new Command<int, string, bool>();
            command.AddAction((i, s, b) => { capturedI = i; capturedS = s; capturedB = b; });

            //Act:
            command.Invoke(99, "hello", true);

            //Assert:
            Assert.AreEqual(99, capturedI);
            Assert.AreEqual("hello", capturedS);
            Assert.IsTrue(capturedB);
        }

        [Test]
        public void AddAction_MultipleActionsAllFire()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command<int, string, bool>();
            command.AddAction((i, s, b) => fireCount++);
            command.AddAction((i, s, b) => fireCount++);
            command.AddAction((i, s, b) => fireCount++);

            //Act:
            command.Invoke(1, "a", true);

            //Assert:
            Assert.AreEqual(3, fireCount);
        }

        [Test]
        public void AddAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string, bool>();

            //Act:
            ICommand<int, string, bool> result = command.AddAction((i, s, b) => { });

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- RemoveAction ---

        [Test]
        public void RemoveAction_RemovesSpecificAction()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command<int, string, bool>();
            Action<int, string, bool> action = (i, s, b) => fireCount++;
            command.AddAction(action);
            command.AddAction((i, s, b) => fireCount++);

            //Act:
            command.RemoveAction(action);
            command.Invoke(1, "a", true);

            //Assert:
            Assert.AreEqual(1, fireCount);
        }

        [Test]
        public void RemoveAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string, bool>();
            Action<int, string, bool> action = (i, s, b) => { };
            command.AddAction(action);

            //Act:
            ICommand<int, string, bool> result = command.RemoveAction(action);

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- OnEvent ---

        [Test]
        public void OnEvent_FiresOnTryInvokeSuccess()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command<int, string, bool>();
            command.OnEvent += (i, s, b) => onEventFired = true;

            //Act:
            command.TryInvoke(42, "hello", true);

            //Assert:
            Assert.IsTrue(onEventFired);
        }

        [Test]
        public void OnEvent_DoesNotFireWhenCanInvokeFails()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) => false);
            command.OnEvent += (i, s, b) => onEventFired = true;

            //Act:
            command.TryInvoke(42, "hello", true);

            //Assert:
            Assert.IsFalse(onEventFired);
        }

        // --- Parameter passing ---

        [Test]
        public void TryInvoke_ParametersPassedToCondition()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            var command = new Command<int, string, bool>();
            command.AddCondition((i, s, b) =>
            {
                capturedI = i;
                capturedS = s;
                capturedB = b;
                return true;
            });

            //Act:
            command.TryInvoke(42, "hello", true);

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
            Assert.IsTrue(capturedB);
        }

        [Test]
        public void TryInvoke_ParametersPassedToAction()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            var command = new Command<int, string, bool>();
            command.AddAction((i, s, b) => { capturedI = i; capturedS = s; capturedB = b; });

            //Act:
            command.TryInvoke(42, "hello", true);

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
            Assert.IsTrue(capturedB);
        }

        [Test]
        public void TryInvoke_ParametersPassedToOnEvent()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            var command = new Command<int, string, bool>();
            command.OnEvent += (i, s, b) => { capturedI = i; capturedS = s; capturedB = b; };

            //Act:
            command.TryInvoke(42, "hello", true);

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
            Assert.IsTrue(capturedB);
        }

        // --- Interface compliance ---

        [Test]
        public void Command_ImplementsICommandT3()
        {
            //Arrange & Act:
            ICommand<int, string, bool> command = new Command<int, string, bool>();

            //Assert:
            Assert.IsNotNull(command);
        }
    }

    // ========================================================================
    // Command<T1, T2, T3, T4>
    // ========================================================================
    [TestFixture]
    public sealed class CommandT4Tests
    {
        // --- Constructor ---

        [Test]
        public void Constructor_DefaultState_CanInvokeReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();

            //Act:
            bool result = command.CanInvoke(1, "hello", true, 1.5f);

            //Assert:
            Assert.IsTrue(result);
        }

        // --- CanInvoke ---

        [Test]
        public void CanInvoke_NoConditions_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();

            //Act:
            bool result = command.CanInvoke(1, "hello", true, 1.5f);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithPassingCondition_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) => i > 0 && s != null && b && f > 0f);

            //Act:
            bool result = command.CanInvoke(5, "test", true, 2.5f);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_WithFailingCondition_ReturnsFalse()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) => f > 100f);

            //Act:
            bool result = command.CanInvoke(5, "test", true, 1.5f);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsAllPass_ReturnsTrue()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) => i > 0);
            command.AddCondition((i, s, b, f) => s != null);
            command.AddCondition((i, s, b, f) => b);
            command.AddCondition((i, s, b, f) => f > 0f);

            //Act:
            bool result = command.CanInvoke(5, "hello", true, 1.5f);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void CanInvoke_MultipleConditionsOneFails_ReturnsFalse()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) => i > 0);
            command.AddCondition((i, s, b, f) => f > 100f);
            command.AddCondition((i, s, b, f) => b);

            //Act:
            bool result = command.CanInvoke(5, "hello", true, 1.5f);

            //Assert:
            Assert.IsFalse(result);
        }

        // --- TryInvoke ---

        [Test]
        public void TryInvoke_NoConditionsFiresActionAndOnEvent_ReturnsTrue()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            float capturedF = -1f;
            bool onEventFired = false;

            var command = new Command<int, string, bool, float>();
            command.AddAction((i, s, b, f) => { capturedI = i; capturedS = s; capturedB = b; capturedF = f; });
            command.OnEvent += (i, s, b, f) => { onEventFired = true; };

            //Act:
            bool result = command.TryInvoke(77, "world", true, 3.14f);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(onEventFired);
            Assert.AreEqual(77, capturedI);
            Assert.AreEqual("world", capturedS);
            Assert.IsTrue(capturedB);
            Assert.AreEqual(3.14f, capturedF);
        }

        [Test]
        public void TryInvoke_FailingConditionDoesNotFire_ReturnsFalse()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) => false);
            command.AddAction((i, s, b, f) => actionFired = true);
            command.OnEvent += (i, s, b, f) => onEventFired = true;

            //Act:
            bool result = command.TryInvoke(50, "test", true, 1.0f);

            //Assert:
            Assert.IsFalse(result);
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void TryInvoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.TryInvoke(1, "a", true, 1.0f));
        }

        // --- Invoke ---

        [Test]
        public void Invoke_NoConditionsFiresActionAndOnEvent()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            float capturedF = -1f;
            bool onEventFired = false;

            var command = new Command<int, string, bool, float>();
            command.AddAction((i, s, b, f) => { capturedI = i; capturedS = s; capturedB = b; capturedF = f; });
            command.OnEvent += (i, s, b, f) => { onEventFired = true; };

            //Act:
            command.Invoke(77, "world", true, 3.14f);

            //Assert:
            Assert.IsTrue(onEventFired);
            Assert.AreEqual(77, capturedI);
            Assert.AreEqual("world", capturedS);
            Assert.IsTrue(capturedB);
            Assert.AreEqual(3.14f, capturedF);
        }

        [Test]
        public void Invoke_FailingConditionDoesNotFire()
        {
            //Arrange:
            bool actionFired = false;
            bool onEventFired = false;

            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) => false);
            command.AddAction((i, s, b, f) => actionFired = true);
            command.OnEvent += (i, s, b, f) => onEventFired = true;

            //Act:
            command.Invoke(42, "test", false, 1.0f);

            //Assert:
            Assert.IsFalse(actionFired);
            Assert.IsFalse(onEventFired);
        }

        [Test]
        public void Invoke_NoActionSet_DoesNotThrow()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();

            //Act & Assert:
            Assert.DoesNotThrow(() => command.Invoke(1, "a", true, 1.0f));
        }

        // --- AddCondition ---

        [Test]
        public void AddCondition_AffectsCanInvoke()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();

            //Act:
            command.AddCondition((i, s, b, f) => f > 10f);

            //Assert:
            Assert.IsTrue(command.CanInvoke(1, "a", true, 20f));
            Assert.IsFalse(command.CanInvoke(1, "a", true, 5f));
        }

        [Test]
        public void AddCondition_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();

            //Act:
            ICommand<int, string, bool, float> result = command.AddCondition((i, s, b, f) => true);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void AddCondition_ResizeBeyondInitialCapacityOfFour()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();

            //Act:
            command.AddCondition((i, s, b, f) => true);
            command.AddCondition((i, s, b, f) => true);
            command.AddCondition((i, s, b, f) => true);
            command.AddCondition((i, s, b, f) => true);
            command.AddCondition((i, s, b, f) => true);

            //Assert:
            Assert.IsTrue(command.CanInvoke(1, "a", true, 1f));
        }

        [Test]
        public void AddCondition_ResizeBeyondCapacity_OneConditionFails()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) => true);
            command.AddCondition((i, s, b, f) => true);
            command.AddCondition((i, s, b, f) => true);
            command.AddCondition((i, s, b, f) => true);
            command.AddCondition((i, s, b, f) => false);

            //Act:
            bool result = command.CanInvoke(1, "a", true, 1f);

            //Assert:
            Assert.IsFalse(result);
        }

        // --- RemoveCondition ---

        [Test]
        public void RemoveCondition_RemovesSpecificCondition()
        {
            //Arrange:
            Func<int, string, bool, float, bool> failingCondition = (i, s, b, f) => false;
            var command = new Command<int, string, bool, float>();
            command.AddCondition(failingCondition);

            //Act:
            command.RemoveCondition(failingCondition);

            //Assert:
            Assert.IsTrue(command.CanInvoke(1, "a", true, 1f));
        }

        [Test]
        public void RemoveCondition_RemovingNonExistentIsNoOp()
        {
            //Arrange:
            Func<int, string, bool, float, bool> failingCondition = (i, s, b, f) => false;
            var command = new Command<int, string, bool, float>();
            command.AddCondition(failingCondition);

            //Act:
            command.RemoveCondition((i, s, b, f) => false);

            //Assert:
            Assert.IsFalse(command.CanInvoke(1, "a", true, 1f));
        }

        [Test]
        public void RemoveCondition_ReturnsThisForChaining()
        {
            //Arrange:
            Func<int, string, bool, float, bool> condition = (i, s, b, f) => true;
            var command = new Command<int, string, bool, float>();
            command.AddCondition(condition);

            //Act:
            ICommand<int, string, bool, float> result = command.RemoveCondition(condition);

            //Assert:
            Assert.AreSame(command, result);
        }

        [Test]
        public void RemoveCondition_FromMultipleConditionsOnlyRemovesTarget()
        {
            //Arrange:
            Func<int, string, bool, float, bool> c1 = (i, s, b, f) => i > 0;
            Func<int, string, bool, float, bool> c2 = (i, s, b, f) => false;
            Func<int, string, bool, float, bool> c3 = (i, s, b, f) => b;

            var command = new Command<int, string, bool, float>();
            command.AddCondition(c1);
            command.AddCondition(c2);
            command.AddCondition(c3);

            //Act:
            command.RemoveCondition(c2);

            //Assert:
            Assert.IsTrue(command.CanInvoke(42, "hello", true, 1.5f));
        }

        // --- AddAction ---

        [Test]
        public void AddAction_ActionFiresOnInvoke()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            float capturedF = -1f;
            var command = new Command<int, string, bool, float>();
            command.AddAction((i, s, b, f) => { capturedI = i; capturedS = s; capturedB = b; capturedF = f; });

            //Act:
            command.Invoke(99, "hello", true, 2.5f);

            //Assert:
            Assert.AreEqual(99, capturedI);
            Assert.AreEqual("hello", capturedS);
            Assert.IsTrue(capturedB);
            Assert.AreEqual(2.5f, capturedF);
        }

        [Test]
        public void AddAction_MultipleActionsAllFire()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command<int, string, bool, float>();
            command.AddAction((i, s, b, f) => fireCount++);
            command.AddAction((i, s, b, f) => fireCount++);
            command.AddAction((i, s, b, f) => fireCount++);

            //Act:
            command.Invoke(1, "a", true, 1f);

            //Assert:
            Assert.AreEqual(3, fireCount);
        }

        [Test]
        public void AddAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();

            //Act:
            ICommand<int, string, bool, float> result = command.AddAction((i, s, b, f) => { });

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- RemoveAction ---

        [Test]
        public void RemoveAction_RemovesSpecificAction()
        {
            //Arrange:
            int fireCount = 0;
            var command = new Command<int, string, bool, float>();
            Action<int, string, bool, float> action = (i, s, b, f) => fireCount++;
            command.AddAction(action);
            command.AddAction((i, s, b, f) => fireCount++);

            //Act:
            command.RemoveAction(action);
            command.Invoke(1, "a", true, 1f);

            //Assert:
            Assert.AreEqual(1, fireCount);
        }

        [Test]
        public void RemoveAction_ReturnsThisForChaining()
        {
            //Arrange:
            var command = new Command<int, string, bool, float>();
            Action<int, string, bool, float> action = (i, s, b, f) => { };
            command.AddAction(action);

            //Act:
            ICommand<int, string, bool, float> result = command.RemoveAction(action);

            //Assert:
            Assert.AreSame(command, result);
        }

        // --- OnEvent ---

        [Test]
        public void OnEvent_FiresOnTryInvokeSuccess()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command<int, string, bool, float>();
            command.OnEvent += (i, s, b, f) => onEventFired = true;

            //Act:
            command.TryInvoke(42, "hello", true, 1.5f);

            //Assert:
            Assert.IsTrue(onEventFired);
        }

        [Test]
        public void OnEvent_DoesNotFireWhenCanInvokeFails()
        {
            //Arrange:
            bool onEventFired = false;
            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) => false);
            command.OnEvent += (i, s, b, f) => onEventFired = true;

            //Act:
            command.TryInvoke(42, "hello", true, 1.5f);

            //Assert:
            Assert.IsFalse(onEventFired);
        }

        // --- Parameter passing ---

        [Test]
        public void TryInvoke_ParametersPassedToCondition()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            float capturedF = -1f;
            var command = new Command<int, string, bool, float>();
            command.AddCondition((i, s, b, f) =>
            {
                capturedI = i;
                capturedS = s;
                capturedB = b;
                capturedF = f;
                return true;
            });

            //Act:
            command.TryInvoke(42, "hello", true, 3.14f);

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
            Assert.IsTrue(capturedB);
            Assert.AreEqual(3.14f, capturedF);
        }

        [Test]
        public void TryInvoke_ParametersPassedToAction()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            float capturedF = -1f;
            var command = new Command<int, string, bool, float>();
            command.AddAction((i, s, b, f) => { capturedI = i; capturedS = s; capturedB = b; capturedF = f; });

            //Act:
            command.TryInvoke(42, "hello", true, 3.14f);

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
            Assert.IsTrue(capturedB);
            Assert.AreEqual(3.14f, capturedF);
        }

        [Test]
        public void TryInvoke_ParametersPassedToOnEvent()
        {
            //Arrange:
            int capturedI = -1;
            string capturedS = null;
            bool capturedB = false;
            float capturedF = -1f;
            var command = new Command<int, string, bool, float>();
            command.OnEvent += (i, s, b, f) => { capturedI = i; capturedS = s; capturedB = b; capturedF = f; };

            //Act:
            command.TryInvoke(42, "hello", true, 3.14f);

            //Assert:
            Assert.AreEqual(42, capturedI);
            Assert.AreEqual("hello", capturedS);
            Assert.IsTrue(capturedB);
            Assert.AreEqual(3.14f, capturedF);
        }

        // --- Interface compliance ---

        [Test]
        public void Command_ImplementsICommandT4()
        {
            //Arrange & Act:
            ICommand<int, string, bool, float> command = new Command<int, string, bool, float>();

            //Assert:
            Assert.IsNotNull(command);
        }
    }
}
