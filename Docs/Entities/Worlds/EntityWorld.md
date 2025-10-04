# 🧩 EntityWorld

```csharp
public class EntityWorld : EntityWorld<IEntity>, IEntityWorld
```

- **Description:** A non-generic shortcut for `EntityWorld<E>`.  
  Manages a world of general-purpose [IEntity](../Entities/IEntity.md) instances with support for enabling, updating,
  and disposing all contained entities.
- **Inheritance:** [EntityWorld\<E>](EntityWorld%601.md), [IEntityWorld](IEntityWorld.md).
- **Note:** Provides a convenient entry point for working with untyped or heterogeneous entities without requiring
  generic parameters.

---

## 🏗 Constructors

#### `EntityWorld()`

```csharp
public EntityWorld();
```

- **Description:** Initializes an empty `EntityWorld` instance with no name.

#### `EntityWorld(params IEntity[])`

```csharp
public EntityWorld(params IEntity[] entities);
```

- **Description:** Initializes a new `EntityWorld` instance with an empty name and prepopulates it with the specified
  entities.
- **Parameter:** `entities` — Array of entities to add to the world.

#### `EntityWorld(string, params IEntity[])`

```csharp
public EntityWorld(string name = null, params IEntity[] entities);
```

- **Description:** Initializes a new `EntityWorld` with the given name and prepopulates it with the specified entities.
- **Parameters:**
    - `name` — Optional name for the world.
    - `entities` — Array of entities to add to the world.

#### `EntityWorld(string, IEnumerable<IEntity>)`

```csharp
public EntityWorld(string name, IEnumerable<IEntity> entities);
```

- **Description:** Initializes a new `EntityWorld` with the specified name and populates it using an enumerable
  collection of entities.
- **Parameters:**
    - `name` — Name of the world.
    - `entities` — Collection of entities to add to the world.

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Raised when entities are added or removed.

#### `OnAdded`

```csharp
public event Action<IEntity> OnAdded;
```

- **Description:** Raised when an entity is added.
- **Parameter:** `entity` — The entity that was added.

#### `OnRemoved`

```csharp
public event Action<IEntity> OnRemoved;
```

- **Description:** Raised when an entity is removed.
- **Parameter:** `entity` — The entity that was removed.

#### `OnEnabled`

```csharp
public event Action OnEnabled;
```

- **Description:** Raised when the world is enabled.

#### `OnDisabled`

```csharp
public event Action OnDisabled;
```

- **Description:** Raised when the world is disabled.

#### `OnTicked`

```csharp
public event Action<float> OnTicked;
```

- **Description:** Raised every `Tick`.
- **Parameter:** `deltaTime` — Time in seconds since the last frame.

#### `OnFixedTicked`

```csharp
public event Action<float> OnFixedTicked;
```

- **Description:** Raised every `FixedTick`.
- **Parameter:** `deltaTime` — Fixed time step used by the physics engine.

#### `OnLateTicked`

```csharp
public event Action<float> OnLateTicked;
```

- **Description:** Raised every `LateTick`.
- **Parameter:** `deltaTime` — Time in seconds since the last frame.

---

## 🔑 Properties

#### `Name`

```csharp
public string Name { get; set; }
```

- **Description:** Gets or sets the name of the entity world.
- **Note:** Can be used for debugging, identification, or UI representation purposes.

#### `Enabled`

```csharp
public bool Enabled { get; }
```

- **Description:** Indicates whether the world is currently enabled.

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of entities currently in the world.

#### `IsReadOnly`

```csharp
public bool IsReadOnly { get; }
```

- **Description:** Indicates whether the collection is read-only.

---

## 🏹 Methods

#### `Enable()`

```csharp
public void Enable();
```

- **Description:** Enables the world and all contained entities.
- **Triggers:** `OnEnabled` event.

#### `Disable()`

```csharp
public void Disable();
```

- **Description:** Disables the world and all contained entities.
- **Triggers:** `OnDisabled` event.

#### `Tick(float)`

```csharp
public void Tick(float deltaTime);
```

- **Description:** Called once per frame during the `Tick` phase for all contained entities.
- **Parameter:** `deltaTime` — Time in seconds since the last frame.
- **Triggers:** `OnTicked` event.

#### `FixedTick(float)`

```csharp
public void FixedTick(float deltaTime);
```

- **Description:** Called during the `FixedTick` phase for all contained entities, typically for physics calculations.
- **Parameter:** `deltaTime` — Fixed time step used by the physics engine.
- **Triggers:** `OnFixedTicked` event.

#### `LateTick(float)`

```csharp
public void LateTick(float deltaTime);
```

- **Description:** Called during the `LateTick` phase for all contained entities, after all Tick calls.
- **Parameter:** `deltaTime` — Time in seconds since the last frame.
- **Triggers:** `OnLateTicked` event.

#### `Add(IEntity)`

```csharp
public bool Add(IEntity entity);
```

- **Description:** Adds an entity to the world.
- **Parameter:** `entity` — The entity to add.
- **Returns:** `true` if the entity was successfully added; `false` if it already exists.

#### `Remove(IEntity)`

```csharp
public bool Remove(IEntity entity);
```

- **Description:** Removes a specific entity from the world.
- **Parameter:** `entity` — The entity to remove.
- **Returns:** `true` if the entity was removed; otherwise `false`.

#### `Contains(IEntity)`

```csharp
public bool Contains(IEntity entity);
```

- **Description:** Checks whether the specified entity exists in the world.
- **Parameter:** `entity` — The entity to check.
- **Returns:** `true` if the entity exists; otherwise `false`.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all entities from the world.
- **Events:** Triggers multiple `OnRemoved` events (one per entity) and `OnStateChanged`.

#### `CopyTo(IEntity[], int)`

```csharp
public void CopyTo(IEntity[] array, int arrayIndex);
```

- **Description:** Copies all entities into the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The zero-based index at which copying begins.

#### `CopyTo(ICollection<IEntity>)`

```csharp
public void CopyTo(ICollection<IEntity> results);
```

- **Description:** Copies all entities into the provided collection.
- **Parameter:** `results` — The target collection.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Disposes the world and its entities.
- **Remarks:** Unsubscribes all event handlers and clears the collection.

#### `OnAdd(IEntity)`

```csharp
protected virtual void OnAdd(IEntity entity);
```

- **Description:** Enables entity automatically when an entity is **added** to the collection.
- **Parameter:** `entity` — The entity that was added.
- **Remarks:** Can be **overridden** in derived classes to implement custom logic, such as enabling the entity, logging,
  or triggering events.
- **Default behavior:** Does nothing.

#### `OnRemove(IEntity)`

```csharp
protected virtual void OnRemove(IEntity entity);
```

- **Description:** Disables entity automatically when an entity is **removed** from the collection.
- **Parameter:** `entity` — The entity that was removed.
- **Remarks:** Can be **overridden** in derived classes to implement custom logic, such as disabling the entity,
  logging, or triggering events.
- **Default behavior:** Does nothing.

---

## 🧩 Enumerator

```csharp
public struct Enumerator : IEnumerator<E>
```

- **Description:** Struct-based enumerator for iterating over `EntityWorld` without heap allocations.
- **Properties:** `Current` — The current entity.
- **Methods:** `MoveNext()`, `Reset()`, `Dispose()`.

---

## 🗂 Example of Usage

```csharp
EntityWorld world = new EntityWorld("GeneralWorld");

// Subscribe to events
world.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
world.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
world.OnStateChanged += () => Console.WriteLine("World state changed");
world.OnEnabled += () => Console.WriteLine("World enabled");
world.OnDisabled += () => Console.WriteLine("World disabled");
world.OnTicked += deltaTime => Console.WriteLine($"Ticked: {deltaTime} seconds");
world.OnFixedTicked += deltaTime => Console.WriteLine($"FixedTicked: {deltaTime} seconds");
world.OnLateTicked += deltaTime => Console.WriteLine($"LateTicked: {deltaTime} seconds");

// Enable the world
world.Enable();

// Add entities
var entity1 = new Entity("Entity1");
var entity2 = new Entity("Entity2");
world.Add(entity1);
world.Add(entity2);

// Check existence
if (world.Contains(entity1))
{
    Console.WriteLine($"{entity1.Name} exists in the world");
}

// Tick the world (simulate frame updates)
world.Tick(0.016f);       // Regular update
world.FixedTick(0.02f);   // Fixed update (physics)
world.LateTick(0.016f);   // Late update

// Remove an entity
world.Remove(entity1);

// Copy entities to an array
var array = new IEntity[world.Count];
world.CopyTo(array, 0);

// Iterate over entities
foreach (var entity in world)
{
    Console.WriteLine(entity.Name);
}

// Disable the world
world.Disable();

// Dispose when done
world.Dispose();
```