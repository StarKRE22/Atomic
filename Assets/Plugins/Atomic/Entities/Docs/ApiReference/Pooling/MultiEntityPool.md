# 🧩 MultiEntityPool

`MultiEntityPool` classes provide a **registry-based object pooling system** for managing multiple pools of entities (`IEntity`) identified by keys.  
These pools reduce allocations by reusing entity instances and allow customization of entity lifecycle events.

## Key Features

- **Generic and non-generic variants**
    - `MultiEntityPool` (non-generic) uses `string` keys and manages base `IEntity` objects.
    - `MultiEntityPool<TKey, E>` provides type-safe pooling for specific entity types with custom keys.

- **Keyed pooling**
    - Each pool is identified by a key (`TKey`) and maintains separate stacks of available and rented entities.

- **Lifecycle hooks**
    - `OnCreate`, `OnRent`, `OnReturn`, `OnDispose` allow customization of entity behavior during the pool lifecycle.

- **Safe rental/return**
    - Tracks rented entities to prevent duplicates and ensure correct pooling behavior.

- **Disposable**
    - Implements `IDisposable` to clear all pools and release resources.

---

## Class MultiEntityPool
A non-generic version using `string` keys and `IEntity` values.

```csharp
public class MultiEntityPool : MultiEntityPool<string, IEntity>, IMultiEntityPool
```

### Constructors

| Constructor                                                     | Description                                                                                            |
|-----------------------------------------------------------------|--------------------------------------------------------------------------------------------------------|
| `MultiEntityPool(IMultiEntityFactory<string, IEntity> factory)` | Initializes a new non-generic pool using the provided factory to create entity instances for each key. |

**Usage**:  
Use this class to manage multiple pools of entities with string identifiers, without needing type-specific access.

---

## Class MultiEntityPool<TKey, E>
A generic pool registry that manages multiple keyed entity pools.

```csharp
public class MultiEntityPool<TKey, E> : IMultiEntityPool<TKey, E> where E : IEntity
````

### Constructors

| Constructor                                             | Description                                                                                                             |
|---------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------|
| `MultiEntityPool(IMultiEntityFactory<TKey, E> factory)` | Initializes a new generic multi-pool using the provided factory. Throws `ArgumentNullException` if the factory is null. |

### Public Methods

| Method                           | Description                                                                                     |
|----------------------------------|-------------------------------------------------------------------------------------------------|
| `void Init(TKey key, int count)` | Pre-populates the pool associated with the specified key with a number of entities.             |
| `E Rent(TKey key)`               | Retrieves an entity from the pool for the given key. Creates a new entity if the pool is empty. |
| `void Return(E entity)`          | Returns an entity to its corresponding pool.                                                    |
| `void Dispose()`                 | Disposes all pooled and rented entities, clearing all pools.                                    |

### Lifecycle Hooks

| Method                                       | Description                                       |
|----------------------------------------------|---------------------------------------------------|
| `protected virtual void OnCreate(E entity)`  | Called when a new entity is created for the pool. |
| `protected virtual void OnDispose(E entity)` | Called when an entity is permanently removed.     |
| `protected virtual void OnRent(E entity)`    | Called when an entity is rented from a pool.      |
| `protected virtual void OnReturn(E entity)`  | Called when an entity is returned to its pool.    |

---

## Notes

- **Entity Lifecycle**:  
  Reset or initialize entities between reuse if they maintain internal state.

- **Debug Safety**:  
  The pool tracks rented entities to prevent duplicate returns.

- **Disposal**:  
  Always call `Dispose()` when the pool is no longer needed to release resources.

- **Preallocation Best Practice**:  
  Use `Init()` to pre-populate pools for performance-critical systems.

- **Custom Lifecycle Hooks**:  
  Override `OnCreate`, `OnDispose`, `OnRent`, and `OnReturn` to implement custom behavior for entities.

---

## Example Usage

### 1. Non-generic MultiEntityPool

```csharp
var factory = new MultiEntityFactory();
factory.Add("Enemy", new InlineEntityFactory(() => new Entity("Enemy")));

var pool = new MultiEntityPool(factory);

// Initialize a pool for key "Enemy"
pool.Init("Enemy", 10);

// Rent an entity
IEntity enemy = pool.Rent("Enemy");

// Return the entity
pool.Return(enemy);
````

---

### 2. Generic MultiEntityPool<TKey, E>

```csharp
public enum EnemyType
{
    Orc,
    Goblin
}

var factory = new MultiEntityFactory<EnemyType, EnemyEntity>();
factory.Add(EnemyType.Orc, new InlineEntityFactory<EnemyEntity>(() => new Entity("Orc")));

var pool = new MultiEntityPool<EnemyType, EnemyEntity>(factory);

// Pre-populate pool for key "EnemyType.Orc"
pool.Init(EnemyType.Orc, 5);

// Rent an entity
EnemyEntity enemy = pool.Rent(EnemyType.Orc);

// Return the entity
pool.Return(enemy);

// Dispose the pool
pool.Dispose();
````