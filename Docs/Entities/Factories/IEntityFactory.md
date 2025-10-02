# 🧩 IEntityFactory

```csharp
public interface IEntityFactory : IEntityFactory<IEntity>
```

- **Description:** Non-generic factory interface that produces a base [IEntity](../Entities/IEntity.md) instance.
- **Inheritance:** [IEntityFactory<E>](IEntityFactory%601.md) 

---

## 🏹 Methods

#### `Create()`

```csharp
public IEntity Create();
```

- **Description:** Creates a new instance of the entity.
- **Returns:** A new instance of [IEntity](../Entities/IEntity.md).

## 🗂 Example of Usage

### Example #1. Non-Generic Factory

```csharp
public class BasicEntityFactory : IEntityFactory
{
    public IEntity Create()
    {
        var entity = new Entity();
        entity.SetValue("Health", 100);
        entity.SetValue("Name", "Unnamed");
        return entity;
    }
}
```

> Useful in registries that handle multiple different entity types without knowing them at compile time.

---

## Remarks

- `IEntityFactory` is most useful for registries, catalogs, and data-driven systems where entities are created
  dynamically.
- The generic form `IEntityFactory<T>` should be preferred when working with a single known entity type.
- Factories may be combined with pooling systems for efficient runtime entity management.  
