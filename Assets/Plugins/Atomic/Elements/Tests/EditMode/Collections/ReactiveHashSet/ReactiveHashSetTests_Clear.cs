using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void Clear()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            
            //Act:
            set.Clear();
            
            //Assert:
            Assert.AreEqual(0, set.Count);
            Assert.IsTrue(set.IsEmpty());
            
            Assert.IsFalse(set.Contains("Vasya"));
            Assert.IsFalse(set.Contains("Petya"));
            Assert.IsFalse(set.Contains("Masha"));
        }

        [Test]
        public void Clear_OnStateChanged()
        {
            //Arrange:
            bool stateChanged = false;
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.Clear();
            
            //Assert:
            Assert.IsTrue(stateChanged);
        }
        
        [Test]
        public void Clear_OnItemRemoved()
        {
            //Arrange:
            var removedItems = new HashSet<string>();
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            set.OnItemRemoved += item => removedItems.Add(item);

            //Act:
            set.Clear();
            
            //Assert:
            Assert.IsTrue(removedItems.Contains("Vasya"));
            Assert.IsTrue(removedItems.Contains("Petya"));
            Assert.IsTrue(removedItems.Contains("Masha"));
        }

        [Test]
        public void Clear_And_SetIsEmpty_NoOnStateChanged()
        {
            //Arrange:
            bool stateChanged = false;
            var set = new ReactiveHashSet<string>();
            set.OnStateChanged += () => stateChanged = true;

            //Act:
            set.Clear();
            
            //Assert:
            Assert.IsFalse(stateChanged);
        }
        
        [Test]
        public void Clear_And_SetIsEmpty_NoOnItemRemoved()
        {
            //Arrange:
            var wasEvent = false;
            var set = new ReactiveHashSet<string>();
            set.OnItemRemoved += _ => wasEvent = true;

            //Act:
            set.Clear();
            
            //Assert:
            Assert.IsFalse(wasEvent);
        }
    }
}