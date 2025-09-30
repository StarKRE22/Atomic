# 🧩 Entities

An **Entity** is the fundamental element in the framework. Each entity is a container that holds **tags**,
**properties**, **behaviors**, and own **lifecycle**. This container is **dynamic** and strictly separates **data** from
**logic**, which allows for rapid development of game mechanics and their reuse. Thus, every game object, entity, and
system is a **composition**.

Below are the interfaces and classes for working with entities.

- **[IEntity](IEntity.md)** <!-- + -->
    - [Core](IEntityCore.md) <!-- + -->
    - [Tags](IEntityTags.md) <!-- + -->
    - [Values](IEntityValues.md) <!-- + -->
    - [Behaviours](IEntityBehaviours.md) <!-- + -->
    - [Lifecycle](IEntityLifecycle.md) <!-- + -->
- **[Entity](Entity.md)** <!-- + -->
    - [Core](EntityCore.md) <!-- + -->
    - [Tags](EntityTags.md) <!-- + -->
    - [Values](EntityValues.md) <!-- + -->
    - [Behaviours](EntityBehaviours.md) <!-- + -->
    - [Lifecycle](EntityLifecycle.md) <!-- + -->
    - [Debug](EntityDebug.md) <!-- + -->
- **[SceneEntity](SceneEntity.md)** <!-- + -->
    - [Core](SceneEntityCore.md) <!-- + -->
    - [Tags](SceneEntityTags.md) <!-- + -->
    - [Values](SceneEntityValues.md) <!-- + -->
    - [Behaviours](SceneEntityBehaviours.md) <!-- + -->
    - [Lifecycle](SceneEntityLifecycle.md) <!-- + -->
    - [Installing](SceneEntityInstalling.md) <!-- + -->
    - [Gizmos](SceneEntityGizmos.md) <!-- + -->
    - [Debug](SceneEntityDebug.md) <!-- + -->
    - [Editor](SceneEntityEditor.md) <!-- + -->
    - [Creation](SceneEntityCreation.md) <!-- + -->
    - [Destruction](SceneEntityDestruction.md) <!-- + -->
    - [Casting](SceneEntityCasting.md) <!-- + -->
- **Singletons**
    - [EntitySingleton](EntitySingleton.md) <!-- + -->
    - [SceneEntitySingleton](SceneEntitySingleton.md)
- **Proxies**
  - [SceneEntityProxy](SceneEntityProxy.md)
- **Extensions**
    - [Core](ExtensionsCore.md)
    - [Tags](ExtensionsTags.md)
    - [Values](ExtensionsValues.md)
    - [Behaviours](ExtensionsBehaviours.md)