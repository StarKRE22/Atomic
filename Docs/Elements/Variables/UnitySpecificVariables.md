# 🧩 Unity-Specific Variables

In addition to the core `IVariable<T>` and `IReactiveVariable<T>` types, the library includes several **Unity-oriented variable wrappers** that make it easy to bind common Unity data sources to the variable system.

---

## PlayerPrefs-Based Variables

These variables use Unity’s `PlayerPrefs` as the storage backend. They are useful for **persistent settings, player preferences, or save data**.

- **`FloatPrefsVariable`** – wraps a `float` value stored in `PlayerPrefs`.
- **`IntPrefsVariable`** – wraps an `int` value stored in `PlayerPrefs`.
- **`StringPrefsVariable`** – wraps a `string` value stored in `PlayerPrefs`.

**Example**
```csharp
var musicVolume = new FloatPrefsVariable("MusicVolume", 1.0f);
musicVolume.Value = 0.5f; // Saved to PlayerPrefs
Debug.Log(musicVolume.Value); // Reads from PlayerPrefs
```

## Transform-Based Variables

These variables wrap `UnityEngine.Transform` properties, allowing them to be used through the `IVariable<T>` interface.

- **TransformParentVariable** – wraps `Transform.parent`.
- **TransformPositionVariable** – wraps `Transform.position`.
- **TransformRotationVariable** – wraps `Transform.rotation`.
- **TransformScaleVariable** – wraps `Transform.localScale`.

They provide a convenient way to work with Unity objects, simplify testing, and allow you to apply a consistent data-driven approach across your project.

```csharp
Transform playerTransform = ...;
IVariable<Vector3> position = new TransformPositionVariable(playerTransform);

// Move the player
position.Value += new Vector3(0, 1, 0);
```

### Why Use These?
- Provides a consistent API for working with Unity data.
- Makes testing easier by allowing you to replace real Unity objects with simple mock variables.
- Reduces direct dependencies on Unity, making the codebase easier to maintain and evolve.  
