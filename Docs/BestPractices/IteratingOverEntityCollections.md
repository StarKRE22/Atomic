# 📌 Iterating over EntityCollections, Worlds and Filters.

When using [EntityCollections](../Entities/Collections/Manual.md), [EntityWorlds](../Entities/Worlds/Manual.md)
or [EntityFilters](../Entities/Filters/Manual.md), it is important to understand that **each iteration over entities
introduces a small overhead**. Knowing how to **optimize performance** becomes crucial when working with a large number
of elements.

---

## 📑 Table of Contents

- [Prefer Concrete Types](#prefer-concrete-types)
- [Summary](#summary)

---

## Prefer Concrete Types

When iterating via interfaces such
as [IReadOnlyEntityCollection\<E>](../Entities/Collections/IReadOnlyEntityCollection%601.md)
or [IEntityWorld](../Entities/Worlds/IEntityWorld.md), the iterator may **box**, causing **heap allocations** and **GC
pressure**. To avoid this, use the **concrete collection type** whenever possible.

---

### ❌ Has Boxing (IEntityCollection)

```csharp
// Assume we have an instance of IEntityCollection
IEntityCollection collection = ...;

// Reference type of "IEnumerator<IEntity>"
foreach (IEntity entity in collection)
{
    ...
}
```

---

### ❌ Has Boxing (IEntityWorld)

```csharp
// Assume we have an instance of IEntityWorld
IEntityWorld world = ...;

// Reference type of "IEnumerator<IEntity>"
foreach (IEntity entity in world)
{
    ...
}
```

---

### ✅ No Boxing (EntityCollection)

```csharp
// Assume we have an instance of EntityCollection
EntityCollection collection = ...;

// Value type of "EntityCollection.Enumerator"
foreach (IEntity entity in collection)
{
    ...
}
```

---

### ✅ No Boxing (EntityWorld)

```csharp
// Assume we have an instance of EntityWorld
EntityWorld world = ...;

// Value type of "EntityCollection.Enumerator"
foreach (IEntity entity in world)
{
    ...
}
```

### ✅ No Boxing (EntityFilter)

```csharp
// Assume we have an instance of EntityFilter
EntityFilter filter = ...;

// Value type of "EntityCollection.Enumerator"
foreach (IEntity entity in filter)
{
    ...
}
```

---

## Summary

| Use Case                                                                       | Recommendation                      |
|--------------------------------------------------------------------------------|-------------------------------------|
| Iterating via `IEntityCollection` or `IEntityWorld` interfaces                 | ⚠️ Causes boxing and GC allocations |
| Iterating via concrete types (`EntityCollection`, `EntityWorld`, `EntityFilter`) | ✅ No boxing, zero allocations       |
| Working with large collections of entities                                     | 🚀 Always prefer concrete types     |

---

**In short:**
> When iterating entities, use concrete collection types (`EntityCollection`, `EntityWorld`) to avoid boxing and
> unnecessary allocations.


<!--

When
using [EntityCollections](../Entities/Collections/Manual.md), [EntityWorlds](../Entities/Worlds/Manual.md), [EntityFilters](../Entities/Filters/Manual.md)
it is important to understand that **each
iteration over entities introduces a small overhead**. Knowing how to **optimize performance** becomes crucial when
working
with a large number of elements.

---

### Prefer concrete types

When iterating via interfaces such
as [IReadOnlyEntityCollection\<E>](../Entities/Collections/IReadOnlyEntityCollection%601.md)
or [IEntityWorld](../Entities/Worlds/IEntityWorld.md), the iterator may **box**, causing **heap allocations** and **GC
pressure**. To avoid this, use the **concrete collection type** whenever possible.

#### ❌ Has Boxing (IEntityCollection)

```csharp
// Assume we have an instance of IEntityCollection
IEntityCollection collection = ...;

//Reference type of "IEnumerator<IEntity>"
foreach(IEntity entity in collection)
{
    ...
}
```

#### ❌ Has Boxing (IEntityWorld)

```csharp
// Assume we have an instance of IEntityCollection
IEntityWorld world = ...;

//Reference type of "IEnumerator<IEntity>"
foreach(IEntity entity in world)
{
    ...
}
```

#### ✅ No Boxing (EntityCollection)

```csharp
// Assume we have an instance of EntityCollection
EntityCollection collection = ...;

//Value type of "EntityCollection.Enumerator"
foreach(string item in collection) 
{
    ...
}
```

#### ✅ No Boxing (EntityWorld)

```csharp
// Assume we have an instance of EntityWorld
EntityWorld world = ...;

//Value type of "EntityCollection.Enumerator"
foreach(string item in world) 
{
    ...
}
```
-->