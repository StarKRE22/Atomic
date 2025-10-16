# 🧩 IEntityInit&lt;E&gt;

Provides a strongly-typed version of [IEntityInit](IEntityInit.md) for handling initialization logic for a specific
entity type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Init(E)](#inite)

---

## 🗂 Example of Usage

Assume we have a concrete entity type:

```csharp
public class UnitEntity : Entity
{
}
```

Set up a `Color` for the `Renderer` of unit entity

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityInit<in E> : IEntityInit where E : IEntity
```

- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inheritance:** [IEntityInit](IEntityInit.md)
- **Note:** This method is automatically invoked by `IEntity.Init` when the behavior is registered on an entity of
  type `E`.

---

### 🏹 Methods

#### `Init(E)`

```csharp
public void Init(E entity);
```

- **Description:** Called when the typed entity is initialized.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityInit.Init(IEntity)` explicitly by casting the `IEntity` to type `E`.