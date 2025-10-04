# 🧩 PrefabEntityPool

`PrefabEntityPool` provides a **multi-prefab pooling system for Unity `SceneEntity` objects**.  
It allows efficient reuse of scene entities across multiple scenes, supporting centralized renting, returning, and disposal.

---

## Key Features

- **Non-generic and generic variants**
    - `PrefabEntityPool` (non-generic) for base `SceneEntity`.
    - `PrefabEntityPool<E>` for any `SceneEntity` subtype.

- **Prefab-based pools**
    - Each pool corresponds to a prefab and tracks its own stack of instances.
    - Prefab names are sanitized to group entities correctly.

- **Flexible renting**
    - Rent entities with optional parent, position, and rotation.

- **Pre-initialization**
    - Warm up pools using `Init(prefab, count)`.

- **Automatic entity lifecycle management**
    - Entities are activated on rent, deactivated on return, and properly disposed on pool destruction.

- **Scene persistence**
    - Optional `DontDestroyOnLoad` support for persistent pools across scenes.

---

## Class `PrefabEntityPool`
Non-generic entry point for pooling `SceneEntity` instances.

```csharp
[AddComponentMenu("Atomic/Entities/Prefab Entity Pool")]
[DisallowMultipleComponent]
public class PrefabEntityPool : PrefabEntityPool<SceneEntity>, IPrefabEntityPool
```
**Remarks:**  
Use this class when you don't need a generic type and just want to pool base `SceneEntity` objects.

---

## Class `PrefabEntityPool<E>`
Generic pool class for scene entities.

```csharp
public abstract class PrefabEntityPool<E> : MonoBehaviour, IPrefabEntityPool<E> where E : SceneEntity
````

### Inspector Settings

| Field               | Description                                                                                                                                         |
|---------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| `container`         | The root transform under which all pooled entities will be parented. If not assigned, defaults to the GameObject this script is attached to.        |
| `dontDestroyOnLoad` | If `true`, the pool GameObject will persist across scene changes using `DontDestroyOnLoad`. Otherwise, it will be destroyed when the scene unloads. |


### Public Methods

| Method                                                                             | Description                                                                                                             |
|------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------|
| `void Init(E prefab, int count)`                                                   | Pre-instantiate a number of entities for a prefab, adding them to the pool in inactive state.                           |
| `E Rent(E prefab)`                                                                 | Rent an entity from the pool at the origin with identity rotation. If none are available, a new instance is created.    |
| `E Rent(E prefab, Transform parent)`                                               | Rent an entity and parent it under the specified transform, preserving parent's position and rotation.                  |
| `E Rent(E prefab, Vector3 position, Quaternion rotation, Transform parent = null)` | Rent an entity from the pool or create a new one, set its position, rotation, and optional parent.                      |
| `void Return(E entity)`                                                            | Return a previously rented entity to its corresponding pool. If the entity is already in the pool, the call is ignored. |
| `void Dispose(E prefab)`                                                           | Clear and destroy all entities in the pool associated with the specified prefab, including its container.               |
| `void Dispose()`                                                                   | Clear all pools and destroy all pooled entities across all prefabs.                                                     |

---

### Protected Hooks

| Method                    | Description                                                                                                                            |
|---------------------------|----------------------------------------------------------------------------------------------------------------------------------------|
| `OnCreate(E entity)`      | Called when a new entity instance is created. Default behavior: deactivates the entity.                                                |
| `OnRent(E entity)`        | Called when an entity is rented. Default behavior: activates the entity.                                                               |
| `OnReturn(E entity)`      | Called when an entity is returned. Default behavior: deactivates the entity.                                                           |
| `OnDispose(E entity)`     | Called when an entity is destroyed, either via `Dispose(E prefab)` or `Dispose()`. Override to release resources or unregister events. |
| `GetEntityName(E entity)` | Returns a clean name for the prefab or entity, stripping Unity's automatic suffixes like " (1)". Used as a key for internal pooling.   |

---


### Notes

- Pools are **created lazily** on first rent or init.
- Returned entities are **reparented** to their internal container to keep hierarchy clean.
- `dontDestroyOnLoad` allows pools to persist across scene changes.
- Override **protected hooks** to customize entity behavior on create, rent, return, or dispose.

