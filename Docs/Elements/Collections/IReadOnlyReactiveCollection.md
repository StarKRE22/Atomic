# 🧩 IReadOnlyReactiveCollection&lt;T&gt;

`IReadOnlyReactiveCollection<T>` represents a **read-only reactive collection** that provides notifications when items are added, removed, or when the overall state changes.  
It extends `IReadOnlyCollection<T>`, `IEnumerable<T>`, and `IEnumerable`.

> [!NOTE]  
> Use this interface when you need **read-only access** to a collection but still require **reactive notifications** on changes.

---

## Events

#### `OnStateChanged`
```csharp
event Action OnStateChanged;
```
- **Description:** Occurs when the overall state of the collection changes.
- **Remarks:** This can happen due to bulk operations or significant modifications.

#### `OnItemAdded`
```csharp
event Action<T> OnItemAdded;
```
- **Description:** Occurs when a new item is added to the collection.
- **Parameter:** `value` — the item that was added to the collection.
- **Remarks:** Use this event to react to additions without iterating over the collection.

#### `OnItemRemoved`
```csharp
event Action<T> OnItemRemoved;
```
- **Description:** Occurs when an existing item is removed from the collection.
- **Parameter:** `value` — the item that was removed from the collection.
- **Remarks:** Use this event to react to removals without iterating over the collection.

---

##  Properties

#### `Count`
```csharp
public int Count { get; }
```
- **Description:** Gets the number of elements in the collection.
- **Returns:** An `int` representing the total number of items.

---

## Methods

#### `GetEnumerator()`
```csharp
public IEnumerator<T> GetEnumerator();
```
- **Description:** Returns an enumerator that iterates through the collection.
- **Returns:** An `IEnumerator<T>` for iterating over the collection’s elements.

#### `IEnumerable.GetEnumerator()`
```csharp
System.Collections.IEnumerator IEnumerable.GetEnumerator();
```
- **Description:** Returns a non-generic enumerator for iterating through the collection.
- **Returns:** An `IEnumerator` for iterating over the elements.

---

## 🗂 Example of Usage
```csharp
IReadOnlyReactiveCollection<string> collection = ...;

// Subscribe to events
collection.OnItemAdded += item => Console.WriteLine($"Added: {item}");
collection.OnItemRemoved += item => Console.WriteLine($"Removed: {item}");
collection.OnStateChanged += () => Console.WriteLine("State changed");

// Iterate over items
foreach (var value in collection)
{
    Console.WriteLine(value);
}
```