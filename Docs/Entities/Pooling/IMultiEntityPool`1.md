# 🧩 IMultiEntityPool<K, E>

```csharp
public interface IMultiEntityPool<in K, E> : IDisposable where E : IEntity
```

- **Description:** Represents a **registry of multiple entity pools**, each identified by a key of type `K`.  
  Allows managing different pools in a centralized way, with each pool able to rent and return entities of type `E`.
- **Type Parameters:**
    - `K` — The key type used to identify individual pools.
    - `E` — The entity type managed by the pools. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `IDisposable`
- **Note:** Useful when you need multiple distinct pools (e.g., different enemy types) under a single manager.

---

## 🏹 Methods

#### `Init(K, int)`

```csharp
public void Init(K key, int count);
```

- **Description:** Initializes the pool associated with the specified key by pre-populating it with entities.
- **Parameters:**
    - `key` — The key identifying which pool to initialize.
    - `count` — The number of entities to preallocate in the pool.

#### `Rent(K)`

```csharp
public E Rent(K key);
```

- **Description:** Rents (retrieves) an entity from the pool associated with the specified key.  
  If the pool is empty, a new entity may be created.
- **Parameter:** `key` — The key identifying the pool to rent from.
- **Returns:** An entity instance of type `E`.

#### `Return(E)`

```csharp
public void Return(E entity);
```

- **Description:** Returns an entity to its corresponding pool, making it available for reuse.
- **Parameter:** `entity` — The entity to return.

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
// Use a multi-entity pool
IMultiEntityPool<EnemyType, EnemyEntity> enemyPool = ...; // get an instance

// Initialize pools for each enemy type
enemyPool.Init(EnemyType.Goblin, 5);
enemyPool.Init(EnemyType.Orc, 3);

// Rent entities from pools
EnemyEntity goblin = enemyPool.Rent(EnemyType.Goblin);
EnemyEntity orc = enemyPool.Rent(EnemyType.Orc);

// Return entities to the pool when done
enemyPool.Return(goblin);
enemyPool.Return(orc);
```