# 🧩 IGizmosSource

A lifecycle contract that exposes the `OnGizmosDraw` event. Used by entity or world implementations that support drawing gizmos in the Unity Editor.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [OnGizmosDraw](#ongizmosdraw)

---

## 🗂 Example of Usage

Subscribe drawing logic without inheriting from `MonoBehaviour`:

```csharp
IGizmosSource gizmosSource = ...; // e.g. an entity or world that implements IGizmosSource

gizmosSource.OnGizmosDraw += () =>
{
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, 1f);
};
```

Use in a per-entity behaviour:

```csharp
public sealed class SelectionGizmoBehaviour : IEntityBehaviour, IEntityGizmos
{
    public void DrawGizmos(IEntity entity)
    {
        if (entity.HasTag("Selected"))
        {
            Vector3 position = entity.GetValue<Vector3>("Position");
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, 0.5f);
        }
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
#if UNITY_EDITOR
public interface IGizmosSource
{
    event Action OnGizmosDraw;
}
#endif
```

- **Description:** A lifecycle contract that exposes the `OnGizmosDraw` event.
- **Inheritance:** None
- **Notes:** Available only in the Unity Editor (`UNITY_EDITOR`).
- **See also:** [IEntityGizmos](../../Behaviours/IEntityGizmos.md)

---

### 🏹 Methods

#### `OnGizmosDraw`

```csharp
event Action OnGizmosDraw;
```

- **Description:** Raised when gizmos should be drawn in the Unity Editor.
- **Remarks:** Subscribers attach drawing logic that runs during Unity's gizmo drawing phase.
