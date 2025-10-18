# 🧩 IEntityFactory\<E>

Defines a generic factory interface for creating instances of [IEntity](../Entities/IEntity.md)-based
types.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Create()](#create)

---

## 🗂 Example of Usage

Assume we have a `UnitEntity` type derived from [Entity](../Entities/Entity.md)

```csharp
public UnitEntity : Entity
{
}
```

Create a generic factory for the `UnitEntity`:

```csharp
//Create factory for UnitEntity 
public sealed class UnitEntityFactory : IEntityFactory<UnitEntity>
{
    public UnitEntity Create()
    {
        var unit = new UnitEntity();
        unit.AddTag("Unit");
        unit.AddValue<int>("Health", 150);
        unit.AddValue<int>("Damage", 25);
        unit.AddBehaviour<MoveBehaviour>();
        return unit;
    }
}
```

Use this factory in your project:

```csharp
IEntityFactory<UnitEntity> factory = new UnitEntityFactory();
UnitEntity unit = factory.Create();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityFactory<out E> where E : IEntity
```

- **Type Parameter:** `E` — The type of `IEntity` this factory creates.
- **See also:** [IEntityFactory](IEntityFactory.md)

---

### 🏹 Methods

#### `Create()`

```csharp
public E Create();
```

- **Description:** Creates and returns a new instance of the entity type `E`.
- **Returns:** A new instance of type `E`.