# 🧩 Bootstrap

**Bootstrapping** allows automatic spawning of essential [MonoEntity](../Entities/MonoEntity.md) prefabs when specific
scenes are loaded. This is useful for global managers, camera rigs, game contexts, and other scene-critical objects.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

The bootstrap system uses [ScriptableObject](https://docs.unity3d.com/Manual/class-ScriptableObject.html) assets placed in
a Resources folder. At startup, the framework finds all bootstrappers and spawns their configured prefabs for any scene
that matches the bootstrapper's regular expression.

---

## 🔍 API Reference

- [ScriptableEntityBootstrapper](ScriptableEntityBootstrapper.md)

---

## 📌 Best Practices

- Use bootstrappers only for entities that must exist in multiple scenes.
- Keep regex patterns simple and specific.
- Use `AfterSceneLoad` mode when spawned entities depend on scene objects.
- Avoid duplicating entities that are already placed in the scene.
