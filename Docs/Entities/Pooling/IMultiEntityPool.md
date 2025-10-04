# 🧩 IMultiEntityPool

```csharp
public interface IMultiEntityPool : IMultiEntityPool<string, IEntity>
```

- **Description:** A **non-generic alias** of `IMultiEntityPool<K, E>` for managing multiple pools of [IEntity](../Entities/IEntity.md) using **string keys**.
- **Inheritance:** [IMultiEntityPool\<K, E>](IMultiEntityPool%601.md)
- **Note:** Useful when you want a simple key-based pool registry without specifying type parameters.

---

## 🏹 Methods

#### `Init(string, int)`

```csharp
public void Init(string key, int count);
```

- **Description:** Initializes the pool associated with the specified string key.
- **Parameters:**
  - `key` — The string key identifying the pool.
  - `count` — Number of entities to preallocate in the pool.

#### `Rent(string)`

```csharp
public IEntity Rent(string key);
```

- **Description:** Rents an entity from the pool associated with the given key.
- **Parameter:** `key` — The string key identifying the pool.
- **Returns:** An `IEntity` instance.

#### `Return(IEntity)`

```csharp
public void Return(IEntity entity);
````

- **Description:** Returns a previously rented entity to its corresponding pool.
- **Parameter:** `entity` — The entity to return.

---

## 🗂 Example of Usage

```csharp
// Use a multi-entity pool
IMultiEntityPool enemyPool = ...; // get an instance

// Initialize pools for each enemy type
enemyPool.Init("Goblin", 5);
enemyPool.Init("Orc", 3);

// Rent entities from pools
EnemyEntity goblin = enemyPool.Rent("Goblin");
EnemyEntity orc = enemyPool.Rent("Orc");

// Return entities to the pool when done
enemyPool.Return(goblin);
enemyPool.Return(orc);
```