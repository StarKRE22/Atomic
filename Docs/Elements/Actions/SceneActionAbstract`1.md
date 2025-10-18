# 🧩 SceneActionAbstract&lt;T&gt;

Represents a scene action with <b>one parameter</b> that can be invoked.

---

## 📑 Table of Contents

- [Quick Start](#-quick-start)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🚀 Quick Start

This example shows how to use `SceneActionAbstract<T>` to create an action that destroys objects when they enter a
trigger.

#### 1. Create `DestroyGameObjectAction`


```csharp
// This action takes a `GameObject` and destroys it:
public sealed class DestroyGameObjectAction : SceneActionAbstract<GameObject>
{
    public override void Invoke(GameObject go) => GameObject.Destroy(go);
}
```

#### 2. Create `ActionTrigger`

```csharp
// This script invokes the action whenever another object enters the trigger collider:
public sealed class ActionTrigger : MonoBehaviour
{
    [SerializeField]
    private SceneActionAbstract<GameObject> _action;

    private void OnTriggerEnter(Collider collider)
    {
        _action.Invoke(collider.gameObject);
    }
}
```

#### 3. Run the scene

- Enter **Play Mode** in Unity and any objects that collide with the trigger will be **destroyed automatically**.

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class SceneActionAbstract<T> : MonoBehaviour, IAction<T>
```

- **Description:** Represents a scene action with <b>one parameter</b> that can be invoked.
- **Inheritance:** `MonoBehaviour`, [IAction&lt;T&gt;](IAction%601.md)
- **Type parameter:** `T` — the input argument type.
- **Note:** Attach to a GameObject and implement `Invoke(T)` to define custom behavior.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public abstract void Invoke(T arg);
```

- **Description:** Executes the action logic with the provided argument.
- **Parameter:** `arg` – The input argument.