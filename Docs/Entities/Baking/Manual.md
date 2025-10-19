# 🧩 Scene Entity Bakers

**Scene Entity Bakers** are responsible for converting Unity GameObjects into [IEntity](../Entities/IEntity.md)
instances. They provide a structured way to bake scene objects into runtime entities, pre-configuring tags, values, and
behaviours. Bakers can be generic ([SceneEntityBaker\<E>](SceneEntityBaker%601.md)) or non-generic
([SceneEntityBaker](SceneEntityBaker.md)), depending on whether you want type-safe entity creation.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [SceneEntityBaker](#ex1)
  - [SceneEntityBaker\<E>](#ex2)
- [API Reference](#-api-reference)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ SceneEntityBaker

```csharp
public class EnemyBaker : SceneEntityBaker
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

### 2️⃣ SceneEntityBaker<E>

```csharp
public class EnemyBaker : SceneEntityBaker<EnemyEntity>
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

### 3️⃣ Bake All

```csharp
// Bake all IEntity bakers in all active scenes
IEntity[] enemies = EnemyBaker.BakeAll();

EntityWorld world = new EntityWorld();
world.AddRange(enemies);
```

---

## 🔍 API Reference

- [SceneEntityBaker](SceneEntityBaker.md) <!-- + -->
- [SceneEntityBaker&lt;E&gt;](SceneEntityBaker%601.md) <!-- + -->

---

## 📝 Notes

- Use [SceneEntityBaker](SceneEntityBaker.md) when you want a non-generic, unified approach for multiple entity types.
- Use [SceneEntityBaker\<E>](SceneEntityBaker%601.md) for type-safe entity creation and compile-time guarantees.
- Both types integrate with Unity workflows, allowing pre-configured tags, values, and behaviours to be baked directly
  from scene objects.
- Generic versions avoid casting and provide clearer API for runtime logic.
- Parameters like `initialTagCapacity` and `initialValueCapacity` help **optimize entity creation** and reduce runtime
  overhead.
