# 🧩 Entities

An **Entity** is the fundamental element in the framework. Каждая сущность представляет собой контейнер, который
содержит в себе тэги, свойства, поведения и жизнненный цикл.
Этот контейнер динамический, и жестко разделяет данные и логику, что позволяет быстро разрабатывать игровые механики и
переиспользовать их. Таким образом каждый объект является композицией данных и логики

> [!IMPORTANT]
> Такой паттерн называется Entity-State-Behaviour.
> - **Entity** — a **container** that contains set of **data** (`State`) and **logic** (`Behaviour`), strictly separated
    from each other.
> - **State** — a set of `atomic` components that define the parameters of an entity.
> - **Behaviour** — a set of controllers that operate on the entity’s `State` they are attached to.

1. 

Ниже приведены 
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
- [Registry]
    - [EntityRegistry](Registry/EntityRegistry.md)