# 🧩 Bootstrap

**Bootstrapping** allows automatic spawning of essential [MonoEntity](../Entities/MonoEntity.md) prefabs when specific
scenes are loaded. This is useful for global managers, camera rigs, game contexts, and other scene-critical objects.

The bootstrap system uses [ScriptableObject](https://docs.unity3d.com/Manual/class-ScriptableObject.html) assets placed
in a `Resources` folder. At startup, the framework finds all bootstrappers and spawns their configured prefabs for any
scene that matches the bootstrapper's regular expression.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Create a Bootstrapper Asset](#create-a-bootstrapper-asset)
  - [Configure Scene Matching](#configure-scene-matching)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Create a Bootstrapper Asset

Create a `ScriptableEntityBootstrapper` asset via the Create menu:

```
Assets > Create > Atomic > Entities > EntityBootstrapper
```

You can also create a typed subclass for project-specific bootstrappers:

```csharp
using Atomic.Entities;
using UnityEngine;

[CreateAssetMenu(fileName = "GameBootstrapper", menuName = "Game/Bootstrapper")]
public sealed class GameBootstrapper : ScriptableEntityBootstrapper { }
```

### Configure Scene Matching

In the Inspector, configure the bootstrapper asset:

| Field | Example | Purpose |
|-------|---------|---------|
| `Is Enabled` | `true` | Toggle bootstrapping on/off. |
| `Scene Regex` | `^Gameplay.*$` | Regex that selects which scenes receive the prefabs. |
| `Mode` | `BeforeSceneLoad` | When prefabs are spawned. |
| `Entity Prefabs` | `[GameContext, CameraRig]` | Prefabs to instantiate. |

```csharp
// After bootstrap runs, the scene contains the configured MonoEntity prefabs.
```

---

## 🔍 API Reference

- [ScriptableEntityBootstrapper](ScriptableEntityBootstrapper.md)

---

## 📌 Best Practices

- Use bootstrappers only for entities that must exist in multiple scenes.
- Keep regex patterns simple and specific.
- Use `AfterSceneLoad` mode when spawned entities depend on scene objects.
- Avoid duplicating entities that are already placed in the scene.
