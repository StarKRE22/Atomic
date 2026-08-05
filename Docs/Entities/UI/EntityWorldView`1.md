# 🧩 EntityWorldView<K, E, V>

`EntityWorldView<K, E, V>` is a generic base class that binds an entity collection to pooled Unity views.

It extends [EntityCollectionView<K, E, V>](EntityCollectionView%601.md) with source-collection binding: when entities are
added to the source, views are rented and activated; when entities are removed, views are deactivated and returned to the
pool.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Create Typed View Components](#1️⃣-create-typed-view-components)
  - [Bind to an Entity Collection](#2️⃣-bind-to-an-entity-collection)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Properties](#-properties)
    - [IsActive](#isactive)
  - [Methods](#-methods)
    - [Activate(IReadOnlyEntityCollection\<E>)](#activateireadonlyentitycollectione)
    - [Deactivate()](#deactivate)
    - [GetKey(E)](#getkeye)
- [Notes](#-notes)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Create Typed View Components

Define a typed entity contract and view:

```csharp
public interface IUnitEntity : IEntity
{
}

public sealed class UnitView : EntityView<IUnitEntity>
{
}
```

Create a typed pool:

```csharp
public sealed class UnitViewPool : EntityViewPool<string, IUnitEntity, UnitView>
{
}
```

Create a concrete world view and define how entities map to pool keys:

```csharp
public sealed class UnitWorldView : EntityWorldView<string, IUnitEntity, UnitView>
{
    protected override string GetKey(IUnitEntity entity)
    {
        return entity.Name;
    }
}
```

Attach `UnitWorldView` to a GameObject and assign:

- `viewport` — parent for active views.
- `viewPool` — pool that contains `UnitView` prefabs.

---

<div id="ex2"></div>

### 2️⃣ Bind to an Entity Collection

```csharp
UnitWorldView worldView = ...;
IReadOnlyEntityCollection<IUnitEntity> units = ...;

// Show existing units and keep the view synchronized with future changes:
worldView.Activate(units);

// The inherited collection API is still available:
foreach (KeyValuePair<IUnitEntity, UnitView> item in worldView)
{
    IUnitEntity entity = item.Key;
    UnitView view = item.Value;
    Debug.Log($"{entity.Name}: {view.name}");
}

// Stop synchronization and return all views to the pool:
worldView.Deactivate();
```

---

## 🛠 Inspector Settings

`EntityWorldView<K, E, V>` uses the inherited collection-view settings:

| Parameter  | Description                                                                                 |
|------------|---------------------------------------------------------------------------------------------|
| `viewport` | The `Transform` under which active entity views will be parented.                           |
| `viewPool` | The typed [EntityViewPool<K, E, V>](EntityViewPool%601.md) used to rent and return views.   |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityWorldView<K, E, V> : EntityCollectionView<K, E, V>
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
  - `K` — The key type used by the view pool.
  - `E` — The entity type. Must implement [IEntity](../Entities/IEntity.md).
  - `V` — The view type. Must inherit from [EntityView<E>](EntityView%601.md).
- **Inheritance:** [EntityCollectionView<K, E, V>](EntityCollectionView%601.md)
- **See also:** [EntityWorldView](EntityWorldView.md), [EntityViewPool<K, E, V>](EntityViewPool%601.md)

---

### 🔑 Properties

#### `IsActive`

```csharp
public bool IsActive { get; }
```

- **Description:** Returns `true` when this world view has an active source collection.

---

### 🏹 Methods

#### `Activate(IReadOnlyEntityCollection<E>)`

```csharp
public void Activate(IReadOnlyEntityCollection<E> source);
```

- **Description:** Binds this world view to `source`, creates views for existing entities, and subscribes to future
  additions and removals.
- **Parameter:** `source` — The entity collection to visualize.
- **Throws:** `ArgumentNullException` if `source` is `null`.
- **Notes:** Calling `Activate` always deactivates the previous source first.

#### `Deactivate()`

```csharp
public void Deactivate();
```

- **Description:** Clears all active views, unsubscribes from the source collection, and marks this world view inactive.
- **Notes:** This method is idempotent and can be called when no source is bound.

#### `GetKey(E)`

```csharp
protected abstract K GetKey(E entity);
```

- **Description:** Inherited key resolver used when renting a view from the pool.
- **Parameter:** `entity` — The entity that needs a view.
- **Returns:** The pool key used to select the prefab/view type.

---

## 📝 Notes

- `EntityWorldView<K, E, V>` owns synchronization with the source collection; do not subscribe to the same source just to
  mirror add/remove operations manually.
- Use a stable `GetKey(E)` implementation. If the key changes while an entity is active, the existing view is not replaced
  automatically.
- Call `Deactivate()` when the visualized source is no longer needed, especially for temporary screens or scene-local UI.
