# 🧩 ReactiveDictionary<K, V>

Represents a **reactive key-value dictionary** that provides notifications when items are added,
removed, or updated. Use this class when you need a dictionary with full read / write access and **reactive
notifications** on changes.

---

## 📑 Table of Contents

<ul>
  <li>
    <a href="#-examples-of-usage">Examples of Usage</a>
    <ul>
      <li><a href="#ex-1">Basic Usage</a></li>
      <li><a href="#ex-2">Using TryAdd</a></li>
      <li><a href="#ex-3">Removing Elements</a></li>
      <li><a href="#ex-4">Iterating Keys and Values</a></li>
      <li><a href="#ex-5">Subscribing to Events</a></li>
      <li><a href="#ex-6">Initializing from Collections</a></li>
    </ul>
  </li>
  <li><a href="#-inspector-settings">Inspector Settings</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-constructors">Constructors</a></summary>
          <ul>
            <li><a href="#reactivedictionaryint">ReactiveDictionary(int)</a></li>
            <li><a href="#reactivedictionaryienumerablekeyvaluepairk-v">ReactiveDictionary(IEnumerable&lt;KeyValuePair&lt;K, V&gt;&gt;)</a></li>
            <li><a href="#reactivedictionaryienumerablek-v">ReactiveDictionary(IEnumerable&lt;(K, V)&gt;)</a></li>
            <li><a href="#reactivedictionaryparams-keyvaluepairk-v">ReactiveDictionary(params KeyValuePair&lt;K, V&gt;[])</a></li>
            <li><a href="#reactivedictionaryparams-k-v">ReactiveDictionary(params (K, V)[])</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
            <li><a href="#onitemchanged">OnItemChanged</a></li>
            <li><a href="#onitemadded">OnItemAdded</a></li>
            <li><a href="#onitemremoved">OnItemRemoved</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#count">Count</a></li>
            <li><a href="#isreadonly">IsReadOnly</a></li>
            <li><a href="#keys">Keys</a></li>
            <li><a href="#values">Values</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-indexers">Indexers</a></summary>
          <ul>
            <li><a href="#k-key">[K key]</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#addk-v">Add(K, V)</a></li>
            <li><a href="#addkeyvaluepairk-v">Add(KeyValuePair&lt;K, V&gt;)</a></li>
            <li><a href="#tryaddkeyvaluepairk-v">TryAdd(KeyValuePair&lt;K, V&gt;)</a></li>
            <li><a href="#tryaddk-v">TryAdd(K, V)</a></li>
            <li><a href="#removek">Remove(K)</a></li>
            <li><a href="#removek-out-v">Remove(K, out V)</a></li>
            <li><a href="#removekeyvaluepairk-v">Remove(KeyValuePair&lt;K, V&gt;)</a></li>
            <li><a href="#containskeyk">ContainsKey(K)</a></li>
            <li><a href="#containskeyvaluepairk-v">Contains(KeyValuePair&lt;K, V&gt;)</a></li>
            <li><a href="#trygetvaluek-out-v">TryGetValue(K, out V)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#copytokeyvaluepairk-v-int">CopyTo(KeyValuePair&lt;K, V&gt;[], int)</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-nested-types">Nested Types</a></summary>
          <ul>
            <li><a href="#keycollection">KeyCollection</a></li>
            <li><a href="#valuecollection">ValueCollection</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
  <li><a href="#-useful-links">Useful Links</a></li>
</ul>


<!--
- [Examples of Usage](#-examples-of-usage)
    - [Basic Usage](#ex-1)
    - [Using TryAdd](#ex-2)
    - [Removing Elements](#ex-3)
    - [Iterating Keys and Values](#ex-4)
    - [Subscribing to Events](#ex-5)
    - [Initializing from Collections](#ex-6)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [ReactiveDictionary(int)](#reactivedictionaryint)
        - [ReactiveDictionary(IEnumerable\<KeyValuePair<K, V>>)](#reactivedictionaryienumerablekeyvaluepairk-v)
        - [ReactiveDictionary(IEnumerable\<(K, V)>)](#reactivedictionaryienumerablek-v)
        - [ReactiveDictionary(params KeyValuePair\<K, V>[])](#reactivedictionaryparams-keyvaluepairk-v)
        - [ReactiveDictionary(params (K, V)[])](#reactivedictionaryparams-k-v)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
        - [OnItemChanged](#onitemchanged)
        - [OnItemAdded](#onitemadded)
        - [OnItemRemoved](#onitemremoved)
    - [Properties](#-properties)
        - [Count](#count)
        - [IsReadOnly](#isreadonly)
        - [Keys](#keys)
        - [Values](#values)
    - [Indexers](#-indexers)
        - [[K key]](#k-key)
    - [Methods](#-methods)
        - [Add(K, V)](#addk-v)
        - [Add(KeyValuePair<K, V>)](#addkeyvaluepairk-v)
        - [TryAdd(KeyValuePair<K, V>)](#tryaddkeyvaluepairk-v)
        - [TryAdd(K, V)](#tryaddk-v)
        - [Remove(K)](#removek)
        - [Remove(K, out V)](#removek-out-v)
        - [Remove(KeyValuePair<K, V>)](#removekeyvaluepairk-v)
        - [ContainsKey(K)](#containskeyk)
        - [Contains(KeyValuePair<K, V>)](#containskeyvaluepairk-v)
        - [TryGetValue(K, out V)](#trygetvaluek-out-v)
        - [Clear()](#clear)
        - [CopyTo(KeyValuePair<K, V>[], int)](#copytokeyvaluepairk-v-int)
        - [GetEnumerator()](#getenumerator)
    - [Nested Types](#-nested-types)
        - [KeyCollection](#keycollection)
        - [ValueCollection](#valuecollection)
- [Useful Links](#-useful-links)
-->
---

## 🗂 Examples of Usage

Below are examples of using `ReactiveDictionary` in different scenarios:

### 1️⃣ Basic Usage <div id="ex-1"></div>

```csharp
var dict = new ReactiveDictionary<string, int>();

dict.Add("One", 1);
dict.Add("Two", 2);

Console.WriteLine(dict["One"]); // Output: 1

dict["Two"] = 22; // Updates the value
Console.WriteLine(dict["Two"]); // Output: 22
```

---

### 2️⃣ Using TryAdd <div id="ex-2"></div>

```csharp
var dict = new ReactiveDictionary<string, int>();

bool added = dict.TryAdd("One", 1); // true
added = dict.TryAdd("One", 11);     // false, key already exists

Console.WriteLine(dict["One"]); // Output: 1
```

---

### 3️⃣ Removing Elements <div id="ex-3"></div>

```csharp
var dict = new ReactiveDictionary<string, int>
{
    { "A", 1 },
    { "B", 2 }
};

bool removed = dict.Remove("A"); // true
removed = dict.Remove("C");      // false, key does not exist

if (dict.Remove("B", out int value))
{
    Console.WriteLine(value); // Output: 2
}
```

---

### 4️⃣ Iterating Keys and Values <div id="ex-4"></div>

```csharp
var dict = new ReactiveDictionary<string, int>
{
    { "X", 10 },
    { "Y", 20 }
};

// Iterate over keys without allocation
foreach (var key in dict.Keys)
{
    Console.WriteLine(key); // X, Y
}

// Iterate over values without allocation
foreach (var val in dict.Values)
{
    Console.WriteLine(val); // 10, 20
}

// Iterate over key-value pairs without allocation
foreach (var kv in dict)
{
    Console.WriteLine($"{kv.Key}: {kv.Value}");
}
```

---

### 5️⃣ Subscribing to Events <div id="ex-5"></div>

```csharp
var dict = new ReactiveDictionary<string, int>();

dict.OnItemAdded += (key, value) => Console.WriteLine($"Added {key}={value}");
dict.OnItemChanged += (key, value) => Console.WriteLine($"Changed {key}={value}");
dict.OnItemRemoved += (key, value) => Console.WriteLine($"Removed {key}={value}");

dict.Add("A", 1);    // Output: Added A=1
dict["A"] = 100;     // Output: Changed A=100
dict.Remove("A");    // Output: Removed A=100
```

---

### 6️⃣ Initializing from Collections <div id="ex-6"></div>

```csharp
var dictFromPairs = new ReactiveDictionary<string, int>(new List<KeyValuePair<string, int>>
{
    new("One", 1),
    new("Two", 2)
});

var dictFromTuples = new ReactiveDictionary<string, int>(new (string, int)[]
{
    ("Three", 3),
    ("Four", 4)
});
```

---

## 🛠 Inspector Settings

| Parameter         | Description                                      |
|-------------------|--------------------------------------------------|
| `serializedItems` | The initial elements of the reactive dictionary. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class ReactiveDictionary<K, V> : IReactiveDictionary<K, V>, IDisposable, ISerializationCallbackReceiver
```

- **Description:** Represents a **reactive key-value dictionary** that provides notifications when items are added,
  removed, or updated.
- **Inheritance:** [IReactiveDictionary<K, V>](IReactiveDictionary.md)
- **Type Parameters:**
    - `K`  — The type of keys in the dictionary. Defines how items are identified and accessed.
    - `V` — The type of values stored in the dictionary. Represents the data associated with each key.
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `ReactiveDictionary(int)`

```csharp
public ReactiveDictionary(int capacity = 0);
```

- **Description:** Initializes a new instance of the `ReactiveDictionary<K,V>` class with the specified capacity.
- **Parameter:** `capacity` — The initial number of elements the dictionary can contain. Must be non-negative.
- **Exception:** Throws `ArgumentOutOfRangeException` if `capacity` is less than 0.
- **Remarks:** The actual internal capacity is adjusted to the nearest prime number greater than or equal to `capacity`.

#### `ReactiveDictionary(IEnumerable<KeyValuePair<K, V>>)`

```csharp
public ReactiveDictionary(IEnumerable<KeyValuePair<K, V>> source);
```

- **Description:** Initializes a new instance of the dictionary and populates it with elements from the specified
  collection of key-value pairs.
- **Parameter:** `source` — The sequence of key-value pairs to copy into the dictionary. Cannot be null.
- **Exceptions:**
    - Throws `ArgumentNullException` if `source` is null.
    - Throws `ArgumentException` if `source` contains duplicate keys.
- **Remarks:** The dictionary is initialized with a capacity equal to the number of elements in `source`.

#### `ReactiveDictionary(IEnumerable<(K, V)>)`

```csharp
public ReactiveDictionary(IEnumerable<(K, V)> source);
```

- **Description:** Initializes a new instance of the dictionary and populates it with elements from the specified
  sequence of tuples.
- **Parameter:** `source` — The sequence of `(key, value)` tuples to copy into the dictionary. Cannot be null.
- **Exceptions:**
    - Throws `ArgumentNullException` if `source` is null.
    - Throws `ArgumentException` if `source` contains duplicate keys.
- **Remarks:** The dictionary is initialized with a capacity equal to the number of elements in `source`.

#### `ReactiveDictionary(params KeyValuePair<K, V>[])`

```csharp
public ReactiveDictionary(params KeyValuePair<K, V>[] source);
```

- **Description:** Initializes a new instance of the dictionary and populates it with elements from the specified array
  of key-value pairs.
- **Parameter:** `source` — An array of key-value pairs to copy into the dictionary. Cannot be null.
- **Exceptions:**
    - Throws `ArgumentNullException` if `source` is null.
    - Throws `ArgumentException` if `source` contains duplicate keys.
- **Remarks:** This constructor supports convenient inline initialization with `KeyValuePair` arguments.

#### `ReactiveDictionary(params (K, V)[])`

```csharp
public ReactiveDictionary(params (K, V)[] source);
```

- **Description:** Initializes a new instance of the dictionary and populates it with elements from the specified array
  of `(key, value)` tuples.
- **Parameter:** `source` — An array of `(key, value)` tuples to copy into the dictionary. Cannot be null.
- **Exceptions:**
    - Throws `ArgumentNullException` if `source` is null.
    - Throws `ArgumentException` if `source` contains duplicate keys.
- **Remarks:** This constructor supports convenient inline initialization with tuple arguments.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Triggered when the dictionary’s state changes globally (e.g., bulk update, clear).

#### `OnItemChanged`

```csharp
public event Action<K, V> OnItemChanged;
```

- **Description:** Triggered when the value of an existing key changes.
- **Parameters:**
    - `key` — the key that changed.
    - `value` — the new value of the key.

#### `OnItemAdded`

```csharp
public event Action<K, V> OnItemAdded;
```

- **Description:** Triggered when a new key-value pair is added.
- **Parameters:**
    - `key` — the added key.
    - `value` — the value of the added key.

#### `OnItemRemoved`

```csharp
public event Action<K, V> OnItemRemoved;
```

- **Description:** Triggered when a key-value pair is removed.
- **Parameters:**
    - `key` — the removed key.
    - `value` — the value of the removed key.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the total number of key-value pairs in the dictionary.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Always returns `false` for `ReactiveDictionary` since it supports full modification.

#### `Keys`

```csharp
public ReadOnlyKeyCollection Keys { get; }
```

- **Description:** Gets a read-only collection of all keys.

#### `Values`

```csharp
public ReadOnlyValueCollection Values { get; }
```

- **Description:** Gets a read-only collection of all values.

---

### 🏷️ Indexers

#### `[K key]`

```csharp
public V this[K key] { get; set; }
```

- **Description:** Gets or sets the value associated with the specified key.
- **Exceptions:** Throws `KeyNotFoundException` if key does not exist when getting.
- **Events:** Triggers `OnItemChanged` (if updating) or `OnItemAdded` (if adding).

---

### 🏹 Methods

#### `Add(K, V)`

```csharp
public void Add(K key, V value);
```

- **Description:** Adds a new key-value pair to the dictionary.
- **Parameters:**
    - `key` — The key to add. Cannot be null.
    - `value` — The value associated with the key. Cannot be null.
- **Exceptions:**
    - Throws `ArgumentNullException` if `value` is null.
    - Throws `ArgumentException` if the key already exists.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged`.
- **Remarks:** Use this method to insert new entries.

#### `Add(KeyValuePair<K, V>)`

```csharp
public void Add(KeyValuePair<K, V> item);
```

- **Description:** Adds a new key-value pair to the dictionary.
- **Parameter:** `item` — The `KeyValuePair<K, V>` to add. Cannot be null.
- **Exceptions:**
    - Throws `ArgumentNullException` if `item.Value` is null.
    - Throws `ArgumentException` if `item.Key` already exists.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged`.
- **Remarks:** This is an overload of <see cref="Add(K,V)"/> that accepts a `KeyValuePair`.

#### `TryAdd(KeyValuePair<K, V>)`

```csharp
public bool TryAdd(KeyValuePair<K, V> item);
```

- **Description:** Attempts to add the specified `KeyValuePair<K, V>` to the dictionary.
- **Parameter:** `item` — The key/value pair to add. The key must be unique within the dictionary.
- **Returns:**
    - `true` if the key/value pair was added successfully.
    - `false` if the key already exists in the dictionary.
- **Remarks:** This method does not throw an exception if the key already exists, unlike `Add(K,V)`.

#### `TryAdd(K, V)`

```csharp
public bool TryAdd(K key, V value);
```

- **Description:** Attempts to add the specified key and value to the dictionary.
- **Parameters:**
    - `key` — The key of the element to add.
    - `value` — The value of the element to add. Cannot be null.
- **Returns:**
    - `true` if the key/value pair was added successfully.
    - `false` if the key already exists in the dictionary or if `value` is `null`.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged`.
- **Remarks:** This method does not throw an exception if the key already exists, unlike `Add(K,V)`.

#### `Remove(K)`

```csharp
public bool Remove(K key);
```

- **Description:** Removes the key-value pair with the specified key.
- **Parameters:**
    - `key` — The key of the element to remove.
- **Returns:** `true` if the element was successfully removed; otherwise `false`.
- **Events:** Triggers `OnItemRemoved` and `OnStateChanged`.
- **Remarks:** Does not throw an exception if the key does not exist.

#### `Remove(K, out V)`

```csharp
public bool Remove(K key, out V value);
```

- **Description:** Removes the key-value pair with the specified key and outputs the removed value.
- **Parameters:**
    - `key` — The key of the element to remove.
    - `value` — Outputs the value associated with the removed key if successful; otherwise, the default value.
- **Returns:** `true` if the element was successfully removed; otherwise `false`.
- **Events:** Triggers `OnItemRemoved` and `OnStateChanged`.
- **Remarks:** Provides the removed value for further processing.

#### `Remove(KeyValuePair<K, V>)`

```csharp
public bool Remove(KeyValuePair<K, V> item);
```

- **Description:** Removes the specified key/value pair from the dictionary.
- **Parameter:** `item` — The key/value pair to remove.
- **Returns:** `true` if the item was found and successfully removed; otherwise `false`.
- **Events:** Triggers `OnItemRemoved` and `OnStateChanged`.
- **Remarks:** The method checks for the existence of the exact key/value pair before removal.

#### `ContainsKey(K)`

```csharp
public bool ContainsKey(K key);
```

- **Description:** Determines whether the dictionary contains the specified key.
- **Parameters:**
    - `key` — The key to locate in the dictionary.
- **Returns:** `true` if the key exists; otherwise `false`.
- **Remarks:** Use this method to check existence before accessing a key to avoid exceptions.

#### `Contains(KeyValuePair<K, V>)`

```csharp
public bool Contains(KeyValuePair<K, V> item);
```

- **Description:** Determines whether the dictionary contains the specified key/value pair.
- **Parameter:** `item` — The key/value pair to locate.
- **Returns:** `true` if the dictionary contains an element with the specified key and value; otherwise `false`.
- **Remarks:** Comparison of values is performed using the dictionary's equality comparer.

#### `TryGetValue(K, out V)`

```csharp
public bool TryGetValue(K key, out V value);
```

- **Description:** Attempts to get the value associated with the specified key without throwing an exception.
- **Parameters:**
    - `key` — The key to look up.
    - `value` — Outputs the value if found; otherwise, the default value of type `V`.
- **Returns:** `true` if the key exists; otherwise `false`.
- **Remarks:** Preferred method for safe retrieval of values.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all key-value pairs from the dictionary.
- **Events:** Triggers `OnItemRemoved` for each item and `OnStateChanged`.
- **Remarks:** Resets the internal state. Use with caution as all data is lost.

#### `CopyTo(KeyValuePair<K, V>[], int)`

```csharp
public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0);
```

- **Description:** Copies the elements of the dictionary to the specified array, starting at the specified index.
- **Parameters:**
    - `array` — The destination array. Cannot be null.
    - `arrayIndex` — The zero-based index in the array at which copying begins. Must be non-negative.
- **Exceptions:**
    - Throws `ArgumentNullException` if `array` is null.
    - Throws `ArgumentOutOfRangeException` if `arrayIndex` is less than 0.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the dictionary.
- **Returns:** An `Enumerator` struct for iterating over key-value pairs.
- **Remarks:** Use `foreach` for iteration. Avoid modifying the dictionary while enumerating.

---

### 🧩 Nested Types

#### `KeyCollection`

```csharp
public readonly struct ReadOnlyKeyCollection : ICollection<K>
```

- **Description:** Represents a read-only collection of keys
- **Inheritance:** `ICollection<T>`
- **See also:** [ReadOnlyKeyCollection Documentation](ReactiveDictionaryKeyCollection.md)

#### `ValueCollection`

```csharp
public readonly struct ReadOnlyValueCollection : ICollection<V>
```

- **Description:** <b>Represents a read-only collection of values</b>.
- **Inheritance:** `ICollection<T>`
- **See also:** [ReadOnlyValueCollection Documentation](ReactiveDictionaryValueCollection.md)

---

## 🔗 Useful Links

- [ReactiveDictionary Performance](../Performance/ReactiveDictionaryPerformance.md) – performance benchmarks for
  reactive list.
- [Iterating over Reactive Collections](../../BestPractices/IteratingReactiveCollections.md) — best practice.