using System;
using System.Collections.Generic;
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
        public void GetEnumerator()
        {
            var array = new ReactiveArray<int>(1, 2, 3, 4, 10);
            IEnumerator<int> enumerator = array.GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(1, enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(2, enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(3, enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(4, enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(10, enumerator.Current);

            Assert.IsFalse(enumerator.MoveNext());

            enumerator.Dispose();
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
            var array = new ReactiveArray<int>(capacity: 3);

            //Act:
            Assert.Catch<IndexOutOfRangeException>(() => { array[20] = 40; });

            Assert.Catch<IndexOutOfRangeException>(() => { array[-1] = 15; });
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

        [Test]
        public void Clear_WithNonDefaultValues_ResetsToDefault_AndFiresEvents()
        {
            // Arrange
            var array = new ReactiveArray<int>(1, 2, 3);
            var changedIndices = new List<int>();
            bool stateChanged = false;

            array.OnItemChanged += (i, v) => changedIndices.Add(i);
            array.OnStateChanged += () => stateChanged = true;

            // Act
            array.Clear();

            // Assert
            Assert.That(array[0], Is.EqualTo(0));
            Assert.That(array[1], Is.EqualTo(0));
            Assert.That(array[2], Is.EqualTo(0));

            Assert.That(stateChanged, Is.True);
            Assert.That(changedIndices, Is.EquivalentTo(new[] {0, 1, 2}));
        }

        [Test]
        public void Clear_WithAlreadyDefaultValues_DoesNotFireItemChanged()
        {
            // Arrange
            var array = new ReactiveArray<string>(null, null, null);
            var changedIndices = new List<int>();
            bool stateChanged = false;

            array.OnItemChanged += (i, v) => changedIndices.Add(i);
            array.OnStateChanged += () => stateChanged = true;

            // Act
            array.Clear();

            // Assert
            Assert.That(array[0], Is.Null);
            Assert.That(changedIndices, Is.Empty);
            Assert.That(stateChanged, Is.True);
        }

        [Test]
        public void Clear_EmptyArray_DoesNotThrowOrFireEvents()
        {
            // Arrange
            var array = new ReactiveArray<int>(0);
            bool stateChanged = false;
            bool itemChangedCalled = false;

            array.OnStateChanged += () => stateChanged = true;
            array.OnItemChanged += (i, v) => itemChangedCalled = true;

            // Act
            array.Clear();

            // Assert
            Assert.That(stateChanged, Is.False);
            Assert.That(itemChangedCalled, Is.False);
        }

        [Test]
        public void SetAll_ValidInput_UpdatesValuesAndFiresEvents()
        {
            // Arrange
            var array = new ReactiveArray<int>(1, 2, 3);
            var changedIndices = new List<int>();
            bool stateChanged = false;

            array.OnItemChanged += (i, v) => changedIndices.Add(i);
            array.OnStateChanged += () => stateChanged = true;

            // Act
            array.Populate(new[] {10, 2, 30});

            // Assert
            Assert.That(array[0], Is.EqualTo(10));
            Assert.That(array[1], Is.EqualTo(2)); // unchanged
            Assert.That(array[2], Is.EqualTo(30));

            Assert.That(stateChanged, Is.True);
            Assert.That(changedIndices, Is.EquivalentTo(new[] {0, 2}));
        }

        [Test]
        public void SetAll_NullInput_ThrowsArgumentNullException()
        {
            var array = new ReactiveArray<string>(3);
            Assert.Throws<ArgumentNullException>(() => array.Populate(null));
        }
        

        [Test]
        public void SetAll_TooManyElements_ThrowsArgumentException()
        {
            var array = new ReactiveArray<int>(2);
            Assert.Throws<ArgumentException>(() => array.Populate(new[] {1, 2, 3}));
        }

        [Test]
        public void SetAll_NoChanges_DoesNotFireItemChanged_ButFiresStateChanged()
        {
            // Arrange
            var array = new ReactiveArray<int>(1, 2, 3);
            bool stateChanged = false;
            bool itemChangedCalled = false;

            array.OnItemChanged += (_, _) => itemChangedCalled = true;
            array.OnStateChanged += () => stateChanged = true;

            // Act
            array.Populate(new[] {1, 2, 3});

            // Assert
            Assert.That(itemChangedCalled, Is.False);
            Assert.That(stateChanged, Is.True);
        }
    }
}
