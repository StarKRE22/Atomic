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


<details>
<summary><a href="SceneEntity.md"><strong>SceneEntity</strong></a></summary>

<ul>
  <li><a href="SceneEntityCore.md">Core</a></li>
  <li><a href="SceneEntityTags.md">Tags</a></li>
  <li><a href="SceneEntityValues.md">Values</a></li>
  <li><a href="SceneEntityBehaviours.md">Behaviours</a></li>
  <li><a href="SceneEntityLifecycle.md">Lifecycle</a></li>
  <li><a href="SceneEntityInstalling.md">Installing</a></li>
  <li><a href="SceneEntityGizmos.md">Gizmos</a></li>
  <li><a href="SceneEntityDebug.md">Debug</a></li>
  <li><a href="SceneEntityEditor.md">Editor</a></li>
  <li><a href="SceneEntityCreation.md">Creation</a></li>
  <li><a href="SceneEntityDestruction.md">Destruction</a></li>
  <li><a href="SceneEntityCasting.md">Casting</a></li>
</ul>

</details>

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