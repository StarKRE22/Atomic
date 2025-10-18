# 🧩 ReactiveList&lt;T&gt;

Represents a **dynamic, resizable reactive list** that emits events when items are inserted, removed,
changed, or when the list state changes globally. Use this class when you need a **mutable, growable list** with
reactive notifications.

> [!IMPORTANT]
> For high-performance iterations, it is recommended to use a `for` loop instead of `foreach`.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li><a href="#-inspector-settings">Inspector Settings</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
<ul>
  <li><a href="#-type">Type</a></li>
  <li>
  <details>
    <summary> <a href="#-constructors">Constructors</a></summary>
    <ul>
      <li><a href="#reactivelistint">ReactiveList(int)</a></li>
      <li><a href="#reactivelistparams-t">ReactiveList(params T[])</a></li>
      <li><a href="#reactivelistienumerablet">ReactiveList(IEnumerable&lt;T&gt;)</a></li>
    </ul>
  </details>
  </li>

  <li>
    <details>
      <summary><a href="#-events">Events</a></summary>
      <ul>
        <li><a href="#onstatechanged">OnStateChanged</a></li>
        <li><a href="#onitemadded">OnItemAdded</a></li>
        <li><a href="#onitemremoved">OnItemRemoved</a></li>
        <li><a href="#onitemchanged">OnItemChanged</a></li>
      </ul>
    </details>
  </li>

  <li>
    <details>
      <summary><a href="#-properties">Properties</a></summary>
      <ul>
        <li><a href="#count">Count</a></li>
        <li><a href="#capacity">Capacity</a></li>
        <li><a href="#isreadonly">IsReadOnly</a></li>
      </ul>
    </details>
  </li>

  <li>
    <details>
      <summary><a href="#-indexers">Indexers</a></summary>
      <ul>
        <li><a href="#int-index">[int index]</a></li>
      </ul>
    </details>
  </li>

  <li>
    <details>
      <summary><a href="#-methods">Methods</a></summary>
      <ul>
        <li><a href="#addt">Add(T)</a></li>
        <li><a href="#addrangeenumerablet">AddRange(IEnumerable&lt;T&gt;)</a></li>
        <li><a href="#insertint-t">Insert(int, T)</a></li>
        <li><a href="#containst">Contains(T)</a></li>
        <li><a href="#removet">Remove(T)</a></li>
        <li><a href="#removeatint">RemoveAt(int)</a></li>
        <li><a href="#clear">Clear()</a></li>
        <li><a href="#indexoft">IndexOf(T)</a></li>
        <li><a href="#copytot-int">CopyTo(T[], int)</a></li>
        <li><a href="#copytoint-t-int-int">CopyTo(int, T[], int, int)</a></li>
        <li><a href="#populateenumerablet">Populate(IEnumerable&lt;T&gt;)</a></li>
        <li><a href="#getenumerator">GetEnumerator()</a></li>
        <li><a href="#dispose">Dispose()</a></li>
      </ul>
    </details>
  </li>
</ul>
  <li><a href="#-useful-links">Useful Links</a></li>
  </li>
</ul>



<!--
- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Inspector Settings](#-inspector-settings)
    - [Constructors](#-constructors)
        - [ReactiveList(int)](#reactivelistint)
        - [ReactiveList(params T[])](#reactivelistparams-t)
        - [ReactiveList(IEnumerable\<T>)](#reactivelistienumerablet)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
        - [OnItemAdded](#onitemadded)
        - [OnItemRemoved](#onitemremoved)
        - [OnItemChanged](#onitemchanged)
    - [Properties](#-properties)
        - [Count](#count)
        - [Capacity](#capacity)
        - [IsReadOnly](#isreadonly)
    - [Indexers](#-indexers)
        - [[int index]](#int-index)
    - [Methods](#-methods)
        - [Add(T)](#addt)
        - [AddRange(IEnumerable<T>)](#addrangeenumerablet)
        - [Insert(int, T)](#insertint-t)
        - [Contains(T)](#containst)
        - [Remove(T)](#removet)
        - [RemoveAt(int)](#removeatint)
        - [Clear()](#clear)
        - [IndexOf(T)](#indexoft)
        - [CopyTo(T[], int)](#copytot-int)
        - [CopyTo(int, T[], int, int)](#copytoint-t-int-int)
        - [Populate(IEnumerable<T>)](#populateenumerablet)
        - [GetEnumerator()](#getenumerator)
        - [Dispose()](#dispose)
    - [Useful Links](#-useful-links)
-->
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

---

## 🛠 Inspector Settings

| Parameter         | Description                       |
|-------------------|-----------------------------------|
| `serializedItems` | The initial elements of the list. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class ReactiveList<T> : IReactiveList<T>, IDisposable, ISerializationCallbackReceiver
```

- **Description:** Represents a **dynamic, resizable reactive list** that emits events when items are inserted, removed,
  changed, or when the list state changes globally.
- **Inheritance:**  [IReactiveList&lt;T&gt;](IReactiveList.md), `IDisposable`, `ISerializationCallbackReceiver`
- **Type Parameter:** `T` — The type of elements stored in the list.
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `ReactiveList(int)`

```csharp
public ReactiveList(int capacity);
```

- **Description:** Initializes an empty reactive list with the given initial capacity.
- **Parameter:** `capacity` — initial number of allocated elements. Must be non-negative. Default is `0`.
- **Exceptions:** Throws `ArgumentOutOfRangeException` if `capacity < 0`.

#### `ReactiveList(params T[])`

```csharp
public ReactiveList(params T[] items);
```

- **Description:** Initializes the list with the given items.
- **Parameter:** `items` — initial items.
- **Remarks:** initial `Count` equals the number of provided elements.

#### `ReactiveList(IEnumerable<T>)`

```csharp
public ReactiveList(IEnumerable<T> items);
```

- **Description:** Initializes the list with a copy of the given enumerable.
- **Parameter:** `items` — initial items.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Triggered when the array's state changes globally (e.g., multiple items updated, cleared, or reset).

#### `OnItemAdded`

```csharp
public event Action<int, T> OnItemAdded;
```

- **Description:** Triggered when a new item is inserted at a specific index.
- **Parameters:**
    - `index` — zero-based index where the item was inserted.
    - `item` — `T` the item that was inserted.

#### `OnItemRemoved`

```csharp
public event Action<int, T> OnItemRemoved;
```

- **Description:** Triggered when an item is removed from a specific index.
- **Parameters:**
    - `index` — zero-based index from which the item was removed.
    - `item` — `T` the item that was deleted.

#### `OnItemChanged`

```csharp
public event Action<int, T> OnItemChanged;
```

- **Description:** Triggered when an item at a specific index changes.
- **Parameters:**
    - `index` — index of the changed element.
    - `item` — `T` the new value of the element.

---

### 🔑 Properties

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

### 🏷️ Indexers

#### `[int index]`

```csharp
public T this[int index] { get; set; }
```

- Gets or sets the element at the given index.
- Setting a new value triggers `OnItemChanged` and `OnStateChanged`.
- Throws `IndexOutOfRangeException` if index is invalid.

---

### 🏹 Methods

#### `Add(T)`

```csharp
public void Add(T item)
```

- **Description:** Adds an item to the end of the list. Automatically resizes the internal array if full (typically
  doubles capacity).
- **Parameter:** `item` — the element to add. Cannot be `null`.
- **Exception:** `ArgumentNullException` — if `item` is `null`.
- **Events:**
    - `OnItemInserted(index, item)` — fired for the new element.
    - `OnStateChanged()` — fired after insertion.

#### `AddRange(IEnumerable<T>)`

```csharp
public void AddRange(IEnumerable<T> items)
```

- **Description:** Adds a collection of items to the end of the list efficiently. Resizes the internal array only once
  if the total count is known.
- **Parameter:** `items` — the collection of elements to add. Cannot be `null`.
- **Exception:** `ArgumentNullException` — if `items` is `null` or any element is `null`.
- **Events:**
    - `OnItemInserted(index, item)` — fired for each added element.
    - `OnStateChanged()` — fired once if at least one element was added.
- **Note:** Always use `AddRange` when adding multiple items at once instead of calling `Add` repeatedly. This is more
  efficient and reduces unnecessary resizing and event firing.

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

#### `Contains(T)`

```csharp
public bool Contains(T item)
```

- **Description:** Checks whether the list contains the specified item.
- **Parameter:** `item` — the element to search for.
- **Returns:** `true` if found; otherwise `false`.

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

#### `Clear()`

```csharp
public void Clear()
```

- **Description:** Removes all elements from the list.
- **Events:**
    - `OnItemDeleted(index, item)` — fired for each element removed.
    - `OnStateChanged()` — fired once at the end.

#### `IndexOf(T)`

```csharp
public int IndexOf(T item)
```

- **Description:** Returns the index of the first occurrence of the specified item.
- **Parameter:** `item` — element to search for.
- **Returns:** Index if found; otherwise `-1`.

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

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator()
```

- **Description:** Returns a struct enumerator for efficient `foreach` iteration.
- **Notes:**
    - Does **not** trigger events.
    - Returns a struct enumerator for efficient `foreach`.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears the list and unsubscribes all events.

---

## 🔗 Useful Links

- [ReactiveList Performance](../Performance/ReactiveListPerformance.md) – performance benchmarks for reactive list.
- [Iterating over Reactive Collections](../../BestPractices/IteratingReactiveCollections.md) — best practice.
