# 🧩️ SceneEntity

```csharp
[AddComponentMenu("Atomic/Entities/Entity")]
[DisallowMultipleComponent]
[DefaultExecutionOrder(-1000)]
public class SceneEntity : MonoBehaviour, IEntity, ISerializationCallbackReceiver
```

- **Description:** Represents the Unity implementation of the entity. It allows installation from the Unity
  Scene and composition through the Inspector or installers.

- **Inheritance:** `MonoBehaviour`, [IEntity](IEntity.md)
- **Notes:** Supports Unity serialization and Odin Inspector

---

<details>
  <summary>
    <h2 id="-core">💠 Core</h2>
    <br> Represent the fundamental identity and state of the entity. It includes unique identifiers, optional names for
         debugging or tooling, and the main event for reactive state changes.
  </summary>

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

#### `Name`

```csharp
public string Name { get; set; }
```

- **Description:** Optional user-defined name for debugging or tooling.
- **Note:** Equals `GameObject` name

---

### 🗂 Examples of Usage

```csharp
// Create a new instance of entity
SceneEntity entity = ...

// Subscribe to the OnStateChanged event
entity.OnStateChanged += (IEntity e) =>
{
    Console.WriteLine($"Entity {e.Name} (ID: {e.InstanceID}) changed state!");
};

// Change name
entity.Name = "Hero"; //Triggers state changed

// Read the unique runtime identifier
int id = entity.InstanceID;
Console.WriteLine($"Created entity '{entity.Name}' with ID: {id}");
```

</details>

---

<details>
  <summary>
    <h2 id="-tags">🏷️ Tags</h2>
    <br> Manage lightweight categorization and filtering of entities. Tags are integer-based labels that can be added, removed,
         enumerated, or checked. They are useful for grouping entities, querying, and driving logic based on assigned tags.
  </summary>

<br>

> Tags in the entity behave like a **HashSet of integers**. All operations such as add, check, or remove have **O(1)
average time complexity**, and duplicate tags are **not allowed**.

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

</details>

---

<details>
  <summary>
    <h2 id="-values">🔑 Values</h2>
    <br> Manage dynamic key-value storage for the entity. Values can be of any type (structs or reference types) and are
         identified by integer keys. This allows flexible runtime data storage, reactive updates, and modular logic.

  </summary>

<br>

> Values in the entity are stored as a **key-value collection with integer keys**. Access, addition, update, and
> removal
> operations generally have **dictionary-like time complexity**. Values can be of any type, including structs and
> reference types, and multiple types can coexist under different keys. Note that adding a struct through the generic
> API
> avoids boxing.

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

### 🗂 Example of Usage

```csharp
// Assume we have an instance of entity
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

</details>

---

<details>
  <summary>
    <h2 id="-behaviours">⚙️ Behaviours</h2>
    <br>
    Manage modular logic attached to the entity. Behaviours implement 
    <a href="../Behaviours/IEntityBehaviour.md">IEntityBehaviour</a> interfaces and can be added, removed, queried, or enumerated at runtime. 
    This allows flexible composition of entity logic, enabling dynamic functionality without changing the core entity structure. 
    Behaviours can respond to lifecycle events (<code>Init</code>, <code>Enable</code>, <code>Tick</code>, <code>Disable</code>, <code>Dispose</code>), 
    enabling dynamic logic composition without changing the core entity structure.
  </summary>

<br>

> For behaviours entity acts as a container using a **List**, which means that all algorithmic operations have
> **List-like time complexity**.
> Additionally, the entity **can store multiple references to the same behaviour instance**,
> so duplicate entries are allowed.

---

### 🛠 Inspector Settings

| Parameters                 | Description                                                                   | 
|----------------------------|-------------------------------------------------------------------------------|
| `initialBehaviourCapacity` | Initial capacity for behaviours to optimize memory allocation. Default is `0` |

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
public BehaviourEnumerator GetBehaviourEnumerator()  
```

- **Description:** Enumerates all behaviours attached to the entity.
- **Returns:** Struct enumerator for iterating through behaviours.

---

### 🗂 Example of Usage

```csharp
// Assume we have an instance of entity
SceneEntity entity = ...

// Subscribe to events
entity.OnBehaviourAdded += (e, b) => 
    Console.WriteLine($"Behaviour {b.GetType().Name} added to {e.Id}");

entity.OnBehaviourDeleted += (e, b) => 
    Console.WriteLine($"Behaviour {b.GetType().Name} removed from {e.Id}");

// Add behaviours
player.AddBehaviour(new MovementBehaviour());
player.AddBehaviour(new RotationBehaviour());

// Check count
Console.WriteLine($"Total behaviours: {entity.BehaviourCount}");

// Retrieve behaviour by type
MovementBehaviour movementBehaviour = entity.GetBehaviour<MovementBehaviour>();

// Try to retrieve behaviour by type
if (entity.TryGetBehaviour(out RotationBehaviour rotation))
    Console.WriteLine("Found RotationBehaviour");

// Remove behaviour
entity.DelBehaviour<MovementBehaviour>();

// Clear all behaviours
entity.ClearBehaviours();

// Enumerate all behaviours
foreach (IEntityBehaviour behaviour in entity.GetBehaviourEnumerator())
    Console.WriteLine($"Behaviour: {behaviour.GetType().Name}");

// Get array of behaviours
IEntityBehaviour[] behaviours = entity.GetBehaviours();

// Copy to array
IEntityBehaviour[] buffer = new IEntityBehaviour[10];
int copied = entity.CopyBehaviours(buffer);

Console.WriteLine($"Copied {copied} behaviours into buffer");
```

</details>

----

<details>
  <summary>
    <h2 id="-lifecycle">♻️ Lifecycle</h2>
    <br>
    Manage the entity's state transitions and update phases. It covers initialization, enabling,
    per-frame updates, disabling, and disposal. Lifecycle events allow reactive systems to respond to changes in the
    entity's state.
  </summary>

### 🛠 Inspector Settings

| Parameters          | Description                                                                                      |
|---------------------|--------------------------------------------------------------------------------------------------|
| `useUnityLifecycle` | Enables automatic syncing with Unity MonoBehaviour lifecycle (`Start`, `OnEnable`, `OnDisable`). |
| `disposeValues`     | Determines whether values are disposed when `Dispose()` is called.                               |

---

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

#### `OnDispose()`

```csharp
protected virtual void OnDispose()  
```

- **Description:**  Called during the disposal process of a `SceneEntity`. Provides a hook for derived classes to
  execute custom cleanup logic when the entity is being disposed.
- **Notes:** This method is invoked by `Dispose()`

---

### 🗂 Example of Usage

```csharp
// Assume we have an instance of entity
SceneEntity player = ...

// Subscribe to lifecycle events
player.OnInitialized += () => Console.WriteLine("Entity initialized");
player.OnDisposed += () => Console.WriteLine("Entity disposed");
player.OnEnabled += () => Console.WriteLine("Entity enabled");
player.OnDisabled += () => Console.WriteLine("Entity disabled");
player.OnTicked += deltaTime => Console.WriteLine($"Tick: {deltaTime}");
player.OnFixedTicked += deltaTime => Console.WriteLine($"FixedTick: {deltaTime}");
player.OnLateTicked += deltaTime => Console.WriteLine($"LateTick: {deltaTime}");

// Initialize and enable the entity
player.Init();
player.Enable();

// Simulate game loop updates
player.Tick(0.016f);       // Update (frame)
player.FixedTick(0.02f);   // Physics update
player.LateTick(0.016f);   // Late update

// Disable the entity
player.Disable();

// Dispose the entity
player.Dispose();
```

</details>

---

<details>
  <summary>
    <h2 id="-installing"> 🔧 Installing</h2>
    <br>
    Describes how a <code>SceneEntity</code> is populated with <b>tags</b>, <b>values</b>, and <b>behaviours</b> at
    runtime or in the editor. It also manages child entities through installers, ensuring that all dependencies are properly configured and applied.
  </summary>

### 🛠 Inspector Settings

| Parameter              | Description                                                                                                                                                                                                                                    |
|------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `installOnAwake`       | If enabled, `Install()` is automatically called in `Awake()`. Default is `true`                                                                                                                                                                |
| `installInEditMode`    | If enabled, `Install()` is called every time `OnValidate` is invoked in Edit Mode. Default is `false`. <br/>**Warning:** If you create Unity objects or other heavy objects in `Install()`, turn this off to avoid performance issues.         |
| `uninstallOnDestroy`   | If enabled, `Uninstall()` is automatically called in `OnDestroy`. Default is `true`                                                                                                                                                            |
| `sceneInstallers`      | List of MonoBehaviour installers that configure values and systems in this entity. Installers are executed in the order they appear in the array. Null references are automatically skipped, making partially configured lists safe to use.    |
| `scriptableInstallers` | List of ScriptableObject installers that configure values and systems in this entity. Installers are executed in the order they appear in the array. Null references are automatically skipped, making partially configured lists safe to use. |
| `children`             | Child entities installed together with this entity. Children are executed in the order they appear in the array. Null references are automatically skipped, making partially configured lists safe to use.                                     |

---

### 🔑 Properties

#### `Installed`

```csharp
public bool Installed { get; }
```

- **Description:** Returns true if the entity already has been installed.

---

### 🏹 Methods

#### `Install()`

```csharp
public void Install()  
```

- **Description:** Installs all configured installers and child entities into this `SceneEntity`. Ensures that tags,
  values, and behaviours are properly set up at runtime or in the editor.
- **Warnings:** Logs warnings when null references are found.
- **Notes:** Skips null installers and null children.

#### `OnInstall()`

```csharp
protected virtual void OnInstall()  
```

- **Description:** Called during the installation process of a `SceneEntity`. Provides a hook for derived classes to
  execute custom logic when the entity is being installed.
- **Notes:** This method is invoked by `Install()` before processing installers and child entities.

#### `Uninstall()`

```csharp
public void Uninstall()  
```

- **Description:** Uninstalls all configured installers and child entities from this `SceneEntity`. Marks the entity as
  not installed, allowing it to be reinstalled.
- **Warnings:** Warnings are logged for null references to help debugging.
- **Notes:** Null installers and null children are safely skipped.

#### `OnUninstall()`

```csharp
protected virtual void OnUninstall()  
```

- **Description:** Called during the uninstallation process of a `SceneEntity`. Provides a hook for derived classes to
  execute custom logic when the entity is being uninstalled.
- **Notes:** This method is invoked by `Uninstall()` before processing installers and child entities.

---

### 🏹 Static Methods

There are also static methods that allow installing entities globally in a scene.

#### `InstallAll(Scene)`

```csharp
public static void InstallAll(Scene scene)  
```

- **Description:** Installs all `SceneEntity` instances found in the given `Scene` that are not yet installed. This is a
  convenience method that calls the generic version `InstallAll<SceneEntity>(scene)`.
- **Parameter:** `scene` – The `Scene` in which to search for `SceneEntity` instances.
- **Exception:** Throws if `scene` is not valid or not loaded.
- **Note:**
    - Skips entities that are already installed.
    - Null GameObjects are skipped.
    - Entities that are already installed are ignored.

#### `InstallAll<E>(Scene)`

```csharp
public static void InstallAll<E>(Scene scene) where E : SceneEntity  
```

- **Description:** Installs all `SceneEntity` instances of type `<E>` found in the specified `Scene` that are not yet
  installed. Iterates through all root GameObjects and all child objects to find entities of type `<E>`.
- **Type Parameters:** `E` – The type of `SceneEntity` to search for and install.
- **Parameter:** `scene` – The `Scene` in which to search for `<E>` instances.
- **Exception:** Throws if `scene` is not valid or not loaded.
- **Note:**
    - Skips entities that are already installed.
    - Null GameObjects are skipped.
    - Entities that are already installed are ignored.

---

### 🗂 Example of Usage

#### 1. Create `CharacterInstaller` script

 ```csharp
//Populates entity with tags, values and behaviours
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f; //Immutable variable
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection; //Mutable variable with subscription

    public override void Install(IEntity entity)
    {
        //Add tags to a character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to a character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
    }
}
```

#### 2. Attach `CharacterInstaller` script to the GameObject

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 3. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="464" height="" alt="изображение" src="../../Images/SceneEntity%20Attach%20Installer.png" />

#### 4. Now your `Entity` has tags and properties.

</details>

---

<details>
  <summary>
    <h2 id="-gizmos"> 🖌️ Gizmos</h2>
    <br>
    Provides visual debugging support through Unity Gizmos in the Scene view.
  </summary>

### 🛠 Inspector Settings

| Parameter            | Description                                                           |
|----------------------|-----------------------------------------------------------------------|
| `onlySelectedGizmos` | Draw gizmos only when this GameObject is selected. Default is `false` |
| `onlyEditModeGizmos` | Draw gizmos only when Unity is not in Play mode.Default is `false`    |

---

</details>

---

<details>
  <summary>
    <h2 id="-context"> ▶️ Context Menu</h2>
  </summary>
<br>

### 🏹 Methods

#### `Compile`

```csharp
[ContextMenu("Compile")]
private void Compile();
```

- **Description:** Fully compiles entity state:
- **Behaviour**:
    1. Disable and Dispose entity in Edit mode if gameObject is not prefab. Only for behaviours
       with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md)
    2. Uninstall previous entity state
    3. Install new entity state
    4. Precomputes **capacity**, **tags**, **values**, **behaviours** of the entity
    5. Init and Enable entity in Edit mode if gameObject is not prefab. Only for behaviours
       with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md)

#### `Reset`

```csharp
[ContextMenu("Reset")]
private void Reset();
```

- **Description:** Fully resets entity state:
    1. Disable and Dispose entity in Edit mode if gameObject is not prefab. Only for behaviours
       with [RunInEditModeAttribute](../Attributes/RunInEditModeAttribute.md)
    2. Uninstall previous entity state
    3. Resets all parameters to default
    4. Gathers all SceneEntityInstallers and child Entities

</details>

---

<details>
  <summary>
    <h2 id="-debug">🐞 Debug Properties</h2>
    <br>
    These properties are available only in <b>Unity Editor</b> when using <b>Odin Inspector</b>.
  </summary>

- `Initialized` — Displays if the entity is initialized.
- `Enabled` — Displays if the entity is enabled.
- `DebugTags` — Sorted list of tags for debug display.
- `DebugValues` — Sorted list of values for debug display.
- `DebugBehaviours` — Sorted list of attached behaviours for debug display.

</details>

---

<details>
  <summary>
    <h2 id="-entity-creation"> 🏗️ Creation</h2>
    <br> The following methods allow you to create entities at runtime, for example from prefabs or entirely new GameObjects.
  </summary>

### 🔹 Parameterized Instantiation

The first way to create entities is through `CreateArgs`, which allows a developer to specify settings for creating a
new GameObject with a `SceneEntity` component.

---

#### `CreateArgs`

```csharp
[Serializable]  
public struct CreateArgs
{
      public string name;
      public IEnumerable<int> tags;
      public IReadOnlyDictionary<int, object> values;
      public IEnumerable<IEntityBehaviour> behaviours;
      public List<SceneEntityInstaller> sceneInstallers;
      public List<ScriptableEntityInstaller> scriptableInstallers;
      public List<SceneEntity> children;

      public int initialTagCapacity;
      public int initialValueCapacity;
      public int initialBehaviourCapacity;

      public bool installOnAwake;
      public bool disposeValues;
      public bool useUnityLifecycle;
}
```

- **Description:** Defines a set of parameters for creating a dynamic entity.
- **Fields:**
    - `string name` – optional name for the GameObject.
    - `IEnumerable<int> tags` – optional tags to assign.
    - `IReadOnlyDictionary<int, object> values` – optional key-value pairs.
    - `IEnumerable<IEntityBehaviour> behaviours` – optional behaviours to attach.
    - `List<SceneEntityInstaller> sceneInstallers` – optional MonoBehaviour installers to run.
    - `List<SceneEntityInstaller> scriptableInstallers` – optional ScriptableObject installers to run.
    - `List<SceneEntity> children` – optional child entities.
    - `int initialTagCapacity` – initial capacity for tags.
    - `int initialValueCapacity` – initial capacity for values.
    - `int initialBehaviourCapacity` – initial capacity for behaviours.
    - `bool installOnAwake` – if true, installs automatically on Awake.
    - `bool uninstallOnDestroy` – if true, uninstalls automatically on Destroy.
    - `bool disposeValues` – if true, disposes values on destruction.
    - `bool useUnityLifecycle` – if true, uses Unity lifecycle methods.

---

#### `Create(in CreateArgs)`

```csharp
public static SceneEntity Create(in CreateArgs args)  
```

- **Description:** Creates a new `SceneEntity` GameObject and configures it with optional tags, values, behaviours,
  installers, and children.
- **Parameter:** `args` – Configuration options in a `CreateArgs` structure.
- **Returns:** The newly created `SceneEntity` instance.
- **Exception:** Throws if `args` contains invalid references.
- **Note:** Skips null installers or children.

#### `Create<E>(in CreateArgs)`

```csharp
public static E Create<E>(in CreateArgs args) where E : SceneEntity  
```

- **Description:** Generic version of `Create` that returns a `SceneEntity` of type `<E>`.
- **Type Parameter:** `E` – The type of SceneEntity to create.
- **Parameter:** `args` – Configuration options in a `CreateArgs` structure.
- **Returns:** A newly created `SceneEntity` of type `E`.
- **Exception:** Throws if `args` contains invalid references.
- **Note:** Skips null installers or children.

#### `Create<E>(...)`

```csharp
public static E Create<E>(  
    string name = null,  
    IEnumerable<int> tags = null,  
    IReadOnlyDictionary<int, object> values = null,  
    IEnumerable<IEntityBehaviour> behaviours = null,  
    bool installOnAwake = true,  
    bool disposeValues = true,  
    bool useUnityLifecycle = true,  
    int initialTagCount = 1,  
    int initialValueCount = 1,  
    int initialBehaviourCount = 1  
) where E : SceneEntity  
```

- **Description:** Convenience overload that constructs a `CreateArgs` internally and calls
  `Create<E>(in CreateArgs args)`.
- **Parameters:**
    - `name` – optional GameObject name.
    - `tags` – optional collection of integer tags.
    - `values` – optional key-value pairs.
    - `behaviours` – optional behaviours to attach.
    - `installOnAwake` – if true, runs installers on Awake.
    - `uninstallOnDestroy` – if true, runs Uninstall on OnDestroy.
    - `disposeValues` – if true, disposes values on destruction.
    - `useUnityLifecycle` – if true, uses Unity lifecycle.
    - `initialTagCount` – initial tag capacity.
    - `initialValueCount` – initial value capacity.
    - `initialBehaviourCount` – initial behaviour capacity.

- **Returns:** A newly created `SceneEntity` of type `<E>`.
- **Exception:** Throws if provided values are invalid.
- **Notes:** Null references are skipped.

---

#### 🗂 Examples of Usage

```csharp
//Non-generic version
var args = new CreateArgs
{
    Name = "Enemy",
    TagCapacity = 2,
    ValueCapacity = 2,
    BehaviourCapacity = 2
};

SceneEntity enemy = SceneEntity.Create(args);
```

```csharp
//Generic version
WeaponEntity enemy = SceneEntity.Create<WeaponEntity>(
    new CreateArgs
    {
        Name = "MachineGun",
        TagCapacity = 3,
        ValueCapacity = 5
    }
);
```

---

### 🔹 Prefab Instantiation

Another approach is creating game entities from prefabs.

#### `Create(SceneEntity, Transform)`

```csharp
public static SceneEntity Create(SceneEntity prefab, Transform parent = null)  
```

- **Description:** Instantiates a prefab and installs the resulting entity under an optional parent.
- **Parameters:**
    - `prefab` – The prefab to instantiate.
    - `parent` – Optional parent transform.

- **Returns:** The newly instantiated `SceneEntity`.

#### `Create<E>(E, Transform)`

```csharp
public static E Create<E>(E prefab, Transform parent = null) where E : SceneEntity  
```

- **Description:** Generic version of prefab instantiation. Defaults position to `Vector3.zero` and rotation to
  `Quaternion.identity`.
- **Parameters:**
    - `prefab` – Prefab to instantiate.
    - `parent` – Optional parent transform.

- **Returns:** The newly instantiated SceneEntity of type `E`.

#### `Create(SceneEntity, Vector3, Quaternion, Transform)`

```csharp
public static SceneEntity Create(
    SceneEntity prefab,
    Vector3 position,
    Quaternion rotation,
    Transform parent = null
)  
```

- **Description:** Instantiates a prefab at a given position and rotation with an optional parent, then installs it.
- **Parameters:**
    - `prefab` – Prefab to instantiate.
    - `position` – Position for the new entity.
    - `rotation` – Rotation for the new entity.
    - `parent` – Optional parent transform.
- **Returns:** The newly instantiated `SceneEntity`.

#### `Create<E>(E, Vector3, Quaternion, Transform)`

```csharp
public static E Create<E>(
    E prefab,
    Vector3 position,
    Quaternion rotation,
    Transform parent = null
) where E : SceneEntity  
```

- **Description:** Generic version of prefab instantiation at a specific position and rotation.

- **Parameters:**
    - `prefab` – Prefab to instantiate.
    - `position` – Position for the new entity.
    - `rotation` – Rotation for the new entity.
    - `parent` – Optional parent transform.

- **Returns:** The newly instantiated SceneEntity of type `E`.
- **Notes:** Automatically calls `Install()` on the created entity.

#### `Create<E>(E, Transform, Transform)`

```csharp
public static E Create<E>(E prefab, Transform point, Transform parent) where E : SceneEntity  
```

- **Description:** Instantiates the prefab at the position and rotation of a reference transform (`point`) with an
  optional parent.
- **Parameters:**
    - `prefab` – Prefab to instantiate.
    - `point` – Reference transform for position and rotation.
    - `parent` – Optional parent transform.

- **Returns:** The newly instantiated SceneEntity of type `E`.
- **Note:** Automatically calls `Install()` on the created entity.

---

#### 🗂 Examples of Usage

```csharp
// Instantiating a prefab at the origin
SceneEntity enemyPrefab = Resources.Load<SceneEntity>("Prefabs/Enemy");
SceneEntity instance = SceneEntity.Create(enemyPrefab);
```

```csharp
// Instantiating a prefab at a specific position and rotation
Vector3 spawnPos = new Vector3(0, 0, 0);
Quaternion rotation = Quaternion.Euler(0, 180, 0);
SceneEntity bossInstance = SceneEntity.Create(enemyPrefab, spawnPos, rotation);
```

</details>

---

<details>
  <summary>
    <h2 id="-entity-destruction"> 🗑️ Destruction</h2>
    <br> This section provides methods of how to destroy entities at runtime.
  </summary>

### 🏹 Methods

#### `Destroy(IEntity, float)`

```csharp
public static void Destroy(IEntity entity, float t = 0)  
```

- **Description:** Destroys the associated `GameObject` of the specified `IEntity` if it can be cast to a `SceneEntity`.
- **Parameters:**
    - `entity` – The entity whose `GameObject` should be destroyed.
    - `t` – Optional delay in seconds before destruction. Defaults to `0`.
- **Note:** Internally casts the `IEntity` to `SceneEntity` before destroying.

#### `Destroy(SceneEntity, float)`

```csharp
public static void Destroy(SceneEntity entity, float t = 0)  
```

- **Description:** Destroys the specified `SceneEntity`'s `GameObject` after an optional delay.
- **Parameters:**
    - `entity` – The `SceneEntity` to destroy.
    - `t` – Optional delay in seconds before destruction. Defaults to `0`.
- **Note:** If `entity` is `null`, no action is taken.

---

### 🗂 Example of Usage

```csharp
// Destroys entity after 3 seconds
SceneEntity.Destroy(sceneEntity, 3f);
```

</details>

---

<details>
  <summary>
    <h2 id="-entity-casting"> 🪄 Casting</h2>
    <br> This section provides methods for safe casting between <code>IEntity</code> and <code>SceneEntity</code>.
  </summary>

### 🏹 Methods

#### `Cast(IEntity)`

```csharp
public static SceneEntity Cast(IEntity entity)  
```

- **Description:** Casts the specified `IEntity` to a `SceneEntity` if possible.
- **Parameter:** `entity` – The entity to cast.
- **Returns:** The entity cast to `SceneEntity`, or `null` if the input is `null`.
- **Exceptions:** Throws `InvalidCastException` if the entity cannot be cast to `SceneEntity`.
- **Note:** Uses `AggressiveInlining` for performance.

#### `Cast<E>(IEntity)`

```csharp
public static E Cast<E>(IEntity entity) where E : SceneEntity  
```

- **Description:** Casts the specified `IEntity` to the target type `E`. Supports direct `SceneEntity` instances and
  `SceneEntityProxy<E>` wrappers.
- **Type Parameter:** `E` – The type of `SceneEntity` to cast to.
- **Parameter:** `entity` – The entity to cast.
- **Returns:** The entity cast to type `E`, or `null` if the input is `null`.
- **Exceptions:** Throws `InvalidCastException` if the entity cannot be cast to the target type `E`.

#### `TryCast(IEntity, out SceneEntity)`

```csharp
public static bool TryCast(IEntity entity, out SceneEntity result)  
```

- **Description:** Attempts to cast the specified `IEntity` to a `SceneEntity`.
- **Parameters:**
    - `entity` – The entity to cast.
    - `result` – The cast result if successful; otherwise, `null`.
- **Returns:** `true` if the cast was successful; otherwise, `false`.

#### `TryCast<E>(IEntity, out E)`

```csharp
public static bool TryCast<E>(IEntity entity, out E result) where E : SceneEntity  
```

- **Description:** Attempts to cast the specified `IEntity` to the target type `E`. Supports direct `SceneEntity`
  instances and `SceneEntityProxy<E>` wrappers.
- **Type Parameter:** `E` – The type of `SceneEntity` to cast to.
- **Parameters:**
    - `entity` – The entity to cast.
    - `result` – The cast result if successful; otherwise, `null`.
- **Returns:** `true` if the cast was successful; otherwise, `false`.

---

### 🗂 Examples of Usage

#### Simple cast to `SceneEntity`

```csharp
IEntity entity = GetEntityFromRegistry();
SceneEntity sceneEntity = SceneEntity.Cast(entity);
```

> Throws an exception if `entity` is not a `SceneEntity`.

#### Generic cast to a specific `SceneEntity` type

```csharp
IEntity entity = GetEntityFromRegistry();
EnemyEntity enemy = SceneEntity.Cast<EnemyEntity>(entity);
```

> Throws an exception if entity is not of type `EnemyEntity` or a proxy of it.

#### Safe cast using `TryCast`

```csharp
IEntity entity = GetEntityFromRegistry();
if (SceneEntity.TryCast(entity, out SceneEntity sceneEntity))
    Debug.Log($"Successfully casted to SceneEntity: {sceneEntity.Name}");
else
    Debug.LogWarning("Entity is not a SceneEntity");
```

#### Safe generic cast using TryCast<E>

```csharp
IEntity entity = GetEntityFromRegistry();
if (SceneEntity.TryCast<EnemyEntity>(entity, out EnemyEntity enemy))
    Debug.Log($"Successfully casted to EnemyEntity: {enemy.Name}");
else
    Debug.LogWarning("Entity is not of type EnemyEntity");
```

</details>

---

## 🗂 Example Usage

Below is the process for quickly creating a character entity in Unity

#### 1. Create a new `GameObject`

<img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### 2. Add `Entity` Component to the GameObject

<img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

#### 3. Create `MoveBehaviour` for your entity

```csharp
// Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    // Called when MonoBehaviour.Start() is invoked
    public void Init(IEntity entity)
    {
        _transform = entity.GetValue<Transform>("Transform");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    // Called when MonoBehaviour.FixedUpdate() is invoked
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _transform.position += _moveSpeed.Value * deltaTime * direction;
    }
}
```

#### 4. Create `CharacterInstaller` script

 ```csharp
//Populates entity with tags, values and behaviours
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f; //Immutable variable
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection; //Mutable variable with subscription

    public override void Install(IEntity entity)
    {
        //Add tags to a character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to a character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
        
        //Add behaviours to a character
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

#### 5. Attach `CharacterInstaller` script to the GameObject

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 6. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="464" height="" alt="изображение" src="../../Images/SceneEntity%20Attach%20Installer.png" />

#### 7. Enter `PlayMode` and check your character movement!

---

<details>
  <summary>
    <h2 id="-notes">📝 Notes</h2>
  </summary>

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
- **Casting & Proxies** – Safe conversion between `IEntity`, `SceneEntity` and `SceneEntityProxy`.
- **Scene-Wide Installation** – Can install all SceneEntities in a scene.
- **Odin Inspector Support** – Optional editor enhancements for configuration and debug.
- **Not Thread Safe** — All operations should be performed on the main Unity thread.
- `SceneEntity` is Unity-specific
- Default execution order is `-1000` (runs early)
- `[DisallowMultipleComponent]` prevents multiple entities per `GameObject`

</details>