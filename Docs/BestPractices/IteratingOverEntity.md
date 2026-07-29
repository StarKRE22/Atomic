# 📌 Iterating over Entity Tags, Values and Behaviours

When using entities such
as [Entity](../Entities/Entities/Entity.md), [MonoEntity](../Entities/Entities/MonoEntity.md),
or [IEntity](../Entities/Entities/IEntity.md), it is important to understand that **each iteration over tags, values and
behaviours introduces a small overhead**. Knowing how to **optimize performance** becomes crucial when working with a
large number of elements.

---

## 📑 Table of Contents

- [Prefer Concrete Types](#prefer-concrete-types)
- [Summary](#-summary)

---

## Prefer Concrete Types

When iterating via interfaces using [IEntity](../Entities/Entities/IEntity.md), the iterator may **box**, causing **heap
allocations** and **GC pressure**. To avoid this, use the **concrete implementation type** whenever possible.

---

### ❌ Has Boxing (IEntity)

```csharp
// Assume we have an instance of IEntity
IEntity entity = ...;

// Reference enumerators
IEnumerator<int> tags = entity.GetTagEnumerator();
IEnumerator<KeyValuePair<int, object>> values = entity.GetValueEnumerator();
IEnumerator<IEntityBehaviour> behaviours = entity.GetBehaviourEnumerator();
```

---

### ✅ No Boxing (Entity)

```csharp
// Assume we have an instance of Entity
Entity entity = ...;

// Struct enumerators
Entity.TagEnumerator tags = entity.GetTagEnumerator();
Entity.ValueEnumerator values = entity.GetValueEnumerator();
Entity.BehaviourEnumerator behaviours = entity.GetBehaviourEnumerator();
```

---

### ✅ No Boxing (MonoEntity)

```csharp
// Assume we have an instance of MonoEntity
MonoEntity entity = ...;

// Struct enumerators
MonoEntity.TagEnumerator tags = entity.GetTagEnumerator();
MonoEntity.ValueEnumerator values = entity.GetValueEnumerator();
MonoEntity.BehaviourEnumerator behaviours = entity.GetBehaviourEnumerator();
```

---

## Summary

| Use Case                                         | Recommendation                      |
|--------------------------------------------------|-------------------------------------|
| Iterating via `IEntity` interface                | ⚠️ Causes boxing and GC allocations |
| Iterating via `Entity` or `MonoEntity` directly | ✅ No boxing, zero allocations       |
| Working with many entities per frame             | 🚀 Always prefer concrete types     |

---

**In short:**
> Use the concrete type (`Entity`, `MonoEntity`) instead of `IEntity` whenever performance matters.


<!--

When using [entities](../Entities/Entities/Manual.md) such
as [Entity](../Entities/Entities/Entity.md), [MonoEntity](../Entities/Entities/MonoEntity.md)
or [IEntity](../Entities/Entities/IEntity.md), it is important to understand that **each
iteration over tags, values and behaviours introduces a small overhead**. Knowing how to **optimize performance**
becomes crucial when working
with a large number of elements.

---

### Prefer concrete types

When iterating via interfaces using [IEntity](../Entities/Entities/IEntity.md), the iterator may **box**, causing
**heap allocations** and **GC pressure**. To avoid this, use the **concrete implementation type** whenever possible.

#### ❌ Has Boxing

```csharp
// Assume we have an instance of IEntity
IEntity entity = ...;

// Reference enumerators
IEnumerator<int> tags = entity.GetTagEnumerator();
IEnumerator<KeyValuePair<int, object>> values = entity.GetValueEnumerator();
IEnumerator<IEntityBehaviour> behaviours = entity.GetBehaviourEnumerator();
```

#### ✅ No Boxing

```csharp
// Assume we have an instance of Entity
Entity entity = ...;

// Struct enumerators
Entity.TagEnumerator tags = entity.GetTagEnumerator();
Entity.ValueEnumerator values = entity.GetValueEnumerator();
Entity.BehaviourEnumerator behaviours = entity.GetBehaviourEnumerator();
```

```csharp
// Assume we have an instance of Entity
MonoEntity entity = ...;

// Struct enumerators
MonoEntity.TagEnumerator tags = entity.GetTagEnumerator();
MonoEntity.ValueEnumerator values = entity.GetValueEnumerator();
MonoEntity.BehaviourEnumerator behaviours = entity.GetBehaviourEnumerator();
```

-->