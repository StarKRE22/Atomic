# 🧩 EntityRegistry

A global singleton registry responsible for tracking and managing all `IEntity` instances.  
Provides unique ID assignment, fast lookup, and name-based search utilities.

---

## Key Features

- **Singleton access** – Only one instance exists via `EntityRegistry.Instance`.
- **Unique ID management** – Automatically assigns and recycles integer IDs for entities.
- **Entity lookup** – Supports search by ID or by reference.
- **Collection support** – Implements `IReadOnlyEntityCollection<IEntity>` for enumeration and copying.
- **Events** – Notifies when entities are added, removed, or the registry state changes.
- **Editor integration** – Can automatically reset in Unity Editor before entering Play Mode.

---

## Static Properties

| Property   | Description                                             |
|------------|---------------------------------------------------------|
| `Instance` | Returns the singleton instance of the `EntityRegistry`. |

## Events

| Event            | Description                                                                                 |
|------------------|---------------------------------------------------------------------------------------------|
| `OnStateChanged` | Triggered whenever the registry’s state changes (e.g., entities added or removed).          |
| `OnAdded`        | Triggered when an entity is registered in the registry. The added entity is provided.       |
| `OnRemoved`      | Triggered when an entity is unregistered from the registry. The removed entity is provided. |

## Properties

| Property          | Description                                             |
|-------------------|---------------------------------------------------------|
| `int Count`       | Gets the total number of entities currently registered. |

## Methods

| Method                                         | Description                                                                          |
|------------------------------------------------|--------------------------------------------------------------------------------------|
| `bool Contains(int id)`                        | Checks if an entity with the specified ID exists in the registry.                    |
| `bool Contains(IEntity entity)`                | Checks if the specified entity exists in the registry.                               |
| `void CopyTo(ICollection<IEntity> results)`    | Copies all registered entities into the provided collection.                         |
| `void CopyTo(IEntity[] array, int arrayIndex)` | Copies all registered entities into the given array starting at the specified index. |
| `bool TryGet(int id, out IEntity entity)`      | Attempts to retrieve an entity by its ID. Returns `true` if found.                   |
| `IEntity Get(int id)`                          | Retrieves an entity by its ID. Throws an exception if not found.                     |
| `IEnumerator<IEntity> GetEnumerator()`         | Returns an enumerator over all registered entities.                                  |
| `void ResetAll()`                              | **Editor only:** Clears the registry when entering Play Mode.                        |

## Usage Example

```csharp
// Get the singleton instance
var registry = EntityRegistry.Instance;

// Check if entity exists
if (registry.Contains(id))
{
    IEntity entity = registry.Get(id);
    Console.WriteLine($"Entity found: {entity}");
}

// Enumerate all entities
foreach (var e in registry)
    Console.WriteLine($"Entity: {e}");

// Unregister entity
registry.Unregister(ref id);
```