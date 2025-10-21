# 🧩 SceneEntityProxy

Represents non-generic proxy component for exposing and interacting with
a [SceneEntity](SceneEntity.md) in the Unity scene. Supports Odin Inspector

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Properties](#-properties)
    - [Source](#source)

---

## 🗂 Example of Usage

1. Assume we have a **Collider** component to the child GameObject.

<img width="150" height="" alt="GameObject creation" src="../../Images/ChildCollider.png" />

2. Attach a **SceneEntityProxy** component to the same GameObject.

<img width="400" height="" alt="GameObject creation" src="../../Images/EntityProxy.png" />

3. In the `SceneEntityProxy`, assign the parent entity reference to the `Source` field.

4. This way, whenever another collider interacts with the child collider, you can easily retrieve the parent entity from it.

```csharp
public class SomeTrigger : MonoBehaviour 
{
    private void OnTriggerEnter(Collider collider)
    {
         // Access the parent entity through the proxy
        if (collider.TryGetComponent(out IEntity)) // Proxy
        {
            // Do something
        }
    }
}
```

---

## 🛠 Inspector Settings

| Parameter | Description                                                        |
|-----------|--------------------------------------------------------------------|
| `source`  | Reference to the actual `SceneEntity` object that this proxy wraps |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AddComponentMenu("Atomic/Entities/Entity Proxy")]
[DisallowMultipleComponent]
public class SceneEntityProxy : SceneEntityProxy<SceneEntity>
```

- **Inheritance:** [SceneEntityProxy&lt;E&gt;](SceneEntityProxy%601.md)

---

### 🔑 Properties

#### `Source`

```csharp
public SceneEntity Source { get; }
```

- **Description:** The source entity that this proxy forwards calls to.