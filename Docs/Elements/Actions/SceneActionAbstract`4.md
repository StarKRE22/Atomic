
<details>
  <summary>
    <h2>🧩 SceneActionAbstract&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a scene action with <b>four parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public abstract class SceneActionAbstract<T1, T2, T3, T4> : MonoBehaviour, IAction<T1, T2, T3, T4>
```

- **Description:** Represents a scene action with **four parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`

```csharp
public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes the action logic with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument

---

### 🗂 Example of Usage

This example demonstrates how to move a `Transform` in a given direction with a specified speed and deltaTime.

#### 1. Create `MoveTransformAction`

This action takes a **Transform**, a **direction vector**, a **speed**, and **deltaTime**, then moves the Transform
accordingly:

```csharp
public sealed class MoveTransformAction : SceneActionAbstract<Transform, Vector3, float, float>
{
    public void Invoke(Transform transform, Vector3 direction, float speed, float deltaTime) => 
        transform.position += direction * (speed * deltaTime);
}
```

#### 2. Usage in Gameplay

- Attach the `MoveTransformAction` to a GameObject.
- Call `Invoke(transform, direction, speed, deltaTime)` in an update loop or event to move the object over time.

#### 3. Result

The GameObject’s position will be updated every frame according to the specified direction and speed, allowing smooth
movement.

</details>
