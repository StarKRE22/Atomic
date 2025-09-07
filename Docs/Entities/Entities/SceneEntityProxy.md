# 🧩 SceneEntityProxy

`SceneEntityProxy` is a Unity component that acts as a proxy or reference to an existing `SceneEntity`. It allows multiple `GameObjects` to share and reference the same entity instance, enabling flexible entity architectures.

---

## Key Features

- **Entity Reference** – Points to an existing SceneEntity
- **Proxy Pattern** – Multiple proxies can reference one entity
- **Inspector Configuration** – Set entity reference in Unity Editor
- **Delegation** – Forwards IEntity interface calls to target

---

## Properties

| Property      | Type     | Description                                          |
|---------------|----------|------------------------------------------------------|
| `Source`      | `E`      | The source entity that this proxy forwards calls to. |
| `InstanceID`  | `int`    | The instance ID of the source entity.                |
| `Name`        | `string` | The name of the source entity.                       |
| `Initialized` | `bool`   | True if the source entity is initialized.            |
| `Enabled`     | `bool`   | True if the source entity is enabled.                |

---

## Lifecycle Methods

| Method                    | Description                                                 |
|---------------------------|-------------------------------------------------------------|
| `Init()`                  | Delegates initialization to the source entity.              |
| `Enable()`                | Delegates enabling to the source entity.                    |
| `Disable()`               | Delegates disabling to the source entity.                   |
| `Dispose()`               | Delegates cleanup and disposal to the source entity.        |
| `OnUpdate(float dt)`      | Delegates `Update` calls to all `IEntityUpdate` behaviours. |
| `OnFixedUpdate(float dt)` | Delegates `FixedUpdate` calls.                              |
| `OnLateUpdate(float dt)`  | Delegates `LateUpdate` calls.                               |

---

## Tags, Values & Behaviours

`SceneEntityProxy` fully supports working with tags, values, and behaviours by delegating:

- `AddTag`, `DelTag`, `HasTag`, `ClearTags`, `GetTags`, `GetTagEnumerator`
- `AddValue`, `SetValue`, `GetValue`, `TryGetValue`, `DelValue`, `ClearValues`, `GetValueEnumerator`
- `AddBehaviour`, `DelBehaviour`, `GetBehaviour`, `GetBehaviours`, `ClearBehaviours`, `GetBehaviourEnumerator`

---

## Events

| Event                                              | Description                              |
|----------------------------------------------------|------------------------------------------|
| `OnStateChanged`                                   | Triggered when the entity state changes. |
| `OnInitialized`                                    | Delegated from the source entity.        |
| `OnEnabled`                                        | Delegated from the source entity.        |
| `OnDisabled`                                       | Delegated from the source entity.        |
| `OnDisposed`                                       | Delegated from the source entity.        |
| `OnUpdated(float dt)`                              | Delegated from the source entity.        |
| `OnFixedUpdated(float dt)`                         | Delegated from the source entity.        |
| `OnLateUpdated(float dt)`                          | Delegated from the source entity.        |
| `OnTagAdded`, `OnTagDeleted`                       | Delegated from the source entity.        |
| `OnValueAdded`, `OnValueDeleted`, `OnValueChanged` | Delegated from the source entity.        |
| `OnBehaviourAdded`, `OnBehaviourDeleted`           | Delegated from the source entity.        |

---

## Using Child Colliders

`SceneEntityProxy` works seamlessly with entities that have multiple child colliders (e.g., hitboxes, triggers).  
By placing a proxy on each child collider, you can ensure that interactions such as raycasts, triggers, or hits always reference the same logical entity, regardless of which physical collider was involved.

### Benefits

- Unified access to the entity through any collider.
- Simplifies hit detection in complex entity setups.
- Eliminates the need for manual mapping between colliders and entities.
- Works for both generic (`SceneEntityProxy<E>`) and non-generic (`SceneEntityProxy`) proxies.

### Example

```csharp
void OnTriggerEnter(Collider other)
{
    if (other.TryGetComponent(out IEntity proxy)) // proxy is SceneEntityProxy
    {
        Debug.Log($"Hit entity: {proxy.Name}");
        proxy.AddTag(Tag.Hit);
    }
}
```
> This approach ensures that all colliders contribute to the same entity logic, making entity management consistent and modular.