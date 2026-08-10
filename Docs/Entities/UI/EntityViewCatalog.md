# 🧩 EntityViewCatalog

A **non-generic catalog** of [EntityView](EntityView.md) prefabs. This is a concrete version of
[EntityViewCatalog\<E, V>](EntityViewCatalog%602.md) with `E` fixed to [IEntity](../Entities/IEntity.md) and `V` fixed to
[EntityView](EntityView.md). It is useful when you do not need strong typing for a specific entity type.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

### 1️⃣ Create Catalog Asset

Select in Unity menu: `Assets → Create → Atomic → Entities → EntityViewCatalog`. Then add prefabs that contain
`EntityView` component.

<img width="400" height="" alt="Entity component" src="../../Images/EntityViewCatalog.png" />

### 2️⃣ Use the catalog in code

```csharp
// Load catalog from Resources
EntityViewCatalog catalog = Resources.Load<EntityViewCatalog>("EntityViewCatalog");

// Get prefab by index
EntityView prefab = catalog.GetPrefab(0);

// Get total prefab count
int count = catalog.Count;
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[CreateAssetMenu(
    fileName = "EntityViewCatalog",
    menuName = "Atomic/Entities/EntityViewCatalog"
)]
public class EntityViewCatalog : EntityViewCatalog<IEntity, EntityView>
```

- **Description:** Ready-to-use catalog for `EntityView` prefabs.
- **Inheritance:** [EntityViewCatalog\<E, V>](EntityViewCatalog%602.md), `ScriptableObject`

---

## 🔗 See Also

- [EntityViewCatalog\<E, V>](EntityViewCatalog%602.md) — generic base class with full API.
- [EntityViewPool](EntityViewPool.md) — pool that consumes catalogs.
- [Entity UI Manual](Manual.md)
