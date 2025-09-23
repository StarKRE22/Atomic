# 🧩 SceneEntitySingleton&lt;E&gt;

Represents a specialized [SceneEntity](SceneEntity.md) that ensures only one instance exists per scene or globally. This
class combines the **Entity–State–Behaviour** model with
the [Singleton Pattern](https://en.wikipedia.org/wiki/Singleton_pattern). It provides easy access via a static
`Instance` property and optional persistence across scenes.

```csharp
public abstract class SceneEntitySingleton<E> : SceneEntity 
    where E : SceneEntitySingleton<E>
```

- **Type Parameter:** `E` — The concrete entity singleton type.
- **Inheritance:** derived from [SceneEntity](SceneEntity.md)
- **Notes:** Subclass must inherit from `SceneEntitySingleton<E>`

---

## 🛠 Inspector Settings

| Field               | Type                                                                                      |
|---------------------|-------------------------------------------------------------------------------------------|
| `isGlobal`          | Allows access via `Instance`. Determines if the singleton is global. Default is `true`    |
| `dontDestroyOnLoad` | Prevents the GameObject from being destroyed when loading a new scene. Default is `false` |

---

## 🔑 Static Properties

#### `Instance`

```csharp
public static E Instance { get; }
```

- **Description:** Returns the global singleton instance of type `E`.
- **Throws:** an exception if no instance exists in the current scene or globally.
- **Notes** Automatically caches the singleton for fast subsequent access.

---

## 🏹 Static Methods

#### `Resolve(Component)`

```csharp
public static E Resolve(Component component)
```

- **Description:** Resolves the singleton instance for the scene containing the given `Component`.
- **Parameter:** `component` – The component whose scene will be used for lookup.
- **Returns:** The singleton instance found in the component's scene.

#### `Resolve(GameObject)`

```csharp
public static E Resolve(GameObject gameObject)
```

- **Description:** Resolves the singleton instance for the scene containing the given `GameObject`.
- **Parameter:** `gameObject` – The `GameObject` whose scene will be used for lookup.
- **Returns:** The singleton instance found in the `GameObject`'s scene.

#### `Resolve(Scene)`

```csharp
public static E Resolve(Scene scene)
```

- **Description:** Resolves the singleton instance for the given `Scene`.
- **Parameter:** `scene` – The scene to search for the singleton.
- **Returns:** The singleton instance if found.
- **Throws:** `Exception` if no singleton of type `E` is found in the scene.

---

## 🗂 Examples of Usage

The following examples demonstrate how to access the `SceneEntitySingleton` and resolve it for each scene

---

### 🔹 Example #1: Using Global Singleton

```csharp
public class GameContext : SceneEntitySingleton<GameContext>
{
}
```

```csharp
// Access globally
var context = GameContext.Instance;
context.AddValue("Score", 42);

int score = context.GetValue<int>("Score");
Debug.Log(score); // 42
```

---

### 🔹Example #2: Resolving Singletons

Allows get instance for each scene using `Component`, `GameObject` and `Scene`

```csharp
//Resolve through Component
GameContext context = GameContext.Resolve(myComponent);
```

```csharp
//Resolves through GameObject
GameContext context = GameContext.Resolve(myGameObject);
```

```csharp
//Resolves through Scene
GameContext context = GameContext.Resolve(myScene);
```

---

## 📝 Notes

- **Singleton Pattern** – Guarantees only one instance of the entity type per scene or globally.
- **Entity Integration** – Inherits all `SceneEntity` features: lifecycle, behaviours, tags, values, and events.
- **Lazy Lookup** – Singleton instance is assigned automatically on first access.
- **Global Access** – Use `isGlobal = true` if you want the singleton to be accessible via `Instance` from anywhere.
- **Persistence Across Scenes** – Use `dontDestroyOnLoad` to keep the singleton alive when loading new scenes.
- **Per-Scene Resolution** – `Resolve` methods are useful in multi-scene setups to retrieve the singleton instance
  specific to a scene, GameObject, or component.
