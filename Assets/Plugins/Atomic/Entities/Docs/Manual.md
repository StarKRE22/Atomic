# ⚛️ Atomic.Entities

`Atomic.Entities` is a library for Unity and C# that allows you to **architect your game using entities**.  
With this library, all game objects, systems, UI elements, and application contexts can be represented as **entities**, each containing **state** (data) and **behaviour** (logic).

Atomic.Entities provides:
- **Clear separation of data and logic** through the Entity–State–Behaviour pattern;
- **Composition over inheritance**, allowing flexible and reusable designs;
- **Flexibility and scalability** for building game systems of any size;
- **Seamless integration with Unity** and the ability to extend for specific game projects.

## 🔍 Table of Contents
- [Requirements](#requirements)
- [Using Odin Inspector](#using-odin-inspector)
- [Using Atomic Plugin for Rider](#using-atomic-plugin-for-rider)
- [Tutorial](#tutorial)
- [API Reference](#api-reference)
- [Performance](#performance)
- [Best Practices](#best-practices)

## Requirements
> [!IMPORTANT]  
> The Atomic Framework requires **Unity 6** or **.NET 7+**.  
> Make sure your development environment meets these requirements before using the framework.

## Using Odin Inspector
> [!TIP]  
> For better **debugging**, **configuration**, and **visualization** of game state, we **optionally recommend** using [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041).  
> The framework **works without Odin**, but Odin makes inspection and tweaking much easier.

## Using Atomic Plugin for Rider
> [!TIP]  
> For better **code generation** and more convenient workflow in `Rider`, we **optionally recommend** installing the [Atomic Plugin](https://github.com/Prylor/atomic-rider-plugin).  
> By default the code generation works with Unity, but with the plugin, development experience in `Rider` become smoother and more powerful than in Unity.







Guides
├── Introduction
│   ├── What is Atomic?
│   ├── Requirements & Installation
│   └── Using Odin Inspector (optional)
│
├── Core Concepts
│   ├── Entities & EntityStateBehaviour Pattern
│   ├── Reactive Values & Variables
│   ├── Events & Signals
│   └── Requests & Actions
│
├── Tutorials
│   ├── Getting Started with Entities (C# example)
│   ├── Unity Quick Start (SceneEntity, Installers)
│   ├── Character Example (MoveBehaviour)
│   └── Building UI Contexts
│
├── Best Practices
│   ├── Prefer Abstract Interfaces
│   ├── Shared Constants
│   ├── Iterating Reactive Collections
│   ├── Request-Condition-Action-Event Pattern
│   ├── Requests vs Actions
│   └── Performance Tips



- **Entities**
- **Factories & Baking, Pooling**
- **EntityCollection**



Entities
├── IEntity
├── Entity
├── SceneEntity
├── Behaviours
│   ├── IEntityBehaviour
│   ├── IEntityInit
│   ├── IEntityDispose
│   └── ...
├── IEntityGizmos (Unity-specific)
├── IEntityInstaller
└── Extensions (Lifecycle, Subscriptions, etc.)


//Второй вариант
- **Entity** //глобальный раздел
  - [IEntity]()
  - [Entity]()
  - [SceneEntity]()
  - *Behaviours*
  Behaviour Lifecycle
    - [IEntityBehaviour]
    - [IEntityInit]
    - [IEntityDispose]
    - [IEntityEnable]
    - [IEntityDisable]
    - [IEntityUpdate]
    - [IEntityFixedUpdate]
    - [IEntityLateUpdate]

- [IEntityGizmos] для Unity, RunInEditModeAttribute


- IEntityInstaller
- Lifecycle Extensions WhenInit, WhenEnable, WhenDisable,
- Subscriptions!
- Code generation, EntityNames
- 
- Pooling
- Factory / Baking
- EntityCollection/ EntityWorld, Extensions
- EntityRegistry
- EntityFilter & EntityTrigger
- EntityView
- Performance

