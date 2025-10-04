# 🧩 MultiEntityPool

```csharp
public class MultiEntityPool : MultiEntityPool<string, IEntity>, IMultiEntityPool
```

- **Description:** A non-generic version of `MultiEntityPool<K, E>` that uses **string keys** to manage multiple pools of [IEntity](../Entities/IEntity.md) instances.
- **Inheritance:** [MultiEntityPool\<K, E>](MultiEntityPool%601.md), [IMultiEntityPool](IMultiEntityPool.md)
- **Note:** Useful when you need a simple key-based pool registry without specifying type parameters.

---

## 🏗️ Constructors

#### `MultiEntityPool(IMultiEntityFactory<string, IEntity>)`

```csharp
public MultiEntityPool(IMultiEntityFactory<string, IEntity> factory);
```

- **Description:** Initializes a new instance of the `MultiEntityPool` class.
- **Parameter:** `factory` — The factory registry used to create and manage entity instances.
- **Note:** The factory is required to generate new entities for each string key.

---

## 🏹 Methods

#### `Init(string, int)`

```csharp
public void Init(string key, int count);
```

- **Description:** Initializes the pool associated with the specified string key by pre-populating it with entities.
- **Parameters:**
  - `key` — The string key identifying the pool.
  - `count` — Number of entities to preallocate.

#### `Rent(string)`

```csharp
public IEntity Rent(string key);
```

- **Description:** Rents (retrieves) an entity from the pool associated with the given key. Creates a new entity if the pool is empty.
- **Parameter:** `key` — The string key identifying the pool.
- **Returns:** An `IEntity` instance.

#### `Return(IEntity)`

```csharp
public void Return(IEntity entity);
```

- **Description:** Returns a previously rented entity to its corresponding pool.
- **Parameter:** `entity` — The entity to return.
- **Note:** Safely ignores duplicate returns.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Disposes all pooled and rented entities and clears internal collections.

#### `OnCreate(IEntity)`

```csharp
protected virtual void OnCreate(IEntity entity);
```

- **Description:** Called when a new entity is created for a pool.
- **Default Behavior:** Empty; override to implement custom logic.

#### `OnDispose(IEntity)`

```csharp
protected virtual void OnDispose(IEntity entity);
```

- **Description:** Called when an entity is permanently removed from the pool.

#### `OnRent(IEntity)`

```csharp
protected virtual void OnRent(IEntity entity);
```

- **Description:** Called when an entity is rented from a pool.

#### `OnReturn(IEntity)`

```csharp
protected virtual void OnReturn(IEntity entity);
```

- **Description:** Called when an entity is returned to its pool.

---

## 🗂 Example of Usage

```csharp
// Assume we have string keys for entity types
const string Goblin = "Goblin";
const string Orc = "Orc";
```

```csharp
// Assume we have an instance of IMultiEntityFactory
IMultiEntityFactory entityFactory = ...;

// Create a non-generic multi-entity pool
var entityPool = new MultiEntityPool(entityFactory);

// Initialize pools for each enemy type
entityPool.Init(Goblin, 5);
entityPool.Init(Orc, 3);

// Rent entities from pools
IEnemyEntity goblin = entityPool.Rent(Goblin);
IEnemyEntity orc = entityPool.Rent(Orc);

// Return entities to the pool when done
entityPool.Return(goblin);
entityPool.Return(orc);
```