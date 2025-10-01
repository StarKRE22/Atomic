#  🧩 IEntityInit&lt;E&gt;

```csharp
public interface IEntityInit<in E> : IEntityInit where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityInit` for handling initialization logic for a specific
  `IEntity` type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityInit](IEntityInit.md)
- **Remarks:** This method is automatically invoked by `IEntity.Init` when the behavior is registered on an entity of
  type `E`.

---

## 🏹 Methods

#### `Init(E)`

```csharp
public void Init(E entity);
```

- **Description:** Called when the typed entity is initialized.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityInit.Init(IEntity)` explicitly by casting the `IEntity` to type `E`.

---

## 🗂 Example of Usage

Set up a `Color` for the `Renderer` of unit entity

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class InitColorBehaviour : IEntityInit<UnitEntity>
{
    public void Init(UnitEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        var color = entity.GetValue<Color>("Color");
        renderer.material.color = color;
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required