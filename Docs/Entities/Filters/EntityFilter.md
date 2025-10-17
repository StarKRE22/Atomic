# 🧩 EntityFilter

A **non-generic wrapper** of [EntityFilter\<E>](EntityFilter%601.md) specialized
for [IEntity](../Entities/IEntity.md). Provides a dynamic, observable, filtered view over a general entity collection
without specifying a generic type
parameter. Use this class when you need to filter **heterogeneous collections** of entities without binding to a
specific entity type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
        - [OnAdded](#onadded)
        - [OnRemoved](#onremoved)
    - [Properties](#-properties)
        - [Count](#count)
    - [Methods](#-methods)
        - [Contains(IEntity)](#containsientity)
        - [CopyTo(ICollection\<IEntity>)](#copytoicollectionientity)
        - [CopyTo(IEntity[], int)](#copytoientity-int)
        - [Dispose()](#dispose)
        - [GetEnumerator()](#getenumerator)

---

## 🗂 Example of Usage

```csharp
// Source of all entities (mixed types)
IReadOnlyEntityCollection<IEntity> allEntities = ...

// Create a non-generic filter: only entities that are visible
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<bool>("IsVisible").Value
);

// Subscribe to events
filter.OnAdded += e => Console.WriteLine($"[+] {e.Id}");
filter.OnRemoved += e => Console.WriteLine($"[-] {e.Id}");

// Iterate filtered entities
foreach (var entity in filter)
    Console.WriteLine($"Visible: {entity.Id}");

// Cleanup
filter.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class EntityFilter : EntityFilter<IEntity>, IReadOnlyEntityCollection
```

- **Inheritance:** [EntityFilter\<E>](./EntityFilter.md),
  [IReadOnlyEntityCollection](../Collections/IReadOnlyEntityCollection.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public EntityFilter(
    IReadOnlyEntityCollection<IEntity> source,
    Predicate<IEntity> predicate,
    params IEntityTrigger<IEntity>[] triggers
)
```

- **Description:** Creates a new entity filter for general `IEntity` objects.
- **Parameters:**
    - `source` — The source collection of entities to observe.
    - `predicate` — A function that determines whether an entity should be included (`true` = include).
    - `triggers` — (optional) Triggers for re-evaluating entities when their state changes.
- **Exception:**
    - `ArgumentNullException` — Thrown if `source` or `predicate` is `null`.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Raised whenever the filter changes (entity added or removed).

#### `OnAdded`

```csharp
public event Action<IEntity> OnAdded;
```

- **Description:** Raised when a new entity satisfies the predicate and is added to the filter.
- **Parameter:** `IEntity` — The entity that was added.

#### `OnRemoved`

```csharp
public event Action<IEntity> OnRemoved;
```

- **Description:** Raised when an entity no longer satisfies the predicate or is removed from the source.
- **Parameter:** `IEntity` — The entity that was removed.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the number of entities currently matching the filter.
- **Returns:** The current count of entities in the filter.

---

### 🏹 Methods

#### `Contains(IEntity)`

```csharp
public bool Contains(IEntity entity);
```

- **Description:** Determines whether the specified entity is currently included in the filter.
- **Parameters:** `entity` — The entity to check.
- **Returns:** `true` if the entity is in the filter; otherwise `false`.

#### `CopyTo(ICollection<IEntity>)`

```csharp
public void CopyTo(ICollection<IEntity> results);
```

- **Description:** Copies all entities currently in the filter into the provided collection.
- **Parameter:** `results` — The collection that will receive the entities. Must not be `null`.

#### `CopyTo(IEntity[], int)`

```csharp
public void CopyTo(IEntity[] array, int arrayIndex);
```

- **Description:** Copies all entities in the filter into the specified array, starting at the given index.
- **Parameters:**
    - `array` — The destination array. Must not be `null`.
    - `arrayIndex` — The zero-based index in the array at which copying begins.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Releases resources used by the filter, unsubscribes from events, and clears internal state.
- **Note:** Always call `Dispose()` when the filter is no longer needed to prevent memory leaks.

#### `GetEnumerator()`

```csharp
public EntityCollection<IEntity>.Enumerator GetEnumerator();
```

- **Description:** Returns an enumerator for iterating over entities in the filter.
- **Returns:** A struct-based enumerator that can be used to iterate through the filtered entities.