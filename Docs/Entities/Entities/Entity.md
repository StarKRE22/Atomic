# 🧩️ Entity

Represents the fundamental implementation of an [IEntity](IEntity.md) in the framework. It follows the
**Entity–State–Behaviour** pattern and provides a modular container for **dynamic state**, **tags**, **values**,
**behaviours**, and **lifecycle management**.

```csharp
public class Entity : IEntity
```

---

## 📚 Content

- [Core](#-core-members)
- [Tags](#-tag-members)
- [Values](#-value-members)
- [Behaviours](#-behaviour-members)
- [Lifecycle](#-lifecycle-members)
- [Nested Types](#-nested-types)
- [Debug Properties](#-debug-properties)
- [Example of Usage](#-example-of-usage-4)
- [Performance](#-performance)
- [Notes](#-notes)

---

<details>
  <summary>
    <h2 id="-core-members">💠 Core</h2>
    <br> Represent the fundamental identity and state of the entity. It includes unique identifiers, optional names for
         debugging or tooling, and the main event for reactive state changes.
  </summary>

### 🏗️ Constructors

#### `String-keyed Constructor`

```csharp
public Entity(
    string name,
    IEnumerable<string> tags,
    IEnumerable<KeyValuePair<string, object>> values,
    IEnumerable<IEntityBehaviour> behaviours,
    Settings? settings = null
) 
```

- **Description:** Creates a new entity with the specified name, string tags, values, and behaviours. Initializes
  internal capacities and immediately adds all specified tags, values, and behaviours.
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tags` – Optional collection of string tag identifiers.
    - `values` – Optional collection of key-value pairs.
    - `behaviours` – Optional collection of behaviours to attach.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

#### `Int-keyed Constructor`

```csharp
    public Entity(
    string name,
    IEnumerable<int> tags,
    IEnumerable<KeyValuePair<int, object>> values,
    IEnumerable<IEntityBehaviour> behaviours,
    Settings? settings = null
)
```

- **Description:** Creates a new entity with the specified name, integer tags, values, and behaviours. Initializes
  internal capacities and immediately adds all specified tags, values, and behaviours.
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tags` – Optional collection of integer tag identifiers.
    - `values` – Optional collection of key-value pairs with integer keys.
    - `behaviours` – Optional collection of behaviours to attach.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

#### `Capacity-based Constructor`

```csharp
public Entity(
    string name = null,
    int tagCapacity = 0,
    int valueCapacity = 0,
    int behaviourCapacity = 0,
    Settings? settings = null
) 
```

- **Description:** Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.
  Initializes internal structures efficiently and registers the entity
  in [EntityRegistry](../Registry/EntityRegistry.md).
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tagCapacity` – Initial capacity for tag storage to minimize memory allocations.
    - `valueCapacity` – Initial capacity for value storage to minimize memory allocations.
    - `behaviourCapacity` – Initial capacity for behaviour storage to minimize memory allocations.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

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

#### `Name`

```csharp
public string Name { get; set; }
```

- **Description:** Optional user-defined name for debugging or tooling.
- **Note:** Useful for logging, inspector display, or editor tooling.


---

## 🗂 Example of Usage

```csharp
// Assume we have instance of entity
Entity entity = ...

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
    <h2 id="-tag-members">🏷️ Tags</h2>
    <br> Manage lightweight categorization and filtering of entities. Tags are integer-based labels that can be added, removed,
         enumerated, or checked. They are useful for grouping entities, querying, and driving logic based on assigned tags.
  </summary>

> [!IMPORTANT]
> Tags in the entity behave like a **HashSet of integers**. All operations such as add, check, or remove have **O(1) average time complexity**, and duplicate tags are **not allowed**.

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

This example demonstrates how to use tags with `Entity`, including adding, removing, and checking tags. Three
approaches
are shown: using **numeric keys** for performance, **string names** for readability and **code generation** for real
projects. Subscriptions to `OnTagAdded` and
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

```csharp
// Create a new entity
IEntity entity = new Entity();

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
    <h2 id="-value-members">🔑 Values</h2>
    <br> Manage dynamic key-value storage for the entity. Values can be of any type (structs or reference types) and are
         identified by integer keys. This allows flexible runtime data storage, reactive updates, and modular logic.

  </summary>

> [!IMPORTANT]
> Values in the entity are stored as a **key-value collection with integer keys**. Access, addition, update, and removal operations generally have **dictionary-like time complexity**. Values can be of any type, including structs and reference types, and multiple types can coexist under different keys. Note that adding a struct through the generic API avoids boxing.

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

This example demonstrates how to use **values** with `Entity`, including adding, retrieving, updating, and removing
values. Three approaches are shown: using **numeric keys** for performance, **string names** for readability, and **code
generation** for real projects. Subscriptions to `OnValueChanged` events are included to react to changes in real time.

---

#### 1️⃣ Using Numeric Keys

By default, all values use `int` keys because this avoids computing hash codes and is very fast; therefore, the example
below uses numeric keys as the default approach.

```csharp
// Create a new entity
IEntity entity = new Entity();

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
// Create a new entity
IEntity entity = new Entity();

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
// Create a new entity
IEntity entity = new Entity();

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
    <h2 id="-behaviour-members">⚙️ Behaviours</h2>
    <br>
    Manage modular logic attached to the entity. Behaviours implement 
    <a href="../Behaviours/IEntityBehaviour.md">IEntityBehaviour</a> interfaces and can be added, removed, queried, or enumerated at runtime. 
    This allows flexible composition of entity logic, enabling dynamic functionality without changing the core entity structure. 
    Behaviours can respond to lifecycle events (<code>Init</code>, <code>Enable</code>, <code>Tick</code>, <code>Disable</code>, <code>Dispose</code>), 
    enabling dynamic logic composition without changing the core entity structure.
  </summary>

> [!IMPORTANT]
> For behaviours entity acts as a container using a **List**, which means that all algorithmic operations have **List-like time complexity**. Additionally, the entity **can store multiple references to the same behaviour instance**, so duplicate entries are allowed.

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
- **Note:** Allows to add existing behaviours.

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

Below is an example of working with behaviours in `Entity`.

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

---

<details>
  <summary>
    <h2 id="-lifecycle-members">♻️ Lifecycle</h2>
    <br>
    Manage the entity's state transitions and update phases. It covers initialization, enabling,
    per-frame updates, disabling, and disposal. Lifecycle events allow reactive systems to respond to changes in the
    entity's state.
  </summary>

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

- **Description:**  Called during the disposal process of a `Entity`. Provides a hook for derived classes to execute
  custom cleanup logic when the entity is being disposed.
- **Notes:** This method is invoked by `Dispose()`

---

### 🗂 Example of Usage

This example demonstrates how to manage the lifecycle of an entity, including initialization, enabling, per-frame
updates, disabling, and disposal. Event subscriptions allow reacting to state changes in real time.

```csharp
// Create a new entity
IEntity player = new Entity();

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

## 🐞 Debug Properties

These properties are available only in **Unity Editor** when using **Odin Inspector**

- `DebugName` — Displays entity name in the Unity Editor.
- `DebugInitialized` — Displays if the entity is initialized.
- `DebugEnabled` — Displays if the entity is enabled.
- `DebugTags` — Sorted list of tags for debug display.
- `DebugValues` — Sorted list of values for debug display.
- `DebugBehaviours` — Sorted list of attached behaviours for debug display.

---

## 🗂 Example of Usage

### 🔹 Example #1: Creating and setting up

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

### 🔹 Example #2. Creating through constructor

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

### 🔹 Example #3. Lifecycle management

```csharp
// Create a new entity
var entity = new Entity("Enemy");

// Initialize the entity and attached behaviours
entity.Init();

// Enable the entity (registers update behaviours)
entity.Enable();

// Simulate game loop updates
float deltaTime = 0.016f; // Example: 60 FPS
float fixedDeltaTime = 0.016f; // Example: 60 FPS

entity.Tick(deltaTime);       // Calls Tick on all IEntityTick behaviours
entity.FixedTick(fixedDeltaTime);  // Calls FixedTick on all IEntityFixedTick behaviours
entity.LateTick(deltaTime);   // Calls LateTick on all IEntityLateTick behaviours

// Disable the entity (unregisters update behaviours)
entity.Disable();

// Dispose the entity when it is no longer needed
entity.Dispose();
```

---

## 🔥 Performance

//TODO:


---

## 📝 Notes

- **Event-Driven** – Reactive programming support via state change notifications.
- **Unique Identity** – Runtime-generated instance ID for entity tracking.
- **Tag System** – Lightweight categorization and filtering.
- **State Management** – Dynamic key-value storage for runtime data.
- **Behaviour Composition** – Attach or detach modular logic at runtime.
- **Lifecycle Control** – Built-in support for `Init`, `Enable`, `Tick`, `Disable`, and `Dispose` phases.
- **Registry Integration** – Automatic registration with EntityRegistry
- **Memory Efficient** – Pre-allocation support for collections
- **Odin Inspector Support** – Optional editor enhancements for configuration and debug.
- **Debug Support** – When used with Unity Editor and Odin Inspector, debug properties provide quick insight into the
  entity state, tags, values, and behaviours.
- **Thread Safety** – `Entity` is **NOT thread-safe**; all interactions should occur on the main thread or be
  synchronized externally.
- **Composition** – Behaviours, tags, and values can be added dynamically at runtime without modifying the core entity
  class.
