# 🧩 EntityPool

A **non-generic version** of [EntityPool\<E>](EntityPool%601.md) that operates on base [IEntity](../Entities/IEntity.md)
types. Use this when pooling a variety of entities that share a common interface but do not require type-specific
access. Ideal for scenarios where you want a simple pool for any entity without specifying a type parameter.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructor](#-constructor)
    - [Methods](#-methods)
        - [Init(int)](#initint)
        - [Rent()](#rent)
        - [Return(IEntity)](#returnientity)
        - [Dispose()](#dispose)
        - [OnCreate(IEntity)](#oncreateientity)
        - [OnDispose(IEntity)](#ondisposeientity)
        - [OnRent(IEntity)](#onrentientity)
        - [OnReturn(IEntity)](#onreturnientity)

---

## 🗂 Example of Usage

```csharp
IEntityFactory factory = ...
EntityPool pool = new(factory);

// Initialize pool
pool.Init(5);

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
public class EntityPool : EntityPool<IEntity>, IEntityPool
```

- **Inheritance:** [EntityPool\<E>](EntityPool%601.md), [IEntityPool](IEntityPool.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

#### `EntityPool(IEntityFactory<IEntity>)`

```csharp
public EntityPool(IEntityFactory<IEntity> factory);
```

- **Description:** Initializes a new instance of the non-generic `EntityPool` using the specified entity factory.
- **Parameter:** `factory` — The factory used to create `IEntity` instances.
- **Exception:** Throws `ArgumentNullException` if `factory` is null.

---

### 🏹 Methods

#### `Init(int)`

```csharp
public void Init(int initialCount);
```

- **Description:** Pre-populates the pool with a specified number of entities.
- **Parameter:** `initialCount` — The number of entities to create and add to the pool.
- **Note:** Calls the `OnCreate` lifecycle hook for each entity created.

#### `Rent()`

```csharp
public IEntity Rent();
```

- **Description:** Retrieves an entity from the pool. If the pool is empty, a new entity is created.
- **Returns:** An `IEntity` instance.
- **Note:** Calls the `OnRent` lifecycle hook after an entity is rented.

#### `Return(IEntity)`

```csharp
public void Return(IEntity entity);
```

- **Description:** Returns an entity to the pool for reuse.
- **Parameter:** `entity` — The entity being returned.
- **Note:** Calls the `OnReturn` lifecycle hook. If the entity was not rented, a warning may be logged.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Clears the pool and disposes of all entities, both pooled and currently rented.
- **Note:** Calls the `OnDispose` lifecycle hook for each entity.

#### `OnCreate(IEntity)`

```csharp
protected virtual void OnCreate(IEntity entity);
```

- **Description:** Called when a new entity is created and added to the pool.
- **Parameter:** `entity` — The newly created entity.
- **Note:** Override to perform custom initialization logic.

#### `OnDispose(IEntity)`

```csharp
protected virtual void OnDispose(IEntity entity);
```

- **Description:** Called when an entity is permanently removed from the pool.
- **Parameter:** `entity` — The entity being disposed.
- **Note:** Override to release resources or perform cleanup.

#### `OnRent(IEntity)`

```csharp
protected virtual void OnRent(IEntity entity);
```

- **Description:** Called when an entity is rented from the pool.
- **Parameter:** `entity` — The entity being rented.
- **Note:** Override to reset or activate the entity upon rental.

#### `OnReturn(IEntity)`

```csharp
protected virtual void OnReturn(IEntity entity);
```

- **Description:** Called when an entity is returned to the pool.
- **Parameter:** `entity` — The entity being returned.
- **Note:** Override to reset or deactivate the entity before it goes back into the pool.