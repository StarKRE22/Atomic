# 🧩️ IReadOnlyEntityCollection<E>

A read-only, observable collection of entities of type `E`.  
Provides enumeration, presence checking, and notifications for changes in the collection.

### Type Parameters
- `E` – The type of entity contained in the collection. Must implement [`IEntity`](#).

---

## Key Features

- **Read-only access** – Prevents direct modifications to the collection, ensuring controlled entity management.
- **Change notifications** – Provides events (`OnStateChanged`, `OnAdded`, `OnRemoved`) for reactive programming or UI updates.
- **Entity presence checks** – Quickly determine if a specific entity exists in the collection via `Contains`.
- **Copy support** – Easily copy entities into arrays or other collections for processing.
- **Enumerable** – Implements `IReadOnlyCollection<E>` allowing iteration over all entities.

---

## Events

| Event            | Description                                                                                          |
|------------------|------------------------------------------------------------------------------------------------------|
| `OnStateChanged` | Raised whenever the collection’s state changes (an entity is added or removed).                      |
| `OnAdded`        | Raised when an entity is added to the collection. The added entity is provided as an argument.       |
| `OnRemoved`      | Raised when an entity is removed from the collection. The removed entity is provided as an argument. |

---

## Methods

| Method                                   | Description                                                                         |
|------------------------------------------|-------------------------------------------------------------------------------------|
| `bool Contains(E entity)`                | Checks if the specified entity exists in the collection. Returns `true` if present. |
| `void CopyTo(ICollection<E> results)`    | Copies all entities into the provided `ICollection<E>`.                             |
| `void CopyTo(E[] array, int arrayIndex)` | Copies all entities into the specified array starting at the given index.           |

---

## Remarks
- Developed for maximum efficiency in memory and speed.
- External modifications are not allowed; collection changes are managed by the system owning the entities.
