# 🧩 EntityWorld

A runtime-managed world that holds a collection of entities and controls their lifecycle, updates, and reactive events.  
Supports both generic (`EntityWorld<E>`) and non-generic (`EntityWorld`) usage.

### Type Parameters
- `E` – The specific type of entity managed by the world. Must implement [`IEntity`](#).

---

## Key Features

- **Entity collection management** – Acts as a container for entities with fast lookup, insertion order, and iteration.
- **Lifecycle control** – Implements `IEnableSource` and `IUpdateSource` to enable, disable, and update entities.
- **Generic and non-generic support** – Flexible usage whether a specific entity type is required or not.
- **Identifiable** – Each world can have a `Name` for easier tracking in logs, UI, or debugging.
- **Reactive events** – Supports `OnEnabled`, `OnDisabled`, `OnUpdated`, `OnFixedUpdated`, and `OnLateUpdated` for integration with systems or UI.

---

## Events

| Event            | Parameters        | Description                                                                     |
|------------------|-------------------|---------------------------------------------------------------------------------|
| `OnStateChanged` | —                 | From `IReadOnlyEntityCollection<E>`. Raised when entities are added or removed. |
| `OnAdded`        | `E entity`        | From `IReadOnlyEntityCollection<E>`. Raised when an entity is added.            |
| `OnRemoved`      | `E entity`        | From `IReadOnlyEntityCollection<E>`. Raised when an entity is removed.          |
| `OnEnabled`      | —                 | From `IEnableSource`. Raised when the world is enabled.                         |
| `OnDisabled`     | —                 | From `IEnableSource`. Raised when the world is disabled.                        |
| `OnUpdated`      | `float deltaTime` | From `IUpdateSource`. Raised every Update.                                      |
| `OnFixedUpdated` | `float deltaTime` | From `IUpdateSource`. Raised every FixedUpdate.                                 |
| `OnLateUpdated`  | `float deltaTime` | From `IUpdateSource`. Raised every LateUpdate.                                  |

---

## Properties

| Property     | Type     | Description                                                                    |
|--------------|----------|--------------------------------------------------------------------------------|
| `Name`       | `string` | Gets or sets the name of the entity world, useful for debugging or UI display. |
| `Enabled`    | `bool`   | From `IEnableSource`. Indicates whether the world is currently enabled.        |
| `Count`      | `int`    | From `IEntityCollection<E>`. Number of entities in the world.                  |
| `IsReadOnly` | `bool`   | From `IEntityCollection<E>`. Indicates if the collection is read-only.         |

---

## Methods

| Method                              | Parameters                          | Description                                                                         |
|-------------------------------------|-------------------------------------|-------------------------------------------------------------------------------------|
| `Enable()`                          | —                                   | From `IEnableSource`. Enables the world.                                            |
| `Disable()`                         | —                                   | From `IEnableSource`. Disables the world.                                           |
| `OnUpdate(float deltaTime)`         | `deltaTime` – Time since last frame | From `IUpdateSource`. Called once per frame.                                        |
| `OnFixedUpdate(float deltaTime)`    | `deltaTime` – Fixed time step       | From `IUpdateSource`. Called during FixedUpdate.                                    |
| `OnLateUpdate(float deltaTime)`     | `deltaTime` – Time since last frame | From `IUpdateSource`. Called during LateUpdate.                                     |
| `Add(E entity)`                     | `entity` – Entity to add            | From `IEntityCollection<E>`. Adds an entity to the world.                           |
| `Remove(E entity)`                  | `entity` – Entity to remove         | From `IEntityCollection<E>`. Removes an entity from the world.                      |
| `Clear()`                           | —                                   | From `IEntityCollection<E>`. Removes all entities.                                  |
| `Contains(E entity)`                | `entity` – Entity to check          | From `IEntityCollection<E>`. Returns `true` if entity exists.                       |
| `CopyTo(E[] array, int arrayIndex)` | `array`, `arrayIndex`               | Copies entities to an array.                                                        |
| `CopyTo(ICollection<E> results)`    | `results`                           | Copies entities to a collection.                                                    |
| `Dispose()`                         | —                                   | From `IEntityCollection<E>` and `IDisposable`. Disposes the world and its entities. |

---

## Example Usage

```csharp
var world = new EntityWorld<MyEntity>("GameplayWorld");

// Add entities
world.Add(new MyEntity("Player"));
world.AddRange(new MyEntity("Enemy1"), new MyEntity("Enemy2"));

// Enable world and entities
world.Enable();

// Update loop
world.OnUpdate(Time.deltaTime);
world.OnFixedUpdate(Time.fixedDeltaTime);
world.OnLateUpdate(Time.deltaTime);

// Subscribe to events
world.OnEnabled += () => Console.WriteLine("World enabled");
world.OnDisabled += () => Console.WriteLine("World disabled");

// Disable world
world.Disable();

// Dispose world
world.Dispose();
```

## Performance
#TODO

## Remarks
- Ensures consistent lifecycle management for all entities in the world.
- Integrates reactive events for real-time updates and UI binding.
- Supports standard .NET collection patterns for seamless integration.
- Optimized for high-performance scenarios like games, simulations, or real-time systems.