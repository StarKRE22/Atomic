# 🧩 ReactiveHashSet&lt;T&gt;

Represents a **reactive hash set** that supports **notifications** when items are added, removed, or when the overall
state changes. It implements `IReactiveSet<T>`, `ISet<T>`, `ICollection<T>`, `IEnumerable<T>`, and `IDisposable`.
Optionally supports serialization for Unity projects.

```csharp
public class ReactiveHashSet<T> : IReactiveSet<T>, IDisposable, ISerializationCallbackReceiver
```

- **Type Parameters:**
  - `T` — The type of elements stored in the set.
- **Notes:**
  - Use this class when you need a **mutable hash-based set** with **reactive events** for any change.
  - Supports Unity serialization

---

## 🏗️ Constructors

#### `ReactiveHashSet(int capacity)`

```csharp
public ReactiveHashSet(int capacity = 0);
```

- **Description:** Initializes the set with a **predefined capacity**.
- **Parameter:** `capacity` — Initial number of slots to allocate. Must be ≥ 0.
- **Remarks:**
    - The actual internal capacity is rounded up to the next prime number for better hash distribution.
    - All internal buckets are initialized, and the set starts empty.
- **Exception:** `ArgumentOutOfRangeException` — if `capacity` is negative.

#### `ReactiveHashSet(params T[])`

```csharp
public ReactiveHashSet(params T[] elements);
```

- **Description:** Initializes the set with an **initial collection of elements**.
- **Parameter:** `elements` — Initial elements to add. Cannot be null.
- **Behavior:**
    - Internally calls the constructor with capacity equal to `elements.Length`.
    - Adds all unique elements from the array to the set.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged` for each element added.

#### `ReactiveHashSet(IReadOnlyCollection<T>)`

```csharp
public ReactiveHashSet(IReadOnlyCollection<T> elements);
```

- **Description:** Initializes the set with an **initial collection of elements**.
- **Parameter:** `elements` — Initial elements to add. Cannot be null.
- **Behavior:**
    - Uses the count of the collection to initialize the internal capacity.
    - Adds all unique elements from the collection.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged` for each element added.

#### `ReactiveHashSet(IEnumerable<T> elements)`

```csharp
public ReactiveHashSet(IEnumerable<T> elements);
```

- **Description:** Initializes the set with an **enumerable collection of elements**.
- **Parameter:** `elements` — Initial elements to add. Cannot be null.
- **Behavior:**
    - Counts the elements to determine the internal capacity.
    - Adds all unique elements from the enumerable.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged` for each element added.

---

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Triggered when the set’s state changes globally (e.g., clear or bulk update).
- **Remarks:** Useful for reacting to any modifications in the set without subscribing to individual item events.

#### `OnItemAdded`

```csharp
public event Action<T> OnItemAdded;
```

- **Description:** Triggered when a new item is added to the set.
- **Parameters:** `T` — the item that was added.

#### `OnItemRemoved`

```csharp
public event Action<T> OnItemRemoved;
```

- **Description:** Triggered when an item is removed from the set.
- **Parameters:** `T` — the item that was removed.

---

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of elements in the set.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Indicates whether the set is read-only. Typically `false` for reactive sets.

---

## 🏹 Methods

#### `Add(T)`

```csharp
public bool Add(T item);
```

- **Description:** Adds an element to the set if it does not already exist.
- **Parameter:** `item` — The element to add. Cannot be `null` for reference types.
- **Returns:** `true` if the element was added; `false` if it already exists in the set.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged` if the element is added.
- **Remarks:** Use this method to insert new elements. Duplicate items are ignored.

#### `Remove(T)`

```csharp
public bool Remove(T item);
```

- **Description:** Removes the specified element from the set.
- **Parameter:** `item` — The element to remove.
- **Returns:** `true` if the element was successfully removed; otherwise `false`.
- **Events:** Triggers `OnItemRemoved` and `OnStateChanged` if the element is removed.
- **Remarks:** If the element does not exist, the method does nothing and returns `false`.

#### `Contains(T)`

```csharp
public bool Contains(T item);
```

- **Description:** Determines whether the set contains a specific element.
- **Parameter:** `item` — The element to locate in the set.
- **Returns:** `true` if the element exists in the set; otherwise `false`.
- **Remarks:** Use this method to safely check for element existence before performing operations.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all elements from the set.
- **Events:** Triggers `OnItemRemoved` for each removed element and `OnStateChanged`.
- **Remarks:** Clears the set completely. All subscribers will be notified for each removed element.

#### `UnionWith(IEnumerable<T>)`

```csharp
public void UnionWith(IEnumerable<T> other);
```

- **Description:** Modifies the set to include all elements from the specified collection.
- **Parameter:** `other` — A collection of elements to merge into the set. Cannot be `null`.
- **Events:** Triggers `OnItemAdded` for each new element added and `OnStateChanged`.
- **Remarks:** Existing elements are preserved; only new unique elements trigger events.

#### `IntersectWith(IEnumerable<T>)`

```csharp
public void IntersectWith(IEnumerable<T> other);
```

- **Description:** Modifies the set to contain only elements that exist in both the set and the specified collection.
- **Parameter:** `other` — The collection to intersect with. Cannot be `null`.
- **Events:** Triggers `OnItemRemoved` for each element removed and `OnStateChanged`.
- **Remarks:** Only elements present in both sets are kept; all others are removed.

#### `ExceptWith(IEnumerable<T>)`

```csharp
public void ExceptWith(IEnumerable<T> other);
```

- **Description:** Removes all elements in the specified collection from the set.
- **Parameter:** `other` — The collection of elements to remove. Cannot be `null`.
- **Events:** Triggers `OnItemRemoved` for each element removed and `OnStateChanged`.
- **Remarks:** Elements not present in the set are ignored.

#### `SymmetricExceptWith(IEnumerable<T>)`

```csharp
public void SymmetricExceptWith(IEnumerable<T> other);
```

- **Description:** Modifies the set to contain only elements that are in either the set or the specified collection, but
  not both.
- **Parameter:** `other` — The collection to compare with. Cannot be `null`.
- **Events:** Triggers `OnItemAdded` and `OnItemRemoved` for elements that are added or removed, plus `OnStateChanged`.
- **Remarks:** Performs a symmetric difference; existing elements in both collections are removed.

#### `IsSubsetOf(IEnumerable<T>)`

```csharp
public bool IsSubsetOf(IEnumerable<T> other);
```

- **Description:** Determines whether the set is a subset of the specified collection.
- **Parameter:** `other` — The collection to compare against. Cannot be `null`.
- **Returns:** `true` if all elements in the set exist in `other`; otherwise `false`.
- **Remarks:** Useful for comparing sets without modifying them.

#### `IsProperSubsetOf(IEnumerable<T>)`

```csharp
public bool IsProperSubsetOf(IEnumerable<T> other);
```

- **Description:** Determines whether the set is a proper subset of the specified collection.
- **Parameter:** `other` — The collection to compare against. Cannot be `null`.
- **Returns:** `true` if the set is a subset of `other` and contains fewer elements than `other`; otherwise `false`.
- **Remarks:** A proper subset is strictly smaller than the compared collection.

#### `IsSupersetOf(IEnumerable<T>)`

```csharp
public bool IsSupersetOf(IEnumerable<T> other);
```

- **Description:** Determines whether the set is a superset of the specified collection.
- **Parameter:** `other` — The collection to compare against. Cannot be `null`.
- **Returns:** `true` if the set contains all elements of `other`; otherwise `false`.

#### `IsProperSupersetOf(IEnumerable<T>)`

```csharp
public bool IsProperSupersetOf(IEnumerable<T> other);
```

- **Description:** Determines whether the set is a proper superset of the specified collection.
- **Parameter:** `other` — The collection to compare against. Cannot be `null`.
- **Returns:** `true` if the set contains all elements of `other` and has additional elements; otherwise `false`.
- **Remarks:** A proper superset is strictly larger than the compared collection.

#### `Overlaps(IEnumerable<T>)`

```csharp
public bool Overlaps(IEnumerable<T> other);
```

- **Description:** Determines whether the set and the specified collection share any elements.
- **Parameter:** `other` — The collection to compare with. Cannot be `null`.
- **Returns:** `true` if at least one element exists in both collections; otherwise `false`.

#### `SetEquals(IEnumerable<T>)`

```csharp
public bool SetEquals(IEnumerable<T> other);
```

- **Description:** Determines whether the set and the specified collection contain the same elements.
- **Parameters:** `other` — The collection to compare with. Cannot be `null`.
- **Returns:** `true` if both contain the same elements; otherwise `false`.
- **Remarks:** Comparison ignores order and duplicates; only element identity matters.

#### `GetEnumerator()`

```csharp
public IEnumerator<T> GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the set.
- **Returns:** An `IEnumerator<T>` for enumerating the elements.
- **Remarks:** Enumeration is safe for reading but modifying the set during iteration may cause exceptions.

---

## 🗂 Example of Usage

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

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements
of type `object`**. The table shows median execution times of key operations, illustrating the overhead of the reactive
wrapper compared to a standard `HashSet<T>`.

| Operation  | HashSet (Median μs) | ReactiveHashSet (Median μs) |
|------------|---------------------|-----------------------------|
| Add        | 51.70               | 21.20                       |
| Clear      | 0.10                | 9.30                        |
| Contains   | 46.00               | 9.60                        |
| Enumerator | 11.00               | 7.40                        |
| Remove     | 23.50               | 53.90                       |

`ReactiveHashSet` shows **much lower median times for Add, Contains, and Enumerator**, thanks to internal optimizations
and preallocated slots. Operations like `Clear` and `Remove` are slightly more expensive due to **event invocation** and
managing free lists for reactive state tracking.

Overall, `ReactiveHashSet` introduces minimal overhead for typical read operations while maintaining full reactive
notifications for all structural changes.
