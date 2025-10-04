# 🧩 IEntityWorld\<E>

```csharp
public interface IEntityWorld<E> : IEntityCollection<E>, IEnableLifecycle, ITickLifecycle 
    where E : IEntity
```

- **Description:** Represents a **world that manages a collection of entities** and controls their lifecycle events.  
  Provides functionality for entity management, lifecycle enable/disable, and per-frame update callbacks.
- **Type Parameter:** `E` — The type of entity managed by this world. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityCollection\<E>](../Collections/IEntityCollection%601.md),
  [IEnableLifecycle](../Lifecycle/Sources/IEnableLifecycle.md),
  [ITickLifecycle](../Lifecycle/Sources/ITickLifecycle.md).
- **Note:** It is ideal for managing a **domain of entities** in Unity, supporting reactive systems, UI integration, and
  structured update loops.
- **See also:** [EntityWorld\<E>](EntityWorld%601.md), [IEntityWorld](IEntityWorld.md), [EntityWorld](EntityWorld.md)

---

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Raised when entities are added or removed.

#### `OnAdded`

```csharp
public event Action<E> OnAdded;
```

- **Description:** Raised when an entity is added.
- **Parameter:** `entity` — The entity that was added.

#### `OnRemoved`

```csharp
public event Action<E> OnRemoved;
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

- **Description:** Enables the world and all contained entities,
- **Triggers:** `OnEnabled` event.

#### `Disable()`

```csharp
public void Disable();
```

- **Description:** Disables the world and all contained entities
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

#### `Add(E)`

```csharp
public bool Add(E entity);
```

- **Description:** Adds an entity to the world.
- **Parameter:** `entity` — The entity to add.
- **Returns:** `true` if the entity was successfully added; `false` if it already exists.

#### `Remove(E)`

```csharp
public bool Remove(E entity);
```

- **Description:** Removes a specific entity from the world.
- **Parameter:** `entity` — The entity to remove.
- **Returns:** `true` if the entity was removed; otherwise `false`.

#### `Contains(E)`

```csharp
public bool Contains(E entity);
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

#### `CopyTo(E[], int)`

```csharp
public void CopyTo(E[] array, int arrayIndex);
```

- **Description:** Copies all entities into the specified array starting at the given index.
- **Parameters:**
    - `array` — The destination array.
    - `arrayIndex` — The zero-based index at which copying begins.

#### `CopyTo(ICollection<E>)`

```csharp
public void CopyTo(ICollection<E> results);
```

- **Description:** Copies all entities into the provided collection.
- **Parameter:** `results` — The target collection.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Disposes the world and its entities.
- **Remarks:** Unsubscribes all event handlers and clears the collection.

---

## 🗂 Example of Usage

```csharp
// Assume we have an IEntityWorld instance
IEntityWorld<GameEntity> world = ...

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
var entity1 = new GameEntity("Entity1");
var entity2 = new GameEntity("Entity2");
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
var array = new GameEntity[world.Count];
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

---

## 📝 Notes

- **Entity management:** Add, remove, query, and enumerate entities.
- **Lifecycle management:** Enable / disable world and entities dynamically.
- **Per-frame updates:** Regular, fixed, and late update callbacks.