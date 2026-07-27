# 🧩 EntitySystem

**EntitySystem\<E\>** is a concrete base class for systems that update entities in a simple round-robin fashion.
It tracks entities in an internal array and processes a configurable number of them each frame.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructor](#constructor)
  - [Methods](#-methods)
    - [Update(E, float)](#updatee-float)
    - [Dispose()](#dispose)
- [Examples of Usage](#-examples-of-usage)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`EntitySystem<E>` extends [EntitySystemBase\<E\>](EntitySystemBase.md). It maintains an internal array of entities
and processes them sequentially using a cursor. Each frame it updates up to `batchSize` entities, wrapping around to
the beginning when it reaches the end.

This approach guarantees that every entity is updated regularly, even when there are too many entities to process in a
single frame.

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public abstract class EntitySystem<E> : EntitySystemBase<E>, IDisposable where E : IEntity
```

- **Type Parameter:** `E` — The entity type managed by the system. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [EntitySystemBase\<E\>](EntitySystemBase.md), `IDisposable`

---

### 🏗️ Constructor

#### `EntitySystem(IReadOnlyEntityCollection<E>, Settings)`

```csharp
protected EntitySystem(IReadOnlyEntityCollection<E> source, Settings settings);
```

- **Description:** Initializes the system with the specified entity collection and settings.
- **Parameter:** `source` — The collection of entities to process.
- **Parameter:** `settings` — System settings including frame budget and adaptive batching.

---

### 🏹 Methods

#### `Update(E, float)`

```csharp
protected abstract void Update(E entity, float deltaTime);
```

- **Description:** Called for each entity that should be updated this frame.
- **Parameter:** `entity` — The entity to update.
- **Parameter:** `deltaTime` — Time elapsed since the last update.
- **Note:** Must be implemented in derived classes.

#### `Dispose()`

```csharp
public override void Dispose();
```

- **Description:** Clears the internal entity array and lookup.

---

## 🗂 Examples of Usage

### Simple Tick System

```csharp
public sealed class MovementSystem : EntitySystem<IGameEntity>
{
    public MovementSystem(IReadOnlyEntityCollection<IGameEntity> source, Settings settings)
        : base(source, settings)
    {
    }

    protected override void Update(IGameEntity entity, float deltaTime)
    {
        Vector3 velocity = entity.GetValue<Vector3>("velocity");
        entity.SetValue("position", entity.GetValue<Vector3>("position") + velocity * deltaTime);
    }
}
```

### Wiring to a Context

```csharp
IEntityWorld<IGameEntity> world = ...;
var settings = new EntitySystem<IGameEntity>.Settings
{
    frameBudget = 0.02f
};

MovementSystem movement = new MovementSystem(world, settings);
contextEntity.AddTickSystem(movement);
```

---

## 📌 Best Practices

- Use `EntitySystem<E>` when all entities should be treated equally.
- Keep `Update` logic lightweight to allow the adaptive batching to scale.
- Use [PriorityEntitySystem\<E\>](PriorityEntitySystem.md) when entities need different update frequencies.
- Dispose systems when the owning context is destroyed.
