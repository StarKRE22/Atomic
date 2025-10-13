# 🧩 IReadOnlyReactiveDictionary&lt;K, V&gt;

Represents a **read-only reactive key-value dictionary** that provides notifications when items are
added, removed, updated, or when the overall state changes. Use this interface when you need **read-only dictionary
access** but still want **reactive notifications** on changes.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#api-reference">API Reference</a>
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
            <li><a href="#containskeyk">ContainsKey(K)</a></li>
            <li><a href="#trygetvaluek-out-v">TryGetValue(K, out V)</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>

<!-- 
- [Example of Usage](#-example-of-usage)
- [API Reference](#api-reference)
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
        - [ContainsKey(K)](#containskeyk)
        - [TryGetValue(K, out V)](#trygetvaluek-out-v)
        - [GetEnumerator()](#getenumerator)
-->
---

## 🗂 Example of Usage

```csharp
// Get a reactive dictionary
IReadOnlyReactiveDictionary<string, int> dictionary = ...;

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
public interface IReadOnlyReactiveDictionary<K, V> : 
    IReadOnlyDictionary<K, V>,
    IReadOnlyReactiveCollection<KeyValuePair<K, V>>
```

- **Description:** Represents a **read-only reactive key-value dictionary** that provides notifications when items are
  added, removed, updated, or when the overall state changes.
- **Inheritance:**
  `IReadOnlyDictionary<K, V>`, [IReadOnlyReactiveCollection<KeyValuePair<K, V>>](IReadOnlyReactiveCollection.md).
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
public V this[K key] { get; }
```

- **Description:** Gets the value associated with the specified key.
- **Parameters:** `key` — the key to locate in the dictionary.
- **Returns:** `V` — the value corresponding to the key.
- **Exception:** Throws `KeyNotFoundException` if the key does not exist.

---

### 🏹 Methods

#### `ContainsKey(K)`

```csharp
public bool ContainsKey(K key);
```

- **Description:** Checks whether the dictionary contains a specific key.
- **Parameters:** `key` — the key to locate in the dictionary.
- **Returns:** `true` if the key exists; otherwise `false`.

#### `TryGetValue(K, out V)`

```csharp
public bool TryGetValue(K key, out V value);
```

- **Description:** Attempts to get the value associated with the specified key without throwing an exception.
- **Parameters:**
    - `key` — the key to look up.
    - `value` — outputs the value associated with the key if found; otherwise, the default value for type `V`.
- **Returns:** `true` if the key exists and value is returned; otherwise, `false`.

#### `GetEnumerator()`

```csharp
public IEnumerator<KeyValuePair<K, V>> GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the key-value pairs in the dictionary.
- **Returns:** An `IEnumerator<KeyValuePair<K, V>>` for iterating over the dictionary entries.