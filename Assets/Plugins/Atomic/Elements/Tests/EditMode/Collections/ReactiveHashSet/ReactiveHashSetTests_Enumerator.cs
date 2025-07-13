using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void GetEnumerator()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            var result = new List<string>();
            
            //Act:
            foreach (string s in set) 
                result.Add(s);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(result[0], "Vasya");
            Assert.AreEqual(result[1], "Petya");
            Assert.AreEqual(result[2], "Masha");
        }
    }
}