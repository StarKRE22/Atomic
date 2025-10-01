# 🧩️ IEntityDispose

```csharp
public interface IEntityDispose : IEntityBehaviour
```

- **Description:** Represents a behavior interface that executes cleanup or resource release logic when
  an [IEntity](../Entities/IEntity.md) **is being disposed**. It is automatically invoked by the entity’s `Dispose`
  method when the entity is permanently destroyed, removed from the game, or otherwise released from use.
- **Inheritance:** [IEntityBehaviour](IEntityBehaviour.md)
- **See also:** [IEntityDispose&lt;E&gt;](IEntityDispose%601.md)

---

## 🏹 Methods

#### `Dispose(IEntity)`

```csharp
public void Dispose(IEntity entity);
```

- **Description:** Called when the entity is being disposed.
- **Parameter:** `entity` – The entity being disposed.
- **Remarks:** Automatically called by `IEntity.Dispose` when the entity is permanently removed or released.

---

## 🗂 Example of Usage

Dispose a `Collider` component

```csharp
public class DisposeColliderBehaviour : IEntityDispose
{
    public void Dispose(IEntity entity)
    {
        var collider = entity.GetValue<Collider>("Collider");
        Object.Destroy(collider);
    }
}
```

> Note: `GetValue<T>` assumes the entity has a `Collider` component already set.