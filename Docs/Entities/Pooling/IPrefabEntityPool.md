# 🧩 IPrefabEntityPool

Non-generic version of [IPrefabEntityPool\<E>](IPrefabEntityPool%601.md) specialized for
base [MonoEntity](../Entities/MonoEntity.md) types. Provides a simple abstraction for working with multiple scene
entity pools, typically used for pooling and managing [MonoEntity](../Entities/MonoEntity.md) instances across
multiple scenes.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Init(MonoEntity, int)](#initMonoEntity-int)
        - [Rent(MonoEntity)](#rentMonoEntity)
        - [Rent(MonoEntity, Transform)](#rentMonoEntity-transform)
        - [Rent(MonoEntity, Vector3, Quaternion, Transform)](#rentMonoEntity-vector3-quaternion-transform)
        - [Return(MonoEntity)](#returnMonoEntity)
        - [Dispose(MonoEntity)](#disposeMonoEntity)
        - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of IPrefabEntityPool
IPrefabEntityPool enemyPool = ...;

// Initialize pools for different prefabs
enemyPool.Init(orcPrefab, 5);
enemyPool.Init(goblinPrefab, 2);

// Rent enemies from the pool
MonoEntity orc = enemyPool.Rent(orcPrefab);
MonoEntity goblin = enemyPool.Rent(goblinPrefab, parentTransform);
MonoEntity troll = enemyPool.Rent(trollPrefab, new Vector3(0,0,0), Quaternion.identity, parentTransform);

// Return enemies to the pool when done
enemyPool.Return(orc);
enemyPool.Return(goblin);
enemyPool.Return(troll);

// Optionally clear the pool for a specific prefab
enemyPool.Dispose(orcPrefab);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IPrefabEntityPool : IPrefabEntityPool<MonoEntity>
```

- **Inheritance:** [IPrefabEntityPool\<E>](IPrefabEntityPool%601.md), IDisposable

---

### 🏹 Methods

#### `Init(MonoEntity, int)`

```csharp
public void Init(MonoEntity prefab, int count);
```

- **Description:** Initializes the pool associated with the specified prefab by pre-populating it with entities.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `count` — Number of entities to preallocate.

#### `Rent(MonoEntity)`

```csharp
public MonoEntity Rent(MonoEntity prefab);
```

- **Description:** Rents an entity instance from the pool associated with the given prefab.
- **Parameter:** `prefab` — The prefab used as the key for the pool.
- **Returns:** A rented instance of the specified prefab.

#### `Rent(MonoEntity, Transform)`

```csharp
public MonoEntity Rent(MonoEntity prefab, Transform parent);
```

- **Description:** Rents an entity instance and parents it under the specified transform.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `parent` — The transform to parent the entity under.
- **Returns:** A rented and parented instance of the specified prefab.

#### `Rent(MonoEntity, Vector3, Quaternion, Transform)`

```csharp
public MonoEntity Rent(MonoEntity prefab, Vector3 position, Quaternion rotation, Transform parent = null);
```

- **Description:** Rents an entity instance with a specific position and rotation, optionally setting a parent.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `position` — The world position for the entity.
    - `rotation` — The rotation for the entity.
    - `parent` — Optional parent transform.
- **Returns:** A rented instance positioned and rotated as specified.

#### `Return(MonoEntity)`

```csharp
public void Return(MonoEntity entity);
```

- **Description:** Returns a previously rented entity to its corresponding pool.
- **Parameter:** `entity` — The entity instance to return.

#### `Dispose(MonoEntity)`

```csharp
public void Dispose(MonoEntity prefab);
```

- **Description:** Clears the pool associated with the given prefab, destroying all pooled instances.
- **Parameter:** `prefab` — The prefab whose pool should be cleared.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Clears all prefab pools and destroys all pooled entities.
