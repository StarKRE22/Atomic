# 🧩️ IEntityCollection<E>

A mutable collection of entities of type `E`.  
Supports standard collection operations and provides utility methods for entity lifecycle management.

### Type Parameters
- `E` – The type of entity stored in the collection. Must implement [`IEntity`](#).

---

## Key Features

- **Optimized storage** – Designed for minimal memory usage and fast entity operations.
- **Fast operations** – Efficient insertion, presence checking, and removal of entities.
- **Reactive** – Inherits `OnStateChanged`, `OnAdded`, and `OnRemoved` from `IReadOnlyEntityCollection<E>`.
- **Enumerable and mutable** – Implements `ICollection<E>` for iteration and modification.
- **Lifecycle-aware** – Designed for managing entities with proper disposal and lifecycle handling.

---

## Events

| Event            | Description                                                                                          |
|------------------|------------------------------------------------------------------------------------------------------|
| `OnStateChanged` | Raised whenever the collection’s state changes (an entity is added or removed).                      |
| `OnAdded`        | Raised when an entity is added to the collection. The added entity is provided as an argument.       |
| `OnRemoved`      | Raised when an entity is removed from the collection. The removed entity is provided as an argument. |


## Properties

| Property          | Description                                                                                |
|-------------------|--------------------------------------------------------------------------------------------|
| `int Count`       | Gets the number of entities in the collection.                                             |
| `bool IsReadOnly` | Indicates whether the collection is read-only. Returns `false` for `IEntityCollection<E>`. |

---

## Methods

| Method                                   | Description                                                                         |
|------------------------------------------|-------------------------------------------------------------------------------------|
| `bool Contains(E entity)`                | Checks if the specified entity exists in the collection. Returns `true` if present. |
| `bool Add(E entity)`                     | Adds the entity to the collection. Returns `false` if the entity already exists.    |
| `bool Remove(E entity)`                  | Removes the entity from the collection. Returns `true` if the entity was removed.   |
| `void Clear()`                           | Removes all entities from the collection and raises appropriate events.             |
| `void CopyTo(E[] array, int arrayIndex)` | Copies all entities into the specified array starting at the given index.           |
| `void CopyTo(ICollection<E> results)`    | Copies all entities into the provided `ICollection<E>`.                             |
| `void Dispose()`                         | Releases resources held by the collection and disposes entities if necessary.       |

---

### Remarks

- Mutable counterpart of [`IReadOnlyEntityCollection<E>`](#).
- Designed for high-performance use in game and simulation environments.
- Supports efficient tracking of entity presence and reactive event notifications.
