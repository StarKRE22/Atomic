# 🧩 IEntityFactory

```csharp
public interface IEntityFactory : IEntityFactory<IEntity>
```

- **Description:** Non-generic factory interface that produces a base [IEntity](../Entities/IEntity.md) instance.
- **Inheritance:** [IEntityFactory\<E>](IEntityFactory%601.md) 

---

## 🏹 Methods

#### `Create()`

```csharp
public IEntity Create();
```

- **Description:** Creates a new instance of the entity.
- **Returns:** A new instance of [IEntity](../Entities/IEntity.md).

---

## 🗂 Example of Usage

Create a non-generic factory for a `Entity`:

```csharp
//Create factory 
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

```csharp
//Usage
IEntityFactory factory = new UnitEntityFactory();
Entity unit = factory.Create();
```