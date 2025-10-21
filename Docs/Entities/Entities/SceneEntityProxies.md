# 🧩 SceneEntity Proxies

Represent family of Unity components those act as a proxy or reference to an existing [SceneEntity](SceneEntity.md).
It allows multiple `GameObjects` to share and reference the same entity instance, enabling flexible entity
architectures.

---



## 📖 Handling child colliders with SceneEntityProxy

In some cases, a GameObject hierarchy may include **child colliders** that interact with other colliders. However, a
common problem arises — you often cannot directly access the **parent entity** from the child collider.


To solve this, you can attach a `SceneEntityProxy` component next to the child collider. The `SceneEntityProxy` serves
as a bridge, allowing you to reference the **parent entity** (or any other relevant
[IEntity](IEntity.md) source) directly.

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


There are both generic and non-generic versions of proxies.

- [SceneEntityProxy](SceneEntityProxy.md)
- [SceneEntityProxy&lt;T&gt;](SceneEntityProxy%601.md)

---

## 📝 Notes

- **Entity Reference** – Points to an existing `SceneEntity`
- **Delegation** – Forwards `IEntity` interface calls to target
- **Proxy Pattern** – Multiple proxies can reference one entity
- **Inspector Configuration** – Set entity reference in Unity Editor