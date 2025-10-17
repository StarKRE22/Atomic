# 🧩 BehaviourEntityTriggers

A trigger that responds to **behaviour changes** (added or removed) on entities. Provides both a **generic** and a
**non-generic** version for flexible use with any entity type. The behaviour trigger allows monitoring **behaviours**
added or removed from entities that implement [IEntity](../Entities/IEntity.md). It invokes a configured action whenever
a tracked entity’s behaviours change.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic Usage](#ex1)
    - [Generic Usage](#ex2)
- [API Reference](#-api-reference)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Non-generic Usage

[BehaviourEntityTrigger](BehaviourEntityTrigger.md) works with plain [IEntity](../Entities/IEntity.md)

```csharp
// Track general entities for behaviour additions and deletions
var trigger = new BehaviourEntityTrigger(
    added: true,
    removed: true,
);

// Usage with non-generic EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<int>("Health") > 0,
    trigger
);
```

<div id="ex2"></div>

### 2️⃣ Generic Usage

[BehaviourEntityTrigger\<E>](BehaviourEntityTrigger%601.md) works with specific entity type derived
from [IEntity](../Entities/IEntity.md)

```csharp
// Track specific entities for behaviour additions and deletions
var trigger = new BehaviourEntityTrigger<PlayerEntity>(
    added: true,
    removed: true,
);

// Usage with non-generic EntityFilter
var filter = new EntityFilter<PlayerEntity>(
    allEntities,
    e => e.GetValue<int>("Health") > 0,
    trigger
);
```

---

## 🔍 API Reference

- [BehaviourEntityTrigger](BehaviourEntityTrigger.md) <!-- + -->
- [BehaviourEntityTrigger\<E>](BehaviourEntityTrigger%601.md) <!-- + -->