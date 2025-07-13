using System;
using NUnit.Framework;
// ReSharper disable AssignNullToNotNullAttribute

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void CopyTo()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            var arr = new string[3];

            //Act:
            set.CopyTo(arr);

            Assert.AreEqual(new[] {"Vasya", "Petya", "Masha"}, arr);
        }

        [Test]
        public void CopyToNull()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            
            //Assert:
            Assert.Throws<ArgumentNullException>(() => set.CopyTo(null, 10));
        }
        
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-8)]
        [TestCase(-10)]
        public void CopyTo_And_InvalidIndex(int arrayIndex)
        {
            //Arrange:
            var set = new ReactiveHashSet<string>("Vasya", "Petya", "Masha");
            var arr = new string[3];

            //Assert:
            Assert.Throws<ArgumentOutOfRangeException>(() => set.CopyTo(arr, arrayIndex));
        }
    }
}