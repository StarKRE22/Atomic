# 🧩️ IEntityCollection Extensions

Extension methods for working with [`IEntityCollection<E>`](#) that simplify adding, initializing, disposing, and managing entities.  
Provides convenient utilities for batch operations and scene-based entity handling.

---

## Key Features

- **Batch addition** – Add multiple entities at once via arrays or enumerables.
- **Scene entity management** – Instantiate or destroy `SceneEntity` objects directly from the collection (Unity only).
- **Reactive lifecycle** – Initialize or dispose all entities in the collection efficiently.
- **Optional parent assignment** – When creating entities, you can assign a parent transform.
- **Delay support** – Destroy entities with an optional delay in Unity.

---

## Methods

| Method                                                                                   | Description                                                                                                       |
|------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------|
| `AddRange(params E[] entities)`                                                          | Adds an array of entities to the collection. Throws if the array is null.                                         |
| `AddRange(IEnumerable<E> entities)`                                                      | Adds entities from an enumerable to the collection. Ignores null entities.                                        |
| `CreateEntity(E prefab, Vector3 position, Quaternion rotation, Transform parent = null)` | Instantiates a prefab as a new entity, adds it to the collection, and optionally assigns a parent. Unity only.    |
| `DestroyEntity(E entity, float delay = 0)`                                               | Removes an entity from the collection and destroys its GameObject. Optional delay before destruction. Unity only. |
| `InitEntities()`                                                                         | Calls `Init()` on every entity in the collection.                                                                 |
| `DisposeEntities()`                                                                      | Calls `Dispose()` on every entity in the collection.                                                              |

---

## Type Parameters

- `E` – The type of entity in the collection. Must implement [`IEntity`](#).  
  When using Unity-specific methods, `E` must inherit from [`SceneEntity`](#).

---

## Remarks

- Designed for **maximum efficiency** in batch operations on entity collections.
- Enables **clean and concise entity lifecycle management** across large sets of entities.
- All Unity-specific methods are wrapped with `#if UNITY_5_3_OR_NEWER` to ensure compatibility.
