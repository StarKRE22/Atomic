# 🧩 IEntityGizmos&lt;E&gt;

Provides a strongly-typed version of [IEntityGizmos](IEntityGizmos.md) for handling gizmo drawing on a specific entity
type.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [DrawGizmos(E)](#drawgizmose)

---

## 🗂 Example of Usage

Assume we have a concrete entity type:

```csharp
public class UnitEntity : Entity
{
}
```

Draw a debug sphere for a `UnitEntity`

```csharp
public class DrawSphereGizmos : IEntityGizmos<UnitEntity>
{
    public void DrawGizmos(UnitEntity entity)
    {
        Vector3 position = entity.GetValue<Vector3>("Position");
        float scale = entity.GetValue<float>("Scale");
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.5f);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityGizmos<in E> : IEntityGizmos where E : IEntity
```

- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityGizmos](IEntityGizmos.md)
- **Note:** Automatically invoked by Unity Editor gizmo methods on entities of type `E`.

---

### 🏹 Methods

#### `DrawGizmos(E)`

```csharp
public void DrawGizmos(E entity);
```

- **Description:** Draws gizmos for the strongly-typed entity.
- **Parameter:** `entity` – The strongly-typed entity.
- **Remarks:** Implements the base `IEntityGizmos.DrawGizmos(IEntity)` explicitly by casting to type `E`.