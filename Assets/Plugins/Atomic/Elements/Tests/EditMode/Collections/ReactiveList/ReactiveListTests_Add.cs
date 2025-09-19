using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed partial class ReactiveListTests
    {
        [Test]
        public void Add()
        {
            //Arrange:
            var list = new ReactiveList<string>();

            //Act:
            list.Add("Vasya");

            //Assert:
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Vasya", list[0]);
            Assert.IsTrue(list.Contains("Vasya"));
        }

        [Test]
        public void Add_OnItemInserted()
        {
            //Arrange:
            string insertedItem = null;
            int insertedIndex = -1;

            var list = new ReactiveList<string>();
            list.OnItemAdded += (i, v) =>
            {
                insertedIndex = i;
                insertedItem = v;
            };

            //Act:
            list.Add("Vasya");

            //Assert:
            Assert.AreEqual("Vasya", insertedItem);
            Assert.AreEqual(0, insertedIndex);
        }

        [Test]
        public void Add_OnStateChanged()
        {
            //Arrange:
            bool stateChanged = false;

            var list = new ReactiveList<string>();
            list.OnStateChanged += () => stateChanged = true;

            //Act:
            list.Add("Vasya");

            //Assert:
            Assert.IsTrue(stateChanged);
        }
    }
}