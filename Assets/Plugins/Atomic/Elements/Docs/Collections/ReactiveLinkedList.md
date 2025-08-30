# 🧩 Reactive Linked List

A reactive singly linked list that allows observation of its elements.  
Supports dynamic resizing and triggers events when items are added, removed, modified, or when the list’s state changes.  
Ideal for UI updates, reactive programming, and event-driven scenarios.

> **Key advantage:** Linked list allows **efficient O(1) insertions and removals** once the node is located, unlike `List` which may require shifting elements.

---

## ReactiveLinkedList\<T\>

`ReactiveLinkedList<T>` is a reactive, resizable singly linked list that **implements the `IReactiveList<T>` interface**.  
It emits events when items are added, removed, changed, or when the global state changes.  
Supports indexed access, enumeration, and manual event handling.

### Constructors

- `ReactiveLinkedList(int capacity = 1)`  
  Initializes an empty list with the specified initial capacity.

- `ReactiveLinkedList(params T[] items)`  
  Initializes the list with the provided array of items.

- `ReactiveLinkedList(IEnumerable<T> items)`  
  Initializes the list as a copy of the given enumerable.

### Properties

- `int Count` – number of elements in the list.
- `bool IsReadOnly` – always `false`.
- `T this[int index]` – read/write element access.
  > Throws `ArgumentOutOfRangeException` if index is out of range.

### Events

- `event StateChangedHandler OnStateChanged` – triggered on any state change.
- `event ChangeItemHandler<T> OnItemChanged` – triggered when an element is updated.
- `event InsertItemHandler<T> OnItemInserted` – triggered when a new element is inserted.
- `event DeleteItemHandler<T> OnItemDeleted` – triggered when an element is removed.

### Methods

- `void Add(T item)`  
  Adds a new item at the end of the list.
  - Triggers `OnItemInserted` and `OnStateChanged`.


- `void Insert(int index, T item)`  
  Inserts an item at the specified index.
  - Triggers `OnItemInserted` and `OnStateChanged`.  
  - Throws `ArgumentOutOfRangeException` if index is invalid.


- `bool Remove(T item)`  
  Removes the first occurrence of the specified item.
  - Triggers `OnItemDeleted` and `OnStateChanged` if successful.


- `void RemoveAt(int index)`  
  Removes the item at the specified index.
  - Triggers `OnItemDeleted` and `OnStateChanged`.  
  - Throws `ArgumentOutOfRangeException` if index is invalid.


- `void Clear()`  
  Removes all elements.
  - Triggers `OnItemDeleted` for each element and `OnStateChanged` once.


- `int IndexOf(T item)` – returns the index of the first occurrence of `item`, or `-1` if not found.


- `bool Contains(T item)` – determines whether the list contains the specified item.


- `void CopyTo(T[] array, int arrayIndex)` – copies elements to an external array starting at `arrayIndex`.


- `Enumerator GetEnumerator()` – returns a lightweight struct-based enumerator for iteration.

---

### Example Usage

```csharp
var list = new ReactiveLinkedList<int>();

list.OnItemInserted += (index, value) => Console.WriteLine($"Inserted {value} at {index}");
list.OnItemDeleted += (index, value) => Console.WriteLine($"Deleted {value} at {index}");
list.OnItemChanged += (index, value) => Console.WriteLine($"Changed {index} to {value}");
list.OnStateChanged += () => Console.WriteLine("List state changed");

list.Add(1);
list.Add(2);
list.Insert(1, 99);
list[0] = 42;
list.RemoveAt(2);
list.Clear();

foreach (var item in list)
    Console.WriteLine(item);
```

### Performance

The performance comparison below was measured on a **MacBook with Apple M1** and for collections containing **1000 elements of type `object`**.  
The table shows **average execution times** of key operations, illustrating the overhead of the reactive wrapper and the cost of using a linked structure.
### Performance
The performance comparison below was measured on a **MacBook with Apple M1** for collections containing **1000 elements of type `object`**.  
The table shows median execution times of key operations.

| Operation       | List<T> Avg (μs) | ReactiveList Avg (μs) | ReactiveLinkedList Avg (μs) |
|-----------------|------------------|-----------------------|-----------------------------|
| Add             | 8.63             | 10.69                 | 16.74                       |
| Clear           | 0.44             | 1.22                  | 0.04                        |
| Contains        | 63.35            | 42.70                 | 51.59                       |
| CopyTo          | 0.45             | 0.48                  | 8.02                        |
| Enumerator      | 7.11             | 7.16                  | 6.81                        |
| ForLoop         | 1.72             | 1.75                  | 1269.22                     |
| Indexer Get     | 1.53             | 1.74                  | 1275.63                     |
| Indexer Set     | 10.13            | 42.12                 | 1316.02                     |
| Remove          | 295.79           | 257.58                | 48.58                       |
| Remove At Last  | 11.08            | 3.03                  | 2539.79                     |
| Insert At First | 226.36           | 225.46                | 16.07                       |

#### Notes

- `ReactiveLinkedList` excels in scenarios with frequent insertions/removals at the head or tail.
- Index-based access (`Get`/`Set`) is significantly slower than arrays due to traversal.
- Iteration using `foreach` is acceptable, but for high-performance scenarios, direct node traversal is recommended.
- Event notifications (`OnItemChanged`, `OnItemInserted`, `OnItemDeleted`) add a slight overhead.