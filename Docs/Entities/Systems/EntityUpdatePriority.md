# 🧩 EntityUpdatePriority

**EntityUpdatePriority** is an enum that defines update priority levels for entities processed by
[PriorityEntitySystem](PriorityEntitySystem.md). It allows systems to spend more update budget on important entities
and less on background ones.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Enum Values](#-enum-values)
- [Examples of Usage](#-examples-of-usage)
- [See Also](#-see-also)

---

## 🧩 Overview

Priority-based systems divide their per-frame budget among entities based on importance. `EntityUpdatePriority`
classifies each entity as **Low**, **Medium**, or **High**. The system then processes them according to configurable
percentage quotas.

---

## 🔍 Enum Values

```csharp
public enum EntityUpdatePriority : byte
{
    Low = 0,
    Medium = 1,
    High = 2
}
```

| Value | Description |
|-------|-------------|
| `Low` | Background or far-away entities. Updated least frequently. |
| `Medium` | Standard entities. Updated with moderate budget. |
| `High` | Critical or near-camera entities. Updated most frequently. |

---

## 🗂 Examples of Usage

### Evaluating Priority in a PriorityEntitySystem

```csharp
public class DistancePrioritySystem : PriorityEntitySystem<IGameEntity>
{
    protected override EntityUpdatePriority EvaluatePriority(IGameEntity entity)
    {
        float distance = entity.GetDistanceToPlayer();
        
        if (distance < 10f)
            return EntityUpdatePriority.High;
        if (distance < 30f)
            return EntityUpdatePriority.Medium;
        
        return EntityUpdatePriority.Low;
    }

    protected override void Update(IGameEntity entity, float deltaTime)
    {
        entity.UpdateLogic(deltaTime);
    }
}
```

---

## 🔗 See Also

- [PriorityEntitySystem](PriorityEntitySystem.md)
- [EntitySystemBase](EntitySystemBase.md)
- [EntitySystem](EntitySystem.md)
