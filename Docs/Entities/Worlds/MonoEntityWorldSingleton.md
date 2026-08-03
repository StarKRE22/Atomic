# 🧩 MonoEntityWorldSingleton

`MonoEntityWorldSingleton` is a non-generic Unity singleton world for managing scene-bound [MonoEntity](../Entities/MonoEntity.md)
instances through one globally reachable world component.

Use it when a scene should contain exactly one shared [MonoEntityWorld](MonoEntityWorld.md) and other systems need access
through `MonoEntityWorldSingleton.Instance`.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Create a Singleton World](#1️⃣-create-a-singleton-world)
  - [Access the Singleton World](#2️⃣-access-the-singleton-world)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Static Properties](#-static-properties)
    - [Instance](#instance)
  - [Static Methods](#-static-methods)
    - [TryGetInstance(out MonoEntityWorldSingleton)](#trygetinstanceout-monoentityworldsingleton)
  - [Inherited APIs](#-inherited-apis)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Create a Singleton World

Create a concrete world type:

```csharp
public sealed class GameWorld : MonoEntityWorldSingleton
{
}
```

Attach `GameWorld` to one GameObject in the scene and configure the inherited world settings:

- `useUnityLifecycle` — automatically enable, disable, and dispose with Unity lifecycle.
- `collectOnAwake` — scan scene entities during `Awake`.
- `includeInactiveOnCollect` — include inactive entities when collecting.
- `dontDestroyOnLoad` — keep this singleton world alive across scene loads.

---

<div id="ex2"></div>

### 2️⃣ Access the Singleton World

```csharp
// Throws if no singleton world exists in the scene:
MonoEntityWorldSingleton world = MonoEntityWorldSingleton.Instance;

world.Enable();
world.Tick(Time.deltaTime);

// Use TryGetInstance when the world is optional:
if (MonoEntityWorldSingleton.TryGetInstance(out MonoEntityWorldSingleton optionalWorld))
{
    Debug.Log($"World contains {optionalWorld.Count} entities");
}
```

Only one active `MonoEntityWorldSingleton` can exist. If a duplicate is initialized, it logs an error and destroys its
GameObject.

---

## 🛠 Inspector Settings

`MonoEntityWorldSingleton` includes its own persistence flag and inherits the regular
[MonoEntityWorld](MonoEntityWorld.md) settings:

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
public abstract class MonoEntityWorldSingleton : MonoEntityWorld
```

- **Inheritance:** [MonoEntityWorld](MonoEntityWorld.md)
- **See also:** [MonoEntityWorldSingleton<E>](MonoEntityWorldSingleton%601.md), [MonoEntity](../Entities/MonoEntity.md)

---

### 🔑 Static Properties

#### `Instance`

```csharp
public static MonoEntityWorldSingleton Instance { get; }
```

- **Description:** Returns the cached singleton world. If no instance is cached, searches the active scene for one.
- **Throws:** `Exception` if no `MonoEntityWorldSingleton` exists in the scene.

---

### 🏹 Static Methods

#### `TryGetInstance(out MonoEntityWorldSingleton)`

```csharp
public static bool TryGetInstance(out MonoEntityWorldSingleton instance);
```

- **Description:** Attempts to get the singleton world without throwing.
- **Parameter:** `instance` — The found singleton world, or `null` if none exists.
- **Returns:** `true` if an instance was found; otherwise, `false`.

---

### 🧬 Inherited APIs

`MonoEntityWorldSingleton` inherits the world lifecycle and collection APIs from [MonoEntityWorld](MonoEntityWorld.md):

- `Add(MonoEntity)` / `Remove(MonoEntity)` / `Clear()`
- `Contains(MonoEntity)` / `Count`
- `Enable()` / `Disable()` / `Dispose()`
- `Tick(float)` / `FixedTick(float)` / `LateTick(float)`
- events such as `OnAdded`, `OnRemoved`, `OnEnabled`, `OnDisabled`, `OnTicked`

---

## 📝 Notes

- Use `Instance` only when a singleton world is guaranteed to exist in the scene.
- Use `TryGetInstance` for optional systems, tests, or additive scene setups.
- Enable `dontDestroyOnLoad` only for worlds that intentionally outlive the current scene.
- Duplicate singleton worlds destroy themselves during `Awake` before running the base world initialization.
- For strongly typed worlds, use [MonoEntityWorldSingleton<E>](MonoEntityWorldSingleton%601.md).
