using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed class EventTests
    {
        [Test]
        public void Subscribe()
        {
            //Arrange:
            var wasEvent1 = false;
            var wasEvent2 = false;

            var eventBase = new Event();
            eventBase.OnEvent += () => wasEvent1 = true;
            eventBase.Subscribe(() => wasEvent2 = true);

            //Act:
            eventBase.Invoke();

            //Assert:
            Assert.IsTrue(wasEvent1);
            Assert.IsTrue(wasEvent2);
        }

        [Test]
        public void Unsubscribe()
        {
            var wasEvent1 = false;
            var wasEvent2 = false;

            void OnEventRaisen1()
            {
                wasEvent1 = true;
            }

            void OnEventRaisen2()
            {
                wasEvent2 = true;
            }

            //Arrange:
            var eventBase = new Event();
            eventBase.OnEvent += OnEventRaisen1;
            eventBase.Subscribe(OnEventRaisen2);

            eventBase.Invoke();
            Assert.IsTrue(wasEvent1);
            Assert.IsTrue(wasEvent2);

            //Act:
            wasEvent1 = false;
            wasEvent2 = false;

            eventBase.OnEvent -= OnEventRaisen1;
            eventBase.Unsubscribe(OnEventRaisen2);
            eventBase.Invoke();

            //Assert:
            Assert.IsFalse(wasEvent1);
            Assert.IsFalse(wasEvent2);
        }

        [Test]
        public void Dispose()
        {
            var wasEvent1 = false;
            var wasEvent2 = false;

            void OnEventRaisen1()
            {
                wasEvent1 = true;
            }

            void OnEventRaisen2()
            {
                wasEvent2 = true;
            }

            //Arrange:
            var eventBase = new Event();
            eventBase.OnEvent += OnEventRaisen1;
            eventBase.Subscribe(OnEventRaisen2);

            eventBase.Invoke();
            Assert.IsTrue(wasEvent1);
            Assert.IsTrue(wasEvent2);

            //Act:
            wasEvent1 = false;
            wasEvent2 = false;

            eventBase.Dispose();
            eventBase.Invoke();

            //Assert:
            Assert.IsFalse(wasEvent1);
            Assert.IsFalse(wasEvent2);
        }
    }


    public sealed class EventT1Tests
    {
        [Test]
        public void Subscribe()
        {
            //Arrange:
            object wasEvent1 = null;
            object wasEvent2 = null;

            var eventBase = new Event<object>();
            eventBase.OnEvent += a => wasEvent1 = a;
            eventBase.Subscribe(a => wasEvent2 = a);

            //Act:
            eventBase.Invoke("Vasya");

            //Assert:
            Assert.AreEqual("Vasya", wasEvent1);
            Assert.AreEqual("Vasya", wasEvent2);
        }

        [Test]
        public void Unsubscribe()
        {
            object wasEvent1 = null;
            object wasEvent2 = null;

            void OnEventRaisen1(object o)
            {
                wasEvent1 = o;
            }

            void OnEventRaisen2(object o)
            {
                wasEvent2 = o;
            }

            //Arrange:
            var eventBase = new Event<object>();
            eventBase.OnEvent += OnEventRaisen1;
            eventBase.Subscribe(OnEventRaisen2);

            eventBase.Invoke("Vasya");
            Assert.AreEqual("Vasya", wasEvent1);
            Assert.AreEqual("Vasya", wasEvent2);

            //Act:
            wasEvent1 = null;
            wasEvent2 = null;

            eventBase.OnEvent -= OnEventRaisen1;
            eventBase.Unsubscribe(OnEventRaisen2);
            eventBase.Invoke("Petya");

            //Assert:
            Assert.AreNotEqual("Petya", wasEvent1);
            Assert.AreNotEqual("Petya", wasEvent2);
            Assert.IsNull(wasEvent1);
            Assert.IsNull(wasEvent2);
        }

        [Test]
        public void Dispose()
        {
            string wasEvent1 = null;
            string wasEvent2 = null;

            void OnEventRaisen1(string s)
            {
                wasEvent1 = s;
            }

            void OnEventRaisen2(string s)
            {
                wasEvent2 = s;
            }

            //Arrange:
            var eventBase = new Event<string>();
            eventBase.OnEvent += OnEventRaisen1;
            eventBase.Subscribe(OnEventRaisen2);

            eventBase.Invoke("Vasya");
            Assert.AreEqual("Vasya", wasEvent1);
            Assert.AreEqual("Vasya", wasEvent2);

            //Act:
            wasEvent1 = null;
            wasEvent2 = null;

            eventBase.Dispose();
            eventBase.Invoke("Petya");

            //Assert:
            Assert.AreNotEqual("Petya", wasEvent1);
            Assert.AreNotEqual("Petya", wasEvent2);
            Assert.IsNull(wasEvent1);
            Assert.IsNull(wasEvent2);
        }
    }

    public sealed class EventT2Tests
    {
        [Test]
        public void Subscribe()
        {
            //Arrange:
            object arg1 = null;
            int arg2 = -1;

            var eventBase = new Event<object, int>();
            eventBase.Subscribe((a1, a2) =>
            {
                arg1 = a1;
                arg2 = a2;
            });

            //Act:
            eventBase.Invoke("Vasya", 10);

            //Assert:
            Assert.AreEqual("Vasya", arg1);
            Assert.AreEqual(10, arg2);
        }

        [Test]
        public void Unsubscribe()
        {
            object arg1 = null;
            object arg2 = null;

            void OnEventRaisen(object a1, int a2)
            {
                arg1 = a1;
                arg2 = a2;
            }

            //Arrange:
            var eventBase = new Event<object, int>();
            eventBase.Subscribe(OnEventRaisen);

            eventBase.Invoke("Vasya", 10);
            Assert.AreEqual("Vasya", arg1);
            Assert.AreEqual(10, arg2);

            //Act:
            arg1 = null;
            arg2 = null;

            eventBase.OnEvent -= OnEventRaisen;
            eventBase.Invoke("Petya", 5);

            //Assert:
            Assert.AreNotEqual("Petya", arg1);
            Assert.AreNotEqual(5, arg2);

            Assert.IsNull(arg1);
            Assert.IsNull(arg2);
        }

        [Test]
        public void Dispose()
        {
            object arg1 = null;
            object arg2 = null;

            void OnEventRaisen(string s, int i)
            {
                arg1 = s;
                arg2 = i;
            }

            //Arrange:
            var eventBase = new Event<string, int>();
            eventBase.OnEvent += OnEventRaisen;

            eventBase.Invoke("Vasya", 3);
            Assert.AreEqual("Vasya", arg1);
            Assert.AreEqual(3, arg2);

            //Act:
            arg1 = null;
            arg2 = null;

            eventBase.Dispose();
            eventBase.Invoke("Petya", -10);

            //Assert:
            Assert.AreNotEqual("Petya", arg1);
            Assert.AreNotEqual(-10, arg2);
            Assert.IsNull(arg1);
            Assert.IsNull(arg2);
        }
    }

    public sealed class EventT3Tests
    {
        [Test]
        public void Subscribe()
        {
            //Arrange:
            object arg1 = null;
            int arg2 = -1;
            bool arg3 = false;

            var eventBase = new Event<object, int, bool>();
            eventBase.Subscribe((a1, a2, a3) =>
            {
                arg1 = a1;
                arg2 = a2;
                arg3 = a3;
            });

            //Act:
            eventBase.Invoke("Vasya", 10, true);

            //Assert:
            Assert.AreEqual("Vasya", arg1);
            Assert.AreEqual(10, arg2);
            Assert.IsTrue(arg3);
        }

        [Test]
        public void Unsubscribe()
        {
            object arg1 = null;
            object arg2 = null;
            object arg3 = null;

            void OnEventRaisen(object a1, int a2, bool a3)
            {
                arg1 = a1;
                arg2 = a2;
                arg3 = a3;
            }

            //Arrange:
            var eventBase = new Event<object, int, bool>();
            eventBase.Subscribe(OnEventRaisen);

            eventBase.Invoke("Vasya", 10, true);
            Assert.AreEqual("Vasya", arg1);
            Assert.AreEqual(10, arg2);
            Assert.AreEqual(true, arg3);

            //Act:
            arg1 = null;
            arg2 = null;
            arg3 = null;

            eventBase.OnEvent -= OnEventRaisen;
            eventBase.Invoke("Petya", 5, false);

            //Assert:
            Assert.IsNull(arg1);
            Assert.IsNull(arg2);
            Assert.IsNull(arg3);
        }

        [Test]
        public void Dispose()
        {
            object arg1 = null;
            object arg2 = null;
            object arg3 = null;

            void OnEventRaisen(string s, int i, bool b)
            {
                arg1 = s;
                arg2 = i;
                arg3 = b;
            }

            //Arrange:
            var eventBase = new Event<string, int, bool>();
            eventBase.OnEvent += OnEventRaisen;

            eventBase.Invoke("Vasya", 3, false);
            Assert.AreEqual("Vasya", arg1);
            Assert.AreEqual(3, arg2);
            Assert.AreEqual(false, arg3);

            //Act:
            arg1 = null;
            arg2 = null;
            arg3 = null;

            eventBase.Dispose();
            eventBase.Invoke("Petya", -10, true);

            //Assert:
            Assert.IsNull(arg1);
            Assert.IsNull(arg2);
            Assert.IsNull(arg3);
        }
    }
}