- **Single Pools**
    - [IEntityPool](IEntityPool.md) <!-- + -->
    - [IEntityPool&lt;E&gt;](IEntityPool%601.md) <!-- + -->
    - [EntityPool](EntityPool.md) <!-- + -->
    - [EntityPool&lt;E&gt;](EntityPool%601.md) <!-- + -->
    - [SceneEntityPool](SceneEntityPool.md)
    - [SceneEntityPool&lt;E&gt;](SceneEntityPool%601.md)
- **Multi Pools**
    - [IMultiEntityPool](IMultiEntityPool.md)
    - [IMultiEntityPool&lt;E&gt;](IMultiEntityPool%601.md)
    - [MultiEntityPool](MultiEntityPool.md)
    - [MultiEntityPool&lt;E&gt;](MultiEntityPool%601.md)
- **Prefab Pools**
    - [IPrefabEntityPool](IPrefabEntityPool.md)
    - [IPrefabEntityPool&lt;E&gt;](IPrefabEntityPool%601.md)
    - [PrefabEntityPool](PrefabEntityPool.md)
    - [PrefabEntityPool&lt;E&gt;](PrefabEntityPool%601.md)




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
