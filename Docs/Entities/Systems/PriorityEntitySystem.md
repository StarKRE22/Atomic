# 🧩 PriorityEntitySystem

Abstract system that divides its per-frame update budget among entities based on their priority. High-priority entities are updated more frequently than low-priority ones.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [PriorityEntitySystem(IReadOnlyEntityCollection&lt;E&gt;, Settings, params IEntityTrigger&lt;E&gt;[])](#priorityentitysystemireadonlyentitycollectione-settings-params-ientitytriggere)
  - [Methods](#-methods)
    - [Update(E, float)](#updatee-float)
    - [EvaluatePriority(E)](#evaluateprioritye)
    - [ChangePriority(E)](#changeprioritye)
    - [RecalculatePriorities()](#recalculatepriorities)
    - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
public sealed class AIPrioritySystem : PriorityEntitySystem<IGameEntity>
{
    public AIPrioritySystem(
        IReadOnlyEntityCollection<IGameEntity> source,
        Settings settings,
        params IEntityTrigger<IGameEntity>[] triggers
    ) : base(source, settings, triggers) { }

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

// Use a value change trigger to re-evaluate priorities when relevant state changes
var trigger = new ValueEntityTrigger<IGameEntity>();
var system = new AIPrioritySystem(world, settings, trigger);
contextEntity.AddTickSystem(system);
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[Serializable]
public abstract class PriorityEntitySystem<E> : EntitySystemBase<E>, IDisposable where E : IEntity
```

- **Description:** Abstract system that divides its per-frame update budget among entities based on their priority.
- **Inheritance:** [EntitySystemBase<E>](EntitySystemBase.md), `IDisposable`
- **Type Parameters:** `E` — The entity type managed by the system. Must implement [IEntity](../Entities/IEntity.md).
- **Notes:**
  - Entities are placed into `High`, `Medium`, or `Low` buckets based on [EntityUpdatePriority](EntityUpdatePriority.md).
  - Leftover budget from a partially empty bucket rolls over to the next bucket.
  - The nested `Settings` class adds priority-specific fields on top of [EntitySystemBase<E>.Settings](EntitySystemBase.md).

  | `Settings` field | Default | Description |
  |------------------|---------|-------------|
  | `cooldown` | `0.25f` | Interval in seconds between automatic priority recalculations. |
  | `highPercent` | `70` | Percentage of the batch budget for high-priority entities. |
  | `midPercent` | `20` | Percentage of the batch budget for medium-priority entities. |
  | `lowPercent` | `100 - highPercent - midPercent` | Remaining budget share for low-priority entities. |

  `highPercent` and `midPercent` are clamped so their sum does not exceed `100`.

- **See also:** [EntityUpdatePriority](EntityUpdatePriority.md), [EntitySystemBase<E>](EntitySystemBase.md), [EntitySystem<E>](EntitySystem.md), [IEntityTrigger<E>](../Filters/IEntityTrigger%601.md)

### 🏗️ Constructors

#### `PriorityEntitySystem(IReadOnlyEntityCollection<E>, Settings, params IEntityTrigger<E>[])`

```csharp
protected PriorityEntitySystem(
    IReadOnlyEntityCollection<E> source,
    Settings settings,
    params IEntityTrigger<E>[] triggers
) : base(source, settings)
```

- **Description:** Initializes the priority system with the specified collection, settings, and optional triggers.
- **Parameters:**
  - `source` — The collection of entities to process.
  - `settings` — Priority system settings.
  - `triggers` — Optional triggers that signal when an entity should be re-evaluated.

### 🏹 Methods

#### `Update(E, float)`

```csharp
protected abstract void Update(E entity, float deltaTime)
```

- **Description:** Called for each entity selected for update this frame.
- **Parameters:**
  - `entity` — The entity to update.
  - `deltaTime` — Time elapsed since the last update.

#### `EvaluatePriority(E)`

```csharp
protected abstract EntityUpdatePriority EvaluatePriority(E entity)
```

- **Description:** Determines the priority of the given entity.
- **Parameter:** `entity` — The entity to evaluate.
- **Returns:** The [EntityUpdatePriority](EntityUpdatePriority.md) for the entity.
- **Remarks:** Must be implemented in derived classes.

#### `ChangePriority(E)`

```csharp
protected void ChangePriority(E entity)
```

- **Description:** Re-evaluates and updates the priority of the specified entity.
- **Parameter:** `entity` — The entity whose priority should be re-evaluated.
- **Remarks:** Safe to call during `Update`; changes are buffered and applied at the end of the frame.

#### `RecalculatePriorities()`

```csharp
protected void RecalculatePriorities()
```

- **Description:** Re-evaluates priorities for all entities in the source collection.
- **Remarks:** Called automatically when `cooldown` expires.

#### `Dispose()`

```csharp
public override void Dispose()
```

- **Description:** Clears all priority buckets and internal state.
