- **Single Pools**
    - [IEntityPool](IEntityPool.md) <!-- + -->
    - [IEntityPool&lt;E&gt;](IEntityPool%601.md) <!-- + -->
    - [EntityPool](EntityPool.md) <!-- + -->
    - [EntityPool&lt;E&gt;](EntityPool%601.md) <!-- + -->
    - [SceneEntityPool](SceneEntityPool.md) <!-- + -->
    - [SceneEntityPool&lt;E&gt;](SceneEntityPool%601.md) <!-- + -->
- **Multi Pools**
    - [IMultiEntityPool](IMultiEntityPool.md) <!-- + -->
    - [IMultiEntityPool&lt;K, E&gt;](IMultiEntityPool%601.md) <!-- + -->
    - [MultiEntityPool](MultiEntityPool.md) <!-- + -->
    - [MultiEntityPool&lt;K, E&gt;](MultiEntityPool%601.md) <!-- + -->
- **Prefab Pools**
    - [IPrefabEntityPool](IPrefabEntityPool.md) <!-- + -->
    - [IPrefabEntityPool&lt;E&gt;](IPrefabEntityPool%601.md) <!-- + -->
    - [PrefabEntityPool](PrefabEntityPool.md)
    - [PrefabEntityPool&lt;E&gt;](PrefabEntityPool%601.md)




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
```

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
```


### Example Usage

#### Pre-initialize Pool

```csharp
PrefabEntityPool pool = ...;
SceneEntity prefab = ...;

// Pre-warm the pool with 5 instances
pool.Init(prefab, 5);
```

#### Rent and Return Entity

```csharp
// Rent entity
SceneEntity entity = pool.Rent(prefab);

// Use entity in scene...

// Return entity to the pool
pool.Return(entity);
```

#### Rent With Position, Rotation, and Parent

```csharp
Transform parent = someContainerTransform;
Vector3 position = new Vector3(0, 0, 0);
Quaternion rotation = Quaternion.identity;

SceneEntity entity = pool.Rent(prefab, position, rotation, parent);
pool.Return(entity);
```

---