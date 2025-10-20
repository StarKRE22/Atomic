# 🧩 IPrefabEntityPool

Non-generic version of [IPrefabEntityPool\<E>](IPrefabEntityPool%601.md) specialized for
base [SceneEntity](../Entities/SceneEntity.md) types. Provides a simple abstraction for working with multiple scene
entity pools, typically used for pooling and managing [SceneEntity](../Entities/SceneEntity.md) instances across
multiple scenes.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Init(SceneEntity, int)](#initsceneentity-int)
        - [Rent(SceneEntity)](#rentsceneentity)
        - [Rent(SceneEntity, Transform)](#rentsceneentity-transform)
        - [Rent(SceneEntity, Vector3, Quaternion, Transform)](#rentsceneentity-vector3-quaternion-transform)
        - [Return(SceneEntity)](#returnsceneentity)
        - [Dispose(SceneEntity)](#disposesceneentity)
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
SceneEntity orc = enemyPool.Rent(orcPrefab);
SceneEntity goblin = enemyPool.Rent(goblinPrefab, parentTransform);
SceneEntity troll = enemyPool.Rent(trollPrefab, new Vector3(0,0,0), Quaternion.identity, parentTransform);

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
public interface IPrefabEntityPool : IPrefabEntityPool<SceneEntity>
```

- **Inheritance:** [IPrefabEntityPool\<E>](IPrefabEntityPool%601.md), IDisposable

---

### 🏹 Methods

#### `Init(SceneEntity, int)`

```csharp
public void Init(SceneEntity prefab, int count);
```

- **Description:** Initializes the pool associated with the specified prefab by pre-populating it with entities.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `count` — Number of entities to preallocate.

#### `Rent(SceneEntity)`

```csharp
public SceneEntity Rent(SceneEntity prefab);
```

- **Description:** Rents an entity instance from the pool associated with the given prefab.
- **Parameter:** `prefab` — The prefab used as the key for the pool.
- **Returns:** A rented instance of the specified prefab.

#### `Rent(SceneEntity, Transform)`

```csharp
public SceneEntity Rent(SceneEntity prefab, Transform parent);
```

- **Description:** Rents an entity instance and parents it under the specified transform.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `parent` — The transform to parent the entity under.
- **Returns:** A rented and parented instance of the specified prefab.

#### `Rent(SceneEntity, Vector3, Quaternion, Transform)`

```csharp
public SceneEntity Rent(SceneEntity prefab, Vector3 position, Quaternion rotation, Transform parent = null);
```

- **Description:** Rents an entity instance with a specific position and rotation, optionally setting a parent.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `position` — The world position for the entity.
    - `rotation` — The rotation for the entity.
    - `parent` — Optional parent transform.
- **Returns:** A rented instance positioned and rotated as specified.

#### `Return(SceneEntity)`

```csharp
public void Return(SceneEntity entity);
```

- **Description:** Returns a previously rented entity to its corresponding pool.
- **Parameter:** `entity` — The entity instance to return.

#### `Dispose(SceneEntity)`

```csharp
public void Dispose(SceneEntity prefab);
```

- **Description:** Clears the pool associated with the given prefab, destroying all pooled instances.
- **Parameter:** `prefab` — The prefab whose pool should be cleared.

#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Clears all prefab pools and destroys all pooled entities.
