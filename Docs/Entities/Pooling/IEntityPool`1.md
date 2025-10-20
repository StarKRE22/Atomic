# 🧩 IEntityPool\<E>

Represents a **generic object pool** for reusing instances of [IEntity](../Entities/IEntity.md). Reduces allocation
overhead by recycling entity instances instead of creating and destroying them repeatedly. Use when entity creation /
destruction is frequent, and you need better performance through object
pooling.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Rent()](#rent)
        - [Return(E)](#returne)
        - [Init(int)](#initint)
        - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of IEntityPool<GameEntity>
IEntityPool<GameEntity> pool = ...
pool.Init(3);

// Rent entities
GameEntity entity1 = pool.Rent();
GameEntity entity2 = pool.Rent();

// Return entities to the pool
pool.Return(entity1);
pool.Return(entity2);

// Dispose pool when no longer needed
pool.Dispose();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityPool<E> : IDisposable where E : IEntity
```

- **Type Parameter:** `E` — The entity type managed by the pool. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `IDisposable`

---

### 🏹 Methods

#### `Rent()`

```csharp
public E Rent();
```

- **Description:** Retrieves an entity instance from the pool. If the pool is empty, a new instance may be
  created.
- **Returns:** An entity instance of type `E`.

#### `Return(E)`

```csharp
public void Return(E entity);
```

- **Description:** Returns a previously rented entity back to the pool, making it available for reuse.
- **Parameter:** `entity` — The entity instance to return to the pool.

#### `Init(int)`

```csharp
public void Init(int initialCount);
```

- **Description:** Initializes the pool with a given number of preallocated entities.
- **Parameter:** `initialCount` — Number of entities to create and store in the pool initially.
- **Note:** Improves performance by reducing runtime allocations, especially for high-frequency spawning.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Disposes of all pooled entities and releases internal resources.
- **Note:** Call when the pool is no longer needed to avoid memory leaks.