# 🧩 SceneEntityProxy

Unity component that acts as a proxy or reference to an existing [SceneEntity](SceneEntity.md).
It allows multiple `GameObjects` to share and reference the same entity instance, enabling flexible entity
architectures.

---

### 🔹 Generic Version

```csharp
public abstract class SceneEntityProxy<E> : MonoBehaviour, IEntity
    where E : SceneEntity
```

- **Description:** Represents a proxy that forwards [IEntity](IEntity.md) calls to an underlying `E` source entity
- **Type Parameter:** `E` — The type of the source entity, must inherit from [SceneEntity](SceneEntity.md)
- **Inheritance:**
    - extends `MonoBehaviour`
    - implements [IEntity](IEntity.md)

---

### 🔹 Non-Generic Version

```csharp
public class SceneEntityProxy : SceneEntityProxy<SceneEntity>
```

- **Description:** Non-generic proxy component for exposing and interacting with a `SceneEntity` in the Unity scene.
- **Inheritance:** extends `SceneEntityProxy<E>`

---

## 🛠 Inspector Settings

| Parameter | Description                                                        |
|-----------|--------------------------------------------------------------------|
| `source`  | Reference to the actual `SceneEntity` object that this proxy wraps |

## 🔑 Properties

#### `Source`
```csharp
public E Source { get; }
```
- **Description:** The source entity that this proxy forwards calls to.

---

## 🗂 Example of Usage

`SceneEntityProxy` works seamlessly with entities that have multiple child colliders (e.g., hitboxes, triggers). By placing a proxy on each child collider, you can ensure that interactions such as raycasts, triggers, or hits always
reference the same logical entity, regardless of which physical collider was involved.

#### 1. Create a new `GameObject`

<img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### 2. Add `Entity` Component to the GameObject

<img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

#### 3. Add `Entity Proxy` and `Collider` for a child GameObject of the entity

<img width="350" height="" alt="Entity component" src="../../Images/EntityProxy.png" />

#### 4. Create `TriggerExample` script

```csharp
public class TriggerExample : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEntity proxy)) // proxy is SceneEntityProxy
            Debug.Log($"Hit entity: {proxy.Name}");
    }
}
```

#### 5. When your entity enters the trigger, it will be logged in the console.

> This approach ensures that all colliders contribute to the same entity logic, making entity management consistent and
> modular.

### Benefits

- Unified access to the entity through any collider.
- Simplifies hit detection in complex entity setups.
- Eliminates the need for manual mapping between colliders and entities.
- Works for both generic (`SceneEntityProxy<E>`) and non-generic (`SceneEntityProxy`) proxies.

---

## 📝 Notes

- **Entity Reference** – Points to an existing `SceneEntity`
- **Delegation** – Forwards `IEntity` interface calls to target
- **Proxy Pattern** – Multiple proxies can reference one entity
- **Inspector Configuration** – Set entity reference in Unity Editor