# 🧩️ Atomic.Entities

Represents a framework for Unity and C# that allows you to **architect your game using entities**. With this
framework, all game objects, systems, UI elements, and application contexts can be represented as
**entities**, each containing **state** and **behaviour**.

---

## 📑 Table of Contents

- [Requirements](#-requirements)
- [Using Odin Inspector](#-using-odin-inspector)
- [Using Rider Plugin](#-using-atomic-plugin-for-rider)
- [API Reference](#-api-reference)
- [Performance](#-performance)
- [Best Practices](#-best-practices)

---

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
the Atomic Rider Plugin from [Jetbrains Marketplace](https://plugins.jetbrains.com/plugin/28321-atomic)
or  [GitHub](https://github.com/Prylor/atomic-rider-plugin). By default, the code generation works with Unity,
but with the plugin, development experience in `Rider IDE` become
smoother and more powerful than in Unity.

---

## 🔍 API Reference

This section provides a complete reference to all major subsystems of the framework. Each module is documented with
usage examples, lifecycle details, and integration notes to help you build, extend, and optimize your architecture.

- [Entities](Entities/Manual.md) 
- [Behaviours](Behaviours/Manual.md) <!-- + -->
- [Installers](Installers/Manual.md)
- [Aspects](Aspects/Manual.md) <!-- + -->
- [Factories](Factories/Manual.md) <!-- + -->
- [Baking](Baking/Manual.md) <!-- + -->
- [Pooling](Pooling/Manual.md) 
- [Collections](Collections/Manual.md) <!-- + -->
- [Worlds](Worlds/Manual.md)
- [Registry](Registry/EntityRegistry.md) <!-- + -->
- [Filters](Filters/Manual.md) <!-- + -->
- [Triggers](Filters/EntityTriggers.md) <!-- + -->
- [Lifecycle](Lifecycle/Manual.md) <!-- + -->
- [UI](UI/Manual.md)
- [Names](Names/Manual.md) <!-- + -->
- [API Generation](EntityAPI/Manual.md)

---

## 🔥 Performance

This section focuses on **runtime efficiency** within the framework. It provides detailed benchmarks, comparisons, and
implementation notes that highlight how different systems and data structures perform under real-world conditions.

- [Entities](Entities/EntityPerformance.md)
- [Collections](Collections/EntityCollectionPerformance.md)

---

## 📌 Best Practices

TO BE ADDED