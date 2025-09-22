# 🧩️ SceneEntity

Represents a Unity component implementation of an [IEntity](IEntity.md). This class follows the
**Entity–State–Behaviour** pattern, providing a modular container for dynamic state, tags, values,
behaviours, and lifecycle management. It allows installation from the Unity Scene and composition through the Inspector
or installers.

```csharp
public class SceneEntity : MonoBehaviour, IEntity, ISerializationCallbackReceiver
```

---

## 📚 Content

- [Core](#-core)
- [Tags](#-tags)
- [Values](#-values)
- [Behaviours](#-behaviours)
- [Lifecycle](#-lifecycle)
- [Installing](#-installing)
- [Optimization](#-optimization)
- [Gizmos](#-gizmos)
- [Debug Properties](#-debug-properties)
- [Creation](#-entity-creation)
- [Destruction](#-entity-destruction)
- [Casting](#-entity-casting)
- [Example of Usage](#-example)
- [Performance](#-performance)
- [Notes](#-notes)

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

## 🗂 Example of Usage

```csharp
// Assume we have instance of entity
SceneEntity entity = ...

// Subscribe to the OnStateChanged event
entity.OnStateChanged += (IEntity e) =>
{
    Console.WriteLine($"Entity {e.Name} (ID: {e.InstanceID}) changed state!");
};

// Change game object name
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

> ❗️ Tags in the entity behave like a **HashSet of integers**. All operations such as add, check, or remove have **O(1)
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

</details>

---

<details>
  <summary>
    <h2 id="-values">🔑 Values</h2>
    <br> Manage dynamic key-value storage for the entity. Values can be of any type (structs or reference types) and are
         identified by integer keys. This allows flexible runtime data storage, reactive updates, and modular logic.

  </summary>

<br>

> ❗️ Values in the entity are stored as a **key-value collection with integer keys**. Access, addition, update, and
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

> ❗ For behaviours entity acts as a container using a **List**, which means that all algorithmic operations have *
*List-like time complexity**.
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

Below is an example of working with behaviours in `SceneEntity`.

#### 1️⃣ Basic Usage

```csharp
// Assume we have a player entity:
Entity player = ...

// Subscribe to events
player.OnBehaviourAdded += (e, b) => 
    Console.WriteLine($"Behaviour {b.GetType().Name} added to {e.Id}");

player.OnBehaviourDeleted += (e, b) => 
    Console.WriteLine($"Behaviour {b.GetType().Name} removed from {e.Id}");

// Add behaviours
player.AddBehaviour(new MovementBehaviour());
player.AddBehaviour(new RotationBehaviour());

// Check count
Console.WriteLine($"Total behaviours: {player.BehaviourCount}");

// Retrieve behaviour by type
MovementBehaviour movementBehaviour = player.GetBehaviour<MovementBehaviour>();

// Try to retrieve behaviour by type
if (player.TryGetBehaviour<RotationBehaviour>(out var rotation))
    Console.WriteLine("Found RotationBehaviour");

// Remove behaviour
player.DelBehaviour<MovementBehaviour>();

// Clear all behaviours
player.ClearBehaviours();

// Enumerate all behaviours
foreach (IEntityBehaviour behaviour in player.GetBehaviourEnumerator())
    Console.WriteLine($"Behaviour: {behaviour.GetType().Name}");

// Get array of behaviours
IEntityBehaviour[] behaviours = player.GetBehaviours();

// Copy to array
IEntityBehaviour[] buffer = new IEntityBehaviour[10];
int copied = player.CopyBehaviours(buffer);

Console.WriteLine($"Copied {copied} behaviours into buffer");
```

#### 2️⃣ Using Extension Methods

The framework also provides [extension methods](Extensions.md#-behaviours) for convenient handling of behaviours.

```csharp
// Create a new entity
IEntity enemy = new Entity();

// Add behaviour by type (using new T())
enemy.AddBehaviour<MoveBehaviour>();

// Add multiple behaviours at once
var attackBehaviour = new AttackBehaviour();
var defenseBehaviour = new DefenseBehaviour();

enemy.AddBehaviours(new IEntityBehaviour[] {
    attackBehaviour, defenseBehaviour
});

// Remove multiple behaviours at once
enemy.DelBehaviours(new IEntityBehaviour[] {
    attackBehaviour, defenseBehaviour
});
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

This example demonstrates how to manage the lifecycle of an entity, including initialization, enabling, per-frame
updates, disabling, and disposal. Event subscriptions allow reacting to state changes in real time.

```csharp
// Assume we have SceneEntity instance
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

| Parameter           | Description                                                                                                                                                                                                                            |
|---------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `installOnAwake`    | If enabled, `Install()` is automatically called in `Awake()`. Default is `true`                                                                                                                                                        |
| `installInEditMode` | If enabled, `Install()` is called every time `OnValidate` is invoked in Edit Mode. Default is `false`. <br/>**Warning:** If you create Unity objects or other heavy objects in `Install()`, turn this off to avoid performance issues. |
| `installers`        | List of installers that configure values and systems in this entity. Installers are executed in the order they appear in the array. Null references are automatically skipped, making partially configured lists safe to use.          |
| `children`          | Child entities installed together with this entity. Children are executed in the order they appear in the array. Null references are automatically skipped, making partially configured lists safe to use.                             |

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

TODO:

</details>

---

<details>
  <summary>
    <h2 id="-optimization"> 📈 Optimization</h2>
    <br>
    Provides a simple workflow for precomputing entity capacities in the Unity Editor.
  </summary>

- **Compile Button** – Available in the context menu or as a button in the Inspector.  
  Pressing **Compile** will precompute and store the current sizes of **tags**, **values**, and **behaviours**.

> This feature helps inspect and optimize memory usage without affecting runtime behaviour.

### 🗂 Example of Usage

TODO:

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

### 🗂 Example of Usage

Below is an example of drawing a circle for a unit using its position and scale:

```csharp
public sealed class TransformGizmos : IEntityGizmos<IGameEntity>
{
    public void DrawGizmos(IGameEntity entity)
    {
        Vector3 center = entity.GetPosition().Value;
        float scale = entity.GetScale().Value;
        Handles.DrawWireDisc(center, Vector3.up, scale);
    }
}
```

Add it in a `SceneEntityInstaller`:

```csharp
[Serializable]
public sealed class TransformEntityInstaller : SceneEntityInstaller<IGameEntity>
{
    [SerializeField]
    private Const<float> _scale = 1;
    
    public void Install(IGameEntity entity)
    {
        entity.AddPosition(new ReactiveVector3());
        entity.AddRotation(new ReactiveQuaternion());
        entity.AddScale(_scale);
        
       // Connect the gizmos drawing logic
        entity.AddBehaviour<TransformGizmos>();
    }
}
```

</details>

---

## 🐞 Debug Properties

These properties are available only in **Unity Editor** when using **Odin Inspector**.

- `Initialized` — Displays if the entity is initialized.
- `Enabled` — Displays if the entity is enabled.
- `DebugTags` — Sorted list of tags for debug display.
- `DebugValues` — Sorted list of values for debug display.
- `DebugBehaviours` — Sorted list of attached behaviours for debug display.

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
      public List<SceneEntityInstaller> installers;
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
    - `List<SceneEntityInstaller> installers` – optional installers to run.
    - `List<SceneEntity> children` – optional child entities.
    - `int initialTagCapacity` – initial capacity for tags.
    - `int initialValueCapacity` – initial capacity for values.
    - `int initialBehaviourCapacity` – initial capacity for behaviours.
    - `bool installOnAwake` – if true, installs automatically on Awake.
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

TODO: с картинками

---

### 🔥 Performance

The performance measurements below were conducted on a **MacBook with Apple M1**, using **1,000 elements** for each
container type.  
All times are **median execution times** in microseconds (μs).

### 🏷️ Tags

Tags are implemented as a **HashSet of integers**, optimized for fast lookups, additions, and removals.

| Operation | HashSet (Median μs) | Tags (Median μs) |
|-----------|---------------------|------------------|
| Add       | 57.40               | 10.30            |
| Clear     | 0.10                | 2.80             |
| Contains  | 47.85               | 3.80             |
| Remove    | 24.30               | 5.50             |

> Tags are extremely lightweight and provide **O(1) average time complexity** for key operations.

---

Values act as a **Dictionary-like storage** mapping integer keys to objects or structs, supporting generic access and
unsafe references for high performance.

### 🔑 Values

| Operation     | Dictionary (Median μs) | Values (Median μs)                    |
|---------------|------------------------|---------------------------------------|
| Clear         | 1.30                   | 2.60                                  |
| Contains      | 6.90                   | 5.95                                  |
| Remove        | 6.70                   | 5.90                                  |
| Get           | 7.45                   | 4.10 (object)                         |
| Get & Cast    | 8.25                   | 12.00 (reference) / 4.70 (primitive)  |
| Get & Unsafe  | 7.80                   | 4.50 (primitive) / 4.20 (ref)         |
| Set           | 37.50                  | 62.50 (object) / 187.35 (primitive)   |
| TryGet        | 34.20                  | 33.80 (object)                        |
| TryGet Cast   | -                      | 50.75 (reference) / 4.90  (primitive) |
| TryGet Unsafe | -                      | 31.90 (reference) / 6.94  (primitive) |
| Add           | 34.10                  | 62.15 (object) / 178.45 (primitive)   |

> Values provide flexible access patterns with **minimal overhead**, especially for primitives and unsafe references.

---

### ⚙️ Behaviours

Behaviours are stored in a **list-like container**, supporting multiple references to the same instance. Operations
include addition, removal, and indexed access.

| Operation    | List (Median μs) | Behaviours (Median μs) |
|--------------|------------------|------------------------|
| Add          | 29.30            | 34.30                  |
| Clear        | 0.40             | 1.20                   |
| Not Contains | 1825.95          | 650.60                 |
| Remove       | 312.63           | 243.91                 |
| Get By Index | 1.60             | 2.30                   |

> Behaviours combine fast index access with flexibility to store duplicate references, though some operations are **O(n)
** in the worst case.


---

## 📝 Notes

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