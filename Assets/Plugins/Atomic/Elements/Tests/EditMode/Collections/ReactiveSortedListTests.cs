// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using NUnit.Framework;
//
// namespace Atomic.Elements
// {
//     [TestFixture]
//     public sealed class ReactiveSortedListTests
//     {
//         [Test]
//         public void Instantiate()
//         {
//             //Arrange:
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5},
//                 {"Bread", 3},
//                 {"Butter", 2}
//             };
//
//             //Assert:
//             Assert.AreEqual(3, dictionary.Count);
//
//             Assert.AreEqual(5, dictionary["Milk"]);
//             Assert.AreEqual(3, dictionary["Bread"]);
//             Assert.AreEqual(2, dictionary["Butter"]);
//
//             Assert.IsTrue(dictionary.ContainsKey("Milk"));
//             Assert.IsTrue(dictionary.Contains(new KeyValuePair<string, int>("Milk", 5)));
//             Assert.IsFalse(dictionary.Contains(new KeyValuePair<string, int>("Milk", 3)));
//
//             Assert.AreEqual(new[] {"Bread", "Butter", "Milk"}, dictionary.Keys);
//             Assert.AreEqual(new[] {3, 2, 5}, dictionary.Values);
//
//             Assert.IsFalse(dictionary.IsReadOnly);
//         }
//
//         [Test]
//         public void TryGetValue()
//         {
//             //Arrange:
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5},
//                 {"Butter", 2}
//             };
//
//             //Act-Assert:
//             bool success = dictionary.TryGetValue("Milk", out int value);
//             Assert.IsTrue(success);
//             Assert.AreEqual(5, value);
//
//             //Act-Assert:
//             success = dictionary.TryGetValue("Honey", out value);
//             Assert.IsFalse(success);
//             Assert.AreEqual(0, value);
//         }
//
//         [Test]
//         public void WhenGetAbsentKeyThenThrowsKeyNotFoundException()
//         {
//             //Act:
//             Assert.Catch<KeyNotFoundException>(() =>
//             {
//                 int _ = new ReactiveSortedList<string, int>()["Milk"];
//             });
//         }
//
//         [Test]
//         public void AddValue()
//         {
//             //Arrange:
//             var dictionary = new ReactiveSortedList<string, int>();
//
//             string addedKey = null;
//             int addedValue = -1;
//             bool stateChanged = false;
//
//             dictionary.OnStateChanged += () => stateChanged = true;
//
//             dictionary.OnItemAdded += (k, v) =>
//             {
//                 addedKey = k;
//                 addedValue = v;
//             };
//
//             //Act:
//             dictionary.Add("Milk", 3);
//
//             //Assert:
//             Assert.IsTrue(stateChanged);
//             Assert.AreEqual(1, dictionary.Count);
//             Assert.AreEqual(3, dictionary["Milk"]);
//             Assert.AreEqual("Milk", addedKey);
//             Assert.AreEqual(3, addedValue);
//         }
//
//
//         [Test]
//         public void AddKeyValuePair()
//         {
//             //Arrange:
//             var dictionary = new ReactiveSortedList<string, int>();
//
//             string addedKey = null;
//             int addedValue = -1;
//             bool stateChanged = false;
//
//             dictionary.OnStateChanged += () => stateChanged = true;
//             dictionary.OnItemAdded += (k, v) =>
//             {
//                 addedKey = k;
//                 addedValue = v;
//             };
//
//             //Act:
//             dictionary.Add(new KeyValuePair<string, int>("Milk", 3));
//
//             //Assert:
//             Assert.IsTrue(stateChanged);
//
//             Assert.AreEqual(1, dictionary.Count);
//             Assert.AreEqual(3, dictionary["Milk"]);
//             Assert.AreEqual("Milk", addedKey);
//             Assert.AreEqual(3, addedValue);
//         }
//
//
//         [Test]
//         public void WhenValueIsAlreadyAddedThenThrowsException()
//         {
//             Assert.Catch<ArgumentException>(() => new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5}
//             }.Add("Milk", 3));
//         }
//
//         [Test]
//         public void WhenAddKeyValuePairThatIsAlreadyAddedThenThrowsException()
//         {
//             Assert.Catch<ArgumentException>(() => new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5}
//             }.Add(new KeyValuePair<string, int>("Milk", 3)));
//         }
//
//         [Test]
//         public void WhenSetValueWithNewKeyThenAddEventWillRaisen()
//         {
//             string addedKey = null;
//             int addedValue = -1;
//
//             string changedKey = null;
//             int changedValue = -1;
//
//             bool stateChanged = false;
//
//             var dictionary = new ReactiveSortedList<string, int>();
//             dictionary.OnStateChanged += () => stateChanged = true;
//             dictionary.OnItemAdded += (k, v) =>
//             {
//                 addedKey = k;
//                 addedValue = v;
//             };
//
//             dictionary.OnItemChanged += (k, v) =>
//             {
//                 changedKey = k;
//                 changedValue = v;
//             };
//
//             //Act:
//             dictionary["Milk"] = 5;
//
//             Assert.IsTrue(stateChanged);
//             Assert.AreEqual("Milk", addedKey);
//             Assert.AreEqual(5, addedValue);
//             Assert.IsNull(changedKey);
//             Assert.AreEqual(-1, changedValue);
//         }
//
//         [Test]
//         public void WhenSetValueWithExistsKeyThenChangeEventWillRaisen()
//         {
//             string addedKey = null;
//             int addedValue = -1;
//
//             string changedKey = null;
//             int changedValue = -1;
//
//             bool stateChanged = false;
//
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5}
//             };
//
//             dictionary.OnStateChanged += () => stateChanged = true;
//             dictionary.OnItemAdded += (k, v) =>
//             {
//                 addedKey = k;
//                 addedValue = v;
//             };
//
//             dictionary.OnItemChanged += (k, v) =>
//             {
//                 changedKey = k;
//                 changedValue = v;
//             };
//
//             //Act:
//             dictionary["Milk"] = 3;
//
//             //Assert:
//             Assert.IsTrue(stateChanged);
//
//             Assert.IsNull(addedKey);
//             Assert.AreEqual(-1, addedValue);
//             Assert.AreEqual("Milk", changedKey);
//             Assert.AreEqual(3, changedValue);
//         }
//
//         [Test]
//         public void WhenSetSameKeyAndValueThenWillNotHappened()
//         {
//             string addedKey = null;
//             int addedValue = -1;
//
//             string changedKey = null;
//             int changedValue = -1;
//
//             bool stateChanged = false;
//
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5}
//             };
//
//             dictionary.OnStateChanged += () => stateChanged = true;
//
//             dictionary.OnItemAdded += (k, v) =>
//             {
//                 addedKey = k;
//                 addedValue = v;
//             };
//
//             dictionary.OnItemChanged += (k, v) =>
//             {
//                 changedKey = k;
//                 changedValue = v;
//             };
//
//             //Act:
//             dictionary["Milk"] = 5;
//
//             Assert.IsFalse(stateChanged);
//
//             Assert.IsNull(addedKey);
//             Assert.AreEqual(-1, addedValue);
//             Assert.IsNull(changedKey);
//             Assert.AreEqual(-1, changedValue);
//         }
//
//         [Test]
//         public void OnBeforSerialize()
//         {
//             //Arrange:
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5},
//                 {"Bread", 3},
//                 {"Butter", 2}
//             };
//
//
//             //Act:
//             dictionary.OnBeforeSerialize();
//
//             //Assert:
//             var pairs = typeof(ReactiveSortedList<string, int>)
//                 .GetField("pairs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!
//                 .GetValue(dictionary) as ReactiveSortedList<string, int>.Pair[];
//
//             Assert.AreEqual(new ReactiveSortedList<string, int>.Pair[]
//             {
//                 new() {key = "Bread", value = 3},
//                 new() {key = "Butter", value = 2},
//                 new() {key = "Milk", value = 5}
//             }, pairs);
//         }
//
//         [Test]
//         public void OnAfterDeserialize()
//         {
//             //Arrange:
//             bool stateChanged = false;
//
//             var dictionary = new ReactiveSortedList<string, int>();
//             dictionary.OnStateChanged += () => stateChanged = true;
//
//             typeof(ReactiveSortedList<string, int>)
//                 .GetField("pairs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!
//                 .SetValue(dictionary, new ReactiveSortedList<string, int>.Pair[]
//                 {
//                     new() {key = "Milk", value = 5},
//                     new() {key = "Bread", value = 3},
//                     new() {key = "Butter", value = 2},
//                 });
//
//             //Pre-assert:
//             Assert.AreEqual(0, dictionary.Count);
//
//             //Act:
//             dictionary.OnAfterDeserialize();
//
//             //Assert:
//
//             Assert.IsTrue(stateChanged);
//
//             Assert.AreEqual(3, dictionary.Count);
//
//             Assert.AreEqual(5, dictionary["Milk"]);
//             Assert.AreEqual(3, dictionary["Bread"]);
//             Assert.AreEqual(2, dictionary["Butter"]);
//         }
//
//
//         [Test]
//         public void RemoveKeyValuePair()
//         {
//             string removedKey = null;
//             int removedValue = -1;
//
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5},
//                 {"Bread", 3},
//             };
//
//             dictionary.OnItemRemoved += (k, v) =>
//             {
//                 removedKey = k;
//                 removedValue = v;
//             };
//
//             bool success = dictionary.Remove("Milk");
//             Assert.IsTrue(success);
//             Assert.IsFalse(dictionary.ContainsKey("Milk"));
//             Assert.AreEqual("Milk", removedKey);
//             Assert.AreEqual(5, removedValue);
//
//             success = dictionary.Remove("Cherry");
//             Assert.IsFalse(success);
//             Assert.AreEqual("Milk", removedKey);
//             Assert.AreEqual(5, removedValue);
//
//             success = dictionary.Remove("Bread", out int value);
//             Assert.IsTrue(success);
//             Assert.AreEqual(3, value);
//             Assert.AreEqual("Bread", removedKey);
//             Assert.AreEqual(3, removedValue);
//
//             success = dictionary.Remove("Honey", out value);
//             Assert.AreEqual("Bread", removedKey);
//             Assert.AreEqual(3, removedValue);
//             Assert.IsFalse(success);
//             Assert.AreEqual(0, value);
//         }
//
//         [Test]
//         public void Remove()
//         {
//             string removedKey = null;
//             int removedValue = -1;
//             bool stateChanged = false;
//
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5},
//                 {"Bread", 3},
//             };
//
//             dictionary.OnStateChanged += () => stateChanged = true;
//             dictionary.OnItemRemoved += (k, v) =>
//             {
//                 removedKey = k;
//                 removedValue = v;
//             };
//
//             bool success = dictionary.Remove("Milk");
//             Assert.IsTrue(stateChanged);
//             Assert.IsTrue(success);
//             Assert.IsFalse(dictionary.ContainsKey("Milk"));
//             Assert.AreEqual("Milk", removedKey);
//             Assert.AreEqual(5, removedValue);
//
//             stateChanged = false;
//             removedKey = null;
//             removedValue = -1;
//
//             success = dictionary.Remove("Cherry");
//             Assert.IsFalse(success);
//             Assert.IsFalse(stateChanged);
//             Assert.IsNull(removedKey);
//             Assert.AreEqual(-1, removedValue);
//
//             stateChanged = false;
//             removedKey = null;
//             removedValue = -1;
//
//             success = dictionary.Remove("Bread", out int value);
//             Assert.IsTrue(success);
//             Assert.AreEqual(3, value);
//             Assert.AreEqual("Bread", removedKey);
//             Assert.AreEqual(3, removedValue);
//
//             stateChanged = false;
//             removedKey = null;
//             removedValue = -1;
//
//             success = dictionary.Remove("Honey", out value);
//             Assert.IsNull(removedKey);
//             Assert.AreEqual(-1, removedValue);
//             Assert.IsFalse(success);
//             Assert.AreEqual(0, value);
//         }
//
//         [Test]
//         public void CopyTo()
//         {
//             //Arrange:
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5},
//                 {"Bread", 3},
//                 {"Butter", 2}
//             };
//
//             var array = new KeyValuePair<string, int>[3];
//             dictionary.CopyTo(array);
//
//             Assert.AreEqual(new KeyValuePair<string, int>[]
//             {
//                 new("Bread", 3),
//                 new("Butter", 2),
//                 new("Milk", 5)
//             }, array);
//         }
//
//         [Test]
//         public void WhenIsEmptyThenCleanNotHappened()
//         {
//             //Arrange:
//             var wasEvent = false;
//             var dictionary = new ReactiveSortedList<string, int>();
//
//             //Act:
//             dictionary.OnCleared += () => wasEvent = true;
//             dictionary.Clear();
//
//             Assert.IsFalse(wasEvent);
//         }
//
//         [Test]
//         public void Clear()
//         {
//             //Arrange:
//             var wasEvent = false;
//             var dictionary = new ReactiveSortedList<string, int>
//             {
//                 {"Milk", 5},
//                 {"Bread", 3},
//                 {"Butter", 2}
//             };
//
//             //Act:
//             dictionary.OnCleared += () => wasEvent = true;
//             dictionary.Clear();
//
//             Assert.IsTrue(wasEvent);
//             Assert.AreEqual(0, dictionary.Count);
//             Assert.AreEqual(Array.Empty<string>(), dictionary.Keys);
//             Assert.AreEqual(Array.Empty<int>(), dictionary.Values);
//         }
//     }
// }