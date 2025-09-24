# 🧩 CompositeAction&lt;T1, T2, T3, T4&gt;

```csharp
[Serializable]
public class CompositeAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Description:** Represents a group of actions <b>with four parameters</b> that are executed sequentially.
- **Inheritance:** [IAction&lt;T1, T2, T3, T4&gt;](IAction%604.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument
- **Notes:** Supports Unity serialization and Odin Inspector

---

## 🏗️ Constructors

#### `CompositeAction()`

- **Description:** Initializes a new instance
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.

#### `CompositeAction(params IAction<T1, T2, T3, T4>[])`

```csharp
public CompositeAction(params IAction<T1, T2, T3, T4>[] actions)
```

- **Description:** Initializes a new instance with the specified actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `CompositeAction(IEnumerable<T1, T2, T3, T4>)`

```csharp
public CompositeAction(IEnumerable<IAction<T1, T2, T3, T4>> actions)
```

- **Description:** Initializes a new instance with the specified actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

## 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Invokes all actions sequentially with the given arguments.
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument
    - `arg4` — the fourth argument