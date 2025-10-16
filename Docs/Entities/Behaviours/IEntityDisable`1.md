#  🧩 IEntityDisable&lt;E&gt;

Provides a strongly-typed version of [IEntityDisable](IEntityDisable.md) for handling disable logic for a specific
entity type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Disable(E)](#disablee)

---

## 🗂 Example of Usage

Assume we have a concrete entity type:

```csharp
public class UnitEntity : Entity
{
}
```

Disable a `Renderer` for a unit entity:

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityDisable<in E> : IEntityDisable where E : IEntity
```

- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inheritance:** [IEntityDisable](IEntityDisable.md)
- **Note:** Automatically invoked by `IEntity.Disable` when the behavior is registered on an entity of type `E`.

---

### 🏹 Methods

#### `Disable(E)`

```csharp
public void Disable(E entity);
```

- **Description:** Called when the typed entity is disabled.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityDisable.Disable(IEntity)` explicitly by casting the `IEntity` to type `E`.