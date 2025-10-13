# 🧩 IReadOnlyReactiveArray&lt;T&gt;

Represents a **read-only reactive array** that notifies subscribers about changes to its elements and
global state.

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
            <li><a href="#onitemchanged">OnItemChanged</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#length">Length</a></li>
            <li><a href="#count">Count</a></li>
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
            <li><a href="#containst">Contains(T)</a></li>
            <li><a href="#indexoft">IndexOf(T)</a></li>
            <li><a href="#copytot-int">CopyTo(T[], int)</a></li>
            <li><a href="#copytot-array-int-arrayindex">CopyTo(int, T[], int, int)</a></li>
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
        - [OnItemChanged](#onitemchanged)
    - [Properties](#-properties)
        - [Length](#length)
        - [Count](#count)
    - [Indexers](#-indexers)
        - [[int index]](#int-index)
    - [Methods](#-methods)
        - [Contains(T)](#containst)
        - [IndexOf(T)](#indexoft)
        - [CopyTo(T[], int)](#copytot-int)
        - [CopyTo(int, T[], int, int)](#copytot-array-int-arrayindex)
        - [GetEnumerator()](#getenumerator)
-->
---

## 🗂 Example of Usage

```csharp
// Assume we have a read-only reactive array
IReadOnlyReactiveArray<int> readOnlyArray = ...; // your implementation

// Subscribe to item changes
readOnlyArray.OnItemChanged += (index, newValue) =>
{
    Console.WriteLine($"Item at index {index} changed to {newValue}");
};

// Subscribe to global state changes
readOnlyArray.OnStateChanged += () =>
{
    Console.WriteLine("Array state changed");
};

// Access elements
for (int i = 0; i < readOnlyArray.Length; i++)
{
    Console.WriteLine($"Element {i}: {readOnlyArray[i]}");
}

// Check if array contains a value
if (readOnlyArray.Contains(42))
{
    Console.WriteLine("Array contains 42");
}

// Get index of a value
int indexOfValue = readOnlyArray.IndexOf(42);
Console.WriteLine($"Index of 42: {indexOfValue}");

// Copy elements to a standard array
int[] target = new int[readOnlyArray.Length];
readOnlyArray.Copy(0, target, 0, readOnlyArray.Length);
Console.WriteLine("Elements copied to target array");
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IReadOnlyReactiveArray<T> : IReadOnlyList<T>
```

- **Description:** Represents a **read-only reactive array** that notifies subscribers about changes to its elements and
  global state.
- **Inheritance:** `IReadOnlyList<T>`, `IReadOnlyCollection<T>`, `IEnumerable<T>`.
- **Type Parameter:** `T` — The type of elements stored in the array.
- **Note:** Ideal for exposing reactive arrays without allowing external modifications, ensuring that only controlled
  updates trigger events.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Triggered when the array's state changes globally (e.g., multiple items updated, cleared, or reset).

#### `OnItemChanged`

```csharp
public event Action<int, T> OnItemChanged;
```

- **Description:** Triggered when an item at a specific index changes.
- **Parameters:**
    - `index` — index of the changed element.
    - `value` — `T` the new value of the element.

---

### 🔑 Properties

#### `Length`

```csharp
public int Length { get; }
```

- **Description:** Gets the total number of elements in the array.

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of elements in the collection.
- **Notes:** Implemented explicitly from `IReadOnlyCollection<T>`. Returns the same value as `Length`.

---

<div id="-indexers"></div>

### 🏷️ Indexers

#### `[int index]`

```csharp
public T this[int index] { get; }
```

- **Description:** Gets the element at the specified index.
- **Parameters:** `index` — zero-based index of the element.
- **Returns:** `T` — the element at the specified index.

---

### 🏹 Methods

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

#### `CopyTo(int, T[], int, int)`

```csharp
public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length);
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

- **Description:** Returns an enumerator that iterates through the array.
- **Remarks:** Inherited from `IEnumerable<T>`.