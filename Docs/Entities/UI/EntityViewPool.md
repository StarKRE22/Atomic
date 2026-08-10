# 🧩 EntityViewPool

A Unity-based pool manager for reusing [EntityView](EntityView.md) instances keyed by their `Name`.
This reduces memory pressure and instantiation overhead. A **non-generic wrapper** around
[EntityViewPool\<K, E, V>](EntityViewPool%603.md) with `K` fixed to `string`, `E` fixed to [IEntity](../Entities/IEntity.md),
and `V` fixed to [EntityView](EntityView.md).

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Scene Setup](#ex1)
    - [Preloading and Renting](#ex2)
- [API Reference](#-api-reference)
- [Notes](#-notes)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Scene Setup

Attach the pool component to a GameObject in the scene:

<img width="450" height="" alt="Entity component" src="../../Images/EntityViewPool.png" />

- Assign a `Transform` to `container` — all pooled views will be parented here.
- Add one or more [EntityViewCatalog](EntityViewCatalog.md) assets to `catalogs` to preload prefabs on `Awake`.

---

<div id="ex2"></div>

### 2️⃣ Preloading and Renting

```csharp
EntityViewPool pool = ...;

// Preload instances synchronously
pool.Init("Player", 10);

// Or preload asynchronously
await pool.InitAsync("Player", 10);

// Register / unregister prefabs manually
EntityView playerPrefab = ...;
pool.Register("Player", playerPrefab);
pool.Unregister("Player");

// Register / unregister whole catalogs
EntityViewCatalog catalog = ...;
pool.Register(catalog);
pool.Unregister(catalog);

// Clear all pooled instances
pool.Clear();
```

> `Rent` and `Return` are internal and are normally driven by [EntityCollectionView](EntityCollectionView.md) or
> [EntityWorldView](EntityWorldView.md). A custom pool can expose them if direct access is required.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public class EntityViewPool : EntityViewPool<string, IEntity, EntityView>
```

- **Description:** Ready-to-use pool for `EntityView` instances keyed by view name.
- **Inheritance:** [EntityViewPool\<K, E, V>](EntityViewPool%603.md), `MonoBehaviour`

---

## 📝 Notes

- The pool uses each view's `Name` property as the lookup key.
- Prefabs can be loaded automatically from assigned catalogs on `Awake`.
- Pooled views are parented to `container` and deactivated when returned.

---

## 🔗 See Also

- [EntityViewPool\<K, E, V>](EntityViewPool%603.md) — generic base class with full API.
- [EntityViewCatalog](EntityViewCatalog.md) — catalog of prefabs consumed by the pool.
- [Entity UI Manual](Manual.md)
