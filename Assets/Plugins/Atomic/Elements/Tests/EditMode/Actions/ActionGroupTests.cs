using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ActionGroupTests
    {
        [Test]
        public void CreateAndInvoke()
        {
            //Arrange:
            var a1 = new ActionStub();
            var a2 = new ActionStub();
            var a3 = new ActionStub();
            var a4 = new ActionStub();
            
            IAction actionGroup = new CompositeAction(a1, a2, a3, a4);
            
            //Act:
            actionGroup.Invoke();
            
            //Assert:
            Assert.IsTrue(a1.wasInvoke);
            Assert.IsTrue(a2.wasInvoke);
            Assert.IsTrue(a3.wasInvoke);
            Assert.IsTrue(a4.wasInvoke);
        }
        
        [Test]
        public void ComposeAndInvoke()
        {
            //Arrange:
            var a1 = new ActionStub();
            var a2 = new ActionStub();
            var a3 = new ActionStub();
            var a4 = new ActionStub();
            
            var actionGroup = new CompositeAction();
            
            //Act:
            actionGroup.Construct(a1, a2, a3, a4);
            actionGroup.Invoke();
            
            //Assert:
            Assert.IsTrue(a1.wasInvoke);
            Assert.IsTrue(a2.wasInvoke);
            Assert.IsTrue(a3.wasInvoke);
            Assert.IsTrue(a4.wasInvoke);
        }
    }
}