# 🧩 SceneActionDefault

The **SceneActionDefault** class implements the [IAction](IAction.md) interface and inherits from [SceneActionAbstract](SceneActionAbstract.md). It allows game designers to build **composite actions directly in the Unity scene** — chaining multiple `IAction` instances (including generic variants like `IAction<T>`) without writing additional code.

In essence, `SceneActionDefault` acts as a **container of actions**, executing them sequentially as configured in the **Inspector**.

> [!NOTE]  
> Actions are executed in the order they appear in the array.  
> Null references are automatically skipped, making partially configured lists safe to use.

---

## 🧩 SceneActionDefault
```csharp
public class SceneActionDefault : SceneActionAbstract
```
- **Description:** Represents a **parameterless composite scene action**.
- **Usage:** Attach to a `GameObject`, assign a list of `IAction` implementations in the `Inspector`, and they will be invoked sequentially.

### Inspector Settings

| Parameter | Type        | Description                             |
|-----------|-------------|-----------------------------------------|
| `actions` | `IAction[]` | The array of actions to execute in order|

### Methods

#### `Invoke()`
```csharp
public override void Invoke();
```
- **Description:** Executes each action in the `actions` array sequentially.

### 🗂 Example of Usage

<img src="../../Images/PlayerEventTrigger.png" alt="img.png" width="408" height="162">


```csharp

```

!!!
public class HelloWorldAction : IAction
{
public void Invoke() => Debug.Log("Hello World!");
}
!!!

!!!
public sealed class StartupSequence : MonoBehaviour
{
[SerializeField] private SceneActionDefault _startup;

    private void Start()
    {
        _startup.Invoke();
    }
}
!!!

---

## 🧩 SceneActionDefault&lt;T&gt;

!!!
public abstract class SceneActionDefault<T> : SceneActionAbstract<T>
!!!
- **Description:** Represents a scene-based composite action with **one parameter**.
- **Type parameter:** `T` — the input argument type.

### Fields
- `IAction<T>[] actions` — the array of actions to execute.

### Methods

#### `Invoke(T arg)`
!!!
public override void Invoke(T arg);
!!!
- **Description:** Executes each action sequentially with the provided argument.

---

## 🧩 SceneActionDefault&lt;T1, T2&gt;

!!!
public abstract class SceneActionDefault<T1, T2> : SceneActionAbstract<T1, T2>
!!!
- **Description:** Represents a scene-based composite action with **two parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

### Fields
- `IAction<T1, T2>[] actions` — the array of actions to execute.

### Methods

#### `Invoke(T1 arg1, T2 arg2)`
!!!
public override void Invoke(T1 arg1, T2 arg2);
!!!
- **Description:** Executes each action sequentially with the provided arguments.

---

## 🧩 SceneActionDefault&lt;T1, T2, T3&gt;

!!!
public abstract class SceneActionDefault<T1, T2, T3> : SceneActionAbstract<T1, T2, T3>
!!!
- **Description:** Represents a scene-based composite action with **three parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

### Fields
- `IAction<T1, T2, T3>[] actions` — the array of actions to execute.

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`
!!!
public override void Invoke(T1 arg1, T2 arg2, T3 arg3);
!!!
- **Description:** Executes each action sequentially with the provided arguments.

---

## 🧩 SceneActionDefault&lt;T1, T2, T3, T4&gt;

!!!
public abstract class SceneActionDefault<T1, T2, T3, T4> : SceneActionAbstract<T1, T2, T3, T4>
!!!
- **Description:** Represents a scene-based composite action with **four parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

### Fields
- `IAction<T1, T2, T3, T4>[] actions` — the array of actions to execute.

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`
!!!
public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
!!!
- **Description:** Executes each action sequentially with the provided arguments.
