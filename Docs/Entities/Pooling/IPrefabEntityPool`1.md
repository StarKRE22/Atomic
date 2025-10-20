# 🧩 IPrefabEntityPool\<E>

Manages **multiple scene entity pools**, each associated with a specific prefab. Provides centralized methods for
renting and returning entities across those pools.
Useful when you want prefab-based pooling with automatic instantiation and reuse in a Unity scene.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Init(E, int)](#inite-int)
        - [Rent(E)](#rente)
        - [Rent(E, Transform)](#rente-transform)
        - [Rent(E, Vector3, Quaternion, Transform)](#rente-vector3-quaternion-transform)
        - [Return(E)](#returne)
        - [Dispose(E)](#disposee)
        - [Dispose()](#dispose)

---

## 🗂 Example of Usage

```csharp
// Assume we have a scene entity prefabs
public class GameEntity : SceneEntity
{
}
```

```csharp
// Assume we have an instance of IPrefabEntityPool<GameEntity>
IPrefabEntityPool<GameEntity> entityPool = ...;

// Initialize pool for a specific prefab
entityPool.Init(orcPrefab, 5);
entityPool.Init(goblinPrefab, 2);

GameEntity orc = enemyPool.Rent(orcPrefab);
GameEntity goblin = enemyPool.Rent(goblinPrefab, parentTransform);
GameEntity troll = enemyPool.Rent(trollPrefab, new Vector3(0,0,0), Quaternion.identity, parentTransform);

// Return enemies to the pool when done
enemyPool.Return(orc);
enemyPool.Return(goblin);
enemyPool.Return(troll);

// Optionally clear the pool for this prefab
enemyPool.Dispose(orc);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IPrefabEntityPool<E> : IDisposable where E : SceneEntity
```

- **Type Parameter:** `E` — The type of scene entity being pooled. Must inherit
  from [SceneEntity](../Entities/SceneEntity.md).
- **Inheritance:** `IDisposable`

---

### 🏹 Methods

#### `Init(E, int)`

```csharp
public void Init(E prefab, int count);
```

- **Description:** Initializes the pool associated with the specified prefab by pre-populating it with entities.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `count` — Number of entities to preallocate.

#### `Rent(E)`

```csharp
public E Rent(E prefab);
```

- **Description:** Rents an entity instance from the pool associated with the given prefab.
- **Parameter:** `prefab` — The prefab used as the key for the pool.
- **Returns:** A rented instance of the specified prefab.

#### `Rent(E, Transform)`

```csharp
public E Rent(E prefab, Transform parent);
```

- **Description:** Rents an entity instance and parents it under the specified transform.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `parent` — The transform to parent the entity under.
- **Returns:** A rented and parented instance of the specified prefab.

#### `Rent(E, Vector3, Quaternion, Transform)`

```csharp
public E Rent(E prefab, Vector3 position, Quaternion rotation, Transform parent = null);
```

- **Description:** Rents an entity instance with a specific position and rotation, optionally setting a parent.
- **Parameters:**
    - `prefab` — The prefab used as the key for the pool.
    - `position` — The world position for the entity.
    - `rotation` — The rotation for the entity.
    - `parent` — Optional parent transform.
- **Returns:** A rented instance positioned and rotated as specified.

#### `Return(E)`

```csharp
public void Return(E entity);
```

- **Description:** Returns a previously rented entity to its corresponding pool.
- **Parameter:** `entity` — The entity instance to return.

#### `Dispose(E)`

```csharp
public void Dispose(E prefab);
```

- **Description:** Clears the pool associated with the given prefab, destroying all pooled instances.
- **Parameter:** `prefab` — The prefab whose pool should be cleared.


#### `Dispose()`

```csharp
public void Dispose();
```

- **Description:** Clears all prefab pools and destroys all pooled entities.
