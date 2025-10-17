# 🧩 EntityFilter\<E>

Represents a **dynamic, observable, filtered view** over an
existing [IReadOnlyEntityCollection\<E>](../Collections/IReadOnlyEntityCollection%601.md). Entities are included based
on a predicate and automatically synchronized using **state triggers**. Useful for dynamically retrieving subsets of
entities (e.g., "all alive enemies", "all objects with a specific component") and keeping them synchronized as changes
happen.

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
        - [Contains(E)](#containse)
        - [CopyTo(ICollection\<E>)](#copytoicollectione)
        - [CopyTo(E[], int)](#copytoe-int)
        - [Dispose()](#dispose)
        - [GetEnumerator()](#getenumerator)

---

## 🗂 Example of Usage

```csharp
// Source of all game entities
IReadOnlyEntityCollection<GameEntity> allEntities = ...

// Filter: only active enemies
var filter = new EntityFilter<GameEntity>(
    source: allEntities,
    predicate: e => e.GetValue<bool>("IsEnemy") && e.GetValue<bool>("IsAlive"),
);

// Subscribe to events
filter.OnAdded += e => Console.WriteLine($"[+] {e.Name}");
filter.OnRemoved += e => Console.WriteLine($"[-] {e.Name}");

// Use the filter
foreach (var enemy in filter)
    Console.WriteLine(enemy.Name);

// Cleanup
filter.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class EntityFilter<E> : IReadOnlyEntityCollection<E>, IDisposable where E : IEntity
```

- **Type Parameter:** `E` — the type of entity being filtered. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IReadOnlyEntityCollection\<E>](../Collections/IReadOnlyEntityCollection%601.md), `IDisposable`
- **See also:** [EntityFilter](EntityFilter.md), [IEntityTrigger\<E>](IEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public EntityFilter(
    IReadOnlyEntityCollection<E> source,
    Predicate<E> predicate,
    params IEntityTrigger<E>[] triggers
)
```

- **Description:** Creates a new filter over the given source collection.
- **Parameters:**
    - `source` — the source entity collection being observed.
    - `predicate` — the filter condition (true = entity is included).
    - `triggers` — (optional) array of triggers used to re-evaluate entities when their state changes.
- **Exception:** `ArgumentNullException` — if `source` or `predicate` is `null`.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action OnStateChanged;
```

- **Description:** Raised whenever the filter state changes (an entity is added or removed).

#### `OnAdded`

```csharp
public event Action<E> OnAdded;
```

- **Description:** Raised when a new entity matches the filter and is added.
- **Parameter:** `E` — the added entity.

#### `OnRemoved`

```csharp
public event Action<E> OnRemoved;
```

- **Description:** Raised when an entity no longer matches the filter or is removed from the source.
- **Parameter:** `E` — the removed entity.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** The number of entities currently matching the filter.
- **Returns:** The current count of entities in the filter.

---

### 🏹 Methods

#### `Contains(E)`

```csharp
public bool Contains(E entity);
```

- **Description:** Checks whether the given entity is included in the filter.
- **Parameters:** `entity` — The entity to check.
- **Returns:** `true` if the entity is present.

#### `CopyTo(ICollection<E>)`

```csharp
public void CopyTo(ICollection<E> results);
```

- **Description:** Copies all entities in the filter into the provided collection.
- **Parameter:** `results` — The collection that will receive the entities. Must not be `null`.

#### `CopyTo(E[], int)`

```csharp
public void CopyTo(E[] array, int arrayIndex);
```

- **Description:** Copies all entities in the filter into the given array, starting at the specified index.
- **Parameters:**
    - `array` — The destination array. Must not be `null`.
    - `arrayIndex` — The zero-based index in the array at which copying begins.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Unsubscribes from the source, clears tracking, and releases internal state.
- **Note:** Call when the filter is no longer needed to prevent memory leaks.

#### `GetEnumerator()`

```csharp
public EntityCollection<E>.Enumerator GetEnumerator();
```

- **Description:** Returns an enumerator for iterating over the entities in the filter.
- **Returns:** A struct-based enumerator that can be used to iterate through the filtered entities.