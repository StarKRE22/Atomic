# 🧩 IEntityWorld

Represents a world that manages a collection of entities and controls their lifecycle events.  
Extends `IEntityCollection`, `IEnableSource`, and `IUpdateSource`.

---

## Key Features

- **Entity collection management** – Serves as a container for entities with fast insertion, removal, and iteration.
- **Lifecycle control** – Integrates with `IEnableSource` and `IUpdateSource` for enabling, disabling, and frame-based updates (Update, FixedUpdate, LateUpdate).
- **Generic and non-generic support** – Can be used with a specific entity type (`IEntityWorld<E>`) or without specifying a type (`IEntityWorld`).
- **Identifiable** – Each world has a `Name` property for easier tracking in logs, UI, or debugging.
- **Reactive events** – Exposes events for entity addition, removal, state changes, and world enable/disable.


## Interfaces

### `IEntityWorld`

A **non-generic version** of [`IEntityWorld<E>`](#) specialized for `IEntity`.  
Use this interface when you do not need to specify a particular entity type.

---

### `IEntityWorld<E>`

A **generic interface** representing a world that manages a collection of entities of type `E`.  

#### Type Parameters
- `E` – The type of entity managed by this world. Must implement [`IEntity`](#).

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

## Remarks

- Designed for **scalable and reactive entity management**.
- Allows combining **collection operations** with **update and enable/disable control** in a single abstraction.
- Works seamlessly with other `Atomic.Entities` systems, such as `IEntityCollection` and lifecycle extension methods.
