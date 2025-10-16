# 🧩️ IEntityDispose

Represents a behavior interface that executes cleanup or resource release logic when
an [IEntity](../Entities/IEntity.md) **is being disposed**.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Dispose(IEntity)](#disposeientity)

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityDispose : IEntityBehaviour
```

- **Inheritance:** [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked by the entity’s `Dispose`
  method when the entity is permanently destroyed, removed from the game, or otherwise released from use.
- **See also:** [IEntityDispose&lt;E&gt;](IEntityDispose%601.md)

---

### 🏹 Methods

#### `Dispose(IEntity)`

```csharp
public void Dispose(IEntity entity);
```

- **Description:** Called when the entity is being disposed.
- **Parameter:** `entity` – The entity being disposed.
- **Remarks:** Automatically called by `IEntity.Dispose` when the entity is permanently removed or released.