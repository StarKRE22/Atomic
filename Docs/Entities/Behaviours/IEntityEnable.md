# 🧩️ IEntityEnable

```csharp
public interface IEntityEnable : IEntityBehaviour
```

- **Description:** Represents a behavior interface that executes logic when an [IEntity](../Entities/IEntity.md) **is enabled**. 
- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked by the entity’s `Enable` method during its transition to the active state, such as after spawning or resuming from a disabled state.
- **See also:** [IEntityEnable&lt;E&gt;](IEntityEnable%601.md)

---

## 🏹 Methods

#### `Enable(IEntity)`

```csharp
public void Enable(IEntity entity);
```

- **Description:** Called when the entity is enabled.
- **Parameter:** `entity` – The entity being enabled.
- **Remarks:** This method is automatically called by `IEntity.Enable` when the entity transitions into its active state.

---

## 🗂 Example of Usage

Enable a `Renderer` component

```csharp
public class EnableRendererBehaviour : IEntityEnable
{
    public void Enable(IEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = true;
    }
}
```

> Note: `GetValue<T>` assumes the entity has a `Renderer` component already set.