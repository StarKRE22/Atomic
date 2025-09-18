# 🧩 IReactiveList&lt;T&gt;

Represents a **reactive list** that notifies subscribers whenever its contents change. It provides **indexed notifications** for insertions, deletions, updates, and overall state changes. It extends `IList<T>` [IReadOnlyReactiveList&lt;T&gt;](IReadOnlyReactiveList.md), and [IReactiveCollection&lt;T&gt;](IReactiveCollection.md).

> [!NOTE]  
> Use this interface when you need **fully mutable indexed access** to a collection with **reactive notifications** on any changes.

---

## ⚡ Events

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

#### `OnItemChanged`
```csharp
public event Action<int, T> OnItemChanged;
```
- **Description:** Triggered when an item at a specific index changes.
- **Parameters:**
  - `index` — index of the changed element.
  - `value` — `T` the new value of the element.

---

## 🔑 Properties

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

## 🏷️ Indexers

#### `[int index]`
```csharp
public T this[int index] { get; set; }
```
- **Description:** Gets or sets the element at the specified index.
- **Parameters:** `index` — zero-based index of the element.
- **Returns:** `T` — the element at the specified index.
- **Remarks:** Setting a new value triggers the `OnItemChanged` event if the value changes.

---

## 🏹 Methods

#### `Add(T)`
```csharp
public void Add(T item)
```
- **Description:** Adds an item to the end of the list.
- **Parameter:** `item` — the element to add.
- **Remarks:** Triggers `OnItemAdded` and `OnStateChanged`.


#### `Insert(int, T)`
```csharp
public void Insert(int index, T item)
```
- **Description:** Inserts an item into the list at the specified index.
- **Parameters:**
  - `index` — the zero-based index at which the item should be inserted.
  - `item` — the element to insert.
- **Exception:** `ArgumentOutOfRangeException` — if `index` is invalid.
- **Remarks:**
  - Shifts elements after the index one position forward.
  - Triggers `OnItemAdded` and `OnStateChanged`.

#### `Remove(T)`
```csharp
public bool Remove(T item)
```
- **Description:** Removes the first occurrence of the specified item.
- **Parameter:** `item` — the element to remove.
- **Returns:** `true` if the element was removed; otherwise `false`.
- **Remarks:** Triggers `OnItemRemoved` and `OnStateChanged` if successful.

#### `RemoveAt(int)`
```csharp
public void RemoveAt(int index)
````
- **Description:** Removes the item at the specified index.
- **Parameter:** `index` — the zero-based index of the item to remove.
- **Exception:** `ArgumentOutOfRangeException` — if `index` is invalid.
- **Remarks:** Triggers `OnItemRemoved` and `OnStateChanged`.

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all items from the list.
- **Remarks:**
  - Triggers `OnStateChanged`.
  - Also, may trigger `OnItemRemoved` for each removed item, depending on implementation.

#### `Contains(T)`
```csharp
public bool Contains(T item)
```
- **Description:** Checks if the list contains the specified item.
- **Parameter:** `item` — the element to locate.
- **Returns:** `true` if the element exists in the list; otherwise `false`.

#### `IndexOf(T)`
```csharp
public int IndexOf(T item)
````
- **Description:** Returns the index of the first occurrence of the specified item.
- **Parameter:** `item` — the element to search for.
- **Returns:** The zero-based index of the element; `-1` if not found.

#### `CopyTo(T[], int)`
```csharp
public void CopyTo(T[] array, int arrayIndex)
```
- **Description:** Copies all elements of the list to the specified array, starting at the given index.
- **Parameters:**
  - `array` — the destination array.
  - `arrayIndex` — the index at which to begin copying.
- **Exceptions:**
  - `ArgumentNullException` — if `array` is `null`.
  - `ArgumentOutOfRangeException` — if `arrayIndex` is negative.
  - `ArgumentException` — if the destination array does not have enough space.

#### `CopyTo(int, T[], int, int)`
```csharp
public void Copy(int sourceIndex, T[] destination, int destinationIndex, int length)
```
- **Description:** Copies a range of elements from the list to the destination array.
- **Parameters:**
  - `sourceIndex` — the starting index in this list.
  - `destination` — the target array.
  - `destinationIndex` — the starting index in the target array.
  - `length` — the number of elements to copy.
- **Exceptions:**
  - `ArgumentNullException` — if `destination` is `null`.
  - `ArgumentOutOfRangeException` — if indices or length are invalid.
  - `ArgumentException` — if the destination array is too small.

#### `GetEnumerator()`
```csharp
public IEnumerator<T> GetEnumerator()
````
- **Description:** Returns an enumerator that iterates through the list.
- **Returns:** `IEnumerator<T>` — an enumerator for the list.
- **Remarks:** Enumeration does not trigger events.

---

## 🗂 Example of Usage
```csharp
IReactiveList<int> reactiveList = ...;

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
foreach (var item in reactiveList)
{
    Console.WriteLine(item);
}

// Modify items
reactiveList[0] = 42;
```