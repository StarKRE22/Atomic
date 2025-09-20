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
- **Throws:** `ArgumentNullException` if `results` is null

#### `GetTagEnumerator`

```csharp
public IEnumerator<int> GetTagEnumerator()
```

- **Description:** Enumerates all tags of the entity.
- **Returns:** `IEnumerator<int>` – Enumerator over tag keys.

---

### 🗂 Example of Usage

This example demonstrates how to use tags with `IEntity`, including adding, removing, and checking tags. Two approaches
are shown: using numeric keys for performance and string names for readability. Subscriptions to `OnTagAdded` and
`OnTagDeleted` events are included to react to changes in real time.


---

#### 1️⃣ Using Numeric Keys

By default, all tags use `int` keys because this avoids computing hash codes and is very fast; therefore, the example
below uses numeric keys as the default approach.

```csharp
// Create a new entity
IEntity entity = new Entity();

// Subscribe to tag events
entity.OnTagAdded += (e, tagId) => 
    Console.WriteLine($"Tag added: {tagId}");
entity.OnTagDeleted += (e, tagId) => 
    Console.WriteLine($"Tag removed: {tagId}");

// Add tags by numeric ID
entity.AddTag(1);         // Player
entity.AddTag(2);         // NPC

// Check tags
if (entity.HasTag(1))
    Console.WriteLine("Entity has tag ID 1 (Player)");

// Remove a tag
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
// Create a new entity
IEntity entity = new Entity();

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

**Step 1:** Create a `.yaml` file where you list all your tags and values:

```yaml
header: EntityAPI
entityType: IEntity
aggressiveInlining: true
namespace: PROJECT_NAMESPACE
className: EntityAPI
directory: CODE_GENERATION_PATH

imports:
  - UnityEngine
  - Atomic.Entities
  - Atomic.Elements

tags:
  - Player
  - NPC

values:
```

- `namespace` — the namespace of the generated code
- `tags` — list of tags that will be turned into constants
- `values` — same for values (empty in this example)

---

**Step 2:** Based on this config, the framework creates a **static API class**:

```csharp
/**
 * Code generation. Don't modify! 
 **/

public static class EntityAPI
{

    ///Tags
    public static readonly int Player;
    public static readonly int NPC;

    ///Values

    static GameEntityAPI()
    {
        //Tags
        Player = NameToId(nameof(Player));
        NPC = NameToId(nameof(NPC));

        //Values
    }


    ///Tag Extensions

    #region Player
    public static bool HasPlayerTag(this IGameEntity entity) => entity.HasTag(Player);
    public static bool AddPlayerTag(this IGameEntity entity) => entity.AddTag(Player);
    public static bool DelPlayerTag(this IGameEntity entity) => entity.DelTag(Player);
    #endregion
    
    #region NPC
    public static bool HasNPCTag(this IGameEntity entity) => entity.HasTag(NPC);
    public static bool AddNPCTag(this IGameEntity entity) => entity.AddTag(NPC);
    public static bool DelNPCTag(this IGameEntity entity) => entity.DelTag(NPC);
    #endregion
}
```

**Step 3:** Now you get ready-to-use methods for each tag: `AddPlayerTag()`, `HasPlayerTag()`, `DelPlayerTag()`, etc. No more “magic
strings” or manual ID lookups.

```csharp
// Create a new entity
IEntity entity = new Entity();

// Add tags by string name
entity.AddPlayerTag();
entity.AddNPCTag(); // Get numeric ID

// Check tags
if (entity.HasPlayerTag())
    Console.WriteLine("Entity is a Player");

// Remove a tag
entity.DelNPCTag();
```

---

## 💠 Value Members

Manage dynamic key-value storage for the entity. Values can be of any type (structs or reference types) and are
identified by integer keys. This allows flexible runtime data storage, reactive updates, and modular logic.

---

### ⚡ Events

#### `OnValueAdded`

```csharp
public event Action<IEntity, int> OnValueAdded  
```

-
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
public IEnumerator<KeyValuePair<int, object>> GetValueEnumerator()  
```

- **Description:** Enumerates all key-value pairs.
- **Returns:** Enumerator for iterating through stored values.

---

## 💠️ Behaviour Members

Manage modular logic attached to the entity. Behaviours implement `IEntityBehaviour` interfaces and can be added,
removed, queried, or enumerated at runtime. This allows flexible composition of entity logic, enabling dynamic
functionality without changing the core entity
structure. Behaviours can respond to lifecycle events (`Init`, `Enable`, `Tick`, `Disable`, `Dispose`),
enabling dynamic logic composition without changing the core entity structure.

---

### ⚡ Events

#### `OnBehaviourAdded`

```csharp
public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded  
```

- **Description:** Triggered when a behaviour is added to the entity.
- **Parameters:**
    - `IEntity` – The entity where the behaviour was added.
    - `IEntityBehaviour` – The behaviour that was added.
- **Note:** Allows subscribers to react whenever a new behaviour is attached.

#### `OnBehaviourDeleted`

```csharp
public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted  
```

- **Description:** Triggered when a behaviour is removed from the entity.
- **Parameters:**
    - `IEntity` – The entity where the behaviour was removed.
    - `IEntityBehaviour` – The behaviour that was removed.
- **Note:** Useful for cleanup or reactive updates when behaviours are detached.

---

### 🔑 Properties

#### `BehaviourCount`

```csharp
public int BehaviourCount { get; }  
```

- **Description:** Number of behaviours currently attached to the entity.
- **Note:** Provides a quick way to check how many behaviours are associated with this entity.

---

### 🏹 Methods

#### `AddBehaviour(IEntityBehaviour)`

```csharp
public void AddBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Adds a behaviour to the entity.
- **Parameters:** `behaviour` – The behaviour instance to attach.
- **Triggers:** `OnBehaviourAdded` and `OnStateChanged`.
- **Exceptions:** Throws if `behaviour` is null.

#### `GetBehaviour<T>()`

```csharp
public T GetBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Gets the first behaviour of the specified type.
- **Returns:** The first attached behaviour of type `T`.
- **Exceptions:** Throws if no behaviour of type `T` exists.

#### `GetBehaviourAt(int)`

```csharp
public IEntityBehaviour GetBehaviour(int index)  
```

- **Description:** Returns the behaviour instance at the given index.
- **Parameters:** `index` – The zero-based index of the behaviour.
- **Returns:** The behaviour at the specified index.
- **Exceptions:** Throws if `index` is out of range.

#### `TryGetBehaviour<T>(out T)`

```csharp
public bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour  
```

- **Description:** Tries to get a behaviour of the specified type.
- **Parameters:** `out behaviour` – Output parameter for the behaviour.
- **Returns:** `true` if a behaviour of type `T` exists, otherwise `false`.

#### `HasBehaviour(IEntityBehaviour)`

```csharp
public bool HasBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Checks if a specific behaviour exists.
- **Parameters:** `behaviour` – The behaviour instance to check.
- **Returns:** `true` if the behaviour is attached, otherwise `false`.

#### `HasBehaviour<T>()`

```csharp
public bool HasBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Checks if a behaviour of the specified type exists.
- **Returns:** `true` if any behaviour of type `T` is attached, otherwise `false`.

#### `DelBehaviour(IEntityBehaviour)`

```csharp
public bool DelBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Removes a specific behaviour.
- **Parameters:** `behaviour` – The behaviour to remove.
- **Returns:** `true` if the behaviour existed and was removed, otherwise `false`.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged`.

#### `DelBehaviour<T>()`

```csharp
public bool DelBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Removes a behaviour of the specified type.
- **Returns:** `true` if a behaviour of type `T` was removed, otherwise `false`.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged`.

#### `DelBehaviours<T>()`

```csharp
public void DelBehaviours<T>() where T : IEntityBehaviour  
```

- **Description:** Removes all behaviours of the specified type.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged` for each removed behaviour.

#### `ClearBehaviours()`

```csharp
public void ClearBehaviours()  
```

- **Description:** Clears all behaviours from the entity.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged` for each removed behaviour.

#### `GetBehaviours()`

```csharp
public IEntityBehaviour[] GetBehaviours()  
```

- **Description:** Returns all behaviours attached to the entity.
- **Returns:** Array of all behaviours.

#### `GetBehaviours<T>()`

```csharp
public T[] GetBehaviours<T>() where T : IEntityBehaviour  
```

- **Description:** Returns all behaviours of type `T` attached to the entity.
- **Returns:** Array of behaviours of type `T`.

#### `CopyBehaviours(IEntityBehaviour[])`

```csharp
public int CopyBehaviours(IEntityBehaviour[] results)  
```

- **Description:** Copies all behaviours into the provided array.
- **Parameters:** `results` – Array to copy behaviours into.
- **Returns:** Number of behaviours copied.
- **Exceptions:** Throws if `results` is null or too small.

#### `CopyBehaviours<T>(T[])`

```csharp
public int CopyBehaviours<T>(T[] results) where T : IEntityBehaviour  
```

- **Description:** Copies behaviours of type `T` into the provided array.
- **Parameters:** `results` – Array to copy behaviours into.
- **Returns:** Number of behaviours copied.
- **Exceptions:** Throws if `results` is null or too small.

#### `GetBehaviourEnumerator()`

```csharp
public IEnumerator<IEntityBehaviour> GetBehaviourEnumerator()  
```

- **Description:** Enumerates all behaviours attached to the entity.
- **Returns:** Enumerator for iterating through behaviours.

----

## 💠 Lifecycle Members

Manage the entity's state transitions and update phases. It covers initialization, enabling,
per-frame updates, disabling, and disposal. Lifecycle events allow reactive systems to respond to changes in the
entity's state.

### ⚡ Events

#### `OnInitialized`

```csharp
public event Action OnInitialized  
```

- **Description:** Occurs when the object has been successfully initialized.
- **Triggers:** Fired by the `Init()` method after successful initialization.

#### `OnDisposed`

```csharp
public event Action OnDisposed  
```

- **Description:** Occurs when the object has been disposed and its resources released.
- **Triggers:** Fired when `Dispose()` is called.

#### `OnEnabled`

```csharp
public event Action OnEnabled  
```

- **Description:** Occurs when the object is enabled.
- **Triggers:** Fired by the `Enable()` method.

#### `OnDisabled`

```csharp
public event Action OnDisabled  
```

- **Description:** Occurs when the object is disabled.
- **Triggers:** Fired by the `Disable()` method.

#### `OnTicked`

```csharp
public event Action<float> OnTicked  
```

- **Description:** Occurs during the regular `Update` phase, once per frame.
- **Triggers:** Fired inside `Tick(float deltaTime)`.
- **Parameter:** `deltaTime` – Time in seconds since the last frame.

#### `OnFixedTicked`

```csharp
public event Action<float> OnFixedTicked  
```

- **Description:** Occurs during the `FixedUpdate` phase, typically used for physics updates.
- **Triggers:** Fired inside `FixedTick(float deltaTime)`.
- **Parameter:** `deltaTime` – Fixed time step used by the physics engine.
- **Exceptions:** None.

#### `OnLateTicked`

```csharp
public event Action<float> OnLateTicked  
```

- **Description:** Occurs during the `LateUpdate` phase, after all `Update` calls have been made.
- **Triggers:** Fired inside `LateTick(float deltaTime)`.
- **Parameter:** `deltaTime` – Time in seconds since the last frame.

---

### 🔑 Properties

#### `Initialized`

```csharp
public bool Initialized { get; }  
```

- **Description:** Indicates whether the object is currently initialized.
- **Returns:** `true` if the object has been initialized, otherwise `false`.

#### `Enabled`

```csharp
public bool Enabled { get; }  
```

- **Description:** Indicates whether the object is currently enabled.
- **Returns:** `true` if enabled, otherwise `false`.

---

### 🏹 Methods

#### `Init()`

```csharp
public void Init()  
```

- **Description:** Initializes the entity.
- **Behavior:**
    - Transitions the entity to the `Initialized` state.
    - Calls `Init` on all behaviours implementing `IEntityInit`.
    - Triggers the `OnInitialized` event.
    - If the entity is already initialized, does nothing.

#### `Enable()`

```csharp
public void Enable()  
```

- **Description:** Enables the entity for updates.
- **Behavior:**
    - Transitions the entity to the `Enabled` state.
    - Calls `Enable` on all behaviours implementing `IEntityEnable`.
    - Triggers the `OnEnabled` event.
    - If the entity is not initialized yet, it will be initialized automatically.
    - If the entity is already enabled, does nothing.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime)  
```

- **Description:** Calls `Update` on all behaviours implementing `IEntityUpdate`.
- **Behavior:**
    - Triggers the `OnTicked` event.
    - Can only be invoked if the entity is enabled.
- **Parameter:** `deltaTime` – Time in seconds since the last frame.
- **Exceptions:** Throws if the entity is not enabled.

#### `FixedTick(float)`

```csharp
public void FixedTick(float deltaTime)  
```

- **Description:** Calls `FixedUpdate` on all behaviours implementing `IEntityFixedUpdate`.
- **Behavior:**
    - Triggers the `OnFixedTicked` event.
    - Can only be invoked if the entity is enabled.
- **Parameter:** `deltaTime` – Fixed time step used by the physics engine.
- **Exceptions:** Throws if the entity is not enabled.

#### `LateTick(float)`

```csharp
public void LateTick(float deltaTime)  
```

- **Description:** Calls `LateUpdate` on all behaviours implementing `IEntityLateUpdate`.
- **Behavior:**
    - Triggers the `OnLateTicked` event.
    - Can only be invoked if the entity is enabled.
- **Parameter:** `deltaTime` – Time in seconds since the last frame.
- **Exceptions:** Throws if the entity is not enabled.

#### `Disable()`

```csharp
public void Disable()  
```

- **Description:** Disables the entity for updates.
- **Behavior:**
    - Transitions the entity to a not `Enabled` state.
    - Calls `Disable` on all behaviours implementing `IEntityDisable`.
    - Triggers the `OnDisabled` event.
    - If the entity is not enabled yet, does nothing.

#### `Dispose()`

```csharp
public void Dispose()  
```

- **Description:** Cleans up all resources used by the entity.
- **Behavior:**
    - Transitions the entity to a not `Initialized` state.
    - Calls `Dispose` on all behaviours implementing `IEntityDispose`.
    - Clears all tags, values, and behaviours.
    - Unsubscribes from all events.
    - Unregisters the entity from the `EntityRegistry`.
    - Disposes stored values if `Settings.disposeValues` is `true`.
    - If the entity is enabled, calls `Disable()` automatically.
    - If the entity is not initialized yet, does not call `IEntityDispose.Dispose` or trigger `OnDisposed`.

---

## 🗂 Example of Usage

```csharp
// Create a new entity in C#
IEntity entity = new Entity();
entity.Name = "Character";

// Add a tag
entity.AddTag("Player");

// Add a value
entity.AddValue("Health", 100);

// Add a behaviour
entity.AddBehaviour<MovementBehaviour>();

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
