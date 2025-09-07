# 🧩 Reactive Hash Set

A reactive hash set that allows observation of its elements.  
Supports dynamic resizing and triggers events when items are added, removed, replaced, or when the set’s state changes.  
Ideal for **UI updates, reactive programming, caching layers, and event-driven scenarios**.

> **Key advantage:** Unlike a plain `HashSet<T>`, the reactive version provides **notifications on every structural change**, making it suitable for synchronization with UI or external systems.

---

## IReactiveSet<T>

`IReactiveSet<T>` extends `ISet<T>` and `IReadOnlyCollection<T>`.  
It defines events for observing set changes.

### Events

- `event StateChangedHandler OnStateChanged`  
  Triggered when any structural change happens (e.g., `Clear`, `ReplaceWith`).

- `event AddItemHandler<T> OnItemAdded`  
  Triggered when a new element is added.

- `event RemoveItemHandler<T> OnItemRemoved`  
  Triggered when an element is removed.

### Members

- `int Count` – number of items.
- `bool Contains(T item)` – checks whether an element exists.
- `bool IsReadOnly` – always `false`.

---

## ReactiveHashSet<T>

`ReactiveHashSet<T>` is the concrete implementation of `IReactiveSet<T>`.  
Internally it uses hash buckets, slot arrays, and free lists for efficient memory management.

### Constructors

- `ReactiveHashSet(int capacity = 0)`  
  Initializes an empty set with given initial capacity.

- `ReactiveHashSet(params T[] elements)`  
  Initializes from an array of elements.

- `ReactiveHashSet(IReadOnlyCollection<T> elements)`  
  Initializes from a read-only collection.

- `ReactiveHashSet(IEnumerable<T> elements)`  
  Initializes from any enumerable.

---

### Properties

- `int Count` – number of elements.
- `bool IsReadOnly` – always `false`.
- `bool IsEmpty()` – true if set has no elements.
- `bool IsNotEmpty()` – true if set contains at least one element.

---

### Events

- `event StateChangedHandler OnStateChanged` – fired on global changes.
- `event AddItemHandler<T> OnItemAdded` – fired when a new element is inserted.
- `event RemoveItemHandler<T> OnItemRemoved` – fired when an element is removed.

---

### Methods

- `bool Add(T item)`  
  Adds a new element. Triggers `OnItemAdded` + `OnStateChanged`.

- `bool Remove(T item)`  
  Removes an element. Triggers `OnItemRemoved` + `OnStateChanged`.

- `void Clear()`  
  Removes all elements. Triggers multiple `OnItemRemoved` + `OnStateChanged`.

- `void ReplaceWith(IEnumerable<T> other)`  
  Replaces all elements with the given collection. Triggers events for added and removed elements.

- `void UnionWith(IEnumerable<T> other)`  
  Adds all elements from another collection.

- `void ExceptWith(IEnumerable<T> other)`  
  Removes all elements found in another collection.

- `void IntersectWith(IEnumerable<T> other)`  
  Keeps only elements also present in another collection.

- `void SymmetricExceptWith(IEnumerable<T> other)`  
  Adds elements not in the set and removes elements that are present in both.

- `bool Contains(T item)`  
  Checks if the element exists in the set.

- `void CopyTo(T[] array, int arrayIndex = 0)`  
  Copies all elements to an external array.

- `IEnumerator<T> GetEnumerator()`  
  Lightweight struct-based enumerator.

---

### Nested Types

- `Enumerator` – struct enumerator over set items.

---

### Unity Integration

When used in Unity (`UNITY_5_3_OR_NEWER`),  
`ReactiveHashSet<T>` implements `ISerializationCallbackReceiver` to support **serialization**:

- `OnBeforeSerialize()` – flattens the set into an array.
- `OnAfterDeserialize()` – clears the set and restores elements after load.

---

### Example Usage

```csharp
var set = new ReactiveHashSet<string>();

set.OnItemAdded += item => Console.WriteLine($"Added {item}");
set.OnItemRemoved += item => Console.WriteLine($"Removed {item}");
set.OnStateChanged += () => Console.WriteLine("Set state changed");

set.Add("apple");
set.Add("banana");
set.Remove("apple");
set.ReplaceWith(new[] { "orange", "banana" });

foreach (var item in set)
    Console.WriteLine(item);
```

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements of type `object`**.  
The table shows median execution times of key operations, illustrating the overhead of the reactive wrapper compared to a standard `HashSet<T>`.

| Operation  | HashSet (Median μs) | ReactiveHashSet (Median μs) |
|------------|---------------------|-----------------------------|
| Add        | 51.70               | 21.20                       |
| Clear      | 0.10                | 9.30                        |
| Contains   | 46.00               | 9.60                        |
| Enumerator | 11.00               | 7.40                        |
| Remove     | 23.50               | 53.90                       |

> **Note:** `ReactiveHashSet` shows **much lower median times for Add, Contains, and Enumerator**, thanks to internal optimizations and preallocated slots.  
> Operations like `Clear` and `Remove` are slightly more expensive due to **event invocation** and managing free lists for reactive state tracking.  
> Overall, `ReactiveHashSet` introduces minimal overhead for typical read operations while maintaining full reactive notifications for all structural changes.
