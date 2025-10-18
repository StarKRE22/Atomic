# 🧩 ReactiveArray&lt;T&gt;

Represents a **fixed-size reactive array** that emits events when elements change.
It provides indexed access, supports enumeration. Use this class when you need a read-write reactive array with change
notifications and iteration support.

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
          <summary><a href="#-constructors">Constructors</a></summary>
          <ul>
            <li><a href="#reactivearrayint">ReactiveArray(int)</a></li>
            <li><a href="#reactivearrayparams-t">ReactiveArray(params T[])</a></li>
            <li><a href="#reactivearrayienumerablet">ReactiveArray(IEnumerable&lt;T&gt;)</a></li>
          </ul>
        </details>
      </li>
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
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#populateienumerablet">Populate(IEnumerable&lt;T&gt;)</a></li>
            <li><a href="#fillt">Fill(T)</a></li>
            <li><a href="#resizeint">Resize(int)</a></li>
            <li><a href="#containst">Contains(T)</a></li>
            <li><a href="#indexoft">IndexOf(T)</a></li>
            <li><a href="#copytot-int">CopyTo(T[], int)</a></li>
            <li><a href="#copyint-t-int-int-int">Copy(int, T[], int, int)</a></li>
            <li><a href="#getenumerator">GetEnumerator()</a></li>
            <li><a href="#dispose">Dispose()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
  <li><a href="#-useful-links">Useful links</a></li>
</ul>

<!--

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Inspector Settings](#-inspector-settings)
    - [Constructors](#-constructors)
        - [ReactiveArray(int)](#reactivearrayint)
        - [ReactiveArray(params T[])](#reactivearrayparams-t)
        - [ReactiveArray(IEnumerable<T>)](#reactivearrayienumerablet)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
        - [OnItemChanged](#onitemchanged)
    - [Properties](#-properties)
        - [Length](#length)
        - [Count](#count)
    - [Indexers](#-indexers)
        - [[int index]](#int-index)
    - [Methods](#-methods)
        - [Clear()](#clear)
        - [Populate(IEnumerable<T>)](#populateienumerablet)
        - [Fill(T)](#fillt)
        - [Resize(int)](#resizeint)
        - [Contains(T)](#containst)
        - [IndexOf(T)](#indexoft)
        - [CopyTo(T[], int)](#copytot-int)
        - [Copy(int, T[], int, int)](#copyint-t-int-int-int)
        - [GetEnumerator()](#getenumerator)
        - [Dispose()](#dispose)
- [Useful links](#-useful-links)

-->
---

## 🗂 Example of Usage

```csharp
// Create a reactive array with initial values
var reactiveArray = new ReactiveArray<int>(1, 2, 3, 4);

// Subscribe to events
reactiveArray.OnItemChanged += (index, value) => Console.WriteLine($"Item {index} changed to {value}");
reactiveArray.OnStateChanged += () => Console.WriteLine("Array state changed");

// Access and modify elements
reactiveArray[1] = 20; // Triggers OnItemChanged and OnStateChanged

// Fill all elements
reactiveArray.Fill(10);

// Populate new values
reactiveArray.Populate(new int[] { 5, 6, 7, 8 });

// Resize the array
reactiveArray.Resize(6); // Adds two default elements

// Clear the array
reactiveArray.Clear();

// Iterate through elements
foreach (var item in reactiveArray)
{
    Console.WriteLine(item);
}

// Copy to standard array
int[] target = new int[reactiveArray.Length];
reactiveArray.Copy(0, target, 0, reactiveArray.Length);
```

---

## 🛠 Inspector Settings

| Parameter | Description                        |
|-----------|------------------------------------|
| `items`   | The initial elements of the array. |

---


## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class ReactiveArray<T> : IReactiveArray<T>, IDisposable
```

- **Description:** Represents a **fixed-size reactive array** that emits events when elements change.
  It provides indexed access, supports enumeration
- **Inheritance:** implements [IReactiveArray&lt;T&gt;](IReactiveArray.md) and `IDisposable`
- **Type Parameter:** `T` — The type of elements stored in the array.
- **Note:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `ReactiveArray(int)`

```csharp
public ReactiveArray(int capacity);
```

- **Description:** Creates a new reactive array with the specified capacity.
- **Parameters:** `capacity` — the size of the internal array. Must be non-negative.
- **Exceptions:** Throws `ArgumentOutOfRangeException` if `capacity` is negative.
- **Remarks:** The array is initialized with default values for type `T`.

#### `ReactiveArray(params T[])`

```csharp
public ReactiveArray(params T[] elements);
```

- **Description:** Creates a reactive array initialized with the given elements.
- **Parameters:** `elements` — elements to initialize the array with.
- **Remarks:** The array length matches the number of provided elements.

#### `ReactiveArray(IEnumerable<T>)`

```csharp
public ReactiveArray(IEnumerable<T> elements);
```

- **Description:** Creates a reactive array initialized with the given elements.
- **Parameters:** `elements` — elements to initialize the array with.
- **Remarks:** The array length matches the number of provided elements.

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

### 🏷️ Indexers

#### `[int index]`

```csharp
public T this[int index] { get; set; }
```

- **Description:** Gets or sets the element at the specified index.
- **Parameters:** `index` — zero-based index of the element.
- **Returns:** `T` — the element at the specified index.
- **Remarks:** Setting a new value triggers the `OnItemChanged` event if the value changes.

---

### 🏹 Methods

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all elements from the array.
- **Remarks:** Triggers the `OnStateChanged` event.

#### `Populate(IEnumerable<T>)`

```csharp
public void Populate(IEnumerable<T> newItems);
```

- **Description:** Updates the contents of the array with values from the specified collection.
- **Parameters:** `newItems` — collection of new elements to populate the array with.
- **Remarks:** Triggers the `OnStateChanged` event.

#### `Fill(T)`

```csharp
public void Fill(T value);
```

- **Description:** Sets all elements of the array to the specified value.
- **Parameters:** `value` — the value to assign to each element.
- **Remarks:** Triggers the `OnStateChanged` event.

#### `Resize(int)`

```csharp
public void Resize(int newSize);
```

- **Description:** Changes the size of the array to the specified length.
- **Parameters:** `newSize` — new length of the array. Must be non-negative.
- **Remarks:** Triggers the `OnStateChanged` event.

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

#### `CopyTo(T[], int)`

```csharp
public void CopyTo(T[] array, int arrayIndex)
```

- **Description:** Copies all items in the array to the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The starting index in the array.

#### `CopyTo(int, T[], int, int)`

```csharp
public void Copy(int sourceIndex, T[] destination, int destinationIndex, int length);
```

- **Description:** Copies a range of elements from this array to a destination array.
- **Parameters:**
    - `int sourceIndex` — starting index in this array.
    - `T[] destination` — array to copy elements to.
    - `int destinationIndex` — starting index in the destination array.
    - `int length` — number of elements to copy.
- **Remarks:** Throws exceptions if indices or lengths are invalid, or if the destination array is too small.

#### `GetEnumerator()`

```csharp
public Enumerator GetEnumerator();
```

- **Description:** Returns a struct-based enumerator that iterates through the collection.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Clears event subscriptions and disposes the array.

---

## 🔗 Useful Links

- [ReactiveArray Performance](../Performance/ReactiveArrayPerformance.md) – performance benchmarks for reactive arrays.
- [Iterating over Reactive Collections](../../BestPractices/IteratingReactiveCollections.md) — best practice.
