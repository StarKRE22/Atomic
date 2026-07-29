# 🧩 DerivedEntityFilter

A dynamic, type-safe filter that selects entities of a specific derived type from a source collection.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
    - [DerivedEntityFilter&lt;T, E&gt;](#derivedentityfiltert-e)
    - [DerivedEntityFilter&lt;T&gt;](#derivedentityfiltert)
  - [Constructors](#-constructors)
    - [DerivedEntityFilter(IReadOnlyEntityCollection&lt;E&gt;, Predicate&lt;T&gt;, IEntityTrigger&lt;T&gt;[])](#derivedentityfilter)
  - [Properties](#-properties)
    - [Count](#count)
  - [Methods](#-methods)
    - [Contains(T)](#containst)
    - [CopyTo(ICollection&lt;T&gt;)](#copytoicollectiont)
    - [CopyTo(T[], int)](#copytot-int)
    - [GetEnumerator()](#getenumerator)
    - [Dispose()](#dispose)

---

## 🗂 Example of Usage

Filter alive players from a heterogeneous world:

```csharp
IEntityWorld world = ...;

var playerFilter = new DerivedEntityFilter<IPlayerEntity>(
    world.Entities,
    player => player.IsAlive
);

foreach (IPlayerEntity player in playerFilter)
    player.UpdateLogic();
```

Use triggers to re-evaluate when state changes:

```csharp
var aliveFilter = new DerivedEntityFilter<IUnitEntity, IGameEntity>(
    world.Entities,
    unit => unit.GetValue<int>("Health") > 0,
    new ValueEntityTrigger<IUnitEntity>("Health")
);

aliveFilter.OnRemoved += unit => Debug.Log($"Unit died: {unit}");
```

Dispose when no longer needed:

```csharp
var filter = new DerivedEntityFilter<IPlayerEntity>(world.Entities, player => true);
// ...
filter.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type

#### `DerivedEntityFilter<T, E>`

```csharp
public class DerivedEntityFilter<T, E> : IReadOnlyEntityCollection<T>, IDisposable
    where E : IEntity
    where T : E
```

- **Description:** A dynamic, type-safe view over an existing entity collection, selecting entities of type `T` from a source of type `E`.
- **Inheritance:** [IReadOnlyEntityCollection&lt;T&gt;](../Collections/IReadOnlyEntityCollection%601.md), `IDisposable`
- **Type Parameters:**
  - `T` – The derived entity type included in the filter.
  - `E` – The base entity type exposed by the source collection.
- **Notes:** Automatically synchronizes when entities are added, removed, or trigger state changes.
- **See also:** [DerivedEntityFilter&lt;T&gt;](#derivedentityfiltert), [IEntityTrigger&lt;T&gt;](../Filters/IEntityTrigger%601.md)

#### `DerivedEntityFilter<T>`

```csharp
public class DerivedEntityFilter<T> : DerivedEntityFilter<T, IEntity>
    where T : IEntity
```

- **Description:** Convenience specialization of [DerivedEntityFilter&lt;T, E&gt;](#derivedentityfiltert-e) where the base type is fixed to [IEntity](../Entities/IEntity.md).
- **Inheritance:** [DerivedEntityFilter&lt;T, IEntity&gt;](#derivedentityfiltert-e)
- **Type Parameter:** `T` – The derived entity type included in the filter.
- **Notes:** Use this when filtering from a heterogeneous [IReadOnlyEntityCollection&lt;IEntity&gt;](../Collections/IReadOnlyEntityCollection%601.md).

---

### 🏗️ Constructors

#### `DerivedEntityFilter(IReadOnlyEntityCollection<E>, Predicate<T>, IEntityTrigger<T>[])`

```csharp
public DerivedEntityFilter(
    IReadOnlyEntityCollection<E> source,
    Predicate<T> predicate,
    params IEntityTrigger<T>[] triggers
)
```

- **Description:** Initializes a new instance that observes `source` and maintains a live filtered subset.
- **Parameters:**
  - `source` – The source entity collection to observe.
  - `predicate` – The predicate used to determine filter inclusion.
  - `triggers` – Optional triggers that signal when an entity should be re-evaluated.
- **Throws:** `ArgumentNullException` if `source` or `predicate` is null.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of entities currently in the filtered set.
- **Access:** Read-only

---

### 🏹 Methods

#### `Contains(T)`

```csharp
public bool Contains(T entity)
```

- **Description:** Returns whether the specified entity is in the filtered set.
- **Parameter:** `entity` – The entity to check.
- **Returns:** `true` if the entity is in the filtered set; otherwise, `false`.

#### `CopyTo(ICollection<T>)`

```csharp
public void CopyTo(ICollection<T> results)
```

- **Description:** Copies the filtered entities into the specified collection.
- **Parameter:** `results` – The collection to receive the entities.

#### `CopyTo(T[], int)`

```csharp
public void CopyTo(T[] array, int arrayIndex)
```

- **Description:** Copies the filtered entities into the specified array starting at `arrayIndex`.
- **Parameters:**
  - `array` – The destination array.
  - `arrayIndex` – The zero-based index at which copying begins.

#### `GetEnumerator()`

```csharp
public EntityCollection<T>.Enumerator GetEnumerator()
```

- **Description:** Returns an enumerator over the filtered entities.
- **Returns:** An enumerator for the filtered set.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Releases all subscriptions and clears internal state.
- **Remarks:** Call this when the filter is no longer needed to avoid memory leaks.
