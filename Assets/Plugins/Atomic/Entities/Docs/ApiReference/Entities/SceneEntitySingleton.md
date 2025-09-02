# 🧩 SceneEntitySingleton<E>

`SceneEntitySingleton<E>` is a specialized `SceneEntity` that ensures only one instance exists per scene or globally.  
It provides easy access via a static `Instance` property and optional persistence across scenes.

---

## Key Features

- **Singleton Pattern** – Guarantees only one instance of the entity type per scene or globally.
- **Global Access** – Use `SceneEntitySingleton<T>.Instance` for easy access.
- **Optional Scene Persistence** – `_dontDestroyOnLoad` allows the singleton to survive scene transitions.
- **Per-Scene Resolution** – `Resolve` methods locate the singleton for a specific scene, GameObject, or component.
- **Entity Integration** – Inherits all `SceneEntity` features: lifecycle (`Init/Enable/Disable/Dispose`), behaviours, tags, values, and events.
- **Lazy Lookup** – Singleton instance is assigned automatically on first access.

---

## Inspector Fields

| Field                | Type   | Default | Description                                                                                  |
|----------------------|--------|---------|----------------------------------------------------------------------------------------------|
| `_isGlobal`          | `bool` | `true`  | Allows access via `SceneEntitySingleton<T>.Instance`. Determines if the singleton is global. |
| `_dontDestroyOnLoad` | `bool` | `false` | Prevents the GameObject from being destroyed when loading a new scene.                       |

---

## Instance Access

### `Instance`

```csharp
E singleton = SceneEntitySingleton<E>.Instance;
```
- Returns the global singleton instance of type `E`.
- Throws an exception if no instance exists in the current scene or globally.
- Automatically caches the singleton for fast subsequent access.
---
## Lifecycle
`SceneEntitySingleton<E>` automatically assigns the singleton instance on `Awake()`
```csharp
protected override void Awake()
{
    if (_instance == null && _isGlobal)
        _instance = (E)this;

    base.Awake();

    if (_dontDestroyOnLoad)
        DontDestroyOnLoad(this.gameObject);
}
```
- Clears `_instance` in `OnDestroy()` if the destroyed instance was the singleton.

## Resolving Singletons
Allows get instance for each scene using `Component`, `GameObject` and `Scene`

```csharp
GameContext singleton1 = GameContext.Resolve(myComponent);
GameContext singleton2 = GameContext.Resolve(myGameObject);
GameContext singleton3 = GameContext.Resolve(myScene);
```
- Resolves the singleton instance associated with the specified scene, GameObject, or component.
- Throws an exception if no instance is found.
- Caches instances per scene for efficient lookup.
---

## Example: Global Singleton
```csharp
public class GameContext : SceneEntitySingleton<GameContext>
{
}

// Access globally
GameContext.Instance.AddValue("Score", 42);
Debug.Log(GameContext.Instance.GetValue<int>("Score")); // 42

```

## Remarks
- **Global Access** – Use `_isGlobal = true` if you want the singleton to be accessible via `Instance` from anywhere.
- **Persistence Across Scenes** – Use `_dontDestroyOnLoad` to keep the singleton alive when loading new scenes.
- **Per-Scene Resolution** – `Resolve` methods are useful in multi-scene setups to retrieve the singleton instance specific to a scene, GameObject, or component.
- Ideal for manager systems, context objects, or other globally unique entities within Unity scenes.
---