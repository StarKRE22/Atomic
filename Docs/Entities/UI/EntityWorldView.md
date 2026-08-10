# 🧩 EntityWorldView

A component that automatically mirrors an [IReadOnlyEntityCollection\<IEntity>](../Collections/IReadOnlyEntityCollection%601.md)
with pooled [EntityView](EntityView.md) instances. It creates views for existing entities and keeps them synchronized
when entities are added or removed. A **non-generic wrapper** around
[EntityWorldView\<K, E, V>](EntityWorldView%603.md).

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Scene Setup](#ex1)
    - [Activation and Deactivation](#ex2)
- [API Reference](#-api-reference)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Scene Setup

Attach `Atomic/Entities/Entity Collection View` to a GameObject:

- Assign a `Transform` to `viewport`.
- Assign the [EntityViewPool](EntityViewPool.md) to `pool`.

---

<div id="ex2"></div>

### 2️⃣ Activation and Deactivation

```csharp
EntityWorldView worldView = ...;
IReadOnlyEntityCollection<IEntity> entityCollection = ...;

// Create views for existing entities and subscribe to additions/removals:
worldView.Activate(entityCollection);

// Stop synchronization and return all views to the pool:
worldView.Deactivate();
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AddComponentMenu("Atomic/Entities/Entity Collection View")]
[DisallowMultipleComponent]
public class EntityWorldView : EntityWorldView<string, IEntity, EntityView>
```

- **Description:** Ready-to-use world view for `IEntity` / `EntityView` pairs.
- **Inheritance:** [EntityWorldView\<K, E, V>](EntityWorldView%603.md), [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md), `MonoBehaviour`

---

## 🔗 See Also

- [EntityWorldView\<K, E, V>](EntityWorldView%603.md) — generic base class with full API.
- [EntityCollectionView](EntityCollectionView.md) — manual collection view.
- [Entity UI Manual](Manual.md)
