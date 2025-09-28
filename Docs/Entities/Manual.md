# ⚛️ Atomic.Entities

`Atomic.Entities` is a framework for Unity and C# that allows you to **architect your game using entities**. With this
framework, all game objects, systems, UI elements, and application contexts can be represented as
**entities**, each containing **state** (data) and **behaviour** (logic).

## 🔍 Table of Contents

- [Requirements](#requirements)
- [Using Odin Inspector](#using-odin-inspector)
- [Using Rider Plugin](#using-atomic-plugin-for-rider)
- [API Reference](#api-reference)
- [Generate Entity API]()
- [Performance](#performance)
- [Best Practices](#-best-practices)

## Requirements

> [!IMPORTANT]  
> The Atomic Framework requires **Unity 6** or **.NET 7+**.  
> Make sure your development environment meets these requirements before using the framework.

## Using Odin Inspector

> [!TIP]  
> For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend**
> using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041).  
> The framework **works without Odin**, but Odin makes inspection and tweaking much easier.

## Using Atomic Plugin for Rider

> [!TIP]  
> For better **code generation** and more convenient workflow in `Rider`, we **optionally recommend** installing
> the [Atomic Plugin](https://github.com/Prylor/atomic-rider-plugin).  
> By default the code generation works with Unity, but with the plugin, development experience in `Rider` become
> smoother and more powerful than in Unity.

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

## Performance

## 📌 Best Practices

## Notes

Atomic.Entities provides

- **Clear separation of data and logic** through the Entity–State–Behaviour pattern;
- **Composition over inheritance**, allowing flexible and reusable designs;
- **Flexibility and scalability** for building game systems of any size;
- **Seamless integration with Unity** and the ability to extend for specific game projects.
