# 🧩️ EntityPool

The `EntityPool` classes provide a **reusable object pooling system** for managing entities (`IEntity`).  
They are designed to reduce allocations by reusing entity instances and provide **lifecycle hooks** for creation, rental, return, and disposal.

## Key Features

- **Generic and non-generic variants**
    - `EntityPool` (non-generic) works with the base `IEntity`.
    - `EntityPool<E>` (generic) provides type-safe pooling for specific entity types.

- **Efficient reuse**
    - Minimizes GC allocations by recycling entity instances.

- **Preallocation**
    - Entities can be pre-created with `Init()` to avoid runtime spikes.

- **Lifecycle hooks**
    - `OnCreate`, `OnRent`, `OnReturn`, `OnDispose` allow customization of entity behavior.

- **Safe return mechanism**
    - Prevents double returns and warns when returning unknown entities.

- **Disposable**
    - Implements `IDisposable` to release resources when no longer needed.

---

## Class EntityPool
A non-generic version bound to the base `IEntity` type.

```csharp
public class EntityPool : EntityPool<IEntity>, IEntityPool
```
### Constructors

| Constructor                                   | Description                                                                                                                                                            |
|-----------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `EntityPool(IEntityFactory<IEntity> factory)` | Initializes a new non-generic `EntityPool` for the base `IEntity` type using the provided factory. The pool will create new entities through this factory when needed. |


**Usage**:  
Use this class when pooling a variety of entities that implement `IEntity` but don’t require type-specific handling.

---

## Class EntityPool<E>
A generic entity pool that provides type-safe pooling.

```csharp
public class EntityPool<E> : IEntityPool<E> where E : IEntity
```

### Constructors

| Constructor                             | Description                                                                                                                                                                                      |
|-----------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `EntityPool(IEntityFactory<E> factory)` | Initializes a new generic `EntityPool<E>` using the provided factory. Throws `ArgumentNullException` if the factory is null. This factory is used to create new entities when the pool is empty. |


### Public Methods

| Method                  | Description                                                         |
|-------------------------|---------------------------------------------------------------------|
| `void Init(int count)`  | Pre-populates the pool with a number of entities using the factory. |
| `E Rent()`              | Retrieves an entity from the pool, or creates one if empty.         |
| `void Return(E entity)` | Returns an entity back to the pool. Warns if entity is not tracked. |
| `void Dispose()`        | Clears the pool and disposes of all entities.                       |

---

### Lifecycle Hooks

| Method                                       | Description                                    |
|----------------------------------------------|------------------------------------------------|
| `protected virtual void OnCreate(E entity)`  | Called when a new entity is created.           |
| `protected virtual void OnRent(E entity)`    | Called when an entity is rented from the pool. |
| `protected virtual void OnReturn(E entity)`  | Called when an entity is returned to the pool. |
| `protected virtual void OnDispose(E entity)` | Called when the pool is cleared or disposed.   |

---

## Notes

- **Entity Lifecycle**:  
  Ensure entities are reset between reuse if they maintain internal state.

- **Debug Safety**:  
  In Unity, attempts to return untracked entities log a warning.

- **Best Practice**:  
  Use `Init()` to preallocate entities in performance-critical systems such as projectiles, AI units, or pooled gameplay objects.


## Example Usage

### 1. Basic Pool Initialization
```csharp
// Define a simple entity
public class BulletEntity : Entity { }

// Create a factory for the entity
public class BulletFactory : ScriptableEntityFactory<BulletEntity>
{
    public override BulletEntity Create() => new BulletEntity();
}

// Initialize a pool with 20 bullets
var factory = new BulletFactory();
var pool = new EntityPool<BulletEntity>(factory);
pool.Init(20);
```
---

### 2. Renting and Returning Entities

```csharp
// Rent a bullet from the pool
BulletEntity bullet = pool.Rent();

// Use it in gameplay
bullet.Set("Damage", 10);

// Return it when done
pool.Return(bullet);
```

---

### 3. Using Lifecycle Hooks

```csharp
public class LoggingPool<E> : EntityPool<E> where E : IEntity
{
    public LoggingPool(IEntityFactory<E> factory) : base(factory) {}

    protected override void OnCreate(E entity) =>
        Debug.Log($"Created: {entity}");

    protected override void OnRent(E entity) =>
        Debug.Log($"Rented: {entity}");

    protected override void OnReturn(E entity) =>
        Debug.Log($"Returned: {entity}");

    protected override void OnDispose(E entity) =>
        Debug.Log($"Disposed: {entity}");
}
```

---

### 4. Non-generic Pool

```csharp
IEntityFactory<IEntity> factory = new SomeGenericFactory();
IEntityPool pool = new EntityPool(factory);

// Works with any IEntity
IEntity entity = pool.Rent();
pool.Return(entity);
```


## Notes

- **Entity Lifecycle**  
  Entities retrieved via `Rent()` may need to be reset or initialized before reuse, depending on their internal state or gameplay logic.

- **Disposal Responsibility**  
  Always call `Dispose()` when the pool is no longer needed to release internal resources and clear references.

- **Thread Safety**  
  The pool is not thread-safe by default. Use proper synchronization if accessing it from multiple threads.

- **Duplicate Return Warning**  
  Returning an entity that was not rented from the pool triggers a warning (in Unity, via `Debug.LogWarning`).

- **Preallocation Best Practice**  
  Use `Init()` to pre-populate the pool for performance-critical systems (e.g., bullets, projectiles, AI units) to avoid runtime allocation spikes.

- **Custom Lifecycle Hooks**  
  Override `OnCreate`, `OnDispose`, `OnRent`, and `OnReturn` to implement custom behavior when entities are created, rented, returned, or disposed.
