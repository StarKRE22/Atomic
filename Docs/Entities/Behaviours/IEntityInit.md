# 🧩️ IEntityInit

Represents a behavior interface that executes logic when an [IEntity](../Entities/IEntity.md) is
initialized.

---


## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Init(IEntity)](#initientity)

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityInit : IEntityBehaviour
```

- **Inheritance:** [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked by the entity’s `Init` method during its transition to the initialized state.
- **See also:** [IEntityInit&lt;E&gt;](IEntityInit%601.md)

---

### 🏹 Methods

#### `Init(IEntity)`

```csharp
public void Init(IEntity entity);
```

- **Description:** Called when the entity is initialized.
- **Parameter:** `entity` – The entity being initialized.
- **Remarks:** This method is automatically called by `IEntity.Init` when the entity transitions into its initialized
  state, such as after construction or deserialization.