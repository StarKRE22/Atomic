# 🧩️ Entity Factories

**Entity Factories** are responsible for creating instances of [IEntity](../Entities/IEntity.md) or its subclasses. They
provide a structured way to encapsulate entity creation logic, optionally pre-configuring tags, values, and behaviours.
Factories can be **generic** or **non-generic**, **scene-based**, **scriptable**, or **inline**.

Below is a list of available factory types:

- **Factories**
    - [IEntityFactory](IEntityFactory.md) <!-- + -->
    - [IEntityFactory&lt;E&gt;](IEntityFactory%601.md) <!-- + -->
    - [SceneEntityFactory](SceneEntityFactory.md) <!-- + -->
    - [SceneEntityFactory&lt;E&gt;](SceneEntityFactory%601.md) <!-- + -->
    - [ScriptableEntityFactory](ScriptableEntityFactory.md) <!-- + -->
    - [ScriptableEntityFactory&lt;E&gt;](ScriptableEntityFactory%601.md) <!-- + -->
    - [InlineEntityFactory](InlineEntityFactory.md) <!-- + -->
    - [InlineEntityFactory&lt;E&gt;](InlineEntityFactory%601.md) <!-- + -->
- **Multi Factories**
    - [IMultiEntityFactory](IMultiEntityFactory.md) <!-- + -->
    - [IMultiEntityFactory&lt;E&gt;](IMultiEntityFactory%601.md) <!-- + -->
    - [MultiEntityFactory](MultiEntityFactory.md) <!-- + -->
    - [MultiEntityFactory&lt;E&gt;](MultiEntityFactory%601.md) <!-- + -->
- **Factory Catalogs**
    - [IEntityFactoryCatalog](IEntityFactoryCatalog.md)
    - [IEntityFactoryCatalog&lt;E&gt;](IEntityFactoryCatalog%601.md)
    - [ScriptableEntityFactoryCatalog](ScriptableEntityFactoryCatalog%601.md)
    - [ScriptableEntityFactoryCatalog&lt;E&gt;](ScriptableEntityFactoryCatalog.md)

---

## 🗂 Examples of Usage

Below is an examples of using factories for different scenarios:

### 1️⃣ Inline Entity Factory

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

### 2️⃣ Scene Entity Factory

```csharp
public class EnemySceneFactory : SceneEntityFactory<EnemyEntity>
{
    public override EnemyEntity Create()
    {
        var enemy = new EnemyEntity();
        enemy.AddValue<int>("Health", 150);
        enemy.AddValue<int>("Damage", 25);
        return enemy;
    }
}
```

- **Description:** Scene-based factory using Unity `MonoBehaviour`.
- **Use Case:** Instantiation tied to scene objects with optional post-creation installation logic.

---

### 3️⃣ Scriptable Entity Factory

```csharp
public class EnemyScriptableFactory : ScriptableEntityFactory<EnemyEntity>
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

### 4️⃣ Multi-Entity Factory

```csharp
var registry = new MultiEntityFactory();
registry.Add("Orc", new InlineEntityFactory(() => new EnemyEntity("Orc")));
registry.Add("Goblin", new InlineEntityFactory(() => new EnemyEntity("Goblin")));

var orc = registry.Create("Orc");
```

- **Description:** Factory registry keyed by string or generic type.
- **Use Case:** Managing multiple entity factories dynamically at runtime.

---

## 📝 Notes

- Use **`IEntityFactory`** for standard creation interfaces.
- Use **`IMultiEntityFactory`** / **`MultiEntityFactory`** for registry-like scenarios.
- Use **`SceneEntityFactory`** or **`ScriptableEntityFactory`** when integrating with Unity workflows.
- Use **`InlineEntityFactory`** for lightweight, temporary, or lambda-based entity creation.
- **Generic versions** provide type-safety and avoid unnecessary casting.