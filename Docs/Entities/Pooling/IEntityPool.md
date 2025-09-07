# 🧩️ IEntityPool

The `IEntityPool` interfaces define a **pooling system** for reusing `IEntity` instances, reducing allocation overhead and improving runtime performance in **Entity-based architectures**.  
They provide both a **non-generic alias** (`IEntityPool`) and a **generic version** (`IEntityPool<E>`) for type-safe pooling.

## Key Features

- **Generic and non-generic support**
    - `IEntityPool` for general-purpose pooling of `IEntity`.
    - `IEntityPool<E>` for type-safe pooling of specific entity types.

- **Reduced allocations**
    - Entities are reused instead of being constantly created and destroyed.

- **Preallocation**
    - Pools can be initialized with a number of pre-created entities to avoid runtime spikes.

- **Safe return mechanism**
    - Entities can be returned back into the pool for consistent lifecycle management.

- **Disposable**
    - Pools implement `IDisposable`, ensuring resources can be properly released.


---

## Interfaces

### Interface IEntityPool
A non-generic alias bound to the base `IEntity` type.

```csharp
public interface IEntityPool : IEntityPool<IEntity>
```

**Usage**:  
Use this interface when you don’t need type-specific entity access and want a pool that can work with any `IEntity`.

---

### Interface IEntityPool&lt;E&gt;
Represents a reusable pool for entity instances of type `E`.

```csharp
public interface IEntityPool<E> : IDisposable where E : IEntity
```

---

## Methods

| Method                        | Description                                                                    |
|-------------------------------|--------------------------------------------------------------------------------|
| `E Rent()`                    | Retrieves an entity instance from the pool. Creates one if none are available. |
| `void Return(E entity)`       | Returns an entity back to the pool for future reuse.                           |
| `void Init(int initialCount)` | Preallocates a specified number of entities in the pool.                       |

## Notes

- **Entity Lifecycle**:  
  Entities retrieved via `Rent()` may need to be reset before reuse, depending on the implementation.

- **Disposal Responsibility**:  
  Always call `Dispose()` when the pool is no longer needed to release internal resources and references.

- **Best Practice**:  
  Use **preallocation** with `Init()` for performance-critical systems (e.g., spawning bullets, units, or projectiles) to avoid runtime allocation spikes.

- **Implementation Detail**:  
  The interfaces do not define how entities are created—implementations may use factories, constructors, or cloning strategies.  
