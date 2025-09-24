🧩 CompositeAction&lt;T1, T2&gt;

```csharp
[Serializable]
public class CompositeAction<T1, T2> : IAction<T1, T2>
```

- **Description:** Represents a group of actions <b>with two parameters</b> that are executed sequentially.
- **Inheritance:** [IAction&lt;T1, T2&gt;](IAction%602.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
- **Notes:** Supports Unity serialization and Odin Inspector

---

## 🏗️ Constructors

#### `CompositeAction()`

```csharp
public CompositeAction()
```

- **Description:** Initializes a new instance
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.

#### `CompositeAction(params IAction<T1, T2>[])`

```csharp
public CompositeAction(params IAction<T1, T2>[] actions)
```

- **Description:** Initializes a new instance with the specified array of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `CompositeAction(IEnumerable<Action<T1, T2>)`

```csharp
public CompositeAction(IEnumerable<IAction<T1, T2>> actions)
```

- **Description:** Initializes a new instance with the specified collection of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

## 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2)
```

- **Description:** Invokes all actions sequentially with the given arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument

---

## 🗂 Example of Usage

```csharp
IAction<int, int> composite = new CompositeAction<int, int>(
    new InlineAction<int, int>((a, b) => Console.WriteLine(a + b)),
    new InlineAction<int, int>((a, b) => Console.WriteLine(a * b))
);z

composite.Invoke(3, 4);

// Output:
// 7
// 12
```