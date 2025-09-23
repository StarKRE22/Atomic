# 🧩️ IEntityGizmos Interfaces

Represents a behavior interface that allows drawing gizmos for an [IEntity](../Entities/IEntity.md) during the **editor or debug rendering phase**. It is automatically invoked by `SceneEntity.OnDrawGizmos()` or `SceneEntity.OnDrawGizmosSelected()` in the Unity Editor, allowing visualization of entity data in the scene view.

<details>
  <summary>
    <h2 id="entity-gizmos"> 🧩 IEntityGizmos</h2>
    <br>Defines a behavior that draws gizmos for an <code>IEntity</code>.
  </summary>

<br>

```csharp
public interface IEntityGizmos : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)

---

### 🏹 Methods

#### `DrawGizmos(IEntity)`

```csharp
public void DrawGizmos(IEntity entity);
```

- **Description:** Draws editor or debug gizmos for the entity.
- **Parameter:** `entity` – The entity to visualize.
- **Remarks:** Automatically called by `SceneEntity.OnDrawGizmos()` or `SceneEntity.OnDrawGizmosSelected()` in the Unity Editor.

---

### 🗂 Example of Usage

Draw a debug sphere at the entity’s position

```csharp
public class DrawSphereGizmo : IEntityGizmos
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

> Note: Assumes the entity has a `Position` value set.

</details>

---

<details>
  <summary>
    <h2 id="entity-gizmos-t"> 🧩 IEntityGizmos&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that draws gizmos for an <code>IEntity</code> of type <code>E</code>.
  </summary>

<br>

```csharp
public interface IEntityGizmos<in E> : IEntityGizmos where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityGizmos` for handling gizmo drawing on a specific entity type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityGizmos](#entity-gizmos)
- **Remarks:** Automatically invoked by Unity Editor gizmo methods on entities of type `E`.

---

## 🏹 Methods

#### `DrawGizmos(E)`

```csharp
public void DrawGizmos(E entity);
```

- **Description:** Draws gizmos for the strongly-typed entity.
- **Parameter:** `entity` – The strongly-typed entity.
- **Remarks:** Implements the base `IEntityGizmos.DrawGizmos(IEntity)` explicitly by casting to type `E`.

---

### 🗂 Example of Usage

Draw a debug sphere for a `UnitEntity`

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class DrawSphereGizmo : IEntityGizmos<UnitEntity>
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

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

</details>

---

## 📝 Notes

- **Editor Visualization** – Intended for editor/debug visualization only.
- **Strongly-Typed Option** – `IEntityGizmos<E>` allows type-specific gizmo logic.
- **Integration** – Automatically invoked by Unity Editor methods (`OnDrawGizmos`, `OnDrawGizmosSelected`).
- **Composable** – Can be combined with other gizmo behaviours to visualize multiple entity aspects simultaneously.
- **Editor Only** – Works only in the Unity Editor