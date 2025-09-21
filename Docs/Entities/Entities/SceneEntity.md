# 🧩️ SceneEntity

Represents a Unity component implementation of an [IEntity](IEntity.md). This class follows the **Entity–State–Behaviour
** pattern, providing a modular container for dynamic state, tags, values,
behaviours, and lifecycle management. It allows installation from the Unity Scene and composition through the Inspector
or installers.

```csharp
public class SceneEntity : MonoBehaviour, IEntity, ISerializationCallbackReceiver
```

---

## 📚 Content

- [Core](#-core-state)
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

---

## 💠 Core Members

Represent the fundamental identity and state of the entity. It includes unique identifiers, optional names for debugging
or tooling, and the main event for reactive state
changes. This section provides the minimal information needed to track and observe the entity’s lifecycle.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action<IEntity> OnStateChanged
```

- **Description:** Triggered whenever the entity’s internal state changes.
- **Parameter:** `IEntity` – This entity.
- **Note:** Useful for reacting to lifecycle or state transitions of an entity.

---

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

### 🛠 Inspector Settings

| Parameter            | Description                                                             |
|----------------------|-------------------------------------------------------------------------|
| `initialTagCapacity` | Initial capacity for tags to optimize memory allocation. Default is `1` |

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
- **Throws:** `ArgumentNullException` if `results` is null

#### `GetTagEnumerator`

```csharp
public TagEnumerator GetTagEnumerator()
```

- **Description:** Enumerates all tags of the entity.
- **Returns:** `TagEnumerator` – Struct enumerator over tag keys.

---

### 🗂 Example of Usage

This example demonstrates how to use tags with `SceneEntity`, including adding, removing, and checking tags. Three
approaches are shown: using **numeric keys** for performance, **string names** for readability and **code generation**
for real projects. Subscriptions to `OnTagAdded` and `OnTagDeleted` events are included to react to changes in real
time.

---

#### 1️⃣ Using Numeric Keys

By default, all tags use `int` keys because this avoids computing hash codes and is very fast; therefore, the example
below uses numeric keys as the default approach.

```csharp
// Assume we have instance of entity
SceneEntity entity = ...

// Subscribe to tag events
entity.OnTagAdded += (e, tagId) => 
    Console.WriteLine($"Tag added: {tagId}");
entity.OnTagDeleted += (e, tagId) => 
    Console.WriteLine($"Tag removed: {tagId}");

// Add tags by numeric ID
entity.AddTag(1);         // Player tag = 1
entity.AddTag(2);         // NPC tag = 2

// Check tags
if (entity.HasTag(1)) //Check if  Player tag exists
    Console.WriteLine("Entity has tag ID 1 (Player)");

// Remove a NPC tag
entity.DelTag(2);

// Add multiple tags
entity.AddTags(new int[] { 3, 4 }); // Ally, Merchant

// Enumerate all tags
foreach (int id in entity.GetTags())
    Console.WriteLine($"Entity tag ID: {id}");
```

---

#### 2️⃣ Using String Names

In this example, for convenience, there are [extension methods](Extensions.md#-tags) for the entity. This format is more
user-friendly but slightly slower than using numeric keys.

```csharp
// Assume we have instance of entity
SceneEntity entity = ...

// Add tags by string name
entity.AddTag("Player");
entity.AddTag("NPC");

// Check tags
if (entity.HasTag("Player"))
    Console.WriteLine("Entity is a Player");

// Remove a tag
entity.DelTag("NPC");

// Add multiple tags at once
entity.AddTags(new string[] { "Ally", "Merchant" });

// Enumerate all tags (numeric IDs)
foreach (int id in entity.GetTags())
    Console.WriteLine($"Entity tag ID: {id}");
```

---

#### 3️⃣ Using Code Generation

Sometimes managing tags by raw `int` keys or `string` names can get messy and error-prone, especially in big projects.
To
make this process easier and **type-safe**, the Atomic Framework supports **code generation**. This means you describe
all your tags (and values) once in a small config file, and the framework will automatically generate C# helpers. You
can learn more about this in the Manual under
the [Entity API Generation](../Manual.md/#-generate-entity-api) section.

```csharp
// Assume we have instance of entity
SceneEntity entity = ...

// Add tags
entity.AddPlayerTag();
entity.AddNPCTag();

// Check tag
if (entity.HasPlayerTag())
    Console.WriteLine("Entity is a Player");

// Remove a tag
entity.DelNPCTag();
```

---

## 💠 Value Members

Manages dynamic key-value storage for the entity. Values can be of any type (structs or reference types) and are
identified by integer keys. This allows flexible runtime data storage, reactive updates, and modular logic. Values
support reactive updates via associated events (`OnValueAdded`, `OnValueDeleted`, `OnValueChanged`),
allowing other systems to respond automatically to state changes.

---

### 🛠 Inspector Settings

| Parameters             | Description                                                               |
|------------------------|---------------------------------------------------------------------------|
| `initialValueCapacity` | Initial capacity for values to optimize memory allocation. Default is `1` |

---

### ⚡ Events

#### `OnValueAdded`

```csharp
public event Action<IEntity, int> OnValueAdded  
```

- **Description:** Triggered when a value is added.
- **Parameters:**
    - `IEntity` – The entity where the value was added.
    - `int` – The key of the value that was added.
- **Note:** Allows subscribers to react whenever a new key-value pair is inserted.

#### `OnValueDeleted`

```csharp
public event Action<IEntity, int> OnValueDeleted  
```

- **Description:** Triggered when a value is deleted.
- **Parameters:**
    - `IEntity` – The entity where the value was deleted.
    - `int` – The key of the value that was removed.
- **Note:** Useful for cleanup or reactive updates when values are removed.

#### `OnValueChanged`

```csharp
public event Action<IEntity, int> OnValueChanged  
```

- **Description:** Triggered when a value is changed.
- **Parameters:**
    - `IEntity` – The entity where the value was changed.
    - `int` – The key of the value that was updated.
- **Note:** Enables reactive programming patterns when values are updated.

---

### 🔑 Properties

#### `ValueCount`

```csharp
public int ValueCount { get; }  
```

- **Description:** Number of stored values in the entity.
- **Note:** Provides a quick way to check how many key-value pairs are currently stored.

---

### 🏹 Methods

#### `GetValue<T>(int)`

```csharp
public T GetValue<T>(int key)  
```

- **Description:** Retrieves a value by key and casts it to the specified type.
- **Parameters:** `key` – The key of the value to retrieve.
- **Returns:** `T` – The value associated with the key.
- **Exceptions:** Throws if the key does not exist or cannot be cast.

#### `GetValueUnsafe<T>(int)`

```csharp
public ref T GetValueUnsafe<T>(int key)  
```

- **Description:** Retrieves a value by key as a reference (unsafe, no boxing).
- **Parameters:** `key` – The key of the value to retrieve.
- **Returns:** `ref T` – Reference to the stored value.
- **Exceptions:** Throws if the key does not exist or cannot be cast.

#### `GetValue(int)`

```csharp
public object GetValue(int key)  
```

- **Description:** Retrieves a value by key as an `object`.
- **Parameters:** `key` – The key of the value to retrieve.
- **Returns:** `object` – The value stored at the key.
- **Exceptions:** Throws if the key does not exist.

#### `TryGetValue<T>(int, out T)`

```csharp
public bool TryGetValue<T>(int key, out T value)  
```

- **Description:** Tries to retrieve a typed value by key.
- **Parameters:**
    - `key` – The key of the value to retrieve.
    - `out value` – Output parameter for the retrieved value.
- **Returns:** `true` if the value exists and is of type `T`, otherwise `false`.

#### `TryGetValueUnsafe<T>(int, out T)`

```csharp
public bool TryGetValueUnsafe<T>(int key, out T value)  
```

- **Description:** Tries to retrieve a value by reference (unsafe).
- **Parameters:**
    - `key` – The key of the value.
    - `out value` – Output reference to the value.
- **Returns:** `true` if the value exists and is of type `T`, otherwise `false`.

#### `TryGetValue(int, out object)`

```csharp
public bool TryGetValue(int key, out object value)  
```

- **Description:** Tries to retrieve a value as `object`.
- **Parameters:**
    - `key` – The key of the value.
    - `out value` – Output parameter for the value.
- **Returns:** `true` if the key exists, otherwise `false`.

#### `SetValue<T>(int, T)`

```csharp
public void SetValue<T>(int key, T value) where T : struct  
```

- **Description:** Sets or updates a struct value.
- **Parameters:**
    - `key` – The key to set.
    - `value` – The value to store.
- **Triggers:**
    - `OnValueAdded` if the key did not exist.
    - `OnValueChanged` if the key already existed.
    - `OnStateChanged` in both cases.
- **Exceptions:** Throws if key is invalid.

#### `SetValue(int, object)`

```csharp
public void SetValue(int key, object value)  
```

- **Description:** Sets or updates a reference value.
- **Parameters:**
    - `key` – The key to set.
    - `value` – The value to store.
- **Triggers:**
    - `OnValueAdded` if the key did not exist.
    - `OnValueChanged` if the key already existed.
    - `OnStateChanged` in both cases.
- **Exceptions:** Throws if key is invalid or value is null.

#### `HasValue(int)`

```csharp
public bool HasValue(int key)  
```

- **Description:** Checks if a value exists for the given key.
- **Parameters:** `key` – The key to check.
- **Returns:** `true` if the key exists, otherwise `false`.
- **Triggers:** None.
- **Exceptions:** None.

#### `AddValue<T>(int, T)`

```csharp
public void AddValue<T>(int key, T value) where T : struct  
```

- **Description:** Adds a struct value.
- **Parameters:**
    - `key` – The key to add.
    - `value` – The value to add.
- **Triggers:** `OnValueAdded` and `OnStateChanged`.
- **Exceptions:** Throws if key already exists.

#### `AddValue(int, object)`

```csharp
public void AddValue(int key, object value)  
```

- **Description:** Adds a reference value.
- **Parameters:**
    - `key` – The key to add.
    - `value` – The value to add.
- **Triggers:** `OnValueAdded` and `OnStateChanged`.
- **Exceptions:** Throws if key already exists or value is null.

#### `DelValue(int)`

```csharp
public bool DelValue(int key)  
```

- **Description:** Deletes a value by key.
- **Parameters:** `key` – The key to delete.
- **Returns:** `true` if the value existed and was removed, otherwise `false`.
- **Triggers:** `OnValueDeleted` and `OnStateChanged` if the value existed.

#### `ClearValues()`

```csharp
public void ClearValues()  
```

- **Description:** Clears all values from the entity.
- **Triggers:** `OnValueDeleted` for each key removed and `OnStateChanged`.

#### `GetValues()`

```csharp
public KeyValuePair<int, object>[] GetValues()  
```

- **Description:** Returns all key-value pairs currently stored.
- **Returns:** Array of `KeyValuePair<int, object>`.

#### `CopyValues(KeyValuePair<int, object>[])`

```csharp
public int CopyValues(KeyValuePair<int, object>[] results)  
```

- **Description:** Copies all key-value pairs into the provided array.
- **Parameters:** `results` – Array to copy key-value pairs into.
- **Returns:** Number of values copied.
- **Exceptions:** Throws if `results` is null or too small.

#### `GetValueEnumerator()`

```csharp
public ValueEnumerator GetValueEnumerator()  
```

- **Description:** Enumerates all key-value pairs.
- **Returns:** Struct enumerator for iterating through stored values.

---

### 🗂 Example of Usage

This example demonstrates how to use **values** with `SceneEntity`, including adding, retrieving, updating, and removing
values. Three approaches are shown: using **numeric keys** for performance, **string names** for readability, and **code
generation** for real projects. Subscriptions to `OnValueChanged` events are included to react to changes in real time.

---

#### 1️⃣ Using Numeric Keys

By default, all values use `int` keys because this avoids computing hash codes and is very fast; therefore, the example
below uses numeric keys as the default approach.

```csharp
// Assume we have instance of entity
SceneEntity entity = ...

// Subscribe to value events
entity.OnValueChanged += (e, key) => Console.WriteLine($"Value {key} changed");

//Add health property
entity.AddValue(1, 100); //Health = 1

//Add speed property
entity.AddValue(2, 12.5f); //Speed = 2

//Add inventory property
entity.AddValue(3, new Inventory()); //Inventory = 3

// Get a value
int health = entity.GetValue<int>(1);
Console.WriteLine($"Health: {health}");

// Update a Health
entity.SetValue(1, 150);

// Remove a Speed value
entity.DelValue(2);
```

---

#### 2️⃣ Using String Names

In this example, for convenience, there are [extension methods](Extensions.md#-values) for the entity. This format is
more user-friendly but slightly slower than using numeric keys.

```csharp
// Assume we have instance of entity
SceneEntity entity = ...
    
// Add values by string key
entity.AddValue("Health", 100);
entity.AddValue("Speed", 12.5f);
entity.AddValue("Inventory", new Inventory());

// Get a value
int health = entity.GetValue<int>("Health");
Console.WriteLine($"Health: {health}");

// Update a value
entity.SetValue("Health", 150);

// Remove a value
entity.DelValue("Inventory");
```

---

#### 3️⃣ Using Code Generation

Managing values by raw `int` keys or `string` names can be error-prone, especially in larger projects. To make the
process easier and **type-safe**, the Atomic Framework supports **code generation**. You describe all your tags and
values once in a small config file, and the framework automatically generates
strongly-typed C# helpers. More details are in the Manual under
the [Entity API Generation](../Manual.md/#-generate-entity-api) section.

```csharp
// Assume we have instance of entity
SceneEntity entity = ...

// Add values
entity.AddHealth(100);
entity.AddSpeed(12.5f);
entity.AddInventory(new GridInventory());

// Get a value
int health = entity.GetHealth();
Console.WriteLine($"Health: {health}");

// Update a value
entity.SetHealth(150);

// Remove a value
entity.DelInventory();
```

---

## 💠 Behaviours

Manage modular logic attached to the entity. Behaviours implement [IEntityBehaviour](../Behaviours/IEntityBehaviour.md)
interfaces and can be added,
removed, queried, or enumerated at runtime. This allows flexible composition of entity logic, enabling dynamic
functionality without changing the core entity
structure. Behaviours can respond to lifecycle events (`Init`, `Enable`, `Tick`, `Disable`, `Dispose`),
enabling dynamic logic composition without changing the core entity structure.

---

### 🛠 Inspector Settings

| Field                      | Type                                                                          | 
|----------------------------|-------------------------------------------------------------------------------|
| `initialBehaviourCapacity` | Initial capacity for behaviours to optimize memory allocation. Default is `0` |

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

The **Installing** section describes how a `SceneEntity` is populated with **tags**, **values**, and **behaviours** at
runtime or in the editor.  
It also manages child entities through installers, ensuring that all dependencies are properly configured and applied.

<!--
//TODO: FORMAT TO INSTALL ACTION!
> [!NOTE]  
> Actions are executed in the order they appear in the array.  
> Null references are automatically skipped, making partially configured lists safe to use.
-->

### Inspector Settings

| Field               | Type                         | Default | Description                                                                                                                                                                                                   |
|---------------------|------------------------------|---------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `installOnAwake`    | `bool`                       | `true`  | If enabled, `Install()` is automatically called in `Awake()`.                                                                                                                                                 |
| `installInEditMode` | `bool`                       | `false` | If enabled, `Install()` is called every time `OnValidate` is invoked in Edit Mode. **Warning:** If you create Unity objects or other heavy objects in `Install()`, turn this off to avoid performance issues. |
| `installers`        | `List<SceneEntityInstaller>` | `null`  | List of installers that configure values and systems in this entity.                                                                                                                                          |
| `children`          | `List<SceneEntity>`          | `null`  | Child entities installed together with this entity.                                                                                                                                                           |

### Properties

| Property         | Type   | Description                                    |
|------------------|--------|------------------------------------------------|
| `bool Installed` | `bool` | Returns true if the entity has been installed. |

### Methods

| Method                 | Description                                                                                                                                                                                                                                                                                       |
|------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Install()`            | Installs all configured installers and child entities. <ul><li>Marks the entity as installed.</li><li>Calls `Install` on all `SceneEntityInstaller` instances.</li><li>Installs all child `SceneEntity` instances recursively.</li><li>Does nothing if the entity is already installed.</li></ul> |
| `MarkAsNotInstalled()` | Marks the entity as not installed, allowing reinstallation.                                                                                                                                                                                                                                       |

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
