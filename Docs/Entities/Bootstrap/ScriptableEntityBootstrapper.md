# 🧩 ScriptableEntityBootstrapper

A `ScriptableObject` that automatically spawns [MonoEntity](../Entities/MonoEntity.md) prefabs when a matching scene is loaded.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Nested Types](#-nested-types)
    - [Mode](#mode)
  - [Methods](#-methods)
    - [IsAvailable(Scene)](#isavailablescene)
    - [BootstrapEntities(Scene)](#bootstrapentitiesscene)

---

## 🗂 Example of Usage

Create a bootstrapper asset and configure it in the Inspector:

```csharp
// Create via Project window: Right-click -> Create -> Atomic -> Entities -> EntityBootstrapper
```

| Setting | Value |
|---------|-------|
| `isEnabled` | `true` |
| `_sceneRegex` | `Level_.*` |
| `_mode` | `AfterSceneLoad` |
| `_entityPrefabs` | `[GameContextPrefab, CameraRigPrefab]` |

This spawns the configured prefabs in every scene whose name starts with `Level_`:

```csharp
// Runs automatically via [RuntimeInitializeOnLoadMethod] before the first scene loads.
// No manual code is required.
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[CreateAssetMenu(
    fileName = "EntityBootstrapper",
    menuName = "Atomic/Entities/EntityBootstrapper"
)]
public class ScriptableEntityBootstrapper : ScriptableObject
```

- **Description:** A `ScriptableObject` that automatically spawns [MonoEntity](../Entities/MonoEntity.md) prefabs when a matching scene is loaded.
- **Inheritance:** `ScriptableObject`
- **Notes:** Assets must be in a Resources folder to be discovered by `Resources.LoadAll`.
- **See also:** [MonoEntity](../Entities/MonoEntity.md)

---

### 🗂️ Nested Types

#### `Mode`

```csharp
public enum Mode
{
    BeforeSceneLoad = 0,
    AfterSceneLoad = 1
}
```

- **Description:** Defines when entity spawning should occur.

| Value | Description |
|-------|-------------|
| `BeforeSceneLoad` | Spawn entities before the scene is fully loaded. |
| `AfterSceneLoad` | Spawn entities after the scene has finished loading. |

---

### 🏹 Methods

#### `IsAvailable(Scene)`

```csharp
protected virtual bool IsAvailable(Scene scene)
```

- **Description:** Determines whether the bootstrapper applies to the given scene.
- **Parameter:** `scene` – The scene to evaluate.
- **Returns:** `true` if the regex is empty or the scene name matches it; otherwise, `false`.

#### `BootstrapEntities(Scene)`

```csharp
protected virtual async void BootstrapEntities(Scene scene)
```

- **Description:** Spawns all configured prefabs into the scene.
- **Parameter:** `scene` – The scene in which to spawn entities.
- **Remarks:** If `_mode` is `AfterSceneLoad`, waits for the scene to finish loading before spawning.


