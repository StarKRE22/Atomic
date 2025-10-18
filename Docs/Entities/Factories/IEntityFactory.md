# 🧩 IEntityFactory

Non-generic factory interface that produces a base [IEntity](../Entities/IEntity.md) instance.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Create()](#create)

---

## 🗂 Example of Usage

Create a non-generic factory for an [Entity](../Entities/Entity.md):

```csharp
public sealed class UnitEntityFactory : IEntityFactory
{
    public IEntity Create()
    {
        var entity = new Entity();
        entity.AddTag("Unit");
        entity.AddValue<int>("Health", 150);
        entity.AddValue<int>("Damage", 25);
        entity.AddBehaviour<MoveBehaviour>();
        return entity;
    }
}
```

Use this factory in a project:

```csharp
IEntityFactory factory = new UnitEntityFactory();
Entity unit = factory.Create();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityFactory : IEntityFactory<IEntity>
```

- **Inheritance:** [IEntityFactory\<E>](IEntityFactory%601.md) 

---

### 🏹 Methods

#### `Create()`

```csharp
public IEntity Create();
```

- **Description:** Creates a new instance of the entity.
- **Returns:** A new instance of [IEntity](../Entities/IEntity.md).