# 🧩️ InlineEntityFactory

```csharp
public class InlineEntityFactory : InlineEntityFactory<IEntity>, IEntityFactory
```

- **Description:** A lightweight, inline implementation of the non-generic entity factory.
- **Inheritance:** [InlineEntityFactory\<E>](InlineEntityFactory%601.md), [IEntityFactory](IEntityFactory.md)

> [!TIP]
> **InlineEntityFactory** can be used as a lightweight mock for unit tests, allowing quick creation of test entities
> without implementing a full factory class.

---

## 🏗️ Constructor

#### `InlineEntityFactory(Func<IEntity> createFunc)`

```csharp
public InlineEntityFactory(Func<IEntity> createFunc);
```

- **Description:** Initializes a new inline factory using the specified creation function.
- **Parameter:** `createFunc` — The delegate used to instantiate the entity. Cannot be `null`.
- **Throws:** `ArgumentNullException` if `createFunc` is `null`.

---

## 🏹 Methods

#### `Create()`

```csharp
public override IEntity Create();
```

- **Description:** Invokes the wrapped creation delegate to produce a new instance of [IEntity](../Entities/IEntity.md).
- **Returns:** A new [IEntity](../Entities/IEntity.md) instance.
- **Notes:** Inherited from [InlineEntityFactory\<IEntity>](InlineEntityFactory%601.md).

---

## 🗂 Example of Usage

```csharp
//Create a factory
var factory = new InlineEntityFactory(() =>
{
    var entity = new Entity();
    entity.AddValue<int>("Health", 100);
    return entity;
});

//Create an entity
IEntity myEntity = factory.Create();
```