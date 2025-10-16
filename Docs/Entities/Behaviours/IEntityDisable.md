# 🧩️ IEntityDisable

Represents a behavior interface that executes logic when an [IEntity](../Entities/IEntity.md) **is
disabled**.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Disable(IEntity)](#disableientity)

---

## 🗂 Example of Usage

Disable a `Renderer` component

```csharp
public class DisableRendererBehaviour : IEntityDisable
{
    public void Disable(IEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = false;
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityDisable : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked by the entity’s `Disable` method when the entity exits the active state, such as
  during pause, unloading, or before being despawned.
- **See also:** [IEntityDisable&lt;E&gt;](IEntityDisable%601.md)

---

### 🏹 Methods

#### `Disable(IEntity)`

```csharp
public void Disable(IEntity entity);
```

- **Description:** Called when the entity is disabled.
- **Parameter:** `entity` – The entity being disabled.
- **Remarks:** Automatically called by `IEntity.Disable` when the entity exits the active state.