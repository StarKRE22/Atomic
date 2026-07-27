# 🧩 ScriptableEntityBootstrapper

**ScriptableEntityBootstrapper** is a `ScriptableObject` that automatically spawns [MonoEntity](../Entities/MonoEntity.md)
prefabs when a matching scene is loaded. It is useful for spawning global managers, camera rigs, player contexts, or other
scene-critical entities without placing them manually in every scene.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Inspector Settings](#-inspector-settings)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

At runtime, before the first scene loads, the bootstrapper automatically:

1. Finds all `ScriptableEntityBootstrapper` assets in Resources.
2. Checks whether the active scene matches the configured regular expression.
3. Spawns the configured `MonoEntity` prefabs either before or after the scene finishes loading.

This is a convenient way to guarantee that essential entities exist in specific scenes.

---

## 🛠 Inspector Settings

| Field | Description |
|-------|-------------|
| `isEnabled` | If `true`, the bootstrapper runs automatically. |
| `_sceneRegex` | Regular expression used to match scene names. Empty string matches all scenes. |
| `_mode` | When to spawn entities: `BeforeSceneLoad` or `AfterSceneLoad`. |
| `_entityPrefabs` | Array of `MonoEntity` prefabs to spawn. |

---

## 🗂 Examples of Usage

### Create a Bootstrapper Asset

1. Right-click in the Project window.
2. Select **Create → Atomic → Entities → EntityBootstrapper**.
3. Configure the scene regex, mode, and prefabs.

### Example Configuration

| Setting | Value |
|---------|-------|
| `isEnabled` | `true` |
| `_sceneRegex` | `Level_.*` |
| `_mode` | `AfterSceneLoad` |
| `_entityPrefabs` | `[GameContextPrefab, CameraRigPrefab]` |

This spawns `GameContextPrefab` and `CameraRigPrefab` in every scene whose name starts with `Level_`.

### Runtime Behaviour

```csharp
// No manual code is required — the bootstrapper runs automatically via
// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[CreateAssetMenu(fileName = "EntityBootstrapper", menuName = "Atomic/Entities/EntityBootstrapper")]
public class ScriptableEntityBootstrapper : ScriptableObject
```

### 🛠 Nested Types

#### `Mode`

```csharp
public enum Mode
{
    BeforeSceneLoad = 0,
    AfterSceneLoad = 1
}
```

| Value | Description |
|-------|-------------|
| `BeforeSceneLoad` | Spawn entities before the scene is fully loaded. |
| `AfterSceneLoad` | Spawn entities after the scene has finished loading. |

### 🏹 Methods

#### `IsAvailable(Scene)`

```csharp
protected virtual bool IsAvailable(Scene scene)
```

- **Description:** Determines whether the bootstrapper applies to the given scene.
- **Parameter:** `scene` — The scene to evaluate.
- **Returns:** `true` if the regex is empty or the scene name matches it.

#### `BootstrapEntities(Scene)`

```csharp
protected virtual async void BootstrapEntities(Scene scene)
```

- **Description:** Spawns all configured prefabs into the scene.
- **Parameter:** `scene` — The scene in which to spawn entities.
- **Note:** Waits for scene load if `_mode` is `AfterSceneLoad`.

---

## 📌 Best Practices

- Use bootstrappers for global or scene-critical entities only.
- Keep regex patterns simple and explicit to avoid unintended matches.
- Use `AfterSceneLoad` when spawned entities need fully initialized scene objects.
- Place bootstrapper assets in a Resources folder so `Resources.LoadAll` can find them.
- Avoid duplicating bootstrapped entities that are already placed in the scene.
- Override `IsAvailable` in a derived class for custom matching logic.
