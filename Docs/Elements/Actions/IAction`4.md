
<details>
  <summary>
    <h2>🧩 IAction&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents an executable action that <b>takes four arguments</b>.
  </summary>

<br>

```csharp
public interface IAction<in T1, in T2, in T3, in T4>
```

- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument
    - `arg4` — the fourth argument

---

### 🗂 Example of Usage

```csharp
public sealed class MoveTransformAction : IAction<Transform, Vector3, float, float>
{
    public void Invoke(Transform transform, Vector3 direction, float speed, float deltaTime) 
    {
        transform.position += direction * (speed * deltaTime);
    }
}

// Usage
IAction<Transform, Vector3, float, float> action = new MoveTransformAction();
action.Invoke(transform, Vector3.forward, 10, 0.02);
```

</details>