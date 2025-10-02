# 🧩 IEntityFactory\<E>

```csharp
public interface IEntityFactory<out E> where E : IEntity
```

- **Description:** Defines a generic factory interface for creating instances of [IEntity](../Entities/IEntity.md)-based
  types.
- **Type Parameter:** `E` — The type of `IEntity` this factory creates.
- **See also:** [IEntityFactory](IEntityFactory.md)

---

## 🏹 Methods

#### `Create()`

```csharp
public E Create();
```

- **Description:** Creates and returns a new instance of the entity type `E`.
- **Returns:** A new instance of type `E`.

---

## 🗂 Examples of Usage

Create a generic factory for a `UnitEntity`:

```csharp
//Define UnitEntity
public UnitEntity : Entity
{
}
```

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

```csharp
//Usage
IEntityFactory<UnitEntity> factory = new UnitEntityFactory();
UnitEntity unit = factory.Create();
```

> This approach is type-safe and avoids casting, ideal for systems working with specific entity types.

---

