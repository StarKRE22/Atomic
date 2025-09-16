# 🧩 IReadOnlyReactiveList&lt;T&gt;

`IReadOnlyReactiveList<T>` represents a **read-only reactive list** that notifies subscribers when its contents change. It provides **indexed notifications** for insertions, deletions, and overall state changes. It extends [IReadOnlyReactiveArray&lt;T&gt;](IReadOnlyReactiveArray.md) and [IReadOnlyReactiveCollection&lt;T&gt;](IReadOnlyReactiveCollection.md).

> [!NOTE]  
> Use this interface when you need **read-only indexed access** to a collection but still require **reactive notifications** on changes.

---

## Events

#### `OnStateChanged`
```csharp
public event Action OnStateChanged;
```
- **Description:** Triggered when the overall state of the list changes.
- **Remarks:** Can happen due to bulk operations or significant modifications, including multiple insertions, deletions, or updates.

#### `OnItemAdded`
```csharp
public event Action<int, T> OnItemAdded;
```
- **Description:** Triggered when a new item is inserted at a specific index.
- **Parameters:**
  - `index` — The zero-based index where the item was added.
  - `value` — The item that was inserted.

#### `OnItemRemoved`
```csharp
public event Action<int, T> OnItemRemoved;
```
- **Description:** Triggered when an item is removed from a specific index.
- **Parameters:**
  - `index` — The zero-based index from which the item was removed.
  - `value` — The item that was removed.
---

## Properties

#### `Length`
```csharp
public int Length { get; }
```
- **Description:** Gets the number of elements in the list.
- **Remarks:** Implemented from [IReadOnlyReactiveArray&lt;T&gt;](IReadOnlyReactiveArray.md); usually returns the same value as `Count`.

#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the number of elements in the collection.
- **Remarks:** Implemented from [IReadOnlyReactiveCollection&lt;T&gt;](IReadOnlyReactiveCollection.md); returns the total number of items.

---

## Indexer

#### `[int index]`
```csharp
public T this[int index] { get; }
```
- **Description:** Gets the element at the specified index.
- **Parameters:** `index` — zero-based index of the element.
- **Returns:** `T` — the element at the specified index.

---

## Methods

#### `Contains(T)`
```csharp
bool Contains(T item);
```
- **Description:** Determines whether the list contains a specific element.
- **Parameter:** `item` — The object to locate in the list.
- **Returns:** `true` if the item is found; otherwise, `false`.

#### `IndexOf(T)`
```csharp
int IndexOf(T item);
```
- **Description:** Returns the index of a specific item in the list.
- **Parameter:** `item` — The object to locate in the list.
- **Returns:** The index of the item if found; otherwise, `-1`.

#### `CopyTo(T[] array, int arrayIndex)`
```csharp
public void CopyTo(T[] array, int arrayIndex)
```
- **Description:** Copies all items in the list to the specified array starting at the given index.
- **Parameters:**
  - `array` — The destination array.
  - `arrayIndex` — The starting index in the array.

#### `CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)`
```csharp
public void Copy(int sourceIndex, T[] destination, int destinationIndex, int length);
```
- **Description:** Copies a range of elements from this list to a destination array.
- **Parameters:**
  - `int sourceIndex` — starting index in this list.
  - `T[] destination` — array to copy elements to.
  - `int destinationIndex` — starting index in the destination array.
  - `int length` — number of elements to copy.
- **Notes:** Throws exceptions if indices or lengths are invalid, or if the destination array is too small.

#### `GetEnumerator()`
```csharp
public IEnumerator<T> GetEnumerator();
```
- **Description:** Returns an enumerator that iterates through the list.
- **Remarks:** Inherited from `IEnumerable<T>`.

---

## 🗂 Example of Usage
```csharp
// Assume we have a read-only reactive list
IReadOnlyReactiveList<int> reactiveList = ...;

// Subscribe to item insertion
reactiveList.OnItemAdded += (index, item) =>
{
    Console.WriteLine($"Item {item} inserted at index {index}");
};

// Subscribe to item removal
reactiveList.OnItemRemoved += (index, item) =>
{
    Console.WriteLine($"Item {item} removed from index {index}");
};

// Subscribe to item changes
reactiveList.OnItemChanged += (index, value) =>
{
    Console.WriteLine($"Item at index {index} changed to {value}");
};

// Subscribe to global state changes
reactiveList.OnStateChanged += () =>
{
    Console.WriteLine("List state changed");
};

// Access elements by index
int firstItem = reactiveList[0];
Console.WriteLine($"First item: {firstItem}");

// Iterate over all items
Console.WriteLine("Iterating over all items:");
foreach (var item in reactiveList)
{
    Console.WriteLine(item);
}

// Check if a specific item exists
int valueToCheck = 42;
bool exists = reactiveList.Contains(valueToCheck);
Console.WriteLine($"Contains {valueToCheck}: {exists}");

// Find index of a specific item
int indexOfItem = reactiveList.IndexOf(valueToCheck);
Console.WriteLine($"Index of {valueToCheck}: {indexOfItem}");

// Copy items to an array
int[] array = new int[reactiveList.Count];
reactiveList.CopyTo(array, 0);
Console.WriteLine("Items copied to array:");
foreach (var item in array)
{
    Console.WriteLine(item);
}

// Copy a range of items to another array (custom method)
int[] destinationArray = new int[reactiveList.Count];
reactiveList.Copy(0, destinationArray, 0, reactiveList.Count);
Console.WriteLine("Range copied to destination array:");
foreach (var item in destinationArray)
{
    Console.WriteLine(item);
}
```

> [!NOTE]
> Also, you can cast a read-only reactive list to a read-only reactive collection in order to subscribe to non-indexed addition and removal events. These events notify you whenever an item is added or removed, without providing the specific index of the change.

```csharp
// Assume we have a read-only reactive list
IReadOnlyReactiveList<int> reactiveList = GetReactiveList(); // Replace with actual implementation

// Cast to IReadOnlyReactiveCollection to use non-indexed events
IReadOnlyReactiveCollection<int> reactiveCollection = reactiveList;

// Subscribe to non-indexed addition events (inherited from IReadOnlyReactiveCollection)
reactiveCollection.OnItemAdded += item =>
{
    Console.WriteLine($"Item {item} added (non-indexed event)");
};

// Subscribe to non-indexed removal events (inherited from IReadOnlyReactiveCollection)
reactiveCollection.OnItemRemoved += item =>
{
Console.WriteLine($"Item {item} removed (non-indexed event)");
};

```