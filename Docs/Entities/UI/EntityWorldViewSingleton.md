# 🧩 EntityWorldViewSingleton

`EntityWorldViewSingleton` is a concrete, non-generic singleton world view for scene-level access to one
[EntityWorldView](EntityWorldView.md).

Use it when a scene has one shared world view that should be reachable through `EntityWorldViewSingleton.Instance` and
should mirror an `IReadOnlyEntityCollection<IEntity>` with pooled [EntityView](EntityView.md) instances.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Singleton Setup](#1️⃣-singleton-setup)
  - [Singleton Usage](#2️⃣-singleton-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Properties](#-properties)
    - [Instance](#instance)
    - [IsActive](#isactive)
  - [Methods](#-methods)
    - [TryGetInstance(out EntityWorldViewSingleton)](#trygetinstanceout-entityworldviewsingleton)
    - [Activate(IReadOnlyEntityCollection\<IEntity>)](#activateireadonlyentitycollectionientity)
    - [Deactivate()](#deactivate)
- [Notes](#-notes)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Singleton Setup

Add one `EntityWorldViewSingleton` component to the scene and configure the inherited world-view fields:

- `viewport` — parent for active entity views.
- `viewPool` — pool that rents and returns [EntityView](EntityView.md) instances.
- `dontDestroyOnLoad` — if enabled, the singleton GameObject is preserved between scene loads.

Only one active singleton of this type should exist. If a duplicate is initialized, the duplicate logs an error and
destroys its GameObject.

---

<div id="ex2"></div>

### 2️⃣ Singleton Usage

```csharp
IReadOnlyEntityCollection<IEntity> enemies = ...;

// Throws if no singleton exists in the active scene:
EntityWorldViewSingleton.Instance.Activate(enemies);

// Use TryGetInstance when the scene may not contain the singleton:
if (EntityWorldViewSingleton.TryGetInstance(out EntityWorldViewSingleton worldView))
{
    worldView.Deactivate();
}
```

The singleton still behaves like a regular [EntityWorldView](EntityWorldView.md): it creates views for existing entities
on `Activate` and keeps them synchronized with future collection additions and removals.

---

## 🛠 Inspector Settings

| Parameter           | Description                                                                          |
|---------------------|--------------------------------------------------------------------------------------|
| `dontDestroyOnLoad` | If enabled, the singleton GameObject is preserved when Unity loads a new scene.      |
| `viewport`          | Inherited field. The `Transform` under which active entity views will be parented.   |
| `viewPool`          | Inherited field. The [EntityViewPool](EntityViewPool.md) used to rent/return views. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class EntityWorldViewSingleton : EntityWorldView<string, IEntity, EntityView>
```

- **Inheritance:** [EntityWorldView<K, E, V>](EntityWorldView%601.md)
- **Key Strategy:** uses `entity.Name` as the view-pool key.
- **See also:** [EntityWorldView](EntityWorldView.md), [EntityWorldViewSingleton<K, E, V>](EntityWorldViewSingleton%601.md)

---

### 🔑 Properties

#### `Instance`

```csharp
public static EntityWorldViewSingleton Instance { get; }
```

- **Description:** Returns the cached singleton instance. If no instance is cached, searches the active scene for one.
- **Throws:** `Exception` if no `EntityWorldViewSingleton` exists in the scene.

#### `IsActive`

```csharp
public bool IsActive { get; }
```

- **Description:** Inherited from [EntityWorldView](EntityWorldView.md). Returns `true` while the singleton is bound to an
  entity collection.

---

### 🏹 Methods

#### `TryGetInstance(out EntityWorldViewSingleton)`

```csharp
public static bool TryGetInstance(out EntityWorldViewSingleton instance);
```

- **Description:** Attempts to get the singleton instance without throwing.
- **Parameter:** `instance` — The found singleton instance, or `null` if none exists.
- **Returns:** `true` if an instance was found; otherwise, `false`.

#### `Activate(IReadOnlyEntityCollection<IEntity>)`

```csharp
public void Activate(IReadOnlyEntityCollection<IEntity> source);
```

- **Description:** Inherited from [EntityWorldView](EntityWorldView.md). Binds the singleton to a source collection and
  creates views for current and future entities.

#### `Deactivate()`

```csharp
public void Deactivate();
```

- **Description:** Inherited from [EntityWorldView](EntityWorldView.md). Unsubscribes from the source collection and
  returns all active views to the pool.

---

## 📝 Notes

- Use `Instance` only when the singleton is guaranteed to exist in the scene.
- Use `TryGetInstance` for optional UI flows, additive scenes, or test scenes where the singleton may be absent.
- Enable `dontDestroyOnLoad` only for world views that should survive scene changes.
- Duplicate singleton instances destroy themselves during `Awake`, so keep only one configured singleton per scene/load
  scope.
