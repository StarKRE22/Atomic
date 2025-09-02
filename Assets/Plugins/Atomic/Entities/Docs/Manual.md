# ⚛️ Atomic.Entities

`Atomic.Entities` is a framework for Unity and C# that allows you to **architect your game using entities**.  
With this framework, all game objects, systems, UI elements, and application contexts can be represented as **entities**, each containing **state** (data) and **behaviour** (logic).

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

## Tutorial

- **What is Entity**
- **Create an Entity**
  - **CSharp Guide** 
  - **Unity Guide**
- **Entity Behaviour Lifecycle** + Gizmos и аттрибут в Edit Mode
- **Using Entity Pools**
- **Using Entity Worlds**
- **Using Entity Filters**
- **Separating Game from Unity** //Factories, Baking, Views 
- **Designing Architecture with Entities** //Contexts

- **Using Code generation**
  - **Unity Guide**
  - **Rider Plugin Guide**

## API Reference

- **Entities**
  - [IEntity](ApiReference/Entities/IEntity.md)
  - [Entity](ApiReference/Entities/Entity.md)
  - [EntitySingleton](ApiReference/Entities/EntitySingleton.md)
  - [SceneEntity](ApiReference/Entities/SceneEntity.md)
  - [SceneEntityProxy](ApiReference/Entities/SceneEntityProxy.md)
  - [SceneEntitySingleton](ApiReference/Entities/SceneEntitySingleton.md)
  - [Extensions](ApiReference/Entities/Extensions.md)
  
- **Entity Behaviours**
  - [IEntityBehaviour]
  - [IEntityInit]
  - [IEntityDispose]
  - [IEntityEnable]
  - [IEntityDisable]
  - [IEntityUpdate]
  - [IEntityFixedUpdate]
  - [IEntityLateUpdate]
  - [IEntityGizmos]
  - [RunInEditModeAttribute]
  
- **Entity Installers**
  - [IEntityInstaller]
  - [ScriptableEntityInstaller]
  - [SceneEntityInstaller]

- **Entity Factories**
  - [IEntityFactory]
  - [InlineEntityFactory]
  - [ScriptableEntityFactory]
  - [SceneEntityFactory]
  - [IEntityCatalog]
  - [ScriptableEntityCatalog]
  - [IMultiEntityFactory]
  - [MultiEntityFactory]

- **Entity Baking**
  - [SceneEntityBaker]

- **Entity Pools**
  - [IEntityPool]
  - [EntityPool]
  - [SceneEntityPool]
  - [IMultiEntityPool]
  - [MultiEntityPool]
  - [IPrefabEntityPool]
  - [PrefabEntityPool]

- **Entity Collections**
  - [IReadOnlyEntityCollection]
  - [IEntityCollection]
  - [EntityCollection]
  - [Extensions]

- **Entity Worlds**
  - [IEntityWorld]
  - [EntityWorld]
  - [SceneEntityWorld]

- **Entity Registry**
  - [EntityRegistry]

- **Entity Filters & Triggers**
  - [EntityFilter]
  - [IEntityTrigger]
  - [EntityTriggerBase]
  - [TagEntityTrigger]
  - [ValueEntityTrigger]
  - [BehaviourEntityTrigger]
  - [StateChangedEntityTrigger]
  - [SubscriptionEntityTrigger]
  - [InlineEntityTrigger]

- **Lifecycle Sources**
  - [Sources]
  - [Extensions]
  - [Subscriptions]

- **Entity Utils**
  - [EntityUtils]

- **Entity Names**
  - [EntityNames]
  
- **User Interface**

  - **Entity View**
    - [IReadOnlyEntityView]
    - [IEntityView]
    - [EntityViewBase]
    - [EntityView]
    - [EntityViewInstaller]
    - [EntityViewCatalog]
    
  - **Entity View Pool**
    - [IEntityViewPool]
    - [EntityViewPoolBase]
    - [EntityViewPool]
    
  - **Entity Collection View**
    - [IReadOnlyEntityCollectionView]
    - [IEntityCollectionView]
    - [EntityCollectionView]
    - [EntityCollectionViewBinder]


## Performance

## Best Practices



[//]: # ()
[//]: # (Guides)

[//]: # (├── Introduction)

[//]: # (│   ├── What is Atomic?)

[//]: # (│   ├── Requirements & Installation)

[//]: # (│   └── Using Odin Inspector &#40;optional&#41;)

[//]: # (│)

[//]: # (├── Core Concepts)

[//]: # (│   ├── Entities & EntityStateBehaviour Pattern)

[//]: # (│   ├── Reactive Values & Variables)

[//]: # (│   ├── Events & Signals)

[//]: # (│   └── Requests & Actions)

[//]: # (│)

[//]: # (├── Tutorials)

[//]: # (│   ├── Getting Started with Entities &#40;C# example&#41;)

[//]: # (│   ├── Unity Quick Start &#40;SceneEntity, Installers&#41;)

[//]: # (│   ├── Character Example &#40;MoveBehaviour&#41;)

[//]: # (│   └── Building UI Contexts)

[//]: # (│)

[//]: # (├── Best Practices)

[//]: # (│   ├── Prefer Abstract Interfaces)

[//]: # (│   ├── Shared Constants)

[//]: # (│   ├── Iterating Reactive Collections)

[//]: # (│   ├── Request-Condition-Action-Event Pattern)

[//]: # (│   ├── Requests vs Actions)

[//]: # (│   └── Performance Tips)




