# 🧩 IEntityWorld

Non-generic version of [IEntityWorld\<E>](IEntityWorld%601.md) specialized for [IEntity](../Entities/IEntity.md). Useful for scenarios where
managing a collection of heterogeneous entities is sufficient.

---

## 📑 Table of Contents

<ul>
  <li><a href="#-example-of-usage">Example of Usage</a></li>
  <li>
    <a href="#-api-reference">API Reference</a>
    <ul>
      <li><a href="#-type">Type</a></li>
      <li>
        <details>
          <summary><a href="#-events">Events</a></summary>
          <ul>
            <li><a href="#onstatechanged">OnStateChanged</a></li>
            <li><a href="#onadded">OnAdded</a></li>
            <li><a href="#onremoved">OnRemoved</a></li>
            <li><a href="#onenabled">OnEnabled</a></li>
            <li><a href="#ondisabled">OnDisabled</a></li>
            <li><a href="#onticked">OnTicked</a></li>
            <li><a href="#onfixedticked">OnFixedTicked</a></li>
            <li><a href="#onlateticked">OnLateTicked</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-properties">Properties</a></summary>
          <ul>
            <li><a href="#name">Name</a></li>
            <li><a href="#enabled">Enabled</a></li>
            <li><a href="#count">Count</a></li>
            <li><a href="#isreadonly">IsReadOnly</a></li>
          </ul>
        </details>
      </li>
      <li>
        <details>
          <summary><a href="#-methods">Methods</a></summary>
          <ul>
            <li><a href="#enable">Enable()</a></li>
            <li><a href="#disable">Disable()</a></li>
            <li><a href="#tickfloat">Tick(float)</a></li>
            <li><a href="#fixedtickfloat">FixedTick(float)</a></li>
            <li><a href="#latetickfloat">LateTick(float)</a></li>
            <li><a href="#addientity">Add(IEntity)</a></li>
            <li><a href="#removeientity">Remove(IEntity)</a></li>
            <li><a href="#containsientity">Contains(IEntity)</a></li>
            <li><a href="#clear">Clear()</a></li>
            <li><a href="#copytoientity-int">CopyTo(IEntity[], int)</a></li>
            <li><a href="#copytoicollectionientity">CopyTo(ICollection&lt;IEntity&gt;)</a></li>
            <li><a href="#dispose">Dispose()</a></li>
          </ul>
        </details>
      </li>
    </ul>
  </li>
  <li><a href="#-notes">Notes</a></li>
</ul>

<!--

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Events](#-events)
    - [OnStateChanged](#onstatechanged)
    - [OnAdded](#onadded)
    - [OnRemoved](#onremoved)
    - [OnEnabled](#onenabled)
    - [OnDisabled](#ondisabled)
    - [OnTicked](#onticked)
    - [OnFixedTicked](#onfixedticked)
    - [OnLateTicked](#onlateticked)
  - [Properties](#-properties)
    - [Name](#name)
    - [Enabled](#enabled)
    - [Count](#count)
    - [IsReadOnly](#isreadonly)
  - [Methods](#-methods)
    - [Enable()](#enable)
    - [Disable()](#disable)
    - [Tick(float)](#tickfloat)
    - [FixedTick(float)](#fixedtickfloat)
    - [LateTick(float)](#latetickfloat)
    - [Add(IEntity)](#addientity)
    - [Remove(IEntity)](#removeientity)
    - [Contains(IEntity)](#containsientity)
    - [Clear()](#clear)
    - [CopyTo(IEntity[], int)](#copytoientity-int)
    - [CopyTo(ICollection<IEntity>)](#copytoicollectionientity)
    - [Dispose()](#dispose)
- [Notes](#-notes)

-->

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

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityWorld : IEntityWorld<IEntity>
```

- **Inheritance:** [IEntityWorld\<E>](IEntityWorld%601.md).
- **See also:** [EntityWorld](EntityWorld.md)

---

### ⚡ Events

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

### 🔑 Properties

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

### 🏹 Methods

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

## 📝 Notes

- **Entity management:** Add, remove, query, and enumerate entities.
- **Lifecycle management:** Enable / disable world and entities dynamically.
- **Per-frame updates:** Regular, fixed, and late update callbacks.