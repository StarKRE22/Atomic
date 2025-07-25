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
            InlineAction action = new InlineAction(() => wasAction = true);
            
            //Act:
            action.Invoke();
            
            //Assert:
            Assert.IsTrue(wasAction);
        }
        
        [Test]
        public void ConstructAndInvoke()
        {
            //Arrange:
            bool wasAction = false;
            InlineAction action = new InlineAction();
            action.Construct(() => wasAction = true);
            
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
            BasicAction<string> action = new BasicAction<string>(args => wasAction = args);
            
            //Act:
            action.Invoke("Vasya");
            
            //Assert:
            Assert.AreEqual("Vasya", wasAction);
        }
        
        [Test]
        public void ConstructAndInvoke()
        {
            //Arrange:
            string wasAction = string.Empty;

            BasicAction<string> action = new BasicAction<string>();
            action.Construct(args => wasAction = args);
            
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
            
            var action = new BasicAction<string, int>((a1, a2) =>
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
        public void ConstructAndInvoke()
        {
            //Arrange:
            string t1 = null;
            int t2 = -1;
            
            var action = new BasicAction<string, int>();
            action.Construct((a1, a2) =>
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
            
            var action = new BasicAction<string, int, bool>((a1, a2, a3) =>
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
        public void ConstructAndInvoke()
        {
            //Arrange:
            string t1 = null;
            int t2 = -1;
            bool t3 = false;
  
            var action = new BasicAction<string, int, bool>();
            action.Construct((a1, a2, a3) =>
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