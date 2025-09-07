# 🧩️ IMultiEntityPool

The `IMultiEntityPool` interfaces provide a **multi-pool system** for managing multiple groups of `IEntity` instances, each identified by a **key**.  
This allows efficient reuse of entities while organizing them under different categories, such as types, tags, or other identifiers.

---

## Key Features

- **Generic and non-generic variants**
    - `IMultiEntityPool` uses **string keys** for general-purpose multi-pooling of `IEntity`.
    - `IMultiEntityPool<TKey, E>` supports custom key types and type-safe pooling of specific entity types.

- **Reduced allocations**
    - Entities are reused instead of being constantly created and destroyed.

- **Preallocation**
    - Each individual pool can be initialized with a number of pre-created entities to avoid runtime spikes.

- **Safe return mechanism**
    - Entities are returned to their corresponding pool, preventing cross-pool contamination.

- **Disposable**
    - Implements `IDisposable` to release all resources when the multi-pool is no longer needed.

---

## Interfaces

### Interface IMultiEntityPool

A non-generic alias for `IMultiEntityPool<string, IEntity>`.

```csharp
public interface IMultiEntityPool : IMultiEntityPool<string, IEntity> {}
```

**Usage**:  
Use this interface when pooling multiple groups of `IEntity` using string identifiers.

---

### Interface IMultiEntityPool<TKey, E>

A generic multi-pool interface that manages multiple pools of entities of type `E` indexed by a key of type `TKey`.

```csharp
public interface IMultiEntityPool<in TKey, E> : IDisposable where E : IEntity
```

**Type Parameters**:
- `TKey` — the type of key identifying individual pools.
- `E` — the entity type managed by the pools, must implement `IEntity`.

---

## Methods

| Method                           | Description                                                                                        |
|----------------------------------|----------------------------------------------------------------------------------------------------|
| `void Init(TKey key, int count)` | Initializes the pool associated with the specified key by pre-populating it with `count` entities. |
| `E Rent(TKey key)`               | Retrieves an entity from the pool identified by `key`. Creates one if the pool is empty.           |
| `void Return(E entity)`          | Returns an entity to its corresponding pool.                                                       |

---

## Notes

- **Entity Lifecycle**:  
  Ensure entities are reset or initialized between reuse if they maintain internal state.

- **Disposal Responsibility**:  
  Always call `Dispose()` to release resources and clear all internal pools.

- **Thread Safety**:  
  The pool is not thread-safe by default. Use synchronization if accessing from multiple threads.

- **Duplicate Return Warning**:  
  Returning an entity that was not rented from the pool may trigger warnings or undefined behavior.

- **Preallocation Best Practice**:  
  Use `Init()` to pre-populate pools for performance-critical systems, such as projectiles or AI units.

- **Custom Lifecycle Hooks**:  
  Implement custom behavior by overriding methods in concrete pool implementations if available.
