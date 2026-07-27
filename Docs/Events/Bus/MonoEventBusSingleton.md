# 🧩 MonoEventBusSingleton

**MonoEventBusSingleton\<T\>** is a singleton version of [MonoEventBus](MonoEventBus.md). It ensures that only one
instance of a specific event bus type exists per scene or globally.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`MonoEventBusSingleton<T>` inherits from `MonoEventBus` and adds singleton access patterns:

- `Instance` — returns the single instance found in the scene.
- `Resolve(scene)` / `Resolve(gameObject)` / `Resolve(component)` — finds the singleton for a specific scene.

Use it for globally or per-scene accessible event buses such as `GameEventBus`.

---

## 🗂 Examples of Usage

### Define a Singleton Event Bus

```csharp
public sealed class GameEventBus : MonoEventBusSingleton<GameEventBus>, IGameEventBus
{
}
```

### Access Instance

```csharp
GameEventBus eventBus = GameEventBus.Instance;

eventBus.Invoke(GameEventAPI.PlayerTurnStarted);
eventBus.Flush();
```

### Resolve for a Scene

```csharp
Scene scene = gameObject.scene;
GameEventBus eventBus = GameEventBus.Resolve(scene);
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public abstract class MonoEventBusSingleton<T> : MonoEventBus where T : MonoEventBus
```

- **Type Parameter:** `T` — The concrete singleton type.
- **Inheritance:** [MonoEventBus](MonoEventBus.md)

### 🏹 Properties

#### `Instance`

```csharp
public static T Instance { get; }
```

- **Description:** Returns the singleton instance from the active scene.
- **Exception:** Throws `NullReferenceException` if no instance is found.

### 🏹 Methods

#### `Resolve(Component)`

```csharp
public static T Resolve(in Component component)
```

- **Description:** Resolves the singleton from the component's scene.

#### `Resolve(GameObject)`

```csharp
public static T Resolve(in GameObject gameObject)
```

- **Description:** Resolves the singleton from the GameObject's scene.

#### `Resolve(Scene)`

```csharp
public static T Resolve(Scene scene)
```

- **Description:** Resolves the singleton for the specified scene.
- **Exception:** Throws if no instance is found.

### 🏷️ Fields

| Field | Description |
|-------|-------------|
| `_dontDestroyOnLoad` | If `true`, the singleton persists across scene loads. |

---

## 📌 Best Practices

- Use `MonoEventBusSingleton<T>` for global or per-scene event buses.
- Place the singleton on a root GameObject in each scene that needs it.
- Enable `_dontDestroyOnLoad` only for truly global buses.
- Use `Resolve(scene)` when the active scene may not be the one containing the bus.
