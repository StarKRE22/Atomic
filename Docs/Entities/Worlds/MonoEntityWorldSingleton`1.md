# 🧩 MonoEntityWorldSingleton<E>

`MonoEntityWorldSingleton<E>` is a generic singleton world for managing one scene/global world of entities of type `E`.

It combines [MonoEntityWorld<E>](MonoEntityWorld%601.md) lifecycle management with static singleton access, so systems can
reach the active world through `MonoEntityWorldSingleton<E>.Instance`.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Create a Typed Singleton World](#1️⃣-create-a-typed-singleton-world)
  - [Access the Typed Singleton World](#2️⃣-access-the-typed-singleton-world)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Static Properties](#-static-properties)
    - [Instance](#instance)
  - [Static Methods](#-static-methods)
    - [TryGetInstance(out MonoEntityWorldSingleton\<E>)](#trygetinstanceout-monoentityworldsingletone)
  - [Inherited APIs](#-inherited-apis)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Create a Typed Singleton World

```csharp
public sealed class UnitEntity : MonoEntity
{
}

public sealed class UnitWorld : MonoEntityWorldSingleton<UnitEntity>
{
}
```

Attach `UnitWorld` to one GameObject in the scene and configure the inherited world settings:

- `useUnityLifecycle` — automatically enable, disable, and dispose with Unity lifecycle.
- `collectOnAwake` — scan scene entities during `Awake`.
- `includeInactiveOnCollect` — include inactive entities when collecting.
- `dontDestroyOnLoad` — keep this singleton world alive across scene loads.

---

<div id="ex2"></div>

### 2️⃣ Access the Typed Singleton World

```csharp
// Throws if no typed singleton world exists in the scene:
MonoEntityWorldSingleton<UnitEntity> world = MonoEntityWorldSingleton<UnitEntity>.Instance;

UnitEntity unit = ...;
world.Add(unit);

// Use TryGetInstance when the typed world is optional:
if (MonoEntityWorldSingleton<UnitEntity>.TryGetInstance(out var optionalWorld))
{
    optionalWorld.Tick(Time.deltaTime);
}
```

Only one active singleton exists for each closed generic world type. If a duplicate is initialized, it logs an error and
destroys its GameObject.

---

## 🛠 Inspector Settings

`MonoEntityWorldSingleton<E>` includes its own persistence flag and inherits the regular
[MonoEntityWorld<E>](MonoEntityWorld%601.md) settings:

| Parameter                  | Description                                                                                 |
|----------------------------|---------------------------------------------------------------------------------------------|
| `dontDestroyOnLoad`        | If enabled, the singleton GameObject persists across scene loads.                           |
| `useUnityLifecycle`        | Inherited setting. Enables automatic Unity lifecycle synchronization.                       |
| `collectOnAwake`           | Inherited setting. Collects matching scene entities during `Awake`.                         |
| `includeInactiveOnCollect` | Inherited setting. Includes inactive entities during collection when enabled.                |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class MonoEntityWorldSingleton<E> : MonoEntityWorld<E>
    where E : IEntity
```

- **Type Parameter:** `E` — The entity type managed by this singleton world. Must implement
  [IEntity](../Entities/IEntity.md).
- **Inheritance:** [MonoEntityWorld<E>](MonoEntityWorld%601.md)
- **See also:** [MonoEntityWorldSingleton](MonoEntityWorldSingleton.md), [MonoEntityWorld<E>](MonoEntityWorld%601.md)

---

### 🔑 Static Properties

#### `Instance`

```csharp
public static MonoEntityWorldSingleton<E> Instance { get; }
```

- **Description:** Returns the cached singleton world for this entity type. If no instance is cached, searches the active
  scene for one.
- **Throws:** `Exception` if no `MonoEntityWorldSingleton<E>` exists in the scene.

---

### 🏹 Static Methods

#### `TryGetInstance(out MonoEntityWorldSingleton<E>)`

```csharp
public static bool TryGetInstance(out MonoEntityWorldSingleton<E> instance);
```

- **Description:** Attempts to get the typed singleton world without throwing.
- **Parameter:** `instance` — The found singleton world, or `null` if none exists.
- **Returns:** `true` if an instance was found; otherwise, `false`.

---

### 🧬 Inherited APIs

`MonoEntityWorldSingleton<E>` inherits the typed world lifecycle and collection APIs from
[MonoEntityWorld<E>](MonoEntityWorld%601.md):

- `Add(E)` / `Remove(E)` / `Clear()`
- `Contains(E)` / `Count`
- `Enable()` / `Disable()` / `Dispose()`
- `Tick(float)` / `FixedTick(float)` / `LateTick(float)`
- events such as `OnAdded`, `OnRemoved`, `OnEnabled`, `OnDisabled`, `OnTicked`

---

## 📝 Notes

- Singleton state is stored per closed generic type.
- Use `Instance` only when the typed singleton world is guaranteed to exist in the scene.
- Use `TryGetInstance` for optional systems, tests, or additive scene setups.
- Enable `dontDestroyOnLoad` only for worlds that intentionally outlive the current scene.
- Duplicate singleton worlds destroy themselves during `Awake` before running the base world initialization.
