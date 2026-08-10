# 🧩 EntityCollectionView

A component that manually manages active [EntityView](EntityView.md) instances for [IEntity](../Entities/IEntity.md)
objects. It rents views from an [EntityViewPool](EntityViewPool.md), tracks entity-view mappings, and returns views
to the pool when they are removed. A **non-generic wrapper** around
[EntityCollectionView\<K, E, V>](EntityCollectionView%603.md) keyed by `entity.Name`.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Scene Setup](#ex1)
    - [Managing Views](#ex2)
- [API Reference](#-api-reference)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Scene Setup

Attach `Atomic/Entities/Entity Collection View` to a GameObject:

<img width="450" height="" alt="Entity component" src="../../Images/EntityCollectionView.png" />

- Assign a `Transform` to `viewport` — spawned views will be parented here.
- Assign the [EntityViewPool](EntityViewPool.md) to `pool`.

---

<div id="ex2"></div>

### 2️⃣ Managing Views

```csharp
EntityCollectionView collectionView = ...;
IEntity someEntity = ...;

// Add a view for an entity
EntityView createdView = collectionView.Add(someEntity);

// Remove a specific entity view
collectionView.Remove(someEntity);

// Remove by view instance
collectionView.Remove(createdView);

// Clear all active views
collectionView.Clear();

// Querying
bool exists = collectionView.Contains(someEntity);
if (collectionView.TryGet(someEntity, out EntityView view))
{
    Debug.Log($"Found view for {someEntity}: {view.name}");
}
EntityView directView = collectionView.Get(someEntity);

// Iteration
foreach (KeyValuePair<IEntity, EntityView> pair in collectionView)
{
    Debug.Log($"Entity: {pair.Key}, View: {pair.Value.name}");
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AddComponentMenu("Atomic/Entities/Entity Collection View")]
[DisallowMultipleComponent]
public class EntityCollectionView : EntityCollectionView<string, IEntity, EntityView>
```

- **Description:** Ready-to-use collection view for `IEntity` / `EntityView` pairs.
- **Inheritance:** [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md), `MonoBehaviour`

---

## 🔗 See Also

- [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md) — generic base class with full API.
- [EntityWorldView](EntityWorldView.md) — auto-synchronizing version bound to an entity collection.
- [Entity UI Manual](Manual.md)
