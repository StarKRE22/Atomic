# 🧩 EntityPool\<E>

A **generic object pool** for managing entities of type `E`. Entities are created via
an [IEntityFactory\<E>](../Factories/IEntityFactory%601.md) and reused through the pool to reduce allocations. Supports
lifecycle hooks for creation, renting, returning, and disposal of entities. Useful in scenarios with frequent entity
creation and destruction to improve performance.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Methods](#-methods)
        - [Init(int)](#initint)
        - [Rent()](#rent)
        - [Return(E)](#returne)
        - [Dispose()](#dispose)
        - [OnCreate(E)](#oncreatee)
        - [OnDispose(E)](#ondisposee)
        - [OnRent(E)](#onrente)
        - [OnReturn(E)](#onreturne)

---

## 🗂 Example of Usage

```csharp
// Assume we have a factory for GameEntity
IEntityFactory<GameEntity> factory = ...
EntityPool<GameEntity> pool = new(factory);

// Initialize pool
pool.Init(3);

// Rent entities
var entity1 = pool.Rent();
var entity2 = pool.Rent();

// Return entities to the pool
pool.Return(entity1);
pool.Return(entity2);

// Dispose pool when done
pool.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class EntityPool<E> : IEntityPool<E> where E : IEntity
```

- **Type Parameter:** `E` — The entity type managed by the pool. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IEntityPool\<E>](IEntityPool%601.md)
- **See also** [IEntityFactory\<E>](../Factories/IEntityFactory%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

#### `EntityPool(IEntityFactory<E>, TArgs, ExpandMode)`

```csharp
public EntityPool(IEntityFactory<TEntity, TArgs> factory, TArgs args, ExpandMode expandMode = ExpandMode.ExpandByOne);
```

- **Description:** Initializes a new instance of this pool class using the specified
  factory, construction arguments, and expansion mode.
- **Parameter:** `factory` — The factory used to create new entity instances when needed.
- **Parameter:** `args` — The arguments passed to the factory when creating entities.
- **Parameter:** `expandMode` — Determines how the pool expands when empty. Defaults to `ExpandByOne`. See [ExpandMode](ExpandMode.md).
- **Exception:** Throws `ArgumentNullException` if `factory` or `args` is null.
- **Note:** Ensures that the pool has a valid factory for creating entities, preventing null reference issues during
  runtime.

---

### 🏹 Methods

#### `Init(int)`

```csharp
public void Init(int initialCount)
```

- **Description:** Pre-populates the pool with a specified number of entities.
- **Parameter:** `initialCount` — Number of entities to create and add to the pool initially.
- **Note:** Calls the virtual `OnCreate` hook for each entity.

#### `Rent()`

```csharp
public E Rent();
```

- **Description:** Retrieves an entity from the pool. If the pool is empty, a new entity is created.
- **Returns:** An entity instance of type `E`.
- **Note:** Calls the virtual `OnRent` hook after an entity is rented.

#### `Return(E)`

```csharp
public void Return(E entity);
```

- **Description:** Returns an entity to the pool for reuse.
- **Parameter:** `entity` — The entity being returned.
- **Note:** Calls the virtual `OnReturn` hook. If the entity was not rented, a warning may be logged.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Clears the pool and disposes of all entities.
- **Note:** Calls the virtual `OnDispose` hook for each pooled or rented entity.

#### `OnCreate(E)`

```csharp
protected virtual void OnCreate(E entity);
```

- **Description:** Called when a new entity is created and added to the pool.
- **Parameter:** `entity` — The newly created entity.
- **Note:** Override to perform custom initialization logic for newly created entities.

#### `OnDispose(E)`

```csharp
protected virtual void OnDispose(E entity);
````

- **Description:** Called when an entity is permanently removed from the pool (during disposal).
- **Parameter:** `entity` — The entity being removed.
- **Note:** Override to perform cleanup or resource release for entities being disposed.

#### `OnRent(E)`

```csharp
protected virtual void OnRent(E entity);
````

- **Description:** Called when an entity is rented from the pool.
- **Parameter:** `entity` — The entity being rented.
- **Note:** Override to perform actions when an entity is taken from the pool (e.g., reset state).

#### `OnReturn(E)`

```csharp
protected virtual void OnReturn(E entity);
````

- **Description:** Called when an entity is returned to the pool.
- **Parameter:** `entity` — The entity being returned.
- **Note:** Override to perform actions when an entity is returned (e.g., deactivate or reset).