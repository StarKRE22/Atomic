# 🧩 IReadOnlyReactiveDictionary&lt;K, V&gt;

`IReadOnlyReactiveDictionary<K, V>` represents a **read-only reactive key-value dictionary** that provides notifications when items are added, removed, updated, or when the overall state changes. It extends [`IReadOnlyDictionary<K, V>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2) and [`IReadOnlyReactiveCollection<KeyValuePair<K, V>>`](IReadOnlyReactiveCollection.md).

> [!NOTE]  
> Use this interface when you need **read-only dictionary access** but still want **reactive notifications** on changes.

---

## Events

#### `OnStateChanged`
```csharp
event Action OnStateChanged;
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

## Properties

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

## Indexer

#### `[K key]`
```csharp
public V this[K key] { get; }
```
- **Description:** Gets the value associated with the specified key.
- **Parameters:** `key` — the key to locate in the dictionary.
- **Returns:** `V` — the value corresponding to the key.
- **Exception:** Throws `KeyNotFoundException` if the key does not exist.

---

## Methods

#### `bool ContainsKey(K)`
```csharp
public bool ContainsKey(K key);
```
- **Description:** Checks whether the dictionary contains a specific key.
- **Parameters:** `key` — the key to locate in the dictionary.
- **Returns:** `true` if the key exists; otherwise `false`.

#### `bool TryGetValue(K, out V)`
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