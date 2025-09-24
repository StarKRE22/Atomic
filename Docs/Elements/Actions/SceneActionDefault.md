# 🧩 SceneActionDefault

```csharp
[AddComponentMenu("Atomic/Elements/Action")]
public class SceneActionDefault : SceneActionAbstract
```

- **Description:**  Represents a <b>parameterless</b> composite scene action that can be invoked.
- **Inheritance:** [SceneActionAbstract](SceneActionAbstract.md)
- **Notes:**
    - Supports Odin Inspector
    - Attach to a `GameObject`, assign a list of `IAction` implementations in the `Inspector`, and they will be
      invoked sequentially.

---

### 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

## 🧱 Fields

#### `actions`

```csharp
public IAction[] actions;
```

- **Description:** The array of actions to invoke in order.
- **Access:** Read / Write

---

## 🏹 Methods

#### `Invoke()`

```csharp
public override void Invoke();
```

- **Description:** Executes each action in the `actions` array sequentially.

---

## 🗂 Example of Usage

#### 1. Add the `Atomic/Elements/Action` component.

<img src="../../Images/SceneAction.png" alt="SceneAction example" width="384" height="137">

#### 2. In the **Inspector**, assign the `PrintAction` value to the `Action` parameter.

#### 3. Use `SceneActionDefault` as `SceneActionAbstract` in your components.

```csharp
// Example of usage "SceneActionDefault"
public sealed class GameStartup : MonoBehaviour
{
    [SerializeField] 
    private SceneActionAbstract _startup;

    private void Start() => _startup.Invoke();
}
```