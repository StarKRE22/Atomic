# 🧩️ IEntityGizmos

Represents a behavior interface that allows drawing gizmos for an [IEntity](../Entities/IEntity.md) during the **editor
or debug rendering phase**.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [DrawGizmos(IEntity)](#drawgizmosientity)

---

## 🗂 Example of Usage

Draw a debug sphere at the entity’s position

```csharp
public class DrawSphereGizmos : IEntityGizmos
{
    public void DrawGizmos(IEntity entity)
    {
        Vector3 position = entity.GetValue<Vector3>("Position");
        float scale = entity.GetValue<float>("Scale");
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, scale);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityGizmos : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)
- **Note:** It is automatically invoked by `MonoEntity.OnDrawGizmos()` or `MonoEntity.OnDrawGizmosSelected()` in the
  Unity Editor, allowing visualization of entity data in the scene view.
- **See also:** [IEntityGizmos&lt;E&gt;](IEntityGizmos%601.md)

---

### 🏹 Methods

#### `DrawGizmos(IEntity)`

```csharp
public void DrawGizmos(IEntity entity);
```

- **Description:** Draws editor or debug gizmos for the entity.
- **Parameter:** `entity` – The entity to visualize.
- **Remarks:** Automatically called by `MonoEntity.OnDrawGizmos()` or `MonoEntity.OnDrawGizmosSelected()` in the Unity
  Editor.