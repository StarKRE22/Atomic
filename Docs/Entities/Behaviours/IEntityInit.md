# 🧩️ IEntityInit

```csharp
public interface IEntityInit : IEntityBehaviour
```

- **Description:** Represents a behavior interface that executes logic when an [IEntity](../Entities/IEntity.md) is
  initialized. It is automatically invoked by the entity’s `Init` method during its transition to the initialized state.
- **Inheritance:** [IEntityBehaviour](IEntityBehaviour.md)
- **See also:** [IEntityInit&lt;E&gt;](IEntityInit%601.md)

---

## 🏹 Methods

#### `Init(IEntity)`

```csharp
public void Init(IEntity entity);
```

- **Description:** Called when the entity is initialized.
- **Parameter:** `entity` – The entity being initialized.
- **Remarks:** This method is automatically called by `IEntity.Init` when the entity transitions into its initialized
  state, such as after construction or deserialization.

---

## 🗂 Example of Usage

Set up a `Color` for the entity `Renderer`

```csharp
public class InitColorBehaviour : IEntityInit
{
    public void Init(IEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        var color = entity.GetValue<Color>("Color");
        renderer.material.color = color;
    }
}
```

> Note: `GetValue<T>` assumes the entity has these values already set.