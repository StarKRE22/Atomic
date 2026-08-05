# 🧩 EntityWorldView

`EntityWorldView` is a concrete, non-generic Unity component that mirrors an entity collection with pooled
[EntityView](EntityView.md) instances.

Use it when you want a scene object to automatically create a view for every entity in an
`IReadOnlyEntityCollection<IEntity>` and remove that view when the entity leaves the collection.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [World View Setup](#1️⃣-world-view-setup)
  - [World View Usage](#2️⃣-world-view-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Properties](#-properties)
    - [IsActive](#isactive)
  - [Methods](#-methods)
    - [Activate(IReadOnlyEntityCollection\<IEntity>)](#activateireadonlyentitycollectionientity)
    - [Deactivate()](#deactivate)
    - [Add(IEntity)](#addientity)
    - [Remove(IEntity)](#removeientity)
    - [Clear()](#clear)
- [Notes](#-notes)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ World View Setup

Attach `Atomic/Entities/Entity Collection View` to a GameObject.

> The component menu name is shared with the collection view because `EntityWorldView` is a concrete world-binding
> specialization built on top of the same pooled-view infrastructure.

Configure the inherited collection fields:

- Assign a `Transform` to `viewport` — active views will be parented here.
- Assign an [EntityViewPool](EntityViewPool.md) to `viewPool` — views will be rented from this pool.

---

<div id="ex2"></div>

### 2️⃣ World View Usage

```csharp
// Assume we have a scene component:
EntityWorldView worldView = ...;

// Any read-only entity collection can be visualized:
IReadOnlyEntityCollection<IEntity> enemies = ...;

// Create views for current entities and subscribe to future additions/removals:
worldView.Activate(enemies);

// Query currently active views using the inherited collection API:
if (worldView.TryGet(enemyEntity, out EntityView view))
{
    Debug.Log($"Enemy view: {view.name}");
}

// Detach from the source collection and return all active views to the pool:
worldView.Deactivate();
```

While active, the world view listens to the source collection:

- `OnAdded` → rents and activates a view for the new entity.
- `OnRemoved` → deactivates and returns the entity view to the pool.

---

## 🛠 Inspector Settings

`EntityWorldView` uses the inherited collection-view settings:

| Parameter  | Description                                                                              |
|------------|------------------------------------------------------------------------------------------|
| `viewport` | The `Transform` under which active entity views will be parented.                        |
| `viewPool` | The [EntityViewPool](EntityViewPool.md) responsible for renting and returning views.     |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AddComponentMenu("Atomic/Entities/Entity Collection View")]
[DisallowMultipleComponent]
public class EntityWorldView : EntityWorldView<string, IEntity, EntityView>
```

- **Inheritance:** [EntityWorldView<K, E, V>](EntityWorldView%601.md)
- **Key Strategy:** uses `entity.Name` as the view-pool key.
- **See also:** [EntityCollectionView](EntityCollectionView.md), [EntityViewPool](EntityViewPool.md),
  [EntityView](EntityView.md)

---

### 🔑 Properties

#### `IsActive`

```csharp
public bool IsActive { get; }
```

- **Description:** Returns `true` while this world view is bound to an entity collection.

---

### 🏹 Methods

#### `Activate(IReadOnlyEntityCollection<IEntity>)`

```csharp
public void Activate(IReadOnlyEntityCollection<IEntity> source);
```

- **Description:** Binds the world view to `source`, creates views for existing entities, and subscribes to collection
  changes.
- **Parameter:** `source` — The entity collection to visualize.
- **Throws:** `ArgumentNullException` if `source` is `null`.
- **Notes:** Calling `Activate` first calls `Deactivate`, so rebinding cleans up the previous source safely.

#### `Deactivate()`

```csharp
public void Deactivate();
```

- **Description:** Unsubscribes from the current source collection and returns all active views to the pool.
- **Notes:** Safe to call even when the world view is not active.

#### `Add(IEntity)`

```csharp
public EntityView Add(IEntity entity);
```

- **Description:** Inherited manual API that rents and activates a view for a single entity.

#### `Remove(IEntity)`

```csharp
public void Remove(IEntity entity);
```

- **Description:** Inherited manual API that deactivates and returns the entity view to the pool.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Inherited manual API that removes all active views.

---

## 📝 Notes

- Use `EntityWorldView` when the source of truth is an entity collection and the view should stay synchronized with it.
- Use [EntityCollectionView](EntityCollectionView.md) when you need only manual `Add`, `Remove`, and `Clear` control.
- The default non-generic key is `entity.Name`, so prefab keys in the view pool should match entity names unless you use a
  custom generic world view.
