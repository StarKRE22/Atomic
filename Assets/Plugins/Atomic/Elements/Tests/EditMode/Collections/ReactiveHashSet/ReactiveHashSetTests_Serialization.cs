using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Elements
{
    public sealed partial class ReactiveHashSetTests
    {
        [Test]
        public void OnBeforSerialize()
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

            //Assert:
            List<string> items = typeof(ReactiveHashSet<string>)
                .GetField("items", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!
                .GetValue(set) as List<string>;

            Assert.AreEqual(new List<string>
            {
                "Milk",
                "Bread",
                "Butter"
            }, items);
        }

        [Test]
        public void OnAfterDeserialize()
        {
            //Arrange:
            bool stateChanged = false;

            var set = new ReactiveHashSet<string>();
            set.OnStateChanged += () => stateChanged = true;

            typeof(ReactiveHashSet<string>)
                .GetField("items", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!
                .SetValue(set, new List<string>
                {
                    "Milk",
                    "Bread",
                    "Butter"
                });

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