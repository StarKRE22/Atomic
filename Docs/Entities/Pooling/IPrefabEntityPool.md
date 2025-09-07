# 🧩 IPrefabEntityPool

`IPrefabEntityPool` provides an **abstraction for multi-scene entity pooling** in Unity.  
It allows centralized management of `SceneEntity` instances across multiple pools keyed by prefabs.

---

## Key Features

- **Non-generic and generic variants**
    - `IPrefabEntityPool` is a non-generic alias for `SceneEntity`.
    - `IPrefabEntityPool<E>` allows pooling for any `SceneEntity` subtype.

- **Prefab-based pooling**
    - Each pool is associated with a prefab and manages instantiation, reuse, and disposal.

- **Flexible rental methods**
    - Entities can be rented with optional parent transforms, world positions, and rotations.

- **Centralized disposal**
    - Pools can be individually cleared, or the entire system disposed to release resources.

---

## Interface IPrefabEntityPool
Non-generic interface for `SceneEntity` pooling.

```csharp
public interface IPrefabEntityPool : IPrefabEntityPool<SceneEntity>
```

**Remarks:**  
Use this interface when you need a centralized, non-generic API for managing scene entity pools.

---

## Interface IPrefabEntityPool<E>
Generic interface for pooling scene-bound entities.

```csharp
public interface IPrefabEntityPool<E> : IDisposable where E : SceneEntity
````

### Public Methods

| Method                                                                             | Description                                                                       |
|------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------|
| `void Init(E prefab, int count)`                                                   | Pre-populates the pool for the given prefab with a specified number of instances. |
| `E Rent(E prefab)`                                                                 | Rents an entity from the pool associated with the prefab.                         |
| `E Rent(E prefab, Transform parent)`                                               | Rents an entity and parents it under the specified transform.                     |
| `E Rent(E prefab, Vector3 position, Quaternion rotation, Transform parent = null)` | Rents an entity with a specific world position, rotation, and optional parent.    |
| `void Return(E entity)`                                                            | Returns a rented entity to its corresponding pool.                                |
| `void Dispose(E prefab)`                                                           | Clears the pool for a given prefab, destroying all pooled instances.              |

---

## Example Usage

### Rent and Return an Entity

```csharp
IPrefabEntityPool pool = ...;
SceneEntity prefab = ...;

// Initialize pool
pool.Init(prefab, 5);

// Rent an entity
SceneEntity entity = pool.Rent(prefab);

// Return it later
pool.Return(entity);
````

### Rent With Position and Parent

```csharp
Transform parent = someContainerTransform;
Vector3 position = new Vector3(0, 0, 0);
Quaternion rotation = Quaternion.identity;

// Rent an entity
SceneEntity entity = pool.Rent(prefab, position, rotation, parent);

// Return it later
pool.Return(entity);
````

## Notes

- **Entity Lifecycle:**  
  Entities rented from a prefab pool may require initialization or resetting before reuse.

- **Thread Safety:**  
  Pools are not inherently thread-safe. Synchronize access if using multiple threads.

- **Custom Parenting and Positioning:**  
  Use `Rent` overloads with `Transform`, `Vector3`, and `Quaternion` to control entity placement in the scene.

- **Disposal:**  
  Always call `Dispose` for prefabs or the entire pool system to prevent memory leaks and orphaned GameObjects.

---