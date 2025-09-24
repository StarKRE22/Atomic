
---

<details>
  <summary>
    <h2 id="composite-action-t1-t2-t3">🧩 CompositeAction&lt;T1, T2, T3&gt;</h2>
    <br>  Represents a group of actions <b>with three parameters</b> that are executed sequentially.
  </summary>

<br>

```csharp
public class CompositeAction<T1, T2, T3> : IAction<T1, T2, T3>
```

- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

---

### 🏗️ Constructors

#### `CompositeAction()`

- **Description:** Initializes a new instance
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.

#### `CompositeAction(params IAction<T1, T2, T3>[])`

```csharp
public CompositeAction(params IAction<T1, T2, T3>[] actions)
```

- **Description:** Initializes a new instance with the specified array of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `CompositeAction(IEnumerable<Action<T1, T2>)`

```csharp
public CompositeAction(IEnumerable<IAction<T1, T2, T3>> actions)
```

- **Description:** Initializes a new instance with the specified collection of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3)
```

- **Description:** Invokes all actions sequentially with the given arguments.

---

### 🗂 Example of Usage

```csharp
var composite = new CompositeAction<int, int, int>(
    new InlineAction<int, int>((a, b, c) => Console.WriteLine(a + b + c)),
    new InlineAction<int, int>((a, b, c) => Console.WriteLine(a * b * c))
);

composite.Invoke(3, 4, 2);

// Output:
// 14
// 24
```

</details>
