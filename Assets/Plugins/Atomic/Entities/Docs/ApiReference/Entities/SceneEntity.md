# 🧩️ SceneEntity

`SceneEntity` is a Unity component that provides a MonoBehaviour implementation of an `IEntity`.  
This class follows the Entity–State–Behaviour pattern, providing a modular container for dynamic state, tags, values, behaviours, and lifecycle management.  
It allows installation from the Unity Scene and composition through the Inspector or installers.

---

## Key Features
- **Unity Component** – Attach directly to GameObjects.
- **Scene Installation** – Automatically installs child entities and configured installers.
- **Lifecycle Management** – Handles initialization, enabling, disabling, and disposal.
- **Unity Lifecycle Integration** – Hooks into Awake, Start, OnEnable, OnDisable, and OnDestroy.
- **Dynamic Tags & Values** – Supports integer-based tags and key-value storage.
- **Behaviour Composition** – Modular behaviors implementing IEntity interfaces.
- **Per-Frame Updates** – Update, FixedUpdate, LateUpdate support for behaviours.
- **Gizmos Support** – Conditional drawing in Scene view.
- **Prefab & Factory Support** – Creation, instantiation, and destruction of entities.
- **Casting & Proxies** – Safe conversion between `IEntity` and `SceneEntity`.
- **Scene-Wide Installation** – Can install all SceneEntities in a scene.
- **Odin Inspector Support** – Optional editor enhancements for configuration and debug.

---

## Thread Safety
- `SceneEntity` is **NOT thread-safe**.
- All operations should be performed on the main Unity thread.
- Use external synchronization if accessing from multiple threads.

---

## Contents
- [Inspector Settings](#inspector-settings)
- [Lifecycle](#lifecycle)
- [Update Events](#update-events)
- [Tags](#tags)
- [Values](#values)
- [Creation & Destruction](#creation--destruction)
- [Casting & Proxies](#casting--proxies)
- [Install All Entities](#install-all-entities)
- [Debug Properties](#debug-properties)
- [Gizmos Support](#gizmos-support)
---
## Inspector Settings

These fields are serialized and configurable from the Unity Inspector.

| Field                       | Type                         | Default | Description                                                                                                                                                                                                   |
|-----------------------------|------------------------------|---------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `installOnAwake`            | `bool`                       | `true`  | If enabled, `Install()` is automatically called in `Awake()`.                                                                                                                                                 |
| `installInEditMode`         | `bool`                       | `false` | If enabled, `Install()` is called every time `OnValidate` is invoked in Edit Mode. **Warning:** If you create Unity objects or other heavy objects in `Install()`, turn this off to avoid performance issues. |
| `disposeValues`             | `bool`                       | `true`  | Determines whether values are disposed when `Dispose()` is called.                                                                                                                                            |
| `useUnityLifecycle`         | `bool`                       | `true`  | Enables automatic syncing with Unity MonoBehaviour lifecycle (`Start`, `OnEnable`, `OnDisable`).                                                                                                              |
| `installers`                | `List<SceneEntityInstaller>` | `null`  | List of installers that configure values and systems in this entity.                                                                                                                                          |
| `children`                  | `List<SceneEntity>`          | `null`  | Child entities installed together with this entity.                                                                                                                                                           |
| `_initialTagCapacity`       | `int`                        | `1`     | Initial capacity for tags to optimize memory allocation.                                                                                                                                                      |
| `_initialValueCapacity`     | `int`                        | `1`     | Initial capacity for values to optimize memory allocation.                                                                                                                                                    |
| `_initialBehaviourCapacity` | `int`                        | `0`     | Initial capacity for behaviours to optimize memory allocation.                                                                                                                                                |

---

## Lifecycle

Manages initialization, enabling, disabling, and disposal of entities.

### Properties

| Property           | Type   | Description                                      |
|--------------------|--------|--------------------------------------------------|
| `bool Initialized` | `bool` | Returns true if the entity has been initialized. |
| `bool Enabled`     | `bool` | Returns true if the entity is currently enabled. |
| `bool Installed`   | `bool` | Returns true if the entity has been installed.   |

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
| `Install()`                      | Installs all configured installers and child entities. <ul><li>Marks the entity as installed.</li><li>Calls `Install` on all `SceneEntityInstaller` instances.</li><li>Installs all child `SceneEntity` instances recursively.</li><li>Does nothing if the entity is already installed.</li></ul>                                                                                                                                                                                                                                                                                        |
| `MarkAsNotInstalled()`           | Marks the entity as not installed, allowing reinstallation.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |

### Events

| Event            | Description                            |
|------------------|----------------------------------------|
| `OnInitialized`  | Called when the entity is initialized. |
| `OnDisposed`     | Called when the entity is disposed.    |
| `OnEnabled`      | Called when the entity is enabled.     |
| `OnDisabled`     | Called when the entity is disabled.    |
| `OnStateChanged` | Called whenever entity state changes.  |

---

## Update Events

Provides per-frame updates to attached behaviours.

### Events

| Event            | Parameter         | Description                                                 |
|------------------|-------------------|-------------------------------------------------------------|
| `OnUpdated`      | `float deltaTime` | Called every frame while entity is enabled.                 |
| `OnFixedUpdated` | `float deltaTime` | Called every fixed frame (physics) while entity is enabled. |
| `OnLateUpdated`  | `float deltaTime` | Called every late frame while entity is enabled.            |

### Methods
Provides per-frame updates to attached behaviours. These methods should be invoked only when the entity is enabled.

| Method                           | Parameter   | Description                                                                                                                                                              |
|----------------------------------|-------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `OnUpdate(float deltaTime)`      | `deltaTime` | Calls `Update` on all `IEntityUpdate` behaviours. <ul><li>Triggers the `OnUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>                |
| `OnFixedUpdate(float deltaTime)` | `deltaTime` | Calls `FixedUpdate` on all `IEntityFixedUpdate` behaviours. <ul><li>Triggers the `OnFixedUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul> |
| `OnLateUpdate(float deltaTime)`  | `deltaTime` | Calls `LateUpdate` on all `IEntityLateUpdate` behaviours. <ul><li>Triggers the `OnLateUpdated` event.</li><li>Can only be invoked if the entity is enabled.</li></ul>    |

---

## Tags

Manages integer-based tags for entities.

### Properties

| Property   | Type  | Description                                |
|------------|-------|--------------------------------------------|
| `TagCount` | `int` | Number of tags associated with the entity. |

### Methods

| Method            | Description                                            |
|-------------------|--------------------------------------------------------|
| `HasTag(int key)` | Returns true if the entity contains the tag.           |
| `AddTag(int key)` | Adds a tag and triggers `OnTagAdded`.                  |
| `DelTag(int key)` | Removes a tag and triggers `OnTagDeleted`.             |
| `GetTags()`       | Returns all tag keys as an array.                      |
| `ClearTags()`     | Removes all tags and triggers `OnTagDeleted` for each. |

### Events

| Event          | Description                    |
|----------------|--------------------------------|
| `OnTagAdded`   | Invoked when a tag is added.   |
| `OnTagDeleted` | Invoked when a tag is removed. |

---

## Values

Stores key-value pairs associated with entities.

### Properties

| Property     | Type  | Description              |
|--------------|-------|--------------------------|
| `ValueCount` | `int` | Number of stored values. |

### Methods

| Method                                   | Description                        |
|------------------------------------------|------------------------------------|
| `AddValue<T>(int key, T value)`          | Adds a struct value.               |
| `AddValue(int key, object value)`        | Adds a reference type value.       |
| `SetValue<T>(int key, T value)`          | Sets or updates a struct value.    |
| `SetValue(int key, object value)`        | Sets or updates a reference value. |
| `GetValue<T>(int key)`                   | Gets value cast to type `T`.       |
| `GetValue(int key)`                      | Gets value as `object`.            |
| `TryGetValue<T>(int key, out T value)`   | Tries to get value of type `T`.    |
| `TryGetValue(int key, out object value)` | Tries to get value as `object`.    |
| `DelValue(int key)`                      | Deletes a value.                   |
| `ClearValues()`                          | Removes all values.                |
| `GetValues()`                            | Returns all key-value pairs.       |

### Events

| Event            | Description                          |
|------------------|--------------------------------------|
| `OnValueAdded`   | Triggered when a new value is added. |
| `OnValueDeleted` | Triggered when a value is removed.   |
| `OnValueChanged` | Triggered when a value is updated.   |

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

## Debug Properties

> **Note:** These properties are available only in **Unity Editor** when using **Odin Inspector**.

Provides inspector-only debug UI for the `SceneEntity`, including read-only state and editable lists for tags, values, and behaviours.

### Tags

- **Description:** Shows a sorted list of tags currently assigned to the entity.
- **Type:** `List<TagElement>`
- **TagElement Fields:**
    - `name` — Display name of the tag.
    - `id` — Internal ID of the tag.
- **Features:**
    - Supports searching and sorting.
    - Allows removing tags directly from the inspector.
- **Inspector Settings:** Read-only list, no add button, custom remove functions.

### Values

- **Description:** Shows a sorted list of values currently stored in the entity.
- **Type:** `List<ValueElement>`
- **ValueElement Fields:**
    - `name` — Name of the value.
    - `value` — Value object.
    - `id` — Internal key.
- **Features:**
    - Supports searching and sorting.
    - Allows removing values directly from the inspector.
- **Inspector Settings:** Read-only list, no add button, custom remove functions.

### Behaviours

- **Description:** Shows a sorted list of behaviours currently attached to the entity.
- **Type:** `List<BehaviourElement>`
- **BehaviourElement Fields:**
    - `name` — Behaviour type name.
    - `value` — Reference to the `IEntityBehaviour` instance.
- **Features:**
    - Supports searching and sorting.
    - Allows removing behaviours directly from the inspector.
- **Inspector Settings:** Read-only list, no add button, custom remove functions.

---

## Gizmos Support

`SceneEntity` provides visual debugging support through Unity Gizmos in the Scene view.

### Fields

| Field                 | Type   | Default | Description                                        |
|-----------------------|--------|---------|----------------------------------------------------|
| `_onlySelectedGizmos` | `bool` | `false` | Draw gizmos only when this GameObject is selected. |
| `_onlyEditModeGizmos` | `bool` | `false` | Draw gizmos only when Unity is not in Play mode.   |

### Methods

| Method                   | Description                                                                                                                                                                             |
|--------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `OnDrawGizmos()`         | Called by Unity to draw gizmos in the scene view. Delegates to `OnDrawGizmosSelected()` unless `_onlySelectedGizmos` is enabled.                                                        |
| `OnDrawGizmosSelected()` | Draws gizmos for this entity and all attached behaviours implementing `IEntityGizmos`. Skips drawing in play mode if `_onlyEditModeGizmos` is enabled. Catches and logs any exceptions. |