using NUnit.Framework;
using UnityEngine;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void OnBeforeSerialize()
        {
            //Arrange:
            var set = new ReactiveHashSet<string>
            {
                "Milk",
                "Bread",
                "Butter"
            };

            //Act:
            ((ISerializationCallbackReceiver) set).OnBeforeSerialize();

            Assert.AreEqual(new[]
            {
                "Milk",
                "Bread",
                "Butter"
            }, set.serializedItems);
        }

        [Test]
        public void OnAfterDeserialize()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveHashSet<string>();
            set.OnStateChanged += () => stateChanged = true;


            set.serializedItems = new[]
            {
                "Milk",
                "Bread",
                "Butter"
            }; 

            //Pre-assert:
            Assert.AreEqual(0, set.Count);

            //Act:
            ((ISerializationCallbackReceiver) set).OnAfterDeserialize();

            //Assert:
            Assert.IsTrue(stateChanged);
            Assert.AreEqual(3, set.Count);

            Assert.IsTrue(set.Contains("Milk"));
            Assert.IsTrue(set.Contains("Bread"));
            Assert.IsTrue(set.Contains("Butter"));
        }
    }
}