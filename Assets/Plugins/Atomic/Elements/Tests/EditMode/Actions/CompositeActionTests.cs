using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class CompositeActionTests
    {
        [Test]
        public void CreateAndInvoke()
        {
            //Arrange:
            bool invoked1 = false;
            bool invoked2 = false;
            bool invoked3 = false;
            
            var a1 = new Action(() => invoked1 = true);
            var a2 = new Action(() => invoked2 = true);
            var a3 = new Action(() => invoked3 = true);
            
            IAction actionGroup = new CompositeAction(a1, a2, a3);
            
            //Act:
            actionGroup.Invoke();
            
            //Assert:
            Assert.IsTrue(invoked1);
            Assert.IsTrue(invoked2);
            Assert.IsTrue(invoked3);
        }
    }
}