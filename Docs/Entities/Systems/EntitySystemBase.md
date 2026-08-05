# 🧩 EntitySystemBase

Abstract base class for all entity systems. It manages subscription to an entity collection, tracks enabled/disabled state, measures frame time, and provides adaptive batch sizing.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [EntitySystemBase(IReadOnlyEntityCollection&lt;E&gt;, Settings)](#entitysystembaseireadonlyentitycollectione-settings)
  - [Methods](#-methods)
    - [Enable()](#enable)
    - [Disable()](#disable)
    - [Update(float)](#updatefloat)
    - [OnEnable()](#onenable)
    - [OnDisable()](#ondisable)
    - [OnAddEntity(E)](#onaddentitye)
    - [OnRemoveEntity(E)](#onremoveentitye)
    - [OnUpdate(int, float)](#onupdateint-float)
    - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
public sealed class MySystem : EntitySystemBase<IGameEntity>
{
    public MySystem(
        IReadOnlyEntityCollection<IGameEntity> source,
        Settings settings
    ) : base(source, settings) { }

    protected override void OnEnable() => Debug.Log("System enabled");
    protected override void OnDisable() => Debug.Log("System disabled");
    protected override void OnUpdate(int batchSize, float deltaTime) { }
    public override void Dispose() { }
}

// Wire to a context entity
IEntityWorld<IGameEntity> world = ...;
var system = new MySystem(world, new EntitySystemBase<IGameEntity>.Settings
{
    frameBudget = 0.02f
});

IEntity gameContext = ...;
gameContext.AddTickSystem(system);
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public abstract class EntitySystemBase<E> : IDisposable where E : IEntity
```

- **Description:** Abstract base class for all entity systems. Manages subscription to an entity collection, tracks enabled/disabled state, measures frame time, and provides adaptive batch sizing.
- **Inheritance:** `IDisposable`
- **Type Parameters:** `E` — The entity type managed by the system. Must implement [IEntity](../Entities/IEntity.md).
- **Notes:**
  - The nested `Settings` class controls the per-frame budget and adaptive batching.
  - The nested `AdaptiveBatching` class defines how the batch size shrinks or grows based on measured frame time.
  - `Update` measures the time spent in `OnUpdate` and adjusts `_batchSize` every frame.

  | `Settings` field | Default | Description |
  |------------------|---------|-------------|
  | `frameBudget` | `0.03f` | Maximum time in seconds the system should spend per update. |
  | `batching` | `new AdaptiveBatching()` | Adaptive batching parameters. |

  | `AdaptiveBatching` field | Default | Description |
  |--------------------------|---------|-------------|
  | `minSize` | `1024` | Minimum batch size. |
  | `maxSize` | `2048` | Maximum batch size. |
  | `scaleDown` | `2` | Factor by which the batch size is reduced when over budget. |
  | `stepUp` | `256` | Amount by which the batch size is increased when under budget. |

- **See also:** [EntitySystem<E>](EntitySystem.md), [PriorityEntitySystem<E>](PriorityEntitySystem.md),
  [Systems Extensions](Extensions.md), [IReadOnlyEntityCollection<E>](../Collections/IReadOnlyEntityCollection%601.md)

### 🏗️ Constructors

#### `EntitySystemBase(IReadOnlyEntityCollection<E>, Settings)`

```csharp
protected EntitySystemBase(IReadOnlyEntityCollection<E> source, Settings settings)
```

- **Description:** Initializes the system with the specified entity collection and settings.
- **Parameters:**
  - `source` — The collection of entities to process.
  - `settings` — System settings including frame budget and adaptive batching.
- **Throws:** `ArgumentNullException` if `source` or `settings` is `null`.

### 🏹 Methods

#### `Enable()`

```csharp
public void Enable()
```

- **Description:** Enables the system and subscribes to entity collection changes.
- **Behavior:**
  - Calls `OnAddEntity` for every entity currently in the source collection.
  - Subscribes to `source.OnAdded` and `source.OnRemoved`.
  - Calls the virtual `OnEnable()` hook.

#### `Disable()`

```csharp
public void Disable()
```

- **Description:** Disables the system and unsubscribes from entity collection changes.
- **Behavior:**
  - Calls the virtual `OnDisable()` hook.
  - Unsubscribes from `source.OnAdded` and `source.OnRemoved`.
  - Calls `OnRemoveEntity` for every entity currently in the source collection.

#### `Update(float)`

```csharp
public void Update(float deltaTime)
```

- **Description:** Updates the system if it is enabled.
- **Parameter:** `deltaTime` — Time elapsed since the last update.
- **Behavior:**
  - Measures the time spent in `OnUpdate`.
  - Adjusts `_batchSize` adaptively based on `frameBudget`.

#### `OnEnable()`

```csharp
protected virtual void OnEnable()
```

- **Description:** Virtual hook called when the system is enabled.
- **Remarks:** Override to perform custom initialization.

#### `OnDisable()`

```csharp
protected virtual void OnDisable()
```

- **Description:** Virtual hook called when the system is disabled.
- **Remarks:** Override to perform custom cleanup.

#### `OnAddEntity(E)`

```csharp
protected virtual void OnAddEntity(E entity)
```

- **Description:** Virtual hook called when an entity is added to the source collection.
- **Parameter:** `entity` — The entity being added.

#### `OnRemoveEntity(E)`

```csharp
protected virtual void OnRemoveEntity(E entity)
```

- **Description:** Virtual hook called when an entity is removed from the source collection.
- **Parameter:** `entity` — The entity being removed.

#### `OnUpdate(int, float)`

```csharp
protected abstract void OnUpdate(int batchSize, float deltaTime)
```

- **Description:** Abstract method that performs the actual update logic.
- **Parameters:**
  - `batchSize` — Maximum number of entities to process this frame.
  - `deltaTime` — Time elapsed since the last update.

#### `Dispose()`

```csharp
public abstract void Dispose()
```

- **Description:** Releases resources held by the system.
- **Remarks:** Must be implemented by derived classes.
