
<details>
  <summary>
    <h2>🧩 InlineAction&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents an action <b>with four parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public class InlineAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Type parameters**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

---

### 🏗️ Constructors

#### `InlineAction(Action<T1, T2, T3, T4> action)`

```csharp
public InlineAction(Action<T1, T2, T3, T4> action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4>)`

```csharp
public static implicit operator InlineAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action);
```

- **Description:** Implicitly converts a delegate of type `Action<T1, T2, T3, T4>` to a `InlineAction<T1, T2, T3, T4>`.
- **Type Parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the third argument
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction<T1, T2, T3, T4>` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
var moveAction = new InlineAction<Transform, Vector3, float, float>(
    (transform, direction, speed, deltaTime) => transform.position += direction * (speed * deltaTime)    
);
moveAction.Invoke(transform, Vector3.forward, 10, 0.02);
```

</details>