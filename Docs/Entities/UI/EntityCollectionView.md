# 🧩 EntityCollectionView

`EntityCollectionView` is a concrete, non-generic Unity component that manually manages pooled
[EntityView](EntityView.md) instances for `IEntity` objects.

Use it when you want direct control over which entity views are active: add a view when you need it, remove it when you
are done, or clear the whole set at once.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Collection Setup](#1️⃣-collection-setup)
  - [Manual View Management](#2️⃣-manual-view-management)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Events](#-events)
    - [OnAdded](#onadded)
    - [OnRemoved](#onremoved)
  - [Properties](#-properties)
    - [Count](#count)
  - [Methods](#-methods)
    - [Get(IEntity)](#getientity)
    - [TryGet(IEntity, out EntityView)](#trygetientity-out-entityview)
    - [Contains(IEntity)](#containsientity)
    - [Add(IEntity)](#addientity)
    - [Remove(IEntity)](#removeientity)
    - [Remove(EntityView)](#removeentityview)
    - [Clear()](#clear)
    - [GetEnumerator()](#getenumerator)
    - [GetKey(IEntity)](#getkeyientity)
- [Notes](#-notes)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Collection Setup

Attach `Atomic/Entities/Entity Collection View` to a GameObject.

- Assign a `Transform` to `viewport` — active views will be parented here.
- Assign an [EntityViewPool](EntityViewPool.md) to `viewPool` — views will be rented from this pool.

---

<div id="ex2"></div>

### 2️⃣ Manual View Management

```csharp
EntityCollectionView collectionView = ...;
IEntity enemy = ...;

// Rent and activate a view for one entity:
EntityView view = collectionView.Add(enemy);

// Query active views:
if (collectionView.TryGet(enemy, out EntityView activeView))
{
    Debug.Log($"Active view: {activeView.name}");
}

// Remove one entity view and return it to the pool:
collectionView.Remove(enemy);

// Remove every active view:
collectionView.Clear();
```

For automatic synchronization with an `IReadOnlyEntityCollection<IEntity>`, use
[EntityWorldView](EntityWorldView.md).

---

## 🛠 Inspector Settings

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
public class EntityCollectionView : EntityCollectionView<string, IEntity, EntityView>
```

- **Inheritance:** [EntityCollectionView<K, E, V>](EntityCollectionView%601.md)
- **Key Strategy:** uses `entity.Name` as the view-pool key.
- **See also:** [EntityWorldView](EntityWorldView.md), [EntityViewPool](EntityViewPool.md), [EntityView](EntityView.md)

---

### ⚡ Events

#### `OnAdded`

```csharp
public event Action<IEntity, EntityView> OnAdded;
```

- **Description:** Raised after a view is rented, activated, and added to the collection.
- **Parameters:**
  - `IEntity entity` — The entity represented by the view.
  - `EntityView view` — The active view instance.

#### `OnRemoved`

```csharp
public event Action<IEntity, EntityView> OnRemoved;
```

- **Description:** Raised after the view is removed from the collection and before it is deactivated and returned to the
  pool.
- **Parameters:**
  - `IEntity entity` — The entity whose view was removed.
  - `EntityView view` — The view that is about to be returned to the pool.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** The number of active entity views currently tracked by this collection.

---

### 🏹 Methods

#### `Get(IEntity)`

```csharp
public EntityView Get(IEntity entity);
```

- **Description:** Returns the active view associated with `entity`.
- **Throws:** `KeyNotFoundException` if the entity has no active view.

#### `TryGet(IEntity, out EntityView)`

```csharp
public bool TryGet(IEntity entity, out EntityView view);
```

- **Description:** Attempts to get the active view for `entity`.
- **Returns:** `true` when a view exists; otherwise, `false`.

#### `Contains(IEntity)`

```csharp
public bool Contains(IEntity entity);
```

- **Description:** Returns `true` if the entity currently has an active view.

#### `Add(IEntity)`

```csharp
public EntityView Add(IEntity entity);
```

- **Description:** Rents and activates a view for `entity` if one does not already exist.
- **Returns:** The active view for the entity. If the entity already has a view, returns the existing view.

#### `Remove(IEntity)`

```csharp
public void Remove(IEntity entity);
```

- **Description:** Removes the active view for `entity`, deactivates it, and returns it to the pool.
- **Notes:** Does nothing if the entity has no active view.

#### `Remove(EntityView)`

```csharp
public void Remove(EntityView view);
```

- **Description:** Removes a view by using its active `Entity` reference.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all active views and returns them to the pool.

#### `GetEnumerator()`

```csharp
public Dictionary<IEntity, EntityView>.Enumerator GetEnumerator();
```

- **Description:** Iterates through active entity-view pairs.

#### `GetKey(IEntity)`

```csharp
protected override string GetKey(IEntity entity);
```

- **Description:** Returns the key used to rent a view from the pool.
- **Default Behavior:** Returns `entity.Name`.

---

## 📝 Notes

- `EntityCollectionView` is manual: it does not subscribe to an entity collection by itself.
- Use [EntityWorldView](EntityWorldView.md) when the view should automatically mirror an entity collection.
- Pool prefab keys should match `entity.Name` unless you create a custom generic collection view with a different key
  strategy.
