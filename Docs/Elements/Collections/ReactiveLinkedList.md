# 🧩 ReactiveLinkedList&lt;T&gt;

Represents a **reactive linked list** that notifies subscribers about changes to its elements.
It supports fast insertions at head and tail, maintains a free-list for removed nodes. Use this class when you need a
**reactive linked list** that supports frequent insertions and removals at arbitrary positions, with notifications for
every change

> [!IMPORTANT]
> For high performance always use `foreach` to iterate over the collection. **Never** use `for` loop for index-based
> traversal!

---

## 📑 Table of Contents

<ul>
  <li><a href="#example-of-usage">Example of Usage</a></li>
  <li><a href="#-inspector-settings">Inspector Settings</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
<ul>
  <li><a href="#-type">Type</a></li>
  <li>
<details>
<summary><a href="#-constructors">Constructors</a></summary>
    <ul>
      <li><a href="#reactivelinkedlistint">ReactiveLinkedList(int)</a></li>
      <li><a href="#reactivelinkedlistparams-t">ReactiveLinkedList(params T[])</a></li>
      <li><a href="#reactivelinkedlistienumerablet">ReactiveLinkedList(IEnumerable&lt;T&gt;)</a></li>
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
        <li><a href="#removet">Remove(T)</a></li>
        <li><a href="#removeatint">RemoveAt(int)</a></li>
        <li><a href="#clear">Clear()</a></li>
        <li><a href="#indexoft">IndexOf(T)</a></li>
        <li><a href="#containst">Contains(T)</a></li>
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
- [Example of Usage](#example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Inspector Settings](#-inspector-settings)
    - [Constructors](#-constructors)
        - [ReactiveLinkedList(int)](#reactivelinkedlistint)
        - [ReactiveLinkedList(params T[])](#reactivelinkedlistparams-t)
        - [ReactiveLinkedList(IEnumerable\<T>)](#reactivelinkedlistienumerablet)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
        - [OnItemAdded](#onitemadded)
        - [OnItemRemoved](#onitemremoved)
        - [OnItemChanged](#onitemchanged)
    - [Properties](#-properties)
        - [Count](#count)
        - [IsReadOnly](#isreadonly)
    - [Indexers](#-indexers)
        - [[int index]](#int-index)
    - [Methods](#-methods)
        - [Add(T)](#addt)
        - [AddRange(IEnumerable<T>)](#addrangeenumerablet)
        - [Insert(int, T)](#insertint-t)
        - [Remove(T)](#removet)
        - [RemoveAt(int)](#removeatint)
        - [Clear()](#clear)
        - [IndexOf(T)](#indexoft)
        - [Contains(T)](#containst)
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
// Create a reactive linked list
var reactiveList = new ReactiveLinkedList<string>();

// Subscribe to events
reactiveList.OnItemAdded += (index, item) =>
    Console.WriteLine($"Inserted '{item}' at index {index}");

reactiveList.OnItemRemoved += (index, item) =>
    Console.WriteLine($"Deleted '{item}' from index {index}");

reactiveList.OnItemChanged += (index, newValue) =>
    Console.WriteLine($"Item at index {index} changed to '{newValue}'");

reactiveList.OnStateChanged += () =>
    Console.WriteLine("List state changed");

// Add items
reactiveList.Add("Apple");
reactiveList.Add("Banana");

// Insert item at index 1
reactiveList.Insert(1, "Orange");

// Modify an item
reactiveList[0] = "Grapes";

// Remove an item
reactiveList.Remove("Banana");

// Enumerate items
foreach (var item in reactiveList)
    Console.WriteLine(item);

// Use AddRange to add multiple items efficiently
reactiveList.AddRange(new[] { "Kiwi", "Mango", "Pineapple" });
```

---

## 🛠 Inspector Settings

| Parameter         | Description                              |
|-------------------|------------------------------------------|
| `serializedItems` | The initial elements of the linked list. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class ReactiveLinkedList<T> : IReactiveList<T>, IDisposable, ISerializationCallbackReceiver
```

- **Description:** Represents a **reactive linked list** that notifies subscribers about changes to its elements.
  It supports fast insertions at head and tail, maintains a free-list for removed nodes
- **Type Parameter:** `T` — The type of elements stored in the list.
- **Inheritance:** [IReactiveList&lt;T&gt;](IReactiveList.md), `IDisposable`, `ISerializationCallbackReceiver`
- **Notes:**
    - Insertions and removals are **O(1)** complexity, traversal — **O(N)**
    - Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `ReactiveLinkedList(int)`

```csharp
public ReactiveLinkedList(int capacity);
```

- **Description:** Initializes a new list with the specified initial capacity.
- **Parameters:** `capacity` — the initial number of nodes to allocate. Default is `4`

#### `ReactiveLinkedList(params T[])`

```csharp
public ReactiveLinkedList(params T[] items);
```

- **Description:** Initializes a new list and adds the provided items.
- **Parameters:** `items` — initial elements to populate the list.

#### `ReactiveLinkedList(IEnumerable<T>)`

```csharp
public ReactiveLinkedList(IEnumerable<T> items);
```

- **Description:** Initializes a new list from an enumerable collection.
- **Parameters:** `items` — collection of items to populate the list.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Triggered whenever the list state changes globally (add, remove, clear).

#### `OnItemAdded`

```csharp
public event Action<int, T> OnItemAdded;
```

- **Description:** Triggered when a new item is inserted.
- **Parameters:**
    - `index` — zero-based index of the inserted element.
    - `item` — the inserted element of type `T`.

#### `OnItemRemoved`

```csharp
public event Action<int, T> OnItemRemoved;
```

- **Description:** Triggered when an item is removed.
- **Parameters:**
    - `index` — zero-based index of the removed element.
    - `item` — the removed element of type `T`.

#### `OnItemChanged`

```csharp
public event Action<int, T> OnItemChanged;
```

- **Description:** Triggered when an existing item is replaced or modified.
- **Parameters:**
    - `index` — zero-based index of the changed element.
    - `item` — the new value of type `T`.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of elements in the list.

#### `IsReadOnly`

```csharp
public bool IsReadOnly => false;
```

- **Description:** Always returns false; the list is mutable.

---

### 🏷️ Indexers

#### `[int index]`

```csharp
public T this[int index] { get; set; }
```

- **Description:** Gets or sets the element at the specified index.
- **Exception:** `ArgumentOutOfRangeException` if the index is invalid.
- **Note:** Setting a value triggers `OnItemChanged` and `OnStateChanged`.

---

### 🏹 Methods

#### `Add(T)`

```csharp
public void Add(T item);
```

- **Description:** Adds an item to the end of the list. Automatically expands the internal storage if necessary.
- **Parameter:** `item` — the element to add. Cannot be `null`.
- **Remarks:** Triggers `OnItemInserted` and `OnStateChanged` events.

#### `AddRange(IEnumerable<T>)`

```csharp
public void AddRange(IEnumerable<T> items);
```

- **Description:** Efficiently adds multiple items to the end of the list.
- **Parameter:** `items` — collection of elements to add. Cannot be `null`.
- **Remarks:** Always use this method instead of multiple `Add` calls when adding a group of items to reduce overhead.
  Triggers `OnItemInserted` for each item and `OnStateChanged` once.
- **Exceptions:** `ArgumentNullException` if `items` is `null`.

#### `Insert(int, T)`

```csharp
public void Insert(int index, T item);
```

- **Description:** Inserts an item at the specified zero-based index.
- **Parameters:**  
  `index` — the position at which to insert the item.  
  `item` — the element to insert. Cannot be `null`.
- **Remarks:** Triggers `OnItemInserted` and `OnStateChanged` events.
- **Exceptions:** `ArgumentOutOfRangeException` if `index` is invalid.

#### `Remove(T)`

```csharp
public bool Remove(T item);
```

- **Description:** Removes the first occurrence of the specified item from the list.
- **Parameter:** `item` — the element to remove.
- **Returns:** `true` if the item was removed; otherwise, `false`.
- **Remarks:** Triggers `OnItemDeleted` and `OnStateChanged` if successful.

#### `RemoveAt(int)`

```csharp
public void RemoveAt(int index);
```

- **Description:** Removes the item at the specified zero-based index.
- **Parameter:** `index` — the index of the item to remove.
- **Remarks:** Triggers `OnItemDeleted` and `OnStateChanged`.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all elements from the list.
- **Remarks:** Triggers `OnItemDeleted` for each removed item and `OnStateChanged` once.

#### `IndexOf(T)`

```csharp
public int IndexOf(T item);
```

- **Description:** Searches for the first occurrence of the specified item.
- **Parameter:** `item` — the element to locate.
- **Returns:** The zero-based index of the item if found; otherwise `-1`.

#### `Contains(T)`

```csharp
public bool Contains(T item);
```

- **Description:** Determines whether the list contains a specific value.
- **Parameter:** `item` — the element to locate.
- **Returns:** `true` if the item exists; otherwise `false`.

#### `CopyTo(T[], int)`

```csharp
public void CopyTo(T[] array, int arrayIndex);
```

- **Description:** Copies the elements of the list to the specified array starting at the given index.
- **Parameters:**  
  `array` — the destination array.  
  `arrayIndex` — the starting index in the destination array.
- **Exceptions:** `ArgumentNullException`, `ArgumentOutOfRangeException`, `ArgumentException`.

#### `CopyTo(int, T[], int, int)`

```csharp
public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length);
```

- **Description:** Copies a range of elements from the list to the destination array.
- **Parameters:**  
  `sourceIndex` — zero-based index in the list where copying starts.  
  `destination` — target array.  
  `destinationIndex` — starting index in the target array.  
  `length` — number of elements to copy.
- **Exceptions:** `ArgumentNullException`, `ArgumentOutOfRangeException`, `ArgumentException`.

#### `Populate(IEnumerable<T>)`

```csharp
public void Populate(IEnumerable<T> newItems);
```

- **Description:** Replaces the current contents of the list with the provided collection.
- **Parameter:** `newItems` — collection of items to populate the list. Cannot be `null`.
- **Remarks:** Updates existing items, adds new items, and removes excess items. Triggers `OnItemChanged`,
  `OnItemInserted`, `OnItemDeleted`, and `OnStateChanged`.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns a struct enumerator that iterates over the elements of the `ReactiveLinkedList<T>`.
- **Returns:** An `Enumerator` struct that implements `IEnumerator<T>` for the list.
- **Remarks:** This enumerator allows `foreach` iteration without allocating on the heap.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Clears the list and unsubscribes all event handlers.
- **Remarks:** Releases internal resources and prevents further event notifications.

---

## 🔗 Useful Links

- [ReactiveLinkedList Performance](../Performance/ReactiveLinkedListPerformance.md) – performance benchmarks for
  reactive list.
- [Iterating over Reactive Collections](../../BestPractices/IteratingReactiveCollections.md) — best practice.
