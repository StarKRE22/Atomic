# 🧩 CompositeAction Classes

The **CompositeAction** classes represent **groups of actions** that implement the corresponding [IAction](IAction.md) interfaces. They follow the [Composite Pattern](https://en.wikipedia.org/wiki/Composite_pattern) — a `CompositeAction` both **groups actions** and itself **acts as a single action**, preserving a uniform interface.

This allows combining multiple actions into a sequence, which will be invoked **sequentially** when triggered. This is especially important when game objects and scripts need to execute complex action scenarios.

---

<hr style="height: 1px; border: none; background-color: #505050;">

<details>
  <summary>
    <h2>🧩 CompositeAction</h2>
    <br>  Represents a group of <b>parameterless actions</b> that are executed sequentially.
  </summary>

<br>

```csharp
public class CompositeAction : IAction
```

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

</details>

---

<details>
  <summary>
    <h2>🧩 CompositeAction&lt;T&gt;</h2>
    <br>  Represents a group of actions <b>with one parameter</b> that are executed sequentially.
  </summary>

<br>

```csharp
public class CompositeAction<T> : IAction<T>
```
- **Type parameter:** `T` — the input parameter.

### 🏗️ Constructors

#### `CompositeAction()`
```csharp
public CompositeAction()
```
- **Description:** Initializes a new instance
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.

#### `CompositeAction(params IAction<T>[])`
```csharp
public CompositeAction(params IAction<T>[] actions)
```
- **Description:** Initializes a new instance with the specified array of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `CompositeAction(IEnumerable<Action<T>)`
```csharp
public CompositeAction(IEnumerable<IAction<T>> actions)
```
- **Description:** Initializes a new instance with the specified collection of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

### 🏹 Methods

#### `Invoke(T arg)`
```csharp
public void Invoke(T arg)
```
- **Description:** Invokes all actions sequentially with the given argument.
- **Parameter:** `arg` – The input argument.

---

### 🗂 Example of Usage
```csharp
var composite = new CompositeAction<string>(
    new InlineAction<string>(msg => Console.WriteLine("Hello " + msg)),
    new InlineAction<string>(msg => Console.WriteLine("Bye " + msg))
);

composite.Invoke("World");

// Output:
// Hello World
// Bye World
```

</details>

---

<details>
  <summary>
    <h2>🧩 CompositeAction&lt;T1, T2&gt;</h2>
    <br>  Represents a group of actions <b>with two parameters</b> that are executed sequentially.
  </summary>

<br>

```csharp
public class CompositeAction<T1, T2> : IAction<T1, T2>
```
- **Type parameters:**
  - `T1` — the first argument
  - `T2` — the second argument

### 🏗️ Constructors

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

### 🏹 Methods
```csharp
public void Invoke(T1 arg1, T2 arg2)
```
- **Description:** Invokes all actions sequentially with the given arguments.

### 🗂 Example of Usage
```csharp
var composite = new CompositeAction<int, int>(
    new InlineAction<int, int>((a, b) => Console.WriteLine(a + b)),
    new InlineAction<int, int>((a, b) => Console.WriteLine(a * b))
);

composite.Invoke(3, 4);

// Output:
// 7
// 12
```

</details>

---

<details>
  <summary>
    <h2>🧩 CompositeAction&lt;T1, T2, T3&gt;</h2>
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

### 🏹 Methods
```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3)
```
- **Description:** Invokes all actions sequentially with the given arguments.

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

---

<details>
  <summary>
    <h2>🧩 CompositeAction&lt;T1, T2, T3, T4&gt;</h2>
    <br>  Represents a group of actions <b>with four parameters</b> that are executed sequentially.
  </summary>

<br>

```csharp
public class CompositeAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

### 🏗️ Constructors

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

### 🏹 Methods
```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```
- **Description:** Invokes all actions sequentially with the given arguments.

</details>

---

## ❗️Using [SerializeReference]

For **narrative or scenario-driven games**, where designers need to configure a lot of actions directly on the scene, `CompositeAction` combined with `[SerializeReference]` is very convenient. It allows designers to visually chain multiple actions in the inspector without writing extra code. This is especially useful for quickly iterating on game logic or events.

> [!WARNING]
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code instead for clarity and maintainability, because `[SerializeReference]` is very fragile during refactoring.

### 🗂 Example of Usage

Create a component that executes an action **when triggered by the player**. The specific action can be assigned by the designer directly in the **Inspector**.

```csharp
using UnityEngine;
using Atomic.Elements;

public sealed class PlayerActionTrigger : MonoBehaviour
{
    private const string PlayerTag = "Player";
    
    [SerializeReference] private IAction _action;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerTag))
            _action.Invoke();
    }
}
```

In the **Inspector**, we can assign the `CompositeAction` value to the `Action` parameter. 

For example, we can add [PrintAction](PrintAction.md) to the action array.

<img src="../../Images/PlayerActionTrigger_Composite.png" alt="Inspector setup example" width="390" height="164">