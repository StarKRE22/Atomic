# 🧩 Entities

An **Entity** is the fundamental element in the framework. Each entity is a container that holds **tags**,
**properties**, **behaviors**, and own **lifecycle**. This container is **dynamic** and strictly separates **data** from
**logic**, which allows for rapid development of game mechanics and their reuse. Thus, every game object, entity, and
system is a **composition**.

Below are the interfaces and classes for working with entities.

<details>
<summary><a href="IEntity.md"><strong>IEntity</strong></a></summary>

<ul>
  <li><a href="IEntityCore.md">Core</a></li>
  <li><a href="IEntityTags.md">Tags</a></li>
  <li><a href="IEntityValues.md">Values</a></li>
  <li><a href="IEntityBehaviours.md">Behaviours</a></li>
  <li><a href="IEntityLifecycle.md">Lifecycle</a></li>
</ul>

</details>



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
    - [SceneEntitySingleton](SceneEntitySingleton.md) <!-- + -->
- **Proxies**
    - [SceneEntityProxies](SceneEntityProxies.md) <!-- + -->
        - [SceneEntityProxy](SceneEntityProxy.md) <!-- + -->
        - [SceneEntityProxy&lt;T&gt;](SceneEntityProxy.md) <!-- + -->
- **Extensions**
    - [Core](ExtensionsCore.md)  <!-- + -->
    - [Tags](ExtensionsTags.md) <!-- + -->
    - [Values](ExtensionsValues.md) <!-- + -->
    - [Behaviours](ExtensionsBehaviours.md) <!-- + -->
    - [Installing](ExtensionsInstalling.md) <!-- + -->
    - [Retrieval](ExtensionsRetrieval.md) <!-- + -->