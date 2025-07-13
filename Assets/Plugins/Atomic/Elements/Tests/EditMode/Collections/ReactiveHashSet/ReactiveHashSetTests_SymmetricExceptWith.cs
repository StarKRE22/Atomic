using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        //TODO: Распилить!
        [Test]
        public void SymmetricExceptWith()
        {
            //Arrange:
            bool stateChanged = false;
            HashSet<string> removedItems = new HashSet<string>();
            HashSet<string> addedItems = new HashSet<string>();

            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Ivan", "Willy");
            set.OnStateChanged += () => stateChanged = true;
            set.OnItemRemoved += item => removedItems.Add(item);
            set.OnItemAdded += item => addedItems.Add(item);

            //Act:
            set.SymmetricExceptWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(4, set.Count);

            Assert.IsTrue(set.Contains("Vasya"));
            Assert.IsTrue(set.Contains("Masha"));
            Assert.IsTrue(set.Contains("Willy"));
            Assert.IsTrue(set.Contains("John"));

            Assert.IsFalse(set.Contains("Petya"));
            Assert.IsFalse(set.Contains("Ivan"));
            
            Assert.IsTrue(removedItems.Contains("Petya"));
            Assert.IsTrue(removedItems.Contains("Ivan"));
            
            Assert.IsTrue(addedItems.Contains("John"));
        }
    }
}