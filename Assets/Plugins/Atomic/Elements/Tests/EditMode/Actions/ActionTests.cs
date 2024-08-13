using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ActionTests
    {
        [Test]
        public void CreateAndInvoke()
        {
            //Arrange:
            bool wasAction = false;
            ProxyAction action = new ProxyAction(() => wasAction = true);
            
            //Act:
            action.Invoke();
            
            //Assert:
            Assert.IsTrue(wasAction);
        }
        
        [Test]
        public void ComposeAndInvoke()
        {
            //Arrange:
            bool wasAction = false;
            ProxyAction action = new ProxyAction();
            action.Compose(() => wasAction = true);
            
            //Act:
            action.Invoke();
            
            //Assert:
            Assert.IsTrue(wasAction);
        }
    }
    
    [TestFixture]
    public sealed class ActionT1Tests
    {
        [Test]
        public void CreateAndInvoke()
        {
            //Arrange:
            string wasAction = string.Empty;
            ProxyAction<string> action = new ProxyAction<string>(args => wasAction = args);
            
            //Act:
            action.Invoke("Vasya");
            
            //Assert:
            Assert.AreEqual("Vasya", wasAction);
        }
        
        [Test]
        public void ComposeAndInvoke()
        {
            //Arrange:
            string wasAction = string.Empty;

            ProxyAction<string> action = new ProxyAction<string>();
            action.Compose(args => wasAction = args);
            
            //Act:
            action.Invoke("Vasya");
            
            //Assert:
            Assert.AreEqual("Vasya", wasAction);
        }
    }
    
    
    [TestFixture]
    public sealed class ActionT2Tests
    {
        [Test]
        public void CreateAndInvoke()
        {
            //Arrange:
            string t1 = null;
            int t2 = -1;
            
            var action = new ProxyAction<string, int>((a1, a2) =>
            {
                t1 = a1;
                t2 = a2;
            });
            
            //Act:
            action.Invoke("Vasya", 10);
            
            //Assert:
            Assert.AreEqual("Vasya", t1);
            Assert.AreEqual(10, t2);
        }
        
        [Test]
        public void ComposeAndInvoke()
        {
            //Arrange:
            string t1 = null;
            int t2 = -1;
            
            var action = new ProxyAction<string, int>();
            action.Compose((a1, a2) =>
            {
                t1 = a1;
                t2 = a2;
            });
            
            //Act:
            action.Invoke("Vasya", 10);
            
            //Assert:
            Assert.AreEqual("Vasya", t1);
            Assert.AreEqual(10, t2);
        }
    }
    
    
    [TestFixture]
    public sealed class ActionT3Tests
    {
        [Test]
        public void CreateAndInvoke()
        {
            //Arrange:
            string t1 = null;
            int t2 = -1;
            bool t3 = false;
            
            var action = new ProxyAction<string, int, bool>((a1, a2, a3) =>
            {
                t1 = a1;
                t2 = a2;
                t3 = a3;
            });
            
            //Act:
            action.Invoke("Vasya", 10, true);
            
            //Assert:
            Assert.AreEqual("Vasya", t1);
            Assert.AreEqual(10, t2);
            Assert.IsTrue(t3);
        }
        
        [Test]
        public void ComposeAndInvoke()
        {
            //Arrange:
            string t1 = null;
            int t2 = -1;
            bool t3 = false;
  
            var action = new ProxyAction<string, int, bool>();
            action.Compose((a1, a2, a3) =>
            {
                t1 = a1;
                t2 = a2;
                t3 = a3;
            });
            
            //Act:
            action.Invoke("Vasya", 10, true);
            
            //Assert:
            Assert.AreEqual("Vasya", t1);
            Assert.AreEqual(10, t2);
            Assert.IsTrue(t3);
        }
    }
}