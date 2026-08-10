# 🧩 EntityWorldView\<K, E, V>

A generic component that automatically mirrors an [IReadOnlyEntityCollection\<E>](../Collections/IReadOnlyEntityCollection%601.md)
with pooled [EntityView\<E>](EntityView%601.md) instances. It creates views for existing entities and keeps them
synchronized when entities are added or removed.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Custom World View](#ex1)
    - [Activation and Deactivation](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [IsActive](#isactive)
    - [Methods](#-methods)
        - [Activate(IReadOnlyEntityCollection<E>)](#activateireadonlyentitycollectione)
        - [Deactivate()](#deactivate)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Custom World View

Create a typed world view for a specific entity/view pair:

```csharp
public interface IUnitEntity : IEntity
{
}

public class UnitView : EntityView<IUnitEntity>
{
}

public sealed class UnitWorldView : EntityWorldView<string, IUnitEntity, UnitView>
{
    protected override string GetKey(IUnitEntity entity) => entity.Name;
}
```

Attach it to a GameObject and assign a pool.

---

<div id="ex2"></div>

### 2️⃣ Activation and Deactivation

```csharp
UnitWorldView worldView = ...;
IReadOnlyEntityCollection<IUnitEntity> entityCollection = ...;

// Create views for existing entities and subscribe to additions/removals:
worldView.Activate(entityCollection);

// Stop synchronization and return all views to the pool:
worldView.Deactivate();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityWorldView<K, E, V> : EntityCollectionView<K, E, V>
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
    - `K` — The key type used to select a prefab from the pool.
    - `E` — The entity type. Must implement [IEntity](../Entities/IEntity.md).
    - `V` — The view type. Must inherit from [EntityView\<E>](EntityView%601.md).
- **Inheritance:** [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md), `MonoBehaviour`

---

### 🔑 Properties

#### `IsActive`

```csharp
public bool IsActive { get; }
```

- **Description:** `true` while the world view is bound to an entity collection source.

---

### 🏹 Methods

#### `Activate(IReadOnlyEntityCollection<E>)`

```csharp
public void Activate(IReadOnlyEntityCollection<E> source);
```

- **Description:** Binds the world view to the source collection and creates views for all existing entities.
- **Parameter:** `source` — The entity collection to visualize.
- **Throws:** `ArgumentNullException` if `source` is `null`.
- **Details:**
    - Calls `Deactivate()` to clean up any previous binding.
    - Subscribes to `OnAdded` and `OnRemoved` events of the source.
    - Iterates the source and calls `Add(entity)` for each existing entity.

#### `Deactivate()`

```csharp
public void Deactivate();
```

- **Description:** Clears all views and unsubscribes from the source collection.
- **Details:**
    - Calls `Clear()` to return all active views to the pool.
    - Unsubscribes from `OnAdded` and `OnRemoved`.
    - Clears the source reference.

---

## 🔗 See Also

- [EntityWorldView](EntityWorldView.md) — non-generic wrapper.
- [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md) — manual collection view base class.
- [IReadOnlyEntityCollection\<E>](../Collections/IReadOnlyEntityCollection%601.md) — source collection interface.
- [Entity UI Manual](Manual.md)
