# 🧩 IReactiveCollection&lt;T&gt;

```csharp
public interface IReactiveCollection<T> : IReadOnlyReactiveCollection<T>, ICollection<T>
```
- **Description:** Represents a **reactive collection** that provides notifications when items are added, removed, or when the overall
state changes. Allows **modification of the collection**. 
- **Inheritance:** [IReadOnlyReactiveCollection&lt;T&gt;](IReadOnlyReactiveCollection.md), `ICollection<T>`.
- **Type Parameter:** `T` — The type of elements stored in the collection.
- **Note:** Use this interface when you need both **reactive notifications** and **write access** (add, remove, clear)
  to the collection.

---

## ⚡ Events

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

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of elements in the collection.
- **Returns:** An `int` representing the total number of items.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Gets a value indicating whether the collection is read-only.
- **Returns:** Always `false` for most implementations of `IReactiveCollection<T>`.

---

## 🏹 Methods

#### `Add(T)`

```csharp
public void Add(T item);
```

- **Description:** Adds an item to the collection.
- **Parameter:** `item` — the object to add to the collection.
- **Events:** Triggers `OnItemAdded` and `OnStateChanged`.

#### `Remove(T)`

```csharp
public bool Remove(T item);
```

- **Description:** Removes the first occurrence of a specific object from the collection.
- **Parameter:** `item` — the object to remove from the collection.
- **Returns:** `true` if the item was successfully removed; otherwise `false`.
- **Events:** Triggers `OnItemRemoved` and `OnStateChanged`.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all items from the collection.
- **Events:** Triggers multiple `OnItemRemoved` events (one per item) and `OnStateChanged`.

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

---

## 🗂 Example of Usage

```csharp
//Assume we have a collection
IReactiveCollection<string> collection = ...;

// Subscribe to events
collection.OnItemAdded += item => Console.WriteLine($"Added: {item}");
collection.OnItemRemoved += item => Console.WriteLine($"Removed: {item}");
collection.OnStateChanged += () => Console.WriteLine("State changed");

// Modify collection
collection.Add("Apple");
collection.Add("Banana");
collection.Remove("Apple");

// Iterate over items
foreach (var value in collection)
{
    Console.WriteLine(value);
}
```
