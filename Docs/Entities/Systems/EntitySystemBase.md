# 🧩 EntitySystemBase

**EntitySystemBase\<E\>** is the abstract base class for all entity systems in the Atomic framework. It manages
subscription to an entity collection, tracks enabled/disabled state, measures frame time, and provides adaptive batch
sizing.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Settings](#settings)
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
- [Examples of Usage](#-examples-of-usage)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

Entity systems process a collection of entities every frame. `EntitySystemBase<E>` provides the shared infrastructure:

- Subscribes to `OnAdded` / `OnRemoved` events of an [IReadOnlyEntityCollection\<E\>](../Collections/IReadOnlyEntityCollection%601.md)
- Tracks whether the system is enabled
- Measures update time and adjusts the batch size to stay within a frame budget
- Calls virtual lifecycle hooks that derived classes can override

Derived classes implement the actual update logic:

- [EntitySystem\<E\>](EntitySystem.md) — processes entities in a round-robin fashion
- [PriorityEntitySystem\<E\>](PriorityEntitySystem.md) — processes entities by priority level

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntitySystemBase<E> : IDisposable where E : IEntity
```

- **Type Parameter:** `E` — The entity type managed by the system. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `IDisposable`

---

### 🛠️ Settings

```csharp
[Serializable]
public class Settings
```

| Field | Description |
|-------|-------------|
| `frameBudget` | Maximum time in seconds the system should spend per update. Default `0.03f`. |
| `batching` | Adaptive batching parameters. |

#### AdaptiveBatching

```csharp
[Serializable]
public sealed class AdaptiveBatching
```

| Field | Description |
|-------|-------------|
| `minSize` | Minimum batch size. Default `1024`. |
| `maxSize` | Maximum batch size. Default `2048`. |
| `scaleDown` | Factor by which the batch size is reduced when over budget. Default `2`. |
| `stepUp` | Amount by which the batch size is increased when under budget. Default `256`. |

The system adjusts `_batchSize` every frame:

- If the frame time exceeds `frameBudget`, batch size is divided by `scaleDown` (but not below `minSize`).
- If the frame time is under budget, batch size is increased by `stepUp` (but not above `maxSize`).

---

### 🏹 Methods

#### `Enable()`

```csharp
public void Enable();
```

- **Description:** Enables the system and subscribes to entity collection changes.
- **Behavior:**
  - Calls `OnAddEntity` for every entity currently in the source collection.
  - Subscribes to `source.OnAdded` and `source.OnRemoved`.
  - Calls the virtual `OnEnable()` hook.

#### `Disable()`

```csharp
public void Disable();
```

- **Description:** Disables the system and unsubscribes from entity collection changes.
- **Behavior:**
  - Calls the virtual `OnDisable()` hook.
  - Unsubscribes from `source.OnAdded` and `source.OnRemoved`.
  - Calls `OnRemoveEntity` for every entity currently in the source collection.

#### `Update(float)`

```csharp
public void Update(float deltaTime);
```

- **Description:** Updates the system if enabled.
- **Parameter:** `deltaTime` — Time elapsed since the last update.
- **Behavior:**
  - Measures the time spent in `OnUpdate`.
  - Adjusts `_batchSize` adaptively based on `frameBudget`.

#### `OnEnable()`

```csharp
protected virtual void OnEnable();
```

- **Description:** Virtual hook called when the system is enabled.
- **Note:** Override to perform custom initialization.

#### `OnDisable()`

```csharp
protected virtual void OnDisable();
```

- **Description:** Virtual hook called when the system is disabled.
- **Note:** Override to perform custom cleanup.

#### `OnAddEntity(E)`

```csharp
protected virtual void OnAddEntity(E entity);
```

- **Description:** Virtual hook called when an entity is added to the source collection.
- **Parameter:** `entity` — The entity being added.

#### `OnRemoveEntity(E)`

```csharp
protected virtual void OnRemoveEntity(E entity);
```

- **Description:** Virtual hook called when an entity is removed from the source collection.
- **Parameter:** `entity` — The entity being removed.

#### `OnUpdate(int, float)`

```csharp
protected abstract void OnUpdate(int batchSize, float deltaTime);
```

- **Description:** Abstract method that performs the actual update logic.
- **Parameter:** `batchSize` — Maximum number of entities to process this frame.
- **Parameter:** `deltaTime` — Time elapsed since the last update.

#### `Dispose()`

```csharp
public abstract void Dispose();
```

- **Description:** Releases resources held by the system.

---

## 🗂 Examples of Usage

### Hooking a System to an Entity Context

```csharp
IEntityWorld<IGameEntity> world = ...;
MyEntitySystem system = new MyEntitySystem(world, settings);

// Wire lifecycle to a context entity
contextEntity.AddTickSystem(contextEntity, system);

// Now system.Enable() is called on enable, system.Update() on tick, etc.
```

### Custom System Base

```csharp
public abstract class MyEntitySystemBase : EntitySystemBase<IGameEntity>
{
    protected MyEntitySystemBase(IReadOnlyEntityCollection<IGameEntity> source, Settings settings)
        : base(source, settings)
    {
    }

    protected override void OnEnable()
    {
        Debug.Log("System enabled");
    }

    protected override void OnDisable()
    {
        Debug.Log("System disabled");
    }
}
```

---

## 📌 Best Practices

- Enable systems only when the owning context is active.
- Disable systems before disposing to ensure proper cleanup.
- Tune `frameBudget` and `batching` settings to match your target platform.
- Keep `OnUpdate` implementations fast to avoid starving the adaptive batching.
- For priority-based processing, use [PriorityEntitySystem\<E\>](PriorityEntitySystem.md).
- For simple round-robin processing, use [EntitySystem\<E\>](EntitySystem.md).
