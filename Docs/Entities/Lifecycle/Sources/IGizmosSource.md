# 🧩 IGizmosSource

**IGizmosSource** is a lifecycle contract that exposes the `OnGizmosDraw` event. It is used by entity or world
implementations that support drawing gizmos in the Unity Editor.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [See Also](#-see-also)

---

## 🧩 Overview

Gizmos are visual debugging aids drawn in the Unity Scene view. `IGizmosSource` defines a minimal interface for objects
that want to expose gizmo drawing through an event-based API. Subscribers can attach drawing logic without inheriting
from `MonoBehaviour` or overriding `OnDrawGizmos` directly.

> **Note:** This interface is available only in the Unity Editor (`UNITY_EDITOR`).

---

## 🗂 Examples of Usage

### Subscribe to Gizmo Drawing

```csharp
IGizmosSource gizmosSource = ...; // e.g. an entity or world that implements IGizmosSource

gizmosSource.OnGizmosDraw += () =>
{
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, 1f);
};
```

### Implementation in a Behaviour

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

### 🏹 Events

#### `OnGizmosDraw`

```csharp
event Action OnGizmosDraw;
```

- **Description:** Raised when gizmos should be drawn in the Unity Editor.

---

## 🔗 See Also

- [IEntityGizmos](../Behaviours/IEntityGizmos.md) — behaviour interface for per-entity gizmo drawing.
- [MonoEntityGizmos](../Entities/MonoEntityGizmos.md) — Unity integration for entity gizmos.
