# 🧩 Entity Pools

**Entity Pools** are responsible for managing and reusing instances of [IEntity](../Entities/IEntity.md) or its subclasses.  
Pools reduce allocations, improve performance, and provide structured ways to manage entity lifecycles.

Pools can be **single**, **multi-keyed**, or **prefab-based**, depending on whether you need a single pool, multiple pools keyed by a string or type, or prefab-specific pooling in Unity scenes.

There are interfaces and implementations of pool depending on scenario:

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
  - [PrefabEntityPool](PrefabEntityPool.md) <!-- + -->
  - [PrefabEntityPool&lt;E&gt;](PrefabEntityPool%601.md) <!-- + -->

---

## 🗂 Examples of Usage

Below are several examples of using different pools:

### 1️⃣ Entity Pool

```csharp
IEntityFactory factory = ...
EntityPool pool = new(factory);

// Initialize pool
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

---

### 2️⃣ Multi Entity Pool

```csharp
// Assume we have string keys for entity types
const string Goblin = "Goblin";
const string Orc = "Orc";

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

---

### 3️⃣ Prefab Pool

```csharp
PrefabEntityPool<EnemyEntity> prefabPool = ...;

// Initialize pool for specific prefab
prefabPool.Init(orcPrefab, 5);
prefabPool.Init(goblinPrefab, 3);

// Rent entities with positions and parent
EnemyEntity orc = prefabPool.Rent(orcPrefab, new Vector3(0,0,0), Quaternion.identity, parentTransform);

// Return entities
prefabPool.Return(orc);

// Dispose specific prefab pool
prefabPool.Dispose(orcPrefab);

// Dispose all prefab pools
prefabPool.Dispose();
```

- **Description:** Scene-based pooling using Unity prefabs. Supports multiple prefab types, lazy pool creation, and automatic activation/deactivation.

---

## 📝 Notes

- **Single Pools** are simple and type-specific. Use when you only need one entity type.
- **Multi Pools** allow dynamic creation and management of multiple entity types by key.
- **Prefab Pools** are optimized for Unity scenes where entities are instantiated from prefabs and reused.
- **All pools** provide `Rent()` and `Return()` methods, reducing GC overhead and improving runtime performance.
- **Prefab and Scene pools** additionally support `Init()` for pre-warming, and `Dispose()` for cleanup.
- **Generic versions** provide type safety; non-generic versions work with `IEntity` and allow heterogeneous usage.
