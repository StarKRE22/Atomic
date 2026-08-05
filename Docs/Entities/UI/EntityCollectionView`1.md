# 🧩 EntityCollectionView<K, E, V>

`EntityCollectionView<K, E, V>` is a generic Unity base class for manually managing pooled entity views.

It rents views from an [EntityViewPool<K, E, V>](EntityViewPool%601.md), activates them for entities, tracks active
entity-view pairs, and returns views to the pool when removed.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Create a Typed Collection View](#1️⃣-create-a-typed-collection-view)
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
    - [Get(E)](#gete)
    - [TryGet(E, out V)](#trygete-out-v)
    - [Contains(E)](#containse)
    - [Add(E)](#adde)
    - [Remove(E)](#removee)
    - [Remove(V)](#removev)
    - [Clear()](#clear)
    - [GetEnumerator()](#getenumerator)
    - [GetKey(E)](#getkeye)
- [Notes](#-notes)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Create a Typed Collection View

```csharp
public interface IUnitEntity : IEntity
{
}

public sealed class UnitView : EntityView<IUnitEntity>
{
}

public sealed class UnitViewPool : EntityViewPool<string, IUnitEntity, UnitView>
{
}

public sealed class UnitCollectionView : EntityCollectionView<string, IUnitEntity, UnitView>
{
    protected override string GetKey(IUnitEntity entity)
    {
        return entity.Name;
    }
}
```

Attach `UnitCollectionView` to a GameObject and assign:

- `viewport` — parent for active views.
- `viewPool` — typed pool containing `UnitView` prefabs.

---

<div id="ex2"></div>

### 2️⃣ Manual View Management

```csharp
UnitCollectionView collectionView = ...;
IUnitEntity unit = ...;

UnitView view = collectionView.Add(unit);

if (collectionView.Contains(unit))
{
    UnitView activeView = collectionView.Get(unit);
    Debug.Log(activeView.name);
}

collectionView.Remove(unit);
collectionView.Clear();
```

For automatic synchronization with `IReadOnlyEntityCollection<IUnitEntity>`, derive from
[EntityWorldView<K, E, V>](EntityWorldView%601.md) instead.

---

## 🛠 Inspector Settings

| Parameter  | Description                                                                               |
|------------|-------------------------------------------------------------------------------------------|
| `viewport` | The `Transform` under which active entity views will be parented.                         |
| `viewPool` | The typed [EntityViewPool<K, E, V>](EntityViewPool%601.md) used to rent and return views. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityCollectionView<K, E, V> : MonoBehaviour,
    IReadOnlyCollection<KeyValuePair<E, V>>
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
  - `K` — The key type used by the view pool.
  - `E` — The entity type. Must implement [IEntity](../Entities/IEntity.md).
  - `V` — The view type. Must inherit from [EntityView<E>](EntityView%601.md).
- **Inheritance:** `MonoBehaviour`, `IReadOnlyCollection<KeyValuePair<E, V>>`
- **See also:** [EntityCollectionView](EntityCollectionView.md), [EntityWorldView<K, E, V>](EntityWorldView%601.md)

---

### ⚡ Events

#### `OnAdded`

```csharp
public event Action<E, V> OnAdded;
```

- **Description:** Raised after a view is rented, activated, and added to the collection.
- **Parameters:**
  - `E entity` — The entity represented by the view.
  - `V view` — The active view instance.

#### `OnRemoved`

```csharp
public event Action<E, V> OnRemoved;
```

- **Description:** Raised after the view is removed from the collection and before it is deactivated and returned to the
  pool.
- **Parameters:**
  - `E entity` — The entity whose view was removed.
  - `V view` — The view that is about to be returned to the pool.

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** The number of active entity views currently tracked by this collection.

---

### 🏹 Methods

#### `Get(E)`

```csharp
public V Get(E entity);
```

- **Description:** Returns the active view associated with `entity`.
- **Throws:** `KeyNotFoundException` if the entity has no active view.

#### `TryGet(E, out V)`

```csharp
public bool TryGet(E entity, out V view);
```

- **Description:** Attempts to get the active view for `entity`.
- **Returns:** `true` when a view exists; otherwise, `false`.

#### `Contains(E)`

```csharp
public bool Contains(E entity);
```

- **Description:** Returns `true` if the entity currently has an active view.

#### `Add(E)`

```csharp
public V Add(E entity);
```

- **Description:** Rents and activates a view for `entity` if one does not already exist.
- **Returns:** The active view for the entity. If the entity already has a view, returns the existing view.

#### `Remove(E)`

```csharp
public void Remove(E entity);
```

- **Description:** Removes the active view for `entity`, deactivates it, and returns it to the pool.
- **Notes:** Does nothing if the entity has no active view.

#### `Remove(V)`

```csharp
public void Remove(V view);
```

- **Description:** Removes a view by using its active `Entity` reference.

#### `Clear()`

```csharp
public void Clear();
```

- **Description:** Removes all active views and returns them to the pool.

#### `GetEnumerator()`

```csharp
public Dictionary<E, V>.Enumerator GetEnumerator();
```

- **Description:** Iterates through active entity-view pairs.

#### `GetKey(E)`

```csharp
protected abstract K GetKey(E entity);
```

- **Description:** Returns the pool key used to rent a view for `entity`.

---

## 📝 Notes

- `EntityCollectionView<K, E, V>` is manual: it does not subscribe to source collection changes by itself.
- Use [EntityWorldView<K, E, V>](EntityWorldView%601.md) when active views should automatically mirror an entity
  collection.
- Keep `GetKey(E)` stable while an entity view is active. Changing the key later does not replace the existing view.
