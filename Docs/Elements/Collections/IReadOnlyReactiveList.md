# 🧩 IReadOnlyReactiveList&lt;T&gt;

`IReadOnlyReactiveList<T>` represents a **read-only reactive list** that notifies subscribers when its contents change. It extends [IReadOnlyReactiveArray&lt;T&gt;](IReadOnlyReactiveArray.md) and adds **insert** and **delete** events.

> [!NOTE]  
> Use this interface when you want a read-only reactive collection that supports item insertion and deletion notifications in addition to standard reactive array behavior.

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
public T this[int index] { get; }
````
- **Description:** Gets the element at the specified index.
- **Parameters:** `index` — zero-based index of the element.
- **Returns:** `T` — the element at the specified index.

---

## Methods

#### `Contains(T)`
```csharp
public bool Contains(T item);
```
- **Description:** Determines whether the array contains a specific element.
- **Parameter:** `item` — The object to locate in the array.
- **Returns:** `true` if the item is found; otherwise, `false`.

#### `IndexOf(T)`
```csharp
public int IndexOf(T item);
```
- **Description:** Returns the index of a specific item in the array.
- **Parameter:** `item` — The object to locate in the array.
- **Returns:** The index of the item if found; otherwise, `-1`.

#### `CopyTo(T[] array, int arrayIndex)`
```csharp
public void CopyTo(T[] array, int arrayIndex)
```
- **Description:** Copies all items in the array to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)`
```csharp
public void Copy(int sourceIndex, T[] destination, int destinationIndex, int length);
```
- **Description:** Copies a range of elements from this array to a destination array.
- **Parameters:**
    - `int sourceIndex` — starting index in this array.
    - `T[] destination` — array to copy elements to.
    - `int destinationIndex` — starting index in the destination array.
    - `int length` — number of elements to copy.
- **Notes:** Throws exceptions if indices or lengths are invalid, or if the destination array is too small.

#### `GetEnumerator()`
```csharp
public IEnumerator<T> GetEnumerator();
```
- **Description:** Returns an enumerator that iterates through the collection.
- **Remarks:** Inherited from `IEnumerable<T>`.

---

## 🗂 Example of Usage

```csharp
// Assume we have a read-only reactive list
IReadOnlyReactiveList<int> reactiveList = ...;

// Subscribe to item insertion
reactiveList.OnItemInserted += (index, item) =>
{
    Console.WriteLine($"Item {item} inserted at index {index}");
};

// Subscribe to item deletion
reactiveList.OnItemDeleted += (index, item) =>
{
    Console.WriteLine($"Item {item} deleted from index {index}");
};

// Subscribe to item changes (inherited from IReadOnlyReactiveArray)
reactiveList.OnItemChanged += (index, value) =>
{
    Console.WriteLine($"Item at index {index} changed to {value}");
};

// Subscribe to global state changes
reactiveList.OnStateChanged += () =>
{
    Console.WriteLine("List state changed");
};
```