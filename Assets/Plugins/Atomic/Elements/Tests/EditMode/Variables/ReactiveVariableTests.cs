using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ReactiveVariableTests
    {
        [Test]
        public void Instantiate()
        {
            //Arrange:
            var variable = new ReactiveVariable<int>(5);
            
            //Assert:
            Assert.AreEqual(5, variable.Value);
        }

        [Test]
        public void ChangeValue()
        {
            int r1 = -1;
            int r2 = -1;
            
            //Arrange:
            var variable = new ReactiveVariable<int>(5);

            variable.OnValueChanged += v => r1 = v;
            variable.Subscribe(v => r2 = v);
            
            //Act:
            variable.Value = 3;

            //Assert:
            Assert.AreEqual(3, variable.Value);
            Assert.AreEqual(3, r1);
            Assert.AreEqual(3, r2);
        }

        [Test]
        public void WhenChangeVariableOnASameValueThenEventWillNotRaisen()
        {
            int r1 = -1;
            int r2 = -1;
            
            //Arrange:
            var variable = new ReactiveVariable<int>(5);

            variable.OnValueChanged += v => r1 = v;
            variable.Subscribe(v => r2 = v);
            
            //Act:
            variable.Value = 5;

            //Assert:
            Assert.AreEqual(5, variable.Value);
            Assert.AreEqual(-1, r1);
            Assert.AreEqual(-1, r2);
        }

        [Test]
        public void WhenDisposeVariableThenEventsWillNotRaisen()
        {
            int r1 = -1;
            int r2 = -1;
            
            //Arrange:
            var variable = new ReactiveVariable<int>(5);

            variable.OnValueChanged += v => r1 = v;
            variable.Subscribe(v => r2 = v);
            
            variable.Value = 3;
            Assert.AreEqual(3, variable.Value);
            Assert.AreEqual(3, r1);
            Assert.AreEqual(3, r2);
            
            //Act:
            r1 = -1;
            r2 = -1;
            variable.Dispose();
            variable.Value = 5;

            //Assert:
            Assert.AreEqual(5, variable.Value);
            Assert.AreEqual(-1, r1);
            Assert.AreEqual(-1, r2);
        }
        
        
        [Test]
        public void Unsubscribe()
        {
            int r1 = -1;
            int r2 = -1;

            void OnValueChanged1(int value)
            {
                r1 = value;
            }
            
            void OnValueChanged2(int value)
            {
                r2 = value;
            }
            
            //Arrange:
            var variable = new ReactiveVariable<int>(5);

            variable.OnValueChanged += OnValueChanged1;
            variable.Subscribe(OnValueChanged2);
            
            variable.Value = 3;
            Assert.AreEqual(3, variable.Value);
            Assert.AreEqual(3, r1);
            Assert.AreEqual(3, r2);
            
            //Act:
            r1 = -1;
            r2 = -1;
            
            variable.OnValueChanged -= OnValueChanged1;
            variable.Unsubscribe(OnValueChanged2);
            variable.Value = 5;

            //Assert:
            Assert.AreEqual(5, variable.Value);
            Assert.AreEqual(-1, r1);
            Assert.AreEqual(-1, r2);
        }
    }
}