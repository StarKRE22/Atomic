# 🧩️ Entity Factories

**Entity Factories** are responsible for creating instances of [IEntity](../Entities/IEntity.md) or its subclasses. They
provide a structured way to encapsulate entity creation logic, optionally pre-configuring tags, values, and behaviours.
Factories can be **generic** or **non-generic**, **scene-based**, **scriptable**, or **inline**.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [InlineEntityFactory](#ex1)
    - [ScriptableEntityFactory\<E>](#ex2)
    - [ScriptableEntityCatalog](#ex3)
    - [MultiEntityFactory](#ex4)
- [API Reference](#-api-reference)
- [Notes](#-notes)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

Below are examples of using factories for different scenarios:

<div id="ex1"></div>

### 1️⃣ InlineEntityFactory

```csharp
var factory = new InlineEntityFactory(() =>
{
    var entity = new Entity();
    entity.AddValue<int>("Health", 100);
    entity.AddTag("Enemy");
    return entity;
});

IEntity myEntity = factory.Create();
```

- **Description:** Creates an entity on demand with minimal boilerplate.
- **Use Case:** Quick instantiation for tests, temporary entities, or procedural content.

---

<div id="ex2"></div>

### 2️⃣ ScriptableEntityFactory\<E>

```csharp
public class EnemyFactory : ScriptableEntityFactory<EnemyEntity>
{
    public override EnemyEntity Create()
    {
        var enemy = new EnemyEntity();
        enemy.AddValue<int>("Health", 200);
        enemy.AddValue<int>("Damage", 30);
        return enemy;
    }
}
```

- **Description:** ScriptableObject-based factory.
- **Use Case:** Centralized, reusable entity creation, suitable for runtime and editor workflows.

---

<div id="ex3"></div>

### 3️⃣ ScriptableEntityCatalog

```csharp
IMultiEntityFactory multiFactory = new MultiEntityFactory();
multiFactory.Register("Orc", new InlineEntityFactory(() => new EnemyEntity("Orc")));
multiFactory.Register("Goblin", new InlineEntityFactory(() => new EnemyEntity("Goblin")));

IEntity orc = registry.Create("Orc");
```

- **Description:** Factory registry keyed by string or generic type.
- **Use Case:** Managing multiple entity factories dynamically at runtime.

---

## 🔍 API Reference

Below is a list of available factory types:

- **EntityFactories**
    - [IEntityFactory](IEntityFactory.md) <!-- + -->
    - [IEntityFactory&lt;E&gt;](IEntityFactory%601.md) <!-- + -->
    - [ScriptableEntityFactory](ScriptableEntityFactory.md) <!-- + -->
    - [ScriptableEntityFactory&lt;E&gt;](ScriptableEntityFactory%601.md) <!-- + -->
    - [InlineEntityFactory](InlineEntityFactory.md) <!-- + -->
    - [InlineEntityFactory&lt;E&gt;](InlineEntityFactory%601.md) <!-- + -->
- **MultiEntityFactories**
    - [IMultiEntityFactory](IMultiEntityFactory.md) <!-- + -->
    - [IMultiEntityFactory&lt;E&gt;](IMultiEntityFactory%601.md) <!-- + -->
    - [MultiEntityFactory](MultiEntityFactory.md) <!-- + -->
    - [MultiEntityFactory&lt;E&gt;](MultiEntityFactory%601.md) <!-- + -->
    - [ScriptableEntityCatalog](ScriptableEntityCatalog.md) <!-- + -->
    - [ScriptableEntityCatalog&lt;E&gt;](ScriptableEntityCatalog%601.md) <!-- + -->


---

## 📝 Notes

- Use **`IEntityFactory`** for standard creation interfaces.
- Use **`IMultiEntityFactory`** / **`MultiEntityFactory`** for registry-like scenarios.
- Use **`MonoEntityBaker`** or **`ScriptableEntityFactory`** when integrating with Unity workflows.
- Use **`InlineEntityFactory`** for lightweight, temporary, or lambda-based entity creation.
- **Generic versions** provide type-safety and avoid unnecessary casting.

---

## 📌 Best Practices

- [Building Entity System with Model & View Separation](../../BestPractices/EntitySystem.md)  <!-- + -->
- [Overriding EntityFactories with EntityBakers](../../BestPractices/OverrideEntityFactoriesWithBakers.md) <!-- + -->
- [Upgrading EntityFactory to the Builder](../../BestPractices/UpgradingEntityFactoryToBuilder.md) <!-- + -->
- [Combine EntityPool with EntityFactory](../../BestPractices/UsingEntityPoolWithFactories.md) <!-- + -->