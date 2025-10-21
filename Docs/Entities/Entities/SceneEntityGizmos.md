# 🧩 SceneEntity Gizmos

Provides visual debugging support through Unity Gizmos in the Scene view.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)

---

## 🗂 Example of Usage

Below is an example of drawing a circle for a unit using its position and scale:

#### 1. Create Gizmos Behaviour

```csharp
public sealed class TransformGizmos : IEntityGizmos<IGameEntity>
{
    public void DrawGizmos(IGameEntity entity)
    {
        Vector3 center = entity.GetPosition().Value;
        float scale = entity.GetScale().Value;
        Handles.DrawWireDisc(center, Vector3.up, scale);
    }
}
```

#### 2. Attach `TransformGizmos` to an entity installer

```csharp
[Serializable]
public sealed class TransformEntityInstaller : SceneEntityInstaller<IGameEntity>
{
    [SerializeField]
    private Const<float> _scale = 1;
    
    public void Install(IGameEntity entity)
    {
        entity.AddPosition(new ReactiveVector3());
        entity.AddRotation(new ReactiveQuaternion());
        entity.AddScale(_scale);
        
       // Connect the gizmos drawing logic
        entity.AddBehaviour<TransformGizmos>();
    }
}
```

---

## 🛠 Inspector Settings

| Parameter            | Description                                                           |
|----------------------|-----------------------------------------------------------------------|
| `onlySelectedGizmos` | Draw gizmos only when this GameObject is selected. Default is `false` |
| `onlyEditModeGizmos` | Draw gizmos only when Unity is not in Play mode. Default is `false`   |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class SceneEntity
```