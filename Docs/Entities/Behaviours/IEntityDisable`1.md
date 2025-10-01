#  🧩 IEntityDisable&lt;E&gt;

```csharp
public interface IEntityDisable<in E> : IEntityDisable where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityDisable` for handling disable logic for a specific
  `IEntity` type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityDisable](IEntityDisable.md)
- **Note:** Automatically invoked by `IEntity.Disable` when the behavior is registered on an entity of type `E`.

---

## 🏹 Methods

#### `Disable(E)`

```csharp
public void Disable(E entity);
```

- **Description:** Called when the typed entity is disabled.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityDisable.Disable(IEntity)` explicitly by casting the `IEntity` to type `E`.

---

## 🗂 Example of Usage

Disable a `Renderer` for a unit entity

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class DisableRendererBehaviour : IEntityDisable<UnitEntity>
{
    public void Disable(UnitEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = false;
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.