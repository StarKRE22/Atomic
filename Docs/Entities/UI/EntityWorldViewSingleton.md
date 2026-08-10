# 🧩 EntityWorldViewSingleton

A singleton version of [EntityWorldView](EntityWorldView.md). Ensures that only one instance exists in the scene
and optionally survives scene loads. Use it when the world view should be globally accessible.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Scene Setup](#ex1)
    - [Accessing the Singleton](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Instance](#instance)
    - [Methods](#-methods)
        - [TryGetInstance](#trygetinstance)
- [See Also](#-see-also)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Scene Setup

Attach the component to a GameObject in the scene:

- Assign a `Transform` to `viewport`.
- Assign the [EntityViewPool](EntityViewPool.md) to `pool`.
- Enable `dontDestroyOnLoad` if the singleton should persist across scenes.

---

<div id="ex2"></div>

### 2️⃣ Accessing the Singleton

```csharp
// Throws if no instance is found in the scene
EntityWorldViewSingleton worldView = EntityWorldViewSingleton.Instance;
worldView.Activate(entityCollection);

// Safe access
if (EntityWorldViewSingleton.TryGetInstance(out EntityWorldViewSingleton instance))
{
    instance.Activate(entityCollection);
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public class EntityWorldViewSingleton : EntityWorldView<string, IEntity, EntityView>
```

- **Description:** Singleton world view for `IEntity` / `EntityView` pairs.
- **Inheritance:** [EntityWorldView\<K, E, V>](EntityWorldView%603.md), [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md), `MonoBehaviour`

---

### 🔑 Properties

#### `Instance`

```csharp
public static EntityWorldViewSingleton Instance { get; }
```

- **Description:** Returns the singleton instance.
- **Throws:** `Exception` if no instance is found in the scene.

---

### 🏹 Methods

#### `TryGetInstance`

```csharp
public static bool TryGetInstance(out EntityWorldViewSingleton instance);
```

- **Description:** Attempts to get the singleton instance without throwing.
- **Returns:** `true` if an instance exists, `false` otherwise.

---

## 🔗 See Also

- [EntityWorldViewSingleton\<K, E, V>](EntityWorldViewSingleton%603.md) — generic singleton base class.
- [EntityWorldView](EntityWorldView.md) — non-singleton world view.
- [Entity UI Manual](Manual.md)
