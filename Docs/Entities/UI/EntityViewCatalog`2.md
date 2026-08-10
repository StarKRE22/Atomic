# 🧩 EntityViewCatalog\<E, V>

A `ScriptableObject` catalog that stores a list of [EntityView\<E>](EntityView%601.md) prefabs.
It provides centralized storage and retrieval of entity view prefabs by index. Generic pools and collections can then
consume these catalogs to preload and reuse views.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Custom Catalog](#ex1)
    - [Prefab Lookup](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Count](#count)
    - [Methods](#-methods)
        - [GetPrefab(int)](#getprefabint)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Custom Catalog

Create a typed catalog for a specific entity/view pair:

```csharp
public interface IUnitEntity : IEntity
{
}

public class UnitView : EntityView<IUnitEntity>
{
}

[CreateAssetMenu(
    fileName = "UnitViewCatalog",
    menuName = "Example/New UnitViewCatalog"
)]
public sealed class UnitViewCatalog : EntityViewCatalog<IUnitEntity, UnitView>
{
}
```

Then create the asset in Unity and assign view prefabs.

---

<div id="ex2"></div>

### 2️⃣ Prefab Lookup

```csharp
EntityViewCatalog<IUnitEntity, UnitView> catalog = ...;

UnitView prefab = catalog.GetPrefab(0);
int count = catalog.Count;
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityViewCatalog<E, V> : ScriptableObject
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
    - `E` — The entity type associated with the views. Must implement [IEntity](../Entities/IEntity.md).
    - `V` — The view type stored in the catalog. Must inherit from [EntityView\<E>](EntityView%601.md).
- **Inheritance:** `ScriptableObject`

---

### 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** The number of prefabs stored in the catalog.

---

### 🏹 Methods

#### `GetPrefab(int)`

```csharp
public V GetPrefab(int index);
```

- **Description:** Gets the prefab at the specified index.
- **Parameter:** `index` — Zero-based index into the prefab list.
- **Returns:** The view prefab at the given index.

---

## 🔗 See Also

- [EntityViewCatalog](EntityViewCatalog.md) — non-generic wrapper.
- [EntityViewPool\<K, E, V>](EntityViewPool%603.md) — pool that consumes catalogs.
- [Entity UI Manual](Manual.md)
