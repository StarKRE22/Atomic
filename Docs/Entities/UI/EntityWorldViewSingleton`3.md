# 🧩 EntityWorldViewSingleton\<K, E, V>

A generic singleton implementation of [EntityWorldView\<K, E, V>](EntityWorldView%603.md).
Ensures that only one instance exists in the scene (or globally if marked as persistent).
Use it when a typed world view should be globally accessible.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Custom Singleton World View](#ex1)
    - [Accessing the Singleton](#ex2)
- [Inspector Settings](#-inspector-settings)
    - [Parameters](#-parameters)
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

### 1️⃣ Custom Singleton World View

Create a typed singleton world view:

```csharp
public interface IUnitEntity : IEntity
{
}

public class UnitView : EntityView<IUnitEntity>
{
}

public sealed class UnitWorldViewSingleton : EntityWorldViewSingleton<string, IUnitEntity, UnitView>
{
    protected override string GetKey(IUnitEntity entity) => entity.Name;
}
```

Attach it to a GameObject and configure the inspector fields.

---

<div id="ex2"></div>

### 2️⃣ Accessing the Singleton

```csharp
// Throws if no instance is found in the scene
UnitWorldViewSingleton worldView = UnitWorldViewSingleton.Instance;
worldView.Activate(entityCollection);

// Safe access
if (UnitWorldViewSingleton.TryGetInstance(out UnitWorldViewSingleton instance))
{
    instance.Activate(entityCollection);
}
```

---

## 🛠 Inspector Settings

### 🎛️ Parameters

| Parameter           | Description                                                                |
|---------------------|----------------------------------------------------------------------------|
| `dontDestroyOnLoad` | If enabled, the GameObject survives scene loads via `DontDestroyOnLoad`.   |
| `viewport`          | Inherited from [EntityWorldView\<K, E, V>](EntityWorldView%603.md).        |
| `pool`              | Inherited from [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md). |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityWorldViewSingleton<K, E, V> : EntityWorldView<K, E, V>
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
    - `K` — Key type used to select prefabs from the pool.
    - `E` — Entity type. Must implement [IEntity](../Entities/IEntity.md).
    - `V` — View type. Must inherit from [EntityView\<E>](EntityView%601.md).
- **Inheritance:** [EntityWorldView\<K, E, V>](EntityWorldView%603.md), [EntityCollectionView\<K, E, V>](EntityCollectionView%603.md), `MonoBehaviour`

---

### 🔑 Properties

#### `Instance`

```csharp
public static EntityWorldViewSingleton<K, E, V> Instance { get; }
```

- **Description:** Returns the singleton instance.
- **Throws:** `Exception` if no instance is found in the scene.

---

### 🏹 Methods

#### `TryGetInstance`

```csharp
public static bool TryGetInstance(out EntityWorldViewSingleton<K, E, V> instance);
```

- **Description:** Attempts to get the singleton instance without throwing.
- **Returns:** `true` if an instance exists, `false` otherwise.

---

## 🔗 See Also

- [EntityWorldViewSingleton](EntityWorldViewSingleton.md) — non-generic wrapper.
- [EntityWorldView\<K, E, V>](EntityWorldView%603.md) — base world view class.
- [Entity UI Manual](Manual.md)
