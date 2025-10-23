# 🧩 Entity Worlds

**Entity Worlds** are high-level managers that combine an [entity collection](../Collections/Manual.md) with a
[lifecycle system](../Lifecycle/Manual.md). They allow managing the state of multiple [IEntity](../Entities/IEntity.md)
objects at once, supporting enable/disable, updates, automatic registration, and Unity integration.

Worlds can be **generic** or **non-generic**, **pure code-based** or **Unity scene-bound**. They provide both low-level
collection operations and high-level control over entity behavior.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Generic EntityWorld](#ex1)
    - [Non-Generic EntityWorld](#ex2)
    - [Unity-Specific EntityWorld](#ex3)
    - [Auto-Scanning Entities](#ex4)
- [API Reference](#-api-reference)
- [Notes](#-notes)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Generic EntityWorld

```csharp
IEntityWorld<GameEntity> world = new EntityWorld<GameEntity>("GameWorld");
world.Enable();
world.Add(new GameEntity("Player"));
world.Tick(0.016f);
```

- **Description:** A generic world managing a specific type of entities.
- **Use Case:** When type-safe access to entities is required.

<div id="ex2"></div>

### 2️⃣ Non-Generic EntityWorld

```csharp
IEntityWorld world = new EntityWorld("GeneralWorld");
world.Add(new Entity("NPC"));
world.Add(new Entity("Prop"));
```

- **Description:** A universal world for any entity type.
- **Use Case:** Managing heterogeneous sets of entities without strict typing.

<div id="ex3"></div>

### 3️⃣ Unity-Specific EntityWorld

```csharp
SceneEntityWorld sceneWorld = SceneEntityWorld.Create("LevelWorld", scanEntities: true);
sceneWorld.OnAdded += e => Debug.Log($"Entity added: {e.name}");
```

- **Description:** A Unity-integrated world that automatically syncs with `MonoBehaviour` lifecycle.
- **Use Case:** Managing scene entities with automatic registration.

<div id="ex4"></div>

### 4️⃣ Auto-Scanning Entities

```csharp
public class GameEntityWorld : SceneEntityWorld<GameEntity> {}

GameEntityWorld world = GameEntityWorld.Create("BattleWorld", scanEntities: true);
// All GameEntity objects in the scene will be automatically discovered and registered
```

- **Description:** Uses built-in scanning to find all entities in the scene.
- **Use Case:** Ideal for dynamic Unity scenes with pre-placed entities.

---

## 🔍 API Reference

There are available interfaces and implementations of the entity world:

- **Interfaces**
    - [IEntityWorld](IEntityWorld.md) <!-- + -->
    - [IEntityWorld&lt;E&gt;](IEntityWorld%601.md) <!-- + -->
- **Plain Implementations**
    - [EntityWorld](EntityWorld.md) <!-- + -->
    - [EntityWorld&lt;E&gt;](EntityWorld%601.md) <!-- + -->
- **Unity Implementations**
    - [SceneEntityWorld](SceneEntityWorld.md) <!-- + -->
    - [SceneEntityWorld&lt;E&gt;](SceneEntityWorld%601.md) <!-- + -->

---

## 📝 Notes

- Use [EntityWorld&lt;E&gt;](EntityWorld%601.md) for type safety and strict entity management.
- Use [SceneEntityWorld](SceneEntityWorld.md) for Unity integration and automatic scene entity registration.
- Event system (`OnAdded`, `OnRemoved`, `OnEnabled`, `OnTicked`) supports **reactive architectures**.
- Worlds support **enable/disable** and a full update cycle (`Tick`, `FixedTick`, `LateTick`).
- All worlds are compatible with **IEntityCollection** and inherit its base functionality.

---

## 📌 Best Practices

- [Iterating over EntityCollections, Worlds and Filters.](../../BestPractices/IteratingOverEntityCollections.md)  <!-- + -->
- [Building Entity System with Model & View Separation](../../BestPractices/EntitySystem.md)  <!-- + -->