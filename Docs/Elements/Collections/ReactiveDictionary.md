# 🧩 ReactiveDictionary<K, V>

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

> [!TIP]
> Use this class when you need a dictionary with full read / write access and **reactive notifications** on changes.

---

## 🛠 Inspector Settings

| Parameter         | Description                                      |
|-------------------|--------------------------------------------------|
| `serializedItems` | The initial elements of the reactive dictionary. |

---

## 🏗️ Constructors

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

## ⚡ Events

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

## 🔑 Properties

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

## 🏷️ Indexers

#### `[K key]`

```csharp
public V this[K key] { get; set; }
```

- **Description:** Gets or sets the value associated with the specified key.
- **Exceptions:** Throws `KeyNotFoundException` if key does not exist when getting.
- **Events:** Triggers `OnItemChanged` (if updating) or `OnItemAdded` (if adding).

---

## 🏹 Methods

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

<details>
  <summary>
    <h2>🧩 ReadOnlyKeyCollection</h2>
  </summary>

```csharp
public readonly struct ReadOnlyKeyCollection : ICollection<K>
```

- **Description:** Represents a read-only collection of keys
- **Inheritance:** `ICollection<T>`

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of keys in the collection.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Gets a value indicating whether the collection is read-only. Always `true`.

---

### 🏹 Methods

#### `Contains(K)`

```csharp
public bool Contains(K item);
```

- **Description:** Determines whether the collection contains the specified key.
- **Parameter:** `item` — The key to locate. Cannot be null.
- **Returns:** `true` if the key exists; otherwise `false`.

#### `CopyTo(K[] array, int arrayIndex)`

```csharp
public void CopyTo(K[] array, int arrayIndex);
```

- **Description:** Copies the keys to the specified array, starting at the specified index.
- **Parameters:**
    - `array` — The destination array. Cannot be null.
    - `arrayIndex` — The zero-based index at which to begin copying. Must be non-negative.
- **Exceptions:**
    - Throws `ArgumentNullException` if `array` is null.
    - Throws `ArgumentOutOfRangeException` if `arrayIndex` is negative.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the keys in the dictionary.
- **Returns:** An `Enumerator` struct for iterating over keys.

---

### ⛔ Unsupported Methods

```csharp
void ICollection<K>.Add(K item);
void ICollection<K>.Clear();
bool ICollection<K>.Remove(K item);
```

- **Description:** All modification methods throw `NotSupportedException` because the collection is read-only.

</details>

---

<details>
  <summary>
    <h2>🧩 ReadOnlyValueCollection</h2>
  </summary>

```csharp
public readonly struct ReadOnlyValueCollection : ICollection<V>
```

- **Description:** <b>Represents a read-only collection of values</b>.
- **Inheritance:** `ICollection<T>`

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of values in the collection.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Gets a value indicating whether the collection is read-only. Always `true`.

---

### 🏹 Methods

#### `Contains(V)`

```csharp
public bool Contains(V item);
```

- **Description:** Determines whether the collection contains the specified value.
- **Parameter:** `item` — The value to locate.
- **Returns:** `true` if the value exists; otherwise `false`.

#### `CopyTo(V[] array, int arrayIndex)`

```csharp
public void CopyTo(V[] array, int arrayIndex);
```

- **Description:** Copies the values to the specified array, starting at the specified index.
- **Parameters:**
    - `array` — The destination array. Cannot be null.
    - `arrayIndex` — The zero-based index at which to begin copying. Must be non-negative.
- **Exceptions:**
    - Throws `ArgumentNullException` if `array` is null.
    - Throws `ArgumentOutOfRangeException` if `arrayIndex` is negative.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the values in the dictionary.
- **Returns:** An `Enumerator` struct for iterating over values.

---

### ⛔ Unsupported Methods

```csharp
void ICollection<V>.Add(V item);
void ICollection<V>.Clear();
bool ICollection<V>.Remove(V item);
```

- **Description:** All modification methods throw `NotSupportedException` because the collection is read-only.

</details>

---

## 🗂 Examples of Usage

### Example #1: Basic Usage

```csharp
var dict = new ReactiveDictionary<string, int>();

dict.Add("One", 1);
dict.Add("Two", 2);

Console.WriteLine(dict["One"]); // Output: 1

dict["Two"] = 22; // Updates the value
Console.WriteLine(dict["Two"]); // Output: 22
```

---

### Example #2:  Using TryAdd

```csharp
var dict = new ReactiveDictionary<string, int>();

bool added = dict.TryAdd("One", 1); // true
added = dict.TryAdd("One", 11);     // false, key already exists

Console.WriteLine(dict["One"]); // Output: 1
```

---

### Example #3: Removing Elements

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

### Example #4:  Iterating Keys and Values

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

### Example #5:  Subscribing to Events

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

### Example #6: Initializing from Collections

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

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**. The table shows median execution times of key operations, illustrating the overhead of the reactive
wrapper compared to a standard `Dictionary`.

| Operation   | Dictionary (Median μs) | ReactiveDictionary (Median μs) |
|-------------|------------------------|--------------------------------|
| Add         | 34.10                  | 64.40                          |
| Clear       | 7.10                   | 2.40                           |
| ContainsKey | 7.10                   | 5.70                           |
| Enumerator  | 56.60                  | 58.60                          |
| Get         | 7.40                   | 5.80                           |
| Set         | 35.50                  | 10.10                          |
| Remove      | 7.40                   | 6.80                           |
| TryGetValue | 34.20                  | 32.90                          |

Thus, `ReactiveDictionary` introduces minimal overhead for common operations like `Add` and `Clear`. In some operations
like `Indexer Get` and `TryGetValue`, it can even be slightly faster due to internal optimizations. `Remove` and
`Indexer Set` may have slightly higher latency compared to a standard `Dictionary` because of event invocation and
reactive state management.