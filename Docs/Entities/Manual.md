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
- [API Reference](#api-reference)
- [Performance](#performance)
- [Best Practices](#-best-practices)

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

## API Reference

- **Entities**
  - [IEntity](ApiReference/Entities/IEntity.md)
  - [Entity](ApiReference/Entities/Entity.md)
  - [EntitySingleton](ApiReference/Entities/EntitySingleton.md)
  - [SceneEntity](ApiReference/Entities/SceneEntity.md)
  - [SceneEntityProxy](ApiReference/Entities/SceneEntityProxy.md)
  - [SceneEntitySingleton](ApiReference/Entities/SceneEntitySingleton.md)
  - [Extensions](ApiReference/Entities/Extensions.md)
  
- **Behaviours**
  - [IEntityBehaviour](ApiReference/Behaviours/IEntityBehaviour.md)
  - [IEntityInit](ApiReference/Behaviours/IEntityInit.md)
  - [IEntityDispose](ApiReference/Behaviours/IEntityDispose.md)
  - [IEntityEnable](ApiReference/Behaviours/IEntityEnable.md)
  - [IEntityDisable](ApiReference/Behaviours/IEntityDisable.md)
  - [IEntityUpdate](ApiReference/Behaviours/IEntityUpdate.md)
  - [IEntityFixedUpdate](ApiReference/Behaviours/IEntityFixedUpdate.md)
  - [IEntityLateUpdate](ApiReference/Behaviours/IEntityLateUpdate.md)
  - [IEntityGizmos](ApiReference/Behaviours/IEntityGizmos.md)
  - [RunInEditModeAttribute](ApiReference/Attributes/RunInEditModeAttribute.md)
  
- **Installers**
  - [IEntityInstaller](ApiReference/Installers/IEntityInstaller.md)
  - [SceneEntityInstaller](ApiReference/Installers/SceneEntityInstaller.md)
  - [ScriptableEntityInstaller](ApiReference/Installers/ScriptableEntityInstaller.md)
  
- **Aspects**
  - [IEntityAspect](ApiReference/Aspects/IEntityAspect.md)
  - [ScriptableEntityAspect](ApiReference/Aspects/ScriptableEntityAspect.md)
  - [SceneEntityAspect](ApiReference/Aspects/SceneEntityAspect.md)

- **Factories**
  - [IEntityFactory](ApiReference/Factories/IEntityFactory.md)
  - [ScriptableEntityFactory](ApiReference/Factories/ScriptableEntityFactory.md)
  - [SceneEntityFactory](ApiReference/Factories/SceneEntityFactory.md)
  - [InlineEntityFactory](ApiReference/Factories/InlineEntityFactory.md)
  - [IMultiEntityFactory](ApiReference/Factories/IMultiEntityFactory.md)
  - [MultiEntityFactory](ApiReference/Factories/MultiEntityFactory.md)
  - [SceneEntityBaker](ApiReference/Factories/SceneEntityBaker.md)
  - [IEntityFactoryCatalog](ApiReference/Factories/IEntityFactoryCatalog.md)
  - [ScriptableEntityFactoryCatalog](ApiReference/Factories/ScriptableEntityFactoryCatalog.md)

- **Pooling**
  - [IEntityPool](ApiReference/Pooling/IEntityPool.md)
  - [EntityPool](ApiReference/Pooling/EntityPool.md)
  - [SceneEntityPool](ApiReference/Pooling/SceneEntityPool.md)
  - [IMultiEntityPool](ApiReference/Pooling/IMultiEntityPool.md)
  - [MultiEntityPool](ApiReference/Pooling/MultiEntityPool.md)
  - [IPrefabEntityPool](ApiReference/Pooling/IPrefabEntityPool.md)
  - [PrefabEntityPool](ApiReference/Pooling/PrefabEntityPool.md)

- **Collections**
  - [IReadOnlyEntityCollection](ApiReference/Collections/IReadOnlyEntityCollection.md)
  - [IEntityCollection](ApiReference/Collections/IEntityCollection.md)
  - [EntityCollection](ApiReference/Collections/EntityCollection.md)
  - [Extensions](ApiReference/Collections/Extensions.md)

- **Worlds**
  - [IEntityWorld](ApiReference/Worlds/IEntityWorld.md)
  - [EntityWorld](ApiReference/Worlds/EntityWorld.md)
  - [SceneEntityWorld](ApiReference/Worlds/SceneEntityWorld.md)

- **Registry**
  - [EntityRegistry](ApiReference/Registry/EntityRegistry.md)

- **Filters & Triggers**
  - [EntityFilter](ApiReference/Filters/EntityFilter.md)
  - [IEntityTrigger](ApiReference/Filters/IEntityTrigger.md)
  - [EntityTriggerBase](ApiReference/Filters/EntityTriggerBase.md)
  - [TagEntityTrigger](ApiReference/Filters/TagEntityTrigger.md)
  - [ValueEntityTrigger](ApiReference/Filters/ValueEntityTrigger.md)
  - [BehaviourEntityTrigger](ApiReference/Filters/BehaviourEntityTrigger.md)
  - [StateChangedEntityTrigger](ApiReference/Filters/StateChangedEntityTrigger.md)
  - [SubscriptionEntityTrigger](ApiReference/Filters/SubscriptionEntityTrigger.md)
  - [InlineEntityTrigger](ApiReference/Filters/InlineEntityTrigger.md)

- **Lifecycle Sources & Subscriptions**
  - [IInitSource](ApiReference/Lifecycle/Sources/IInitSource.md)
  - [IEnableSource](ApiReference/Lifecycle/Sources/IEnableSource.md)
  - [IUpdateSource](ApiReference/Lifecycle/Sources/IUpdateSource.md)
  - [InitSubscription](ApiReference/Lifecycle/Subscriptions/InitSubscription.md)
  - [EnableSubscription](ApiReference/Lifecycle/Subscriptions/EnableSubscription.md)
  - [DisableSubscription](ApiReference/Lifecycle/Subscriptions/DisableSubscription.md)
  - [DisposeSubscription](ApiReference/Lifecycle/Subscriptions/DisposeSubscription.md)
  - [UpdateSubscription](ApiReference/Lifecycle/Subscriptions/UpdateSubscription.md)
  - [FixedUpdateSubscription](ApiReference/Lifecycle/Subscriptions/FixedUpdateSubscription.md)
  - [LateUpdateSubscription](ApiReference/Lifecycle/Subscriptions/LateUpdateSubscription.md)
  - [Extensions](ApiReference/Lifecycle/Extensions.md)

- **Utils**
  - [EntityUtils](ApiReference/Utils/EntityUtils.md)
  - [EntityNames](ApiReference/Utils/EntityNames.md)

- **UI**
  - [EntityView](ApiReference/UI/EntityView.md)
  - [EntityViewCatalog](ApiReference/UI/EntityViewCatalog.md)
  - [EntityViewPool](ApiReference/UI/EntityViewPool.md)
  - [EntityCollectionView](ApiReference/UI/EntityViewPool.md)


## Performance

## 📌 Best Practices