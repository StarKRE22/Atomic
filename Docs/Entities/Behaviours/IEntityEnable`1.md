# 🧩 IEntityEnable&lt;E&gt;

Provides a strongly-typed version of [IEntityEnable](IEntityEnable.md) for handling enable logic for a specific
entity type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Enable(E)](#enablee)

---

## 🗂 Example of Usage

Assume we have a concrete entity type:

```csharp
public class UnitEntity : Entity
{
}
```

Enable a `Renderer` for a unit entity

```csharp
public class EnableRendererBehaviour : IEntityEnable<UnitEntity>
{
    public void Enable(UnitEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = true;
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityEnable<in E> : IEntityEnable where E : IEntity
```

- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityEnable](IEntityEnable.md)
- **Remarks:** Automatically invoked by `IEntity.Enable` when the behavior is registered on an entity of type `E`.

---

### 🏹 Methods

#### `Enable(E)`

```csharp
public void Enable(E entity);
```

- **Description:** Called when the typed entity is enabled.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityEnable.Enable(IEntity)` explicitly by casting the `IEntity` to type `E`.