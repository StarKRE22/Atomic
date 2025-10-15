# 🧩 Entities

An **Entity** is the fundamental element in the framework. Each entity is a container that holds **tags**,
**properties**, **behaviors**, and own **lifecycle**. This container is **dynamic** and strictly separates **data** from
**logic**, which allows for rapid development of game mechanics and their reuse. Thus, every game object, entity, and
system is a **composition**.

---

---

## 🔍 API Reference

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
<summary><a href="Entity.md"><strong>Entity</strong></a></summary>

<ul>
  <li><a href="EntityCore.md">Core</a></li>
  <li><a href="EntityTags.md">Tags</a></li>
  <li><a href="EntityValues.md">Values</a></li>
  <li><a href="EntityBehaviours.md">Behaviours</a></li>
  <li><a href="EntityLifecycle.md">Lifecycle</a></li>
  <li><a href="EntityDebug.md">Debug</a></li>
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

<details>
<summary><a href="SceneEntityProxies.md"><strong>SceneEntityProxies</strong></a></summary>
    <ul>
      <li><a href="SceneEntityProxy.md">SceneEntityProxy</a></li>
      <li><a href="SceneEntityProxy.md">SceneEntityProxy&lt;T&gt;</a></li>
    </ul>
</details>

<details>
<summary><strong>Singletons</strong></summary>
    <ul>
      <li><a href="EntitySingleton.md">EntitySingleton</a></li>
      <li><a href="SceneEntitySingleton.md">SceneEntitySingleton</a></li>
    </ul>
</details>

<details>
<summary><strong>Extensions</strong></summary>
    <ul>
      <li><a href="ExtensionsCore.md">Core</a></li>
      <li><a href="ExtensionsTags.md">Tags</a></li>
      <li><a href="ExtensionsValues.md">Values</a></li>
      <li><a href="ExtensionsBehaviours.md">Behaviours</a></li>
      <li><a href="ExtensionsInstalling.md">Installing</a></li>
      <li><a href="ExtensionsRetrieval.md">Retrieval</a></li>
    </ul>
</details>