# 🧩 IEntityPool

```csharp
public interface IEntityPool : IEntityPool<IEntity>
```

- **Description:** Represents a **non-generic object pool** for reusing instances
  of [IEntity](../Entities/IEntity.md).  
  Provides the same pooling functionality as `IEntityPool<E>`, but operates on the base `IEntity` type.  
  Useful when type-specific access is not required.
- **Inheritance:** [IEntityPool\<E>](IEntityPool%601.md)
- **Note:** Ideal for scenarios where you want a simple pool for any entity without specifying a type parameter.

---

## 🏹 Methods

#### `Rent()`

```csharp
public IEntity Rent();
```

- **Description:** Retrieves an entity instance from the pool. If the pool is empty, a new instance may be created.
- **Returns:** An `IEntity` instance.

---

#### `Return(IEntity)`

```csharp
public void Return(IEntity entity);
```

- **Description:** Returns a previously rented entity back to the pool for reuse.
- **Parameter:** `entity` — The entity instance to return.

---

#### `Init(int)`

```csharp
public void Init(int initialCount);
```

- **Description:** Initializes the pool with a specified number of preallocated entities.
- **Parameter:** `initialCount` — Number of entities to create initially.
- **Note:** Helps reduce runtime allocations in high-frequency entity creation scenarios.

---

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Disposes of all pooled entities and releases internal resources.
- **Note:** Should be called when the pool is no longer needed to prevent memory leaks.

---

## 🗂 Example of Usage

```csharp
// Non-generic IEntityPool operating on base IEntity
IEntityPool pool = ...
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