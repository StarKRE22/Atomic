using NUnit.Framework;
// ReSharper disable AssignNullToNotNullAttribute

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void Add()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>();

            //Act:
            bool success = set.Add("Vasya");

            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(set.Contains("Vasya"));
            Assert.AreEqual(1, set.Count);
        }
        
        [Test]
        public void AddNull()
        {
            //Arrange:
            var set = new ReactiveHashSet<object>();

            //Act:
            bool success = set.Add(null);

            //Assert:
            Assert.IsFalse(success);
            Assert.AreEqual(0, set.Count);
        }

        [Test]
        public void Add_OnItemAdded()
        {
            //Arrange:
            string addedItem = null;

            var set = new ReactiveHashSet<string>();
            set.OnItemAdded += v => { addedItem = v; };

            //Act:
            set.Add("Vasya");

            //Assert:
            Assert.AreEqual("Vasya", addedItem);
        }

        [Test]
        public void Add_OnStateChanged()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveHashSet<string>();
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.Add("Vasya");

            //Assert:
            Assert.IsTrue(stateChanged);
        }

        [TestCase("Vasya")]
        [TestCase("Petya")]
        [TestCase("Masha")]
        public void AddItemThatContains_False(string value)
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
        
            //Act:
            bool success = set.Add(value);
        
            //Assert:
            Assert.IsFalse(success);
            Assert.AreEqual(3, set.Count);
        }
        
        [TestCase("Vasya")]
        [TestCase("Petya")]
        [TestCase("Masha")]
        public void AddItemThatContains_NoOnItemAdded(string value)
        {
            //Arrange:
            string addedItem = null;
        
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            set.OnItemAdded += v => addedItem = v;
        
            //Act:
            set.Add(value);
        
            //Assert:
            Assert.IsNull(addedItem);
        }
        
        [TestCase("Vasya")]
        [TestCase("Petya")]
        [TestCase("Masha")]
        public void AddItemThatContains_NoOnStateChanged(string value)
        {
            //Arrange:
            bool stateChanged = false;
        
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            set.OnStateChanged += () => stateChanged = true;
        
            //Act:
            set.Add(value);
        
            //Assert:
            Assert.IsFalse(stateChanged);
        }
    }
}