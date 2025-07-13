using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [TestCase("Vasya", ExpectedResult = true)]
        [TestCase("Petya", ExpectedResult = true)]
        [TestCase("Masha", ExpectedResult = true)]
        [TestCase("Ivan", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        public bool Remove(string item)
        {
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            return set.Remove(item);
        }

        [TestCase("Vasya", ExpectedResult = 2)]
        [TestCase("Petya", ExpectedResult = 2)]
        [TestCase("Masha", ExpectedResult = 2)]
        [TestCase("Ivan", ExpectedResult = 3)]
        [TestCase(null, ExpectedResult = 3)]
        public int Remove_CountChanged(string item)
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            
            //Act:
            set.Remove(item);

            //Assert:
            return set.Count;
        }

        [Test]
        public void Remove_OnStateChanged()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            bool stateChanged = false;
            set.OnStateChanged += () => stateChanged = true;
            
            //Act:
            set.Remove("Petya");
            
            //Assert:
            Assert.IsTrue(stateChanged);
        }

        [Test]
        public void RemoveItemThatNotContains_NoOnStateChanged()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            bool stateChanged = false;
            set.OnStateChanged += () => stateChanged = true;
            
            //Act:
            set.Remove("Ivan");
            
            //Assert:
            Assert.IsFalse(stateChanged);
        }

        [TestCase("Vasya")]
        [TestCase("Petya")]
        [TestCase("Masha")]
        public void Remove_OnItemRemoved(string item)
        {
            //Arrange:
            string removedItem = null;
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            set.OnItemRemoved += value => removedItem = value;
            
            //Act:
            set.Remove(item);
            
            //Assert:
            Assert.AreEqual(item, removedItem);
        }

        [Test]
        public void RemoveItemThatNotContains_NoOnItemRemoved()
        {
            //Arrange:
            string removedItem = null;
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            set.OnItemRemoved += item => removedItem = item;
            
            //Act:
            set.Remove("Ivan");
            
            //Assert:
            Assert.IsNull(removedItem);
        }
        
        [Test]
        public void RemoveRange()
        {
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            var range = new[] {"Vasya", "Petya", "Masha"};

            foreach (string item in range) 
                set.Remove(item);

            Assert.AreEqual(0, set.Count);
        }
    }
}