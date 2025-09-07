# 🧩️ SceneEntityPool

The `SceneEntityPool` classes provide a **Unity MonoBehaviour-based pooling system** for managing scene-bound entities (`SceneEntity`).  
They are designed to **pre-instantiate entities**, **reuse them efficiently**, and handle **activation/deactivation** automatically.

There are **generic (`SceneEntityPool<E>`)** and **non-generic (`SceneEntityPool`)** variants.  
The non-generic variant operates on the base `SceneEntity` type and implements `IEntityPool` for compatibility with systems expecting non-generic access.

---

## Key Features

- **Generic and non-generic support**
    - `SceneEntityPool<E>` for type-safe pooling of specific `SceneEntity` subclasses.
    - `SceneEntityPool` for general-purpose pooling of `SceneEntity`.

- **Prefab-based instantiation**
    - Entities are created from a prefab, allowing configuration in the Unity Editor.

- **Automatic activation/deactivation**
    - Entities are automatically activated on `Rent()` and deactivated on `Return()`.

- **Preallocation**
    - Pool can pre-instantiate entities via `Init(int)` or on `Awake()` using `_initialCount`.

- **Safe return mechanism**
    - Prevents returning entities that were not rented and logs warnings if attempted.

- **Disposable**
    - Implements `Dispose()` to destroy all pooled and rented entities.

- **Lifecycle hooks**
    - `OnCreate`, `OnRent`, `OnReturn`, `OnDispose` allow customization of entity behavior.

- **Editor-friendly**
    - Fields exposed in the Unity Inspector for prefab, container, initial count, and auto-initialization.

---

## Class SceneEntityPool

A non-generic pool for base `SceneEntity`.

```csharp
public class SceneEntityPool : SceneEntityPool<SceneEntity>, IEntityPool
```

### Static Methods

| Method                                           | Description                                                                                                    |
|--------------------------------------------------|----------------------------------------------------------------------------------------------------------------|
| `static SceneEntityPool Create(CreateArgs args)` | Creates a new non-generic pool in the scene with the specified prefab, container, and initialization settings. |

---

## Class SceneEntityPool<E>

A generic pool for scene-bound entities of type `E`.

```csharp
public abstract class SceneEntityPool<E> : MonoBehaviour, IEntityPool<E> where E : SceneEntity
```

### Inspector Fields

| Field          | Type        | Description                                                            |
|----------------|-------------|------------------------------------------------------------------------|
| `initOnAwake`  | `bool`      | If `true`, pool is initialized automatically in `Awake()`.             |
| `initialCount` | `int`       | Number of entities to pre-instantiate when initializing.               |
| `prefab`       | `E`         | Prefab used to create pooled entities.                                 |
| `container`    | `Transform` | Parent transform for pooled entities. Defaults to the pool GameObject. |

### Public Methods

| Method                  | Description                                                                                                 |
|-------------------------|-------------------------------------------------------------------------------------------------------------|
| `void Init(int count)`  | Pre-instantiates `count` entities and adds them to the pool.                                                |
| `E Rent()`              | Retrieves an entity from the pool. Creates a new one if empty. Activates it automatically.                  |
| `void Return(E entity)` | Returns an entity to the pool. Deactivates it and re-parents to `_container`. Warns if entity is untracked. |
| `void Dispose()`        | Destroys all pooled and rented entities, clearing the internal collections.                                 |

### Lifecycle Hooks

| Method                                       | Description                                                                           |
|----------------------------------------------|---------------------------------------------------------------------------------------|
| `protected virtual void OnCreate(E entity)`  | Called when a new entity is created. Default behavior: deactivate entity.             |
| `protected virtual void OnRent(E entity)`    | Called when an entity is rented. Default behavior: activate entity.                   |
| `protected virtual void OnReturn(E entity)`  | Called when an entity is returned. Default behavior: deactivate and re-parent entity. |
| `protected virtual void OnDispose(E entity)` | Called when an entity is destroyed during pool disposal.                              |

### Static Methods

| Method                                                      | Description                                                                                                                   |
|-------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------|
| `static T Create<T>(CreateArgs args)`                       | Creates a new pool of type `T` in the scene with the specified prefab, container, initial count, and initialization settings. |
| `static void Destroy(SceneEntityPool<E> pool, float t = 0)` | Disposes the pool and destroys its GameObject after `t` seconds.                                                              |

### CreateArgs

| Field          | Type        | Description                                   |
|----------------|-------------|-----------------------------------------------|
| `name`         | `string`    | Name of the new GameObject for the pool.      |
| `prefab`       | `E`         | Prefab to instantiate entities from.          |
| `container`    | `Transform` | Parent transform for pooled entities.         |
| `initOnAwake`  | `bool`      | If `true`, initializes the pool on `Awake()`. |
| `initialCount` | `int`       | Number of entities to pre-instantiate.        |

---

## Example Usage

### 1. Creating and Initializing a Pool

```csharp
var poolArgs = new SceneEntityPool.CreateArgs
{
    name = "EnemyPool",
    prefab = enemyPrefab,
    container = parentTransform,
    initOnAwake = true,
    initialCount = 10
};

var enemyPool = SceneEntityPool.Create(in poolArgs);
```

---

### 2. Renting and Returning Entities

```csharp
SceneEntity enemy = enemyPool.Rent();

// Use the enemy in gameplay...

enemyPool.Return(enemy);
```

---

### 3. Disposing the Pool

```csharp
SceneEntityPool.Destroy(enemyPool);
```
---

## Notes

- **Entity Lifecycle**  
  Entities retrieved via `Rent()` may need to be reset before reuse depending on internal state or gameplay logic.

- **Disposal Responsibility**  
  Call `Dispose()` when the pool is no longer needed to release references and destroy entities.

- **Thread Safety**  
  Not thread-safe by default. Synchronize access if used across threads.

- **Duplicate Return Warning**  
  Returning an entity that was not rented logs a warning via `Debug.LogWarning`.

- **Preallocation Best Practice**  
  Use `Init()` or `_initialCount` for preloading entities in performance-critical systems.

- **Custom Lifecycle Hooks**  
  Override `OnCreate`, `OnRent`, `OnReturn`, `OnDispose` for custom behavior.
