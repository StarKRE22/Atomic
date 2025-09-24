# 🧩 CompositeAction

```csharp
[Serializable]
public class CompositeAction : IAction
```
- **Description:** Represents a group of **parameterless actions** that are executed sequentially.
- **Inheritance:** [IAction](IAction.md)
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors

#### `CompositeAction()`

```csharp
public CompositeAction()
```

- **Description:** Initializes a new instance
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.

#### `CompositeAction(params IAction[])`

```csharp
public CompositeAction(params IAction[] actions)
```

- **Description:** Initializes a new instance with the specified array of actions.
- **Parameter:** `actions` – One or more actions to include in the group.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `CompositeAction(IEnumerable<IAction>)`

```csharp
public CompositeAction(IEnumerable<IAction> actions)
```

- **Description:** Initializes a new instance with the specified collection of actions.
- **Parameter:** `actions` – A collection of actions to include in the group.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke()
```

- **Description:** Invokes all actions in the group sequentially.

---

### 🗂 Example of Usage

```csharp
var composite = new CompositeAction(
    new InlineAction(() => Console.WriteLine("Action 1")),
    new InlineAction(() => Console.WriteLine("Action 2"))
);

composite.Invoke();

// Output:
// Action 1
// Action 2
```
