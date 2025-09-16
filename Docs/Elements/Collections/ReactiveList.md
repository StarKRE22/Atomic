# 🧩 ReactiveList&lt;T&gt;

`ReactiveList<T>` represents a **dynamic, resizable reactive list** that emits events when items are inserted, removed, changed, or when the list state changes globally. It implements [IReactiveList&lt;T&gt;](IReactiveList.md) and `IDisposable`.

> [!NOTE]  
> Use this class when you need a **mutable, growable list** with reactive notifications.

---

## Constructors

#### `ReactiveList(int)`
```csharp
public ReactiveList(int capacity = 0);
```
- **Description:** Initializes an empty reactive list with the given initial capacity.
- **Parameter:** `capacity` — initial number of allocated elements. Must be non-negative.
- **Exceptions:** Throws `ArgumentOutOfRangeException` if `capacity < 0`.
- **Example of usage:**
  
  ```csharp
  var list = new ReactiveList<string>(10); // Capacity = 10, Count = 0
  ```

#### `ReactiveList(params T[])`
```csharp
public ReactiveList(params T[] items);
```
- **Description:** Initializes the list with the given items.
- **Parameter:** `items` — initial items.
- **Remarks:** initial `Count` equals the number of provided elements.
- **Example of usage:**
  
  ```csharp
  var list = new ReactiveList<int>(1, 2, 3); // Count = 3
  ```

#### `ReactiveList(IEnumerable<T>)`
```csharp
public ReactiveList(IEnumerable<T> items);
```
- **Description:** Initializes the list with a copy of the given enumerable.
- **Parameter:** `items` — initial items.
- **Example of usage:**
  
  ```csharp
  var list = new ReactiveList<int>(Enumerable.Range(1, 5)); // [1,2,3,4,5]
  ```

---

## Events

#### `OnItemInserted`
```csharp
public event InsertItemHandler<T> OnItemInserted;
```
- **Description:** Triggered when a new item is inserted at a specific index.
- **Parameters:**
  - `index` — zero-based index where the item was inserted.
  - `item` — `T` the item that was inserted.
- **Remarks:** See [InsertItemHandler&lt;T&gt;](Delegates.md/#-insertitemhandlert)

#### `OnItemDeleted`
```csharp
public event DeleteItemHandler<T> OnItemDeleted;
```
- **Description:** Triggered when an item is removed from a specific index.
- **Parameters:**
  - `index` — zero-based index from which the item was removed.
  - `item` — `T` the item that was deleted.
- **Remarks:** See [DeleteItemHandler&lt;T&gt;](Delegates.md/#-deleteitemhandlert)

#### `OnItemChanged`
```csharp
public event ChangeItemHandler<T> OnItemChanged;
```
- **Description:** Triggered when an item at a specific index changes.
- **Parameters:**
  - `index` — index of the changed element.
  - `newValue` — `T` the new value of the element.
- **Remarks:** See [ChangeItemHandler&lt;T&gt;](Delegates.md/#-changeitemhandlert)

#### `OnStateChanged`
```csharp
public event StateChangedHandler OnStateChanged;
```
- **Description:** Triggered when the array's state changes globally (e.g., multiple items updated, cleared, or reset).
- **Remarks:** See [StateChangedHandler](Delegates.md/#-statechangedhandler)

---

## Properties

#### `Count`
```csharp
public int Count { get; }
````
- **Description:** Gets the number of elements in the list.

#### `Capacity`
```csharp
public int Capacity { get; }
```
- **Description:** Gets the current internal array capacity.

#### `IsReadOnly`
```csharp
public bool IsReadOnly { get; }
```
- **Description:** Always `false`.

---

## Indexers

#### `[int index]`
```csharp
public T this[int index] { get; set; }
```
- Gets or sets the element at the given index.
- Setting a new value triggers `OnItemChanged` and `OnStateChanged`.
- Throws `IndexOutOfRangeException` if index is invalid.

---

## Methods

#### `Add(T)`
```csharp
public void Add(T item)
```
- **Description:** Adds an item to the end of the list. Automatically resizes the internal array if full (typically doubles capacity).
- **Parameter:** `item` — the element to add. Cannot be `null`.
- **Exception:** `ArgumentNullException` — if `item` is `null`.
- **Events:**
  - `OnItemInserted(index, item)` — fired for the new element.
  - `OnStateChanged()` — fired after insertion.
- **Complexity:** Amortized **O(1)**; worst case **O(n)** on resize.
- **Example:**  
  
  ```csharp
  list.Add("apple");
  ```

#### `AddRange(IEnumerable<T>)`
```csharp
public void AddRange(IEnumerable<T> items)
```
- **Description:** Adds a collection of items to the end of the list efficiently. Resizes the internal array only once if the total count is known.
- **Parameter:** `items` — the collection of elements to add. Cannot be `null`.
- **Exception:** `ArgumentNullException` — if `items` is `null` or any element is `null`.
- **Events:**
  - `OnItemInserted(index, item)` — fired for each added element.
  - `OnStateChanged()` — fired once if at least one element was added.
- **Complexity:** O(m) where m = number of items added; occasional O(n) if resize occurs.
- **Note:** Always use `AddRange` when adding multiple items at once instead of calling `Add` repeatedly. This is more efficient and reduces unnecessary resizing and event firing.
- **Example:**  
  
  ```csharp
  list.AddRange(new[] { "banana", "cherry" });
  ```

#### `Insert(int, T)`
```csharp
public void Insert(int index, T item)
```
- **Description:** Inserts an element at the specified index, shifting subsequent elements.
- **Parameters:**
  - `index` — zero-based position for insertion.
  - `item` — the element to insert. Cannot be `null`.
- **Exceptions:**
  - `ArgumentNullException` — if `item` is `null`.
  - `IndexOutOfRangeException` — if `index` is invalid.
- **Events:**
  - `OnItemInserted(index, item)`
  - `OnStateChanged()`
- **Complexity:** O(n) due to element shifting.
- **Example:**  
  
  ```csharp
  list.Insert(1, "orange");
  ```

#### `Contains(T)`
```csharp
public bool Contains(T item)
```
- **Description:** Checks whether the list contains the specified item.
- **Parameter:** `item` — the element to search for.
- **Returns:** `true` if found; otherwise `false`.
- **Complexity:** O(n) linear search.
- **Example:**  
  
  ```csharp
  bool hasApple = list.Contains("apple");
  ```

#### `Remove(T)`
```csharp
public bool Remove(T item)
```
- **Description:** Removes the first occurrence of the specified item.
- **Parameter:** `item` — the element to remove.
- **Returns:** `true` if an element was removed; otherwise `false`.
- **Events:**
  - `OnItemDeleted(index, item)`
  - `OnStateChanged()`
- **Complexity:** O(n) due to search and shifting.
- **Example:**  
  
  ```csharp
  list.Remove("banana");
  ```

#### `RemoveAt(int)`
```csharp
public void RemoveAt(int index)
```
- **Description:** Removes the element at the specified index.
- **Parameter:** `index` — zero-based index to remove.
- **Exception:** `IndexOutOfRangeException` — if index is invalid.
- **Events:**
  - `OnItemDeleted(index, item)`
  - `OnStateChanged()`
- **Complexity:** O(n) due to shifting.
- **Example:**  
  
  ```csharp
  list.RemoveAt(0);
  ```

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all elements from the list.
- **Events:**
  - `OnItemDeleted(index, item)` — fired for each element removed.
  - `OnStateChanged()` — fired once at the end.
- **Complexity:** O(n).
- **Example:**  
  
  ```csharp
  list.Clear();
  ```

#### `IndexOf(T)`
```csharp
public int IndexOf(T item)
```
- **Description:** Returns the index of the first occurrence of the specified item.
- **Parameter:** `item` — element to search for.
- **Returns:** Index if found; otherwise `-1`.
- **Complexity:** O(n).
- **Example:**  
  
  ```csharp
  int index = list.IndexOf("cherry");
  ```

#### `CopyTo(T[], int)`
```csharp
public void CopyTo(T[] array, int arrayIndex)
```
- **Description:** Copies all elements to the specified array starting at `arrayIndex`.
- **Parameters:**
  - `array` — destination array.
  - `arrayIndex` — starting index in the destination.
- **Exceptions:**
  - `ArgumentNullException` — if `array` is null.
  - `ArgumentOutOfRangeException` — if `arrayIndex` is negative.
  - `ArgumentException` — if the destination array is too small.
- **Complexity:** O(n).
- **Example:**  
  
  ```csharp
  string[] target = new string[list.Count];
  list.CopyTo(target, 0);
  ```

#### `CopyTo(int, T[], int, int)`
```csharp
public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
```
- **Description:** Copies a range of elements to a destination array.
- **Parameters:**
  - `sourceIndex` — start index in the list.
  - `destination` — target array.
  - `destinationIndex` — start index in target array.
  - `length` — number of elements to copy.
- **Exceptions:** `ArgumentNullException`, `ArgumentOutOfRangeException`, `ArgumentException`.
- **Complexity:** O(length).
- **Example:**  
  
  ```csharp
  list.CopyTo(1, target, 0, 2);
  ```

#### `Populate(IEnumerable<T>)`
```csharp
public void Populate(IEnumerable<T> newItems)
```
- **Description:** Updates the list contents to match `newItems`.
- **Behavior:**
  - Updates differing elements, firing `OnItemChanged`.
  - Adds extra elements, firing `OnItemInserted`.
  - Removes excess elements, firing `OnItemDeleted`.
  - Always fires `OnStateChanged` at the end.
- **Exception:** `ArgumentNullException` — if `newItems` is null.
- **Complexity:** O(n + m).
- **Example:**  
  
  ```csharp
  list.Populate(new[] { "apple", "banana", "orange" });
  ```

#### `Dispose()`
```csharp
public void Dispose()
```
- **Description:** Clears the list and unsubscribes all events.
- **Complexity:** O(n).
- **Example:**  
  
  ```csharp
  list.Dispose();
  ```

#### `GetEnumerator()`
```csharp
public Enumerator GetEnumerator()
```
- **Description:** Returns a struct enumerator for efficient `foreach` iteration.
- **Notes:** 
  - Does **not** trigger events.
  - Returns a struct enumerator for efficient `foreach`.
- **Complexity:** O(1) for enumerator creation; O(n) for iteration.
- **Example:**  
  
  ```csharp
  foreach (var item in list)
  {
      Console.WriteLine(item);
  }
  ```
---

## 🗂 Example of Usage

```csharp
var list = new ReactiveList<string>();

// Subscribe to events
list.OnItemInserted += (i, v) => Console.WriteLine($"Inserted {v} at {i}");
list.OnItemDeleted += (i, v) => Console.WriteLine($"Deleted {v} from {i}");
list.OnItemChanged += (i, v) => Console.WriteLine($"Changed {i} to {v}");
list.OnStateChanged += () => Console.WriteLine("State changed");

// Add items
list.Add("A");
list.Add("B");

// Insert
list.Insert(1, "X");

// Modify
list[0] = "Z";

// Remove
list.Remove("B");

// Enumerate
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

`ReactiveList` shows slightly higher latency when setting elements (`Indexer Set`) due to event invocation, but is faster in some removal operations (`RemoveAt`) thanks to internal optimizations.