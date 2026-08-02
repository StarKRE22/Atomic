# 🧩 Mono Entity Bakers

**Mono Entity Bakers** are responsible for converting Unity GameObjects into [IEntity](../Entities/IEntity.md)
instances. They provide a structured way to bake scene objects into runtime entities, pre-configuring tags, values, and
behaviours. Bakers can be generic ([MonoEntityBaker\<E>](MonoEntityBaker%601.md)) or non-generic
([MonoEntityBaker](MonoEntityBaker.md)), depending on whether you want type-safe entity creation.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [MonoEntityBaker](#ex1)
    - [MonoEntityBaker\<E>](#ex2)
    - [Bake All](#ex3)
- [API Reference](#-api-reference)
- [Notes](#-notes)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ MonoEntityBaker

```csharp
public class EnemyBaker : MonoEntityBaker
{
    protected override void Install(IEntity entity)
    {
        entity.AddTag("Enemy");
        entity.AddValue<int>("Health", 100);
        entity.AddValue<float>("Speed", 5f);
        entity.AddBehaviour<PatrolBehaviour>();
    }
}
```

<div id="ex2"></div>

### 2️⃣ MonoEntityBaker<E>

```csharp
public class EnemyBaker : MonoEntityBaker<EnemyEntity>
{
    protected override EnemyEntity Create()
    {
        var enemy = new EnemyEntity(
            this.name,
            tagCapacity: this.initialTagCapacity,
            valueCapacity: this.initialValueCapacity,
            behaviourCapacity: this.initialBehaviourCapacity
        );
    
        enemy.AddTag("Enemy");
        enemy.AddValue<int>("Health", 100);
        enemy.AddValue<float>("Speed", 5f);
        enemy.AddBehaviour<PatrolBehaviour>();

        return enemy;
    }
}
```

<div id="ex3"></div>

### 3️⃣ Bake All

```csharp
// Bake all IEntity bakers in all active scenes
IEntity[] enemies = EnemyBaker.BakeAll();

EntityWorld world = new EntityWorld();
world.AddRange(enemies);
```

---

## 🔍 API Reference

- [MonoEntityBaker](MonoEntityBaker.md) <!-- + -->
- [MonoEntityBaker&lt;E&gt;](MonoEntityBaker%601.md) <!-- + -->

---

## 📝 Notes

- Use [MonoEntityBaker](MonoEntityBaker.md) when you want a non-generic, unified approach for multiple entity types.
- Use [MonoEntityBaker\<E>](MonoEntityBaker%601.md) for type-safe entity creation and compile-time guarantees.
- Both types integrate with Unity workflows, allowing pre-configured tags, values, and behaviours to be baked directly
  from scene objects.
- Generic versions avoid casting and provide clearer API for runtime logic.
- Parameters like `initialTagCapacity` and `initialValueCapacity` help **optimize entity creation** and reduce runtime
  overhead.

---

## 📌 Best Practices

- [Overriding EntityFactories with EntityBakers](../../BestPractices/OverrideEntityFactoriesWithBakers.md) <!-- + -->

<!--

### What is Baking?

In the context of `MonoEntityBaker`, **Baking** is the process of taking a **GameObject** from a Unity scene and converting it into a fully configured **entity**.

Baking can be thought of as **overriding factory defaults**. The `ScriptableEntityFactory<E>` provides default properties for an entity (e.g., health, damage, stats). During baking, a `MonoEntityBaker` can:

- Customize these properties for the specific GameObject.
- Apply scene-specific values.
- Optionally destroy the GameObject after baking.

Additionally, **the Baker itself implements the `IEntityFactory<E>` interface**, meaning it can act as a factory directly, allowing code to create entities either via the assigned factory or via baking logic.

This allows you to reuse factory defaults while still customizing entities directly in the scene without manually changing the factory itself.

## Key Features

- **Generic and non-generic versions**: Supports both specific entity types (`MonoEntityBaker<E>`) and a shortcut for general `IEntity` (`MonoEntityBaker`).
- **Factory-based entity creation**: Uses `ScriptableEntityFactory<E>` to instantiate entities, ensuring consistent creation logic.
- **Baker as a factory**: Can be used as an `IEntityFactory<E>` itself, providing flexible entity creation directly from the Baker.
- **Customizable entity setup**: Subclasses implement `Install(E entity)` to configure baked entities with scene-specific data.
- **Automatic GameObject cleanup**: Optionally destroys the GameObject after baking to avoid leftover objects in the scene.
- **Batch baking support**:
  - Bake all entities in the current scene.
  - Bake all entities under a specific `GameObject`.
  - Bake all entities in a specific `Scene`.
  - Bake directly into an existing `ICollection<E>` to reuse memory or append results.
- **Active/inactive inclusion**: Can optionally include inactive GameObjects when searching for bakers.
- **Editor-friendly**: Inspector fields allow configuring destruction behavior and linking the factory directly in the Unity Editor.

-->
