# 🧩 IEntityDispose&lt;E&gt;

Provides a strongly-typed version of [IEntityDispose](IEntityDispose.md) for handling disposal logic for a
specific entity type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Dispose(E)](#disposee)

---

## 🗂 Example of Usage

Assume we have a concrete entity type:

```csharp
public class UnitEntity : Entity
{
}
```

Dispose a `Collider` component

```csharp
public class DisposeColliderBehaviour : IEntityDispose<UnitEntity>
{
    public void Dispose(UnitEntity entity)
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
public interface IEntityDispose<in E> : IEntityDispose where E : IEntity
```

- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inheritance:** [IEntityDispose](IEntityDispose.md)
- **Note:** Automatically invoked by `IEntity.Dispose` when the behavior is registered on an entity of type `E`.

---

### 🏹 Methods

#### `Dispose(E)`

```csharp
public void Dispose(E entity);
```

- **Description:** Called when the typed entity is being disposed.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityDispose.Dispose(IEntity)` explicitly by casting the `IEntity` to type `E`.