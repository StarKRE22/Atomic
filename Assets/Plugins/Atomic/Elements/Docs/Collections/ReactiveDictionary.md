# 🧩 Reactive Dictionary

A reactive key-value dictionary that allows observation of its elements.  
Supports dynamic resizing and triggers events when items are added, removed, modified, or when the dictionary’s state changes.  
Ideal for **UI updates, reactive programming, caching layers, and event-driven scenarios**.

> **Key advantage:** Unlike a plain `Dictionary<K,V>`, the reactive version provides **notifications on every structural change**, making it suitable for synchronization with UI or external systems.

---

## IReactiveDictionary<K,V>

`IReactiveDictionary<K,V>` extends `IDictionary<K,V>` and `IReadOnlyDictionary<K,V>`.  
It defines events for observing dictionary changes.

### Events

- `event StateChangedHandler OnStateChanged`  
  Triggered when any structural change happens (e.g., `Clear`, bulk update).

- `event SetItemHandler<K,V> OnItemChanged`  
  Triggered when an existing key’s value is updated.

- `event AddItemHandler<K,V> OnItemAdded`  
  Triggered when a new key-value pair is added.

- `event RemoveItemHandler<K,V> OnItemRemoved`  
  Triggered when a key-value pair is removed.

### Members

- `int Count` – number of items.
- `V this[K key]` – read/write access by key.
- `bool ContainsKey(K key)` – checks whether a key exists.

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

## Performance

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
