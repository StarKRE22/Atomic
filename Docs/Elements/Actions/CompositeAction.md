# 🧩 CompositeAction Classes

The **CompositeAction** classes represent **groups of actions** that implement the corresponding [IAction](IAction.md) interfaces.  

They follow the [Composite design pattern](https://en.wikipedia.org/wiki/Composite_pattern) — a `CompositeAction` both **groups actions** and itself **acts as a single action**, preserving a uniform interface.

This allows combining multiple actions into a sequence, which will be invoked **sequentially** when triggered. This is especially important when game objects and scripts need to execute complex action scenarios.

---

## 🧩 CompositeAction
```csharp
public class CompositeAction : IAction
```
- **Description:** Represents a group of **parameterless actions** that are executed sequentially.

### Constructors

#### `CompositeAction()`
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

### Methods

#### `Invoke()`
```csharp
public void Invoke()
```
- **Description:** Invokes all actions in the group sequentially.

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

---

## 🧩 CompositeAction&lt;T&gt;
```csharp
public class CompositeAction<T> : IAction<T>
```
- **Description:** Represents a group of actions with one parameter executed sequentially.
- **Type parameter:** `T` — the input parameter.

### Constructors

#### `CompositeAction()`
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

### Methods
```csharp
public void Invoke(T arg)
```
- **Description:** Invokes all actions sequentially with the given argument.
- **Parameter:** `arg` – The input argument.

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

---

## 🧩 CompositeAction<T1, T2>
```csharp
public class CompositeAction<T1, T2> : IAction<T1, T2>
```
- **Description:** Represents a group of actions with two parameters executed sequentially.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

### Constructors

#### `CompositeAction()`
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

### Methods
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

---

## 🧩 CompositeAction<T1, T2, T3>
```csharp
public class CompositeAction<T1, T2, T3> : IAction<T1, T2, T3>
```
- **Description:** Represents a group of actions with three parameters executed sequentially.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

### Constructors

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

### Methods
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

---

## 🧩 CompositeAction<T1, T2, T3, T4>
```csharp
public class CompositeAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```
- **Description:** Represents a group of actions with four parameters executed sequentially.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

### Constructors

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

### Methods
```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```
- **Description:** Invokes all actions sequentially with the given arguments.

---

## 🎛️ Using with `[SerializeReference]` and `Odin Inspector`

For **narrative or scenario-driven games**, where designers need to configure a lot of actions directly on the scene, `CompositeAction` combined with `[SerializeReference]` is very convenient.

It allows designers to visually chain multiple actions in the inspector without writing extra code. This is especially useful for quickly iterating on game logic or events.

### 🗂 Example of Usage

```csharp
using UnityEngine;
using Atomic.Elements;

public class PlayerTriggerEvent : MonoBehaviour
{
    [SerializeReference]
    private CompositeAction _eventActions;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Invoke all actions configured in the inspector
            _eventActions.Invoke();
        }
    }
}
```

<img src="../../Images/PlayerEventTrigger.png" alt="img.png" width="409" height="224">

> [!NOTE]
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code instead for clarity and maintainability, because `[SerializeReference]` is very fragile during refactoring.