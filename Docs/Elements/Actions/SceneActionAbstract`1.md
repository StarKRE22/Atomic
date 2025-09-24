
<details>
  <summary>
    <h2>🧩 SceneActionAbstract&lt;T&gt;</h2>
    <br> Represents a scene action with <b>one parameter</b> that can be invoked.
  </summary>

<br>

```csharp
public abstract class SceneActionAbstract<T> : MonoBehaviour, IAction<T>
```

- **Type parameter:** `T` — the input argument type.

---

### 🏹 Methods

#### `Invoke(T arg)`

```csharp
public abstract void Invoke(T arg);
```

- **Description:** Executes the action logic with the provided argument.
- **Parameter:** `arg` – The input argument.

---

### 🗂 Example of Usage

This example shows how to use `SceneActionAbstract<T>` to create an action that destroys objects when they enter a
trigger.

#### 1. Create `DestroyGameObjectAction`

This action takes a `GameObject` and destroys it:

```csharp
public sealed class DestroyGameObjectAction : SceneActionAbstract<GameObject>
{
    public override void Invoke(GameObject go) => GameObject.Destroy(go);
}
```

#### 2. Create `ActionTrigger`

This script invokes the action whenever another object enters the trigger collider:

```csharp
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

Enter **Play Mode** in Unity and any objects that collide with the trigger will be **destroyed automatically**.

</details>