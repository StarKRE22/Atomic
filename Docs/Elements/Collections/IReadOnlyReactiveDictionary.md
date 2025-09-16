# 🧩 `IReadOnlyReactiveDictionary<K, V>`

`IReadOnlyReactiveDictionary<K, V>` represents a **read-only reactive dictionary** that provides notifications when items are added, removed, updated, or when the overall state changes. It extends `IReadOnlyDictionary<K, V>`.

> [!NOTE]  
> Use this interface when you need **read-only access** to a dictionary but still require **reactive notifications** on changes.

---

## Events

### `OnStateChanged`
```csharp
event StateChangedHandler OnStateChanged;
```
- **Description:** Triggered when the dictionary’s state changes globally (e.g., bulk update, clear).
- **Remarks:** Useful for reacting to any modifications in the dictionary without subscribing to individual item events.

### `OnItemChanged`
```csharp
event SetItemHandler<K, V> OnItemChanged;
```
- **Description:** Triggered when an existing key's value is updated.
- **Parameters:**
    - `key` — the key of the updated item.
    - `value` — the new value assigned to the key.
- **Remarks:** Allows monitoring changes to specific keys in the dictionary.

### `OnItemAdded`
```csharp
event AddItemHandler<K, V> OnItemAdded;
```
- **Description:** Triggered when a new key-value pair is added to the dictionary.
- **Parameters:**
    - `key` — the key that was added.
    - `value` — the value associated with the added key.
- **Remarks:** Useful to react only to newly inserted entries.

### `OnItemRemoved`
```csharp
event RemoveItemHandler<K, V> OnItemRemoved;
```
- **Description:** Triggered when an existing key-value pair is removed from the dictionary.
- **Parameters:**
    - `key` — the key that was removed.
    - `value` — the value associated with the removed key.
- **Remarks:** Allows monitoring deletions from the dictionary.

---

## Properties

#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the total number of key-value pairs contained in the dictionary.
- **Returns:** The number of elements as an integer.

#### `Keys`
```csharp
public IEnumerable<K> Keys { get; }
```
- **Description:** Returns an enumerable collection of all keys in the dictionary.
- **Returns:** An `IEnumerable<K>` representing the dictionary’s keys.

#### `Values`
```csharp
public IEnumerable<V> Values { get; }
```
- **Description:** Returns an enumerable collection of all values in the dictionary.
- **Returns:** An `IEnumerable<V>` representing the dictionary’s values.

## Indexer

#### `V this[K key]`
```csharp
public V this[K key] { get; }
```
- **Description:** Gets the value associated with the specified key.
- **Parameters:** `key` — the key whose value to retrieve.
- **Returns:** The value of type `V` associated with the given key.
- **Exceptions:** Throws `KeyNotFoundException` if the key does not exist.

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
