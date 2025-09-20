# 🧩 IEntity

Represents the fundamental interface of entity in the framework. It follows the **Entity–State–Behaviour** pattern and
provides a modular container for **dynamic state**, **tags**, **values**, **behaviours**, and **lifecycle management**.

```csharp
public interface IEntity : IInitLifecycle, IEnableLifecycle, ITickLifecycle
```

- **Inheritance:**
    - [IInitLifecycle](../Lifecycle/Sources/IInitLifecycle.md) – Supports explicit initialization and disposal.
    - [IEnableLifecycle](../Lifecycle/Sources/IEnableLifecycle.md) – Supports runtime enabling and disabling.
    - [ITickLifecycle](../Lifecycle/Sources/ITickLifecycle.md) – Supports `Tick`, `FixedTick`, and `LateTick` callbacks.

---

## 📚 Content

- [Core](#-core-members)
- [Tags](#-tag-members)
- [Values](#-value-members)
- [Behaviours](#-behaviour-members)
- [Lifecycle](#-lifecycle-members)
- [Example Usage](#-example-usage)
- [Notes](#-notes)

---

## 💠 Core Members

Represent the fundamental identity and state of the entity. It includes unique identifiers, optional names for
debugging or tooling, and the main event for reactive state
changes.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action<IEntity> OnStateChanged
```

- **Description:** Triggered whenever the entity’s internal state changes.
- **Parameter:** `IEntity` – This entity.
- **Note:** Useful for reacting to lifecycle or state transitions of an entity.

### 🔑 Properties

#### `InstanceID`

```csharp
public int InstanceID { get; }
```

- **Description:** Runtime-generated unique identifier.
- **Notes:**
    - Ensures uniqueness of the entity instance during runtime.
    - Should not be used for persistence or serialization.

---

#### `Name`

```csharp
public string Name { get; set; }
```

- **Description:** Optional user-defined name for debugging or tooling.
- **Note:** Useful for logging, inspector display, or editor tooling.

---

## 💠 Tag Members

Manage lightweight categorization and filtering of entities. Tags are integer-based labels that can be added, removed,
enumerated, or checked. They are useful for grouping entities, querying, and driving logic based on assigned tags.

---

### ⚡ Events

#### `OnTagAdded`

```csharp
public event Action<IEntity, int> OnTagAdded
```

- **Description:** Triggered when a tag is added.
- **Parameters:**
    - `IEntity` — This entity.
    - `int` – The tag that was added.
- **Note:** Useful for reacting to dynamic tagging of entities.

---

#### `OnTagDeleted`

```csharp
public event Action<IEntity, int> OnTagDeleted
```

- **Description:** Triggered when a tag is removed.
- **Parameters:**
    - `IEntity` — This entity.
    - `int` – The tag that was removed.

- **Note:** Allows cleanup or logic adjustment when tags are deleted.

---

### 🔑 Properties

#### `TagCount`

```csharp
public int TagCount { get; }
```

- **Description:** Number of associated tags.
- **Note:** Reflects how many tags are currently attached to the entity.

---

### 🏹 Methods

#### `HasTag`

```csharp
public bool HasTag(int tag)
```

- **Description:** Checks if the entity has the given tag.
- **Parameter:** `tag` – The tag to check for.
- **Returns:** `true` if the tag exists, otherwise `false`.

#### `AddTag`

```csharp
public bool AddTag(int tag)
```

- **Description:** Adds a tag to the entity.
- **Parameter:** `int tag` – The tag to add.
- **Returns:** `true` if the tag was added, otherwise `false`.
- **Triggers:** `OnTagAdded` and `OnStateChanged`

#### `DelTag`

```csharp
public bool DelTag(int tag)
```

- **Description:** Removes a tag from the entity.
- **Parameter:** `tag` – The tag to remove.
- **Returns:** `true` if the tag was removed, otherwise `false`.
- **Triggers:** `OnTagDeleted` and `OnStateChanged`

#### `ClearTags`

```csharp
public void ClearTags()
```

- **Description:** Removes all tags from the entity.
- **Triggers:** `OnTagDeleted` and `OnStateChanged`

#### `GetTags`

```csharp
public int[] GetTags()
```

- **Description:** Returns all tag keys associated with the entity.
- **Returns:** Array of tag keys.

#### `CopyTags`

```csharp
public int CopyTags(int[] results)
```

- **Description:** Copies tag keys into the provided array.
- **Parameter:** `results` – Array to copy the tags into.
- **Returns:** Number of tags copied.

#### `GetTagEnumerator`

```csharp
public IEnumerator<int> GetTagEnumerator()
```

- **Description:** Enumerates all tags of the entity.
- **Returns:** `IEnumerator<int>` – Enumerator over tag keys.

---

## 💠 Value Members

Manage dynamic key-value storage for the entity. Values can be of any type (structs or reference types) and are
identified by integer keys. This allows flexible runtime data storage, reactive updates, and modular logic.

### Events

| Event            | Description                        |
|------------------|------------------------------------|
| `OnValueAdded`   | Triggered when a value is added.   |
| `OnValueDeleted` | Triggered when a value is deleted. |
| `OnValueChanged` | Triggered when a value is updated. |

### Properties

| Property     | Type | Description              |
|--------------|------|--------------------------|
| `ValueCount` | int  | Number of stored values. |

### Methods

| Method                                    | Description                              |
|-------------------------------------------|------------------------------------------|
| `GetValue<T>(int)`                        | Retrieves a value by key.                |
| `GetValueUnsafe<T>(int)`                  | Retrieves a value by reference (unsafe). |
| `GetValue(int)`                           | Retrieves a value as `object`.           |
| `TryGetValue<T>(int, out T)`              | Tries to retrieve a typed value.         |
| `TryGetValueUnsafe<T>(int, out T)`        | Tries to retrieve by reference.          |
| `TryGetValue(int, out object)`            | Tries to retrieve as `object`.           |
| `SetValue<T>(int, T)`                     | Sets or updates a struct value.          |
| `SetValue(int, object)`                   | Sets or updates a reference value.       |
| `HasValue(int)`                           | Checks if a value exists.                |
| `AddValue<T>(int, T)`                     | Adds a struct value.                     |
| `AddValue(int, object)`                   | Adds a reference value.                  |
| `DelValue(int)`                           | Deletes a value.                         |
| `ClearValues()`                           | Clears all values.                       |
| `GetValues()`                             | Returns all key-value pairs.             |
| `CopyValues(KeyValuePair<int, object>[])` | Copies all key-value pairs.              |
| `GetValueEnumerator()`                    | Enumerates all values.                   |

---

## 💠️ Behaviour Members

The **Behaviours** section manages modular logic attached to the entity.  
Behaviours implement `IEntityBehaviour` interfaces and can be added, removed, queried, or enumerated at runtime.  
This allows flexible composition of entity logic, enabling dynamic functionality without changing the core entity
structure.

Behaviours can respond to lifecycle events (`Init`, `Enable`, `Update`, `Disable`, `Dispose`),
enabling dynamic logic composition without changing the core entity structure.

### Events

| Event                | Description                            |
|----------------------|----------------------------------------|
| `OnBehaviourAdded`   | Triggered when a behaviour is added.   |
| `OnBehaviourDeleted` | Triggered when a behaviour is removed. |

### Properties

| Property         | Type | Description                    |
|------------------|------|--------------------------------|
| `BehaviourCount` | int  | Number of attached behaviours. |

### Methods

| Method                               | Description                               |
|--------------------------------------|-------------------------------------------|
| `AddBehaviour(IEntityBehaviour)`     | Adds a behaviour.                         |
| `GetBehaviour<T>()`                  | Returns first behaviour of type `T`.      |
| `TryGetBehaviour<T>(out T)`          | Tries to get a behaviour of type `T`.     |
| `HasBehaviour(IEntityBehaviour)`     | Checks if a specific behaviour exists.    |
| `HasBehaviour<T>()`                  | Checks if a behaviour of type `T` exists. |
| `DelBehaviour(IEntityBehaviour)`     | Removes a specific behaviour.             |
| `DelBehaviour<T>()`                  | Removes the first behaviour of type `T`.  |
| `DelBehaviours<T>()`                 | Removes all behaviours of type `T`.       |
| `ClearBehaviours()`                  | Removes all behaviours.                   |
| `GetBehaviours()`                    | Returns all behaviours.                   |
| `GetBehaviours<T>()`                 | Returns all behaviours of type `T`.       |
| `CopyBehaviours(IEntityBehaviour[])` | Copies behaviours into an array.          |
| `CopyBehaviours<T>(T[])`             | Copies behaviours of type `T`.            |
| `GetBehaviourEnumerator()`           | Enumerates behaviours.                    |

----

## 💠 Lifecycle Members

The **Lifecycle** section manages the entity's state transitions and update phases.  
It covers initialization, enabling, per-frame updates, disabling, and disposal.  
Lifecycle events allow reactive systems to respond to changes in the entity's state.

### Events

| Event                   | Description                                 |
|-------------------------|---------------------------------------------|
| `OnInitialize`          | Triggered when the entity is initialized.   |
| `OnEnabled`             | Triggered when the entity is enabled.       |
| `OnDisabled`            | Triggered when the entity is disabled.      |
| `OnDisposed`            | Triggered when the entity is disposed.      |
| `OnUpdated(float)`      | Triggered when the entity is updated.       |
| `OnFixedUpdated(float)` | Triggered when the entity is fixed updated. |
| `OnLateUpdated(float)`  | Triggered when the entity is late updated.  |

### Properties

| Property      | Type | Description                        |
|---------------|------|------------------------------------|
| `Initialized` | bool | True if the entity is initialized. |
| `Enabled`     | bool | True if the entity is enabled.     |

### Methods

| Method                           | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
|----------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Init()`                         | Initializes the entity. <ul><li>Transitions the entity to the `Initialized` state.</li><li>Calls `Init` on all behaviours implementing `IEntityInit`.</li><li>Triggers the `OnInitialized` event.</li><li>If the entity is already initialized, does nothing.</li></ul>                                                                                                                                                                                                                                                     |
| `Enable()`                       | Enables the entity for updates. <ul><li>Transitions the entity to the `Enabled` state.</li><li>Calls `Enable` on all behaviours implementing `IEntityEnable`.</li><li>Triggers the `OnEnabled` event.</li><li>If the entity is not initialized yet, it will be initialized automatically.</li><li>If the entity is already enabled, does nothing.</li></ul>                                                                                                                                                                 |
| `OnUpdate(float deltaTime)`      | Calls `Update` on all behaviours implementing `IEntityUpdate`. <ul><li>Triggers the `OnUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                                      |
| `OnFixedUpdate(float deltaTime)` | Calls `FixedUpdate` on all behaviours implementing `IEntityFixedUpdate`. <ul><li>Triggers the `OnFixedUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                       |
| `OnLateUpdate(float deltaTime)`  | Calls `LateUpdate` on all behaviours implementing `IEntityLateUpdate`. <ul><li>Triggers the `OnLateUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                          |
| `Disable()`                      | Disables the entity for updates. <ul><li>Transitions the entity to a not `Enabled` state.</li><li>Calls `Disable` on all behaviours implementing `IEntityDisable`.</li><li>Triggers the `OnDisabled` event.</li><li>If the entity is not enabled yet, does nothing.</li></ul>                                                                                                                                                                                                                                               |
| `Dispose()`                      | Cleans up all resources used by the entity. <ul><li>Transitions the entity to a not `Initialized` state.</li><li>Calls `Dispose` on all behaviours implementing `IEntityDispose`.</li><li>Clears all tags, values, and behaviours.</li><li>Unsubscribes from all events.</li><li>Unregisters the entity from the `EntityRegistry`.</li><li>If the entity is enabled, calls `Disable` automatically.</li><li>If the entity is not initialized yet, does not call `IEntityDispose.Dispose` or trigger `OnDisposed`.</li></ul> |

---

## 💡 Example Usage

```csharp
// Create a new entity in pure C#
IEntity entity = new Entity();
entity.Name = "Player";

// Add a tag
entity.AddTag(1); // 1 = PlayerTag

// Add a value
entity.AddValue("Health".GetHashCode(), 100);

// Add a behaviour
entity.AddBehaviour(new MovementBehaviour());

// Initialize and enable the entity
entity.Init();
entity.Enable();

// Update manually (for example in a game loop)
entity.OnUpdate(Time.deltaTime);
```

## 📝 Notes

- Supports **reactive programming** via `OnStateChanged`.
- Focused on **interface contract**, not implementation.
- Can be implemented by pure C# classes or Unity `MonoBehaviour`s.
- Interface-based design enables **easy mocking and testing**.

## 📝 Notes

- **Event-Driven** – Reactive programming support via state change notifications.
- **Unique Identity** – Runtime-generated instance ID for entity tracking.
- **Tag System** – Lightweight categorization and filtering.
- **State Management** – Dynamic key-value storage for runtime data.
- **Behaviour Composition** – Attach or detach modular logic at runtime.
- **Lifecycle Control** – Built-in support for `Init`, `Enable`, `Update`, `Disable`, and `Dispose` phases.
