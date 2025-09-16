# 🧩 IReactiveList&lt;T&gt;

`IReactiveList<T>` represents a **reactive list with read-write access** that notifies subscribers when its contents change. It extends [IReadOnlyReactiveList&lt;T&gt;](IReadOnlyReactiveList.md) and `IList<T>`, combining full list functionality with **reactive notifications**.

> [!NOTE]  
> Use this interface when you need a mutable list that supports insertion, deletion, modification, and state change events.

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
- **Description:** Gets the total number of elements in the list.
- **Remarks:** Implements `IReadOnlyCollection<T>`.  
  Returns the same value as the `Length` property from `IReadOnlyReactiveArray<T>`.

#### `Length`
```csharp
public int Length { get; }
````
- **Description:** Inherited from `IReadOnlyReactiveArray<T>`.  
  Returns the number of elements in the list.
- **Remarks:** Explicitly implemented as `Length => Count`.

---

## Indexers

#### `[int index]`
```csharp
public T this[int index] { get; set; }
````
- **Description:** Gets the element at the specified index.
- **Parameters:** `index` — zero-based index of the element.
- **Returns:** `T` — the element at the specified index.

---

## Methods

#### `Add(T)`
```csharp
public void Add(T item)
```
- **Description:** Adds an item to the end of the list.
- **Parameter:** `item` — the element to add.
- **Remarks:** Triggers `OnItemInserted` and `OnStateChanged`.

#### `Clear()`
```csharp
public void Clear()
```
- **Description:** Removes all items from the list.
- **Remarks:**
    - Triggers `OnStateChanged`.
    - Also, may trigger `OnItemDeleted` for each removed item, depending on implementation.

#### `Contains(T)`
```csharp
public bool Contains(T item)
```
- **Description:** Checks if the list contains the specified item.
- **Parameter:** `item` — the element to locate.
- **Returns:** `true` if the element exists in the list; otherwise `false`.

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

#### `Copy(int, T[], int, int)`
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

#### `Remove(T)`
```csharp
public bool Remove(T item)
```
- **Description:** Removes the first occurrence of the specified item.
- **Parameter:** `item` — the element to remove.
- **Returns:** `true` if the element was removed; otherwise `false`.
- **Remarks:** Triggers `OnItemDeleted` and `OnStateChanged` if successful.

#### `IndexOf(T)`
```csharp
public int IndexOf(T item)
````
- **Description:** Returns the index of the first occurrence of the specified item.
- **Parameter:** `item` — the element to search for.
- **Returns:** The zero-based index of the element; `-1` if not found.

#### `Insert(int, T)`
```csharp
public void Insert(int index, T item)
````
- **Description:** Inserts an item into the list at the specified index.
- **Parameters:**
    - `index` — the zero-based index at which the item should be inserted.
    - `item` — the element to insert.
- **Exception:** `ArgumentOutOfRangeException` — if `index` is invalid.
- **Remarks:**
    - Shifts elements after the index one position forward.
    - Triggers `OnItemInserted` and `OnStateChanged`.

#### `RemoveAt(int)`
```csharp
public void RemoveAt(int index)
````
- **Description:** Removes the item at the specified index.
- **Parameter:** `index` — the zero-based index of the item to remove.
- **Exception:** `ArgumentOutOfRangeException` — if `index` is invalid.
- **Remarks:** Triggers `OnItemDeleted` and `OnStateChanged`.

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
// Get an instance of reactive list:
IReactiveList<string> reactiveList = ...;

// Subscribe to events
reactiveList.OnItemInserted += (index, item) =>
Console.WriteLine($"Inserted '{item}' at index {index}");

reactiveList.OnItemDeleted += (index, item) =>
Console.WriteLine($"Deleted '{item}' from index {index}");

reactiveList.OnItemChanged += (index, newValue) =>
Console.WriteLine($"Item at index {index} changed to '{newValue}'");

reactiveList.OnStateChanged += () =>
Console.WriteLine("List state changed");

// Add items
reactiveList.Add("Apple");
reactiveList.Add("Banana");

// Insert item
reactiveList.Insert(1, "Orange");

// Modify item
reactiveList[0] = "Grapes";

// Remove item
reactiveList.Remove("Banana");

// Enumerate
foreach (var item in reactiveList)
    Console.WriteLine(item);
````