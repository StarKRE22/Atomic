# 🧩 Reactive List

A reactive list is designed to provide a collection of elements that can be observed.  
It supports dynamic resizing and triggers events when items are added, removed, modified, or when the list’s state changes.  
This is useful for UI updates, event handling, and following reactive programming principles.

---

## IReactiveList\<T\>

Represents a reactive list that notifies subscribers when its contents change.  
Includes events for inserts, deletions, modifications, and global state changes.

### Events

- `event StateChangedHandler OnStateChanged` – triggered when the list’s state changes (e.g., reset, bulk update).
- `event ChangeItemHandler<T> OnItemChanged` – triggered when an existing item is modified at a specific index.
- `event InsertItemHandler<T> OnItemInserted` – triggered when a new item is inserted at a specific index.
- `event DeleteItemHandler<T> OnItemDeleted` – triggered when an item is deleted from a specific index.

---

## ReactiveList\<T\>

A reactive, resizable list that emits events when items are added, removed, changed, or when the global state changes.  
Supports manual memory pooling for temporary buffers, indexed access, and enumeration.

### Constructors

- `ReactiveList(int capacity = 0)`  
  Initializes an empty list with the specified initial capacity.
  - Throws `ArgumentOutOfRangeException` if `capacity < 0`.


- `ReactiveList(params T[] items)`  
  Initializes the list with the provided items.


- `ReactiveList(IEnumerable<T> items)`  
  Initializes the list as a copy of the given enumerable.

### Properties

- `int Count` – the number of elements in the list.
- `int Capacity` – the current internal array capacity.
- `T this[int index]` – read/write element access.
  > Throws `IndexOutOfRangeException` if index is out of range.
- `bool IsReadOnly` – always `false`.

### Events

- `event StateChangedHandler OnStateChanged` – triggered when the list’s global state changes.
- `event ChangeItemHandler<T> OnItemChanged` – triggered when an element is modified.
- `event InsertItemHandler<T> OnItemInserted` – triggered when a new element is inserted.
- `event DeleteItemHandler<T> OnItemDeleted` – triggered when an element is removed.

### Methods

- `void Add(T item)`  
  Adds a new item to the end of the list.
    - Triggers `OnItemInserted` and `OnStateChanged`.


- `void AddRange(IEnumerable<T> items)`
  Adds a range of items to the end of the list 
  - Triggers `OnItemInserted` and `OnStateChanged`
  - Throws `ArgumentNullException` if parameter `items` is null 
 

- `void Clear()`  
  Removes all elements.
    - Triggers `OnItemDeleted` for each element.
    - Triggers `OnStateChanged`.


- `bool Remove(T item)`  
  Removes the first occurrence of the specified item.
    - Triggers `OnItemDeleted` and `OnStateChanged` if found.


- `void RemoveAt(int index)`  
  Removes the item at the specified index.
    - Triggers `OnItemDeleted` and `OnStateChanged`.
    - Throws `IndexOutOfRangeException` if index is invalid.


- `void Insert(int index, T item)`  
  Inserts an item at the specified index.
    - Triggers `OnItemInserted` and `OnStateChanged`.
    - Throws `IndexOutOfRangeException` if index is invalid.


- `void CopyTo(T[] array, int arrayIndex = 0)`  
  Copies elements to an external array starting at `arrayIndex`.


- `int IndexOf(T item)`  
  Returns the index of the first occurrence of the item, or `-1` if not found.


- `void Dispose()`  
  Clears all elements and event subscriptions.


- `Enumerator GetEnumerator()`  
  Returns a lightweight struct-based enumerator for iteration.


- `void Populate(IEnumerable<T> newItems)`  
  Updates the contents of the list with the values from `newItems`.
  - Existing elements that differ from the new values are updated, triggering `OnItemChanged`.
  - If there are more new elements than the current list, the additional elements are added, triggering `OnItemInserted`.
  - If there are fewer new elements than the current list, the excess elements are removed, triggering `OnItemDeleted`.
  - Throws `ArgumentNullException` if `newItems` is `null`.
  - `OnStateChanged` is fired once at the end.

---

### Example of Usage

```csharp
var list = new ReactiveList<int>(3);

list.OnItemInserted += (index, value) => Console.WriteLine($"Inserted {value} at {index}");
list.OnItemDeleted += (index, value) => Console.WriteLine($"Deleted {value} at {index}");
list.OnItemChanged += (index, value) => Console.WriteLine($"Changed {index} to {value}");
list.OnStateChanged += () => Console.WriteLine("List state changed");

// Add elements
list.Add(1);
list.Add(2);

// Insert an element
list.Insert(1, 99);

// Modify an element
list[0] = 42;

// Remove an element
list.RemoveAt(2);

// Clear the list
list.Clear();

// Iterating
foreach (var item in list)
    Console.WriteLine(item);
```

## 🔥 Performance

The performance comparison below was measured on a **MacBook with Apple M1** and for collections containing **1000 elements of type `object`**.  
The table shows median execution times of key operations, illustrating the overhead of the reactive wrapper.

| Operation       | List (Median μs) | ReactiveList (Median μs) |
|-----------------|------------------|--------------------------|
| Add             | 8.60             | 10.70                    |
| Clear           | 0.40             | 1.20                     |
| Contains        | 62.80            | 41.90                    |
| CopyTo          | 0.40             | 0.50                     |
| Enumerator      | 7.00             | 7.10                     |
| For             | 1.70             | 1.70                     |
| Indexer Get     | 1.50             | 1.70                     |
| Indexer Set     | 9.50             | 42.00                    |
| Remove          | 293.35           | 254.25                   |
| Remove At Last  | 10.80            | 3.00                     |
| Insert At First | 222.65           | 223.60                   |

> **Note:** `ReactiveList` shows slightly higher latency when setting elements (`Indexer Set`) due to event invocation, but is faster in some removal operations (`RemoveAt`) thanks to internal optimizations.
