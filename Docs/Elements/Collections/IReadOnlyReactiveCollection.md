# 🧩 IReadOnlyReactiveCollection&lt;T&gt;

Represents a **read-only reactive collection** that provides notifications when items are added,
removed, or when the overall state changes.

---

## 📑 Table of Contents


<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
            <li><a href="#onitemadded">OnItemAdded</a></li>
            <li><a href="#onitemremoved">OnItemRemoved</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#count">Count</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#containst">Contains(T)</a></li>
            <li><a href="#copytot-int">CopyTo(T[], int)</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
</ul>


<!--
- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
        - [OnItemAdded](#onitemadded)
        - [OnItemRemoved](#onitemremoved)
    - [Properties](#-properties)
        - [Count](#count)
    - [Methods](#-methods)
        - [Contains(T)](#containst)
        - [CopyTo(T[], int)](#copytot-int)
        - [GetEnumerator()](#getenumerator)
-->
---

## 🗂 Example of Usage

```csharp
//Assume we have a collection
IReadOnlyReactiveCollection<string> collection = ...;

// Subscribe to events
collection.OnItemAdded += item => Console.WriteLine($"Added: {item}");
collection.OnItemRemoved += item => Console.WriteLine($"Removed: {item}");
collection.OnStateChanged += () => Console.WriteLine("State changed");

// Contains
collection.Contains("Banana");

// Copy to
collection.CopyTo(someArray, 0);

// Iterate over items
foreach (var value in collection)
{
    Console.WriteLine(value);
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IReadOnlyReactiveCollection<out T> : IReadOnlyCollection<T>
```

- **Description:** Represents a **read-only reactive collection** that provides notifications when items are added,
  removed, or when the overall state changes.
- **Inheritance:**  `IReadOnlyCollection<T>`, `IEnumerable<T>`, `IEnumerable`
- **Type Parameter:** `T` — The type of elements stored in the collection.
- **Note:** Use this interface when you need **read-only access** to a collection but still require **reactive
  notifications** on changes.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Occurs when the overall state of the collection changes.
- **Remarks:** This can happen due to bulk operations or significant modifications.

#### `OnItemAdded`

```csharp
public event Action<T> OnItemAdded;
```

- **Description:** Occurs when a new item is added to the collection.
- **Parameter:** `value` — the item that was added to the collection.
- **Remarks:** Use this event to react to additions without iterating over the collection.

#### `OnItemRemoved`

```csharp
public event Action<T> OnItemRemoved;
```

- **Description:** Occurs when an existing item is removed from the collection.
- **Parameter:** `value` — the item that was removed from the collection.
- **Remarks:** Use this event to react to removals without iterating over the collection.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of elements in the collection.
- **Returns:** An `int` representing the total number of items.

---

### 🏹 Methods

#### `Contains(T)`

```csharp
public bool Contains(T item);
```

- **Description:** Determines whether the collection contains a specific value.
- **Parameter:** `item` — the object to locate in the collection.
- **Returns:** `true` if the item is found; otherwise `false`.

#### `CopyTo(T[], int)`

```csharp
public void CopyTo(T[] array, int arrayIndex);
```

- **Description:** Copies the elements of the collection to an array, starting at a particular array index.
- **Parameters:**
    - `array` — the destination one-dimensional array.
    - `arrayIndex` — the zero-based index in the array at which copying begins.

#### `GetEnumerator()`

```csharp
public IEnumerator<T> GetEnumerator();
```

- **Description:** Returns an enumerator that iterates through the collection.
- **Returns:** An `IEnumerator<T>` for iterating over the collection’s elements.