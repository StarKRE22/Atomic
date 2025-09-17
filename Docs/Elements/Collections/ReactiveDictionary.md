# 🧩 `ReactiveDictionary<K, V>`

`ReactiveDictionary<K, V>` is a **reactive key-value dictionary** that provides notifications when items are added, removed, or updated. It implements [IReactiveDictionary<K, V>](IReactiveDictionary.md).

> [!NOTE]  
> Use this class when you need a dictionary with full read/write access and **reactive notifications** on changes.

---

## Constructors

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
- **Description:** Initializes a new instance of the dictionary and populates it with elements from the specified collection of key-value pairs.
- **Parameter:** `source` — The sequence of key-value pairs to copy into the dictionary. Cannot be null.
- **Exceptions:**
  - Throws `ArgumentNullException` if `source` is null.
  - Throws `ArgumentException` if `source` contains duplicate keys.
- **Remarks:** The dictionary is initialized with a capacity equal to the number of elements in `source`.

#### `ReactiveDictionary(IEnumerable<(K, V)>)`
```csharp
public ReactiveDictionary(IEnumerable<(K, V)> source);
```
- **Description:** Initializes a new instance of the dictionary and populates it with elements from the specified sequence of tuples.
- **Parameter:** `source` — The sequence of `(key, value)` tuples to copy into the dictionary. Cannot be null.
- **Exceptions:**
  - Throws `ArgumentNullException` if `source` is null.
  - Throws `ArgumentException` if `source` contains duplicate keys.
- **Remarks:** The dictionary is initialized with a capacity equal to the number of elements in `source`.

#### `ReactiveDictionary(params KeyValuePair<K, V>[])`
```csharp
public ReactiveDictionary(params KeyValuePair<K, V>[] source);
```
- **Description:** Initializes a new instance of the dictionary and populates it with elements from the specified array of key-value pairs.
- **Parameter:** `source` — An array of key-value pairs to copy into the dictionary. Cannot be null.
- **Exceptions:**
  - Throws `ArgumentNullException` if `source` is null.
  - Throws `ArgumentException` if `source` contains duplicate keys.
- **Remarks:** This constructor supports convenient inline initialization with `KeyValuePair` arguments.

#### `ReactiveDictionary(params (K, V)[])`
```csharp
public ReactiveDictionary(params (K, V)[] source);
```
- **Description:** Initializes a new instance of the dictionary and populates it with elements from the specified array of `(key, value)` tuples.
- **Parameter:** `source` — An array of `(key, value)` tuples to copy into the dictionary. Cannot be null.
- **Exceptions:**
  - Throws `ArgumentNullException` if `source` is null.
  - Throws `ArgumentException` if `source` contains duplicate keys.
- **Remarks:** This constructor supports convenient inline initialization with tuple arguments.

---

## Events

#### `OnStateChanged`
```csharp
event StateChangedHandler OnStateChanged;
```
- **Description:** Triggered when the dictionary’s state changes globally (e.g., bulk update, clear).

#### `OnItemChanged`
```csharp
event SetItemHandler<K, V> OnItemChanged;
```
- **Description:** Triggered when the value of an existing key changes.
- **Parameters:**
  - `key` — the key that changed.
  - `value` — the new value of the key.

#### `OnItemAdded`
```csharp
event AddItemHandler<K, V> OnItemAdded;
```
- **Description:** Triggered when a new key-value pair is added.
- **Parameters:**
  - `key` — the added key.
  - `value` — the value of the added key.

#### `OnItemRemoved`
```csharp
event RemoveItemHandler<K, V> OnItemRemoved;
```
- **Description:** Triggered when a key-value pair is removed.
- **Parameters:**
  - `key` — the removed key.
  - `value` — the value of the removed key.

---

## Properties

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

## Indexer

#### `[K key]`
```csharp
public V this[K key] { get; set; }
```
- **Description:** Gets or sets the value associated with the specified key.
- **Exceptions:** Throws `KeyNotFoundException` if key does not exist when getting.
- **Events:** Triggers `OnItemChanged` (if updating) or `OnItemAdded` (if adding).

---

## Methods

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

#### `Remove(KeyValuePair<K, V> item)`
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

#### `CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0)`
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


## Nested Collections

<details>
  <summary>
    <h3>🧩 ReadOnlyKeyCollection</h3>
    <br> Represents a read-only collection of keys.
  </summary>

</details>

---

<details>
  <summary>
    <h3>🧩 ReadOnlyValueCollection</h3>
    <br> Represents a read-only collection of keys.
  </summary>

</details>





---

## Example of Usage

```
ReactiveDictionary<string, int> dict = new ReactiveDictionary<string, int>();

dict.OnItemAdded += (k, v) => Console.WriteLine($"Added: {k} => {v}");
dict.OnItemChanged += (k, v) => Console.WriteLine($"Changed: {k} => {v}");
dict.OnItemRemoved += (k, v) => Console.WriteLine($"Removed: {k} => {v}");
dict.OnStateChanged += () => Console.WriteLine("Dictionary state changed.");

dict.Add("apple", 5);
dict["apple"] = 10;
dict.Remove("apple");

foreach (var kvp in dict)
{
Console.WriteLine($"{kvp.Key} => {kvp.Value}");
}
```




=====
=====

# 🧩 Reactive Dictionary

A reactive key-value dictionary that allows observation of its elements.  
Supports dynamic resizing and triggers events when items are added, removed, modified, or when the dictionary’s state changes.  
Ideal for **UI updates, reactive programming, caching layers, and event-driven scenarios**.

> **Key advantage:** Unlike a plain `Dictionary<K,V>`, the reactive version provides **notifications on every structural change**, making it suitable for synchronization with UI or external systems.

---


## ReactiveDictionary<K,V>

`ReactiveDictionary<K,V>` is the concrete implementation of `IReactiveDictionary<K,V>`.  
Internally it uses hash buckets, dynamic slot arrays, and free lists for efficient memory reuse.

### Constructors

- `ReactiveDictionary(int capacity = 1)`  
  Initializes an empty dictionary with given initial capacity.

- `ReactiveDictionary(IDictionary<K,V> dictionary)`  
  Initializes from an existing dictionary.

- `ReactiveDictionary(IEnumerable<KeyValuePair<K,V>> items)`  
  Initializes from an enumerable of key-value pairs.

---

### Properties

- `int Count` – number of key-value pairs.
- `bool IsReadOnly` – always `false`.
- `ICollection<K> Keys` – read-only view of keys.
- `ICollection<V> Values` – read-only view of values.
- `V this[K key]` – access value by key. Throws if not found.

---

### Events

- `event StateChangedHandler OnStateChanged` – fired on global changes.
- `event SetItemHandler<K,V> OnItemChanged` – fired when a value is replaced.
- `event AddItemHandler<K,V> OnItemAdded` – fired when a new pair is inserted.
- `event RemoveItemHandler<K,V> OnItemRemoved` – fired when a pair is removed.

---

### Methods

- `void Add(K key, V value)`  
  Adds a new entry. Triggers `OnItemAdded` + `OnStateChanged`.


- `bool Remove(K key)`  
  Removes an entry by key. Triggers `OnItemRemoved` + `OnStateChanged`.


- `void Clear()`  
  Removes all entries. Triggers multiple `OnItemRemoved` + `OnStateChanged`.


- `bool ContainsKey(K key)`  
  Checks if key exists.


- `bool TryGetValue(K key, out V value)`  
  Gets value safely.


- `IEnumerator<KeyValuePair<K,V>> GetEnumerator()`  
  Lightweight struct-based enumerator.


- `void CopyTo(KeyValuePair<K,V>[] array, int arrayIndex)`  
  Copies all entries to external array.

---

### Nested Types

- `Enumerator` – struct enumerator over dictionary items.
- `ReadOnlyKeyCollection` – read-only view over keys.
- `ReadOnlyValueCollection` – read-only view over values.

---

### Unity Integration

When used in Unity (`UNITY_5_3_OR_NEWER`),  
`ReactiveDictionary<K,V>` implements `ISerializationCallbackReceiver` to support **serialization**:

- `OnBeforeSerialize()` – flattens dictionary into an array.
- `OnAfterDeserialize()` – reconstructs dictionary after load.

---

### Example Usage

```csharp
var dict = new ReactiveDictionary<string, int>();

dict.OnItemAdded += (key, value) => Console.WriteLine($"Added {key}:{value}");
dict.OnItemRemoved += (key, value) => Console.WriteLine($"Removed {key}:{value}");
dict.OnItemChanged += (key, value) => Console.WriteLine($"Updated {key} -> {value}");
dict.OnStateChanged += () => Console.WriteLine("Dictionary state changed");

dict.Add("x", 1);
dict["x"] = 42;
dict.Remove("x");
dict.Add("y", 99);

foreach (var kv in dict)
    Console.WriteLine($"{kv.Key} = {kv.Value}");
```

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements of type `object`**.  
The table shows median execution times of key operations, illustrating the overhead of the reactive wrapper compared to a standard `Dictionary`.

| Operation       | Dictionary (Median μs) | ReactiveDictionary (Median μs) |
|-----------------|------------------------|--------------------------------|
| Add             | 14.60                  | 17.60                          |
| Clear           | 1.30                   | 2.10                           |
| ContainsKey     | 6.70                   | 5.70                           |
| Enumerator      | 13.00                  | 13.40                          |
| Indexer Get     | 7.30                   | 5.70                           |
| Indexer Set     | 13.20                  | 9.80                           |
| Remove          | 7.20                   | 11.50                          |
| TryGetValue     | 12.30                  | 10.40                          |

> **Note:** `ReactiveDictionary` introduces minimal overhead for common operations like `Add` and `Clear`. In some operations like `Indexer Get` and `TryGetValue`, it can even be slightly faster due to internal optimizations. `Remove` and `Indexer Set` may have slightly higher latency compared to a standard `Dictionary` because of event invocation and reactive state management.
