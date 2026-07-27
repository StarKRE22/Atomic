# 🧩 PriorityEntitySystem

**PriorityEntitySystem\<E\>** is an abstract system that divides its per-frame update budget among entities based on
their priority. High-priority entities are updated more frequently than low-priority ones.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Settings](#settings)
  - [Constructor](#constructor)
  - [Methods](#-methods)
    - [Update(E, float)](#updatee-float)
    - [EvaluatePriority(E)](#evaluateprioritye)
    - [ChangePriority(E)](#changeprioritye)
    - [RecalculatePriorities()](#recalculatepriorities)
    - [Dispose()](#dispose)
- [Examples of Usage](#-examples-of-usage)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`PriorityEntitySystem<E>` extends [EntitySystemBase\<E\>](EntitySystemBase.md). It places each tracked entity into one
of three buckets based on [EntityUpdatePriority](EntityUpdatePriority.md):

- **High**
- **Medium**
- **Low**

During `Update`, the system allocates its batch budget according to configurable percentages:

- `highPercent` — share of the budget for high-priority entities
- `midPercent` — share of the budget for medium-priority entities
- `lowPercent` — remaining share for low-priority entities

If a bucket has fewer entities than its quota, the leftover budget rolls over to the next bucket.

Priorities can be recalculated periodically using a `cooldown` interval, or on demand via `ChangePriority`.

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public abstract class PriorityEntitySystem<E> : EntitySystemBase<E>, IDisposable where E : IEntity
```

- **Type Parameter:** `E` — The entity type managed by the system. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [EntitySystemBase\<E\>](EntitySystemBase.md), `IDisposable`

---

### 🛠️ Settings

```csharp
[Serializable]
public new class Settings : EntitySystemBase<E>.Settings
```

| Field | Description |
|-------|-------------|
| `cooldown` | Interval in seconds between automatic priority recalculations. Default `0.25f`. |
| `highPercent` | Percentage of the batch budget for high-priority entities. Default `70`. |
| `midPercent` | Percentage of the batch budget for medium-priority entities. Default `20`. |
| `lowPercent` | Computed as `100 - highPercent - midPercent`. |

**Validation:** `highPercent` and `midPercent` are clamped so their sum does not exceed 100.

---

### 🏗️ Constructor

#### `PriorityEntitySystem(IReadOnlyEntityCollection<E>, Settings, params IEntityTrigger<E>[])`

```csharp
protected PriorityEntitySystem(
    IReadOnlyEntityCollection<E> source,
    Settings settings,
    params IEntityTrigger<E>[] triggers
) : base(source, settings);
```

- **Description:** Initializes the priority system with the specified collection, settings, and optional triggers.
- **Parameter:** `source` — The collection of entities to process.
- **Parameter:** `settings` — Priority system settings.
- **Parameter:** `triggers` — Optional triggers that signal when an entity should be re-evaluated.

---

### 🏹 Methods

#### `Update(E, float)`

```csharp
protected abstract void Update(E entity, float deltaTime);
```

- **Description:** Called for each entity selected for update this frame.
- **Parameter:** `entity` — The entity to update.
- **Parameter:** `deltaTime` — Time elapsed since the last update.

#### `EvaluatePriority(E)`

```csharp
protected abstract EntityUpdatePriority EvaluatePriority(E entity);
```

- **Description:** Determines the priority of the given entity.
- **Parameter:** `entity` — The entity to evaluate.
- **Returns:** The [EntityUpdatePriority](EntityUpdatePriority.md) for the entity.
- **Note:** Must be implemented in derived classes.

#### `ChangePriority(E)`

```csharp
protected void ChangePriority(E entity);
```

- **Description:** Re-evaluates and updates the priority of the specified entity.
- **Parameter:** `entity` — The entity whose priority should be re-evaluated.
- **Note:** Safe to call during `Update`; changes are buffered and applied at the end of the frame.

#### `RecalculatePriorities()`

```csharp
protected void RecalculatePriorities();
```

- **Description:** Re-evaluates priorities for all entities in the source collection.
- **Note:** Called automatically when `cooldown` expires.

#### `Dispose()`

```csharp
public override void Dispose();
```

- **Description:** Clears all priority buckets and internal state.

---

## 🗂 Examples of Usage

### Distance-Based Priority System

```csharp
public sealed class AIPrioritySystem : PriorityEntitySystem<IGameEntity>
{
    public AIPrioritySystem(
        IReadOnlyEntityCollection<IGameEntity> source,
        Settings settings,
        params IEntityTrigger<IGameEntity>[] triggers
    ) : base(source, settings, triggers)
    {
    }

    protected override EntityUpdatePriority EvaluatePriority(IGameEntity entity)
    {
        float distance = entity.GetDistanceToPlayer();

        if (distance < 10f)
            return EntityUpdatePriority.High;
        if (distance < 40f)
            return EntityUpdatePriority.Medium;

        return EntityUpdatePriority.Low;
    }

    protected override void Update(IGameEntity entity, float deltaTime)
    {
        entity.GetValue<AIBehaviour>("ai").Update(deltaTime);
    }
}
```

### Using a Trigger to Recalculate on Value Change

```csharp
var valueTrigger = new ValueEntityTrigger<IGameEntity>("distanceToPlayer");
var system = new AIPrioritySystem(world, settings, valueTrigger);
contextEntity.AddTickSystem(system);
```

---

## 📌 Best Practices

- Keep `EvaluatePriority` fast — it runs for every entity on every `RecalculatePriorities` call.
- Use triggers to re-evaluate priorities only when relevant state changes.
- Tune `highPercent` / `midPercent` based on how many entities fall into each category.
- Set `cooldown` low enough to react to changes, but high enough to avoid frequent re-sorting.
- For equal-priority processing, use [EntitySystem\<E\>](EntitySystem.md).
