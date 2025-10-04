# 🧩 IEntityWorld

```csharp
public interface IEntityWorld : IEntityWorld<IEntity>
```

- **Description:** Non-generic version of `IEntityWorld<E>` specialized for [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityWorld\<E>](IEntityWorld%601.md).
- **Note:** Useful for scenarios where managing a collection of heterogeneous entities is sufficient.
- **See also:** [EntityWorld](EntityWorld.md)

---

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

---

## 🗂 Example of Usage

```csharp
//Assume we have an IEntityWorld instance
IEntityWorld world = ...

// Subscribe to events
world.OnAdded += e => Console.WriteLine($"Added entity: {e.Name}");
world.OnRemoved += e => Console.WriteLine($"Removed entity: {e.Name}");
world.OnStateChanged += () => Console.WriteLine("World state changed");
world.OnEnabled += () => Console.WriteLine("World enabled");
world.OnDisabled += () => Console.WriteLine("World disabled");
world.OnTicked += dt => Console.WriteLine($"Ticked: {dt} seconds");

// Enable the world
world.Enable();

// Add entities
var entity1 = new Entity("Entity1");
var entity2 = new Entity("Entity2");
world.Add(entity1);
world.Add(entity2);

// Tick the world
world.Tick(0.016f);

// Remove an entity
world.Remove(entity1);

// Dispose when done
world.Dispose();
```

---

## 📝 Notes

- **Entity management:** Add, remove, query, and enumerate entities.
- **Lifecycle management:** Enable / disable world and entities dynamically.
- **Per-frame updates:** Regular, fixed, and late update callbacks.