# 🧩 Reactive Collections

Provide a set of **reactive collection types** such as arrays, lists, dictionaries, and sets. These collections
automatically notify subscribers of changes, making them ideal for **data binding, UI updates, and reactive
programming**. Both read-only and fully mutable reactive collections are supported, allowing fine-grained control over
data access and modification.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Reactive Array](#-reactive-array)
    - [Reactive List](#-reactive-list)
    - [Reactive Dictionary](#-reactive-dictionary)
    - [Reactive Hash Set](#-reactive-hash-set)
- [API Reference](#-api-reference)
- [Performance](#-performance)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

Below are examples of using reactive collections depending on scenarios:

### 1️⃣ Reactive Array <div id="-reactive-array"></div>

```csharp
// Create a reactive array with initial values
var reactiveArray = new ReactiveArray<int>(1, 2, 3, 4);

// Subscribe to events
reactiveArray.OnItemChanged += (index, value) => Console.WriteLine($"Item {index} changed to {value}");
reactiveArray.OnStateChanged += () => Console.WriteLine("Array state changed");

// Access and modify elements
reactiveArray[1] = 20; // Triggers OnItemChanged and OnStateChanged

// Fill all elements
reactiveArray.Fill(10);

// Populate new values
reactiveArray.Populate(new int[] { 5, 6, 7, 8 });

// Resize the array
reactiveArray.Resize(6); // Adds two default elements

// Clear the array
reactiveArray.Clear();

// Iterate through elements
foreach (var item in reactiveArray)
{
    Console.WriteLine(item);
}

// Copy to standard array
int[] target = new int[reactiveArray.Length];
reactiveArray.Copy(0, target, 0, reactiveArray.Length);
```

---

### 2️⃣ Reactive List <div id="-reactive-list"></div>

```csharp
var list = new ReactiveList<string>();

// Subscribe to events
list.OnItemInserted += (i, v) => Console.WriteLine($"Inserted {v} at {i}");
list.OnItemDeleted += (i, v) => Console.WriteLine($"Deleted {v} from {i}");
list.OnItemChanged += (i, v) => Console.WriteLine($"Changed {i} to {v}");
list.OnStateChanged += () => Console.WriteLine("State changed");

// Add items
list.Add("A");
list.Add("B");

// Insert
list.Insert(1, "X");

// Modify
list[0] = "C";

// Remove
list.Remove("B");

// Enumerate
foreach (var item in list)
    Console.WriteLine(item);
```

---

### 3️⃣ Reactive Dictionary <div id="-reactive-dictionary"></div>

```csharp
var dict = new ReactiveDictionary<string, int>
{
    { "One", 1 },
    { "Two", 2 }
};

// Basic usage
Console.WriteLine(dict["One"]); // Output: 1
dict["Two"] = 22;
Console.WriteLine(dict["Two"]); // Output: 22

// TryAdd
bool added = dict.TryAdd("Three", 3);  // true
added = dict.TryAdd("Three", 33);      // false

// Remove elements
bool removed = dict.Remove("One"); // true
if (dict.Remove("Two", out int removedValue))
{
    Console.WriteLine($"Removed value: {removedValue}"); // Output: 22
}

// Subscribing to events
dict.OnItemAdded += (key, value) => Console.WriteLine($"Added {key}={value}");
dict.OnItemChanged += (key, value) => Console.WriteLine($"Changed {key}={value}");
dict.OnItemRemoved += (key, value) => Console.WriteLine($"Removed {key}={value}");

// Trigger events
dict.Add("A", 10);   // Added A=10
dict["A"] = 100;     // Changed A=100
dict.Remove("A");    // Removed A=100

// Iteration
dict["X"] = 5;
dict["Y"] = 15;

Console.WriteLine("Keys:");
foreach (var key in dict.Keys)
    Console.WriteLine(key);

Console.WriteLine("Values:");
foreach (var val in dict.Values)
    Console.WriteLine(val);

Console.WriteLine("Key-Value pairs:");
foreach (var kv in dict)
    Console.WriteLine($"{kv.Key}: {kv.Value}");
```

---

### 4️⃣ Reactive Hash Set <div id="-reactive-hash-set"></div>

```csharp

```csharp
IReactiveSet<string> reactiveSet = new ReactiveHashSet<string>();

// Subscribe to events
reactiveSet.OnItemAdded += item => Console.WriteLine($"Added: {item}");
reactiveSet.OnItemRemoved += item => Console.WriteLine($"Removed: {item}");
reactiveSet.OnStateChanged += () => Console.WriteLine("Set state changed.");

// Adding items
reactiveSet.Add("Apple");   // Output: Added: Apple
reactiveSet.Add("Banana");  // Output: Added: Banana

// Attempt to add an existing item
bool added = reactiveSet.Add("Apple"); // false, already exists

// Check if an item exists
if (reactiveSet.Contains("Banana"))
{
    Console.WriteLine("Banana is in the set."); // Output: Banana is in the set.
}

// Removing an item
reactiveSet.Remove("Banana"); // Output: Removed: Banana

// Union with another collection
reactiveSet.UnionWith(new[] { "Cherry", "Date", "Apple" });
// Output: Added: Cherry
// Output: Added: Date

// Intersect with another collection
reactiveSet.IntersectWith(new[] { "Apple", "Date", "Elderberry" });
// Output: Removed: Cherry

// Symmetric difference with another collection
reactiveSet.SymmetricExceptWith(new[] { "Date", "Fig", "Grape" });
// Output: Removed: Date
// Output: Added: Fig
// Output: Added: Grape

// Iterate through the set
foreach (var item in reactiveSet)
{
    Console.WriteLine($"Set item: {item}");
}

// Clear the set
reactiveSet.Clear();
// Output: Removed: Apple
// Output: Removed: Fig
// Output: Removed: Grape
// Output: Set state changed.
```

---

## 🔍 API Reference

- **Common**
    - [IReadOnlyReactiveCollection](IReadOnlyReactiveCollection.md) <!-- + -->
    - [IReactiveCollection](IReactiveCollection.md) <!-- + -->
- **ReactiveArrays**
    - [IReadOnlyReactiveArray](IReadOnlyReactiveArray.md) <!-- + -->
    - [IReactiveArray](IReactiveArray.md) <!-- + -->
    - [ReactiveArray](ReactiveArray.md) <!-- + -->
- **ReactiveLists**
    - [IReadOnlyReactiveList](IReadOnlyReactiveList.md) <!-- + -->
    - [IReactiveList](IReactiveList.md) <!-- + -->
    - [ReactiveList](ReactiveList.md) <!-- + -->
    - [ReactiveLinkedList](ReactiveLinkedList.md) <!-- + -->
- **ReactiveDictionaries**
    - [IReadOnlyReactiveDictionary](IReadOnlyReactiveDictionary.md) <!-- + -->
    - [IReactiveDictionary](IReactiveDictionary.md) <!-- + -->
    - [ReactiveDictionary](ReactiveDictionary.md) <!-- + -->
        - [KeyCollection](ReactiveDictionaryKeyCollection.md)
        - [ValueCollection](ReactiveDictionaryValueCollection.md)
- **ReactiveSets**
    - [IReactiveSet](IReactiveSet.md) <!-- + -->
    - [ReactiveHashSet](ReactiveHashSet.md) <!-- + -->

---

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**.

- [ReactiveArray](../Performance/ReactiveArrayPerformance.md) – performance benchmarks for reactive arrays.
- [ReactiveList](../Performance/ReactiveListPerformance.md) – performance benchmarks for reactive lists.
- [ReactiveLinkedList](../Performance/ReactiveLinkedListPerformance.md) – performance benchmarks for reactive linked
  lists.
- [ReactiveDictionary](../Performance/ReactiveDictionaryPerformance.md) – performance benchmarks for reactive
  dictionaries.
- [ReactiveHashSet](../Performance/ReactiveHashSetPerformance.md) – performance benchmarks for reactive hash sets.

---

## 📌 Best Practices

- [Iterating over Reactive Collections](../../BestPractices/IteratingReactiveCollections.md)