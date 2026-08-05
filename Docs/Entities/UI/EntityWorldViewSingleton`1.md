# 🧩 EntityWorldViewSingleton<K, E, V>

`EntityWorldViewSingleton<K, E, V>` is a generic singleton base class for typed world views.

It combines [EntityWorldView<K, E, V>](EntityWorldView%601.md) source-collection binding with singleton access, so one
scene component can mirror a typed `IReadOnlyEntityCollection<E>` and be reached through a static `Instance` property.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Create a Typed Singleton World View](#1️⃣-create-a-typed-singleton-world-view)
  - [Bind the Singleton to a Source Collection](#2️⃣-bind-the-singleton-to-a-source-collection)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Properties](#-properties)
    - [Instance](#instance)
    - [IsActive](#isactive)
  - [Methods](#-methods)
    - [TryGetInstance(out EntityWorldViewSingleton\<K, E, V>)](#trygetinstanceout-entityworldviewsingletonk-e-v)
    - [Activate(IReadOnlyEntityCollection\<E>)](#activateireadonlyentitycollectione)
    - [Deactivate()](#deactivate)
    - [GetKey(E)](#getkeye)
- [Notes](#-notes)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Create a Typed Singleton World View

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

public sealed class UnitWorldViewSingleton : EntityWorldViewSingleton<string, IUnitEntity, UnitView>
{
    protected override string GetKey(IUnitEntity entity)
    {
        return entity.Name;
    }
}
```

Attach `UnitWorldViewSingleton` to one GameObject in the scene and assign:

- `viewport` — parent for active `UnitView` instances.
- `viewPool` — typed pool containing `UnitView` prefabs.
- `dontDestroyOnLoad` — optional persistence between scene loads.

---

<div id="ex2"></div>

### 2️⃣ Bind the Singleton to a Source Collection

```csharp
IReadOnlyEntityCollection<IUnitEntity> units = ...;

// Throws if no singleton exists:
EntityWorldViewSingleton<string, IUnitEntity, UnitView>.Instance.Activate(units);

// Non-throwing access:
if (EntityWorldViewSingleton<string, IUnitEntity, UnitView>.TryGetInstance(out var worldView))
{
    foreach (KeyValuePair<IUnitEntity, UnitView> item in worldView)
    {
        Debug.Log($"{item.Key.Name}: {item.Value.name}");
    }
}
```

---

## 🛠 Inspector Settings

| Parameter           | Description                                                                                 |
|---------------------|---------------------------------------------------------------------------------------------|
| `dontDestroyOnLoad` | If enabled, the singleton GameObject is preserved when Unity loads a new scene.             |
| `viewport`          | Inherited field. The `Transform` under which active typed views will be parented.           |
| `viewPool`          | Inherited field. The typed [EntityViewPool<K, E, V>](EntityViewPool%601.md) used for views. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class EntityWorldViewSingleton<K, E, V> : EntityWorldView<K, E, V>
    where E : class, IEntity
    where V : EntityView<E>
```

- **Type Parameters:**
  - `K` — The key type used by the view pool.
  - `E` — The entity type. Must implement [IEntity](../Entities/IEntity.md).
  - `V` — The view type. Must inherit from [EntityView<E>](EntityView%601.md).
- **Inheritance:** [EntityWorldView<K, E, V>](EntityWorldView%601.md)
- **See also:** [EntityWorldViewSingleton](EntityWorldViewSingleton.md), [EntityViewPool<K, E, V>](EntityViewPool%601.md)

---

### 🔑 Properties

#### `Instance`

```csharp
public static EntityWorldViewSingleton<K, E, V> Instance { get; }
```

- **Description:** Returns the cached singleton for this closed generic type. If no instance is cached, searches the
  active scene for one.
- **Throws:** `Exception` if no singleton instance exists in the scene.

#### `IsActive`

```csharp
public bool IsActive { get; }
```

- **Description:** Inherited from [EntityWorldView<K, E, V>](EntityWorldView%601.md). Returns `true` while this singleton
  is bound to a source entity collection.

---

### 🏹 Methods

#### `TryGetInstance(out EntityWorldViewSingleton<K, E, V>)`

```csharp
public static bool TryGetInstance(out EntityWorldViewSingleton<K, E, V> instance);
```

- **Description:** Attempts to get the singleton instance without throwing.
- **Parameter:** `instance` — The found singleton instance, or `null` if none exists.
- **Returns:** `true` if an instance was found; otherwise, `false`.

#### `Activate(IReadOnlyEntityCollection<E>)`

```csharp
public void Activate(IReadOnlyEntityCollection<E> source);
```

- **Description:** Inherited from [EntityWorldView<K, E, V>](EntityWorldView%601.md). Binds this singleton to a source
  collection and keeps active views synchronized with it.

#### `Deactivate()`

```csharp
public void Deactivate();
```

- **Description:** Inherited from [EntityWorldView<K, E, V>](EntityWorldView%601.md). Unsubscribes from the current source
  and returns all active views to the pool.

#### `GetKey(E)`

```csharp
protected abstract K GetKey(E entity);
```

- **Description:** Inherited key resolver used by the collection view when renting a typed view from the pool.
- **Parameter:** `entity` — The entity that needs a view.
- **Returns:** The pool key used to choose a prefab/view type.

---

## 📝 Notes

- Singleton state is stored per closed generic type. `EntityWorldViewSingleton<string, IUnitEntity, UnitView>` and another
  type combination have separate static instances.
- Use `TryGetInstance` when the singleton is optional or may live in an additive scene.
- Enable `dontDestroyOnLoad` only for world views that intentionally outlive the current scene.
- Duplicate singleton instances log an error and destroy their GameObject during `Awake`.
