# 🧩 MultiEntityPool<K, E>

A registry that manages **multiple pools of entities**, each identified by a unique key of type
`K`. Each pool can rent and return entities of type `E`. Useful for managing multiple types or categories of entities in
a single centralized system.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructor)
    - [Methods](#-methods)
        - [Init(K, int)](#initk-int)
        - [Rent(K)](#rentk)
        - [Return(E)](#returne)
        - [Dispose()](#dispose)
        - [OnCreate(E)](#oncreatee)
        - [OnDispose(E)](#ondisposee)
        - [OnRent(E)](#onrente)
        - [OnReturn(E)](#onreturne)

---

## 🗂 Example of Usage

```csharp
// Assume we have two types of enemies, identified by an enum
public enum EnemyType
{
    Goblin,
    Orc
}
```

```csharp
// Define enemy entity type
public interface IEnemyEntity : IEntity
{
}
```

```csharp
//Assume we have an instance of IMultiEntityFactory
IMultiEntityFactory<EnemyType, IEnemyEntity> enemyFactory = ...

// Create a multi-entity pool
var enemyPool = new MultiEntityPool<EnemyType, IEnemyEntity>(factory);

// Initialize pools for each enemy type
enemyPool.Init(EnemyType.Goblin, 5);
enemyPool.Init(EnemyType.Orc, 3);

// Rent entities from pools
IEnemyEntity goblin = enemyPool.Rent(EnemyType.Goblin);
IEnemyEntity orc = enemyPool.Rent(EnemyType.Orc);

// Return entities to the pool when done
enemyPool.Return(goblin);
enemyPool.Return(orc);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class MultiEntityPool<K, E> : IMultiEntityPool<K, E> where E : IEntity
```

- **Type Parameters:**
    - `K` — The key type used to identify individual pools.
    - `E` — The entity type managed by the pools. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** [IMultiEntityPool\<K, E>](IMultiEntityPool%601.md), IDisposable

---

<div id="-constructor"></div>

### 🏗️ Constructors

#### `MultiEntityPool(IMultiEntityFactory<K, E>)`

```csharp
public MultiEntityPool(IMultiEntityFactory<K, E> factory);
```

- **Description:** Initializes a new instance of the `MultiEntityPool<K, E>`( class.
- **Parameter:** `factory` — The factory registry used to create entities for each key.
- **Exception:** `ArgumentNullException` — Thrown if `factory` is `null`.
- **Note:** This constructor is required to provide a factory that knows how to create entities for each key in the
  pool.

---

### 🏹 Methods

#### `Init(K, int)`

```csharp
public void Init(K key, int count);
```

- **Description:** Initializes the pool for the given key by pre-populating it with entities.
- **Parameters:**
    - `key` — The key identifying the pool.
    - `count` — Number of entities to preallocate.

#### `Rent(K)`

```csharp
public E Rent(K key);
```

- **Description:** Rents (retrieves) an entity from the pool associated with the key. Creates a new entity if the pool
  is empty.
- **Parameter:** `key` — The key identifying the pool.
- **Returns:** An entity of type `E`.

#### `Return(E)`

```csharp
public void Return(E entity);
```

- **Description:** Returns a previously rented entity to its corresponding pool.
- **Parameter:** `entity` — The entity to return.
- **Note:** Safely ignores duplicate returns.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Disposes all pooled and rented entities and clears internal collections.

#### `OnCreate(E)`

```csharp
protected virtual void OnCreate(E entity);
```

- **Description:** Called when a new entity is created for a pool.
- **Default Behavior:** Empty; override to implement custom logic.

#### `OnDispose(E)`

```csharp
protected virtual void OnDispose(E entity);
```

- **Description:** Called when an entity is permanently removed from the pool.

#### `OnRent(E)`

```csharp
protected virtual void OnRent(E entity);
```

- **Description:** Called when an entity is rented from a pool.

#### `OnReturn(E)`

```csharp
protected virtual void OnReturn(E entity);
```

- **Description:** Called when an entity is returned to its pool.