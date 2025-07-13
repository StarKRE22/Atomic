using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        //TODO: Распилить тест!
        [Test]
        public void IntersectWith()
        {
            //Arrange:
            bool stateChanged = false;
            HashSet<string> removedItems = new HashSet<string>();

            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha", "Ivan", "Willy");
            set.OnStateChanged += () => stateChanged = true;
            set.OnItemRemoved += item => removedItems.Add(item);

            //Act:
            set.IntersectWith(new[] {"Petya", "Ivan", "John"});

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(2, set.Count);

            Assert.IsFalse(set.Contains("Vasya"));
            Assert.IsFalse(set.Contains("Masha"));

            Assert.IsTrue(set.Contains("Petya"));
            Assert.IsTrue(set.Contains("Ivan"));
            
            Assert.IsTrue(removedItems.Contains("Vasya"));
            Assert.IsTrue(removedItems.Contains("Masha"));
        }
    }
}