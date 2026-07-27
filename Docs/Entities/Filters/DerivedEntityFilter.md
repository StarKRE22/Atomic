# 🧩 DerivedEntityFilter

**DerivedEntityFilter** is a dynamic, type-safe filter that selects entities of a specific derived type from a source
collection. It implements [IReadOnlyEntityCollection\<T\>](../Collections/IReadOnlyEntityCollection%601.md) and stays
synchronized as entities are added, removed, or changed.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Variants](#-variants)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`DerivedEntityFilter<T, E>` observes a source collection of base type `E` and maintains a live subset of entities that:

1. Can be cast to derived type `T`
2. Satisfy the provided predicate

It raises `OnAdded`, `OnRemoved`, and `OnStateChanged` events whenever the filtered set changes. Optional triggers can be
used to re-evaluate entities when their state changes.

`DerivedEntityFilter<T>` is a convenience specialization where the source type is `IEntity`.

---

## 🔍 Variants

| Class | Description |
|-------|-------------|
| `DerivedEntityFilter<T, E>` | Filters entities of type `T` from a collection of base type `E`. |
| `DerivedEntityFilter<T>` | Shorthand for `DerivedEntityFilter<T, IEntity>`. |

---

## 🗂 Examples of Usage

### Basic Type Filter

```csharp
IEntityWorld<IGameEntity> world = ...;

var playerFilter = new DerivedEntityFilter<IPlayerEntity>(
    world,
    player => player.IsAlive
);

foreach (IPlayerEntity player in playerFilter)
    player.UpdateLogic();
```

### Filter with Triggers

```csharp
var aliveFilter = new DerivedEntityFilter<IUnitEntity, IGameEntity>(
    world,
    unit => unit.GetValue<int>("Health") > 0,
    new ValueEntityTrigger<IUnitEntity>("Health")
);

aliveFilter.OnRemoved += unit => Debug.Log($"Unit died: {unit}");
```

### Dispose When Done

```csharp
var filter = new DerivedEntityFilter<IPlayerEntity>(world, player => true);

// Later
filter.Dispose();
```

---

## 🔍 API Reference

### DerivedEntityFilter\<T, E\>

```csharp
public class DerivedEntityFilter<T, E> : IReadOnlyEntityCollection<T>, IDisposable
    where E : IEntity
    where T : E
```

- **Type Parameters:**
  - `T` — The derived entity type included in the filter.
  - `E` — The base entity type exposed by the source collection.
- **Inheritance:** [IReadOnlyEntityCollection\<T\>](../Collections/IReadOnlyEntityCollection%601.md), `IDisposable`

#### Constructor

```csharp
public DerivedEntityFilter(
    IReadOnlyEntityCollection<E> source,
    Predicate<T> predicate,
    params IEntityTrigger<T>[] triggers
)
```

- **Parameter:** `source` — The source entity collection to observe.
- **Parameter:** `predicate` — The predicate used to determine filter inclusion.
- **Parameter:** `triggers` — Optional triggers that signal when an entity should be re-evaluated.

#### Events

| Event | Description |
|-------|-------------|
| `OnAdded` | Raised when an entity enters the filter. |
| `OnRemoved` | Raised when an entity leaves the filter. |
| `OnStateChanged` | Raised when the filter set changes in any way. |

#### Methods

| Method | Description |
|--------|-------------|
| `Contains(T)` | Returns whether the entity is in the filtered set. |
| `CopyTo(...)` | Copies filtered entities to a collection or array. |
| `GetEnumerator()` | Returns an enumerator over the filtered entities. |
| `Dispose()` | Unsubscribes from the source and clears state. |

### DerivedEntityFilter\<T\>

```csharp
public class DerivedEntityFilter<T> : DerivedEntityFilter<T, IEntity>
    where T : IEntity
```

Convenience specialization for filtering from `IReadOnlyEntityCollection<IEntity>`.

---

## 📌 Best Practices

- Dispose filters when they are no longer needed to avoid memory leaks.
- Use triggers only when entity state affects the predicate.
- Keep predicates fast — they run on every source add/remove and every trigger firing.
- Prefer `DerivedEntityFilter<T>` when filtering from a heterogeneous world collection.
- Use `DerivedEntityFilter<T, E>` when the source collection already has a typed base type.
