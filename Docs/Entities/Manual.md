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

## API Reference

- [Entities]()
  - [IEntity](Entities/IEntity.md)
  - [Entity](Entities/Entity.md) 
  - [SceneEntity](Entities/SceneEntity.md)
  - [Extensions](Entities/Extensions.md)

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

- [Factories]
  - [IEntityFactory](Factories/IEntityFactory.md)
  - [SceneEntityFactory](Factories/SceneEntityFactory.md)
  - [ScriptableEntityFactory](Factories/ScriptableEntityFactory.md)
  - [InlineEntityFactory](Factories/InlineEntityFactory.md)
  - [IMultiEntityFactory](Factories/IMultiEntityFactory.md)
  - [MultiEntityFactory](Factories/MultiEntityFactory.md)
  - [IEntityFactoryCatalog](Factories/IEntityFactoryCatalog.md)
  - [ScriptableEntityFactoryCatalog](Factories/ScriptableEntityFactoryCatalog.md)

- [Baking]
  - [SceneEntityBaker](Factories/SceneEntityBaker.md)

- [Pooling]
  - [IEntityPool](Pooling/IEntityPool.md)
  - [EntityPool](Pooling/EntityPool.md)
  - [SceneEntityPool](Pooling/SceneEntityPool.md)
  - [IMultiEntityPool](Pooling/IMultiEntityPool.md)
  - [MultiEntityPool](Pooling/MultiEntityPool.md)
  - [IPrefabEntityPool](Pooling/IPrefabEntityPool.md)
  - [PrefabEntityPool](Pooling/PrefabEntityPool.md)

- [Collections]
  - [IReadOnlyEntityCollection](Collections/IReadOnlyEntityCollection.md)
  - [IEntityCollection](Collections/IEntityCollection.md)
  - [EntityCollection](Collections/EntityCollection.md)
  - [Extensions](Collections/Extensions.md)

- [Worlds]
  - [IEntityWorld](Worlds/IEntityWorld.md)
  - [EntityWorld](Worlds/EntityWorld.md)
  - [SceneEntityWorld](Worlds/SceneEntityWorld.md)

- [Filters & Triggers]
  - [EntityFilter](Filters/EntityFilter.md)
  - [IEntityTrigger](Filters/IEntityTrigger.md)
  - [EntityTriggerBase](Filters/EntityTriggerBase.md)
  - [TagEntityTrigger](Filters/TagEntityTrigger.md)
  - [ValueEntityTrigger](Filters/ValueEntityTrigger.md)
  - [BehaviourEntityTrigger](Filters/BehaviourEntityTrigger.md)
  - [StateChangedEntityTrigger](Filters/StateChangedEntityTrigger.md)
  - [SubscriptionEntityTrigger](Filters/SubscriptionEntityTrigger.md)
  - [InlineEntityTrigger](Filters/InlineEntityTrigger.md)

- [Registry]
  - [EntityRegistry](Registry/EntityRegistry.md)

- [UI]
  - [EntityView](UI/EntityView.md)
  - [EntityViewCatalog](UI/EntityViewCatalog.md)
  - [EntityViewPool](UI/EntityViewPool.md)
  - [EntityCollectionView](UI/EntityViewPool.md)

- [Aspects]
  - [IEntityAspect](Aspects/IEntityAspect.md)
  - [SceneEntityAspect](Aspects/SceneEntityAspect.md)
  - [ScriptableEntityAspect](Aspects/ScriptableEntityAspect.md)

- [Utils]
  - [EntityUtils](Utils/EntityUtils.md)
  - [EntityNames](Utils/EntityNames.md)

- [Framework Lifecycle]
  - [IInitSource](Lifecycle/Sources/IInitSource.md)
  - [IEnableSource](Lifecycle/Sources/IEnableSource.md)
  - [IUpdateSource](Lifecycle/Sources/IUpdateSource.md)
  - [InitSubscription](Lifecycle/Subscriptions/InitSubscription.md)
  - [EnableSubscription](Lifecycle/Subscriptions/EnableSubscription.md)
  - [DisableSubscription](Lifecycle/Subscriptions/DisableSubscription.md)
  - [DisposeSubscription](Lifecycle/Subscriptions/DisposeSubscription.md)
  - [UpdateSubscription](Lifecycle/Subscriptions/UpdateSubscription.md)
  - [FixedUpdateSubscription](Lifecycle/Subscriptions/FixedUpdateSubscription.md)
  - [LateUpdateSubscription](Lifecycle/Subscriptions/LateUpdateSubscription.md)
  - [Extensions](Lifecycle/Extensions.md)

## 🤖 Generate Entity API

## Performance

## 📌 Best Practices

## Notes

Atomic.Entities provides

- **Clear separation of data and logic** through the Entity–State–Behaviour pattern;
- **Composition over inheritance**, allowing flexible and reusable designs;
- **Flexibility and scalability** for building game systems of any size;
- **Seamless integration with Unity** and the ability to extend for specific game projects.
