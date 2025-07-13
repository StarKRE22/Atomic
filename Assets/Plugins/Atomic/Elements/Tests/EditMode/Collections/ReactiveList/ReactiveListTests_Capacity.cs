using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveListTests
    {
        [Test]
        public void WhenAddItem_ThenCapacityDoubles()
        {
            var list = new ReactiveList<object>();
            Assert.AreEqual(0, list.Capacity);

            list.Add(new object());
            Assert.AreEqual(1, list.Capacity);

            list.Add(new object());
            Assert.AreEqual(2, list.Capacity);
            
            list.Add(new object());
            Assert.AreEqual(4, list.Capacity);

            list.Add(new object());
            Assert.AreEqual(4, list.Capacity);

            list.Add(new object());
            Assert.AreEqual(8, list.Capacity);
        }

        [Test]
        public void WhenListHasInitialCapacityAndAddItem_ThenCapacityExpandsCorrectly()
        {
            var list = new ReactiveList<object>(3);
            Assert.AreEqual(3, list.Capacity);

            list.Add(new object());
            Assert.AreEqual(3, list.Capacity);

            list.Add(new object());
            Assert.AreEqual(3, list.Capacity);
            
            list.Add(new object());
            Assert.AreEqual(3, list.Capacity);

            list.Add(new object());
            Assert.AreEqual(6, list.Capacity);
        }
    }
}