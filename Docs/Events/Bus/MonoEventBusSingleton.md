# 🧩 MonoEventBusSingleton&lt;T&gt;

Singleton version of [MonoEventBus](MonoEventBus.md). Ensures that only one instance of a specific event bus type exists per scene or globally.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Properties](#-properties)
    - [Instance](#instance)
  - [Methods](#-methods)
    - [Resolve (Component)](#resolve-component)
    - [Resolve (GameObject)](#resolve-gameobject)
    - [Resolve (Scene)](#resolve-scene)

---

## 🗂 Example of Usage

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

- **Description:** Singleton version of [MonoEventBus](MonoEventBus.md).
- **Inheritance:** [MonoEventBus](MonoEventBus.md)
- **Type Parameter:** `T` – The concrete singleton type.
- **Notes:** Set `_dontDestroyOnLoad` to `true` in the Inspector to persist the singleton across scene loads.
- **See also:** [MonoEventBus](MonoEventBus.md)

---

### 🔑 Properties

#### `Instance`

```csharp
public static T Instance { get; }
```

- **Description:** Returns the singleton instance from the active scene.
- **Access:** Read-only
- **Throws:** `NullReferenceException` if no instance is found.

---

### 🏹 Methods

#### `Resolve` (Component)

```csharp
public static T Resolve(in Component component);
```

- **Description:** Resolves the singleton from the component's scene.
- **Parameter:** `component` – The component whose scene is searched.
- **Returns:** The singleton instance for that scene.
- **Throws:** `Exception` if no instance is found.

#### `Resolve` (GameObject)

```csharp
public static T Resolve(in GameObject gameObject);
```

- **Description:** Resolves the singleton from the GameObject's scene.
- **Parameter:** `gameObject` – The GameObject whose scene is searched.
- **Returns:** The singleton instance for that scene.
- **Throws:** `Exception` if no instance is found.

#### `Resolve` (Scene)

```csharp
public static T Resolve(Scene scene);
```

- **Description:** Resolves the singleton for the specified scene.
- **Parameter:** `scene` – The scene to search.
- **Returns:** The singleton instance for that scene.
- **Throws:** `Exception` if no instance is found.
