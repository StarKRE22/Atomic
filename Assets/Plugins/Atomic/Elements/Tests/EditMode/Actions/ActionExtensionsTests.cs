using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ActionExtensionsTests
    {
        [Test]
        public void InvokeAll()
        {
            //Arrange:
            var a1 = new ActionSpy();
            var a2 = new ActionSpy();
            var a3 = new ActionSpy();
            var a4 = new ActionSpy();

            var collection = new IAction[]
            {
                a1,
                a2,
                a3,
                a4,
                null
            };
            
            //Act"
            collection.InvokeRange();
            

            //Assert:
            Assert.IsTrue(a1.WasInvoked);
            Assert.IsTrue(a2.WasInvoked);
            Assert.IsTrue(a3.WasInvoked);
            Assert.IsTrue(a4.WasInvoked);
        }

        [Test]
        public void WhenNullThenNothingHappened()
        {
            Extensions.InvokeRange(null);
        }
    }
}