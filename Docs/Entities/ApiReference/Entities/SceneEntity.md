# 🧩️ SceneEntity

`SceneEntity` is a Unity component implementation of an `IEntity`.  
This class follows the Entity–State–Behaviour pattern, providing a modular container for dynamic state, tags, values,
behaviours, and lifecycle management.  
It allows installation from the Unity Scene and composition through the Inspector or installers.

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
- [Installing](#-installing)
- [Gizmos Support](#-gizmos-support)
- [Optimization](#-optimization)
- [Debug Properties](#-debug-properties)
- [Creation & Destruction](#creation--destruction)
- [Casting & Proxies](#casting--proxies)
- [Install All Entities](#install-all-entities)
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
- **Unity Component** – Attach directly to GameObjects.
- **Scene Installation** – Automatically installs child entities and configured installers.
- **Unity Lifecycle Integration** – Hooks into Awake, Start, OnEnable, OnDisable, and OnDestroy.
- **Gizmos Support** – Conditional drawing in Scene view.
- **Prefab & Factory Support** – Creation, instantiation, and destruction of entities.
- **Casting & Proxies** – Safe conversion between `IEntity` and `SceneEntity`.
- **Scene-Wide Installation** – Can install all SceneEntities in a scene.
- **Odin Inspector Support** – Optional editor enhancements for configuration and debug.

## 🔒 Thread Safety

- `SceneEntity` is **NOT thread-safe**.
- All operations should be performed on the main Unity thread.
- Use external synchronization if accessing from multiple threads.

---

## 🛠️ Inspector Settings

These fields are serialized and configurable from the Unity Inspector.

| Field                      | Type                         | Default | Description                                                                                                                                                                                                   |
|----------------------------|------------------------------|---------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `installOnAwake`           | `bool`                       | `true`  | If enabled, `Install()` is automatically called in `Awake()`.                                                                                                                                                 |
| `installInEditMode`        | `bool`                       | `false` | If enabled, `Install()` is called every time `OnValidate` is invoked in Edit Mode. **Warning:** If you create Unity objects or other heavy objects in `Install()`, turn this off to avoid performance issues. |
| `disposeValues`            | `bool`                       | `true`  | Determines whether values are disposed when `Dispose()` is called.                                                                                                                                            |
| `useUnityLifecycle`        | `bool`                       | `true`  | Enables automatic syncing with Unity MonoBehaviour lifecycle (`Start`, `OnEnable`, `OnDisable`).                                                                                                              |
| `installers`               | `List<SceneEntityInstaller>` | `null`  | List of installers that configure values and systems in this entity.                                                                                                                                          |
| `children`                 | `List<SceneEntity>`          | `null`  | Child entities installed together with this entity.                                                                                                                                                           |
| `initialBehaviourCapacity` | `int`                        | `0`     | Initial capacity for behaviours to optimize memory allocation.                                                                                                                                                |

---

## 🧩 Core State

The **Core State** section represents the fundamental identity and state of the entity.  
It includes unique identifiers, optional names for debugging or tooling, and the main event for reactive state
changes.  
This section provides the minimal information needed to track and observe the entity’s lifecycle.

### Events

| Event            | Description                                             |
|------------------|---------------------------------------------------------|
| `OnStateChanged` | Triggered whenever the entity’s internal state changes. |

### Properties

| Property     | Type   | Description                                                                      |
|--------------|--------|----------------------------------------------------------------------------------|
| `InstanceID` | int    | Runtime-generated unique identifier.                                             |
| `Name`       | string | Optional user-defined name for debugging or tooling. Equals to `GameObject.name` |

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

### Inspector Settings

| Field                | Type  | Default | Description                                              |
|----------------------|-------|---------|----------------------------------------------------------|
| `initialTagCapacity` | `int` | `1`     | Initial capacity for tags to optimize memory allocation. |

### Properties

| Property   | Type | Description                |
|------------|------|----------------------------|
| `TagCount` | int  | Number of associated tags. |

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

### Inspector Settings

| Field                  | Type  | Default | Description                                                |
|------------------------|-------|---------|------------------------------------------------------------|
| `initialValueCapacity` | `int` | `1`     | Initial capacity for values to optimize memory allocation. |

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
This allows flexible composition of entity logic, enabling dynamic functionality without changing the core entity
structure.

Behaviours can respond to lifecycle events (`Init`, `Enable`, `Update`, `Disable`, `Dispose`),
enabling dynamic logic composition without changing the core entity structure.

### Inspector Settings

| Field                      | Type  | Default | Description                                                    |
|----------------------------|-------|---------|----------------------------------------------------------------|
| `initialBehaviourCapacity` | `int` | `0`     | Initial capacity for behaviours to optimize memory allocation. |

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

### Inspector Settings

| Field               | Type   | Default | Description                                                                                      |
|---------------------|--------|---------|--------------------------------------------------------------------------------------------------|
| `useUnityLifecycle` | `bool` | `true`  | Enables automatic syncing with Unity MonoBehaviour lifecycle (`Start`, `OnEnable`, `OnDisable`). |
| `disposeValues`     | `bool` | `true`  | Determines whether values are disposed when `Dispose()` is called.                               |

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

| Method                           | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
|----------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Init()`                         | Initializes the entity. <ul><li>Transitions the entity to the `Initialized` state.</li><li>Calls `Init` on all behaviours implementing `IEntityInit`.</li><li>Triggers the `OnInitialized` event.</li><li>If the entity is already initialized, does nothing.</li></ul>                                                                                                                                                                                                                                                                                                                  |
| `Enable()`                       | Enables the entity for updates. <ul><li>Transitions the entity to the `Enabled` state.</li><li>Calls `Enable` on all behaviours implementing `IEntityEnable`.</li><li>Triggers the `OnEnabled` event.</li><li>If the entity is not initialized yet, it will be initialized automatically.</li><li>If the entity is already enabled, does nothing.</li></ul>                                                                                                                                                                                                                              |
| `OnUpdate(float deltaTime)`      | Calls `Update` on all behaviours implementing `IEntityUpdate`. <ul><li>Triggers the `OnUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                                                                                                   |
| `OnFixedUpdate(float deltaTime)` | Calls `FixedUpdate` on all behaviours implementing `IEntityFixedUpdate`. <ul><li>Triggers the `OnFixedUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                                                                                    |
| `OnLateUpdate(float deltaTime)`  | Calls `LateUpdate` on all behaviours implementing `IEntityLateUpdate`. <ul><li>Triggers the `OnLateUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                                                                                                                                                                                                                                                                                                                                                                                                       |
| `Disable()`                      | Disables the entity for updates. <ul><li>Transitions the entity to a not `Enabled` state.</li><li>Calls `Disable` on all behaviours implementing `IEntityDisable`.</li><li>Triggers the `OnDisabled` event.</li><li>If the entity is not enabled yet, does nothing.</li></ul>                                                                                                                                                                                                                                                                                                            |
| `Dispose()`                      | Cleans up all resources used by the entity. <ul><li>Transitions the entity to a not `Initialized` state.</li><li>Calls `Dispose` on all behaviours implementing `IEntityDispose`.</li><li>Clears all tags, values, and behaviours.</li><li>Unsubscribes from all events.</li><li>Unregisters the entity from the `EntityRegistry`.</li><li>Disposes stored values if `disposeValues` is `true`.</li><li>If the entity is enabled, calls `Disable` automatically.</li><li>If the entity is not initialized yet, does not call `IEntityDispose.Dispose` or trigger `OnDisposed`.</li></ul> |

---

## 🔹 Nested Types
- `BehaviourEnumerator` — Enumerator for behaviours.
- `TagEnumerator` — Enumerator for tags.
- `ValueEnumerator` — Enumerator for values.

---

## 🛠️ Installing

The **Installing** section describes how a `SceneEntity` is populated with **tags**, **values**, and **behaviours** at runtime or in the editor.  
It also manages child entities through installers, ensuring that all dependencies are properly configured and applied.

### Inspector Settings

| Field                      | Type                         | Default | Description                                                                                                                                                                                                   |
|----------------------------|------------------------------|---------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `installOnAwake`           | `bool`                       | `true`  | If enabled, `Install()` is automatically called in `Awake()`.                                                                                                                                                 |
| `installInEditMode`        | `bool`                       | `false` | If enabled, `Install()` is called every time `OnValidate` is invoked in Edit Mode. **Warning:** If you create Unity objects or other heavy objects in `Install()`, turn this off to avoid performance issues. |
| `installers`               | `List<SceneEntityInstaller>` | `null`  | List of installers that configure values and systems in this entity.                                                                                                                                          |
| `children`                 | `List<SceneEntity>`          | `null`  | Child entities installed together with this entity.                                                                                                                                                           |

### Properties

| Property           | Type   | Description                                      |
|--------------------|--------|--------------------------------------------------|
| `bool Installed`   | `bool` | Returns true if the entity has been installed.   |

### Methods

| Method                           | Description                                                                                                                                                                                                                                                                                                                                                 |
|----------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Install()`                      | Installs all configured installers and child entities. <ul><li>Marks the entity as installed.</li><li>Calls `Install` on all `SceneEntityInstaller` instances.</li><li>Installs all child `SceneEntity` instances recursively.</li><li>Does nothing if the entity is already installed.</li></ul>                                                           |
| `MarkAsNotInstalled()`           | Marks the entity as not installed, allowing reinstallation.                                                                                                                                                                                                                                                                                                 |

---

## 🖌️ Gizmos Support

`SceneEntity` provides visual debugging support through Unity Gizmos in the Scene view.

### Inspector Settings

| Field                | Type   | Default | Description                                        |
|----------------------|--------|---------|----------------------------------------------------|
| `onlySelectedGizmos` | `bool` | `false` | Draw gizmos only when this GameObject is selected. |
| `onlyEditModeGizmos` | `bool` | `false` | Draw gizmos only when Unity is not in Play mode.   |

### Methods

| Method                   | Description                                                                                                                                                                             |
|--------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `OnDrawGizmos()`         | Called by Unity to draw gizmos in the scene view. Delegates to `OnDrawGizmosSelected()` unless `_onlySelectedGizmos` is enabled.                                                        |
| `OnDrawGizmosSelected()` | Draws gizmos for this entity and all attached behaviours implementing `IEntityGizmos`. Skips drawing in play mode if `_onlyEditModeGizmos` is enabled. Catches and logs any exceptions. |

----

## ⚡ Optimization

The **Optimization** section provides a simple workflow for precomputing entity capacities in the Unity Editor.

- **Compile Button** – Available in the context menu or as a button in the Inspector.  
  Pressing **Compile** will precompute and store the current sizes of **tags**, **values**, and **behaviours**.

> This feature helps inspect and optimize memory usage without affecting runtime behaviour.

## 🐞 Debug Properties

> **Note:** These properties are available only in **Unity Editor** when using **Odin Inspector**.

- `Initialized` — Displays if the entity is initialized.
- `Enabled` — Displays if the entity is enabled.
- `DebugTags` — Sorted list of tags for debug display.
- `DebugValues` — Sorted list of values for debug display.
- `DebugBehaviours` — Sorted list of attached behaviours for debug display.

---

## Creation & Destruction
Provides static factory methods for creating and destroying entities.

### Methods

| Method                                                                                | Description                                        |
|---------------------------------------------------------------------------------------|----------------------------------------------------|
| `Create(in CreateArgs args)`                                                          | Creates a new entity with configuration.           |
| `Create<E>(...)`                                                                      | Generic creation for type `E : SceneEntity`.       |
| `Create(SceneEntity prefab, Transform parent = null)`                                 | Instantiate prefab at origin.                      |
| `Create<E>(E prefab, Vector3 position, Quaternion rotation, Transform parent = null)` | Instantiate prefab at position/rotation.           |
| `Destroy(IEntity entity, float t = 0)`                                                | Destroys entity's GameObject after optional delay. |

---

### Examples

#### Example #1: Creating an entity with `CreateArgs`

```csharp
var args = new CreateArgs
{
    Name = "Enemy",
    TagCapacity = 2,
    ValueCapacity = 2,
    BehaviourCapacity = 2
};

SceneEntity enemy = SceneEntity.Create(args);
```

#### Example #2: Generic creation for a specific `SceneEntity` type

```csharp
WeaponEntity enemy = SceneEntity.Create<WeaponEntity>(
    new CreateArgs
    {
        Name = "MachineGun",
        TagCapacity = 3,
        ValueCapacity = 5
    }
);

```

#### Example #3: Instantiating a prefab at the origin

```csharp
SceneEntity enemyPrefab = Resources.Load<SceneEntity>("Prefabs/Enemy");
SceneEntity instance = SceneEntity.Create(enemyPrefab);
```

#### Example #4: Instantiating a prefab at a specific position and rotation

```csharp
Vector3 spawnPos = new Vector3(0, 0, 0);
Quaternion rotation = Quaternion.Euler(0, 180, 0);

SceneEntity bossInstance = SceneEntity.Create(enemyPrefab, spawnPos, rotation);
```

#### Example #5: Destroying an entity after a delay

```csharp
SceneEntity.Destroy(bossInstance, 3f); // Destroys entity after 3 seconds
```

## Casting & Proxies
Methods for safe casting between `IEntity` and `SceneEntity`.

### Methods

| Method                                            | Description                                           |
|---------------------------------------------------|-------------------------------------------------------|
| `Cast(IEntity entity)`                            | Casts to `SceneEntity` or throws.                     |
| `Cast<E>(IEntity entity)`                         | Casts to generic `E : SceneEntity`, supports proxies. |
| `TryCast(IEntity entity, out SceneEntity result)` | Attempts cast, returns bool.                          |
| `TryCast<E>(IEntity entity, out E result)`        | Attempts generic cast, supports proxies.              |

---

### Examples

#### Example #1: Simple cast to `SceneEntity`

```csharp
IEntity entity = GetEntityFromRegistry();
SceneEntity sceneEntity = SceneEntity.Cast(entity);
```

> Throws an exception if `entity` is not a `SceneEntity`.

#### Example #2: Generic cast to a specific `SceneEntity` type

```csharp
IEntity entity = GetEntityFromRegistry();
EnemyEntity enemy = SceneEntity.Cast<EnemyEntity>(entity);
```

> Throws an exception if entity is not of type `EnemyEntity` or a proxy of it.

#### Example #3: Safe cast using `TryCast`

```csharp
IEntity entity = GetEntityFromRegistry();
if (SceneEntity.TryCast(entity, out SceneEntity sceneEntity))
    Debug.Log($"Successfully casted to SceneEntity: {sceneEntity.Name}");
else
    Debug.LogWarning("Entity is not a SceneEntity");
```

#### Example #4: Safe generic cast using TryCast<E>

```csharp
IEntity entity = GetEntityFromRegistry();
if (SceneEntity.TryCast<EnemyEntity>(entity, out EnemyEntity enemy))
    Debug.Log($"Successfully casted to EnemyEntity: {enemy.Name}");
else
    Debug.LogWarning("Entity is not of type EnemyEntity");
```

## Install All Entities

Installs all `SceneEntity` instances in a scene that are not yet installed.

### Methods

| Method                       | Description                                                    |
|------------------------------|----------------------------------------------------------------|
| `InstallAll(Scene scene)`    | Installs all `SceneEntity` instances in the scene.             |
| `InstallAll<E>(Scene scene)` | Installs all `SceneEntity` instances of type `E` in the scene. |

---

## 💡 Example Usage
TODO: с картинками

## Performance
TODO: 

## Notes

- `SceneEntity` is Unity-specific (requires `UNITY_5_3_OR_NEWER`)
- Implements `ISerializationCallbackReceiver` for Unity serialization
- Default execution order is `-1000` (runs early)
- `[DisallowMultipleComponent]` prevents multiple entities per `GameObject`
- Supports `Odin Inspector` attributes for enhanced editor experience

