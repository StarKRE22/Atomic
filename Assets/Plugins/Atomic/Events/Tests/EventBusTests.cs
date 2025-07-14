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
        public void Declare_Then_Defined()
        {
            //Arrange:
            var eventBus = new EventBus();
            
            //Act:
            eventBus.Declare(1);
            
            //Assert:
            Assert.IsTrue(eventBus.IsDeclared(1));
        }

        [Test]
        public void Subscribe_Then_Declared()
        {
            //Arrange:
            var eventBus = new EventBus();
            
            //Act:
            eventBus.Subscribe(1, () => {});
            
            //Assert:
            Assert.IsTrue(eventBus.IsDeclared(1));
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
            eventBus.Declare<object>("Hello");
            eventBus.Subscribe<object>("Hello", t =>
            {
                wasTarget = t;
                wasEvent = true;
            });
            eventBus.Invoke("Hello", args);
            
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(args, wasTarget);
        }
    }
}