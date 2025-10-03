# 🧩️ EntityCollection

A high-performance, mutable, and observable collection of unique entities of type `E`.  
Optimized for fast insertion, removal, and lookup while maintaining insertion order through a linked-list/hash-table hybrid.

### Type Parameters
- `E` – The type of entity stored in the collection. Must implement [`IEntity`](#).

---

## Key Features

- **Optimized storage** – Combines hash table for fast lookups with a doubly-linked list for ordered enumeration.
- **Fast operations** – Constant-time insertion, presence checking, and removal for typical use cases.
- **Reactive events** – `OnStateChanged`, `OnAdded`, `OnRemoved` allow UI updates or reactive systems.
- **Enumerable** – Supports iteration over entities in insertion order.
- **Copy support** – Copy entities to arrays or other collections efficiently.
- **Memory-efficient** – Uses pooled arrays for temporary operations and minimizes allocations.
- **Lifecycle management** – Supports clearing, disposal, and proper resource management.

---

## Events

| Event            | Description                                                                                          |
|------------------|------------------------------------------------------------------------------------------------------|
| `OnStateChanged` | Raised whenever the collection’s state changes (an entity is added, removed, or cleared).            |
| `OnAdded`        | Raised when an entity is added to the collection. The added entity is provided as an argument.       |
| `OnRemoved`      | Raised when an entity is removed from the collection. The removed entity is provided as an argument. |

---

## Properties

| Property          | Description                                                     |
|-------------------|-----------------------------------------------------------------|
| `int Count`       | Gets the number of entities in the collection.                  |
| `bool IsReadOnly` | Indicates whether the collection is read-only (always `false`). |

---

## Methods

| Method                                   | Description                                                                         |
|------------------------------------------|-------------------------------------------------------------------------------------|
| `bool Contains(E item)`                  | Checks if the specified entity exists in the collection. Returns `true` if present. |
| `bool Add(E item)`                       | Adds an entity to the collection. Returns `false` if it already exists.             |
| `bool Remove(E item)`                    | Removes an entity from the collection. Returns `true` if the entity was removed.    |
| `void Clear()`                           | Removes all entities from the collection and raises appropriate events.             |
| `void CopyTo(E[] array, int arrayIndex)` | Copies all entities into the specified array starting at the given index.           |
| `void CopyTo(ICollection<E> results)`    | Copies all entities into the provided `ICollection<E>`.                             |
| `Enumerator GetEnumerator()`             | Returns a strongly-typed enumerator for iterating over the collection.              |
| `void Dispose()`                         | Clears the collection and releases resources. Unsubscribes all events.              |

---

## Enumerator

- Iterates over entities in insertion order.
- Implements `IEnumerator<E>` and `IEnumerator`.
- Automatically tracks the current entity and moves through the linked list of slots.

---

## Remarks

- Designed for maximum efficiency in memory usage and speed.
- Ensures unique entity storage with fast hash-based lookups.
- Reactive events allow integration with UI, game logic, or other systems that require real-time updates.
- Supports standard .NET collection interfaces for seamless API compatibility.

## Example Usage

```csharp
// Create a new collection of entities
var collection = new EntityCollection<MyEntity>();

// Add entities individually
collection.Add(new MyEntity("Player1"));
collection.Add(new MyEntity("Player2"));

// Add a range of entities
collection.AddRange(
    new MyEntity("Enemy1"),
    new MyEntity("Enemy2")
);

// Iterate over entities
foreach (MyEntity entity in collection)
    Console.WriteLine(entity.Name);

// Check for presence
if (collection.Contains(someEntity))
    Console.WriteLine($"{someEntity.Name} is in the collection.");

// Remove an entity
collection.Remove(someEntity);

// Clear all entities
collection.Clear();

// Subscribe to events
collection.OnAdded += entity => Console.WriteLine($"Added: {entity.Name}");
collection.OnRemoved += entity => Console.WriteLine($"Removed: {entity.Name}");
collection.OnStateChanged += () => Console.WriteLine("Collection changed");

// Dispose the collection when done
collection.Dispose();
```