# 🧩️ Atomic.Entities

Represents a framework for Unity and C# that allows you to **architect your game using entities**. With this
framework, all game objects, systems, UI elements, and application contexts can be represented as
**entities**, each containing **state** and **behaviour**.

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Using Odin Inspector](#-using-odin-inspector)
- [Using Rider Plugin](#-using-atomic-plugin-for-rider)
- [API Reference](#-api-reference)
- [Performance](#-performance)
- [Best Practices](#-best-practices)

## 📝 Requirements

The framework requires **Unity 6** or **.NET 7+**. Make sure your development environment meets these requirements
before using the framework.

---

## 🎛 Using Odin Inspector

For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend**
using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041). The
framework **works without Odin**, but Odin makes inspection and tweaking much easier.

---

## 🔌 Using Plugin for Rider

For better **code generation** and more convenient workflow in `Rider IDE`, we **highly recommend** installing
the [Atomic Plugin](https://github.com/Prylor/atomic-rider-plugin). By default, the code generation works with Unity,
but
with the plugin, development experience in `Rider IDE` become
smoother and more powerful than in Unity.

---

## 🔍 API Reference

- [Entities](Entities/Manual.md)
- [Entity API](EntityAPI/Manual.md)
- [Factories](Factories/Manual.md)
- [Baking](Baking/Manual.md)
- [Collections](Collections/Manual.md)
- [Worlds](Worlds/Manual.md)
- [Filters](Filters/Manual.md)
- [UI](UI/Manual.md)
- [Utilities](Utils/Manual.md)
- [Lifecycle](Lifecycle/Manual.md)



<!-- 

- [Behaviours]()
  - [IEntityBehaviour](Behaviours/IEntityBehaviour.md)
  - [IEntityInit](Behaviours/IEntityInit.md)
  - [IEntityDispose](Behaviours/IEntityDispose.md)
  - [IEntityEnable](Behaviours/IEntityEnable.md)
  - [IEntityDisable](Behaviours/IEntityDisable.md)
  - [IEntityTick](Behaviours/IEntityTick.md)
  - [IEntityFixedTick](Behaviours/IEntityFixedTick.md)
  - [IEntityLateTick](Behaviours/IEntityLateTick.md)
  - [IEntityGizmos](Behaviours/IEntityGizmos.md)
  - [RunInEditModeAttribute](Attributes/RunInEditModeAttribute.md)
  - [Extensions](Entities/Extensions.md)

- [Installers]()
  - [IEntityInstaller](Installers/IEntityInstaller.md)
  - [SceneEntityInstaller](Installers/SceneEntityInstaller.md)
  - [ScriptableEntityInstaller](Installers/ScriptableEntityInstaller.md)
  - [Extensions](Entities/Extensions.md)

- [Proxies]()
  - [SceneEntityProxy](Entities/SceneEntityProxy.md)
  - [Extensions](Entities/Extensions.md)

- [Singletons]()
  - [EntitySingleton](Entities/EntitySingleton.md)
  - [SceneEntitySingleton](Entities/SceneEntitySingleton.md)
- [Registry]
  - [EntityRegistry](Registry/EntityRegistry.md)

-->

---

## 🔥 Performance

[Entities](Entities/EntityPerformance.md)

---

## 📌 Best Practices

---
