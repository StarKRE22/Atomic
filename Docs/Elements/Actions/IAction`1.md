
<details>
  <summary>
    <h2>🧩 IAction&lt;T&gt;</h2>
    <br> Represents an executable action that <b>takes one argument</b>.
  </summary>

<br>

```csharp
public interface IAction<in T>
```

- **Type parameter:** `T` — the input parameter

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
void Invoke(T arg);
```

- **Description:** Executes the action with the specified argument
- **Parameter:** `arg` — the input parameter

---

### 🗂 Example of Usage

```csharp
public sealed class DestroyGameObjectAction : IAction<GameObject>
{
    public void Invoke(GameObject go) => GameObject.Destroy(go);
}

// Usage
IAction<GameObject> action = new DestroyGameObjectAction();
action.Invoke(gameObject);
```

</details>