# 🧩 IReactiveDictionary&lt;K, V&gt;

Represents a **reactive key-value dictionary** that supports notifications when items are added, removed, updated, or
when the overall state changes. Use this interface when you need **mutable dictionary access** with **reactive
notifications** for all
changes.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
            <li><a href="#onitemadded">OnItemAdded</a></li>
            <li><a href="#onitemremoved">OnItemRemoved</a></li>
            <li><a href="#onitemchanged">OnItemChanged</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#count">Count</a></li>
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
            <li><a href="#removek">Remove(K)</a></li>
            <li><a href="#removekeyvaluepairk-v">Remove(KeyValuePair&lt;K, V&gt;)</a></li>
            <li><a href="#containskeyk">ContainsKey(K)</a></li>
            <li><a href="#containskeyvaluepairk-v">Contains(KeyValuePair&lt;K, V&gt;)</a></li>
            <li><a href="#trygetvaluek-out-v">TryGetValue(K, out V)</a></li>
            <li><a href="#copytokeyvaluepairk-v-int">CopyTo(KeyValuePair&lt;K, V&gt;[], int)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
            <li><a href="#dispose">Dispose()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>


<!--
- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
        - [OnItemAdded](#onitemadded)
        - [OnItemRemoved](#onitemremoved)
        - [OnItemChanged](#onitemchanged)
    - [Properties](#-properties)
        - [Count](#count)
        - [Keys](#keys)
        - [Values](#values)
    - [Indexers](#-indexers)
        - [[K key]](#k-key)
    - [Methods](#-methods)
        - [Add(K, V)](#addk-v)
        - [Add(KeyValuePair<K, V>)](#addkeyvaluepairk-v)
        - [Remove(K)](#removek)
        - [Remove(KeyValuePair<K, V>)](#removekeyvaluepairk-v)
        - [ContainsKey(K)](#containskeyk)
        - [Contains(KeyValuePair<K, V>)](#containskeyvaluepairk-v)
        - [TryGetValue(K, out V)](#trygetvaluek-out-v)
        - [CopyTo(KeyValuePair<K, V>[], int)](#copytokeyvaluepairk-v-int)
        - [Clear()](#clear)
        - [GetEnumerator()](#getenumerator)
        - [Dispose()](#dispose)
-->
---

## 🗂 Example of Usage

```csharp
// Get a reactive dictionary
IReactiveDictionary<string, int> dictionary = ...;

// Subscribe to events
dictionary.OnItemAdded += (key, value) =>
{
    Console.WriteLine($"Added: {key} => {value}");
};

dictionary.OnItemChanged += (key, value) =>
{
    Console.WriteLine($"Changed: {key} => {value}");
};

dictionary.OnItemRemoved += (key, value) =>
{
    Console.WriteLine($"Removed: {key} => {value}");
};

dictionary.OnStateChanged += () =>
{
    Console.WriteLine("Dictionary state changed.");
};

// Example: adding items
dictionary.Add("apple", 5);
dictionary.Add("banana", 3);

// Change an existing value
dictionary["apple"] = 10; // Triggers OnItemChanged

// Remove an item
dictionary.Remove("banana");

// Access elements using the indexer
if (dictionary.ContainsKey("apple"))
{
    int count = dictionary["apple"];
    Console.WriteLine($"Apple count: {count}");
}

// Iterate through the dictionary using foreach (read-only)
foreach (var kvp in dictionary)
{
    Console.WriteLine($"{kvp.Key} => {kvp.Value}");
}

// Try to get a value safely
if (dictionary.TryGetValue("orange", out int orangeCount))
{
    Console.WriteLine($"Orange count: {orangeCount}");
}
else
{
    Console.WriteLine("Orange is not in the dictionary.");
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IReactiveDictionary<K, V> : 
    IDictionary<K, V>,
    IReadOnlyReactiveDictionary<K, V>,
    IReactiveCollection<KeyValuePair<K, V>>
```

- **Description:** Represents a **reactive key-value dictionary** that supports notifications when items are added,
  removed, updated, or when the overall state changes.
- **Inheritance:**
  `IDictionary<K,V>`, [IReadOnlyReactiveDictionary<K, V>](IReadOnlyReactiveDictionary.md), [IReactiveCollection<KeyValuePair<K, V>>](IReactiveCollection.md)
- **Type Parameters:**
    - `K`  — The type of keys in the dictionary. Defines how items are identified and accessed.
    - `V` — The type of values stored in the dictionary. Represents the data associated with each key.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Triggered when the dictionary’s state changes globally (e.g., bulk update, clear).
- **Remarks:** Useful for reacting to any modifications in the dictionary without subscribing to individual item events.

#### `OnItemAdded`

```csharp
public event Action<K, V> OnItemAdded;
```

- **Description:** Triggered when a new key-value pair is added to the dictionary.
- **Parameters:**
    - `K` — the key that was added.
    - `V` — the value associated with the key.

#### `OnItemRemoved`

```csharp
public event Action<K, V> OnItemRemoved;
```

- **Description:** Triggered when a key-value pair is removed from the dictionary.
- **Parameters:**
    - `K` — the key that was removed.
    - `V` — the value associated with the removed key.

#### `OnItemChanged`

```csharp
public event Action<K, V> OnItemChanged;
```

- **Description:** Triggered when the value of an existing key changes.
- **Parameters:**
    - `K` — the key whose value was changed.
    - `V` — the new value of the key.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of key-value pairs in the dictionary.

#### `Keys`

```csharp
public IEnumerable<K> Keys { get; }
```

- **Description:** Gets a collection containing all the keys in the dictionary.

#### `Values`

```csharp
public IEnumerable<V> Values { get; }
```

- **Description:** Gets a collection containing all the values in the dictionary.

---

<div id="-indexers"></div>

### 🏷️ Indexers

#### `[K key]`

```csharp
public V this[K key] { get; set; }
```

- **Description:** Gets or sets the value associated with the specified key.
- **Parameters:** `key` — the key to locate in the dictionary.
- **Returns:** `V` — the value corresponding to the key.
- **Remarks:** Setting a value triggers `OnItemChanged` if the key already exists, or `OnItemAdded` if it's new.

---

### 🏹 Methods

#### `Add(K, V)`

```csharp
public void Add(K key, V value);
```

- **Description:** Adds a new key-value pair to the dictionary.
- **Parameters:**
    - `key` — the key to add.
    - `value` — the value associated with the key.
- **Exceptions:** Throws an exception if the key already exists.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged`.

#### `Add(KeyValuePair<K, V>)`

```csharp
public void Add(KeyValuePair<K, V> item);
```

- **Description:** Adds a key-value pair to the dictionary.
- **Parameter:** `item` — A `KeyValuePair<K, V>` representing the key and value to add.
- **Exceptions:** Throws if the key already exists or the value is null.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged`.
- **Remarks:** Internally calls `Add(K key, V value)`.

#### `Remove(K)`

```csharp
public bool Remove(K key);
```

- **Description:** Removes the value with the specified key from the dictionary.
- **Parameters:** `key` — the key of the element to remove.
- **Returns:** `true` if the element was successfully removed; otherwise, `false`.
- **Events:** Triggers `OnItemRemoved` and `OnStateChanged`.

#### `Remove(KeyValuePair<K, V>)`

```csharp
public bool Remove(KeyValuePair<K, V> item);
```

- **Description:** Removes the specified key-value pair from the dictionary.
- **Parameter:** `item` — The `KeyValuePair<K, V>` to remove.
- **Returns:** `true` if the item was removed; otherwise, `false`.
- **Events:** Triggers `OnItemRemoved` and `OnStateChanged` if removal is successful.
- **Remarks:** Only removes the pair if both the key exists and the value matches.

#### `ContainsKey(K)`

```csharp
public bool ContainsKey(K key);
```

- **Description:** Checks whether the dictionary contains a specific key.
- **Parameters:** `key` — the key to locate in the dictionary.
- **Returns:** `true` if the key exists; otherwise `false`.

#### `Contains(KeyValuePair<K, V>)`

```csharp
public bool Contains(KeyValuePair<K, V> item);
```

- **Description:** Determines whether the dictionary contains the specified key-value pair.
- **Parameter:** `item` — The `KeyValuePair<K, V>` to locate.
- **Returns:** `true` if the pair exists; otherwise, `false`.
- **Remarks:** Both key and value must match for the pair to be considered present.

#### `TryGetValue(K, out V)`

```csharp
public bool TryGetValue(K key, out V value);
```

- **Description:** Attempts to get the value associated with the specified key without throwing an exception.
- **Parameters:**
    - `key` — the key to look up.
    - `value` — outputs the value associated with the key if found; otherwise, the default value for type `V`.
- **Returns:** `true` if the key exists and value is returned; otherwise, `false`.

#### `CopyTo(KeyValuePair<K, V>[], int)`

```csharp
public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0);
```

- **Description:** Copies the elements of the dictionary to a provided array, starting at the specified index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The zero-based index in the array at which copying begins.
- **Exceptions:**
    - Throws `ArgumentNullException` if `array` is null.
    - Throws `ArgumentOutOfRangeException` if `arrayIndex` is less than 0.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all key-value pairs from the dictionary.
- **Events:** Triggers `OnItemRemoved` for each item and `OnStateChanged`.

#### `GetEnumerator()`

```csharp
public IEnumerator<KeyValuePair<K, V>> GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the key-value pairs in the dictionary.
- **Returns:** An `IEnumerator<KeyValuePair<K, V>>` for iterating over the dictionary entries.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Releases all resources used by the dictionary.
- **Remarks:**
    - Clears the dictionary.
    - Unsubscribes all event handlers.
    - After calling `Dispose`, the dictionary is empty and no longer raises events.