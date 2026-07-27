# 🧩 Entity Systems

**Entity Systems** process collections of entities every frame. They are useful for updating logic such as movement,
AI, animation, or physics across many entities efficiently. Systems support adaptive batching to stay within a frame
budget, and priority-based systems can focus updates on the most important entities.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [System Types](#-system-types)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

An entity system receives an [IReadOnlyEntityCollection\<E\>](../Collections/IReadOnlyEntityCollection%601.md) and
updates entities over time. Systems are typically wired to a context entity's lifecycle using extension methods such as
`AddTickSystem`.

Key features:

- **Adaptive batching** — adjusts how many entities are processed per frame based on a time budget
- **Priority support** — spend more updates on important entities
- **Lifecycle integration** — enable/disable with the owning context

---

## 🔍 System Types

| System | Description |
|--------|-------------|
| [EntitySystemBase\<E\>](EntitySystemBase.md) | Abstract base class with shared system infrastructure. |
| [EntitySystem\<E\>](EntitySystem.md) | Round-robin system; updates all entities evenly. |
| [PriorityEntitySystem\<E\>](PriorityEntitySystem.md) | Priority-based system; updates high-priority entities more often. |
| [EntityUpdatePriority](EntityUpdatePriority.md) | Enum defining `Low`, `Medium`, `High` priorities. |

---

## 🗂 Examples of Usage

### Round-Robin System

```csharp
public sealed class MovementSystem : EntitySystem<IGameEntity>
{
    public MovementSystem(IReadOnlyEntityCollection<IGameEntity> source, Settings settings)
        : base(source, settings)
    {
    }

    protected override void Update(IGameEntity entity, float deltaTime)
    {
        entity.Move(deltaTime);
    }
}
```

### Priority System

```csharp
public sealed class AIPrioritySystem : PriorityEntitySystem<IGameEntity>
{
    protected override EntityUpdatePriority EvaluatePriority(IGameEntity entity)
    {
        return entity.GetDistanceToPlayer() < 20f
            ? EntityUpdatePriority.High
            : EntityUpdatePriority.Low;
    }

    protected override void Update(IGameEntity entity, float deltaTime)
    {
        entity.UpdateAI(deltaTime);
    }
}
```

### Wiring to a Context

```csharp
IEntityWorld<IGameEntity> world = ...;
MovementSystem movement = new MovementSystem(world, new MovementSystem.Settings());
contextEntity.AddTickSystem(movement);
```

---

## 🔍 API Reference

- [EntitySystemBase\<E\>](EntitySystemBase.md)
- [EntitySystem\<E\>](EntitySystem.md)
- [PriorityEntitySystem\<E\>](PriorityEntitySystem.md)
- [EntityUpdatePriority](EntityUpdatePriority.md)

---

## 📌 Best Practices

- Keep per-entity update logic fast to allow adaptive batching to scale.
- Use `PriorityEntitySystem<E>` when entity importance varies (e.g., distance to camera).
- Dispose systems when the owning context is destroyed.
- Tune `frameBudget` and `batching` settings per platform.
- Use triggers with `PriorityEntitySystem<E>` to avoid recalculating priorities every frame.
