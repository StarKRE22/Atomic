# 🧩 EntityUpdatePriority

Enum that defines update priority levels for entities processed by [PriorityEntitySystem](PriorityEntitySystem.md).

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Fields](#-fields)
    - [Low](#low)
    - [Medium](#medium)
    - [High](#high)
- [See Also](#-see-also)

---

## 🗂 Example of Usage

```csharp
public sealed class DistancePrioritySystem : PriorityEntitySystem<IGameEntity>
{
    protected override EntityUpdatePriority EvaluatePriority(IGameEntity entity)
    {
        float distance = PlayerUseCase.GetDistanceToPlayer(entity);

        if (distance < 10f)
            return EntityUpdatePriority.High;
        if (distance < 40f)
            return EntityUpdatePriority.Medium;

        return EntityUpdatePriority.Low;
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public enum EntityUpdatePriority : byte
```

- **Description:** Defines update priority levels for entities processed by [PriorityEntitySystem](PriorityEntitySystem.md).
- **Inheritance:** `Enum`, `byte`
- **Notes:** Higher priority entities receive a larger share of the per-frame update budget.
- **See also:** [PriorityEntitySystem<E>](PriorityEntitySystem.md)

### 🏹 Fields

#### `Low`

```csharp
Low = 0
```

- **Description:** Background or far-away entities. Updated least frequently.

#### `Medium`

```csharp
Medium = 1
```

- **Description:** Standard entities. Updated with a moderate share of the budget.

#### `High`

```csharp
High = 2
```

- **Description:** Critical or near-camera entities. Updated most frequently.

---

## 🔗 See Also

- [PriorityEntitySystem<E>](PriorityEntitySystem.md)
- [EntitySystemBase<E>](EntitySystemBase.md)
- [EntitySystem<E>](EntitySystem.md)
