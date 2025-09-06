# 🧩️ Entity

Represents the fundamental implementation of an `IEntity` in the framework.  
It follows the **Entity–State–Behaviour** pattern and provides a modular container for **dynamic state**, **tags**, **values**, **behaviours**, and **lifecycle management**.

---

## 📚 Content

- [Key Features](#-key-features)
- [Thread Safety](#-thread-safety)
- [Core State](#-core-state)
- [Tags](#-tags)
- [Values](#-values)
- [Behaviours](#-behaviours)
- [Lifecycle](#-lifecycle)
- [Nested Types](#-nested-types)
- [Debug Properties](#-debug-properties)
- [Example Usage](#-example-usage)
- [Performance](#-performance)
- [Notes](#-notes)

## 🔑 Key Features

- **Event-Driven** – Reactive programming support via state change notifications.
- **Unique Identity** – Runtime-generated instance ID for entity tracking.
- **Tag System** – Lightweight categorization and filtering.
- **State Management** – Dynamic key-value storage for runtime data.
- **Behaviour Composition** – Attach or detach modular logic at runtime.
- **Lifecycle Control** – Built-in support for `Init`, `Enable`, `Update`, `Disable`, and `Dispose` phases.
- **Registry Integration** – Automatic registration with EntityRegistry
- **Memory Efficient** – Pre-allocation support for collections
- **Odin Inspector Support** – Optional editor enhancements for configuration and debug.

---

## 🔒 Thread Safety
- `Entity` is **NOT thread-safe**
- All operations should be performed on the main thread
- Use synchronization if accessing from multiple threads

---
## 🧩 Core State

The Core State section defines the essential identity and fundamental data of an entity.
It captures the minimal information needed to track, identify, and observe the entity during its lifecycle.

This section includes:

- **Unique Identity** – runtime-generated `InstanceID` to distinguish entities.
- **Optional Name** – user-defined `Name` for debugging, tooling, or editor purposes.
- **Reactive State** – `OnStateChanged` event to respond to runtime state changes.
- **Entity Creation** – constructors for creating entities with initial names, tags, values, behaviours, and optional settings.

### Events

| Event                   | Description                                             |
|-------------------------|---------------------------------------------------------|
| `OnStateChanged`        | Triggered whenever the entity’s internal state changes. |

### Properties

| Property     | Type   | Description                                          |
|--------------|--------|------------------------------------------------------|
| `InstanceID` | int    | Runtime-generated unique identifier.                 |
| `Name`       | string | Optional user-defined name for debugging or tooling. |

### Constructors

| Signature                                                                                                                                                                                   | Parameters                                                                                                                                                                                                                                          |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Entity(string name, IEnumerable<string> tags = null, IEnumerable<KeyValuePair<string, object>> values = null, IEnumerable<IEntityBehaviour> behaviours = null, Settings? settings = null)` | `name` — Entity name.<br>`tags` — Optional initial tags.<br>`values` — Optional initial values.<br>`behaviours` — Optional initial behaviours.<br>`settings` — Optional settings (disposeValues defaults to true).                                  |
| `Entity(string name, IEnumerable<int> tags = null, IEnumerable<KeyValuePair<int, object>> values = null, IEnumerable<IEntityBehaviour> behaviours = null, Settings? settings = null)`       | `name` — Entity name.<br>`tags` — Optional initial tags.<br>`values` — Optional initial values.<br>`behaviours` — Optional initial behaviours.<br>`settings` — Optional settings (disposeValues defaults to true).                                  |
| `Entity(string name = null, int tagCapacity = 0, int valueCapacity = 0, int behaviourCapacity = 0, Settings? settings = null)`                                                              | `name` — Entity name.<br>`tagCapacity` — Initial capacity for tags.<br>`valueCapacity` — Initial capacity for values.<br>`behaviourCapacity` — Initial capacity for behaviours.<br>`settings` — Optional settings (disposeValues defaults to true). |

---

## 🏷 Tags

The **Tags** section manages lightweight categorization and filtering of entities.  
Tags are integer-based labels that can be added, removed, enumerated, or checked.  
They are useful for grouping entities, querying, and driving logic based on assigned tags.

### Events

| Event          | Description                      |
|----------------|----------------------------------|
| `OnTagAdded`   | Triggered when a tag is added.   |
| `OnTagDeleted` | Triggered when a tag is removed. |

### Properties

| Property             | Type   | Description                                           |
|----------------------|--------|-------------------------------------------------------|
| `TagCount`           | int    | Number of associated tags.                            |

### Methods

| Method               | Description                             |
|----------------------|-----------------------------------------|
| `HasTag(int)`        | Checks if the entity has the given tag. |
| `AddTag(int)`        | Adds a tag.                             |
| `DelTag(int)`        | Removes a tag.                          |
| `ClearTags()`        | Removes all tags.                       |
| `GetTags()`          | Returns all tag keys.                   |
| `CopyTags(int[])`    | Copies tag keys into an array.          |
| `GetTagEnumerator()` | Enumerates all tags.                    |

---


## 💾 Values

The **Values** section manages dynamic key-value storage for the entity.  
Values can be of any type (structs or reference types) and are identified by integer keys.  
This allows flexible runtime data storage, reactive updates, and modular logic.

Values support reactive updates via associated events (`OnValueAdded`, `OnValueDeleted`, `OnValueChanged`),
allowing other systems to respond automatically to state changes.

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

## ⚙️ Behaviours

The **Behaviours** section manages modular logic attached to the entity.  
Behaviours implement `IEntityBehaviour` interfaces and can be added, removed, queried, or enumerated at runtime.  
This allows flexible composition of entity logic, enabling dynamic functionality without changing the core entity structure.

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

## 🔄 Lifecycle

The **Lifecycle** section manages the entity's state transitions and update phases.  
It covers initialization, enabling, per-frame updates, disabling, and disposal.  
Lifecycle events allow reactive systems to respond to changes in the entity's state.

### Events

| Event                   | Description                                             |
|-------------------------|---------------------------------------------------------|
| `OnInitialize`          | Triggered when the entity is initialized.               |
| `OnEnabled`             | Triggered when the entity is enabled.                   |
| `OnDisabled`            | Triggered when the entity is disabled.                  |
| `OnDisposed`            | Triggered when the entity is disposed.                  |
| `OnUpdated(float)`      | Triggered when the entity is updated.                   |
| `OnFixedUpdated(float)` | Triggered when the entity is fixed updated.             |
| `OnLateUpdated(float)`  | Triggered when the entity is late updated.              |

### Properties

| Property      | Type | Description                        |
|---------------|------|------------------------------------|
| `Initialized` | bool | True if the entity is initialized. |
| `Enabled`     | bool | True if the entity is enabled.     |

### Methods

| Method                           | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
|----------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Init()`                         | Initializes the entity. <ul><li>Transitions the entity to the `Initialized` state.</li><li>Calls `Init` on all behaviours implementing `IEntityInit`.</li><li>Triggers the `OnInitialized` event.</li><li>If the entity is already initialized, does nothing.</li></ul>                                                                                                                                                                                                                                                                                                                           |
| `Enable()`                       | Enables the entity for updates. <ul><li>Transitions the entity to the `Enabled` state.</li><li>Calls `Enable` on all behaviours implementing `IEntityEnable`.</li><li>Triggers the `OnEnabled` event.</li><li>If the entity is not initialized yet, it will be initialized automatically.</li><li>If the entity is already enabled, does nothing.</li></ul>                                                                                                                                                                                                                                       |
| `OnUpdate(float deltaTime)`      | Calls `Update` on all behaviours implementing `IEntityUpdate`. <ul><li>Triggers the `OnUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                                                                                                            |
| `OnFixedUpdate(float deltaTime)` | Calls `FixedUpdate` on all behaviours implementing `IEntityFixedUpdate`. <ul><li>Triggers the `OnFixedUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                                                                                             |
| `OnLateUpdate(float deltaTime)`  | Calls `LateUpdate` on all behaviours implementing `IEntityLateUpdate`. <ul><li>Triggers the `OnLateUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                                                                                                |
| `Disable()`                      | Disables the entity for updates. <ul><li>Transitions the entity to a not `Enabled` state.</li><li>Calls `Disable` on all behaviours implementing `IEntityDisable`.</li><li>Triggers the `OnDisabled` event.</li><li>If the entity is not enabled yet, does nothing.</li></ul>                                                                                                                                                                                                                                                                                                                     |
| `Dispose()`                      | Cleans up all resources used by the entity. <ul><li>Transitions the entity to a not `Initialized` state.</li><li>Calls `Dispose` on all behaviours implementing `IEntityDispose`.</li><li>Clears all tags, values, and behaviours.</li><li>Unsubscribes from all events.</li><li>Unregisters the entity from the `EntityRegistry`.</li><li>Disposes stored values if `Settings.disposeValues` is `true`.</li><li>If the entity is enabled, calls `Disable` automatically.</li><li>If the entity is not initialized yet, does not call `IEntityDispose.Dispose` or trigger `OnDisposed`.</li></ul> |

---

## 🔹 Nested Types

- `Settings` — Entity configuration (e.g., `disposeValues`).
- `BehaviourEnumerator` — Enumerator for behaviours.
- `TagEnumerator` — Enumerator for tags.
- `ValueEnumerator` — Enumerator for values.

## 🐞 Debug Properties

> **Note:** These properties are available only in **Unity Editor** when using **Odin Inspector**.

- `DebugName` — Displays entity name in the Unity Editor.
- `DebugInitialized` — Displays if the entity is initialized.
- `DebugEnabled` — Displays if the entity is enabled.
- `DebugTags` — Sorted list of tags for debug display.
- `DebugValues` — Sorted list of values for debug display.
- `DebugBehaviours` — Sorted list of attached behaviours for debug display.

## 💡 Example Usage

### Example #1: Creating and setting up an entity

```csharp
var entity = new Entity(
    "Character", 
    tagCapacity: 2, //Optionally precompile memory for tags
    valueCapacity: 2, //Optionally precompile memory for values
    behaviourCapacity: 2, //Optionally precompile memory for behaviours
    new Settings { disposeValues = false } //Optionally don't dispose values when Entity.Dispose()
);

// Add tags
entity.AddTag(EntityNames.NameToId("Moveable"));
entity.AddTag("Damageable"); //There is an extension method

// Add values
entity.AddValue(EntityNames.NameToId("Health"), 100);
entity.AddValue("Speed", 5.5f); //There is an extension method

// Add behaviours
entity.AddBehaviour(new MoveBehaviour());
entity.AddBehaviour<HealthBehaviour>(); //There is an extension method
```

### Example #2. Creating an entity through constructor
```csharp
var entity = new Entity(
    name: "Character",
    tags: new[] { "Moveable", "Damageable" },
    values: new[]
    {
        new KeyValuePair<string, object>("Health", 100),
        new KeyValuePair<string, object>("Speed", 5.5f)
    }, 
    behaviours: new IEntityBehaviour[]
    {
        new MoveBehaviour(),
        new HealthBehaviour()
    }
);
```

### Example #3. Lifecycle management

```csharp
// Create a new entity
var entity = new Entity("Enemy");

// Initialize the entity and attached behaviours
entity.Init();

// Enable the entity (registers update behaviours)
entity.Enable();

// Simulate game loop updates
float deltaTime = 0.016f; // Example: 60 FPS

entity.OnUpdate(deltaTime);       // Calls Update on all IEntityUpdate behaviours
entity.OnFixedUpdate(deltaTime);  // Calls FixedUpdate on all IEntityFixedUpdate behaviours
entity.OnLateUpdate(deltaTime);   // Calls LateUpdate on all IEntityLateUpdate behaviours

// Disable the entity (unregisters update behaviours)
entity.Disable();

// Dispose the entity when it is no longer needed
entity.Dispose();
```

## ⏱ Performance
//TODO:

## 📝 Notes

- **Implementation of IEntity** – `Entity` provides a concrete implementation of the `IEntity` interface with full support for tags, values, behaviours, and lifecycle management.
- **Reactive Events** – All core operations (adding/removing tags, values, behaviours, state changes) trigger events, allowing other systems to react automatically.
- **Pre-allocation and Memory Efficiency** – Initial capacities for tags, values, and behaviours can be set in constructors to reduce runtime allocations.
- **Lifecycle Discipline** – Proper use of `Init()`, `Enable()`, `Disable()`, and `Dispose()` ensures predictable entity behaviour and resource cleanup.
- **Thread Safety** – `Entity` is **NOT thread-safe**; all interactions should occur on the main thread or be synchronized externally.
- **Registry Integration** – Entities are automatically registered with the `EntityRegistry` for global tracking and lookup.
- **Debug Support** – When used with Unity Editor and Odin Inspector, debug properties provide quick insight into the entity state, tags, values, and behaviours.
- **Extensibility** – Behaviours, tags, and values can be added dynamically at runtime without modifying the core entity class.
