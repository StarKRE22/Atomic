using System;
using NUnit.Framework;
// ReSharper disable UnusedVariable

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ReactiveArrayTests
    {
        [Test]
        public void Instantiate()
        {
            //Act:
            var array = new ReactiveArray<int>(5);

            //Assert:
            Assert.AreEqual(5, array.Length);
        }

        [Test]
        public void GetElement()
        {
            var array = new ReactiveArray<int>(1, 2, 3, 4, 10);

            //Assert:
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(3, array[2]);
            Assert.AreEqual(4, array[3]);
            Assert.AreEqual(10, array[4]);
            Assert.AreEqual(5, array.Length);
        }

        [Test]
        public void WhenGetElementOutOfRangeThenCatchException()
        {
            //Arrange:
            var array = new ReactiveArray<int>(1, 2, 3, 4, 10);

            //Act:
            Assert.Catch<IndexOutOfRangeException>(() =>
            {
                int value = array[20];
            });
            
            Assert.Catch<IndexOutOfRangeException>(() =>
            {
                int value = array[-1];
            });
        }
        
        [Test]
        public void WhenSetElementOutOfRangeThenCatchException()
        {
            //Arrange:
            var array = new ReactiveArray<int>(length: 3);

            //Act:
            Assert.Catch<IndexOutOfRangeException>(() =>
            {
                array[20] = 40;
            });
            
            Assert.Catch<IndexOutOfRangeException>(() =>
            {
                array[-1] = 15;
            });
        }

        [Test]
        public void ChangeElement()
        {
            //Arrange:
            var array = new ReactiveArray<int>(5);
            var changedItem = -1;
            var changedIndex = -1;
            var wasEvent = false;

            //Act:
            array.OnItemChanged += (index, value) =>
            {
                changedIndex = index;
                changedItem = value;
            };
            array.OnStateChanged += () => wasEvent = true;

            array[3] = 25;

            //Assert:
            Assert.IsTrue(wasEvent);
            Assert.AreEqual(25, array[3]);
            Assert.AreEqual(25, changedItem);
            Assert.AreEqual(3, changedIndex);
        }
    }
}