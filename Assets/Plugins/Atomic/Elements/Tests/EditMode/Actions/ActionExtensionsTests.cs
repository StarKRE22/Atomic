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
            var a1 = new ActionStub();
            var a2 = new ActionStub();
            var a3 = new ActionStub();
            var a4 = new ActionStub();

            var collection = new IAction[]
            {
                a1,
                a2,
                a3,
                a4,
                null
            };
            
            //Act"
            collection.InvokeAll();
            

            //Assert:
            Assert.IsTrue(a1.wasInvoke);
            Assert.IsTrue(a2.wasInvoke);
            Assert.IsTrue(a3.wasInvoke);
            Assert.IsTrue(a4.wasInvoke);
        }

        [Test]
        public void WhenNullThenNothingHappened()
        {
            Extensions.InvokeAll(null);
        }
    }
}