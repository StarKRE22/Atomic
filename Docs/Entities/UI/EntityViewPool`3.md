# 🧩 EntityViewPool\<K, E, V>

A generic Unity pool for creating, recycling, and managing [EntityView\<E>](EntityView%601.md) instances.
It preloads prefabs from [EntityViewCatalog\<E, V>](EntityViewCatalog%602.md) assets, supports synchronous and
asynchronous warm-up, and keeps instantiated views under a common container.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Custom Pool](#ex1)
    - [Preloading](#ex2)
- [Inspector Settings](#-inspector-settings)
    - [Parameters](#-parameters)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Register(K, V)](#registerk-v)
        - [Unregister(K)](#unregisterk)
        - [Register(EntityViewCatalog<E, V>)](#registerentityviewcataloge-v)
        - [Unregister(EntityViewCatalog<E, V>)](#unregisterentityviewcataloge-v)
        - [Clear()](#clear)
        - [InitAsync(K, int)](#initasynck-int)
        - [Init(K, int)](#initk-int)
        - [GetKey(V)](#getkeyv)
- [Notes](#-notes)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Custom Pool

Create a typed pool for a specific entity/view pair:

```csharp
public interface IUnitEntity : IEntity
{
}

public class UnitView : EntityView<IUnitEntity>
{
}

public sealed class UnitViewPool : EntityViewPool<string, IUnitEntity, UnitView>
{
    protected override string GetKey(UnitView view) => view.Name;
}
```

Attach it to a GameObject and assign catalogs.

---

<div id="ex2"></div>

### 2️⃣ Preloading

```csharp
UnitViewPool pool = ...;

// Synchronous warm-up
pool.Init("Knight", 20);

// Asynchronous warm-up
await pool.InitAsync("Knight", 20);
```

---

## 🛠 Inspector Settings

| Parameter         | Description                                                                                     |
|-------------------|-------------------------------------------------------------------------------------------------|
| `container`       | The parent transform under which all pooled views will be stored.                               |
| `initialCapacity` | Default capacity used when a new per-key stack is created.                                      |
| `catalogs`        | Catalogs to preload on `Awake`. All prefabs are registered automatically using `GetKey(view)`.  |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityViewPool<K, E, V> : MonoBehaviour
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
    - `K` — The key type used to identify view prefabs (commonly `string`).
    - `E` — The entity type. Must implement [IEntity](../Entities/IEntity.md).
    - `V` — The view type. Must inherit from [EntityView\<E>](EntityView%601.md).
- **Inheritance:** `MonoBehaviour`

---

### 🏹 Methods

#### `Register(K, V)`

```csharp
public void Register(K key, V prefab);
```

- **Description:** Registers a new view prefab under the specified key.
- **Parameters:**
    - `key` — The lookup key for the prefab.
    - `prefab` — The view prefab to register.

#### `Unregister(K)`

```csharp
public void Unregister(K key);
```

- **Description:** Removes a registered prefab by key.

#### `Register(EntityViewCatalog<E, V>)`

```csharp
public void Register(EntityViewCatalog<E, V> catalog);
```

- **Description:** Registers all prefabs from a catalog, using `GetKey(view)` for each entry.

#### `Unregister(EntityViewCatalog<E, V>)`

```csharp
public void Unregister(EntityViewCatalog<E, V> catalog);
```

- **Description:** Removes all prefabs that belong to the given catalog.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Destroys all pooled view instances and clears all per-key pools.

#### `InitAsync(K, int)`

```csharp
public async ValueTask InitAsync(K key, int count);
```

- **Description:** Asynchronously instantiates `count` views for the given key and pushes them into the pool.
- **Parameters:**
    - `key` — The key of the prefab to instantiate.
    - `count` — Number of instances to create.
- **Throws:** `KeyNotFoundException` if the key is not registered.

#### `Init(K, int)`

```csharp
public void Init(K key, int count);
```

- **Description:** Synchronously instantiates `count` views for the given key and pushes them into the pool.
- **Parameters:**
    - `key` — The key of the prefab to instantiate.
    - `count` — Number of instances to create.
- **Throws:** `KeyNotFoundException` if the key is not registered.

#### `GetKey(V)`

```csharp
protected abstract K GetKey(V view);
```

- **Description:** Determines the lookup key for a given view or prefab.
- **Parameter:** `view` — The view instance to extract the key from.
- **Returns:** The key used to rent and return the view.

---

## 📝 Notes

- `Rent` and `Return` are `internal`. They are invoked by collection/world-view components and can be exposed by a
  custom pool if direct control is needed.
- The pool creates a `Stack<V>` for each unique key on demand.
- Returned views are reparented to `container` and deactivated.

---

## 🔗 See Also

- [EntityViewPool](EntityViewPool.md) — non-generic wrapper keyed by view name.
- [EntityViewCatalog\<E, V>](EntityViewCatalog%602.md) — catalog consumed by the pool.
- [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md) — uses the pool to manage active views.
- [Entity UI Manual](Manual.md)
